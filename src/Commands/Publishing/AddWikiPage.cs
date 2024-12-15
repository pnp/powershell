using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Add, "PnPWikiPage")]
    public class AddWikiPage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = "WithContent")]
        public string Content = null;

        [Parameter(Mandatory = true, ParameterSetName = "WithLayout")]
        public WikiPageLayout Layout;

        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case "WithContent":
                    {
                        CurrentWeb.AddWikiPageByUrl(ServerRelativePageUrl, Content);
                        break;
                    }
                case "WithLayout":
                    {
                        CurrentWeb.AddWikiPageByUrl(ServerRelativePageUrl);
                        CurrentWeb.AddLayoutToWikiPage(Layout, ServerRelativePageUrl);
                        break;
                    }
                default:
                    {
                        CurrentWeb.AddWikiPageByUrl(ServerRelativePageUrl);
                        break;
                    }
            }
        }
    }
}
