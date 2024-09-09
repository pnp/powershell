using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.BrandCenter;

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

        [Parameter(Mandatory = false)]
        public OrgAssetType OrgAssetType = OrgAssetType.ImageDocumentLibrary;

        [Parameter(Mandatory = false)]
        public bool DefaultOriginAdded = true;

        [Parameter(Mandatory = false)]
        public bool IsCopilotSearchable = true;

        protected override void ExecuteCmdlet()
        {
            Tenant.AddToOrgAssetsLibWithConfig(CdnType, LibraryUrl, ThumbnailUrl, OrgAssetType, DefaultOriginAdded, new OrgAssetsLibraryConfigParam { IsCopilotSearchable = IsCopilotSearchable});
            AdminContext.ExecuteQueryRetry();
        }
    }
}