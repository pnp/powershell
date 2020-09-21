using PnP.Framework.Pages;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.ClientSidePages
{
    [Cmdlet(VerbsCommon.Set, "PnPClientSidePage")]
    public class SetClientSidePage : PnPWebCmdlet, IDynamicParameters
    {
        const string ParameterSet_CUSTOMHEADER = "Custom Header";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ClientSidePagePipeBind Identity;

        [Parameter(Mandatory = false)]
        public string Name = null;

        [Parameter(Mandatory = false)]
        public string Title = null;

        [Parameter(Mandatory = false)]
        public ClientSidePageLayoutType LayoutType = ClientSidePageLayoutType.Article;

        [Parameter(Mandatory = false)]
        public ClientSidePagePromoteType PromoteAs = ClientSidePagePromoteType.None;

        [Parameter(Mandatory = false)]
        public SwitchParameter CommentsEnabled = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter Publish;

        [Parameter(Mandatory = false)]
        public ClientSidePageHeaderType HeaderType;

        [Parameter(Mandatory = false)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public string ThumbnailUrl;

        private CustomHeaderDynamicParameters customHeaderParameters;

        public object GetDynamicParameters()
        {
            if (HeaderType == ClientSidePageHeaderType.Custom)
            {
                customHeaderParameters = new CustomHeaderDynamicParameters();
                return customHeaderParameters;
            }
            return null;
        }

        protected override void ExecuteCmdlet()
        {

            ClientSidePage clientSidePage = Identity?.GetPage(ClientContext);

            if (clientSidePage == null)
                // If the client side page object cannot be found
                throw new Exception($"Page {Identity?.Name} cannot be found.");

            // We need to have the page name, if not found, raise an error
            string name = ClientSidePageUtilities.EnsureCorrectPageName(Name ?? Identity?.Name);
            if (name == null)
                throw new Exception("Insufficient arguments to update a client side page");

            clientSidePage.LayoutType = LayoutType;

            if (Title != null)
            {
                clientSidePage.PageTitle = Title;
            }

            if(ThumbnailUrl != null)
            {
                clientSidePage.ThumbnailUrl = ThumbnailUrl;
            }

            if (ParameterSpecified(nameof(HeaderType)))
            {
                switch (HeaderType)
                {
                    case ClientSidePageHeaderType.Default:
                        {
                            clientSidePage.SetDefaultPageHeader();
                            break;
                        }
                    case ClientSidePageHeaderType.Custom:
                        {
                            clientSidePage.SetCustomPageHeader(customHeaderParameters.ServerRelativeImageUrl, customHeaderParameters.TranslateX, customHeaderParameters.TranslateY);
                            break;
                        }
                    case ClientSidePageHeaderType.None:
                        {
                            clientSidePage.RemovePageHeader();
                            break;
                        }
                }
            }

            if (PromoteAs == ClientSidePagePromoteType.Template)
            {
                clientSidePage.SaveAsTemplate(name);
            } else
            {
                clientSidePage.Save(name);
            }

            // If a specific promote type is specified, promote the page as Home or Article or ...
            switch (PromoteAs)
            {
                case ClientSidePagePromoteType.HomePage:
                    clientSidePage.PromoteAsHomePage();
                    break;
                case ClientSidePagePromoteType.NewsArticle:
                    clientSidePage.PromoteAsNewsArticle();
                    break;
                case ClientSidePagePromoteType.None:
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

            if(ParameterSpecified(nameof(ContentType)))
            {
                ContentType ct = null;
                if (ContentType.ContentType == null)
                {
                    if (ContentType.Id != null)
                    {
                        ct = SelectedWeb.GetContentTypeById(ContentType.Id, true);
                    }
                    else if (ContentType.Name != null)
                    {
                        ct = SelectedWeb.GetContentTypeByName(ContentType.Name, true);
                    }
                }
                else
                {
                    ct = ContentType.ContentType;
                }
                if (ct != null)
                {
                    ct.EnsureProperty(w => w.StringId);

                    clientSidePage.PageListItem["ContentTypeId"] = ct.StringId;
                    clientSidePage.PageListItem.SystemUpdate();
                    ClientContext.ExecuteQueryRetry();
                }
            }

            if (Publish)
            {
                clientSidePage.Publish();
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