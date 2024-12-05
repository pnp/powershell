using PnP.Framework;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Linq;
using PnP.Framework.Diagnostics;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Utility class for working with the Power Platform
    /// </summary>
    internal static class PowerPlatformUtility
    {
        /// <summary>
        /// Returns the BaseUrl for calling into the Power Automate APIs based on the Azure Environment
        /// </summary>
        /// <param name="environment">Azure Environment to indicate the type of cloud to target</param>
        /// <returns>Base URL for calling into Power Automate APIs</returns>
        public static string GetPowerAutomateEndpoint(AzureEnvironment environment)
        {
            return environment switch
            {
                AzureEnvironment.Production => "https://api.flow.microsoft.com",
                AzureEnvironment.Germany => "https://api.flow.microsoft.com",
                AzureEnvironment.China => "https://api.powerautomate.cn",
                AzureEnvironment.USGovernment => "https://gov.api.flow.microsoft.us",
                AzureEnvironment.USGovernmentHigh => "https://high.api.flow.microsoft.us",
                AzureEnvironment.USGovernmentDoD => "https://api.flow.appsplatform.us",
                AzureEnvironment.PPE => "https://api.flow.microsoft.com",
                _ => "https://api.flow.microsoft.com"
            };
        }

        /// <summary>
        /// Returns the BaseUrl for calling into the Power Apps APIs based on the Azure Environment
        /// </summary>
        /// <param name="environment">Azure Environment to indicate the type of cloud to target</param>
        /// <returns>Base URL for calling into Power Apps APIs</returns>
        public static string GetPowerAppsEndpoint(AzureEnvironment environment)
        {
            return environment switch
            {
                AzureEnvironment.Production => "https://api.powerapps.com",
                AzureEnvironment.Germany => "https://api.powerapps.com",
                AzureEnvironment.China => "https://api.powerautomate.cn",
                AzureEnvironment.USGovernment => "https://gov.api.powerapps.us",
                AzureEnvironment.USGovernmentHigh => "https://high.api.powerapps.us",
                AzureEnvironment.USGovernmentDoD => "https://api.apps.appsplatform.us",
                AzureEnvironment.PPE => "https://api.powerapps.com",
                _ => "https://api.powerapps.com"
            };
        }

        /// <summary>
        /// Returns the BaseUrl for calling into the Business Applications APIs based on the Azure Environment
        /// </summary>
        /// <param name="environment">Azure Environment to indicate the type of cloud to target</param>
        /// <returns>Base URL for calling into Business Applications APIs</returns>
        public static string GetBapEndpoint(AzureEnvironment environment)
        {
            return environment switch
            {
                AzureEnvironment.Production => "https://api.bap.microsoft.com",
                AzureEnvironment.Germany => "https://api.bap.microsoft.com",
                AzureEnvironment.China => "https://api.bap.partner.microsoftonline.cn",
                AzureEnvironment.USGovernment => "https://gov.api.bap.microsoft.us",
                AzureEnvironment.USGovernmentHigh => "https://high.api.bap.microsoft.us",
                AzureEnvironment.USGovernmentDoD => "https://api.bap.appsplatform.us",
                AzureEnvironment.PPE => "https://api.bap.microsoft.com",
                _ => "https://api.bap.microsoft.com"
            };
        }

        /// <summary>
        /// Returns the default Power Platform environment
        /// </summary>
        /// <param name="cmdlet">The cmdlet from which this is being requested</param>
        /// <param name="connection">The connection to use to retrieve the default</param>
        /// <param name="accessToken">Optional access token to use to retrieve the default environment. When omitted, it will try to retrieve an access token for the call itself.</param>
        /// <param name="azureEnvironment">The type of cloud to communicate with</param>
        /// <returns></returns>
        public static Model.PowerPlatform.Environment.Environment GetDefaultEnvironment(ApiRequestHelper requestHelper, AzureEnvironment azureEnvironment)
        {
            Log.Debug("PowerPlatformUtility","Retrieving default Power Platform environment");

            // If we don't have an access token yet, try to retrieve one
            //accessToken ??= TokenHandler.GetAccessToken(cmdlet, $"{Endpoints.GetArmEndpoint(connection)}/.default", connection);
            
            // Request the available environments and try to define the one marked as default
            string baseUrl = GetPowerAutomateEndpoint(azureEnvironment);
            return requestHelper.GetResultCollection<Model.PowerPlatform.Environment.Environment>($"{baseUrl}/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01").FirstOrDefault(e => e.Properties.IsDefault.GetValueOrDefault(false));
        }
    }
}
