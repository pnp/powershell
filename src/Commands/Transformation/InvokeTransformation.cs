using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.Framework.Modernization;
using System.Reflection;
using Microsoft.SharePoint.Client;
using System.IO;
using PnP.PowerShell.Commands.Base;
using PnP.Framework.Modernization.Transform;
using PnP.Framework.Modernization.Publishing;
using PnP.Framework.Modernization.Telemetry.Observers;
using PnP.Framework.Modernization.Cache;
using PnP.PowerShell.Commands.Attributes;
using System.Collections;
using System.Collections.Generic;
using PnP.Core.Transformation.SharePoint.Services;
using System.Linq;
using PnP.Core.Transformation.SharePoint.Services.Builder.Configuration;
using PnP.Core.Services;
using PnP.Core.Transformation.SharePoint;
using System.Text.RegularExpressions;

namespace PnP.PowerShell.Commands.Transformation
{

    [Cmdlet(VerbsLifecycle.Invoke, "PnPTransformation")]
    public class InvokeTransformation : PnPWebCmdlet
    {
        private static string rootFolder = "<root>";
        private static Regex baseUrlRegex = new Regex(@"(?<baseUrl>(http(s)?:\/\/(\w|\.)*\/))", RegexOptions.IgnoreCase);

        #region Source classic page

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ClassicPagePipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string Library;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string Folder;

        [Parameter(Mandatory = false)]
        public SwitchParameter PublishingPage = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter BlogPage = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter DelveBlogPage = false;

        #endregion

        #region Global transformation settings

        [Parameter(Mandatory = false)]
        public SwitchParameter DisableTelemetry = false;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string PersistenceProviderConnectionString;

        #endregion

        #region Target Page generation input arguments

        [Parameter(Mandatory = false)]
        public SwitchParameter CopyPageMetadata = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter KeepPageCreationModificationInformation = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter PostAsNews = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter PublishPage = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter DisablePageComments = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter KeepPageSpecificPermissions = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter Overwrite = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter ReplaceHomePageWithDefaultHomePage = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter SetAuthorInPageHeader = false;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string TargetPageFolder;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string TargetPageName;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string TargetPagePrefix;

        [Parameter(Mandatory = false)]
        public SwitchParameter TargetPageTakesSourcePageName = false;

        #endregion

        #region SharePoint Source input arguments

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string WebPartMappingFile;

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string PageLayoutMappingFile;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveEmptySectionsAndColumns = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter ShouldMapUsers = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter HandleWikiImagesAndVideos = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter AddTableListImageAsImageWebPart = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeTitleBarWebPart = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipHiddenWebParts = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipUrlRewrite = false;

        [Parameter(Mandatory = false)]
        public Hashtable MappingProperties;

        [Parameter(Mandatory = false)]
        public Hashtable UrlMappings;

        [Parameter(Mandatory = false)]
        public Hashtable UserMappings;

        #endregion

        #region SharePoint Target input arguments

        [Parameter(Mandatory = false)] // do not remove '#!#99'
        public PnPConnection TargetConnection = null;

        [Parameter(Mandatory = false)]
        public string TargetWebUrl;

        #endregion

        protected override void ExecuteCmdlet()
        {
            WriteWarning("Please consider that the PnP Transformation Framework is still under preview and there could be issues while executing this cdmlet. If you run into any issue, plese file a new item here: https://github.com/pnp/pnpcore/issues");

            #region Source page selection

            // Load the page to transform
            Identity.Library = this.Library;
            Identity.Folder = this.Folder;
            Identity.BlogPage = this.BlogPage;
            Identity.DelveBlogPage = this.DelveBlogPage;

            if ((this.PublishingPage && this.BlogPage) ||
                (this.PublishingPage && this.DelveBlogPage) ||
                (this.BlogPage && this.DelveBlogPage))
            {
                throw new Exception($"The page is either a blog page, a publishing page or a Delve blog page. Setting PublishingPage, BlogPage and DelveBlogPage to true is not valid.");
            }

            ListItem page = null;
            if (this.PublishingPage)
            {
                page = Identity.GetPage(this.ClientContext.Web, CacheManager.Instance.GetPublishingPagesLibraryName(this.ClientContext));
            }
            else if (this.BlogPage)
            {
                // Blogs don't live in other libraries or sub folders
                Identity.Library = null;
                Identity.Folder = null;
                page = Identity.GetPage(this.ClientContext.Web, CacheManager.Instance.GetBlogListName(this.ClientContext));
            }
            else if (this.DelveBlogPage)
            {
                // Blogs don't live in other libraries or sub folders
                Identity.Library = null;
                Identity.Folder = null;
                page = Identity.GetPage(this.ClientContext.Web, "pPg");
            }
            else
            {
                if (this.Folder == null || !this.Folder.Equals(rootFolder, StringComparison.InvariantCultureIgnoreCase))
                {
                    page = Identity.GetPage(this.ClientContext.Web, "sitepages");
                }
            }

            if (page == null && (Folder == null || !this.Folder.Equals(rootFolder, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new Exception($"Page '{Identity?.Name}' does not exist");
            }

            // Publishing specific validation
            if (this.PublishingPage && string.IsNullOrEmpty(this.TargetWebUrl) && TargetConnection == null)
            {
                throw new Exception($"Publishing page transformation is only supported when transformating into another site collection. Use the -TargetWebUrl to specify a modern target site.");
            }

            // Blog specific validation
            if ((this.BlogPage || this.DelveBlogPage) && string.IsNullOrEmpty(this.TargetWebUrl) && TargetConnection == null)
            {
                throw new Exception($"Blog and Delve blog page transformation is only supported when transformating into another site collection. Use the -TargetWebUrl to specify a modern target site.");
            }

            #endregion

            #region Transformation settings configuration

            var transformationInstance = new PnPCoreTransformationSharePoint(
                pnpOptions => // Global settings
                {
                    pnpOptions.DisableTelemetry = this.DisableTelemetry;
                    pnpOptions.PersistenceProviderConnectionString = this.PersistenceProviderConnectionString;
                },
                pageOptions => // Target modern page creation settings
                {
                    pageOptions.CopyPageMetadata = this.CopyPageMetadata;
                    pageOptions.KeepPageCreationModificationInformation = this.KeepPageCreationModificationInformation;
                    pageOptions.PostAsNews = this.PostAsNews;
                    pageOptions.PublishPage = this.PublishPage;
                    pageOptions.DisablePageComments = this.DisablePageComments;
                    pageOptions.KeepPageSpecificPermissions = this.KeepPageSpecificPermissions;
                    pageOptions.Overwrite = this.Overwrite;
                    pageOptions.ReplaceHomePageWithDefaultHomePage = this.ReplaceHomePageWithDefaultHomePage;
                    pageOptions.SetAuthorInPageHeader = this.SetAuthorInPageHeader;
                    pageOptions.TargetPageFolder = this.TargetPageFolder;
                    pageOptions.TargetPageName = this.TargetPageName;
                    pageOptions.TargetPagePrefix = this.TargetPagePrefix;
                    pageOptions.TargetPageTakesSourcePageName = this.TargetPageTakesSourcePageName;
                },
                spOptions => // SharePoint classic source settings
                {
                    if (!string.IsNullOrEmpty(this.WebPartMappingFile))
                    {
                        spOptions.WebPartMappingFile = this.WebPartMappingFile;
                    }
                    if (!string.IsNullOrEmpty(this.PageLayoutMappingFile))
                    {
                        spOptions.PageLayoutMappingFile = this.PageLayoutMappingFile;
                    }
                    spOptions.RemoveEmptySectionsAndColumns = this.RemoveEmptySectionsAndColumns;
                    spOptions.ShouldMapUsers = this.ShouldMapUsers;
                    spOptions.HandleWikiImagesAndVideos = this.HandleWikiImagesAndVideos;
                    spOptions.AddTableListImageAsImageWebPart = this.AddTableListImageAsImageWebPart;
                    spOptions.IncludeTitleBarWebPart = this.IncludeTitleBarWebPart;
                    if (this.MappingProperties != null && this.MappingProperties.Count > 0)
                    {
                        spOptions.MappingProperties = new Dictionary<string, string>();
                        foreach (string key in this.MappingProperties.Keys)
                        {
                            spOptions.MappingProperties.Add(key, this.MappingProperties[key] as string);
                        }
                    }
                    spOptions.SkipHiddenWebParts = this.SkipHiddenWebParts;
                    spOptions.SkipUrlRewrite = this.SkipUrlRewrite;
                    if (this.UrlMappings != null && this.UrlMappings.Count > 0)
                    {
                        spOptions.UrlMappings = new List<UrlMapping>();
                        foreach (string key in this.UrlMappings.Keys)
                        {
                            spOptions.UrlMappings.Add(new UrlMapping
                            {
                                SourceUrl = key,
                                TargetUrl = this.UrlMappings[key] as string
                            });
                        }
                    }
                    if (this.UserMappings != null && this.UserMappings.Count > 0)
                    {
                        spOptions.UserMappings = new List<UserMapping>();
                        foreach (string key in this.UserMappings.Keys)
                        {
                            spOptions.UserMappings.Add(new UserMapping
                            {
                                SourceUser = key,
                                TargetUser = this.UserMappings[key] as string
                            });
                        }
                    }
                }
            );

            #endregion

            var pageTransformator = transformationInstance.GetPnPSharePointPageTransformator();

            ClientContext sourceContext = this.ClientContext;

            // Create target PnP Context (when needed)
            PnPContext targetContext = this.TargetConnection?.PnPContext;
            if (targetContext == null)
            {
                targetContext = this.PnPContext;
            }

            var sourcePage = page;
            sourcePage.EnsureProperty(p => p.File);

            // Determine the full URL of the source page
            this.ClientContext.Web.EnsureProperty(w => w.Url);
            var baseUrlMatch = baseUrlRegex.Match(this.ClientContext.Web.Url);
            var baseUrl = baseUrlMatch.Groups["baseUrl"].Value;
            var sourceUri = new Uri($"{baseUrl.TrimEnd('/')}{sourcePage.File.ServerRelativeUrl}");

            var result = pageTransformator.TransformSharePointAsync(sourceContext, targetContext, sourceUri).GetAwaiter().GetResult();

            // Output the server relative url to the newly created page
            if (result != null)
            {
                WriteObject(result.AbsoluteUri);
            }
        }
    }
}