using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;
using System.Linq;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.PowerPlatform.Environment
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerPlatformEnvironment", DefaultParameterSetName = ParameterSet_DEFAULT)]
    public class GetPowerPlatformEnvironment : PnPAzureManagementApiCmdlet
    {
        private const string ParameterSet_BYIDENTITY = "By Identity";
        private const string ParameterSet_DEFAULT = "Default";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        public SwitchParameter IsDefault;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYIDENTITY)]
        public PowerPlatformEnvironmentPipeBind Identity;        

        protected override void ExecuteCmdlet()
        {
            
            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
            var environments = ArmRequestHelper.GetResultCollection<Model.PowerPlatform.Environment.Environment>(baseUrl + "/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01");

            if(ParameterSpecified(nameof(IsDefault)) && IsDefault.ToBool())
            {
                environments = environments.Where(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == IsDefault.ToBool());
            }

            if(ParameterSpecified(nameof(Identity)))
            {
                var environmentName = Identity.GetName();
                environments = environments.Where(e => e.Properties.DisplayName == environmentName || e.Name == environmentName);
            }

            WriteObject(environments, true);
        }
    }
}