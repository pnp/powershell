using Microsoft.Online.SharePoint.TenantAdministration;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantRestrictedSearchMode")]
    public class SetTenantRestrictedSearchMode : PnPAdminCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        public RestrictedSearchMode mode;
        protected override void ExecuteCmdlet()
        {
            Tenant.SetSPORestrictedSearchMode(mode);
            AdminContext.ExecuteQueryRetry();
        }
    }
}
