using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, " PnPTenantRestrictedSearchAllowedList")]
    public class GetTenantRestrictedSearchAllowedList : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var results = Tenant.GetSPORestrictedSearchAllowedList();
            AdminContext.ExecuteQueryRetry();
            WriteObject(results, true);
        }
    }
}