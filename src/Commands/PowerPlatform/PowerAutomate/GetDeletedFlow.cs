using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPDeletedFlow", DefaultParameterSetName = ParameterSet_ALL)]
    public class GetDeletedFlow : PnPAzureManagementApiCmdlet
    {
        private const string ParameterSet_ALL = "All";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_ALL)]
        public PowerPlatformEnvironmentPipeBind Environment;

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


            WriteVerbose($"Retrieving all Power Automate Flows within environment '{environmentName}'");

            var flowUrl = $"{baseUrl}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environmentName}/v2/flows?api-version=2016-11-01&include=softDeletedFlows";
            var results = GraphHelper.GetResultCollection<Model.PowerPlatform.PowerAutomate.Flow>(this, Connection, flowUrl, AccessToken);

            var deletedFlowList = results
                .Where(flow => flow.Properties.StateRaw == "Deleted")
                .Select(flow => new Model.PowerPlatform.PowerAutomate.DeletedFlow(
                    flow.Name,
                    flow.Properties.DisplayName,
                    flow.Properties.LastModifiedTime
                ))
                .ToList();
            WriteObject(deletedFlowList, true);

        }
    }
}
