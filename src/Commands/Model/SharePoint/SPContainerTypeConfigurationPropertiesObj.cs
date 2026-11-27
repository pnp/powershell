using Microsoft.Online.SharePoint.TenantAdministration;
using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public class SPContainerTypeConfigurationPropertiesObj
    {
        public Guid ContainerTypeId { get; private set; }

        public Guid OwningApplicationId { get; private set; }

        public string ContainerTypeName { get; private set; }

        public SPContainerTypeBillingClassification Classification { get; private set; }

        public bool? DiscoverabilityDisabled { get; private set; }

        public bool? SharingRestricted { get; private set; }

        public string ApplicationRedirectUrl { get; private set; }

        public Guid[] WhoCanShareAnonymousAllowList { get; private set; }

        public Guid[] WhoCanShareAuthenticatedGuestAllowList { get; private set; }

        public bool? OverrideTenantWhoCanShareAnonymousAllowList { get; private set; }

        public bool? OverrideTenantWhoCanShareAuthenticatedGuestAllowList { get; private set; }

        public IList<string> CopilotEmbeddedChatHosts { get; private set; }

        public int AnonymousLinkExpirationInDays { get; private set; }

        public bool? IsArchiveEnabled { get; private set; }

        public bool? UseLegacyItemWebUrl { get; private set; }

        internal SPContainerTypeConfigurationPropertiesObj(SPContainerTypeConfigurationProperties containerTypeConfigurationProperties)
        {
            ContainerTypeId = containerTypeConfigurationProperties.ContainerTypeId;
            OwningApplicationId = containerTypeConfigurationProperties.OwningAppId;
            ContainerTypeName = containerTypeConfigurationProperties.ContainerTypeName;
            Classification = containerTypeConfigurationProperties.Classification;
            DiscoverabilityDisabled = ToNullableBool(containerTypeConfigurationProperties.IsDiscoverablilityDisabled);
            SharingRestricted = ToNullableBool(containerTypeConfigurationProperties.IsSharingRestricted);
            ApplicationRedirectUrl = containerTypeConfigurationProperties.ApplicationRedirectUrl;
            WhoCanShareAnonymousAllowList = containerTypeConfigurationProperties.WhoCanShareAnonymousAllowList;
            WhoCanShareAuthenticatedGuestAllowList = containerTypeConfigurationProperties.WhoCanShareAuthenticatedGuestAllowList;
            OverrideTenantWhoCanShareAnonymousAllowList = ToNullableBool(containerTypeConfigurationProperties.OverrideTenantWhoCanShareAnonymousAllowList);
            OverrideTenantWhoCanShareAuthenticatedGuestAllowList = ToNullableBool(containerTypeConfigurationProperties.OverrideTenantWhoCanShareAuthenticatedGuestAllowList);
            CopilotEmbeddedChatHosts = containerTypeConfigurationProperties.CopilotEmbeddedChatHosts;
            AnonymousLinkExpirationInDays = containerTypeConfigurationProperties.AnonymousLinkExpirationInDays;
            IsArchiveEnabled = ToNullableBool(containerTypeConfigurationProperties.IsArchiveEnabled);
            UseLegacyItemWebUrl = ToNullableBool(containerTypeConfigurationProperties.ShouldUseLegacyItemWebUrl);
        }

        private static bool? ToNullableBool(NullableBoolean value)
        {
            return value switch
            {
                NullableBoolean.TRUE => true,
                NullableBoolean.FALSE => false,
                _ => null
            };
        }
    }
}
