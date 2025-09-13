using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Administration;
using Microsoft.SharePoint.Client.Sharing;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using InformationBarriersMode = PnP.PowerShell.Commands.Enums.InformationBarriersMode;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPTenant", DefaultParameterSetName = ParameterAttribute.AllParameterSets)]
    public class SetTenant : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public int MinCompatibilityLevel;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public int MaxCompatibilityLevel;

        [Parameter(Mandatory = false)]
        public bool? ExternalServicesEnabled;

        [Parameter(Mandatory = false)]
        public string NoAccessRedirectUrl;

        [Parameter(Mandatory = false)]
        public SharingCapabilities? SharingCapability;

        [Parameter(Mandatory = false)]
        public bool? DisplayStartASiteOption;

        [Parameter(Mandatory = false)]
        public string StartASiteFormUrl;

        [Parameter(Mandatory = false)]
        public bool? ShowEveryoneClaim;

        [Parameter(Mandatory = false)]
        public bool? ShowAllUsersClaim;

        [Parameter(Mandatory = false)]
        public bool? ShowEveryoneExceptExternalUsersClaim;

        [Parameter(Mandatory = false)]
        public bool? SearchResolveExactEmailOrUPN;

        [Parameter(Mandatory = false)]
        public bool? OfficeClientADALDisabled;

        [Parameter(Mandatory = false)]
        public bool? LegacyAuthProtocolsEnabled;

        [Parameter(Mandatory = false)]
        public bool? RequireAcceptingAccountMatchInvitedAccount;

        [Parameter(Mandatory = false)]
        public bool? ProvisionSharedWithEveryoneFolder;

        [Parameter(Mandatory = false)]
        public string SignInAccelerationDomain;

        [Parameter(Mandatory = false)]
        public bool? EnableGuestSignInAcceleration;

        [Parameter(Mandatory = false)]
        public bool? UsePersistentCookiesForExplorerView;

        [Parameter(Mandatory = false)]
        public bool? BccExternalSharingInvitations;

        [Parameter(Mandatory = false)]
        public string BccExternalSharingInvitationsList;

        [Parameter(Mandatory = false)]
        public bool? PublicCdnEnabled;

        [Parameter(Mandatory = false)]
        public string PublicCdnAllowedFileTypes;

        [Parameter(Mandatory = false)]
        public int? RequireAnonymousLinksExpireInDays;

        [Parameter(Mandatory = false)]
        public string SharingAllowedDomainList;

        [Parameter(Mandatory = false)]
        public string SharingBlockedDomainList;

        [Parameter(Mandatory = false)]
        public SharingDomainRestrictionModes? SharingDomainRestrictionMode;

        [Parameter(Mandatory = false)]
        public long? OneDriveStorageQuota;

        [Parameter(Mandatory = false)]
        public bool? OneDriveForGuestsEnabled;

        [Parameter(Mandatory = false)]
        public bool? IPAddressEnforcement;

        [Parameter(Mandatory = false)]
        public string IPAddressAllowList;

        [Parameter(Mandatory = false)]
        public int? IPAddressWACTokenLifetime;

        [Parameter(Mandatory = false)]
        public bool? UseFindPeopleInPeoplePicker;

        [Parameter(Mandatory = false)]
        public SharingLinkType? DefaultSharingLinkType;

        [Parameter(Mandatory = false)]
        public SharingState? ODBMembersCanShare;

        [Parameter(Mandatory = false)]
        public SharingState? ODBAccessRequests;

        [Parameter(Mandatory = false)]
        public bool? PreventExternalUsersFromResharing;

        [Parameter(Mandatory = false)]
        public bool? ShowPeoplePickerSuggestionsForGuestUsers;

        [Parameter(Mandatory = false)]
        public AnonymousLinkType? FileAnonymousLinkType;

        [Parameter(Mandatory = false)]
        public AnonymousLinkType? FolderAnonymousLinkType;

        [Parameter(Mandatory = false)]
        public bool? NotifyOwnersWhenItemsReshared;

        [Parameter(Mandatory = false)]
        public bool? NotifyOwnersWhenInvitationsAccepted;

        [Parameter(Mandatory = false)]
        public bool? NotificationsInOneDriveForBusinessEnabled;

        [Parameter(Mandatory = false)]
        public bool? NotificationsInSharePointEnabled;

        [Parameter(Mandatory = false)]
        public SpecialCharactersState? SpecialCharactersStateInFileFolderNames { get; set; }

        [Parameter(Mandatory = false)]
        public bool? OwnerAnonymousNotification;

        [Parameter(Mandatory = false)]
        public bool? CommentsOnSitePagesDisabled;

        [Parameter(Mandatory = false)]
        public bool? SocialBarOnSitePagesDisabled;

        [Parameter(Mandatory = false)]
        public int? OrphanedPersonalSitesRetentionPeriod;

        [Parameter(Mandatory = false)]
        public bool? DisallowInfectedFileDownload;

        [Parameter(Mandatory = false)]
        public SharingPermissionType? DefaultLinkPermission;

        [Parameter(Mandatory = false)]
        public SPOConditionalAccessPolicyType? ConditionalAccessPolicy;

        [Parameter(Mandatory = false)]
        public bool? AllowDownloadingNonWebViewableFiles;

        [Parameter(Mandatory = false)]
        public bool? AllowEditing;

        [Parameter(Mandatory = false)]
        public bool? ApplyAppEnforcedRestrictionsToAdHocRecipients;

        [Parameter(Mandatory = false)]
        public bool? FilePickerExternalImageSearchEnabled;

        [Parameter(Mandatory = false)]
        public bool? EmailAttestationRequired;

        [Parameter(Mandatory = false)]
        public int? EmailAttestationReAuthDays;

        [Parameter(Mandatory = false)]
        public bool? HideDefaultThemes;

        [Parameter(Mandatory = false)]
        public Guid[] DisabledWebPartIds;

        [Parameter(Mandatory = false)]
        public bool? EnableAIPIntegration;

        [Parameter(Mandatory = false)]
        public bool? DisableCustomAppAuthentication;

        [Parameter(Mandatory = false, HelpMessage = "Boolean indicating if a news digest should automatically be sent to end users to inform them about news that they may have missed. On by default. For more information, see https://aka.ms/autonewsdigest")]
        public bool? EnableAutoNewsDigest;

        [Parameter(Mandatory = false)]
        public bool? CommentsOnListItemsDisabled;

        [Parameter(Mandatory = false)]
        public bool? CommentsOnFilesDisabled;

        [Parameter(Mandatory = false)]
        public bool? AllowCommentsTextOnEmailEnabled;

        [Parameter(Mandatory = false)]
        public SensitiveByDefaultState? MarkNewFilesSensitiveByDefault;

        [Parameter(Mandatory = false)]
        public bool? DisableBackToClassic;

        [Parameter(Mandatory = false)]
        public bool? StopNew2013Workflows;

        [Parameter(Mandatory = false)]
        public bool? ViewInFileExplorerEnabled;

        [Parameter(Mandatory = false)]
        public bool? InformationBarriersSuspension;

        [Parameter(Mandatory = false)]
        public bool? AllowFilesWithKeepLabelToBeDeletedSPO;

        [Parameter(Mandatory = false)]
        public bool? AllowFilesWithKeepLabelToBeDeletedODB;

        [Parameter(Mandatory = false)]
        [Alias("DisableAddShortcutsToOneDrive")]
        public bool? DisableAddToOneDrive;

        [Parameter(Mandatory = false)]
        public bool? IsFluidEnabled;

        [Parameter(Mandatory = false)]
        public bool? DisablePersonalListCreation;

        [Parameter(Mandatory = false)]
        public Guid[] DisabledModernListTemplateIds;

        [Parameter(Mandatory = false)]
        public Guid[] EnableModernListTemplateIds;

        [Parameter(Mandatory = false)]
        public bool? ExternalUserExpirationRequired;

        [Parameter(Mandatory = false)]
        public int? ExternalUserExpireInDays;

        [Parameter(Mandatory = false)]
        public bool? DisplayNamesOfFileViewers;

        [Parameter(Mandatory = false)]
        public bool? DisplayNamesOfFileViewersInSpo;

        [Parameter(Mandatory = false)]
        public bool? IsLoopEnabled;

        [Parameter(Mandatory = false)]
        public bool? OneDriveRequestFilesLinkEnabled;

        [Parameter(Mandatory = false)]
        public bool? EnableRestrictedAccessControl;

        [Parameter(Mandatory = false)]
        public bool? EnableAzureADB2BIntegration;

        [Parameter(Mandatory = false)]
        public bool? CoreRequestFilesLinkEnabled;

        [Parameter(Mandatory = false)]
        public int? CoreRequestFilesLinkExpirationInDays;

        [Parameter(Mandatory = false)]
        public string LabelMismatchEmailHelpLink;

        [Parameter(Mandatory = false)]
        public bool? DisableDocumentLibraryDefaultLabeling { get; set; }

        [Parameter(Mandatory = false)]
        public bool? IsEnableAppAuthPopUpEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool? EnableAutoExpirationVersionTrim { get; set; }

        [Parameter(Mandatory = false)]
        public int? ExpireVersionsAfterDays { get; set; }

        [Parameter(Mandatory = false)]
        public int? MajorVersionLimit { get; set; }

        [Parameter(Mandatory = false)]
        public SharingCapabilities? OneDriveLoopSharingCapability { get; set; }

        [Parameter(Mandatory = false)]
        public SharingScope? OneDriveLoopDefaultSharingLinkScope { get; set; }

        [Parameter(Mandatory = false)]
        public Role? OneDriveLoopDefaultSharingLinkRole { get; set; }

        [Parameter(Mandatory = false)]
        public SharingCapabilities? CoreLoopSharingCapability { get; set; }

        [Parameter(Mandatory = false)]
        public SharingScope? CoreLoopDefaultSharingLinkScope { get; set; }

        [Parameter(Mandatory = false)]
        public Role? CoreLoopDefaultSharingLinkRole { get; set; }

        [Parameter(Mandatory = false)]
        public bool? DisableVivaConnectionsAnalytics { get; set; }

        [Parameter(Mandatory = false)]
        public bool? CoreDefaultLinkToExistingAccess { get; set; }

        [Parameter(Mandatory = false)]
        public bool? HideSyncButtonOnTeamSite { get; set; }

        [Parameter(Mandatory = false)]
        public SharingState? CoreBlockGuestsAsSiteAdmin { get; set; }

        [Parameter(Mandatory = false)]
        public bool? IsWBFluidEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool? IsCollabMeetingNotesFluidEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public SharingState? AllowAnonymousMeetingParticipantsToAccessWhiteboards { get; set; }

        [Parameter(Mandatory = false)]
        public bool? IBImplicitGroupBased { get; set; }

        [Parameter(Mandatory = false)]
        public bool? ShowOpenInDesktopOptionForSyncedFiles { get; set; }

        [Parameter(Mandatory = false)]
        public bool? ShowPeoplePickerGroupSuggestionsForIB { get; set; }

        [Parameter(Mandatory = false)]
        public int? OneDriveRequestFilesLinkExpirationInDays { get; set; }

        [Parameter(Mandatory = false)]
        public bool? BlockDownloadFileTypePolicy { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        public SPBlockDownloadFileTypeId[] BlockDownloadFileTypeIds { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        public Guid[] ExcludedBlockDownloadGroupIds { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public string ArchiveRedirectUrl { get; set; }

        [Parameter(Mandatory = false)]
        public bool? BlockSendLabelMismatchEmail { get; set; }

        [Parameter(Mandatory = false)]
        public MediaTranscriptionPolicyType? MediaTranscription { get; set; }

        [Parameter(Mandatory = false)]
        public MediaTranscriptionAutomaticFeaturesPolicyType? MediaTranscriptionAutomaticFeatures { get; set; }

        [Parameter(Mandatory = false)]
        public bool? SiteOwnerManageLegacyServicePrincipalEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool? ReduceTempTokenLifetimeEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public int? ReduceTempTokenLifetimeValue;

        [Parameter(Mandatory = false)]
        public bool? ViewersCanCommentOnMediaDisabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool? AllowGuestUserShareToUsersNotInSiteCollection { get; set; }

        [Parameter(Mandatory = false)]
        public string ConditionalAccessPolicyErrorHelpLink;

        [Parameter(Mandatory = false)]
        public string CustomizedExternalSharingServiceUrl;

        [Parameter(Mandatory = false)]
        public bool? IncludeAtAGlanceInShareEmails { get; set; }

        [Parameter(Mandatory = false)]
        public bool? MassDeleteNotificationDisabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool? BusinessConnectivityServiceDisabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool? EnableSensitivityLabelForPDF { get; set; }

        [Parameter(Mandatory = false)]
        public bool? IsDataAccessInCardDesignerEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool? AppBypassInformationBarriers { get; set; }

        [Parameter(Mandatory = false)]
        public InformationBarriersMode? DefaultOneDriveInformationBarrierMode { get; set; }

        [Parameter(Mandatory = false)]
        public SharingCapabilities? CoreSharingCapability { get; set; }

        [Parameter(Mandatory = false)]
        public TenantBrowseUserInfoPolicyValue? BlockUserInfoVisibilityInOneDrive;

        [Parameter(Mandatory = false)]
        public bool? AllowOverrideForBlockUserInfoVisibility { get; set; }

        [Parameter(Mandatory = false)]
        public bool? AllowEveryoneExceptExternalUsersClaimInPrivateSite { get; set; }

        [Parameter(Mandatory = false)]
        public bool? AIBuilderEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool? AllowSensitivityLabelOnRecords { get; set; }

        [Parameter(Mandatory = false)]
        public bool? AnyoneLinkTrackUsers { get; set; }

        [Parameter(Mandatory = false)]
        public bool? EnableSiteArchive { get; set; }

        [Parameter(Mandatory = false)]
        public bool? ESignatureEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public TenantBrowseUserInfoPolicyValue? BlockUserInfoVisibilityInSharePoint { get; set; }

        [Parameter(Mandatory = false)]
        public SharingScope? OneDriveDefaultShareLinkScope { get; set; }

        [Parameter(Mandatory = false)]
        public Role? OneDriveDefaultShareLinkRole { get; set; }

        [Parameter(Mandatory = false)]
        public bool? OneDriveDefaultLinkToExistingAccess { get; set; }

        [Parameter(Mandatory = false)]
        public SharingState? OneDriveBlockGuestsAsSiteAdmin { get; set; }

        [Parameter(Mandatory = false)]
        public int? RecycleBinRetentionPeriod { get; set; }

        [Parameter(Mandatory = false)]
        public bool? IsSharePointAddInsDisabled { get; set; }

        [Parameter(Mandatory = false)]
        public SharingScope? CoreDefaultShareLinkScope { private set; get; }

        [Parameter(Mandatory = false)]
        public Role? CoreDefaultShareLinkRole { private set; get; }

        [Parameter(Mandatory = false)]
        public SharingCapabilities? OneDriveSharingCapability { private set; get; }

        [Parameter(Mandatory = false)]
        public string[] GuestSharingGroupAllowListInTenantByPrincipalIdentity { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? SelfServiceSiteCreationDisabled { private set; get; }

        [Parameter(Mandatory = false)]
        public SwitchParameter SyncAadB2BManagementPolicy { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? ExtendPermissionsToUnprotectedFiles { private set; get; }

        [Parameter(Mandatory = false)]
        public string WhoCanShareAllowListInTenant { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? LegacyBrowserAuthProtocolsEnabled { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? EnableDiscoverableByOrganizationForVideos { private set; get; }

        [Parameter(Mandatory = false)]
        public string RestrictedAccessControlforSitesErrorHelpLink { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? Workflow2010Disabled { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? AllowSharingOutsideRestrictedAccessControlGroups { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? HideSyncButtonOnDocLib { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? HideSyncButtonOnODB { private set; get; }

        [Parameter(Mandatory = false)]
        public int? StreamLaunchConfig { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? EnableMediaReactions { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? ContentSecurityPolicyEnforcement { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? DisableSpacesActivation { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? AllowClassicPublishingSiteCreation { private set; get; }

        [Parameter(Mandatory = false)]
        public bool? DelayDenyAddAndCustomizePagesEnforcementOnClassicPublishingSites { private set; get; }

        protected override void ExecuteCmdlet()
        {
            AdminContext.Load(Tenant);
            AdminContext.ExecuteQueryRetry();

            bool modified = false;
            if (MinCompatibilityLevel != 0 && MaxCompatibilityLevel != 0)
            {
                var compatibilityRange = $"{MinCompatibilityLevel},{MaxCompatibilityLevel}";
                Tenant.CompatibilityRange = compatibilityRange;
                modified = true;
            }
            else if (MinCompatibilityLevel != 0 || MaxCompatibilityLevel != 0)
            {
                throw new ArgumentNullException("You must specify both Min & Max compatibility levels together");
            }
            if (NoAccessRedirectUrl != null)
            {
                Tenant.NoAccessRedirectUrl = NoAccessRedirectUrl;
                modified = true;
            }

            if (CustomizedExternalSharingServiceUrl != null)
            {
                Tenant.CustomizedExternalSharingServiceUrl = CustomizedExternalSharingServiceUrl;
                modified = true;
            }

            if (ExternalServicesEnabled.HasValue)
            {
                Tenant.ExternalServicesEnabled = ExternalServicesEnabled.Value;
                modified = true;
            }
            if (DisplayStartASiteOption.HasValue)
            {
                Tenant.DisplayStartASiteOption = DisplayStartASiteOption.Value;
                modified = true;
            }
            if (SharingCapability != null)
            {
                Tenant.SharingCapability = SharingCapability.Value;
                modified = true;
            }
            if (StartASiteFormUrl != null)
            {
                Tenant.StartASiteFormUrl = StartASiteFormUrl;
                modified = true;
            }
            if (ShowEveryoneClaim.HasValue)
            {
                Tenant.ShowEveryoneClaim = ShowEveryoneClaim.Value;
                modified = true;
            }
            if (ShowAllUsersClaim.HasValue)
            {
                Tenant.ShowAllUsersClaim = ShowAllUsersClaim.Value;
                modified = true;
            }
            if (OfficeClientADALDisabled.HasValue)
            {
                Tenant.OfficeClientADALDisabled = OfficeClientADALDisabled.Value;
                modified = true;
            }
            if (LegacyAuthProtocolsEnabled.HasValue)
            {
                Tenant.LegacyAuthProtocolsEnabled = LegacyAuthProtocolsEnabled.Value;
                modified = true;
            }
            if (ShowEveryoneExceptExternalUsersClaim.HasValue)
            {
                Tenant.ShowEveryoneExceptExternalUsersClaim = ShowEveryoneExceptExternalUsersClaim.Value;
                modified = true;
            }
            if (SearchResolveExactEmailOrUPN.HasValue)
            {
                Tenant.SearchResolveExactEmailOrUPN = SearchResolveExactEmailOrUPN.Value;
                modified = true;
            }
            if (RequireAcceptingAccountMatchInvitedAccount.HasValue)
            {
                Tenant.RequireAcceptingAccountMatchInvitedAccount = RequireAcceptingAccountMatchInvitedAccount.Value;
                modified = true;
            }
            if (ProvisionSharedWithEveryoneFolder.HasValue)
            {
                Tenant.ProvisionSharedWithEveryoneFolder = ProvisionSharedWithEveryoneFolder.Value;
                modified = true;
            }
            if (SignInAccelerationDomain != null && (Force || ShouldContinue($@"Please confirm that ""{SignInAccelerationDomain}"" is correct, and you have federated sign-in configured for that domain. Otherwise, your users will no longer be able to sign in. Do you want to continue?", Properties.Resources.Confirm)))
            {
                Tenant.SignInAccelerationDomain = SignInAccelerationDomain;
                modified = true;
            }
            if (EnableGuestSignInAcceleration.HasValue)
            {
                if (string.IsNullOrWhiteSpace(Tenant.SignInAccelerationDomain))
                {
                    throw new InvalidOperationException("This setting cannot be changed until you set the SignInAcceleration Domain.");
                }
                if (Force || ShouldContinue("Make sure that your federated sign-in supports guest users. If it doesn’t, your guest users will no longer be able to sign in after you set EnableGuestSignInAcceleration to $true.", Properties.Resources.Confirm))
                {
                    Tenant.EnableGuestSignInAcceleration = EnableGuestSignInAcceleration.Value;
                    modified = true;
                }
            }
            if (DisableBackToClassic.HasValue)
            {
                Tenant.DisableBackToClassic = DisableBackToClassic.Value;
                modified = true;
            }
            if (UsePersistentCookiesForExplorerView.HasValue)
            {
                Tenant.UsePersistentCookiesForExplorerView = UsePersistentCookiesForExplorerView.Value;
                modified = true;
            }
            if (BccExternalSharingInvitations.HasValue && (!BccExternalSharingInvitations.Value || (BccExternalSharingInvitations.Value && (Force || ShouldContinue("The recipients listed in BccExternalSharingInvitationsList will be blind copied on all external sharing invitations. Do you want to continue?", Properties.Resources.Confirm)))))
            {
                Tenant.BccExternalSharingInvitations = BccExternalSharingInvitations.Value;
                modified = true;
            }
            if (!string.IsNullOrEmpty(BccExternalSharingInvitationsList))
            {
                Tenant.BccExternalSharingInvitationsList = BccExternalSharingInvitationsList;
                modified = true;
            }
            if (PublicCdnEnabled != null)
            {
                Tenant.PublicCdnEnabled = PublicCdnEnabled.Value;
                modified = true;
            }
            if (!string.IsNullOrEmpty(PublicCdnAllowedFileTypes))
            {
                Tenant.PublicCdnAllowedFileTypes = PublicCdnAllowedFileTypes;
                modified = true;
            }
            if (RequireAnonymousLinksExpireInDays.HasValue)
            {
                try
                {
                    Tenant.EnsureProperty(t => t.SharingCapability);
                    if (Tenant.SharingCapability != SharingCapabilities.ExternalUserAndGuestSharing)
                    {
                        LogWarning("Warning: anonymous links are not enabled on your tenant. Enable them with SharingCapability.");
                    }
                    if (RequireAnonymousLinksExpireInDays.Value != 0 && (RequireAnonymousLinksExpireInDays.Value < 1 || RequireAnonymousLinksExpireInDays.Value > 730))
                    {
                        throw new ArgumentException("RequireAnonymousLinksExpireInDays must have a value between 1 and 730");
                    }
                    int requireAnonymousLinksExpireInDays = Tenant.EnsureProperty(t => t.RequireAnonymousLinksExpireInDays);
                    if (requireAnonymousLinksExpireInDays != RequireAnonymousLinksExpireInDays.Value)
                    {
                        Tenant.RequireAnonymousLinksExpireInDays = RequireAnonymousLinksExpireInDays.Value;
                        modified = true;
                    }
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property RequireAnonymousLinksExpireInDays is not supported by your version of the service");
                }
            }
            if (SharingAllowedDomainList != null)
            {
                if (!Tenant.RequireAcceptingAccountMatchInvitedAccount)
                {
                    LogWarning("We automatically enabled RequireAcceptingAccountMatchInvitedAccount because you selected to limit external sharing using domains.");
                    Tenant.RequireAcceptingAccountMatchInvitedAccount = true;
                }
                Tenant.SharingAllowedDomainList = SharingAllowedDomainList;
                modified = true;
                if ((SharingDomainRestrictionMode == null && Tenant.SharingDomainRestrictionMode != SharingDomainRestrictionModes.AllowList) || SharingDomainRestrictionMode == SharingDomainRestrictionModes.None)
                {
                    LogWarning("You must set SharingDomainRestrictionMode to AllowList in order to have the list of domains you configured for SharingAllowedDomainList to take effect.");
                }
                else if (SharingDomainRestrictionMode == SharingDomainRestrictionModes.BlockList)
                {
                    LogWarning("The list of domains in SharingAllowedDomainsList is ignored when you set the SharingDomainRestrictionMode to BlockList. Set the list of blocked domains using the SharingBlockedDomainsList parameter.");
                }
            }
            if (PreventExternalUsersFromResharing.HasValue)
            {
                Tenant.PreventExternalUsersFromResharing = PreventExternalUsersFromResharing.Value;
                modified = true;
            }
            if (NotifyOwnersWhenItemsReshared.HasValue)
            {
                Tenant.NotifyOwnersWhenItemsReshared = NotifyOwnersWhenItemsReshared.Value;
                modified = true;
            }
            if (NotifyOwnersWhenInvitationsAccepted.HasValue)
            {
                Tenant.NotifyOwnersWhenInvitationsAccepted = NotifyOwnersWhenInvitationsAccepted.Value;
                modified = true;
            }
            if (NotificationsInOneDriveForBusinessEnabled.HasValue)
            {
                Tenant.NotificationsInOneDriveForBusinessEnabled = NotificationsInOneDriveForBusinessEnabled.Value;
                modified = true;
            }
            if (NotificationsInSharePointEnabled.HasValue)
            {
                Tenant.NotificationsInSharePointEnabled = NotificationsInSharePointEnabled.Value;
                modified = true;
            }
            if (SpecialCharactersStateInFileFolderNames.HasValue)
            {
                Tenant.SpecialCharactersStateInFileFolderNames = SpecialCharactersStateInFileFolderNames.Value;
                modified = true;
            }
            if (OwnerAnonymousNotification.HasValue)
            {
                Tenant.OwnerAnonymousNotification = OwnerAnonymousNotification.Value;
                modified = true;
            }
            if (OrphanedPersonalSitesRetentionPeriod.HasValue)
            {
                if (OrphanedPersonalSitesRetentionPeriod.Value < 30 || OrphanedPersonalSitesRetentionPeriod > 3650)
                {
                    throw new ArgumentException("OrphanedPersonalSitesRetentionPeriod must have a value between 30 and 3650");
                }
                if (Force || ShouldContinue("This will update the Retention Policy for All Orphaned OneDrive for Business sites.", Properties.Resources.Confirm))
                {
                    try
                    {
                        Tenant.OrphanedPersonalSitesRetentionPeriod = OrphanedPersonalSitesRetentionPeriod.Value;
                        modified = true;
                    }
                    catch (PropertyOrFieldNotInitializedException)
                    {
                        throw new InvalidOperationException("Setting the property OrphanedPersonalSitesRetentionPeriod is not supported by your version of the service");
                    }
                }
            }

            if (DisallowInfectedFileDownload.HasValue)
            {
                Tenant.DisallowInfectedFileDownload = DisallowInfectedFileDownload.Value;
                modified = true;
            }
            if (!string.IsNullOrEmpty(SharingBlockedDomainList))
            {
                if (!Tenant.RequireAcceptingAccountMatchInvitedAccount)
                {
                    LogWarning("We automatically enabled RequireAcceptingAccountMatchInvitedAccount because you selected to limit external sharing using domains.");
                    Tenant.RequireAcceptingAccountMatchInvitedAccount = true;
                }
                Tenant.SharingBlockedDomainList = SharingBlockedDomainList;
                modified = true;
                if ((SharingDomainRestrictionMode == null && Tenant.SharingDomainRestrictionMode != SharingDomainRestrictionModes.BlockList) || SharingDomainRestrictionMode == SharingDomainRestrictionModes.None)
                {
                    LogWarning("You must set SharingDomainRestrictionMode to BlockList in order to have the list of domains you configured for SharingBlockedDomainList to take effect");
                }
                else if (SharingDomainRestrictionMode == SharingDomainRestrictionModes.AllowList)
                {
                    LogWarning("The list of domains in SharingBlockedDomainsList is ignored when you set the SharingDomainRestrictionMode to AllowList.Set the list of allowed domains using the SharingAllowedDomainsList parameter.");
                }
            }
            if (SharingDomainRestrictionMode.HasValue)
            {
                if (SharingDomainRestrictionMode == SharingDomainRestrictionModes.AllowList && string.IsNullOrEmpty(Tenant.SharingAllowedDomainList))
                {
                    throw new InvalidOperationException("You set the SharingDomainRestrictionMode to AllowList but there are no domains to allow. Specify the list of allowed domains using the SharingAllowedDomainList parameter.");
                }
                if (SharingDomainRestrictionMode == SharingDomainRestrictionModes.BlockList && string.IsNullOrEmpty(Tenant.SharingBlockedDomainList))
                {
                    throw new InvalidOperationException("You set the SharingDomainRestrictionMode to BlockList but there are no domains to block. Specify the list of blocked domains using the SharingBlockedDomainsList parameter.");
                }
                if (!Tenant.RequireAcceptingAccountMatchInvitedAccount)
                {
                    LogWarning("We automatically enabled RequireAcceptingAccountMatchInvitedAccount because you selected to limit external sharing using domains.");
                    Tenant.RequireAcceptingAccountMatchInvitedAccount = true;
                }
                Tenant.SharingDomainRestrictionMode = SharingDomainRestrictionMode.Value;
                modified = true;
            }
            if (OneDriveStorageQuota.HasValue)
            {
                Tenant.OneDriveStorageQuota = OneDriveStorageQuota.Value;
                modified = true;
            }
            if (OneDriveForGuestsEnabled.HasValue)
            {
                string message = OneDriveForGuestsEnabled.Value ? "This will enable all users, including guests, to create OneDrive for Business sites. You must first assign OneDrive for Business licenses to the guests before they can create their OneDrive for Business sites." : "Guests will no longer be able to create new OneDrive for Business sites. Existing sites won’t be impacted.";
                if (Force || ShouldContinue(message, Properties.Resources.Confirm))
                {
                    Tenant.OneDriveForGuestsEnabled = OneDriveForGuestsEnabled.Value;
                    modified = true;
                }
            }
            if (IPAddressEnforcement.HasValue)
            {
                if (IPAddressEnforcement == true && string.IsNullOrEmpty(Tenant.IPAddressAllowList))
                {
                    throw new InvalidOperationException("You are setting IPAddressEnforcement to true, but the allow list of IPAddresses is empty. Please set it using the IPAddressAllowList parameter");
                }
                Tenant.IPAddressEnforcement = IPAddressEnforcement.Value;
                modified = true;
            }
            if (!string.IsNullOrEmpty(IPAddressAllowList))
            {
                Tenant.IPAddressAllowList = IPAddressAllowList;
                modified = true;
                if ((IPAddressEnforcement == null && !Tenant.IPAddressEnforcement) || IPAddressEnforcement == false)
                {
                    LogWarning("The list of IP Addresses you provided will not be enforced until you set IPAddressEnforcement to true");
                }
            }
            if (IPAddressWACTokenLifetime.HasValue)
            {
                if (!(IPAddressWACTokenLifetime >= 15) || !(IPAddressWACTokenLifetime <= 600))
                {
                    throw new InvalidOperationException("The value must be in the range 15-1440 minutes");
                }
                Tenant.IPAddressWACTokenLifetime = IPAddressWACTokenLifetime.Value;
                modified = true;
            }
            if (UseFindPeopleInPeoplePicker.HasValue)
            {
                Tenant.UseFindPeopleInPeoplePicker = UseFindPeopleInPeoplePicker.Value;
                modified = true;
            }
            if (ShowPeoplePickerSuggestionsForGuestUsers.HasValue)
            {
                Tenant.ShowPeoplePickerSuggestionsForGuestUsers = ShowPeoplePickerSuggestionsForGuestUsers.Value;
                modified = true;
            }
            if (DefaultSharingLinkType.HasValue)
            {
                try
                {
                    Tenant.EnsureProperty(t => t.DefaultSharingLinkType);
                    if (Tenant.DefaultSharingLinkType != DefaultSharingLinkType.Value)
                    {
                        if (DefaultSharingLinkType.Value == SharingLinkType.AnonymousAccess && Tenant.SharingCapability != SharingCapabilities.ExternalUserAndGuestSharing)
                        {
                            LogWarning(@"Anonymous access links aren’t enabled for your organization. You must first enable them by running the command ""Set-PnPTenant -SharingCapability ExternalUserAndGuestSharing"" before you can set the DefaultSharingLinkType parameter to AnonymousAccess. We will not set the value in this case.");
                        }
                        else
                        {
                            Tenant.DefaultSharingLinkType = DefaultSharingLinkType.Value;
                        }
                    }
                    modified = true;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property DefaultSharingLinkType is not supported by your version of the service");
                }
            }
            if (ODBMembersCanShare.HasValue)
            {
                Tenant.ODBMembersCanShare = ODBMembersCanShare.Value;
                modified = true;
            }
            if (ODBAccessRequests.HasValue)
            {
                Tenant.ODBAccessRequests = ODBAccessRequests.Value;
                modified = true;
            }
            if (FileAnonymousLinkType.HasValue)
            {
                try
                {
                    Tenant.EnsureProperty(t => t.FileAnonymousLinkType);
                    if (Tenant.FileAnonymousLinkType != FileAnonymousLinkType.Value)
                    {
                        if (Tenant.SharingCapability != SharingCapabilities.ExternalUserAndGuestSharing)
                        {
                            LogWarning(@"Anonymous access links aren’t enabled for your organization. You must first enable them by running the command ""Set-PnPTenant -SharingCapability ExternalUserAndGuestSharing"" before you can set the FileAnonymousLinkType property. We will not set the value in this case.");
                        }
                        else
                        {
                            Tenant.FileAnonymousLinkType = FileAnonymousLinkType.Value;
                        }
                    }
                    modified = true;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property FileAnonymousLinkType is not supported by your version of the service");
                }
            }
            if (FolderAnonymousLinkType.HasValue)
            {
                try
                {
                    Tenant.EnsureProperty(t => t.FolderAnonymousLinkType);
                    if (Tenant.FolderAnonymousLinkType != FolderAnonymousLinkType.Value)
                    {
                        if (Tenant.SharingCapability != SharingCapabilities.ExternalUserAndGuestSharing)
                        {
                            LogWarning(@"Anonymous access links aren’t enabled for your organization. You must first enable them by running the command ""Set-PnPTenant -SharingCapability ExternalUserAndGuestSharing"" before you can set the FolderAnonymousLinkType property. We will not set the value in this case.");
                        }
                        else
                        {
                            Tenant.FolderAnonymousLinkType = FolderAnonymousLinkType.Value;
                        }
                    }
                    modified = true;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property FolderAnonymousLinkType is not supported by your version of the service");
                }
            }
            if (CommentsOnSitePagesDisabled.HasValue)
            {
                try
                {
                    Tenant.CommentsOnSitePagesDisabled = CommentsOnSitePagesDisabled.Value;
                    modified = true;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property CommentsOnSitePagesDisabled is not supported by your version of the service");
                }
            }
            if (SocialBarOnSitePagesDisabled.HasValue)
            {
                try
                {
                    Tenant.SocialBarOnSitePagesDisabled = SocialBarOnSitePagesDisabled.Value;
                    modified = true;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property SocialBarOnSitePagesDisabled is not supported by your version of the service");
                }
            }
            if (DefaultLinkPermission.HasValue)
            {
                try
                {
                    Tenant.EnsureProperty(t => t.DefaultLinkPermission);
                    if (Tenant.DefaultLinkPermission != DefaultLinkPermission.Value)
                    {
                        Tenant.DefaultLinkPermission = DefaultLinkPermission.Value;
                    }
                    modified = true;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property DefaultLinkPermission is not supported by your version of the service");
                }
            }
            if (ConditionalAccessPolicy.HasValue)
            {
                try
                {
                    Tenant.ConditionalAccessPolicy = ConditionalAccessPolicy.Value;
                    modified = true;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property ConditionalAccessPolicy is not supported by your version of the service");
                }
            }
            if (AllowDownloadingNonWebViewableFiles.HasValue)
            {
                try
                {
                    Tenant.EnsureProperty(t => t.AllowDownloadingNonWebViewableFiles);
                    if (Tenant.ConditionalAccessPolicy == SPOConditionalAccessPolicyType.AllowLimitedAccess)
                    {
                        Tenant.AllowDownloadingNonWebViewableFiles = AllowDownloadingNonWebViewableFiles.Value;
                        modified = true;
                        if (!AllowDownloadingNonWebViewableFiles.Value)
                        {
                            LogWarning("Users will not be able to download files that can't be viewed on the web. To allow download of files that can't be viewed on the web, run the cmdlet again and set AllowDownloadingNonWebViewableFiles to true.");
                        }
                    }
                    else if (Force || ShouldContinue("To set this parameter, you need to set the Set-PnPTenant -ConditionalAccessPolicy to AllowLimitedAccess. Would you like to set it now?", Properties.Resources.Confirm))
                    {
                        Tenant.ConditionalAccessPolicy = SPOConditionalAccessPolicyType.AllowLimitedAccess;
                        Tenant.AllowDownloadingNonWebViewableFiles = AllowDownloadingNonWebViewableFiles.Value;
                        modified = true;
                        if (!AllowDownloadingNonWebViewableFiles.Value)
                        {
                            LogWarning("Users will not be able to download files that can't be viewed on the web. To allow download of files that can't be viewed on the web, run the cmdlet again and set AllowDownloadingNonWebViewableFiles to true.");
                        }
                    }
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property AllowDownloadingNonWebViewableFiles is not supported by your version of the service");
                }
            }
            if (AllowEditing.HasValue)
            {
                try
                {
                    Tenant.EnsureProperty(t => t.ConditionalAccessPolicy);
                    if (Tenant.ConditionalAccessPolicy == SPOConditionalAccessPolicyType.AllowLimitedAccess)
                    {
                        Tenant.AllowEditing = AllowEditing.Value;
                        modified = true;
                    }
                    else if (Force || ShouldContinue("To set this parameter, you need to set the Set-PnPTenant -ConditionalAccessPolicy to AllowLimitedAccess. Would you like to set it now?", Properties.Resources.Confirm))
                    {
                        Tenant.ConditionalAccessPolicy = SPOConditionalAccessPolicyType.AllowLimitedAccess;
                        Tenant.AllowEditing = AllowEditing.Value;
                        modified = true;
                    }
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property AllowEditing is not supported by your version of the service");
                }
            }
            if (ApplyAppEnforcedRestrictionsToAdHocRecipients.HasValue)
            {
                try
                {
                    Tenant.ApplyAppEnforcedRestrictionsToAdHocRecipients = ApplyAppEnforcedRestrictionsToAdHocRecipients.Value;
                    modified = true;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property ApplyAppEnforcedRestrictionsToAdHocRecipients is not supported by your version of the service");
                }
            }
            if (FilePickerExternalImageSearchEnabled.HasValue)
            {
                try
                {
                    Tenant.FilePickerExternalImageSearchEnabled = FilePickerExternalImageSearchEnabled.Value;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property FilePickerExternalImageSearchEnabled is not supported by your version of the service");
                }
                modified = true;
            }
            if (EmailAttestationRequired.HasValue)
            {
                try
                {
                    Tenant.EmailAttestationRequired = EmailAttestationRequired.Value;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property EmailAttestationRequired is not supported by your version of the service");
                }
                modified = true;
            }
            if (EmailAttestationReAuthDays.HasValue)
            {
                try
                {
                    Tenant.EmailAttestationReAuthDays = EmailAttestationReAuthDays.Value;
                }
                catch (PropertyOrFieldNotInitializedException)
                {
                    throw new InvalidOperationException("Setting the property EmailAttestationReAuthDays is not supported by your version of the service");
                }
                modified = true;
            }
            if (HideDefaultThemes.HasValue)
            {
                Tenant.HideDefaultThemes = HideDefaultThemes.Value;
                modified = true;
            }
            if (DisabledWebPartIds != null)
            {
                Tenant.DisabledWebPartIds = DisabledWebPartIds;
                modified = true;
            }
            if (EnableAIPIntegration.HasValue)
            {
                Tenant.EnableAIPIntegration = EnableAIPIntegration.Value;
                modified = true;
            }
            if (DisableCustomAppAuthentication.HasValue)
            {
                Tenant.DisableCustomAppAuthentication = DisableCustomAppAuthentication.Value;
                modified = true;
            }
            if (EnableAutoNewsDigest.HasValue)
            {
                Tenant.EnableAutoNewsDigest = EnableAutoNewsDigest.Value;
                modified = true;
            }
            if (CommentsOnListItemsDisabled.HasValue)
            {
                Tenant.CommentsOnListItemsDisabled = CommentsOnListItemsDisabled.Value;
                modified = true;
            }
            if (CommentsOnFilesDisabled.HasValue)
            {
                Tenant.CommentsOnFilesDisabled = CommentsOnFilesDisabled.Value;
                modified = true;
            }
            if (AllowCommentsTextOnEmailEnabled.HasValue)
            {
                Tenant.AllowCommentsTextOnEmailEnabled = AllowCommentsTextOnEmailEnabled.Value;
                modified = true;
            }
            if (MarkNewFilesSensitiveByDefault.HasValue)
            {
                Tenant.MarkNewFilesSensitiveByDefault = MarkNewFilesSensitiveByDefault.Value;
                modified = true;
            }

            if (StopNew2013Workflows.HasValue)
            {
                Tenant.StopNew2013Workflows = StopNew2013Workflows.Value;
                modified = true;
            }

            if (ViewInFileExplorerEnabled.HasValue)
            {
                Tenant.ViewInFileExplorerEnabled = ViewInFileExplorerEnabled.Value;
                modified = true;
            }

            if (InformationBarriersSuspension.HasValue)
            {
                Tenant.InformationBarriersSuspension = InformationBarriersSuspension.Value;
                modified = true;
            }
            if (AllowFilesWithKeepLabelToBeDeletedSPO.HasValue)
            {
                Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.SetAllowFilesWithKeepLabelToBeDeletedSPO(AdminContext, AllowFilesWithKeepLabelToBeDeletedSPO.Value);
                modified = true;
            }

            if (AllowFilesWithKeepLabelToBeDeletedODB.HasValue)
            {
                Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.SetAllowFilesWithKeepLabelToBeDeletedODB(AdminContext, AllowFilesWithKeepLabelToBeDeletedODB.Value);
                modified = true;
            }

            if (DisableAddToOneDrive.HasValue)
            {
                Tenant.DisableAddToOneDrive = DisableAddToOneDrive.Value;
                modified = true;
            }

            if (IsFluidEnabled.HasValue)
            {
                Tenant.IsFluidEnabled = IsFluidEnabled.Value;
                Tenant.IsCollabMeetingNotesFluidEnabled = IsFluidEnabled.Value;
                Tenant.IsWBFluidEnabled = IsFluidEnabled.Value;
                modified = true;
            }

            if (DisablePersonalListCreation.HasValue)
            {
                Tenant.DisablePersonalListCreation = DisablePersonalListCreation.Value;
                modified = true;
            }

            if (DisabledModernListTemplateIds != null && DisabledModernListTemplateIds.Length > 0)
            {
                Tenant.DisabledModernListTemplateIds = DisabledModernListTemplateIds;
                modified = true;
            }

            if (ExternalUserExpirationRequired.HasValue)
            {
                Tenant.ExternalUserExpirationRequired = ExternalUserExpirationRequired.Value;
                modified = true;
            }

            if (ExternalUserExpireInDays.HasValue)
            {
                Tenant.ExternalUserExpireInDays = ExternalUserExpireInDays.Value;
                modified = true;
            }

            if (DisplayNamesOfFileViewers.HasValue)
            {
                Tenant.DisplayNamesOfFileViewers = DisplayNamesOfFileViewers.Value;
                modified = true;
            }

            if (DisplayNamesOfFileViewersInSpo.HasValue)
            {
                Tenant.DisplayNamesOfFileViewersInSpo = DisplayNamesOfFileViewersInSpo.Value;
                modified = true;
            }

            if (IsLoopEnabled.HasValue)
            {
                Tenant.IsLoopEnabled = IsLoopEnabled.Value;
                modified = true;
            }

            if (OneDriveRequestFilesLinkEnabled.HasValue)
            {
                Tenant.OneDriveRequestFilesLinkEnabled = OneDriveRequestFilesLinkEnabled.Value;
                modified = true;
            }

            if (OneDriveRequestFilesLinkExpirationInDays.HasValue)
            {
                if (OneDriveRequestFilesLinkExpirationInDays.Value < 0 || OneDriveRequestFilesLinkExpirationInDays.Value > 730)
                {
                    throw new PSArgumentException("OneDriveRequestFilesLinkExpirationInDays must have a value between 0 and 730", nameof(OneDriveRequestFilesLinkExpirationInDays));
                }

                Tenant.OneDriveRequestFilesLinkExpirationInDays = OneDriveRequestFilesLinkExpirationInDays.Value;
                modified = true;
            }

            if (EnableRestrictedAccessControl.HasValue)
            {
                Tenant.EnableRestrictedAccessControl = EnableRestrictedAccessControl.Value;
                modified = true;
            }

            if (EnableAzureADB2BIntegration.HasValue)
            {
                Tenant.EnableAzureADB2BIntegration = EnableAzureADB2BIntegration.Value;
                modified = true;
            }

            if (CoreRequestFilesLinkEnabled.HasValue)
            {
                Tenant.CoreRequestFilesLinkEnabled = CoreRequestFilesLinkEnabled.Value;
                modified = true;
            }

            if (CoreRequestFilesLinkExpirationInDays.HasValue)
            {
                if (CoreRequestFilesLinkExpirationInDays.Value < 0 || CoreRequestFilesLinkExpirationInDays > 730)
                {
                    throw new PSArgumentException("CoreRequestFilesLinkExpirationInDays must have a value between 0 and 730", nameof(CoreRequestFilesLinkExpirationInDays));
                }

                Tenant.CoreRequestFilesLinkExpirationInDays = CoreRequestFilesLinkExpirationInDays.Value;
                modified = true;
            }

            if (ReduceTempTokenLifetimeValue.HasValue)
            {
                if (ReduceTempTokenLifetimeValue.Value < 5 || ReduceTempTokenLifetimeValue > 15)
                {
                    throw new PSArgumentException("ReduceTempTokenLifetimeValue must have a value between 5 and 15", nameof(ReduceTempTokenLifetimeValue));
                }

                Tenant.ReduceTempTokenLifetimeValue = ReduceTempTokenLifetimeValue.Value;
                modified = true;
            }

            if (LabelMismatchEmailHelpLink != null)
            {
                Tenant.LabelMismatchEmailHelpLink = LabelMismatchEmailHelpLink;
                modified = true;
            }

            if (ConditionalAccessPolicyErrorHelpLink != null)
            {
                Tenant.ConditionalAccessPolicyErrorHelpLink = ConditionalAccessPolicyErrorHelpLink;
                modified = true;
            }

            if (DisableDocumentLibraryDefaultLabeling.HasValue)
            {
                Tenant.DisableDocumentLibraryDefaultLabeling = DisableDocumentLibraryDefaultLabeling.Value;
                modified = true;
            }

            if (IsEnableAppAuthPopUpEnabled.HasValue)
            {
                Tenant.IsEnableAppAuthPopUpEnabled = IsEnableAppAuthPopUpEnabled.Value;
                modified = true;
            }

            if (OneDriveLoopSharingCapability.HasValue)
            {
                Tenant.OneDriveLoopSharingCapability = OneDriveLoopSharingCapability.Value;
                modified = true;
            }

            if (OneDriveLoopDefaultSharingLinkScope.HasValue)
            {
                Tenant.OneDriveLoopDefaultSharingLinkScope = OneDriveLoopDefaultSharingLinkScope.Value;
                modified = true;
            }

            if (OneDriveLoopDefaultSharingLinkRole.HasValue)
            {
                Tenant.OneDriveLoopDefaultSharingLinkRole = OneDriveLoopDefaultSharingLinkRole.Value;
                modified = true;
            }

            if (CoreLoopSharingCapability.HasValue)
            {
                Tenant.CoreLoopSharingCapability = CoreLoopSharingCapability.Value;
                modified = true;
            }

            if (CoreLoopDefaultSharingLinkScope.HasValue)
            {
                Tenant.CoreLoopDefaultSharingLinkScope = CoreLoopDefaultSharingLinkScope.Value;
                modified = true;
            }

            if (CoreLoopDefaultSharingLinkRole.HasValue)
            {
                Tenant.CoreLoopDefaultSharingLinkRole = CoreLoopDefaultSharingLinkRole.Value;
                modified = true;
            }

            if (DisableVivaConnectionsAnalytics.HasValue)
            {
                Tenant.DisableVivaConnectionsAnalytics = DisableVivaConnectionsAnalytics.Value;
                modified = true;
            }

            if (CoreDefaultLinkToExistingAccess.HasValue)
            {
                Tenant.CoreDefaultLinkToExistingAccess = CoreDefaultLinkToExistingAccess.Value;
                modified = true;
            }

            if (HideSyncButtonOnTeamSite.HasValue)
            {
                Tenant.HideSyncButtonOnDocLib = HideSyncButtonOnTeamSite.Value;
                modified = true;
            }

            if (CoreBlockGuestsAsSiteAdmin.HasValue)
            {
                Tenant.CoreBlockGuestsAsSiteAdmin = CoreBlockGuestsAsSiteAdmin.Value;
                modified = true;
            }

            if (IsWBFluidEnabled.HasValue)
            {
                Tenant.IsWBFluidEnabled = IsWBFluidEnabled.Value;
                modified = true;
            }

            if (IsCollabMeetingNotesFluidEnabled.HasValue)
            {
                Tenant.IsCollabMeetingNotesFluidEnabled = IsCollabMeetingNotesFluidEnabled.Value;
                modified = true;
            }

            if (AllowAnonymousMeetingParticipantsToAccessWhiteboards.HasValue)
            {
                Tenant.AllowAnonymousMeetingParticipantsToAccessWhiteboards = AllowAnonymousMeetingParticipantsToAccessWhiteboards.Value;
                modified = true;
            }

            if (IBImplicitGroupBased.HasValue)
            {
                Tenant.IBImplicitGroupBased = IBImplicitGroupBased.Value;
                modified = true;
            }

            if (ShowOpenInDesktopOptionForSyncedFiles.HasValue)
            {
                Tenant.ShowOpenInDesktopOptionForSyncedFiles = ShowOpenInDesktopOptionForSyncedFiles.Value;
                modified = true;
            }

            if (ShowPeoplePickerGroupSuggestionsForIB.HasValue)
            {
                Tenant.ShowPeoplePickerGroupSuggestionsForIB = ShowPeoplePickerGroupSuggestionsForIB.Value;
                modified = true;
            }

            if (ArchiveRedirectUrl != null)
            {
                Tenant.ArchiveRedirectUrl = ArchiveRedirectUrl;
                modified = true;
            }

            if (MediaTranscription.HasValue)
            {
                Tenant.MediaTranscription = MediaTranscription.Value;
                modified = true;
            }

            if (MediaTranscriptionAutomaticFeatures.HasValue)
            {
                Tenant.MediaTranscriptionAutomaticFeatures = MediaTranscriptionAutomaticFeatures.Value;
                modified = true;
            }

            if (BlockSendLabelMismatchEmail.HasValue)
            {
                Tenant.BlockSendLabelMismatchEmail = BlockSendLabelMismatchEmail.Value;
                modified = true;
            }

            if (SiteOwnerManageLegacyServicePrincipalEnabled.HasValue)
            {
                Tenant.SiteOwnerManageLegacyServicePrincipalEnabled = SiteOwnerManageLegacyServicePrincipalEnabled.Value;
                modified = true;
            }

            if (ReduceTempTokenLifetimeEnabled.HasValue)
            {
                Tenant.ReduceTempTokenLifetimeEnabled = ReduceTempTokenLifetimeEnabled.Value;
                modified = true;
            }

            if (ViewersCanCommentOnMediaDisabled.HasValue)
            {
                Tenant.ViewersCanCommentOnMediaDisabled = ViewersCanCommentOnMediaDisabled.Value;
                modified = true;
            }

            if (AllowGuestUserShareToUsersNotInSiteCollection.HasValue)
            {
                Tenant.AllowGuestUserShareToUsersNotInSiteCollection = AllowGuestUserShareToUsersNotInSiteCollection.Value;
                modified = true;
            }

            if (IncludeAtAGlanceInShareEmails.HasValue)
            {
                Tenant.IncludeAtAGlanceInShareEmails = IncludeAtAGlanceInShareEmails.Value;
                modified = true;
            }

            if (MassDeleteNotificationDisabled.HasValue)
            {
                Tenant.MassDeleteNotificationDisabled = MassDeleteNotificationDisabled.Value;
                modified = true;
            }

            if (BusinessConnectivityServiceDisabled.HasValue)
            {
                Tenant.BusinessConnectivityServiceDisabled = BusinessConnectivityServiceDisabled.Value;
                modified = true;
            }

            if (EnableSensitivityLabelForPDF.HasValue)
            {
                Tenant.EnableSensitivityLabelForPDF = EnableSensitivityLabelForPDF.Value;
                modified = true;
            }

            if (IsDataAccessInCardDesignerEnabled.HasValue)
            {
                Tenant.IsDataAccessInCardDesignerEnabled = IsDataAccessInCardDesignerEnabled.Value;
                modified = true;
            }

            if (AppBypassInformationBarriers.HasValue)
            {
                Tenant.AppBypassInformationBarriers = AppBypassInformationBarriers.Value;
                modified = true;
            }

            if (DefaultOneDriveInformationBarrierMode.HasValue)
            {
                Tenant.DefaultODBMode = DefaultOneDriveInformationBarrierMode.Value.ToString();
                modified = true;
            }

            if (CoreSharingCapability.HasValue)
            {
                Tenant.CoreSharingCapability = CoreSharingCapability.Value;
                modified = true;
            }

            if (BlockUserInfoVisibilityInOneDrive.HasValue)
            {
                Tenant.BlockUserInfoVisibilityInOneDrive = BlockUserInfoVisibilityInOneDrive.Value;
                modified = true;
            }
            if (BlockUserInfoVisibilityInSharePoint.HasValue)
            {
                Tenant.BlockUserInfoVisibilityInSharePoint = BlockUserInfoVisibilityInSharePoint.Value;
                modified = true;
            }
            if (AllowEveryoneExceptExternalUsersClaimInPrivateSite.HasValue)
            {
                Tenant.AllowEveryoneExceptExternalUsersClaimInPrivateSite = AllowEveryoneExceptExternalUsersClaimInPrivateSite.Value;
                modified = true;
            }
            if (AIBuilderEnabled.HasValue)
            {
                Tenant.AIBuilderEnabled = AIBuilderEnabled.Value;
                modified = true;
            }
            if (AllowSensitivityLabelOnRecords.HasValue)
            {
                Tenant.AllowSensitivityLabelOnRecords = AllowSensitivityLabelOnRecords.Value;
                modified = true;
            }
            if (AnyoneLinkTrackUsers.HasValue)
            {
                Tenant.AnyoneLinkTrackUsers = AnyoneLinkTrackUsers.Value;
                modified = true;
            }
            if (EnableSiteArchive.HasValue)
            {
                Tenant.EnableSiteArchive = EnableSiteArchive.Value;
                modified = true;
            }
            if (ESignatureEnabled.HasValue)
            {
                Tenant.ESignatureEnabled = ESignatureEnabled.Value;
                modified = true;
            }
            if (AllowOverrideForBlockUserInfoVisibility.HasValue)
            {
                Tenant.AllowOverrideForBlockUserInfoVisibility = AllowOverrideForBlockUserInfoVisibility.Value;
                modified = true;
            }
            if (OneDriveDefaultShareLinkScope.HasValue)
            {
                Tenant.OneDriveDefaultShareLinkScope = OneDriveDefaultShareLinkScope.Value;
                modified = true;
            }
            if (OneDriveDefaultShareLinkRole.HasValue)
            {
                Tenant.OneDriveDefaultShareLinkRole = OneDriveDefaultShareLinkRole.Value;
                modified = true;
            }
            if (OneDriveDefaultLinkToExistingAccess.HasValue)
            {
                Tenant.OneDriveDefaultLinkToExistingAccess = OneDriveDefaultLinkToExistingAccess.Value;
                modified = true;
            }
            if (OneDriveBlockGuestsAsSiteAdmin.HasValue)
            {
                Tenant.OneDriveBlockGuestsAsSiteAdmin = OneDriveBlockGuestsAsSiteAdmin.Value;
                modified = true;
            }
            if (RecycleBinRetentionPeriod.HasValue)
            {
                Tenant.RecycleBinRetentionPeriod = RecycleBinRetentionPeriod.Value;
                modified = true;
            }
            if (IsSharePointAddInsDisabled.HasValue)
            {
                Tenant.SharePointAddInsDisabled = IsSharePointAddInsDisabled.Value;
                modified = true;
            }
            if (CoreDefaultShareLinkScope.HasValue)
            {
                Tenant.CoreDefaultShareLinkScope = CoreDefaultShareLinkScope.Value;
                modified = true;
            }
            if (CoreDefaultShareLinkRole.HasValue)
            {
                Tenant.CoreDefaultShareLinkRole = CoreDefaultShareLinkRole.Value;
                modified = true;
            }
            if (OneDriveSharingCapability.HasValue)
            {
                Tenant.ODBSharingCapability = OneDriveSharingCapability.Value;
                modified = true;
            }
            if (AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled.HasValue)
            {
                Tenant.AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled = AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled.Value;
                modified = true;
            }
            if (SelfServiceSiteCreationDisabled.HasValue)
            {
                Tenant.SelfServiceSiteCreationDisabled = SelfServiceSiteCreationDisabled.Value;
                modified = true;
            }
            if (SyncAadB2BManagementPolicy)
            {
                Tenant.SyncAadB2BManagementPolicy();
                modified = true;
            }
            if (ExtendPermissionsToUnprotectedFiles.HasValue)
            {
                Tenant.ExtendPermissionsToUnprotectedFiles = ExtendPermissionsToUnprotectedFiles.Value;
                modified = true;
            }
            if (WhoCanShareAllowListInTenant != null)
            {
                Tenant.WhoCanShareAllowListInTenant = WhoCanShareAllowListInTenant;
                modified = true;
            }
            if (LegacyBrowserAuthProtocolsEnabled.HasValue)
            {
                Tenant.LegacyBrowserAuthProtocolsEnabled = LegacyBrowserAuthProtocolsEnabled.Value;
                modified = true;
            }
            if (EnableDiscoverableByOrganizationForVideos.HasValue)
            {
                Tenant.EnableDiscoverableByOrganizationForVideos = EnableDiscoverableByOrganizationForVideos.Value;
                modified = true;
            }
            if (RestrictedAccessControlforSitesErrorHelpLink != null)
            {
                Tenant.RestrictedAccessControlforSitesErrorHelpLink = RestrictedAccessControlforSitesErrorHelpLink;
                modified = true;
            }
            if (Workflow2010Disabled.HasValue)
            {
                Tenant.Workflow2010Disabled = Workflow2010Disabled.Value;
                modified = true;
            }
            if (AllowSharingOutsideRestrictedAccessControlGroups.HasValue)
            {
                Tenant.AllowSharingOutsideRestrictedAccessControlGroups = AllowSharingOutsideRestrictedAccessControlGroups.Value;
                modified = true;
            }
            if (HideSyncButtonOnDocLib.HasValue)
            {
                Tenant.HideSyncButtonOnDocLib = HideSyncButtonOnDocLib.Value;
                modified = true;
            }
            if (HideSyncButtonOnODB.HasValue)
            {
                Tenant.HideSyncButtonOnODB = HideSyncButtonOnODB.Value;
                modified = true;
            }
            if (StreamLaunchConfig.HasValue)
            {
                Tenant.StreamLaunchConfig = StreamLaunchConfig.Value;
                modified = true;
            }
            if (EnableMediaReactions.HasValue)
            {
                Tenant.EnableMediaReactions = EnableMediaReactions.Value;
                modified = true;
            }
            if (ContentSecurityPolicyEnforcement.HasValue)
            {
                Tenant.ContentSecurityPolicyEnforcement = ContentSecurityPolicyEnforcement.Value;
                modified = true;
            }
            if (DisableSpacesActivation.HasValue)
            {
                Tenant.DisableSpacesActivation = DisableSpacesActivation.Value;
                modified = true;
            }
            if (AllowClassicPublishingSiteCreation.HasValue)
            {
                Tenant.AllowClassicPublishingSiteCreation = AllowClassicPublishingSiteCreation.Value;
                modified = true;
            }
            if (DelayDenyAddAndCustomizePagesEnforcementOnClassicPublishingSites.HasValue)
            {
                Tenant.DelayDenyAddAndCustomizePagesEnforcementOnClassicPublishingSites = DelayDenyAddAndCustomizePagesEnforcementOnClassicPublishingSites.Value;
                modified = true;
            }
            if (GuestSharingGroupAllowListInTenantByPrincipalIdentity != null)
            {
                if (GuestSharingGroupAllowListInTenantByPrincipalIdentity.Length > 0)
                {
                    if (!string.IsNullOrEmpty(GuestSharingGroupAllowListInTenantByPrincipalIdentity[0].ToString()))
                    {
                        Tenant.GuestSharingGroupAllowListInTenantByPrincipalIdentity = GuestSharingGroupAllowListInTenantByPrincipalIdentity;
                    }
                    else
                    {
                        Tenant.GuestSharingGroupAllowListInTenantByPrincipalIdentity = new string[0];
                    }
                }
                modified = true;
            }
            if (BlockDownloadFileTypePolicy.HasValue)
            {
                if (!BlockDownloadFileTypePolicy.Value)
                {
                    Tenant.SetBlockDownloadFileTypePolicyData(BlockDownloadFileTypePolicy.Value, new SPBlockDownloadFileTypeId[0], new Guid[0]);
                    modified = true;
                }
                else
                {
                    if (BlockDownloadFileTypeIds == null || BlockDownloadFileTypeIds.Length == 0)
                    {
                        throw new InvalidOperationException("Please specify the File Type Ids that you want to block for download.");
                    }
                    if (BlockDownloadFileTypeIds.Contains(SPBlockDownloadFileTypeId.TeamsMeetingRecording))
                    {
                        LogWarning("Please note that this policy only prevents download of Teams Meeting Recording files saved in SharePoint Online by the Teams service. Only new meeting recordings saved after this policy is set will be impacted.");
                    }
                    BlockDownloadFileTypeIds = BlockDownloadFileTypeIds.Distinct().ToArray();
                    if (ExcludedBlockDownloadGroupIds != null && ExcludedBlockDownloadGroupIds.Length != 0)
                    {
                        if (ExcludedBlockDownloadGroupIds.Length > 10)
                        {
                            throw new InvalidOperationException("You can only specify 10 IDs in the Block Download File Type Policy Invalid Exclusion List");
                        }
                        Tenant.SetBlockDownloadFileTypePolicyData(BlockDownloadFileTypePolicy.Value, BlockDownloadFileTypeIds, ExcludedBlockDownloadGroupIds);
                    }
                    else
                    {
                        Tenant.SetBlockDownloadFileTypePolicyData(BlockDownloadFileTypePolicy.Value, BlockDownloadFileTypeIds, new Guid[0]);
                    }
                    modified = true;
                }
            }
            else if (ExcludedBlockDownloadGroupIds != null)
            {
                if (ExcludedBlockDownloadGroupIds.Length > 10)
                {
                    throw new InvalidOperationException("You can only specify 10 IDs in the Block Download File Type Policy Invalid Exclusion List");
                }
                Tenant.SetBlockDownloadFileTypePolicyExclusionList(ExcludedBlockDownloadGroupIds);
                modified = true;
            }
            if (modified)
            {
                AdminContext.ExecuteQueryRetry();
            }

            if (EnableModernListTemplateIds != null && EnableModernListTemplateIds.Length > 0)
            {
                Tenant.EnsureProperty(t => t.DisabledModernListTemplateIds);
                List<Guid> disabledListTemplateIds = new List<Guid>(Tenant.DisabledModernListTemplateIds);
                Guid[] disableModernListTemplateIds = EnableModernListTemplateIds;
                foreach (Guid templateId in disableModernListTemplateIds)
                {
                    if (templateId != Guid.Empty)
                    {
                        disabledListTemplateIds.Remove(templateId);
                    }
                }

                Tenant.DisabledModernListTemplateIds = disabledListTemplateIds.ToArray();
                AdminContext.ExecuteQueryRetry();
            }

            modified = false;
            if (ExpireVersionsAfterDays.HasValue || MajorVersionLimit.HasValue || EnableAutoExpirationVersionTrim.HasValue)
            {
                Tenant.EnsureProperties(t => t.ExpireVersionsAfterDays, t => t.MajorVersionLimit, t => t.EnableAutoExpirationVersionTrim);
            }
            if (ExpireVersionsAfterDays.HasValue)
            {
                if (EnableAutoExpirationVersionTrim.HasValue)
                {
                    if (EnableAutoExpirationVersionTrim.Value)
                    {
                        throw new PSArgumentException($"The parameter {nameof(ExpireVersionsAfterDays)} can't be set when AutoExpiration is enabled");
                    }
                }
                else if (Tenant.EnableAutoExpirationVersionTrim)
                {
                    throw new PSArgumentException($"The parameter {nameof(ExpireVersionsAfterDays)} can't be set because the Tenant has AutoExpiration enabled");
                }
                if (ExpireVersionsAfterDays.Value != 0 && ExpireVersionsAfterDays.Value < 30)
                {
                    throw new PSArgumentException($"The parameter {nameof(ExpireVersionsAfterDays)} can't be set because the value needs to greater than 30");
                }
                if (ExpireVersionsAfterDays.Value > 36500)
                {
                    throw new PSArgumentException($"The parameter {nameof(ExpireVersionsAfterDays)} can't be set because the value needs to less than 36500");
                }
                modified = true;
            }
            if (MajorVersionLimit.HasValue)
            {
                if (EnableAutoExpirationVersionTrim.HasValue)
                {
                    if (EnableAutoExpirationVersionTrim.Value)
                    {
                        throw new PSArgumentException($"The parameter {nameof(MajorVersionLimit)} can't be set when AutoExpiration is enabled", nameof(MajorVersionLimit));
                    }
                }
                else if (Tenant.EnableAutoExpirationVersionTrim)
                {
                    throw new PSArgumentException($"The parameter {nameof(MajorVersionLimit)} can't be set when AutoExpiration is enabled", nameof(MajorVersionLimit));
                }
                if (MajorVersionLimit.Value < 1)
                {
                    throw new PSArgumentException($"You must specify a value for {nameof(MajorVersionLimit)} greater than 1", nameof(MajorVersionLimit));
                }
                if (MajorVersionLimit.Value > 50000)
                {
                    throw new PSArgumentException($"You must specify a value for {nameof(MajorVersionLimit)} less than 50000", nameof(MajorVersionLimit));
                }
                modified = true;
            }
            if (EnableAutoExpirationVersionTrim.HasValue)
            {
                if (!EnableAutoExpirationVersionTrim.Value && (!ExpireVersionsAfterDays.HasValue || !MajorVersionLimit.HasValue))
                {
                    throw new PSArgumentException($"The parameter {nameof(ExpireVersionsAfterDays)} and {nameof(MajorVersionLimit)} need to be specified", nameof(MajorVersionLimit));
                }
                modified = true;
            }
            if (modified)
            {
                bool isAutoTrimEnabled = (EnableAutoExpirationVersionTrim.HasValue ? EnableAutoExpirationVersionTrim.Value : Tenant.EnableAutoExpirationVersionTrim);
                Tenant.SetFileVersionPolicy(isAutoTrimEnabled, MajorVersionLimit ?? (-1), ExpireVersionsAfterDays ?? (-1));
                AdminContext.ExecuteQueryRetry();
            }
        }
    }
}