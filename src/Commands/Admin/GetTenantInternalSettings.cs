using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantInternalSettings")]
    public class GetTenantInternalSettings : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            AdminContext.Load(Tenant);

            AdminContext.ExecuteQueryRetry();
            WriteObject(new SPOTenantInternalSettings(Tenant, AdminContext));
        }
    }
}
