﻿using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPFlowRun")]
    public class GetFlowRun : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Flow;

        [Parameter(Mandatory = false)]
        public PowerAutomateFlowRunPipeBind Identity;

        protected override void ExecuteCmdlet()
        {

            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
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
                var flowRun = ArmRequestHelper.Get<FlowRun>($"{baseUrl}/providers/Microsoft.ProcessSimple/environments/{environmentName}/flows/{flowName}/runs/{flowRunName}?api-version=2016-11-01");
                WriteObject(flowRun, false);
            }
            else
            {
                var flowRuns = ArmRequestHelper.GetResultCollection<FlowRun>($"{baseUrl}/providers/Microsoft.ProcessSimple/environments/{environmentName}/flows/{flowName}/runs?api-version=2016-11-01");
                WriteObject(flowRuns, true);
            }
        }
    }
}
