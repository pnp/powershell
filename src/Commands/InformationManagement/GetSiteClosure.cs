using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.InformationManagement
{

    [Cmdlet(VerbsCommon.Get, "PnPSiteClosure")]
    
    
    public class GetSiteClosure : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var isClosed = SelectedWeb.IsClosedBySitePolicy();

            WriteObject(isClosed ? ClosureState.Closed : ClosureState.Open, false);
        }
    }
}
