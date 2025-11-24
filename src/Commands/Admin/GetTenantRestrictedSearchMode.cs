using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantRestrictedSearchMode")]
    public class GetTenantRestrictedSearchMode : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            try
            {
                var results = Tenant.GetSPORestrictedSearchMode();
                AdminContext.ExecuteQueryRetry();
                WriteObject(results, true);
            }
            catch
            {
                WriteObject("Restricted search mode is currently not set.");
            }
        }
    }
}
