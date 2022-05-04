﻿using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System;
using Microsoft.Online.SharePoint.TenantManagement;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPTenant", DefaultParameterSetName = ParameterAttribute.AllParameterSets)]

    public class SetTenant : PnPAdminCmdlet
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
        public bool? UserVoiceForFeedbackEnabled;

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
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(Tenant);
            ClientContext.ExecuteQueryRetry();

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
            if (SignInAccelerationDomain != null && (Force || ShouldContinue($@"Please confirm that ""{SignInAccelerationDomain}"" is correct, and you have federated sign-in configured for that domain. Otherwise, your users will no longer be able to sign in. Do you want to continue?", "Confirm")))
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
                if (Force || ShouldContinue("Make sure that your federated sign-in supports guest users. If it doesn’t, your guest users will no longer be able to sign in after you set EnableGuestSignInAcceleration to $true.", "Confirm"))
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
            if (BccExternalSharingInvitations.HasValue && (!BccExternalSharingInvitations.Value || (BccExternalSharingInvitations.Value && (Force || ShouldContinue("The recipients listed in BccExternalSharingInvitationsList will be blind copied on all external sharing invitations. Do you want to continue?", "Confirm")))))
            {
                Tenant.BccExternalSharingInvitations = BccExternalSharingInvitations.Value;
                modified = true;
            }
            if (!string.IsNullOrEmpty(BccExternalSharingInvitationsList))
            {
                Tenant.BccExternalSharingInvitationsList = BccExternalSharingInvitationsList;
                modified = true;
            }
            if (UserVoiceForFeedbackEnabled.HasValue)
            {
                Tenant.UserVoiceForFeedbackEnabled = UserVoiceForFeedbackEnabled.Value;
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
                        WriteWarning("Warning: anonymous links are not enabled on your tenant. Enable them with SharingCapability.");
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
                    WriteWarning("We automatically enabled RequireAcceptingAccountMatchInvitedAccount because you selected to limit external sharing using domains.");
                    Tenant.RequireAcceptingAccountMatchInvitedAccount = true;
                }
                Tenant.SharingAllowedDomainList = SharingAllowedDomainList;
                modified = true;
                if ((SharingDomainRestrictionMode == null && Tenant.SharingDomainRestrictionMode != SharingDomainRestrictionModes.AllowList) || SharingDomainRestrictionMode == SharingDomainRestrictionModes.None)
                {
                    WriteWarning("You must set SharingDomainRestrictionMode to AllowList in order to have the list of domains you configured for SharingAllowedDomainList to take effect.");
                }
                else if (SharingDomainRestrictionMode == SharingDomainRestrictionModes.BlockList)
                {
                    WriteWarning("The list of domains in SharingAllowedDomainsList is ignored when you set the SharingDomainRestrictionMode to BlockList. Set the list of blocked domains using the SharingBlockedDomainsList parameter.");
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
                if (Force || ShouldContinue("This will update the Retention Policy for All Orphaned OneDrive for Business sites.", "Confirm"))
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
                    WriteWarning("We automatically enabled RequireAcceptingAccountMatchInvitedAccount because you selected to limit external sharing using domains.");
                    Tenant.RequireAcceptingAccountMatchInvitedAccount = true;
                }
                Tenant.SharingBlockedDomainList = SharingBlockedDomainList;
                modified = true;
                if ((SharingDomainRestrictionMode == null && Tenant.SharingDomainRestrictionMode != SharingDomainRestrictionModes.BlockList) || SharingDomainRestrictionMode == SharingDomainRestrictionModes.None)
                {
                    WriteWarning("You must set SharingDomainRestrictionMode to BlockList in order to have the list of domains you configured for SharingBlockedDomainList to take effect");
                }
                else if (SharingDomainRestrictionMode == SharingDomainRestrictionModes.AllowList)
                {
                    WriteWarning("The list of domains in SharingBlockedDomainsList is ignored when you set the SharingDomainRestrictionMode to AllowList.Set the list of allowed domains using the SharingAllowedDomainsList parameter.");
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
                    WriteWarning("We automatically enabled RequireAcceptingAccountMatchInvitedAccount because you selected to limit external sharing using domains.");
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
                if (Force || ShouldContinue(message, "Confirm"))
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
                    WriteWarning("The list of IP Addresses you provided will not be enforced until you set IPAddressEnforcement to true");
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
                            WriteWarning(@"Anonymous access links aren’t enabled for your organization. You must first enable them by running the command ""Set-PnPTenant -SharingCapability ExternalUserAndGuestSharing"" before you can set the DefaultSharingLinkType parameter to AnonymousAccess. We will not set the value in this case.");
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
                            WriteWarning(@"Anonymous access links aren’t enabled for your organization. You must first enable them by running the command ""Set-PnPTenant -SharingCapability ExternalUserAndGuestSharing"" before you can set the FileAnonymousLinkType property. We will not set the value in this case.");
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
                            WriteWarning(@"Anonymous access links aren’t enabled for your organization. You must first enable them by running the command ""Set-PnPTenant -SharingCapability ExternalUserAndGuestSharing"" before you can set the FolderAnonymousLinkType property. We will not set the value in this case.");
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
                            WriteWarning("Users will not be able to download files that can't be viewed on the web. To allow download of files that can't be viewed on the web, run the cmdlet again and set AllowDownloadingNonWebViewableFiles to true.");
                        }
                    }
                    else if (Force || ShouldContinue("To set this parameter, you need to set the Set-PnPTenant -ConditionalAccessPolicy to AllowLimitedAccess. Would you like to set it now?", "Confirm"))
                    {
                        Tenant.ConditionalAccessPolicy = SPOConditionalAccessPolicyType.AllowLimitedAccess;
                        Tenant.AllowDownloadingNonWebViewableFiles = AllowDownloadingNonWebViewableFiles.Value;
                        modified = true;
                        if (!AllowDownloadingNonWebViewableFiles.Value)
                        {
                            WriteWarning("Users will not be able to download files that can't be viewed on the web. To allow download of files that can't be viewed on the web, run the cmdlet again and set AllowDownloadingNonWebViewableFiles to true.");
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
                    else if (Force || ShouldContinue("To set this parameter, you need to set the Set-PnPTenant -ConditionalAccessPolicy to AllowLimitedAccess. Would you like to set it now?", "Confirm"))
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
                Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.SetAllowFilesWithKeepLabelToBeDeletedSPO(ClientContext, AllowFilesWithKeepLabelToBeDeletedSPO.Value);
                modified = true;
            }

            if (AllowFilesWithKeepLabelToBeDeletedODB.HasValue)
            {
                Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.SetAllowFilesWithKeepLabelToBeDeletedODB(ClientContext, AllowFilesWithKeepLabelToBeDeletedODB.Value);
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

            if (modified)
            {
                ClientContext.ExecuteQueryRetry();
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
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}