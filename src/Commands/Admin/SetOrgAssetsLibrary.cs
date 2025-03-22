using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.BrandCenter;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPOrgAssetsLibrary")]
    public class SetOrgAssetsLibrary : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string LibraryUrl;

        [Parameter(Mandatory = false)]
        public string ThumbnailUrl;

        [Parameter(Mandatory = false)]
        public OrgAssetType? OrgAssetType;

        [Parameter(Mandatory = false)]
        public bool? IsCopilotSearchable;

        protected override void ExecuteCmdlet()
        {
            if(ParameterSpecified(nameof(IsCopilotSearchable)) && ParameterSpecified(nameof(OrgAssetType)) && ParameterSpecified(nameof(ThumbnailUrl)))
            {
                LogDebug("Setting org assets library with thumbnail url, organizational asset type and copilot searchable");
                Tenant.SetOrgAssetsWithConfig(LibraryUrl, ThumbnailUrl, OrgAssetType.Value, new OrgAssetsLibraryConfigParam { IsCopilotSearchable = IsCopilotSearchable.Value});
            }
            else if(ParameterSpecified(nameof(IsCopilotSearchable)) && ParameterSpecified(nameof(OrgAssetType)))
            {
                LogDebug("Setting org assets library with organizational asset type and copilot searchable");
                Tenant.SetOrgAssetsWithConfig(LibraryUrl, null, OrgAssetType.Value, new OrgAssetsLibraryConfigParam { IsCopilotSearchable = IsCopilotSearchable.Value});
            }
            else if(ParameterSpecified(nameof(OrgAssetType)) && ParameterSpecified(nameof(ThumbnailUrl)))
            {
                LogDebug("Setting org assets library with thumbnail url and organizational asset type");
                Tenant.SetOrgAssetsWithType(LibraryUrl, ThumbnailUrl, OrgAssetType.Value);
            }
            else if(ParameterSpecified(nameof(OrgAssetType)))
            {
                LogDebug("Setting org assets library with organizational asset type");
                Tenant.SetOrgAssetsWithType(LibraryUrl, null, OrgAssetType.Value);
            }
            else if(ParameterSpecified(nameof(ThumbnailUrl)))
            {
                LogDebug("Setting org assets library with thumbnail url");
                Tenant.SetOrgAssets(LibraryUrl, ThumbnailUrl);
            }
            AdminContext.ExecuteQueryRetry();
        }
    }
}