using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsCommon.Set, "PnPPowerAppByPassConsent")]
    [RequiredMinimalApiPermissions("https://management.azure.com/.default")]
    public class PnPPowerAppByPassConsent : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAppPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string BypassConsent;

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

                WriteVerbose($"Setting specific PowerApp with the provided name '{appName}' consent within the environment '{environmentName}'");
                var postData = new
                {
                    bypassconsent = BypassConsent
                };
                var response = RestHelper.PostAsync(Connection.HttpClient, $"{powerAppsUrl}/providers/Microsoft.PowerApps/scopes/admin/environments/{environmentName}/apps/{appName}/setPowerAppConnectionDirectConsentBypass?api-version=2021-02-01", AccessToken, payload: postData).GetAwaiter().GetResult();
                WriteObject(response, false);
            }
        }
    }

}
