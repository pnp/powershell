using System;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using PnP.Framework;
using PnP.Framework.Entities;
using Microsoft.Online.SharePoint.TenantAdministration;
using System.Net;
using System.Threading;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantSite")]
    public class SetTenantSite : PnPAdminCmdlet
    {
        private const string ParameterSet_LOCKSTATE = "Set Lock State";
        private const string ParameterSet_PROPERTIES = "Set Properties";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("Url")]
        public SPOSitePipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public string Title;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public uint LocaleId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter AllowSelfServiceUpgrade;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public UserPipeBind PrimarySiteCollectionAdmin;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public List<string> Owners;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        [Alias("NoScriptSite")]
        public SwitchParameter DenyAddAndCustomizePages;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter DisableSharingForNonOwners;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SharingCapabilities SharingCapability;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        [Alias("StorageMaximumLevel")]
        public long StorageQuota;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        [Alias("StorageWarningLevel")]
        public long StorageQuotaWarningLevel;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter StorageQuotaReset;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public double ResourceQuota;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public double ResourceQuotaWarningLevel;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public bool EnablePWA;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public bool ShowPeoplePickerSuggestionsForGuestUsers;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LOCKSTATE)]
        public SiteLockState? LockState;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SharingPermissionType DefaultLinkPermission;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SharingLinkType DefaultSharingLinkType;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public bool? DefaultLinkToExistingAccess;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter DefaultLinkToExistingAccessReset;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public string SharingAllowedDomainList;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public string SharingBlockedDomainList;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public bool AllowDownloadingNonWebViewableFiles;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SharingDomainRestrictionModes SharingDomainRestrictionMode = SharingDomainRestrictionModes.None;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter CommentsOnSitePagesDisabled;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public AppViewsPolicy DisableAppViews;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public CompanyWideSharingLinksPolicy DisableCompanyWideSharingLinks;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public FlowsPolicy DisableFlows;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public int? AnonymousLinkExpirationInDays;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter OverrideTenantAnonymousLinkExpirationPolicy;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter RemoveLabel;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public string SensitivityLabel;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public PnPConditionalAccessPolicyType ConditionalAccessPolicy;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public string ProtectionLevelName;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SPOLimitedAccessFileType LimitedAccessFileType;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public bool AllowEditing;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public RestrictedToRegion RestrictedToGeo;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public Guid HubSiteId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public int ExternalUserExpirationInDays;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public bool OverrideTenantExternalUserExpirationPolicy;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public Guid[] AddInformationSegment;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public Guid[] RemoveInformationSegment;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public BlockDownloadLinksFileTypes BlockDownloadLinksFileType;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SiteUserInfoVisibilityPolicyValue OverrideBlockUserInfoVisibility { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public InformationBarriersMode InformationBarriersMode;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public MediaTranscriptionPolicyType? MediaTranscription { get; set; }        

        [Parameter(Mandatory = false)]
        public SwitchParameter Wait;

        protected override void ExecuteCmdlet()
        {
            ClientContext.ExecuteQueryRetry(); // fixes issue where ServerLibraryVersion is not available.

            Func<TenantOperationMessage, bool> timeoutFunction = TimeoutFunction;

            if (LockState.HasValue)
            {
                Tenant.SetSiteLockState(Identity.Url, LockState.Value, Wait, Wait ? timeoutFunction : null);
                WriteWarning("You changed the lockstate of a site. This change is not guaranteed to be effective immediately. Please wait a few minutes for this to take effect.");
            }
            if (!LockState.HasValue)
            {
                SetSiteProperties(timeoutFunction);
            }
        }

        private void SetSiteProperties(Func<TenantOperationMessage, bool> timeoutFunction)
        {
            var props = GetSiteProperties(Identity.Url);
            var updateRequired = false;

            ClientContext.Load(props);
            ClientContext.ExecuteQueryRetry();

            if (ParameterSpecified(nameof(Title)))
            {
                props.Title = Title;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(DenyAddAndCustomizePages)))
            {
                props.DenyAddAndCustomizePages = DenyAddAndCustomizePages ? DenyAddAndCustomizePagesStatus.Enabled : DenyAddAndCustomizePagesStatus.Disabled;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(LocaleId)))
            {
                props.Lcid = LocaleId;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(AllowSelfServiceUpgrade)))
            {
                props.AllowSelfServiceUpgrade = AllowSelfServiceUpgrade;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(SharingAllowedDomainList)))
            {
                props.SharingAllowedDomainList = SharingAllowedDomainList;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(SharingBlockedDomainList)))
            {
                props.SharingBlockedDomainList = SharingBlockedDomainList;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(SharingDomainRestrictionMode)))
            {
                props.SharingDomainRestrictionMode = SharingDomainRestrictionMode;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(StorageQuota)))
            {
                props.StorageMaximumLevel = StorageQuota;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(StorageQuotaWarningLevel)))
            {
                props.StorageWarningLevel = StorageQuotaWarningLevel;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(StorageQuotaReset)))
            {
                props.StorageMaximumLevel = 0;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(ResourceQuota)))
            {
                props.UserCodeMaximumLevel = ResourceQuota;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(ResourceQuotaWarningLevel)))
            {
                props.UserCodeWarningLevel = ResourceQuotaWarningLevel;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(SharingCapability)))
            {
                props.SharingCapability = SharingCapability;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(DefaultLinkPermission)))
            {
                props.DefaultLinkPermission = DefaultLinkPermission;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(ShowPeoplePickerSuggestionsForGuestUsers)))
            {
                Tenant.EnsureProperty(t => t.ShowPeoplePickerSuggestionsForGuestUsers);
                if (!Tenant.ShowPeoplePickerSuggestionsForGuestUsers)
                {
                    WriteWarning("ShowPeoplePickerSuggestionsForGuests users has been disabled for this tenant. See Set-PnPTenant");
                }
                props.ShowPeoplePickerSuggestionsForGuestUsers = ShowPeoplePickerSuggestionsForGuestUsers;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(DefaultSharingLinkType)))
            {
                props.DefaultSharingLinkType = DefaultSharingLinkType;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(DefaultLinkToExistingAccess)))
            {
                props.DefaultLinkToExistingAccess = DefaultLinkToExistingAccess.Value;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(DefaultLinkToExistingAccessReset)))
            {
                props.DefaultLinkToExistingAccessReset = true;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(AllowDownloadingNonWebViewableFiles)))
            {
                var value = AllowDownloadingNonWebViewableFiles;
                if (ConditionalAccessPolicy == PnPConditionalAccessPolicyType.AllowLimitedAccess)
                {
                    props.AllowDownloadingNonWebViewableFiles = value;
                    updateRequired = true;
                    if (!value)
                    {
                        WriteWarning("Users will not be able to download files that cannot be viewed on the web. To allow download of files that cannot be viewed on the web run the cmdlet again and set AllowDownloadingNonWebViewableFiles to true.");
                    }
                }
                else
                {
                    if (ShouldContinue("To set AllowDownloadingNonWebViewableFiles parameter you need to set the -ConditionalAccessPolicy parameter to AllowLimitedAccess. We can set the Conditional Access Policy of this site to AllowLimitedAccess. Would you like to continue?", string.Empty))
                    {
                        ConditionalAccessPolicy = PnPConditionalAccessPolicyType.AllowLimitedAccess;
                        props.ConditionalAccessPolicy = SPOConditionalAccessPolicyType.AllowLimitedAccess;
                        props.AllowDownloadingNonWebViewableFiles = value;
                        if (!value)
                        {
                            WriteWarning("Users will not be able to download files that cannot be viewed on the web. To allow download of files that cannot be viewed on the web run the cmdlet again and set AllowDownloadingNonWebViewableFiles to true.");
                        }
                    }
                }
            }

            if (ParameterSpecified(nameof(CommentsOnSitePagesDisabled)))
            {
                props.CommentsOnSitePagesDisabled = CommentsOnSitePagesDisabled;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(DisableAppViews)))
            {
                props.DisableAppViews = DisableAppViews;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(DisableCompanyWideSharingLinks)))
            {
                props.DisableCompanyWideSharingLinks = DisableCompanyWideSharingLinks;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(DisableFlows)))
            {
                props.DisableFlows = DisableFlows;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(EnablePWA)))
            {
                props.PWAEnabled = EnablePWA ? PWAEnabledStatus.Enabled : PWAEnabledStatus.Disabled;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(OverrideTenantAnonymousLinkExpirationPolicy)))
            {
                props.OverrideTenantAnonymousLinkExpirationPolicy = OverrideTenantAnonymousLinkExpirationPolicy.ToBool();
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(AnonymousLinkExpirationInDays)) && AnonymousLinkExpirationInDays.HasValue)
            {
                props.AnonymousLinkExpirationInDays = AnonymousLinkExpirationInDays.Value;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(ConditionalAccessPolicy)) && ConditionalAccessPolicy == PnPConditionalAccessPolicyType.ProtectionLevel || ConditionalAccessPolicy == PnPConditionalAccessPolicyType.AuthenticationContext)
            {
                if (IsRootSite(Identity.Url))
                {
                    throw new PSInvalidOperationException("You cannot set the conditional access policy 'ProtectionLevel' on the root site.");
                }
                if (string.IsNullOrEmpty(ProtectionLevelName))
                {
                    props.AuthContextStrength = null;
                }
                else
                {
                    props.AuthContextStrength = ProtectionLevelName;
                }
                updateRequired = true;
            }
            else
            {
                if (ParameterSpecified(nameof(ProtectionLevelName)))
                {
                    throw new PSArgumentException("ConditionalAccessPolicy has to be set too when using this parameter.");
                }
                if (ParameterSpecified(nameof(ConditionalAccessPolicy)))
                {
                    props.AuthContextStrength = null;
                    if (ConditionalAccessPolicy == PnPConditionalAccessPolicyType.AllowFullAccess || ConditionalAccessPolicy == PnPConditionalAccessPolicyType.ProtectionLevel)
                    {
                        props.ConditionalAccessPolicy = SPOConditionalAccessPolicyType.AuthenticationContext;
                    }
                    else
                    {
                        props.ConditionalAccessPolicy = (SPOConditionalAccessPolicyType)Enum.Parse(typeof(SPOConditionalAccessPolicyType), ConditionalAccessPolicy.ToString());
                    }
                    updateRequired = true;
                }
            }

            if (ClientContext.ServerVersion >= new Version(16, 0, 8715, 1200)) // ServerSupportsIpLabelId2
            {
                if (ParameterSpecified(nameof(SensitivityLabel)))
                {
                    props.SensitivityLabel2 = SensitivityLabel;
                    updateRequired = true;
                }
                if (ParameterSpecified(nameof(RemoveLabel)))
                {
                    props.SensitivityLabel2 = null;
                    updateRequired = true;
                }
            }
            else
            {
                WriteWarning("Server does not support setting sensitity label");
            }

            if (ParameterSpecified(nameof(LimitedAccessFileType)))
            {
                if (ConditionalAccessPolicy == PnPConditionalAccessPolicyType.AllowLimitedAccess)
                {
                    props.LimitedAccessFileType = LimitedAccessFileType;
                    updateRequired = true;
                }
                else if (ShouldContinue("To set LimitedAccessFileType you need to set the -ConditionalAccessPolicy parameter to AllowLimitedAccess. We can set the Conditional Access Policy of this site to AllowLimitedAccess. Would you like to continue?", string.Empty))
                {
                    ConditionalAccessPolicy = PnPConditionalAccessPolicyType.AllowLimitedAccess;
                    props.ConditionalAccessPolicy = SPOConditionalAccessPolicyType.AllowLimitedAccess;
                    props.LimitedAccessFileType = LimitedAccessFileType;
                    updateRequired = true;
                }
            }

            if (ParameterSpecified(nameof(AllowEditing)))
            {
                if (ConditionalAccessPolicy == PnPConditionalAccessPolicyType.AllowLimitedAccess)
                {
                    props.AllowEditing = AllowEditing;
                }
                else if (ShouldContinue("To set AllowEditing you need to set the -ConditionalAccessPolicy parameter to AllowLimitedAccess. We can set the Conditional Access Policy of this site to AllowLimitedAccess. Would you like to continue?", string.Empty))
                {
                    ConditionalAccessPolicy = PnPConditionalAccessPolicyType.AllowLimitedAccess;
                    props.ConditionalAccessPolicy = SPOConditionalAccessPolicyType.AllowLimitedAccess;
                    props.AllowEditing = AllowEditing;
                }
            }

            if (ParameterSpecified(nameof(RestrictedToGeo)))
            {
                props.RestrictedToRegion = RestrictedToGeo;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(ExternalUserExpirationInDays)))
            {
                props.ExternalUserExpirationInDays = ExternalUserExpirationInDays;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(OverrideTenantExternalUserExpirationPolicy)))
            {
                props.OverrideTenantExternalUserExpirationPolicy = OverrideTenantExternalUserExpirationPolicy;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(AddInformationSegment)) && AddInformationSegment.Length > 0)
            {
                props.IBSegmentsToAdd = AddInformationSegment;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(RemoveInformationSegment)) && RemoveInformationSegment.Length > 0)
            {
                props.IBSegmentsToRemove = RemoveInformationSegment;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(BlockDownloadLinksFileType)))
            {
                props.BlockDownloadLinksFileType = BlockDownloadLinksFileType;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(OverrideBlockUserInfoVisibility)))
            {
                props.OverrideBlockUserInfoVisibility = OverrideBlockUserInfoVisibility;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(InformationBarriersMode)))
            {
                props.IBMode = InformationBarriersMode.ToString();
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(MediaTranscription)) && MediaTranscription.HasValue)
            {
                props.MediaTranscription = MediaTranscription.Value;
                updateRequired = true;
            }

            if (updateRequired)
            {
                var op = props.Update();
                ClientContext.Load(op, i => i.IsComplete, i => i.PollingInterval);
                ClientContext.ExecuteQueryRetry();

                if (Wait)
                {
                    WaitForIsComplete(ClientContext, op, timeoutFunction, TenantOperationMessage.SettingSiteProperties);
                }
            }

            if (ParameterSpecified(nameof(DisableSharingForNonOwners)))
            {
                var office365Tenant = new Office365Tenant(ClientContext);
                ClientContext.Load(office365Tenant);
                ClientContext.ExecuteQueryRetry();
                office365Tenant.DisableSharingForNonOwnersOfSite(Identity.Url);
            }

            if (ParameterSpecified(nameof(HubSiteId)))
            {
                var hubsiteProperties = Tenant.GetHubSitePropertiesById(HubSiteId);
                ClientContext.Load(hubsiteProperties);
                ClientContext.ExecuteQueryRetry();
                if (hubsiteProperties == null || string.IsNullOrEmpty(hubsiteProperties.SiteUrl))
                {
                    throw new PSArgumentException("Hubsite not found with the ID specified");
                }
                if (hubsiteProperties.ID != Guid.Empty)
                {
                    Tenant.ConnectSiteToHubSiteById(Identity.Url, hubsiteProperties.ID);
                }
                else
                {
                    Tenant.ConnectSiteToHubSite(Identity.Url, hubsiteProperties.SiteUrl);
                }
                ClientContext.ExecuteQueryRetry();
            }

            if(PrimarySiteCollectionAdmin != null)
            {
                using (var siteContext = Tenant.Context.Clone(Identity.Url))
                {
                    var spAdmin = PrimarySiteCollectionAdmin.GetUser(siteContext, true);                   
                    siteContext.Load(spAdmin);
                    siteContext.ExecuteQueryRetry();

                    siteContext.Site.Owner = spAdmin;
                    siteContext.ExecuteQueryRetry();
                }
            }

            if (Owners != null && Owners.Count > 0)
            {
                var admins = new List<UserEntity>();
                foreach (var owner in Owners)
                {
                    var userEntity = new UserEntity { LoginName = owner };
                    admins.Add(userEntity);
                }
                foreach (UserEntity admin in admins)
                {
                    try
                    {
                        Tenant.SetSiteAdmin(Identity.Url, admin.LoginName, true);
                        Tenant.Context.ExecuteQueryRetry();
                    }
                    catch (Exception)
                    {
                        using (var siteContext = Tenant.Context.Clone(Identity.Url))
                        {
                            var spAdmin = siteContext.Web.EnsureUser(admin.LoginName);
                            siteContext.Load(spAdmin);
                            siteContext.ExecuteQueryRetry();

                            Tenant.SetSiteAdmin(Identity.Url, spAdmin.LoginName, true);
                            Tenant.Context.ExecuteQueryRetry();
                        }
                    }
                }
            }
        }

        private SiteProperties GetSiteProperties(string url)
        {
            return Tenant.GetSitePropertiesByUrl(url, true);
        }

        private bool TimeoutFunction(TenantOperationMessage message)
        {
            if (message == TenantOperationMessage.SettingSiteProperties || message == TenantOperationMessage.SettingSiteLockState)
            {
                Host.UI.Write(".");
            }
            return Stopping;
        }

        private bool WaitForIsComplete(ClientContext context, SpoOperation op, Func<TenantOperationMessage, bool> timeoutFunction = null, TenantOperationMessage operationMessage = TenantOperationMessage.None)
        {
            bool succeeded = true;
            while (!op.IsComplete)
            {
                if (timeoutFunction != null && timeoutFunction(operationMessage))
                {
                    succeeded = false;
                    break;
                }
                Thread.Sleep(op.PollingInterval);

                op.RefreshLoad();
                if (!op.IsComplete)
                {
                    try
                    {
                        context.ExecuteQueryRetry();
                    }
                    catch (WebException)
                    {
                        // Context connection gets closed after action completed.
                        // Calling ExecuteQuery again returns an error which can be ignored
                    }
                }
            }
            return succeeded;
        }

        private bool IsRootSite(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var result))
            {
                return result.AbsolutePath.TrimEnd('/').Length == 0;
            }
            return false;
        }
    }
}