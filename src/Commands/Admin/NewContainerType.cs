using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.New, "PnPContainerType", DefaultParameterSetName = ParameterSet_Trial)]
    public class NewContainerType : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSet_Trial = "Trial";
        private const string ParameterSet_Standard = "Standard";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Trial)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Standard)]
        public string ContainerTypeName;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Trial)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Standard)]
        public Guid OwningApplicationId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Trial)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Standard)]
        public SwitchParameter TrialContainerType;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Standard)]
        public Guid? AzureSubscriptionId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Standard)]
        public string ResourceGroup;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Standard)]
        public string Region;

        protected override void ExecuteCmdlet()
        {
            // Ensure when creating a standard container type that the required parameters are provided
            if(ParameterSpecified(nameof(TrialContainerType)) && !TrialContainerType.ToBool() && (!AzureSubscriptionId.HasValue || string.IsNullOrWhiteSpace(ResourceGroup) || string.IsNullOrWhiteSpace(Region)))
            {
                throw new PSArgumentException($"{nameof(AzureSubscriptionId)}, {nameof(ResourceGroup)} and {nameof(Region)} are required when creating a standard container type");
            }

            SPContainerTypeProperties sPContainerTypeProperties;
            if (!ParameterSpecified(nameof(TrialContainerType)) || !TrialContainerType.ToBool())
            {
                WriteWarning($"Creation of standard container types is not yet supported. This will be enabled in the future. For now, only trial container types can be created by adding the -{nameof(TrialContainerType)} parameter.");
                return;

                // NOTICE:
                // Currently disabled by request of the product group as it doesn't work reliably yet
                // Once the official endpoint to create standard container types is available, this code can be enabled again and will point to the proper API to use

                // WriteVerbose("Creating a standard container type");

                // sPContainerTypeProperties = new SPContainerTypeProperties
                // {
                //     DisplayName = ContainerTypeName,
                //     OwningAppId = OwningApplicationId,
                //     AzureSubscriptionId = AzureSubscriptionId.Value,
                //     ResourceGroup = ResourceGroup,
                //     Region = Region,
                //     SPContainerTypeBillingClassification = SPContainerTypeBillingClassification.Standard
                // };
            }
            else
            {
                WriteVerbose("Creating a trial container type");

                sPContainerTypeProperties = new SPContainerTypeProperties
                {
                    DisplayName = ContainerTypeName,
                    OwningAppId = OwningApplicationId,
                    SPContainerTypeBillingClassification = SPContainerTypeBillingClassification.Trial
                };
            }

            //
            // NOTICE: The SharePoint API being used in this code is of temporary nature.
            //         It will be replaced by Microsoft Graph in due time. 
            //         This SharePoint API should not be called directly or implemented into your own tools or software.
            //         When the Microsoft Graph alternative becomes available, this PnP cmdlet will be rewritten to use it instead.
            //         So when using this PnP PowerShell cmdlet, the goal is to seemlessly transition to the new API.
            //         When you would use it in your own code directly, it will stop working at some point in time without prior announcement.
            //

            var sPOContainerTypeId = Tenant.NewSPOContainerType(sPContainerTypeProperties);
            AdminContext.ExecuteQueryRetry();

            if (sPOContainerTypeId != null && sPOContainerTypeId.Value != null)
            {
                WriteObject(new Model.SharePoint.SPContainerTypeObj(sPOContainerTypeId.Value));
            }
            else
            {
                WriteVerbose("Failed to create container type");
            }
        }
    }
}