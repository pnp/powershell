using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Set, "PnPPage")]
    [Alias("Set-PnPClientSidePage")]
    [OutputType(typeof(PnP.Core.Model.SharePoint.IPage))]
    public class SetPage : PnPWebCmdlet, IDynamicParameters
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public PagePipeBind Identity;

        [Parameter(Mandatory = false)]
        public string Name = null;

        [Parameter(Mandatory = false)]
        public string Title = null;

        [Parameter(Mandatory = false)]
        public PageLayoutType LayoutType = PageLayoutType.Article;

        [Parameter(Mandatory = false)]
        public PagePromoteType PromoteAs = PagePromoteType.None;

        [Parameter(Mandatory = false)]
        public SwitchParameter CommentsEnabled = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter Publish;

        [Parameter(Mandatory = false)]
        public DateTime? ScheduledPublishDate;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveScheduledPublish;

        [Parameter(Mandatory = false)]
        public PageHeaderType HeaderType;

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public string ThumbnailUrl;

        [Parameter(Mandatory = false)]
        public PageHeaderLayoutType HeaderLayoutType = PageHeaderLayoutType.FullWidthImage;

        [Parameter(Mandatory = false)]
        public SwitchParameter DemoteNewsArticle;

        [Parameter(Mandatory = false)]
        public SwitchParameter Translate;

        [Parameter(Mandatory = false)]
        public int[] TranslationLanguageCodes;

        [Parameter(Mandatory = false)]
        public bool ShowPublishDate;

        private CustomHeaderDynamicParameters customHeaderParameters;

        public object GetDynamicParameters()
        {
            if (HeaderType == PageHeaderType.Custom)
            {
                customHeaderParameters = new CustomHeaderDynamicParameters();
                return customHeaderParameters;
            }
            return null;
        }

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Identity?.GetPage(Connection);

            if (clientSidePage == null)
            {
                // If the client side page object cannot be found
                throw new Exception($"Page {Identity?.Name} cannot be found.");
            }

            // We need to have the page name, if not found, raise an error
            string name = PageUtilities.EnsureCorrectPageName(Name ?? Identity?.Name);
            if (name == null)
                throw new Exception("Insufficient arguments to update a client side page");

            // Don't allow changing a topic page into a regular page as that could lead to data loss
            if (clientSidePage.LayoutType != PageLayoutType.Topic)
            {
                clientSidePage.LayoutType = LayoutType;
            }

            if (Title != null)
            {
                clientSidePage.PageTitle = Title;
            }

            if (ThumbnailUrl != null)
            {
                clientSidePage.ThumbnailUrl = ThumbnailUrl;
            }

            if (ParameterSpecified(nameof(HeaderType)))
            {
                switch (HeaderType)
                {
                    case PageHeaderType.Default:
                        {
                            clientSidePage.SetDefaultPageHeader();
                            break;
                        }
                    case PageHeaderType.Custom:
                        {
                            clientSidePage.SetCustomPageHeader(customHeaderParameters.ServerRelativeImageUrl, customHeaderParameters.TranslateX, customHeaderParameters.TranslateY);
                            break;
                        }
                    case PageHeaderType.None:
                        {
                            clientSidePage.RemovePageHeader();
                            break;
                        }
                }
            }

            if (ParameterSpecified(nameof(HeaderLayoutType)))
            {
                clientSidePage.PageHeader.LayoutType = HeaderLayoutType;
            }
            
            if(ParameterSpecified(nameof(ShowPublishDate)))
            {
                clientSidePage.PageHeader.ShowPublishDate = ShowPublishDate;
            }

            if (PromoteAs == PagePromoteType.Template)
            {
                clientSidePage.SaveAsTemplate(name);
            }
            else
            {
                clientSidePage.Save(name);
            }

            // If a specific promote type is specified, promote the page as Home or Article or ...
            switch (PromoteAs)
            {
                case PagePromoteType.HomePage:
                    clientSidePage.PromoteAsHomePage();
                    break;
                case PagePromoteType.NewsArticle:
                    clientSidePage.PromoteAsNewsArticle();
                    break;
                case PagePromoteType.None:
                default:
                    break;
            }

            if (ParameterSpecified(nameof(CommentsEnabled)))
            {
                if (CommentsEnabled)
                {
                    clientSidePage.EnableComments();
                }
                else
                {
                    clientSidePage.DisableComments();
                }
            }

            if (ContentType != null)
            {
                string ctId = ContentType.GetIdOrWarn(this, CurrentWeb);
                if (ctId != null)
                {
                    var pageFile = clientSidePage.GetPageFile(p => p.UniqueId, p => p.ListId, p => p.ListItemAllFields);
                    pageFile.ListItemAllFields["ContentTypeId"] = ctId;
                    pageFile.ListItemAllFields.SystemUpdate();
                }
            }

            if (Publish)
            {
                clientSidePage.Publish();
            }

            if (ParameterSpecified(nameof(ScheduledPublishDate)))
            {
                clientSidePage.SchedulePublish(ScheduledPublishDate.Value);
            }

            if (ParameterSpecified(nameof(RemoveScheduledPublish)))
            {
                clientSidePage.RemoveSchedulePublish();
            }

            if (ParameterSpecified(nameof(DemoteNewsArticle)))
            {
                clientSidePage.DemoteNewsArticle();
            }

            if (ParameterSpecified(nameof(Translate)))
            {
                if (ParameterSpecified(nameof(TranslationLanguageCodes)))
                {
                    PageTranslationOptions options = new PageTranslationOptions();
                    if (TranslationLanguageCodes != null && TranslationLanguageCodes.Length > 0)
                    {
                        var translationLanguagesList = new List<int>(TranslationLanguageCodes);

                        try
                        {
                            MultilingualHelper.EnsureMultilingual(Connection, translationLanguagesList);
                        }
                        catch
                        {
                            // swallow exception
                        }

                        foreach (int i in TranslationLanguageCodes)
                        {
                            options.AddLanguage(i);
                        }
                        clientSidePage.TranslatePages(options);
                    }
                }
                else
                {
                    clientSidePage.TranslatePages();
                }
            }

            WriteObject(clientSidePage);
        }

        public class CustomHeaderDynamicParameters
        {
            [Parameter(Mandatory = true)]
            public string ServerRelativeImageUrl { get; set; }

            [Parameter(Mandatory = false)]
            public double TranslateX { get; set; } = 0.0;

            [Parameter(Mandatory = false)]
            public double TranslateY { get; set; } = 0.0;
        }
    }
}
