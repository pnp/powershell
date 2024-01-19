﻿using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate;
using PnP.PowerShell.Commands.Properties;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsLifecycle.Restart, "PnPFlowRun")]
    public class RestartFlowRun : PnPAzureManagementApiCmdlet
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
            string baseUrl = "https://api.flow.microsoft.com/";
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

            if (!Force && !ShouldContinue($"Restart flow run with name '{flowRunName}'?", Resources.Confirm))
                return;

            var triggers = GraphHelper.GetResultCollectionAsync<FlowRunTrigger>(Connection, baseUrl + $"providers/Microsoft.ProcessSimple/environments/{environmentName}/flows/{flowName}/triggers?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
            RestHelper.PostAsync(Connection.HttpClient, baseUrl  + $"providers/Microsoft.ProcessSimple/environments/{environmentName}/flows/{flowName}/triggers/{triggers.First().Name}/histories/{flowRunName}/resubmit?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
        }
    }
}
