﻿using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.PowerPlatform.Environment
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerPlatformCustomConnector")]
    public class GetPowerPlatformCustomConnector : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public PowerPlatformConnectorPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(this, Connection, Connection.AzureEnvironment, AccessToken)?.Name;
            var powerAppsUrl = PowerPlatformUtility.GetPowerAppsEndpoint(Connection.AzureEnvironment);

            if (ParameterSpecified(nameof(Identity)))
            {
                var appName = Identity.GetName();

                WriteVerbose($"Retrieving specific Custom Connector with the provided name '{appName}' within the environment '{environmentName}'");

                var result = GraphHelper.Get<Model.PowerPlatform.Environment.PowerPlatformConnector>(this, Connection, $"{powerAppsUrl}/providers/Microsoft.PowerApps{(AsAdmin ? "/scopes/admin/environments/" + environmentName : "")}/apis/{appName}?api-version=2016-11-01&$filter=environment eq '{environmentName}' and isCustomApi eq 'True'", PowerAppsServiceAccessToken);
                WriteObject(result, false);
            }
            else
            {
                WriteVerbose($"Retrieving all Connectors within environment '{environmentName}'");

                var connectors = GraphHelper.GetResultCollection<Model.PowerPlatform.Environment.PowerPlatformConnector>(this, Connection, $"{powerAppsUrl}/providers/Microsoft.PowerApps/apis?api-version=2016-11-01&$filter=environment eq '{environmentName}' and isCustomApi eq 'True'", PowerAppsServiceAccessToken);
                WriteObject(connectors, true);
            }
        }
    }
}