using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPOrgAssetsLibrary")]
    public class RemoveOrgAssetsLibrary : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string LibraryUrl;

        [Parameter(Mandatory = false)]
        public bool ShouldRemoveFromCdn = false;

        [Parameter(Mandatory = false)]
        public SPOTenantCdnType CdnType = SPOTenantCdnType.Public;

        protected override void ExecuteCmdlet()
        {
            Tenant.RemoveFromOrgAssetsAndCdn(ShouldRemoveFromCdn, CdnType, LibraryUrl);
            AdminContext.ExecuteQueryRetry();
        }
    }
}