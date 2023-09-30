﻿using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Model
{
    public class SPOTenant
    {
        public SPOTenant(Tenant tenant, ClientContext clientContext)
        {
            this.hideDefaultThemes = tenant.HideDefaultThemes;
            this.storageQuota = tenant.StorageQuota;
            this.storageQuotaAllocated = tenant.StorageQuotaAllocated;
            this.resourceQuota = tenant.ResourceQuota;
            this.resourceQuotaAllocated = tenant.ResourceQuotaAllocated;
            this.oneDriveStorageQuota = tenant.OneDriveStorageQuota;
            this.compatibilityRange = tenant.CompatibilityRange;
            this.externalServicesEnabled = tenant.ExternalServicesEnabled;
            this.noAccessRedirectUrl = tenant.NoAccessRedirectUrl;
            this.sharingCapability = tenant.SharingCapability;
            this.displayStartASiteOption = tenant.DisplayStartASiteOption;
            this.startASiteFormUrl = tenant.StartASiteFormUrl;
            this.showEveryoneClaim = tenant.ShowEveryoneClaim;
            this.showAllUsersClaim = tenant.ShowAllUsersClaim;
            this.officeClientADALDisabled = tenant.OfficeClientADALDisabled;
            this.orphanedPersonalSitesRetentionPeriod = tenant.OrphanedPersonalSitesRetentionPeriod;
            this.legacyAuthProtocolsEnabled = tenant.LegacyAuthProtocolsEnabled;
            this.showEveryoneExceptExternalUsersClaim = tenant.ShowEveryoneExceptExternalUsersClaim;
            this.searchResolveExactEmailOrUPN = tenant.SearchResolveExactEmailOrUPN;
            this.requireAcceptingAccountMatchInvitedAccount = tenant.RequireAcceptingAccountMatchInvitedAccount;
            this.provisionSharedWithEveryoneFolder = tenant.ProvisionSharedWithEveryoneFolder;
            this.signInAccelerationDomain = tenant.SignInAccelerationDomain;
            this.disabledWebPartIds = tenant.DisabledWebPartIds;
            this.stopNew2013Workflows = tenant.StopNew2013Workflows;
            this.viewInFileExplorerEnabled = tenant.ViewInFileExplorerEnabled;
            this.externalUserExpirationRequired = tenant.ExternalUserExpirationRequired;
            this.externalUserExpireInDays = tenant.ExternalUserExpireInDays;
            this.displayNamesOfFileViewers = tenant.DisplayNamesOfFileViewers;
            this.displayNamesOfFileViewersInSpo = tenant.DisplayNamesOfFileViewersInSpo;
            this.isLoopEnabled = tenant.IsLoopEnabled;
            this.enableAzureADB2BIntegration = tenant.EnableAzureADB2BIntegration;
            this.siteOwnerManageLegacyServicePrincipalEnabled = tenant.SiteOwnerManageLegacyServicePrincipalEnabled;

            try
            {
                this.enableGuestSignInAcceleration = tenant.EnableGuestSignInAcceleration;
            }
            catch
            {
                this.enableGuestSignInAcceleration = false;
            }
            this.usePersistentCookiesForExplorerView = tenant.UsePersistentCookiesForExplorerView;
            this.bccExternalSharingInvitations = tenant.BccExternalSharingInvitations;
            this.bccExternalSharingInvitationsList = tenant.BccExternalSharingInvitationsList;
            try
            {
                this.useFindPeopleInPeoplePicker = tenant.UseFindPeopleInPeoplePicker;
            }
            catch
            {
                this.useFindPeopleInPeoplePicker = false;
            }
            try
            {
                this.userVoiceForFeedbackEnabled = tenant.UserVoiceForFeedbackEnabled;
            }
            catch
            {
                this.userVoiceForFeedbackEnabled = true;
            }
            try
            {
                this.requireAnonymousLinksExpireInDays = tenant.RequireAnonymousLinksExpireInDays;
            }
            catch
            {
                this.requireAnonymousLinksExpireInDays = 0;
            }
            this.sharingAllowedDomainList = tenant.SharingAllowedDomainList;
            this.sharingBlockedDomainList = tenant.SharingBlockedDomainList;
            this.sharingDomainRestrictionMode = tenant.SharingDomainRestrictionMode;
            try
            {
                this.oneDriveStorageQuota = tenant.OneDriveStorageQuota;
            }
            catch
            {
                this.oneDriveStorageQuota = 0L;
            }
            this.oneDriveForGuestsEnabled = tenant.OneDriveForGuestsEnabled;
            try
            {
                this.ipAddressEnforcement = tenant.IPAddressEnforcement;
            }
            catch
            {
                this.ipAddressEnforcement = false;
            }
            try
            {
                this.ipAddressAllowList = tenant.IPAddressAllowList;
            }
            catch
            {
                this.ipAddressAllowList = "";
            }
            try
            {
                this.ipAddressWACTokenLifetime = tenant.IPAddressWACTokenLifetime;
            }
            catch
            {
                this.ipAddressWACTokenLifetime = 600;
            }
            try
            {
                this.defaultSharingLinkType = tenant.DefaultSharingLinkType;
            }
            catch
            {
                this.defaultSharingLinkType = SharingLinkType.None;
            }
            try
            {
                this.showPeoplePickerSuggestionsForGuestUsers = tenant.ShowPeoplePickerSuggestionsForGuestUsers;
            }
            catch
            {
                this.showPeoplePickerSuggestionsForGuestUsers = false;
            }
            try
            {
                this.odbMembersCanShare = tenant.ODBMembersCanShare;
            }
            catch
            {
                this.odbMembersCanShare = SharingState.Unspecified;
            }
            try
            {
                this.odbAccessRequests = tenant.ODBAccessRequests;
            }
            catch
            {
                this.odbAccessRequests = SharingState.Unspecified;
            }
            try
            {
                this.preventExternalUsersFromResharing = tenant.PreventExternalUsersFromResharing;
            }
            catch
            {
                this.preventExternalUsersFromResharing = false;
            }
            try
            {
                this.publicCdnEnabled = tenant.PublicCdnEnabled;
            }
            catch
            {
                this.publicCdnEnabled = false;
            }
            try
            {
                this.publicCdnAllowedFileTypes = tenant.PublicCdnAllowedFileTypes;
            }
            catch
            {
                this.publicCdnAllowedFileTypes = string.Empty;
            }
            try
            {
                this.notifyOwnersWhenItemsReshared = tenant.NotifyOwnersWhenItemsReshared;
            }
            catch
            {
                this.notifyOwnersWhenItemsReshared = true;
            }
            try
            {
                this.notifyOwnersWhenInvitationsAccepted = tenant.NotifyOwnersWhenInvitationsAccepted;
            }
            catch
            {
                this.notifyOwnersWhenInvitationsAccepted = true;
            }
            try
            {
                this.notificationsInOneDriveForBusinessEnabled = tenant.NotificationsInOneDriveForBusinessEnabled;
            }
            catch
            {
                this.notificationsInOneDriveForBusinessEnabled = true;
            }
            try
            {
                this.notificationsInSharePointEnabled = tenant.NotificationsInSharePointEnabled;
            }
            catch
            {
                this.notificationsInSharePointEnabled = true;
            }
            try
            {
                this.ownerAnonymousNotification = tenant.OwnerAnonymousNotification;
            }
            catch
            {
                this.ownerAnonymousNotification = true;
            }
            this.publicCdnOrigins = new List<SPOPublicCdnOrigin>();
            try
            {
                tenant.PublicCdnOrigins.ToList<string>().ForEach(delegate (string s)
                {
                    string[] array = s.Split(new char[]
                    {
                        ','
                    });
                    this.publicCdnOrigins.Add(new SPOPublicCdnOrigin(array[1], array[0]));
                });
            }
            catch
            {
            }
            try
            {
                this.fileAnonymousLinkType = tenant.FileAnonymousLinkType;
            }
            catch
            {
                this.fileAnonymousLinkType = AnonymousLinkType.None;
            }
            try
            {
                this.folderAnonymousLinkType = tenant.FolderAnonymousLinkType;
            }
            catch
            {
                this.folderAnonymousLinkType = AnonymousLinkType.None;
            }
            try
            {
                this.permissiveBrowserFileHandlingOverride = tenant.PermissiveBrowserFileHandlingOverride;
            }
            catch
            {
                this.permissiveBrowserFileHandlingOverride = false;
            }
            try
            {
                this.specialCharactersStateInFileFolderNames = tenant.SpecialCharactersStateInFileFolderNames;
            }
            catch
            {
                this.specialCharactersStateInFileFolderNames = SpecialCharactersState.NoPreference;
            }
            try
            {
                this.disallowInfectedFileDownload = tenant.DisallowInfectedFileDownload;
            }
            catch
            {
                this.disallowInfectedFileDownload = false;
            }
            try
            {
                this.commentsOnSitePagesDisabled = tenant.CommentsOnSitePagesDisabled;
            }
            catch
            {
                this.commentsOnSitePagesDisabled = false;
            }
            try
            {
                this.socialBarOnSitePagesDisabled = tenant.SocialBarOnSitePagesDisabled;
            }
            catch
            {
                this.socialBarOnSitePagesDisabled = true;
            }
            try
            {
                this.defaultLinkPermission = tenant.DefaultLinkPermission;
            }
            catch
            {
                this.defaultLinkPermission = SharingPermissionType.None;
            }
            try
            {
                this.conditionalAccessPolicy = tenant.ConditionalAccessPolicy;
            }
            catch
            {
                this.conditionalAccessPolicy = SPOConditionalAccessPolicyType.AllowFullAccess;
            }
            try
            {
                this.allowDownloadingNonWebViewableFiles = tenant.AllowDownloadingNonWebViewableFiles;
            }
            catch
            {
                this.allowDownloadingNonWebViewableFiles = true;
            }
            try
            {
                this.allowEditing = tenant.AllowEditing;
            }
            catch
            {
                this.allowEditing = true;
            }
            try
            {
                this.applyAppEnforcedRestrictionsToAdHocRecipients = tenant.ApplyAppEnforcedRestrictionsToAdHocRecipients;
            }
            catch
            {
                this.applyAppEnforcedRestrictionsToAdHocRecipients = true;
            }
            try
            {
                this.filePickerExternalImageSearchEnabled = tenant.FilePickerExternalImageSearchEnabled;
            }
            catch
            {
                this.filePickerExternalImageSearchEnabled = true;
            }
            try
            {
                this.emailAttestationRequired = tenant.EmailAttestationRequired;
            }
            catch
            {
                this.emailAttestationRequired = false;
            }
            try
            {
                this.emailAttestationReAuthDays = tenant.EmailAttestationReAuthDays;
            }
            catch
            {
                this.emailAttestationReAuthDays = 30;
            }
            try
            {
                this.disableCustomAppAuthentication = tenant.DisableCustomAppAuthentication;
            }
            catch
            {
                this.disableCustomAppAuthentication = false;
            }
            this.markNewFilesSensitiveByDefault = tenant.MarkNewFilesSensitiveByDefault;
            try
            {
                this.disableSpacesActivation = tenant.DisableSpacesActivation;
            }
            catch
            {
                this.disableSpacesActivation = false;
            }
            try
            {
                this.disableAddToOneDrive = tenant.DisableAddToOneDrive;
            }
            catch
            {
                this.disableAddToOneDrive = false;
            }
            try
            {
                this.isFluidEnabled = tenant.IsFluidEnabled;
            }
            catch
            {
                this.isFluidEnabled = false;
            }
            try
            {
                this.disablePersonalListCreation = tenant.DisablePersonalListCreation;
            }
            catch
            {
                this.disablePersonalListCreation = false;
            }

            this.disabledModernListTemplateIds = tenant.DisabledModernListTemplateIds;
            this.restrictedAccessControl = tenant.EnableRestrictedAccessControl;

            try
            {
                this.disableDocumentLibraryDefaultLabeling = tenant.DisableDocumentLibraryDefaultLabeling;
            }
            catch
            {
                this.disableDocumentLibraryDefaultLabeling = false;
            }

            try
            {
                this.isEnableAppAuthPopUpEnabled = tenant.IsEnableAppAuthPopUpEnabled;
            }
            catch
            {
                this.isEnableAppAuthPopUpEnabled = false;
            }

            try
            {
                var getAllowFilesWithKeepLabelToBeDeletedSPO = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAllowFilesWithKeepLabelToBeDeletedSPO(clientContext);
                var getAllowFilesWithKeepLabelToBeDeletedODB = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAllowFilesWithKeepLabelToBeDeletedODB(clientContext);
                clientContext.ExecuteQueryRetry();

                this.allowFilesWithKeepLabelToBeDeletedSPO = getAllowFilesWithKeepLabelToBeDeletedSPO.Value;
                this.allowFilesWithKeepLabelToBeDeletedODB = getAllowFilesWithKeepLabelToBeDeletedODB.Value;
            }
            catch { }

            try
            {
                this.expireVersionsAfterDays = tenant.ExpireVersionsAfterDays;
            }
            catch
            {
                this.expireVersionsAfterDays = 0;
            }

            try
            {
                this.majorVersionLimit = tenant.MajorVersionLimit;
            }
            catch
            {
                this.majorVersionLimit = 0;
            }

            try
            {
                this.enableAutoExpirationVersionTrim = tenant.EnableAutoExpirationVersionTrim;
            }
            catch
            {
                this.enableAutoExpirationVersionTrim = false;
            }

            try
            {
                this.enableAzureADB2BIntegration = tenant.EnableAzureADB2BIntegration;
            }
            catch
            {
            }

            try
            {
                this.siteOwnerManageLegacyServicePrincipalEnabled = tenant.SiteOwnerManageLegacyServicePrincipalEnabled;
            }
            catch
            {
            }
        }

        public bool HideDefaultThemes => hideDefaultThemes;

        public long StorageQuota => storageQuota;

        public long StorageQuotaAllocated => storageQuotaAllocated;

        public double ResourceQuota => resourceQuota;

        public double ResourceQuotaAllocated => resourceQuotaAllocated;

        public double OneDriveStorageQuota => oneDriveStorageQuota;

        public string CompatibilityRange => compatibilityRange;

        public bool ExternalServicesEnabled => externalServicesEnabled;

        public string NoAccessRedirectUrl => noAccessRedirectUrl;

        public SharingCapabilities SharingCapability => sharingCapability;

        public bool DisplayStartASiteOption => displayStartASiteOption;

        public string StartASiteFormUrl => startASiteFormUrl;

        public bool ShowEveryoneClaim => showEveryoneClaim;

        public bool ShowAllUsersClaim => showAllUsersClaim;

        public bool OfficeClientADALDisabled => officeClientADALDisabled;

        public bool LegacyAuthProtocolsEnabled => legacyAuthProtocolsEnabled;

        public bool ShowEveryoneExceptExternalUsersClaim => showEveryoneExceptExternalUsersClaim;

        public bool SearchResolveExactEmailOrUPN => searchResolveExactEmailOrUPN;

        public bool RequireAcceptingAccountMatchInvitedAccount => requireAcceptingAccountMatchInvitedAccount;

        public bool ProvisionSharedWithEveryoneFolder => provisionSharedWithEveryoneFolder;

        public string SignInAccelerationDomain => signInAccelerationDomain;

        public bool EnableGuestSignInAcceleration => enableGuestSignInAcceleration;

        public bool UsePersistentCookiesForExplorerView => usePersistentCookiesForExplorerView;

        public bool BccExternalSharingInvitations => bccExternalSharingInvitations;

        public string BccExternalSharingInvitationsList => bccExternalSharingInvitationsList;

        public bool UserVoiceForFeedbackEnabled => userVoiceForFeedbackEnabled;

        public bool PublicCdnEnabled => publicCdnEnabled;

        public string PublicCdnAllowedFileTypes => publicCdnAllowedFileTypes;

        public IList<SPOPublicCdnOrigin> PublicCdnOrigins => publicCdnOrigins;

        public int RequireAnonymousLinksExpireInDays => requireAnonymousLinksExpireInDays;

        public string SharingAllowedDomainList => sharingAllowedDomainList;

        public string SharingBlockedDomainList => sharingBlockedDomainList;

        public SharingDomainRestrictionModes SharingDomainRestrictionMode => sharingDomainRestrictionMode;

        public bool OneDriveForGuestsEnabled => oneDriveForGuestsEnabled;

        public bool IPAddressEnforcement => ipAddressEnforcement;

        public string IPAddressAllowList => ipAddressAllowList;

        public int IPAddressWACTokenLifetime => ipAddressWACTokenLifetime;

        public bool UseFindPeopleInPeoplePicker => useFindPeopleInPeoplePicker;

        public SharingLinkType DefaultSharingLinkType => defaultSharingLinkType;

        public SharingState ODBMembersCanShare => odbMembersCanShare;

        public SharingState ODBAccessRequests => odbAccessRequests;

        public bool PreventExternalUsersFromResharing => preventExternalUsersFromResharing;

        public bool ShowPeoplePickerSuggestionsForGuestUsers => showPeoplePickerSuggestionsForGuestUsers;

        public AnonymousLinkType FileAnonymousLinkType => fileAnonymousLinkType;

        public AnonymousLinkType FolderAnonymousLinkType => folderAnonymousLinkType;

        public bool NotifyOwnersWhenItemsReshared => notifyOwnersWhenItemsReshared;

        public bool NotifyOwnersWhenInvitationsAccepted => notifyOwnersWhenInvitationsAccepted;

        public bool NotificationsInOneDriveForBusinessEnabled => notificationsInOneDriveForBusinessEnabled;

        public bool NotificationsInSharePointEnabled => notificationsInSharePointEnabled;

        public SpecialCharactersState SpecialCharactersStateInFileFolderNames => specialCharactersStateInFileFolderNames;

        public bool OwnerAnonymousNotification => ownerAnonymousNotification;

        public bool CommentsOnSitePagesDisabled => commentsOnSitePagesDisabled;

        public bool SocialBarOnSitePagesDisabled => socialBarOnSitePagesDisabled;

        public int OrphanedPersonalSitesRetentionPeriod => orphanedPersonalSitesRetentionPeriod;

        public bool PermissiveBrowserFileHandlingOverride => permissiveBrowserFileHandlingOverride;

        public bool DisallowInfectedFileDownload => disallowInfectedFileDownload;

        public SharingPermissionType DefaultLinkPermission => defaultLinkPermission;

        public SPOConditionalAccessPolicyType ConditionalAccessPolicy => conditionalAccessPolicy;

        public bool AllowDownloadingNonWebViewableFiles => allowDownloadingNonWebViewableFiles;

        public bool AllowEditing => allowEditing;

        public bool ApplyAppEnforcedRestrictionsToAdHocRecipients => applyAppEnforcedRestrictionsToAdHocRecipients;

        public bool FilePickerExternalImageSearchEnabled => filePickerExternalImageSearchEnabled;

        public bool EmailAttestationRequired => emailAttestationRequired;

        public int EmailAttestationReAuthDays => emailAttestationReAuthDays;

        public Guid[] DisabledWebPartIds => disabledWebPartIds;

        public bool DisableCustomAppAuthentication => disableCustomAppAuthentication;

        public SensitiveByDefaultState MarkNewFilesSensitiveByDefault => markNewFilesSensitiveByDefault;

        public bool StopNew2013Workflows => stopNew2013Workflows;

        public bool ViewInFileExplorerEnabled => viewInFileExplorerEnabled;

        public bool DisableSpacesActivation => disableSpacesActivation;

        public bool? AllowFilesWithKeepLabelToBeDeletedSPO => allowFilesWithKeepLabelToBeDeletedSPO;

        public bool? AllowFilesWithKeepLabelToBeDeletedODB => allowFilesWithKeepLabelToBeDeletedODB;

        public bool DisableAddToOneDrive => disableAddToOneDrive;

        public bool IsFluidEnabled => isFluidEnabled;
        public bool DisablePersonalListCreation => disablePersonalListCreation;

        public bool ExternalUserExpirationRequired => externalUserExpirationRequired;

        public int ExternalUserExpireInDays => externalUserExpireInDays;

        public bool DisplayNamesOfFileViewers => displayNamesOfFileViewers;

        public bool DisplayNamesOfFileViewersInSpo => displayNamesOfFileViewersInSpo;
        public bool IsLoopEnabled => isLoopEnabled;
        public Guid[] DisabledModernListTemplateIds => disabledModernListTemplateIds;
        public bool RestrictedAccessControl => restrictedAccessControl;
        public bool DisableDocumentLibraryDefaultLabeling => disableDocumentLibraryDefaultLabeling;
        public bool IsEnableAppAuthPopUpEnabled => isEnableAppAuthPopUpEnabled;
        public int? ExpireVersionsAfterDays => expireVersionsAfterDays;
        public int? MajorVersionLimit => majorVersionLimit;
        public bool? EnableAutoExpirationVersionTrim => enableAutoExpirationVersionTrim;
        public bool? EnableAzureADB2BIntegration => enableAzureADB2BIntegration;
        public bool? SiteOwnerManageLegacyServicePrincipalEnabled => siteOwnerManageLegacyServicePrincipalEnabled;

        private bool hideDefaultThemes;

        private long storageQuota;

        private long storageQuotaAllocated;

        private double resourceQuota;

        private double resourceQuotaAllocated;

        private long oneDriveStorageQuota;

        private string compatibilityRange;

        private bool externalServicesEnabled;

        private string noAccessRedirectUrl;

        private SharingCapabilities sharingCapability;

        private bool displayStartASiteOption;

        private string startASiteFormUrl;

        private bool showEveryoneClaim;

        private bool showAllUsersClaim;

        private bool officeClientADALDisabled;

        private bool legacyAuthProtocolsEnabled;

        private bool showEveryoneExceptExternalUsersClaim;

        private bool searchResolveExactEmailOrUPN;

        private bool requireAcceptingAccountMatchInvitedAccount;

        private bool provisionSharedWithEveryoneFolder;

        private string signInAccelerationDomain;

        private bool enableGuestSignInAcceleration;

        private bool usePersistentCookiesForExplorerView;

        private bool bccExternalSharingInvitations;

        private string bccExternalSharingInvitationsList;

        private bool userVoiceForFeedbackEnabled;

        private bool publicCdnEnabled;

        private string publicCdnAllowedFileTypes;

        private IList<SPOPublicCdnOrigin> publicCdnOrigins;

        private int requireAnonymousLinksExpireInDays;

        private string sharingAllowedDomainList;

        private string sharingBlockedDomainList;

        private SharingDomainRestrictionModes sharingDomainRestrictionMode;

        private bool oneDriveForGuestsEnabled;

        private bool ipAddressEnforcement;

        private string ipAddressAllowList;

        private int ipAddressWACTokenLifetime;

        private bool useFindPeopleInPeoplePicker;

        private SharingLinkType defaultSharingLinkType;

        private SharingState odbMembersCanShare;

        private SharingState odbAccessRequests;

        private bool preventExternalUsersFromResharing;

        private bool showPeoplePickerSuggestionsForGuestUsers;

        private AnonymousLinkType fileAnonymousLinkType;

        private AnonymousLinkType folderAnonymousLinkType;

        private bool notifyOwnersWhenItemsReshared;

        private bool notifyOwnersWhenInvitationsAccepted;

        private bool notificationsInOneDriveForBusinessEnabled;

        private bool notificationsInSharePointEnabled;

        private SpecialCharactersState specialCharactersStateInFileFolderNames;

        private bool ownerAnonymousNotification;

        private bool commentsOnSitePagesDisabled;

        private bool socialBarOnSitePagesDisabled;

        private int orphanedPersonalSitesRetentionPeriod;

        private bool permissiveBrowserFileHandlingOverride;

        private bool disallowInfectedFileDownload;

        private SharingPermissionType defaultLinkPermission;

        private SPOConditionalAccessPolicyType conditionalAccessPolicy;

        private bool allowDownloadingNonWebViewableFiles = true;

        private bool allowEditing = true;

        private bool applyAppEnforcedRestrictionsToAdHocRecipients;

        private bool filePickerExternalImageSearchEnabled;

        private bool emailAttestationRequired;

        private int emailAttestationReAuthDays;

        private Guid[] disabledWebPartIds;

        private bool disableCustomAppAuthentication;

        private SensitiveByDefaultState markNewFilesSensitiveByDefault;

        private bool stopNew2013Workflows;

        private bool viewInFileExplorerEnabled;

        private bool disableSpacesActivation;

        private bool? allowFilesWithKeepLabelToBeDeletedSPO;

        private bool? allowFilesWithKeepLabelToBeDeletedODB;

        private bool disableAddToOneDrive;

        private bool isFluidEnabled;

        private bool disablePersonalListCreation;

        private Guid[] disabledModernListTemplateIds;

        private bool externalUserExpirationRequired;

        private int externalUserExpireInDays;

        private bool displayNamesOfFileViewers;

        private bool displayNamesOfFileViewersInSpo;

        private bool isLoopEnabled;

        private bool restrictedAccessControl;

        private bool disableDocumentLibraryDefaultLabeling;

        private bool isEnableAppAuthPopUpEnabled;

        private int? expireVersionsAfterDays;

        private int? majorVersionLimit;

        private bool? enableAutoExpirationVersionTrim;

        private bool? enableAzureADB2BIntegration;

        private bool? siteOwnerManageLegacyServicePrincipalEnabled;
    }
}
