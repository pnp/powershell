using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantCdnOrigin")]
    public class AddTenantCdnOrigin : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string OriginUrl;

        [Parameter(Mandatory = true)]
        public SPOTenantCdnType CdnType;

        protected override void ExecuteCmdlet()
        {
            Tenant.AddTenantCdnOrigin(CdnType, OriginUrl);
            AdminContext.ExecuteQueryRetry();
        }
    }
}