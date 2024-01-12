﻿using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Linq;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.PowerPlatform.Environment
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerPlatformConnector")]
    public class GetPowerPlatformConnector : PnPAzureManagementApiCmdlet
    {
      
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public PowerPlatformConnectorPipeBind Identity; 

        protected override void ExecuteCmdlet()
        {
            string environmentName = null;
            string baseUrl = "https://api.flow.microsoft.com/";
            if (ParameterSpecified(nameof(Environment)))
            {
                environmentName = Environment.GetName();
                WriteVerbose($"Using environment as provided '{environmentName}'");
            }
            else
            {
                var environments = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.Environment.Environment>(Connection, baseUrl + "/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                environmentName = environments.FirstOrDefault(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == true)?.Name;

                if (string.IsNullOrEmpty(environmentName))
                {
                    throw new Exception($"No default environment found, please pass in a specific environment name using the {nameof(Environment)} parameter");
                }

                WriteVerbose($"Using default environment as retrieved '{environmentName}'");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var appName = Identity.GetName();

                WriteVerbose($"Retrieving specific Custom Connector with the provided name '{appName}' within the environment '{environmentName}'");

                var result = GraphHelper.GetAsync<Model.PowerPlatform.Environment.PowerPlatformConnector>(Connection, $"https://api.powerapps.com/providers/Microsoft.PowerApps{(AsAdmin ? "/scopes/admin/environments/" + environmentName : "")}/apis/{appName}?api-version=2016-11-01&$filter=environment eq '{environmentName}' and isCustomApi eq 'True'", AccessToken).GetAwaiter().GetResult();
                WriteObject(result, false);
            }
            else
            {
                WriteVerbose($"Retrieving all Connectors within environment '{environmentName}'");

                var connectors = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.Environment.PowerPlatformConnector>(Connection, $"https://api.powerapps.com/providers/Microsoft.PowerApps/apis?api-version=2016-11-01&$filter=environment eq '{environmentName}' and isCustomApi eq 'True'", AccessToken).GetAwaiter().GetResult();
                WriteObject(connectors, true);
            }
        }
    }
}