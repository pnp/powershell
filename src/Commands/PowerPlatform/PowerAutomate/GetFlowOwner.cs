using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPFlowOwner")]
    public class GetFlowOwner : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        protected override void ExecuteCmdlet()
        {
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
            if (string.IsNullOrEmpty(environmentName))
            {
                throw new PSArgumentException("Environment not found.", nameof(Environment));
            }

            var flowName = Identity.GetName();
            if (string.IsNullOrEmpty(flowName))
            {
                throw new PSArgumentException("Flow not found.", nameof(Identity));
            }

            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
            var flowOwners = ArmRequestHelper.GetResultCollection<FlowPermission>($"{baseUrl}/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}/permissions?api-version=2016-11-01");
            WriteObject(flowOwners, true);
        }
    }
}