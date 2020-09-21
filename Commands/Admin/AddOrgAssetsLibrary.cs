using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPOrgAssetsLibrary")]
    public class AddOrgAssetsLibrary : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string LibraryUrl;

        [Parameter(Mandatory = false)]
        public string ThumbnailUrl;

        [Parameter(Mandatory = false)]
        public SPOTenantCdnType CdnType = SPOTenantCdnType.Public;

        protected override void ExecuteCmdlet()
        {
            Tenant.AddToOrgAssetsLibAndCdn(CdnType, LibraryUrl, ThumbnailUrl);
            ClientContext.ExecuteQueryRetry();
        }
    }
}