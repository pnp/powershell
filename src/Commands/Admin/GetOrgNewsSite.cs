using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPOrgNewsSite")]
    public class GetOrgNewsSite : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var results = Tenant.GetOrgNewsSites();
            AdminContext.ExecuteQueryRetry();
            WriteObject(results, true);
        }
    }
}