using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPFlowRun")]
    public class GetFlowRun : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Flow;

        [Parameter(Mandatory = false)]
        public PowerAutomateFlowRunPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var environmentName = Environment.GetName();
            if (string.IsNullOrEmpty(environmentName))
            {
                throw new PSArgumentException("Environment not found.");
            }

            var flowName = Flow.GetName();
            if (string.IsNullOrEmpty(flowName))
            {
                throw new PSArgumentException("Flow not found.");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var flowRunName = Identity.GetName();
                var flowRun = GraphHelper.GetAsync<FlowRun>(Connection, $"https://management.azure.com/providers/Microsoft.ProcessSimple/environments/{environmentName}/flows/{flowName}/runs/{flowRunName}?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                WriteObject(flowRun, false);
            }
            else
            {
                var flowRuns = GraphHelper.GetResultCollectionAsync<FlowRun>(Connection, $"https://management.azure.com/providers/Microsoft.ProcessSimple/environments/{environmentName}/flows/{flowName}/runs?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                WriteObject(flowRuns, true);
            }
        }
    }
}
