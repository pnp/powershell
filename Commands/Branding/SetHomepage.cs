using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Set, "PnPHomePage")]
    public class SetHomePage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("Path")]
        public string RootFolderRelativeUrl = string.Empty;

        protected override void ExecuteCmdlet()
        {
            SelectedWeb.SetHomePage(RootFolderRelativeUrl);
        }
    }

}
