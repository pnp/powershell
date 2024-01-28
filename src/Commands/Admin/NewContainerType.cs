using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.New, "PnPContainerType")]
    public class NewContainerType : PnPAdminCmdlet,IDynamicParameters
    {
        private const string ParameterSet_Standard = "Standard";
        [Parameter(Mandatory = true)]
        public string ContainerTypeName;

        [Parameter(Mandatory = true)]
        public Guid OwningApplicationId;

        [Parameter(Mandatory = false)]
        public SwitchParameter TrialContainerType;
        private StandardContainerParameters _standardContainerParameters;
        public object GetDynamicParameters()
        {
            if(!ParameterSpecified(nameof(TrialContainerType)))
            {
                _standardContainerParameters = new StandardContainerParameters();
                return _standardContainerParameters;
            }
            return null;        
        }

        protected override void ExecuteCmdlet()
        {
            SPContainerTypeProperties sPContainerTypeProperties = new SPContainerTypeProperties();
            sPContainerTypeProperties.DisplayName = ContainerTypeName;
            sPContainerTypeProperties.OwningAppId = OwningApplicationId;
            
            sPContainerTypeProperties.SPContainerTypeBillingClassification = TrialContainerType ? SPContainerTypeBillingClassification.Trial : SPContainerTypeBillingClassification.Standard;
            if(!ParameterSpecified(nameof(TrialContainerType)))
            {
                sPContainerTypeProperties.AzureSubscriptionId = _standardContainerParameters.AzureSubscriptionId;
                sPContainerTypeProperties.ResourceGroup = _standardContainerParameters.ResourceGroup;
                sPContainerTypeProperties.Region = _standardContainerParameters.Region;
            }
            ClientResult<SPContainerTypeProperties> sPOContainerTypeId = Tenant.NewSPOContainerType(sPContainerTypeProperties);
            AdminContext.ExecuteQueryRetry();
            if (sPOContainerTypeId != null && sPOContainerTypeId.Value != null)
            {
                WriteObject(new Model.SharePoint.SPContainerTypeObj(sPOContainerTypeId.Value));
            }
        }

        public class StandardContainerParameters
        {
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Standard)]
            public Guid AzureSubscriptionId;

            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Standard)]
            public string ResourceGroup;

            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Standard)]
            public string Region;
        }
    }
}