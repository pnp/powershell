using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.New, "PnPContainerType", SupportsShouldProcess = true)]
    [OutputType(typeof(Model.SharePoint.SPContainerTypeObj))]
    public class NewContainerType : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string ContainerTypeName;

        [Parameter(Mandatory = true)]
        public Guid OwningApplicationId;

        [Parameter(Mandatory = false)]
        public SwitchParameter TrialContainerType;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsPassThroughBilling;

        [Parameter(Mandatory = false)]
        public string ApplicationRedirectUrl;

        [Parameter(Mandatory = false)]
        public bool IsGovernableByAdmin;

        [Parameter(Mandatory = false)]
        public bool IsArchiveEnabled;

        [Obsolete("Billing is no longer set up when creating a container type. Set up billing for a standard container type separately, for example with Add-SPOContainerTypeBilling in the SharePoint Online Management Shell.")]
        [Parameter(Mandatory = false)]
        public Guid? AzureSubscriptionId;

        [Obsolete("Billing is no longer set up when creating a container type. Set up billing for a standard container type separately, for example with Add-SPOContainerTypeBilling in the SharePoint Online Management Shell.")]
        [Parameter(Mandatory = false)]
        public string ResourceGroup;

        [Obsolete("Billing is no longer set up when creating a container type. Set up billing for a standard container type separately, for example with Add-SPOContainerTypeBilling in the SharePoint Online Management Shell.")]
        [Parameter(Mandatory = false)]
        public string Region;

        protected override void ExecuteCmdlet()
        {
            if (TrialContainerType && IsPassThroughBilling)
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException($"-{nameof(TrialContainerType)} and -{nameof(IsPassThroughBilling)} cannot be combined, as a trial container type has no billing."), "TrialWithPassThroughBilling", ErrorCategory.InvalidArgument, null));
            }

            var billingClassification = TrialContainerType ? SPContainerTypeBillingClassification.Trial
                : IsPassThroughBilling ? SPContainerTypeBillingClassification.DirectToCustomer
                : SPContainerTypeBillingClassification.Standard;

            if (!ShouldProcess(ContainerTypeName, $"Create {billingClassification} container type"))
            {
                return;
            }

            var containerTypeProperties = new SPContainerTypeProperties
            {
                DisplayName = ContainerTypeName,
                OwningAppId = OwningApplicationId,
                SPContainerTypeBillingClassification = billingClassification,
                ApplicationRedirectUrl = ApplicationRedirectUrl
            };

            if (ParameterSpecified(nameof(IsGovernableByAdmin)))
            {
                containerTypeProperties.IsGovernableByAdminNullable = IsGovernableByAdmin ? NullableBoolean.TRUE : NullableBoolean.FALSE;
            }

            if (ParameterSpecified(nameof(IsArchiveEnabled)))
            {
                containerTypeProperties.IsArchiveEnabled = IsArchiveEnabled ? NullableBoolean.TRUE : NullableBoolean.FALSE;
            }

            //
            // NOTICE: The SharePoint API being used in this code is of temporary nature.
            //         It will be replaced by Microsoft Graph in due time.
            //         This SharePoint API should not be called directly or implemented into your own tools or software.
            //         When the Microsoft Graph alternative becomes available, this PnP cmdlet will be rewritten to use it instead.
            //         So when using this PnP PowerShell cmdlet, the goal is to seemlessly transition to the new API.
            //         When you would use it in your own code directly, it will stop working at some point in time without prior announcement.
            //

            LogDebug($"Creating a {billingClassification} container type");
            var sPOContainerTypeId = Tenant.NewSPOContainerType(containerTypeProperties);
            AdminContext.ExecuteQueryRetry();

            if (sPOContainerTypeId?.Value == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException($"Container type '{ContainerTypeName}' was not created, as the server returned no container type."), "ContainerTypeNotCreated", ErrorCategory.InvalidResult, ContainerTypeName));
            }
            WriteObject(new Model.SharePoint.SPContainerTypeObj(sPOContainerTypeId.Value));
        }
    }
}
