using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPContainerApplication", SupportsShouldProcess = true)]
    public class SetContainerApplication : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public Guid OwningApplicationId { get; set; }

        [Parameter(Mandatory = false)]
        public bool OverrideTenantSharingCapability { get; set; }

        [Parameter(Mandatory = false)]
        public SharingCapabilities SharingCapability { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string[] CopilotEmbeddedChatHosts { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateRange(1, 50000)]
        public int ItemMajorVersionLimit { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (OwningApplicationId == Guid.Empty)
            {
                ThrowArgumentError($"-{nameof(OwningApplicationId)} cannot be an empty GUID.", nameof(OwningApplicationId));
            }

            if (!ParameterSpecified(nameof(OverrideTenantSharingCapability)) && !ParameterSpecified(nameof(SharingCapability)) && !ParameterSpecified(nameof(CopilotEmbeddedChatHosts)) && !ParameterSpecified(nameof(ItemMajorVersionLimit)))
            {
                ThrowArgumentError("Specify at least one setting to change.", null);
            }

            var overrideTenantSharingCapability = ParameterSpecified(nameof(OverrideTenantSharingCapability)) && OverrideTenantSharingCapability;
            if (ParameterSpecified(nameof(SharingCapability)) && !overrideTenantSharingCapability)
            {
                ThrowArgumentError($"-{nameof(SharingCapability)} can only be used together with -{nameof(OverrideTenantSharingCapability)} $true.", nameof(SharingCapability));
            }

            if (overrideTenantSharingCapability && !ParameterSpecified(nameof(SharingCapability)))
            {
                ThrowArgumentError($"-{nameof(OverrideTenantSharingCapability)} $true requires -{nameof(SharingCapability)}.", nameof(SharingCapability));
            }

            var applicationProperties = new SPSyntexApplicationProperties
            {
                OwningApplicationId = OwningApplicationId
            };

            if (ParameterSpecified(nameof(OverrideTenantSharingCapability)))
            {
                applicationProperties.OverrideTenantSharingCapabilityNullable = OverrideTenantSharingCapability ? NullableBoolean.TRUE : NullableBoolean.FALSE;
                if (OverrideTenantSharingCapability)
                {
                    applicationProperties.SharingCapability = SharingCapability;
                }
            }

            if (ParameterSpecified(nameof(CopilotEmbeddedChatHosts)))
            {
                applicationProperties.CopilotEmbeddedChatHosts = CopilotEmbeddedChatHosts.ToList();
            }

            if (ParameterSpecified(nameof(ItemMajorVersionLimit)))
            {
                applicationProperties.ItemMajorVersionLimit = ItemMajorVersionLimit;
            }

            if (!ShouldProcess(OwningApplicationId.ToString(), "Set SharePoint Embedded application settings"))
            {
                return;
            }

            Tenant.SetSPEmbeddedApplicationProperties(applicationProperties);
            AdminContext.ExecuteQueryRetry();
        }

        private void ThrowArgumentError(string message, string parameterName)
        {
            ThrowTerminatingError(new ErrorRecord(new PSArgumentException(message, parameterName), "InvalidContainerApplicationSettings", ErrorCategory.InvalidArgument, OwningApplicationId));
        }
    }
}
