using System;
using Microsoft.Online.SharePoint.TenantAdministration;

namespace PnP.PowerShell.Commands.Model
{
    public class SPOSite
    {
        #region Properties
        public bool AllowDownloadingNonWebViewableFiles { get; set; }
        public bool AllowEditing { get; set; }
        public bool AllowSelfServiceUpgrade { get; set; }
        public int AnonymousLinkExpirationInDays { get; set; }
        public Microsoft.Online.SharePoint.TenantManagement.BlockDownloadLinksFileTypes BlockDownloadLinksFileType { get; set; }
        public bool CommentsOnSitePagesDisabled { get; set; }
        public int CompatibilityLevel { get; set; }
        public Microsoft.Online.SharePoint.TenantManagement.SPOConditionalAccessPolicyType ConditionalAccessPolicy { get; set; }
        public Microsoft.Online.SharePoint.TenantManagement.SharingPermissionType DefaultLinkPermission { get; set; }
        public bool DefaultLinkToExistingAccess { get; set; }
        public Microsoft.Online.SharePoint.TenantManagement.SharingLinkType DefaultSharingLinkType { get; set; }
        public DenyAddAndCustomizePagesStatus DenyAddAndCustomizePages { get; set; }
        public string Description { get; set; }
        public AppViewsPolicy DisableAppViews { get; set; }
        public CompanyWideSharingLinksPolicy DisableCompanyWideSharingLinks { get; set; }
        public FlowsPolicy DisableFlows { get; set; }
        public bool? DisableSharingForNonOwnersStatus { get; set; }
        public int ExternalUserExpirationInDays { get; set; }
        public Guid GroupId { get; set; }
        public Guid HubSiteId { get; }
        public Guid[] InformationSegment { get; set; }
        public bool IsHubSite { get; }
        public bool IsTeamsChannelConnected { get; }
        public bool IsTeamsConnected { get; }
        public DateTime LastContentModifiedDate { get; }
        public Microsoft.Online.SharePoint.TenantManagement.SPOLimitedAccessFileType LimitedAccessFileType { get; set; }
        public UInt32 LocaleId { get; set; }
        public string LockIssue { get; }
        public string LockState { get; set; }
        public bool OverrideTenantAnonymousLinkExpirationPolicy { get; set; }
        public bool OverrideTenantExternalUserExpirationPolicy { get; set; }
        public string Owner { get; }
        public string OwnerEmail { get; }
        public string OwnerLoginName { get; }
        public string OwnerName { get; }
        public string ProtectionLevelName { get; set; }
        public PWAEnabledStatus PWAEnabled { get; set; }
        public Guid RelatedGroupId { get; }
        public double ResourceQuota { get; set; }
        public double ResourceQuotaWarningLevel { get; set; }
        public double ResourceUsageAverage { get; set; }
        public double ResourceUsageCurrent { get; set; }
        public RestrictedToRegion RestrictedToGeo { get; set; }
        public SandboxedCodeActivationCapabilities SandboxedCodeActivationCapability { get; set; }
        public string SensitivityLabel { get; set; }
        public string SharingAllowedDomainList { get; set; }
        public string SharingBlockedDomainList { get; set; }
        public Microsoft.Online.SharePoint.TenantManagement.SharingCapabilities SharingCapability { get; set; }
        public Microsoft.Online.SharePoint.TenantManagement.SharingDomainRestrictionModes SharingDomainRestrictionMode { get; set; }
        public bool ShowPeoplePickerSuggestionsForGuestUsers { get; set; }
        public Microsoft.Online.SharePoint.TenantManagement.SharingCapabilities SiteDefinedSharingCapability { get; set; }
        public bool SocialBarOnSitePagesDisabled { get; set; }
        public string Status { get; set; }
        public long StorageQuota { get; set; }
        public string StorageQuotaType { get; set; }
        public long StorageQuotaWarningLevel { get; set; }
        public long StorageUsageCurrent { get; set; }
        public TeamsChannelTypeValue TeamsChannelType { get; set; }
        public string Template { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int WebsCount { get; set; }

        public string InformationBarrierMode { get; set; }
        public Guid[] InformationBarrierSegments { get; set; }
        public Guid[] InformationBarrierSegmentsToAdd { get; set; }
        public Guid[] InformationBarrierSegmentsToRemove { get; set; }

        public bool? RequestFilesLinkEnabled { private set; get; }
        public int? RequestFilesLinkExpirationInDays { private set; get; }
      
        #endregion


        public SPOSite(SiteProperties props, bool? disableSharingForNonOwnersStatus)
        {
            AllowDownloadingNonWebViewableFiles = props.AllowDownloadingNonWebViewableFiles;
            AllowEditing = props.AllowEditing;
            AllowSelfServiceUpgrade = props.AllowSelfServiceUpgrade;
            AnonymousLinkExpirationInDays = props.AnonymousLinkExpirationInDays;
            BlockDownloadLinksFileType = props.BlockDownloadLinksFileType;
            CommentsOnSitePagesDisabled = props.CommentsOnSitePagesDisabled;
            CompatibilityLevel = props.CompatibilityLevel;
            ConditionalAccessPolicy = props.ConditionalAccessPolicy;
            DefaultLinkPermission = props.DefaultLinkPermission;
            DefaultLinkToExistingAccess = props.DefaultLinkToExistingAccess;
            DefaultSharingLinkType = props.DefaultSharingLinkType;
            DenyAddAndCustomizePages = props.DenyAddAndCustomizePages;
            Description = props.Description;
            DisableAppViews = props.DisableAppViews;
            DisableCompanyWideSharingLinks = props.DisableCompanyWideSharingLinks;
            DisableFlows = props.DisableFlows;
            DisableSharingForNonOwnersStatus = disableSharingForNonOwnersStatus;
            ExternalUserExpirationInDays = props.ExternalUserExpirationInDays;
            GroupId = props.GroupId;
            HubSiteId = props.HubSiteId;
            IsHubSite = props.IsHubSite;
            IsTeamsChannelConnected = props.IsTeamsChannelConnected;
            IsTeamsConnected = props.IsTeamsConnected;
            LastContentModifiedDate = props.LastContentModifiedDate;
            LimitedAccessFileType = props.LimitedAccessFileType;
            LocaleId = props.Lcid;
            LockIssue = props.LockIssue;
            LockState = props.LockState;
            Owner = props.Owner;
            OwnerEmail = props.OwnerEmail;
            OwnerLoginName = props.OwnerLoginName;
            OwnerName = props.OwnerName;
            OverrideTenantAnonymousLinkExpirationPolicy = props.OverrideTenantAnonymousLinkExpirationPolicy;
            OverrideTenantExternalUserExpirationPolicy = props.OverrideTenantExternalUserExpirationPolicy;
            PWAEnabled = props.PWAEnabled;
            RelatedGroupId = props.RelatedGroupId;
            ResourceQuota = props.UserCodeMaximumLevel;
            ResourceQuotaWarningLevel = props.UserCodeWarningLevel;
            ResourceUsageAverage = props.AverageResourceUsage;
            ResourceUsageCurrent = props.CurrentResourceUsage;
            RestrictedToGeo = props.RestrictedToRegion;
            SandboxedCodeActivationCapability = props.SandboxedCodeActivationCapability;
            SensitivityLabel = props.SensitivityLabel2;
            SharingAllowedDomainList = props.SharingAllowedDomainList;
            SharingBlockedDomainList = props.SharingBlockedDomainList;
            SharingCapability = props.SharingCapability;
            SharingDomainRestrictionMode = props.SharingDomainRestrictionMode;
            ShowPeoplePickerSuggestionsForGuestUsers = props.ShowPeoplePickerSuggestionsForGuestUsers;
            SiteDefinedSharingCapability = props.SiteDefinedSharingCapability;
            SocialBarOnSitePagesDisabled = props.SocialBarOnSitePagesDisabled;
            Status = props.Status;
            StorageQuota = props.StorageMaximumLevel;
            StorageQuotaType = props.StorageQuotaType;
            StorageQuotaWarningLevel = props.StorageWarningLevel;
            StorageUsageCurrent = props.StorageUsage;
            TeamsChannelType = props.TeamsChannelType;
            Template = props.Template;
            Title = props.Title;            
            WebsCount = props.WebsCount;
            Url = props.Url;
            InformationBarrierMode = props.IBMode;
            InformationBarrierSegments = props.IBSegments;
            InformationBarrierSegmentsToAdd = props.IBSegmentsToAdd;
            InformationBarrierSegmentsToRemove = props.IBSegmentsToRemove;
            RequestFilesLinkEnabled = props.RequestFilesLinkEnabled;
            RequestFilesLinkExpirationInDays = props.RequestFilesLinkExpirationInDays;
        }
    }
}