using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.PowerShell.CmdletHelpAttributes;

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
                        SelectedWeb.AddWikiPageByUrl(ServerRelativePageUrl, Content);
                        break;
                    }
                case "WithLayout":
                    {
                        SelectedWeb.AddWikiPageByUrl(ServerRelativePageUrl);
                        SelectedWeb.AddLayoutToWikiPage(Layout, ServerRelativePageUrl);
                        break;
                    }
                default:
                    {
                        SelectedWeb.AddWikiPageByUrl(ServerRelativePageUrl);
                        break;
                    }
            }
        }
    }
}
