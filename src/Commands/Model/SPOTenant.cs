using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Model
{
    public class SPOTenant
    {
        #region Properties

        public bool HideDefaultThemes { private set; get; }

        public long StorageQuota { private set; get; }

        public long StorageQuotaAllocated { private set; get; }

        public double ResourceQuota { private set; get; }

        public double ResourceQuotaAllocated { private set; get; }

        public double OneDriveStorageQuota { private set; get; }

        public string CompatibilityRange { private set; get; }

        public bool ExternalServicesEnabled { private set; get; }
        public string NoAccessRedirectUrl { private set; get; }

        public SharingCapabilities SharingCapability { private set; get; }

        public bool DisplayStartASiteOption { private set; get; }

        public string StartASiteFormUrl { private set; get; }

        public bool ShowEveryoneClaim { private set; get; }

        public bool ShowAllUsersClaim { private set; get; }

        public bool OfficeClientADALDisabled { private set; get; }

        public bool LegacyAuthProtocolsEnabled { private set; get; }

        public bool ShowEveryoneExceptExternalUsersClaim { private set; get; }

        public bool SearchResolveExactEmailOrUPN { private set; get; }

        public bool RequireAcceptingAccountMatchInvitedAccount { private set; get; }
        public bool ProvisionSharedWithEveryoneFolder { private set; get; }

        public string SignInAccelerationDomain { private set; get; }
        public bool EnableGuestSignInAcceleration { private set; get; }

        public bool UsePersistentCookiesForExplorerView { private set; get; }

        public bool BccExternalSharingInvitations { private set; get; }

        public string BccExternalSharingInvitationsList { private set; get; }

        public bool UserVoiceForFeedbackEnabled { private set; get; }

        public bool PublicCdnEnabled { private set; get; }

        public string PublicCdnAllowedFileTypes { private set; get; }

        public IList<SPOPublicCdnOrigin> PublicCdnOrigins { private set; get; }

        public int RequireAnonymousLinksExpireInDays { private set; get; }

        public string SharingAllowedDomainList { private set; get; }

        public string SharingBlockedDomainList { private set; get; }

        public SharingDomainRestrictionModes SharingDomainRestrictionMode { private set; get; }

        public bool OneDriveForGuestsEnabled { private set; get; }

        public bool IPAddressEnforcement { private set; get; }

        public string IPAddressAllowList { private set; get; }

        public int IPAddressWACTokenLifetime { private set; get; }

        public bool UseFindPeopleInPeoplePicker { private set; get; }

        public SharingLinkType DefaultSharingLinkType { private set; get; }

        public SharingState ODBMembersCanShare { private set; get; }

        public SharingState ODBAccessRequests { private set; get; }

        public bool PreventExternalUsersFromResharing { private set; get; }

        public bool ShowPeoplePickerSuggestionsForGuestUsers { private set; get; }

        public AnonymousLinkType FileAnonymousLinkType { private set; get; }

        public AnonymousLinkType FolderAnonymousLinkType { private set; get; }

        public bool NotifyOwnersWhenItemsReshared { private set; get; }

        public bool NotifyOwnersWhenInvitationsAccepted { private set; get; }

        public bool NotificationsInOneDriveForBusinessEnabled { private set; get; }

        public bool NotificationsInSharePointEnabled { private set; get; }

        public SpecialCharactersState SpecialCharactersStateInFileFolderNames { private set; get; }

        public bool OwnerAnonymousNotification { private set; get; }

        public bool CommentsOnSitePagesDisabled { private set; get; }

        public bool SocialBarOnSitePagesDisabled { private set; get; }

        public int OrphanedPersonalSitesRetentionPeriod { private set; get; }

        public bool PermissiveBrowserFileHandlingOverride { private set; get; }

        public bool DisallowInfectedFileDownload { private set; get; }

        public SharingPermissionType DefaultLinkPermission { private set; get; }

        public SPOConditionalAccessPolicyType ConditionalAccessPolicy { private set; get; }

        public bool AllowDownloadingNonWebViewableFiles { private set; get; }

        public bool AllowEditing { private set; get; }

        public bool ApplyAppEnforcedRestrictionsToAdHocRecipients { private set; get; }

        public bool FilePickerExternalImageSearchEnabled { private set; get; }

        public bool EmailAttestationRequired { private set; get; }

        public int EmailAttestationReAuthDays { private set; get; }

        public Guid[] DisabledWebPartIds { private set; get; }

        public bool DisableCustomAppAuthentication { private set; get; }

        public SensitiveByDefaultState MarkNewFilesSensitiveByDefault { private set; get; }

        public bool StopNew2013Workflows { private set; get; }

        public bool ViewInFileExplorerEnabled { private set; get; }

        public bool DisableSpacesActivation { private set; get; }

        public bool? AllowFilesWithKeepLabelToBeDeletedSPO { private set; get; }

        public bool? AllowFilesWithKeepLabelToBeDeletedODB { private set; get; }

        public bool DisableAddToOneDrive { private set; get; }

        public bool IsFluidEnabled { private set; get; }
        public bool DisablePersonalListCreation { private set; get; }

        public bool ExternalUserExpirationRequired { private set; get; }

        public int ExternalUserExpireInDays { private set; get; }

        public bool DisplayNamesOfFileViewers { private set; get; }

        public bool DisplayNamesOfFileViewersInSpo { private set; get; }
        public bool IsLoopEnabled { private set; get; }
        public Guid[] DisabledModernListTemplateIds { private set; get; }
        public bool RestrictedAccessControl { private set; get; }
        public bool DisableDocumentLibraryDefaultLabeling { private set; get; }
        public bool IsEnableAppAuthPopUpEnabled { private set; get; }
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
        public bool? ShowOpenInDesktopOptionForSyncedFiles { private set; get; }

        #endregion

        public SPOTenant(Tenant tenant, ClientContext clientContext)
        {
            HideDefaultThemes = tenant.HideDefaultThemes;
            StorageQuota = tenant.StorageQuota;
            StorageQuotaAllocated = tenant.StorageQuotaAllocated;
            ResourceQuota = tenant.ResourceQuota;
            ResourceQuotaAllocated = tenant.ResourceQuotaAllocated;
            OneDriveStorageQuota = tenant.OneDriveStorageQuota;
            CompatibilityRange = tenant.CompatibilityRange;
            ExternalServicesEnabled = tenant.ExternalServicesEnabled;
            NoAccessRedirectUrl = tenant.NoAccessRedirectUrl;
            SharingCapability = tenant.SharingCapability;
            DisplayStartASiteOption = tenant.DisplayStartASiteOption;
            StartASiteFormUrl = tenant.StartASiteFormUrl;
            ShowEveryoneClaim = tenant.ShowEveryoneClaim;
            ShowAllUsersClaim = tenant.ShowAllUsersClaim;
            OfficeClientADALDisabled = tenant.OfficeClientADALDisabled;
            OrphanedPersonalSitesRetentionPeriod = tenant.OrphanedPersonalSitesRetentionPeriod;
            LegacyAuthProtocolsEnabled = tenant.LegacyAuthProtocolsEnabled;
            ShowEveryoneExceptExternalUsersClaim = tenant.ShowEveryoneExceptExternalUsersClaim;
            SearchResolveExactEmailOrUPN = tenant.SearchResolveExactEmailOrUPN;
            RequireAcceptingAccountMatchInvitedAccount = tenant.RequireAcceptingAccountMatchInvitedAccount;
            ProvisionSharedWithEveryoneFolder = tenant.ProvisionSharedWithEveryoneFolder;
            SignInAccelerationDomain = tenant.SignInAccelerationDomain;
            DisabledWebPartIds = tenant.DisabledWebPartIds;
            StopNew2013Workflows = tenant.StopNew2013Workflows;
            ViewInFileExplorerEnabled = tenant.ViewInFileExplorerEnabled;
            ExternalUserExpirationRequired = tenant.ExternalUserExpirationRequired;
            ExternalUserExpireInDays = tenant.ExternalUserExpireInDays;
            DisplayNamesOfFileViewers = tenant.DisplayNamesOfFileViewers;
            DisplayNamesOfFileViewersInSpo = tenant.DisplayNamesOfFileViewersInSpo;
            IsLoopEnabled = tenant.IsLoopEnabled;
            EnableAzureADB2BIntegration = tenant.EnableAzureADB2BIntegration;
            SiteOwnerManageLegacyServicePrincipalEnabled = tenant.SiteOwnerManageLegacyServicePrincipalEnabled;
            ShowOpenInDesktopOptionForSyncedFiles = tenant.ShowOpenInDesktopOptionForSyncedFiles;

            try
            {
                EnableGuestSignInAcceleration = tenant.EnableGuestSignInAcceleration;
            }
            catch
            {
                EnableGuestSignInAcceleration = false;
            }
            UsePersistentCookiesForExplorerView = tenant.UsePersistentCookiesForExplorerView;
            BccExternalSharingInvitations = tenant.BccExternalSharingInvitations;
            BccExternalSharingInvitationsList = tenant.BccExternalSharingInvitationsList;
            try
            {
                UseFindPeopleInPeoplePicker = tenant.UseFindPeopleInPeoplePicker;
            }
            catch
            {
                UseFindPeopleInPeoplePicker = false;
            }
            try
            {
                UserVoiceForFeedbackEnabled = tenant.UserVoiceForFeedbackEnabled;
            }
            catch
            {
                UserVoiceForFeedbackEnabled = true;
            }
            try
            {
                RequireAnonymousLinksExpireInDays = tenant.RequireAnonymousLinksExpireInDays;
            }
            catch
            {
                RequireAnonymousLinksExpireInDays = 0;
            }
            SharingAllowedDomainList = tenant.SharingAllowedDomainList;
            SharingBlockedDomainList = tenant.SharingBlockedDomainList;
            SharingDomainRestrictionMode = tenant.SharingDomainRestrictionMode;
            try
            {
                OneDriveStorageQuota = tenant.OneDriveStorageQuota;
            }
            catch
            {
                OneDriveStorageQuota = 0L;
            }
            OneDriveForGuestsEnabled = tenant.OneDriveForGuestsEnabled;
            try
            {
                IPAddressEnforcement = tenant.IPAddressEnforcement;
            }
            catch
            {
                IPAddressEnforcement = false;
            }
            try
            {
                IPAddressAllowList = tenant.IPAddressAllowList;
            }
            catch
            {
                IPAddressAllowList = "";
            }
            try
            {
                IPAddressWACTokenLifetime = tenant.IPAddressWACTokenLifetime;
            }
            catch
            {
                IPAddressWACTokenLifetime = 600;
            }
            try
            {
                DefaultSharingLinkType = tenant.DefaultSharingLinkType;
            }
            catch
            {
                DefaultSharingLinkType = SharingLinkType.None;
            }
            try
            {
                ShowPeoplePickerSuggestionsForGuestUsers = tenant.ShowPeoplePickerSuggestionsForGuestUsers;
            }
            catch
            {
                ShowPeoplePickerSuggestionsForGuestUsers = false;
            }
            try
            {
                ODBMembersCanShare = tenant.ODBMembersCanShare;
            }
            catch
            {
                ODBMembersCanShare = SharingState.Unspecified;
            }
            try
            {
                ODBAccessRequests = tenant.ODBAccessRequests;
            }
            catch
            {
                ODBAccessRequests = SharingState.Unspecified;
            }
            try
            {
                PreventExternalUsersFromResharing = tenant.PreventExternalUsersFromResharing;
            }
            catch
            {
                PreventExternalUsersFromResharing = false;
            }
            try
            {
                PublicCdnEnabled = tenant.PublicCdnEnabled;
            }
            catch
            {
                PublicCdnEnabled = false;
            }
            try
            {
                PublicCdnAllowedFileTypes = tenant.PublicCdnAllowedFileTypes;
            }
            catch
            {
                PublicCdnAllowedFileTypes = string.Empty;
            }
            try
            {
                NotifyOwnersWhenItemsReshared = tenant.NotifyOwnersWhenItemsReshared;
            }
            catch
            {
                NotifyOwnersWhenItemsReshared = true;
            }
            try
            {
                NotifyOwnersWhenInvitationsAccepted = tenant.NotifyOwnersWhenInvitationsAccepted;
            }
            catch
            {
                NotifyOwnersWhenInvitationsAccepted = true;
            }
            try
            {
                NotificationsInOneDriveForBusinessEnabled = tenant.NotificationsInOneDriveForBusinessEnabled;
            }
            catch
            {
                NotificationsInOneDriveForBusinessEnabled = true;
            }
            try
            {
                NotificationsInSharePointEnabled = tenant.NotificationsInSharePointEnabled;
            }
            catch
            {
                NotificationsInSharePointEnabled = true;
            }
            try
            {
                OwnerAnonymousNotification = tenant.OwnerAnonymousNotification;
            }
            catch
            {
                OwnerAnonymousNotification = true;
            }
            PublicCdnOrigins = new List<SPOPublicCdnOrigin>();
            try
            {
                tenant.PublicCdnOrigins.ToList<string>().ForEach(delegate (string s)
                {
                    string[] array = s.Split(new char[]
                    {
                        ','
                    });
                    PublicCdnOrigins.Add(new SPOPublicCdnOrigin(array[1], array[0]));
                });
            }
            catch
            {
            }
            try
            {
                FileAnonymousLinkType = tenant.FileAnonymousLinkType;
            }
            catch
            {
                FileAnonymousLinkType = Microsoft.SharePoint.Client.AnonymousLinkType.None;
            }
            try
            {
                FolderAnonymousLinkType = tenant.FolderAnonymousLinkType;
            }
            catch
            {
                FolderAnonymousLinkType = Microsoft.SharePoint.Client.AnonymousLinkType.None;
            }
            try
            {
                PermissiveBrowserFileHandlingOverride = tenant.PermissiveBrowserFileHandlingOverride;
            }
            catch
            {
                PermissiveBrowserFileHandlingOverride = false;
            }
            try
            {
                SpecialCharactersStateInFileFolderNames = tenant.SpecialCharactersStateInFileFolderNames;
            }
            catch
            {
                SpecialCharactersStateInFileFolderNames = SpecialCharactersState.NoPreference;
            }
            try
            {
                DisallowInfectedFileDownload = tenant.DisallowInfectedFileDownload;
            }
            catch
            {
                DisallowInfectedFileDownload = false;
            }
            try
            {
                CommentsOnSitePagesDisabled = tenant.CommentsOnSitePagesDisabled;
            }
            catch
            {
                CommentsOnSitePagesDisabled = false;
            }
            try
            {
                SocialBarOnSitePagesDisabled = tenant.SocialBarOnSitePagesDisabled;
            }
            catch
            {
                SocialBarOnSitePagesDisabled = true;
            }
            try
            {
                DefaultLinkPermission = tenant.DefaultLinkPermission;
            }
            catch
            {
                DefaultLinkPermission = SharingPermissionType.None;
            }
            try
            {
                ConditionalAccessPolicy = tenant.ConditionalAccessPolicy;
            }
            catch
            {
                ConditionalAccessPolicy = SPOConditionalAccessPolicyType.AllowFullAccess;
            }
            try
            {
                AllowDownloadingNonWebViewableFiles = tenant.AllowDownloadingNonWebViewableFiles;
            }
            catch
            {
                AllowDownloadingNonWebViewableFiles = true;
            }
            try
            {
                AllowEditing = tenant.AllowEditing;
            }
            catch
            {
                AllowEditing = true;
            }
            try
            {
                ApplyAppEnforcedRestrictionsToAdHocRecipients = tenant.ApplyAppEnforcedRestrictionsToAdHocRecipients;
            }
            catch
            {
                ApplyAppEnforcedRestrictionsToAdHocRecipients = true;
            }
            try
            {
                FilePickerExternalImageSearchEnabled = tenant.FilePickerExternalImageSearchEnabled;
            }
            catch
            {
                FilePickerExternalImageSearchEnabled = true;
            }
            try
            {
                EmailAttestationRequired = tenant.EmailAttestationRequired;
            }
            catch
            {
                EmailAttestationRequired = false;
            }
            try
            {
                EmailAttestationReAuthDays = tenant.EmailAttestationReAuthDays;
            }
            catch
            {
                EmailAttestationReAuthDays = 30;
            }
            try
            {
                DisableCustomAppAuthentication = tenant.DisableCustomAppAuthentication;
            }
            catch
            {
                DisableCustomAppAuthentication = false;
            }
            MarkNewFilesSensitiveByDefault = tenant.MarkNewFilesSensitiveByDefault;
            try
            {
                DisableSpacesActivation = tenant.DisableSpacesActivation;
            }
            catch
            {
                DisableSpacesActivation = false;
            }
            try
            {
                DisableAddToOneDrive = tenant.DisableAddToOneDrive;
            }
            catch
            {
                DisableAddToOneDrive = false;
            }
            try
            {
                IsFluidEnabled = tenant.IsFluidEnabled;
            }
            catch
            {
                IsFluidEnabled = false;
            }
            try
            {
                DisablePersonalListCreation = tenant.DisablePersonalListCreation;
            }
            catch
            {
                DisablePersonalListCreation = false;
            }

            DisabledModernListTemplateIds = tenant.DisabledModernListTemplateIds;
            RestrictedAccessControl = tenant.EnableRestrictedAccessControl;

            try
            {
                DisableDocumentLibraryDefaultLabeling = tenant.DisableDocumentLibraryDefaultLabeling;
            }
            catch
            {
                DisableDocumentLibraryDefaultLabeling = false;
            }

            try
            {
                IsEnableAppAuthPopUpEnabled = tenant.IsEnableAppAuthPopUpEnabled;
            }
            catch
            {
                IsEnableAppAuthPopUpEnabled = false;
            }

            try
            {
                var getAllowFilesWithKeepLabelToBeDeletedSPO = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAllowFilesWithKeepLabelToBeDeletedSPO(clientContext);
                var getAllowFilesWithKeepLabelToBeDeletedODB = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAllowFilesWithKeepLabelToBeDeletedODB(clientContext);
                clientContext.ExecuteQueryRetry();

                AllowFilesWithKeepLabelToBeDeletedSPO = getAllowFilesWithKeepLabelToBeDeletedSPO.Value;
                AllowFilesWithKeepLabelToBeDeletedODB = getAllowFilesWithKeepLabelToBeDeletedODB.Value;
            }
            catch { }

            try
            {
                ExpireVersionsAfterDays = tenant.ExpireVersionsAfterDays;
            }
            catch
            {
                ExpireVersionsAfterDays = 0;
            }

            try
            {
                MajorVersionLimit = tenant.MajorVersionLimit;
            }
            catch
            {
                MajorVersionLimit = 0;
            }

            try
            {
                EnableAutoExpirationVersionTrim = tenant.EnableAutoExpirationVersionTrim;
            }
            catch
            {
                EnableAutoExpirationVersionTrim = false;
            }

            try
            {
                EnableAzureADB2BIntegration = tenant.EnableAzureADB2BIntegration;
            }
            catch
            {
            }

            try
            {
                SiteOwnerManageLegacyServicePrincipalEnabled = tenant.SiteOwnerManageLegacyServicePrincipalEnabled;
            }
            catch
            {
            }

            CoreRequestFilesLinkEnabled = tenant.CoreRequestFilesLinkEnabled;
            CoreRequestFilesLinkExpirationInDays = tenant.CoreRequestFilesLinkExpirationInDays;
            OneDriveRequestFilesLinkEnabled = tenant.OneDriveRequestFilesLinkEnabled;
            OneDriveRequestFilesLinkExpirationInDays = tenant.OneDriveRequestFilesLinkExpirationInDays;
            BusinessConnectivityServiceDisabled = tenant.BusinessConnectivityServiceDisabled;

            try 
            {
                EnableSensitivityLabelForPDF = tenant.EnableSensitivityLabelForPDF;
            }
            catch 
            {
                EnableSensitivityLabelForPDF = false;
            }
        }
    }
}
