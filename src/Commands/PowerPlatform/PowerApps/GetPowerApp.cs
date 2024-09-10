using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerApp")]
    [RequiredMinimalApiPermissions("https://management.azure.com/user_impersonation", "https://service.powerapps.com/user")]
    [OutputType(typeof(Model.PowerPlatform.PowerApp.PowerApp))]
    public class GetPowerApp : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public PowerAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            string environmentName = null;
            string powerAppsUrl = PowerPlatformUtility.GetPowerAppsEndpoint(Connection.AzureEnvironment);
            if (ParameterSpecified(nameof(Environment)))
            {
                environmentName = Environment.GetName();

                WriteVerbose($"Using environment as provided '{environmentName}'");
            }
            else
            {
                string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
                var environments = GraphHelper.GetResultCollection<Model.PowerPlatform.Environment.Environment>(this, Connection, baseUrl + "/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken);
                environmentName = environments.FirstOrDefault(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == true)?.Name;

                if(string.IsNullOrEmpty(environmentName))
                {
                    throw new Exception($"No default environment found, please pass in a specific environment name using the {nameof(Environment)} parameter");
                }

                WriteVerbose($"Using default environment as retrieved '{environmentName}'");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var appName = Identity.GetName();

                WriteVerbose($"Retrieving specific PowerApp with the provided name '{appName}' within the environment '{environmentName}'");

                var result = GraphHelper.Get<Model.PowerPlatform.PowerApp.PowerApp>(this, Connection, $"{powerAppsUrl}/providers/Microsoft.PowerApps{(AsAdmin ? "/scopes/admin/environments/" + environmentName : "")}/apps/{appName}?api-version=2016-11-01", PowerAppsServiceAccessToken);
                 
                WriteObject(result, false);
            }
            else
            {
                WriteVerbose($"Retrieving all PowerApps within environment '{environmentName}'");

                var apps = GraphHelper.GetResultCollection<Model.PowerPlatform.PowerApp.PowerApp>(this, Connection, $"{powerAppsUrl}/providers/Microsoft.PowerApps/apps?api-version=2016-11-01&$filter=environment eq '{environmentName}'", PowerAppsServiceAccessToken);
                WriteObject(apps, true);
            }
        }
    }
}