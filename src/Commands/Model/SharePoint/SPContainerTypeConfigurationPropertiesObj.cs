﻿using Microsoft.Online.SharePoint.TenantAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public class SPContainerTypeConfigurationPropertiesObj
    {
        public Guid ContainerTypeId { get; private set; }

        public Guid OwningApplicationId { get; private set; }

        public string ContainerTypeName { get; private set; }

        public SPContainerTypeBillingClassification Classification { get; private set; }

        public bool DiscoverabilityDisabled { get; private set; }

        public bool SharingRestricted { get; private set; }

        internal SPContainerTypeConfigurationPropertiesObj(SPContainerTypeConfigurationProperties containerTypeConfigurationProperties)
        {
            ContainerTypeId = containerTypeConfigurationProperties.ContainerTypeId;
            OwningApplicationId = containerTypeConfigurationProperties.OwningAppId;
            ContainerTypeName = containerTypeConfigurationProperties.ContainerTypeName;
            Classification = containerTypeConfigurationProperties.Classification;
            DiscoverabilityDisabled = containerTypeConfigurationProperties.IsDiscoverablilityDisabled;
            SharingRestricted = containerTypeConfigurationProperties.IsSharingRestricted;
        }
    }
}
