using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public class SPConsumingTenantContainerByIdentity
    {

        public string ContainerId { get; private set; }

        public string ContainerName { get; private set; }

        public string Description { get; private set; }

        public Guid OwningApplicationId { get; private set; }

        public string OwningApplicationName { get; private set; }

        public string ContainerApiUrl { get; private set; }

        public string ContainerSiteUrl { get; private set; }

        public string SensitivityLabel { get; private set; }

        public IList<string> Owners { get; private set; }

        public IList<string> Managers { get; private set; }

        public IList<string> Readers { get; private set; }

        public IList<string> Writers { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public long StorageUsedInBytes { get; private set; }

        public SPOConditionalAccessPolicyType ConditionalAccessPolicy { get; private set; }

        public bool AllowEditing { get; private set; }

        public SPOLimitedAccessFileType LimitedAccessFileType { get; private set; }

        public bool ReadOnlyForUnmanagedDevices { get; private set; }

        public string AuthenticationContextName { get; private set; }

        public bool BlockDownloadPolicy { get; private set; }

        public bool ReadOnlyForBlockDownloadPolicy { get; private set; }

        public SharingDomainRestrictionModes SharingDomainRestrictionMode { get; private set; }
        public string SharingAllowedDomainList { get; private set; }
        public string SharingBlockedDomainList { get; private set; }
        public string Status { get; private set; }
        public int OwnersCount { get; private set; }
        public long StorageUsed { get; private set; }

        internal SPConsumingTenantContainerByIdentity(SPContainerProperties spContainerProperties)
        {
            ContainerId = spContainerProperties.ContainerId;
            ContainerName = spContainerProperties.ContainerName;
            CreatedOn = spContainerProperties.CreatedOn;
            Status = spContainerProperties.Status;
            SensitivityLabel = spContainerProperties.SensitivityLabel;
            OwnersCount = spContainerProperties.OwnersCount;
            _ = spContainerProperties.StorageUsed;
            int digits = 2;
            double value = BytesToGB(spContainerProperties.StorageUsed);
            value = Math.Round(value, digits);
            StorageUsed = spContainerProperties.StorageUsed;
        }

        private static double BytesToGB(long value)
        {
            double num = 1073741824.0;
            return (double)value / num;
        }
    }
}
