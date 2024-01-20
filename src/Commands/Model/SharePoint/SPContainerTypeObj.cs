using Microsoft.Online.SharePoint.TenantAdministration;
using System;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public class SPContainerTypeObj
    {
        public Guid ContainerTypeId { get; private set; }

        public Guid OwningApplicationId { get; private set; }

        public string ContainerTypeName { get; private set; }

        public SPContainerTypeBillingClassification Classification { get; private set; }
        
        public string Region { get; private set; }

        public Guid AzureSubscriptionId { get; private set; }

        public string ResourceGroup { get; private set; }

        internal SPContainerTypeObj(SPContainerTypeProperties containerTypeConfigurationProperties)
        {
            ContainerTypeId = containerTypeConfigurationProperties.ContainerTypeId;
            OwningApplicationId = containerTypeConfigurationProperties.OwningAppId;
            ContainerTypeName = containerTypeConfigurationProperties.DisplayName;
            Region = containerTypeConfigurationProperties.Region;
            AzureSubscriptionId = containerTypeConfigurationProperties.AzureSubscriptionId;
            ResourceGroup = containerTypeConfigurationProperties.ResourceGroup;
            Classification = containerTypeConfigurationProperties.SPContainerTypeBillingClassification;
        }
    }
}
