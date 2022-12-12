using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Add, "PnPPage")]
    public class AddPage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Name = null;

        [Parameter(Mandatory = false)]
        public PageLayoutType LayoutType = PageLayoutType.Article;

        [Parameter(Mandatory = false)]
        public PagePromoteType PromoteAs = PagePromoteType.None;

        [Parameter(Mandatory = false)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter CommentsEnabled = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter Publish;

        [Parameter(Mandatory = false)]
        public DateTime? ScheduledPublishDate;

        [Parameter(Mandatory = false)]
        public PageHeaderLayoutType HeaderLayoutType = PageHeaderLayoutType.FullWidthImage;

        [Parameter(Mandatory = false)]
        public SwitchParameter Translate;

        [Parameter(Mandatory = false)]
        public int[] TranslationLanguageCodes;

        [Parameter(Mandatory = false)]
        public string Title;
        
        protected override void ExecuteCmdlet()
        {
            IPage clientSidePage = null;

            // Check if the page exists
            string name = PageUtilities.EnsureCorrectPageName(Name);

            bool pageExists = false;
            try
            {
                var pages = PnPContext.Web.GetPages(name);
                if (pages != null && pages.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) != null)
                {
                    pageExists = true;
                }
            }
            catch { }

            if (pageExists)
            {
                throw new Exception($"Page {name} already exists");
            }

            // Create a page that persists immediately
            clientSidePage = PnPContext.Web.NewPage(LayoutType);
            clientSidePage.PageHeader.LayoutType = HeaderLayoutType;

            if (ParameterSpecified(nameof(Title)))
            {
                // Set the page title that will be shown at the top of the page. If omitted, the title will be the equivallent of the name of the page which will be used as the filename as well.
                clientSidePage.PageTitle = Title;
            }

            if (PromoteAs == PagePromoteType.Template)
            {
                clientSidePage.SaveAsTemplate(name);
            }
            else
            {
                clientSidePage.Save(name);
            }

            if (ContentType != null)
            {
                string ctId = ContentType.GetIdOrThrow(nameof(ContentType), CurrentWeb);
                var pageFile = clientSidePage.GetPageFile(p => p.UniqueId, p => p.ListId, p => p.ListItemAllFields);
                pageFile.ListItemAllFields["ContentTypeId"] = ctId;
                pageFile.ListItemAllFields.SystemUpdate();
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

            if (Publish)
            {
                clientSidePage.Publish();
            }

            if (ParameterSpecified(nameof(ScheduledPublishDate)))
            {
                clientSidePage.SchedulePublish(ScheduledPublishDate.Value);
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
                            PnPContext.Web.EnsureMultilingual(translationLanguagesList);
                        }
                        catch
                        {
                            // swallow exception, assumes multilingual features is activated                            
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
    }
}