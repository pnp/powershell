using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Azure Management API related cmdlets
    /// </summary>
    public abstract class PnPAzureManagementApiCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// Returns an Access Token for the Microsoft Office Management API, if available, otherwise NULL
        /// </summary>
        public string AccessToken => TokenHandler.GetAccessToken(this, GetARMEndpoint(Connection), Connection);

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (Connection?.Context != null)
            {
                if (Connection?.Context.GetContextSettings().Type == Framework.Utilities.Context.ClientContextType.Cookie)
                {
                    throw new PSInvalidOperationException("This cmdlet not work with a WebLogin/Cookie based connection towards SharePoint.");
                }
            }
        }

        protected static string GetARMEndpoint(PnPConnection connection)
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