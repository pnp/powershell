using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities.Auth
{
    internal static class ARMEndpoint
    {
        public static string GetARMEndpoint(PnPConnection connection)
        {
            string endpoint;
            switch (connection.AzureEnvironment)
            {
                case Framework.AzureEnvironment.Production:
                    endpoint = "https://management.azure.com/.default";
                    break;
                case Framework.AzureEnvironment.China:
                    endpoint = "https://management.chinacloudapi.cn/.default";
                    break;
                case Framework.AzureEnvironment.USGovernment:
                    endpoint = "https://management.usgovcloudapi.net/.default";
                    break;
                case Framework.AzureEnvironment.USGovernmentHigh:
                    endpoint = "https://management.usgovcloudapi.net/.default";
                    break;
                case Framework.AzureEnvironment.USGovernmentDoD:
                    endpoint = "https://management.usgovcloudapi.net/.default";
                    break;
                default:
                    endpoint = "https://management.azure.com/.default";
                    break;
            }
            return endpoint;
        }
    }
}
