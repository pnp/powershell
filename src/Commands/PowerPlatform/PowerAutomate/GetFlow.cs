using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPFlow", DefaultParameterSetName = ParameterSet_ALL)]
    public class GetFlow : PnPAzureManagementApiCmdlet
    {
        private const string ParameterSet_BYIDENTITY = "By Identity";
        private const string ParameterSet_ALL = "All";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYIDENTITY)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_ALL)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYIDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYIDENTITY)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public FlowSharingStatus SharingStatus = FlowSharingStatus.All;

        protected override void ExecuteCmdlet()
        {
            string environmentName = null;
            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
            if (ParameterSpecified(nameof(Environment)))
            {
                environmentName = Environment.GetName();

                WriteVerbose($"Using environment as provided '{environmentName}'");
            }
            else
            {
                var environments = GraphHelper.GetResultCollection<Model.PowerPlatform.Environment.Environment>(this, Connection,  baseUrl + "/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken);
                environmentName = environments.FirstOrDefault(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == true)?.Name;

                if(string.IsNullOrEmpty(environmentName))
                {
                    throw new Exception($"No default environment found, please pass in a specific environment name using the {nameof(Environment)} parameter");
                }

                WriteVerbose($"Using default environment as retrieved '{environmentName}'");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var flowName = Identity.GetName();

                WriteVerbose($"Retrieving specific Power Automate Flow with the provided name '{flowName}' within the environment '{environmentName}'");

                var result = GraphHelper.Get<Model.PowerPlatform.PowerAutomate.Flow>(this, Connection, baseUrl + $"/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}?api-version=2016-11-01", AccessToken);
                WriteObject(result, false);
            }
            else
            {
                string filter = null;
                switch (SharingStatus)
                {
                    case FlowSharingStatus.SharedWithMe:
                        filter = "search('team')";
                        break;

                    case FlowSharingStatus.Personal:
                        filter = "search('personal')";
                        break;

                    case FlowSharingStatus.All:
                        filter = "search('team AND personal')";
                        break;
                }

                WriteVerbose($"Retrieving all Power Automate Flows within environment '{environmentName}'{(filter != null ? $" with filter '{filter}'" : "")}");

                var flows = GraphHelper.GetResultCollection<Model.PowerPlatform.PowerAutomate.Flow>(this, Connection, baseUrl + $"/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows?api-version=2016-11-01{(filter != null ? $"&$filter={filter}" : "")}", AccessToken);
                WriteObject(flows, true);

            }
        }
    }
}
