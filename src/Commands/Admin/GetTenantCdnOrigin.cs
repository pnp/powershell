using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantCdnOrigin")]
    public class GetTenantCdnOrigin : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SPOTenantCdnType CdnType;

        protected override void ExecuteCmdlet()
        {
            var origins = Tenant.GetTenantCdnOrigins(CdnType);
            AdminContext.ExecuteQueryRetry();
            WriteObject(origins, true);
        }
    }
}