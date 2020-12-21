using PnP.Core.Model.SharePoint;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ClientSidePages
{
    [Cmdlet(VerbsCommon.Add, "PnPClientSidePage")]
    public class AddClientSidePage : PnPWebCmdlet
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
        public PageHeaderLayoutType HeaderLayoutType = PageHeaderLayoutType.FullWidthImage;

        protected override void ExecuteCmdlet()
        {
            IPage clientSidePage = null;

            // Check if the page exists
            string name = ClientSidePageUtilities.EnsureCorrectPageName(Name);

            bool pageExists = false;
            try
            {
                var pages = PnPContext.Web.GetPages(name);
                pageExists = pages != null && pages.Any();
            }
            catch { }

            if (pageExists)
            {
                throw new Exception($"Page {name} already exists");
            }

            // Create a page that persists immediately
            clientSidePage = PnPContext.Web.NewPage(LayoutType);
            clientSidePage.PageHeader.LayoutType = HeaderLayoutType;

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
                var pageFile = clientSidePage.GetPageFile();
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

            WriteObject(clientSidePage);
        }
    }
}