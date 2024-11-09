using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPDeletedFlow", DefaultParameterSetName = ParameterSet_ALL)]
    [RequiredApiDelegatedPermissions("azure/user_impersonation")]
    public class GetDeletedFlow : PnPAzureManagementApiCmdlet
    {
        private const string ParameterSet_ALL = "All";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_ALL)]
        public PowerPlatformEnvironmentPipeBind Environment;

        protected override void ExecuteCmdlet()
        {
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(this, Connection, Connection.AzureEnvironment, AccessToken)?.Name;
            var baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);

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
