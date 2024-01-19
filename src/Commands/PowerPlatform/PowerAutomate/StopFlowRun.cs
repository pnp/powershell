﻿using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.REST;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsLifecycle.Stop, "PnPFlowRun")]
    public class StopFlowRun : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Flow;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowRunPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

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

            var flowRunName = Identity.GetName();
            if (string.IsNullOrEmpty(flowRunName))
            {
                throw new PSArgumentException("Flow run not found.");
            }

            if (Force || ShouldContinue($"Stop flow run with name '{flowRunName}'?", Resources.Confirm))
            {
                RestHelper.PostAsync(Connection.HttpClient, $"https://api.flow.microsoft.com/providers/Microsoft.ProcessSimple/environments/{environmentName}/flows/{flowName}/runs/{flowRunName}/cancel?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
            }
        }
    }
}
