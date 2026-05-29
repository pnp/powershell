using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenant")]
    public class GetTenant : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            AdminContext.Load(Tenant);
            AdminContext.Load(
                Tenant,
                t => t.HideDefaultThemes,
                t => t.KnowledgeAgentSiteList,
                t => t.ContentSecurityPolicyConfigSynced,
                t => t.ArchivedFileStorageUsageMB,
                t => t.AllOrganizationSecurityGroupId,
                t => t.AllowAppsBypassOfUnmanagedDevicePolicy,
                t => t.BlockDownloadFileTypeIds,
                t => t.BlockDownloadFileTypePolicy,
                t => t.ContentTypeSyncSiteTemplatesList,
                t => t.DisabledAdaptiveCardExtensionIds,
                t => t.EnableNotificationsSubscriptions,
                t => t.EnforceRequestDigest,
                t => t.ExcludedBlockDownloadGroupIds,
                t => t.M365AdditionalStorageSPOEnabled,
                t => t.M365SharePointStorageEnabled,
                t => t.OneDriveOrganizationSharingLinkMaxExpirationInDays,
                t => t.OneDriveOrganizationSharingLinkRecommendedExpirationInDays,
                t => t.ReduceTempTokenLifetimeEnabled,
                t => t.ReduceTempTokenLifetimeValue,
                t => t.RestrictExternalSharing,
                t => t.TlsTokenBindingPolicyValue,
                t => t.AuthContextResilienceMode,
                t => t.VersionPolicyFileTypeOverride,
                t => t.ViewersCanCommentOnMediaDisabled);
            AdminContext.ExecuteQueryRetry();
            WriteObject(new SPOTenant(Tenant, AdminContext, this));
        }
    }
}
