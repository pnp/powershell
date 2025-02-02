using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantCdnEnabled")]
    public class GetTenantCdnEnabled : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SPOTenantCdnType CdnType;

        protected override void ExecuteCmdlet()
        {
            var result = Tenant.GetTenantCdnEnabled(CdnType);
            AdminContext.ExecuteQueryRetry();
            WriteObject(result);
        }
    }
}