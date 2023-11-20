using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Linq;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.PowerPlatform.Environment
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerPlatformCustomConnector")]
    public class GetPowerPlatformCustomConnector : PnPAzureManagementApiCmdlet
    {
      
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public PowerPlatformCustomConnectorPipeBind Identity; 

        protected override void ExecuteCmdlet()
        {
            string environmentName = null;
            if (ParameterSpecified(nameof(Environment)))
            {
                environmentName = Environment.GetName();
                WriteVerbose($"Using environment as provided '{environmentName}'");
            }
            else
            {
                var environments = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.Environment.Environment>(Connection, "https://management.azure.com/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                environmentName = environments.FirstOrDefault(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == true)?.Name;

                if (string.IsNullOrEmpty(environmentName))
                {
                    throw new Exception($"No default environment found, please pass in a specific environment name using the {nameof(Environment)} parameter");
                }

                WriteVerbose($"Using default environment as retrieved '{environmentName}'");
            }
            var apiURL = $"https://api.powerapps.com/providers/Microsoft.PowerApps/apis?api-version=2016-11-01&$filter=environment eq '{environmentName}' and isCustomApi eq 'True'";
            if (ParameterSpecified(nameof(Identity)))
            {
                var appName = Identity.GetName();

                WriteVerbose($"Retrieving specific Custom Connector with the provided name '{appName}' within the environment '{environmentName}'");

                var connectors = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.Environment.PowerPlatformConnector>(Connection, apiURL , AccessToken).GetAwaiter().GetResult();
                var connector = connectors.FirstOrDefault(e => e.Properties.displayName == appName);
                WriteObject(connector, false);
            }
            else
            {
                WriteVerbose($"Retrieving all Connectors within environment '{environmentName}'");

                var connectors = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.Environment.PowerPlatformConnector>(Connection, apiURL, AccessToken).GetAwaiter().GetResult();
                WriteObject(connectors, true);
            }
        }
    }
}