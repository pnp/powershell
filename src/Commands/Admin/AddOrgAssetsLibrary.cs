using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.BrandCenter;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPOrgAssetsLibrary")]
    public class AddOrgAssetsLibrary : PnPSharePointOnlineAdminCmdlet
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
        public bool IsCopilotSearchable = false;

        protected override void ExecuteCmdlet()
        {
            var config = new OrgAssetsLibraryConfigParam();

            // Copilot search is only supported for ImageDocumentLibrary.
            // SharePoint has started enforcing this more strictly, so we guard against sending an invalid config.
            if (OrgAssetType != OrgAssetType.ImageDocumentLibrary)
            {
                if (ParameterSpecified(nameof(IsCopilotSearchable)) && IsCopilotSearchable)
                {
                    ThrowTerminatingError(new ErrorRecord(
                        new PSArgumentException("-IsCopilotSearchable can only be set to $true when -OrgAssetType is ImageDocumentLibrary."),
                        "IsCopilotSearchableUnsupportedForOrgAssetType",
                        ErrorCategory.InvalidArgument,
                        IsCopilotSearchable));
                }

                config.IsCopilotSearchable = false;
                config.IsCopilotSearchablePresent = false;
            }
            else
            {
                config.IsCopilotSearchable = IsCopilotSearchable;
                config.IsCopilotSearchablePresent = ParameterSpecified(nameof(IsCopilotSearchable));
            }

            Tenant.AddToOrgAssetsLibWithConfig(CdnType, LibraryUrl, ThumbnailUrl, OrgAssetType, DefaultOriginAdded, config);
            AdminContext.ExecuteQueryRetry();
        }
    }
}