using PnP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class PowerPlatformUtility
    {
        public static string GetPowerAutomateEndpoint(AzureEnvironment environment)
        {
            return (environment) switch
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

        public static string GetPowerAppsEndpoint(AzureEnvironment environment)
        {
            return (environment) switch
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

        public static string GetBapEndpoint(AzureEnvironment environment)
        {
            return (environment) switch
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
    }
}
