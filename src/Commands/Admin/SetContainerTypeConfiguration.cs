using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPContainerTypeConfiguration", SupportsShouldProcess = true)]
    [OutputType(typeof(Model.SharePoint.SPContainerTypeConfigurationPropertiesObj))]
    public class SetContainerTypeConfiguration : PnPSharePointOnlineAdminCmdlet
    {
        private const int MaxWhoCanShareAllowListSize = 12;

        [Parameter(Mandatory = true, Position = 0)]
        [Alias("ContainerTypeId")]
        public Guid Identity { get; set; }

        [Parameter(Mandatory = false)]
        public bool DiscoverabilityDisabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool SharingRestricted { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string ApplicationRedirectUrl { get; set; }

        [Parameter(Mandatory = false)]
        public bool OverrideTenantWhoCanShareAnonymousAllowList { get; set; }

        [Parameter(Mandatory = false)]
        public Guid[] WhoCanShareAnonymousAllowList { get; set; }

        [Parameter(Mandatory = false)]
        public bool OverrideTenantWhoCanShareAuthenticatedGuestAllowList { get; set; }

        [Parameter(Mandatory = false)]
        public Guid[] WhoCanShareAuthenticatedGuestAllowList { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string[] CopilotEmbeddedChatHosts { get; set; }

        [Parameter(Mandatory = false)]
        public int AnonymousLinkExpirationInDays { get; set; }

        [Parameter(Mandatory = false)]
        public bool IsArchiveEnabled { get; set; }

        [Parameter(Mandatory = false)]
        public bool UseLegacyItemWebUrl { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (Identity == Guid.Empty)
            {
                ThrowArgumentError($"-{nameof(Identity)} cannot be an empty GUID.", nameof(Identity));
            }

            string[] settings =
            [
                nameof(DiscoverabilityDisabled), nameof(SharingRestricted), nameof(ApplicationRedirectUrl),
                nameof(OverrideTenantWhoCanShareAnonymousAllowList), nameof(WhoCanShareAnonymousAllowList),
                nameof(OverrideTenantWhoCanShareAuthenticatedGuestAllowList), nameof(WhoCanShareAuthenticatedGuestAllowList),
                nameof(CopilotEmbeddedChatHosts), nameof(AnonymousLinkExpirationInDays), nameof(IsArchiveEnabled), nameof(UseLegacyItemWebUrl)
            ];
            if (!settings.Any(ParameterSpecified))
            {
                ThrowArgumentError("Specify at least one setting to change.", null);
            }

            if (ParameterSpecified(nameof(AnonymousLinkExpirationInDays)) && AnonymousLinkExpirationInDays != -1 && (AnonymousLinkExpirationInDays < 1 || AnonymousLinkExpirationInDays > 730))
            {
                ThrowArgumentError($"-{nameof(AnonymousLinkExpirationInDays)} must be between 1 and 730, or -1 to follow the tenant level setting.", nameof(AnonymousLinkExpirationInDays));
            }

            var configuration = new SPContainerTypeConfigurationProperties
            {
                ContainerTypeId = Identity
            };

            if (ParameterSpecified(nameof(DiscoverabilityDisabled)))
            {
                configuration.IsDiscoverablilityDisabled = ToNullableBoolean(DiscoverabilityDisabled);
            }

            if (ParameterSpecified(nameof(SharingRestricted)))
            {
                configuration.IsSharingRestricted = ToNullableBoolean(SharingRestricted);
            }

            if (ParameterSpecified(nameof(ApplicationRedirectUrl)))
            {
                configuration.ApplicationRedirectUrl = ApplicationRedirectUrl;
            }

            if (ParameterSpecified(nameof(CopilotEmbeddedChatHosts)))
            {
                configuration.CopilotEmbeddedChatHosts = CopilotEmbeddedChatHosts.ToList();
            }

            ApplyWhoCanShareAllowList(nameof(OverrideTenantWhoCanShareAnonymousAllowList), OverrideTenantWhoCanShareAnonymousAllowList, nameof(WhoCanShareAnonymousAllowList), WhoCanShareAnonymousAllowList,
                (overrideTenant, allowList) =>
                {
                    configuration.OverrideTenantWhoCanShareAnonymousAllowList = overrideTenant;
                    configuration.WhoCanShareAnonymousAllowList = allowList;
                });

            ApplyWhoCanShareAllowList(nameof(OverrideTenantWhoCanShareAuthenticatedGuestAllowList), OverrideTenantWhoCanShareAuthenticatedGuestAllowList, nameof(WhoCanShareAuthenticatedGuestAllowList), WhoCanShareAuthenticatedGuestAllowList,
                (overrideTenant, allowList) =>
                {
                    configuration.OverrideTenantWhoCanShareAuthenticatedGuestAllowList = overrideTenant;
                    configuration.WhoCanShareAuthenticatedGuestAllowList = allowList;
                });

            if (ParameterSpecified(nameof(AnonymousLinkExpirationInDays)))
            {
                configuration.AnonymousLinkExpirationInDays = AnonymousLinkExpirationInDays;
            }

            if (ParameterSpecified(nameof(IsArchiveEnabled)))
            {
                configuration.IsArchiveEnabled = ToNullableBoolean(IsArchiveEnabled);
            }

            if (ParameterSpecified(nameof(UseLegacyItemWebUrl)))
            {
                configuration.ShouldUseLegacyItemWebUrl = ToNullableBoolean(UseLegacyItemWebUrl);
            }

            if (!ShouldProcess(Identity.ToString(), "Set container type configuration"))
            {
                return;
            }

            var result = Tenant.SetSPOContainerTypeConfiguration(configuration);
            AdminContext.ExecuteQueryRetry();

            if (result?.Value != null)
            {
                WriteObject(new Model.SharePoint.SPContainerTypeConfigurationPropertiesObj(result.Value));
            }
        }

        // An allow list is only sent when the tenant level allow list is overridden. Overriding with no list, or $null, removes the restriction.
        private void ApplyWhoCanShareAllowList(string overrideParameterName, bool overrideTenant, string allowListParameterName, Guid[] allowList, Action<NullableBoolean, Guid[]> apply)
        {
            if (!ParameterSpecified(overrideParameterName))
            {
                if (ParameterSpecified(allowListParameterName))
                {
                    ThrowArgumentError($"-{allowListParameterName} can only be used together with -{overrideParameterName} $true.", allowListParameterName);
                }
                return;
            }

            if (!overrideTenant)
            {
                if (ParameterSpecified(allowListParameterName))
                {
                    ThrowArgumentError($"-{allowListParameterName} can only be used together with -{overrideParameterName} $true.", allowListParameterName);
                }
                apply(NullableBoolean.FALSE, null);
                return;
            }

            if (allowList != null && allowList.Length == 0)
            {
                ThrowArgumentError($"-{allowListParameterName} cannot be an empty list. Use $null to remove the restriction.", allowListParameterName);
            }

            if (allowList != null && allowList.Length > MaxWhoCanShareAllowListSize)
            {
                ThrowArgumentError($"-{allowListParameterName} can contain at most {MaxWhoCanShareAllowListSize} security groups.", allowListParameterName);
            }
            apply(NullableBoolean.TRUE, allowList);
        }

        private void ThrowArgumentError(string message, string parameterName)
        {
            ThrowTerminatingError(new ErrorRecord(new PSArgumentException(message, parameterName), "InvalidContainerTypeConfiguration", ErrorCategory.InvalidArgument, Identity));
        }

        private static NullableBoolean ToNullableBoolean(bool value)
        {
            return value ? NullableBoolean.TRUE : NullableBoolean.FALSE;
        }
    }
}
