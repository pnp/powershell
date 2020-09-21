using Microsoft.SharePoint.Client;
using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Request, "PnPReIndexWeb")]
    public class RequestReIndexWeb : PnPWebCmdlet
    {

        protected override void ExecuteCmdlet()
        {
            SelectedWeb.ReIndexWeb();
        }
    }
}
