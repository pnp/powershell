using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPSdnProvider")]
    public class RemoveSdnProvider : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            if (ShouldContinue("Removes a SDN Provider", Properties.Resources.Confirm))
            {
                Tenant.RemoveSdnProvider();
                AdminContext.ExecuteQueryRetry();
            }
        }
    }
}