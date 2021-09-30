//using System;
//using System.Management.Automation;
//using PnP.PowerShell.Commands.Base.PipeBinds;
//using PnP.Framework.Modernization;
//using System.Reflection;
//using Microsoft.SharePoint.Client;
//using System.IO;
//using PnP.PowerShell.Commands.Base;
//using PnP.Framework.Modernization.Transform;
//using PnP.Framework.Modernization.Publishing;
//using PnP.Framework.Modernization.Telemetry.Observers;
//using PnP.Framework.Modernization.Cache;
//using PnP.PowerShell.Commands.Attributes;
//using System.Collections;
//using System.Collections.Generic;
//using PnP.Core.Transformation.SharePoint.Services;
//using System.Linq;
//using PnP.Core.Transformation.SharePoint.Services.Builder.Configuration;
//using PnP.Core.Services;
//using PnP.Core.Transformation.SharePoint;

//namespace PnP.PowerShell.Commands.Transformation
//{

//    [Cmdlet(VerbsLifecycle.Invoke, "PnPTransformation")]
//    public class InvokeTransformation : PnPWebCmdlet
//    {
//        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
//        public ClassicPagePipeBind Identity;

//        #region Global transformation settings

//        [Parameter(Mandatory = false)]
//        public SwitchParameter DisableTelemetry = false;

//        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
//        public string PersistenceProviderConnectionString;

//        #endregion

//        #region Target Page generation input arguments

//        [Parameter(Mandatory = false)]
//        public SwitchParameter CopyPageMetadata = false;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter KeepPageCreationModificationInformation = true;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter PostAsNews = false;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter PublishPage = true;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter DisablePageComments = false;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter KeepPageSpecificPermissions = true;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter Overwrite = true;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter ReplaceHomePageWithDefaultHomePage = false;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter SetAuthorInPageHeader = false;

//        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
//        public string TargetPageFolder;

//        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
//        public string TargetPageName;

//        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
//        public string TargetPagePrefix;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter TargetPageTakesSourcePageName = false;

//        #endregion

//        #region SharePoint Source input arguments

//        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
//        public string WebPartMappingFile;

//        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
//        public string PageLayoutMappingFile;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter RemoveEmptySectionsAndColumns = false;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter ShouldMapUsers = true;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter HandleWikiImagesAndVideos = true;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter AddTableListImageAsImageWebPart = true;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter IncludeTitleBarWebPart = false;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter SkipHiddenWebParts = false;

//        [Parameter(Mandatory = false)]
//        public SwitchParameter SkipUrlRewrite = false;

//        [Parameter(Mandatory = false)]
//        public Hashtable MappingProperties;

//        [Parameter(Mandatory = false)]
//        public Hashtable UrlMappings;

//        [Parameter(Mandatory = false)]
//        public Hashtable UserMappings;

//        #endregion

//        protected override void ExecuteCmdlet()
//        {
//            WriteWarning("Please consider that the PnP Transformation Framework is still under preview and there could be issues while executing this cdmlet. If you run into any issue, plese file a new item here: https://github.com/pnp/pnpcore/issues");

//            var transformationInstance = new PnPCoreTransformationSharePoint(
//                pnpOptions => // Global settings
//                {
//                    pnpOptions.DisableTelemetry = this.DisableTelemetry;
//                    pnpOptions.PersistenceProviderConnectionString = this.PersistenceProviderConnectionString;
//                },
//                pageOptions => // Target modern page creation settings
//                {
//                    pageOptions.CopyPageMetadata = this.CopyPageMetadata;
//                    pageOptions.KeepPageCreationModificationInformation = this.KeepPageCreationModificationInformation;
//                    pageOptions.PostAsNews = this.PostAsNews;
//                    pageOptions.PublishPage = this.PublishPage;
//                    pageOptions.DisablePageComments = this.DisablePageComments;
//                    pageOptions.KeepPageSpecificPermissions = this.KeepPageSpecificPermissions;
//                    pageOptions.Overwrite = this.Overwrite;
//                    pageOptions.ReplaceHomePageWithDefaultHomePage = this.ReplaceHomePageWithDefaultHomePage;
//                    pageOptions.SetAuthorInPageHeader = this.SetAuthorInPageHeader;
//                    pageOptions.TargetPageFolder = this.TargetPageFolder;
//                    pageOptions.TargetPageName = this.TargetPageName;
//                    pageOptions.TargetPagePrefix = this.TargetPagePrefix;
//                    pageOptions.TargetPageTakesSourcePageName = this.TargetPageTakesSourcePageName;
//                },
//                spOptions => // SharePoint classic source settings
//                {
//                    if (!string.IsNullOrEmpty(this.WebPartMappingFile))
//                    {
//                        spOptions.WebPartMappingFile = this.WebPartMappingFile;
//                    }
//                    if (!string.IsNullOrEmpty(this.PageLayoutMappingFile))
//                    {
//                        spOptions.PageLayoutMappingFile = this.PageLayoutMappingFile;
//                    }
//                    spOptions.RemoveEmptySectionsAndColumns = this.RemoveEmptySectionsAndColumns;
//                    spOptions.ShouldMapUsers = this.ShouldMapUsers;
//                    spOptions.HandleWikiImagesAndVideos = this.HandleWikiImagesAndVideos;
//                    spOptions.AddTableListImageAsImageWebPart = this.AddTableListImageAsImageWebPart;
//                    spOptions.IncludeTitleBarWebPart = this.IncludeTitleBarWebPart;
//                    if (this.MappingProperties != null && this.MappingProperties.Count > 0)
//                    {
//                        spOptions.MappingProperties = new Dictionary<string, string>();
//                        foreach (string key in this.MappingProperties.Keys)
//                        {
//                            spOptions.MappingProperties.Add(key, this.MappingProperties[key] as string);
//                        }
//                    }
//                    spOptions.SkipHiddenWebParts = this.SkipHiddenWebParts;
//                    spOptions.SkipUrlRewrite = this.SkipUrlRewrite;
//                    if (this.UrlMappings != null && this.UrlMappings.Count > 0)
//                    {
//                        spOptions.UrlMappings = new List<UrlMapping>();
//                        foreach (string key in this.UrlMappings.Keys)
//                        {
//                            spOptions.UrlMappings.Add(new UrlMapping
//                            {
//                                SourceUrl = key,
//                                TargetUrl = this.UrlMappings[key] as string
//                            });
//                        }
//                    }
//                    if (this.UserMappings != null && this.UserMappings.Count > 0)
//                    {
//                        spOptions.UserMappings = new List<UserMapping>();
//                        foreach (string key in this.UserMappings.Keys)
//                        {
//                            spOptions.UserMappings.Add(new UserMapping
//                            {
//                                SourceUser = key,
//                                TargetUser = this.UserMappings[key] as string
//                            });
//                        }
//                    }
//                }
//            );

//            var pageTransformator = transformationInstance.GetPnPSharePointPageTransformator();

//            ClientContext sourceContext = this.ClientContext;
//            PnPContext targetContext = null; // TODO: ???

//            var sourcePage = this.Identity.GetPage(sourceContext.Web, ""); // TODO: Complete ...
//            // sourcePage.EnsureProperty(p => p.File);
//            var sourceUri = new Uri(sourcePage.File.ServerRelativeUrl);

//            var result = pageTransformator.TransformSharePointAsync(sourceContext, targetContext, sourceUri).GetAwaiter().GetResult();

//            // Output the server relative url to the newly created page
//            if (result != null)
//            {
//                WriteObject(result);
//            }
//        }
//    }
//}