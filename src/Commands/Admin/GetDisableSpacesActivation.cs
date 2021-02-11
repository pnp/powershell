using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using Microsoft.Online.SharePoint.TenantAdministration;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPDisableSpacesActivation")]
    public class GetDisableSpacesActivation : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(Tenant, t => t.DisableSpacesActivation);
            ClientContext.ExecuteQueryRetry();

            WriteObject(Tenant.DisableSpacesActivation, false);
        }
    }
}
