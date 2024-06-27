using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantRestrictedSearchMode")]
    public class GetTenantRestrictedSearchMode : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var results = Tenant.GetSPORestrictedSearchMode();
            AdminContext.ExecuteQueryRetry();
            WriteObject(results, true);
        }
    }
}
