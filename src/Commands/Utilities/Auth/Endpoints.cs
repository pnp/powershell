using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Utilities.Auth
{
    /// <summary>
    /// Utility class to provide the proper endpoints with the current Sovereign or Commercial Cloud environment
    /// </summary>
    internal static class Endpoints
    {
        /// <summary>
        /// Returns the endpoint for the Azure Resource Manager API based on the current connection
        /// </summary>
        /// <param name="connection">Connection to base the proper API endpoint on</param>
        /// <returns>The API endpoint</returns>
        public static string GetArmEndpoint(PnPConnection connection)
        {
            return connection.AzureEnvironment switch
            {
                Framework.AzureEnvironment.Production => "https://management.azure.com",
                Framework.AzureEnvironment.China => "https://management.chinacloudapi.cn",
                Framework.AzureEnvironment.USGovernment => "https://management.usgovcloudapi.net",
                Framework.AzureEnvironment.USGovernmentHigh => "https://management.usgovcloudapi.net",
                Framework.AzureEnvironment.USGovernmentDoD => "https://management.usgovcloudapi.net",
                _ => "https://management.azure.com",
            };
        }

        /// <summary>
        /// Returns the endpoint for the Microsoft Graph API based on the current connection
        /// </summary>
        /// <param name="connection">Connection to base the proper API endpoint on</param>
        /// <returns>The API endpoint</returns>
        public static string GetGraphEndpoint(PnPConnection connection)
        {
            return connection.GraphEndPoint;
        }
    }
}
