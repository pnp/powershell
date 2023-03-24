using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPSdnProvider", SupportsShouldProcess = true)]
    public class RemoveSdnProvider : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            if (ShouldProcess("Removes a SDN Provider"))
            {
                this.Tenant.RemoveSdnProvider();
                AdminContext.ExecuteQueryRetry();
            }
        }
    }
}