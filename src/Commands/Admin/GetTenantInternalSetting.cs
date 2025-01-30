using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantInternalSetting")]
    public class GetTenantInternalSetting : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            AdminContext.Load(Tenant);

            AdminContext.ExecuteQueryRetry();
            WriteObject(new SPOTenantInternalSetting(Tenant, AdminContext));
        }
    }
}
