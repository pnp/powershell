using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPTenantTheme")]
    public class RemoveTenantTheme : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ThemePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Tenant.DeleteTenantTheme(Identity.Name);
            AdminContext.ExecuteQueryRetry();
        }
    }
}