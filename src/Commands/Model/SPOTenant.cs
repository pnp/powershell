using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Administration;
using Microsoft.SharePoint.Client.Sharing;
using PnP.PowerShell.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace PnP.PowerShell.Commands.Model
{
    public class SPOTenant
    {
        #region Properties

        public bool? HideDefaultThemes { private set; get; }

        public long? StorageQuota { private set; get; }

        public long? StorageQuotaAllocated { private set; get; }

        public double? ResourceQuota { private set; get; }

        public double? ResourceQuotaAllocated { private set; get; }

        public long? OneDriveStorageQuota { private set; get; }

        public string CompatibilityRange { private set; get; }

        public bool? ExternalServicesEnabled { private set; get; }
        public string NoAccessRedirectUrl { private set; get; }

        public SharingCapabilities? SharingCapability { private set; get; }

        public bool? DisplayStartASiteOption { private set; get; }

        public string StartASiteFormUrl { private set; get; }

        public bool? ShowEveryoneClaim { private set; get; }

        public bool? ShowAllUsersClaim { private set; get; }

        public bool? OfficeClientADALDisabled { private set; get; }

        public bool? LegacyAuthProtocolsEnabled { private set; get; }

        public bool? ShowEveryoneExceptExternalUsersClaim { private set; get; }

        public bool? SearchResolveExactEmailOrUPN { private set; get; }

        public bool? RequireAcceptingAccountMatchInvitedAccount { private set; get; }

        public bool? ProvisionSharedWithEveryoneFolder { private set; get; }

        public string SignInAccelerationDomain { private set; get; }

        public bool? EnableGuestSignInAcceleration { private set; get; }

        public bool? UsePersistentCookiesForExplorerView { private set; get; }

        public bool? BccExternalSharingInvitations { private set; get; }

        public string BccExternalSharingInvitationsList { private set; get; }

        public bool? PublicCdnEnabled { private set; get; }

        public string PublicCdnAllowedFileTypes { private set; get; }

        public List<string> PublicCdnOrigins { private set; get; }

        public IList<SPOPublicCdnOrigin> PublicCdnOriginParsed => PublicCdnOrigins.Select(o => new SPOPublicCdnOrigin(o.Split(',')[1], o.Split(',')[0])).ToList();

        public int? RequireAnonymousLinksExpireInDays { private set; get; }

        public string SharingAllowedDomainList { private set; get; }

        public string SharingBlockedDomainList { private set; get; }

        public SharingDomainRestrictionModes? SharingDomainRestrictionMode { private set; get; }

        public bool? OneDriveForGuestsEnabled { private set; get; }

        public bool? IPAddressEnforcement { private set; get; }

        public string IPAddressAllowList { private set; get; }

        public int? IPAddressWACTokenLifetime { private set; get; }

        public bool? UseFindPeopleInPeoplePicker { private set; get; }

        public SharingLinkType DefaultSharingLinkType { private set; get; }

        public SharingState? ODBMembersCanShare { private set; get; }

        public SharingState? ODBAccessRequests { private set; get; }

        public bool? PreventExternalUsersFromResharing { private set; get; }

        public bool? ShowPeoplePickerSuggestionsForGuestUsers { private set; get; }

        public AnonymousLinkType? FileAnonymousLinkType { private set; get; }

        public AnonymousLinkType? FolderAnonymousLinkType { private set; get; }

        public bool? NotifyOwnersWhenItemsReshared { private set; get; }

        public bool? NotifyOwnersWhenInvitationsAccepted { private set; get; }

        public bool? NotificationsInOneDriveForBusinessEnabled { private set; get; }

        public bool? NotificationsInSharePointEnabled { private set; get; }

        public SpecialCharactersState? SpecialCharactersStateInFileFolderNames { private set; get; }

        public bool? OwnerAnonymousNotification { private set; get; }

        public bool? CommentsOnSitePagesDisabled { private set; get; }

        public bool? SocialBarOnSitePagesDisabled { private set; get; }

        public int? OrphanedPersonalSitesRetentionPeriod { private set; get; }

        public bool? PermissiveBrowserFileHandlingOverride { private set; get; }

        public bool? DisallowInfectedFileDownload { private set; get; }

        public SharingPermissionType DefaultLinkPermission { private set; get; }

        public SPOConditionalAccessPolicyType? ConditionalAccessPolicy { private set; get; }

        public bool? AllowDownloadingNonWebViewableFiles { private set; get; }

        public bool? AllowEditing { private set; get; }

        public bool? ApplyAppEnforcedRestrictionsToAdHocRecipients { private set; get; }

        public bool? FilePickerExternalImageSearchEnabled { private set; get; }

        public bool? EmailAttestationRequired { private set; get; }

        public int? EmailAttestationReAuthDays { private set; get; }

        public Guid[] DisabledWebPartIds { private set; get; }

        public bool? DisableCustomAppAuthentication { private set; get; }

        public SensitiveByDefaultState? MarkNewFilesSensitiveByDefault { private set; get; }

        public bool? StopNew2013Workflows { private set; get; }

        public bool? ViewInFileExplorerEnabled { private set; get; }

        public bool? DisableSpacesActivation { private set; get; }

        public bool? AllowFilesWithKeepLabelToBeDeletedSPO { private set; get; }

        public bool? AllowFilesWithKeepLabelToBeDeletedODB { private set; get; }

        public bool? DisableAddToOneDrive { private set; get; }

        public bool? IsFluidEnabled { private set; get; }

        public bool? DisablePersonalListCreation { private set; get; }

        public bool? ExternalUserExpirationRequired { private set; get; }

        public int? ExternalUserExpireInDays { private set; get; }

        public bool? DisplayNamesOfFileViewers { private set; get; }

        public bool? DisplayNamesOfFileViewersInSpo { private set; get; }

        public bool? IsLoopEnabled { private set; get; }

        public Guid[] DisabledModernListTemplateIds { private set; get; }

        public bool? RestrictedAccessControl { private set; get; }

        public bool? DisableDocumentLibraryDefaultLabeling { private set; get; }

        public bool? IsEnableAppAuthPopUpEnabled { private set; get; }

        public int? ExpireVersionsAfterDays { private set; get; }

        public int? MajorVersionLimit { private set; get; }

        public bool? EnableAutoExpirationVersionTrim { private set; get; }

        public bool? EnableAzureADB2BIntegration { private set; get; }

        public bool? SiteOwnerManageLegacyServicePrincipalEnabled { private set; get; }

        public bool? CoreRequestFilesLinkEnabled { private set; get; }

        public int? CoreRequestFilesLinkExpirationInDays { private set; get; }

        public bool? OneDriveRequestFilesLinkEnabled { private set; get; }

        public int? OneDriveRequestFilesLinkExpirationInDays { private set; get; }

        public bool? BusinessConnectivityServiceDisabled { private set; get; }

        public bool? EnableSensitivityLabelForPDF { private set; get; }

        public bool? IsDataAccessInCardDesignerEnabled { private set; get; }

        public bool? ShowPeoplePickerGroupSuggestionsForIB { private set; get; }

        public bool? InformationBarriersSuspension { private set; get; }

        public bool? IBImplicitGroupBased { private set; get; }

        public bool? AppBypassInformationBarriers { private set; get; }

        [CsomToModelConverter(Skip = true)]
        public Enums.InformationBarriersMode? DefaultOneDriveInformationBarrierMode { private set; get; }

        public SharingCapabilities? CoreSharingCapability { private set; get; }

        public TenantBrowseUserInfoPolicyValue? BlockUserInfoVisibilityInOneDrive { private set; get; }

        public bool? AllowOverrideForBlockUserInfoVisibility { private set; get; }

        public bool? AllowEveryoneExceptExternalUsersClaimInPrivateSite { private set; get; }

        public bool? AIBuilderEnabled { private set; get; }

        public bool? AllowSensitivityLabelOnRecords { private set; get; }

        public bool? AnyoneLinkTrackUsers { private set; get; }

        public bool? EnableSiteArchive { private set; get; }

        public bool? ESignatureEnabled { private set; get; }

        public TenantBrowseUserInfoPolicyValue? BlockUserInfoVisibilityInSharePoint { private set; get; }

        public SharingScope? OneDriveLoopDefaultSharingLinkScope { private set; get; }

        public SharingScope? CoreLoopDefaultSharingLinkScope { private set; get; }

        public SharingCapabilities? OneDriveLoopSharingCapability { private set; get; }

        public SharingCapabilities? CoreLoopSharingCapability { private set; get; }

        public Role? OneDriveLoopDefaultSharingLinkRole { private set; get; }

        public Role? CoreLoopDefaultSharingLinkRole { private set; get; }

        public bool? IsCollabMeetingNotesFluidEnabled { private set; get; }

        public SharingState? AllowAnonymousMeetingParticipantsToAccessWhiteboards { private set; get; }

        public SharingScope? OneDriveDefaultShareLinkScope { private set; get; }

        public Role? OneDriveDefaultShareLinkRole { private set; get; }

        public bool? OneDriveDefaultLinkToExistingAccess { private set; get; }

        public SharingState? OneDriveBlockGuestsAsSiteAdmin { private set; get; }

        public int? RecycleBinRetentionPeriod { private set; get; }

        public bool? EnableAIPIntegration { private set; get; }

        public SharingScope? CoreDefaultShareLinkScope { private set; get; }

        public Role? CoreDefaultShareLinkRole { private set; get; }

        public bool? SharePointAddInsDisabled { private set; get; }

        [CsomToModelConverter("ODBSharingCapability")]
        public SharingCapabilities? OneDriveSharingCapability { private set; get; }

        public string[] GuestSharingGroupAllowListInTenantByPrincipalIdentity { private set; get; }

        public bool? AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled { private set; get; }

        public bool? SelfServiceSiteCreationDisabled { private set; get; }

        public string WhoCanShareAllowListInTenant { private set; get; }

        public bool? ExtendPermissionsToUnprotectedFiles { private set; get; }

        public bool? LegacyBrowserAuthProtocolsEnabled { private set; get; }

        public bool? EnableDiscoverableByOrganizationForVideos { private set; get; }

        public string RestrictedAccessControlforSitesErrorHelpLink { private set; get; }

        public bool? Workflow2010Disabled { private set; get; }

        public bool? AllowSharingOutsideRestrictedAccessControlGroups { private set; get; }

        public Workflows2013State Workflows2013State { private set; get; }

        public bool? DisableVivaConnectionsAnalytics { private set; get; }

        public bool? HideSyncButtonOnDocLib { private set; get; }

        public bool? HideSyncButtonOnODB { private set; get; }

        public int? StreamLaunchConfig { private set; get; }

        public bool? EnableRestrictedAccessControl { private set; get; }

        public SPBlockDownloadFileTypeId[] BlockDownloadFileTypeIds { private set; get; }

        public Guid[] ExcludedBlockDownloadGroupIds { private set; get; }

        public bool? EnableMediaReactions { private set; get; }

        public bool? ContentSecurityPolicyEnforcement { private set; get; }

        #endregion

        public SPOTenant(Tenant tenant, ClientContext clientContext, Cmdlet cmdlet)
        {
            // Loop through all properties defined in this class and load the corresponding property from the Tenant object
            var properties = GetType().GetProperties();
            var failedProperties = 0;
            foreach(var property in properties)
            {
                var propertyName = property.Name;

                try
                {
                    // Check if the property has a CsomToModelConverter attribute, if so use the PropertyName defined in the attribute instead of looking for a property with the same name on the Tenant object
                    if(property.IsDefined(typeof(CsomToModelConverter)))
                    {
                        var converter = property.GetCustomAttribute<CsomToModelConverter>();

                        if(converter.Skip) continue;
                        propertyName = converter.PropertyName;
                    }
                    var tenantProperty = tenant.GetType().GetProperty(propertyName);
                    if (tenantProperty != null)
                    {
                        property.SetValue(this, tenantProperty.GetValue(tenant));
                    }
                }
                catch(Exception e)
                {
                    failedProperties++;
                    cmdlet.WriteVerbose($"Property {propertyName} not loaded due to error '{e.Message}'");
                }
            }

            // Load the properties that are not part of the Tenant object and require special handling to be retrieved
            try
            {
                var getAllowFilesWithKeepLabelToBeDeletedSPO = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAllowFilesWithKeepLabelToBeDeletedSPO(clientContext);
                var getAllowFilesWithKeepLabelToBeDeletedODB = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAllowFilesWithKeepLabelToBeDeletedODB(clientContext);
                clientContext.ExecuteQueryRetry();

                AllowFilesWithKeepLabelToBeDeletedSPO = getAllowFilesWithKeepLabelToBeDeletedSPO.Value;
                AllowFilesWithKeepLabelToBeDeletedODB = getAllowFilesWithKeepLabelToBeDeletedODB.Value;
            }
            catch(Exception e)
            {
                failedProperties++;
                cmdlet.WriteVerbose($"Property AllowFilesWithKeepLabelToBeDeletedSPO and/or AllowFilesWithKeepLabelToBeDeletedODB not loaded due to error '{e.Message}'");
            }

            // DefaultOneDriveInformationBarrierMode requires manual handling as it cannot be parsed directly from the Tenant object value
            try
            {
                DefaultOneDriveInformationBarrierMode = Enum.Parse<Enums.InformationBarriersMode>(tenant.DefaultODBMode);
            }
            catch(Exception e)
            {
                failedProperties++;
                cmdlet.WriteVerbose($"Property DefaultOneDriveInformationBarrierMode not loaded due to error '{e.Message}'");
            }

            // If one or more properties failed to load, show a warning
            if(failedProperties > 0)
            {
                cmdlet.WriteWarning($"Failed to load {(failedProperties != 1 ? $"{failedProperties} properties" : "one property")}. Use -Verbose to see the details.");
            }
        }
    }
}
