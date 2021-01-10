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

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantSite")]
    public class SetTenantSite : PnPAdminCmdlet
    {
        private const string ParameterSet_LOCKSTATE = "Set Lock State";
        private const string ParameterSet_PROPERTIES = "Set Properties";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public string Title;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public uint LocaleId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter AllowSelfServiceUpgrade;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public List<string> Owners;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter DenyAddAndCustomizePages;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SharingCapabilities SharingCapability;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public long StorageMaximumLevel;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public long StorageWarningLevel;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LOCKSTATE)]
        public SiteLockState? LockState;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SharingPermissionType DefaultLinkPermission;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SharingLinkType DefaultSharingLinkType;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public string SharingAllowedDomainList;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public string SharingBlockedDomainList;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
        public SwitchParameter BlockDownloadOfNonViewableFiles;

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

        [Parameter(Mandatory = false)]
        public SwitchParameter Wait;
        protected override void ExecuteCmdlet()
        {
            Func<TenantOperationMessage, bool> timeoutFunction = TimeoutFunction;

            if (LockState.HasValue)
            {
                Tenant.SetSiteLockState(Url, LockState.Value, Wait, Wait ? timeoutFunction : null);
                WriteWarning("You changed the lockstate of a site. This change is not guaranteed to be effective immediately. Please wait a few minutes for this to take effect.");
            }
            if (!LockState.HasValue)
            {
                SetSiteProperties(timeoutFunction);
            }
        }

        private void SetSiteProperties(Func<TenantOperationMessage, bool> timeoutFunction)
        {
            var props = GetSiteProperties(Url);
            var updateRequired = false;

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
            if (ParameterSpecified(nameof(StorageMaximumLevel)))
            {
                props.StorageMaximumLevel = StorageMaximumLevel;
                updateRequired = true;
            }
            if (ParameterSpecified(nameof(StorageWarningLevel)))
            {
                props.StorageWarningLevel = StorageWarningLevel;
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
            if (ParameterSpecified(nameof(DefaultSharingLinkType)))
            {
                props.DefaultSharingLinkType = DefaultSharingLinkType;
                updateRequired = true;
            }

            if (ParameterSpecified(nameof(BlockDownloadOfNonViewableFiles)))
            {
                props.AllowDownloadingNonWebViewableFiles = !BlockDownloadOfNonViewableFiles;
                updateRequired = true;
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

            if (Owners != null && Owners.Count > 0)
            {
                var admins = new List<UserEntity>();
                foreach (var owner in Owners)
                {
                    var userEntity = new UserEntity { LoginName = owner };
                    admins.Add(userEntity);
                }
                Tenant.AddAdministrators(admins, new Uri(Url));
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
    }
}