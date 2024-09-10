using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsCommon.Set, "PnPPowerAppByPassConsent")]
    [RequiredMinimalApiPermissions("https://management.azure.com/user_impersonation", "https://service.powerapps.com/user")]
    public class PnPPowerAppByPassConsent : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAppPipeBind Identity;

        [Parameter(Mandatory = false)]
        public bool Enabled = true;

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

                if (string.IsNullOrEmpty(environmentName))
                {
                    throw new Exception($"No default environment found, please pass in a specific environment name using the {nameof(Environment)} parameter");
                }

                WriteVerbose($"Using default environment as retrieved '{environmentName}'");
            }

            var appName = Identity.GetName();

            WriteVerbose($"Setting specific PowerApp with the provided name '{appName}' consent within the environment '{environmentName}'");

            var postData = new
            {
                bypassconsent = Enabled.ToString()
            };
            var response = RestHelper.Post(Connection.HttpClient, $"{powerAppsUrl}/providers/Microsoft.PowerApps/scopes/admin/environments/{environmentName}/apps/{appName}/setPowerAppConnectionDirectConsentBypass?api-version=2021-02-01", PowerAppsServiceAccessToken, payload: postData);
            WriteObject(response, false);
        }
    }
}
