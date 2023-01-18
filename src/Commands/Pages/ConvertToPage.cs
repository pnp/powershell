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

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsData.ConvertTo, "PnPPage")]
    public class ConvertToClientSidePage : PnPWebCmdlet
    {
        private static string rootFolder = "<root>";
        private Assembly sitesCoreAssembly;
        // private Assembly newtonsoftAssembly;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ClassicPagePipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string Library;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string Folder;

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string WebPartMappingFile;

        [Parameter(Mandatory = false)]
        public SwitchParameter Overwrite = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter TakeSourcePageName = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter ReplaceHomePageWithDefault = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter AddPageAcceptBanner = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipItemLevelPermissionCopyToClientSidePage = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipUrlRewriting = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipDefaultUrlRewriting = false;

        [Parameter(Mandatory = false)]
        public string UrlMappingFile = "";

        [Parameter(Mandatory = false)]
        public SwitchParameter ClearCache = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter CopyPageMetadata = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter AddTableListImageAsImageWebPart = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter UseCommunityScriptEditor = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter SummaryLinksToHtml = false;

        [Parameter(Mandatory = false)]
        public string TargetWebUrl;

        [Parameter(Mandatory = false)]
        public PageTransformatorLogType LogType = PageTransformatorLogType.None;

        [Parameter(Mandatory = false)]
        public string LogFolder = "";

        [Parameter(Mandatory = false)]
        public SwitchParameter LogSkipFlush = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter LogVerbose = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter DontPublish = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter KeepPageCreationModificationInformation = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter SetAuthorInPageHeader = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter PostAsNews = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter DisablePageComments = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter PublishingPage = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter BlogPage = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter DelveBlogPage = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter DelveKeepSubTitle = false;

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string PageLayoutMapping;

        [Parameter(Mandatory = false)]
        public string PublishingTargetPageName = "";

        [Parameter(Mandatory = false)]
        public string TargetPageName = "";

        [Parameter(Mandatory = false)]
        public string TargetPageFolder = "";

        [Parameter(Mandatory = false)]
        public SwitchParameter TargetPageFolderOverridesDefaultFolder = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveEmptySectionsAndColumns = true;

        [Parameter(Mandatory = false)] // do not remove '#!#99'
        public PnPConnection TargetConnection = null;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipUserMapping = false;

        [Parameter(Mandatory = false)]
        public string UserMappingFile = "";

        [Parameter(Mandatory = false)]
        public string TermMappingFile = "";

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipTermStoreMapping = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipHiddenWebParts = false;

        [Parameter(Mandatory = false)]
        public string LDAPConnectionString = "";

        protected override void ExecuteCmdlet()
        {
            //Fix loading of modernization framework
            FixLocalAssemblyResolving();

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

            // Load transformation models
            PageTransformation webPartMappingModel = null;
            if (string.IsNullOrEmpty(this.WebPartMappingFile))
            {
                webPartMappingModel = PageTransformator.LoadDefaultWebPartMapping();
                this.WriteVerbose("Using embedded webpartmapping file. Use Export-PnPClientSidePageMapping to get that file in case you want to base your version of the embedded version.");
            }

            // Validate webpartmappingfile
            if (!string.IsNullOrEmpty(this.WebPartMappingFile))
            {
                if (!System.IO.File.Exists(this.WebPartMappingFile))
                {
                    throw new Exception($"Provided webpartmapping file {this.WebPartMappingFile} does not exist");
                }
            }

            if (this.PublishingPage && !string.IsNullOrEmpty(this.PageLayoutMapping) && !System.IO.File.Exists(this.PageLayoutMapping))
            {
                throw new Exception($"Provided pagelayout mapping file {this.PageLayoutMapping} does not exist");
            }

            bool crossSiteTransformation = TargetConnection != null || !string.IsNullOrEmpty(TargetWebUrl);

            // Create target client context (when needed)
            ClientContext targetContext = null;
            if (TargetConnection == null)
            {
                if (!string.IsNullOrEmpty(TargetWebUrl))
                {
                    targetContext = this.ClientContext.Clone(TargetWebUrl);
                }
            }
            else
            {
                targetContext = TargetConnection.Context;
            }


            // Create transformator instance
            PageTransformator pageTransformator = null;
            PublishingPageTransformator publishingPageTransformator = null;

            if (!string.IsNullOrEmpty(this.WebPartMappingFile))
            {
                // Using custom web part mapping file
                if (this.PublishingPage)
                {
                    if (!string.IsNullOrEmpty(this.PageLayoutMapping))
                    {
                        // Using custom page layout mapping file + default one (they're merged together)
                        publishingPageTransformator = new PublishingPageTransformator(this.ClientContext, targetContext, this.WebPartMappingFile, this.PageLayoutMapping);
                    }
                    else
                    {
                        // Using default page layout mapping file
                        publishingPageTransformator = new PublishingPageTransformator(this.ClientContext, targetContext, this.WebPartMappingFile, null);
                    }
                }
                else
                {
                    // Use web part mapping file
                    pageTransformator = new PageTransformator(this.ClientContext, targetContext, this.WebPartMappingFile);
                }
            }
            else
            {
                // Using default web part mapping file
                if (this.PublishingPage)
                {
                    if (!string.IsNullOrEmpty(this.PageLayoutMapping))
                    {
                        // Load and validate the custom mapping file
                        PageLayoutManager pageLayoutManager = new PageLayoutManager();
                        var pageLayoutMappingModel = pageLayoutManager.LoadPageLayoutMappingFile(this.PageLayoutMapping);

                        // Using custom page layout mapping file + default one (they're merged together)
                        publishingPageTransformator = new PublishingPageTransformator(this.ClientContext, targetContext, webPartMappingModel, pageLayoutMappingModel);
                    }
                    else
                    {
                        // Using default page layout mapping file
                        publishingPageTransformator = new PublishingPageTransformator(this.ClientContext, targetContext, webPartMappingModel, null);
                    }
                }
                else
                {
                    // Use web part mapping model loaded from embedded mapping file
                    pageTransformator = new PageTransformator(this.ClientContext, targetContext, webPartMappingModel);
                }
            }

            // Setup logging
            if (this.LogType == PageTransformatorLogType.File)
            {
                if (this.PublishingPage)
                {
                    publishingPageTransformator.RegisterObserver(new MarkdownObserver(folder: this.LogFolder, includeVerbose: this.LogVerbose, includeDebugEntries: this.LogVerbose));
                }
                else
                {
                    pageTransformator.RegisterObserver(new MarkdownObserver(folder: this.LogFolder, includeVerbose: this.LogVerbose, includeDebugEntries: this.LogVerbose));
                }
            }
            else if (this.LogType == PageTransformatorLogType.SharePoint)
            {
                if (this.PublishingPage)
                {
                    publishingPageTransformator.RegisterObserver(new MarkdownToSharePointObserver(targetContext ?? this.ClientContext, includeVerbose: this.LogVerbose, includeDebugEntries: this.LogVerbose));
                }
                else
                {
                    pageTransformator.RegisterObserver(new MarkdownToSharePointObserver(targetContext ?? this.ClientContext, includeVerbose: this.LogVerbose, includeDebugEntries: this.LogVerbose));
                }
            }
            else if (this.LogType == PageTransformatorLogType.Console)
            {
                if (this.PublishingPage)
                {
                    publishingPageTransformator.RegisterObserver(new ConsoleObserver(includeDebugEntries: this.LogVerbose));
                }
                else
                {
                    pageTransformator.RegisterObserver(new ConsoleObserver(includeDebugEntries: this.LogVerbose));
                }
            }

            // Clear the client side component cache
            if (this.ClearCache)
            {
                CacheManager.Instance.ClearAllCaches();
            }

            string serverRelativeClientPageUrl = "";
            if (this.PublishingPage)
            {
                // Setup Transformation information
                PublishingPageTransformationInformation pti = new PublishingPageTransformationInformation(page)
                {
                    Overwrite = this.Overwrite,
                    KeepPageSpecificPermissions = !this.SkipItemLevelPermissionCopyToClientSidePage,
                    PublishCreatedPage = !this.DontPublish,
                    KeepPageCreationModificationInformation = this.KeepPageCreationModificationInformation,
                    PostAsNews = this.PostAsNews,
                    DisablePageComments = this.DisablePageComments,
                    TargetPageName = !string.IsNullOrEmpty(this.PublishingTargetPageName) ? this.PublishingTargetPageName : this.TargetPageName,
                    SkipUrlRewrite = this.SkipUrlRewriting,
                    SkipDefaultUrlRewrite = this.SkipDefaultUrlRewriting,
                    UrlMappingFile = this.UrlMappingFile,
                    AddTableListImageAsImageWebPart = this.AddTableListImageAsImageWebPart,
                    SkipUserMapping = this.SkipUserMapping,
                    UserMappingFile = this.UserMappingFile,
                    LDAPConnectionString = this.LDAPConnectionString,
                    TargetPageFolder = this.TargetPageFolder,
                    TargetPageFolderOverridesDefaultFolder = this.TargetPageFolderOverridesDefaultFolder,
                    TermMappingFile = TermMappingFile,
                    SkipTermStoreMapping = SkipTermStoreMapping,
                    RemoveEmptySectionsAndColumns = this.RemoveEmptySectionsAndColumns,
                    SkipHiddenWebParts = this.SkipHiddenWebParts,
                };

                // Set mapping properties
                pti.MappingProperties["SummaryLinksToQuickLinks"] = (!SummaryLinksToHtml).ToString().ToLower();
                pti.MappingProperties["UseCommunityScriptEditor"] = UseCommunityScriptEditor.ToString().ToLower();

                try
                {
                    serverRelativeClientPageUrl = publishingPageTransformator.Transform(pti);
                }
                finally
                {
                    // Flush log
                    if (this.LogType != PageTransformatorLogType.None && this.LogType != PageTransformatorLogType.Console && !this.LogSkipFlush)
                    {
                        publishingPageTransformator.FlushObservers();
                    }
                }
            }
            else
            {
                Microsoft.SharePoint.Client.File fileToModernize = null;
                if (this.Folder != null && this.Folder.Equals(rootFolder, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Load the page file from the site root folder
                    var webServerRelativeUrl = this.ClientContext.Web.EnsureProperty(p => p.ServerRelativeUrl);
                    fileToModernize = this.ClientContext.Web.GetFileByServerRelativeUrl($"{webServerRelativeUrl}/{this.Identity.Name}");
                    this.ClientContext.Load(fileToModernize);
                    this.ClientContext.ExecuteQueryRetry();
                }

                // Setup Transformation information
                PageTransformationInformation pti = new PageTransformationInformation(page)
                {
                    SourceFile = fileToModernize,
                    Overwrite = this.Overwrite,
                    TargetPageTakesSourcePageName = this.TakeSourcePageName,
                    ReplaceHomePageWithDefaultHomePage = this.ReplaceHomePageWithDefault,
                    KeepPageSpecificPermissions = !this.SkipItemLevelPermissionCopyToClientSidePage,
                    CopyPageMetadata = this.CopyPageMetadata,
                    PublishCreatedPage = !this.DontPublish,
                    KeepPageCreationModificationInformation = this.KeepPageCreationModificationInformation,
                    SetAuthorInPageHeader = this.SetAuthorInPageHeader,
                    PostAsNews = this.PostAsNews,
                    DisablePageComments = this.DisablePageComments,
                    TargetPageName = crossSiteTransformation ? this.TargetPageName : "",
                    SkipUrlRewrite = this.SkipUrlRewriting,
                    SkipDefaultUrlRewrite = this.SkipDefaultUrlRewriting,
                    UrlMappingFile = this.UrlMappingFile,
                    AddTableListImageAsImageWebPart = this.AddTableListImageAsImageWebPart,
                    SkipUserMapping = this.SkipUserMapping,
                    UserMappingFile = this.UserMappingFile,
                    LDAPConnectionString = this.LDAPConnectionString,
                    TargetPageFolder = this.TargetPageFolder,
                    TargetPageFolderOverridesDefaultFolder = this.TargetPageFolderOverridesDefaultFolder,
                    ModernizationCenterInformation = new ModernizationCenterInformation()
                    {
                        AddPageAcceptBanner = this.AddPageAcceptBanner
                    },
                    TermMappingFile = TermMappingFile,
                    SkipTermStoreMapping = SkipTermStoreMapping,
                    RemoveEmptySectionsAndColumns = this.RemoveEmptySectionsAndColumns,
                    SkipHiddenWebParts = this.SkipHiddenWebParts,
                };

                // Set mapping properties
                pti.MappingProperties["SummaryLinksToQuickLinks"] = (!SummaryLinksToHtml).ToString().ToLower();
                pti.MappingProperties["UseCommunityScriptEditor"] = UseCommunityScriptEditor.ToString().ToLower();

                try
                {
                    serverRelativeClientPageUrl = pageTransformator.Transform(pti);
                }
                finally
                {
                    // Flush log
                    if (this.LogType != PageTransformatorLogType.None && this.LogType != PageTransformatorLogType.Console && !this.LogSkipFlush)
                    {
                        pageTransformator.FlushObservers();
                    }
                }
            }

            // Output the server relative url to the newly created page
            if (!string.IsNullOrEmpty(serverRelativeClientPageUrl))
            {
                WriteObject(serverRelativeClientPageUrl);
            }
        }

        private Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private string AssemblyDirectory
        {
            get
            {
                string location = Assembly.GetExecutingAssembly().Location;
                UriBuilder uri = new UriBuilder(location);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private void FixLocalAssemblyResolving()
        {
            try
            {
                sitesCoreAssembly = Assembly.LoadFrom(Path.Combine(AssemblyDirectory, "PnP.Framework.dll"));
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_LocalAssemblyResolve;
            }
            catch { }
        }

        private Assembly CurrentDomain_LocalAssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("PnP.Framework"))
            {
                return sitesCoreAssembly;
            }
            return null;
        }
    }
}
