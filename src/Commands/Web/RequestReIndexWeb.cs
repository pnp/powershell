using Microsoft.SharePoint.Client;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Request, "PnPReIndexWeb")]
    [OutputType(typeof(void))]
    public class RequestReIndexWeb : PnPWebCmdlet
    {

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.ReIndexWeb();
        }
    }
}
