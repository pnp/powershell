using Microsoft.Identity.Client;
using PnP.Framework;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Contains a Graph JWT oAuth token
    /// </summary>
    public class GraphToken : GenericToken
    {
        /// <summary>
        /// The resource identifier for Microsoft Graph API tokens
        /// </summary>
        public const string ResourceIdentifier = "https://graph.microsoft.com";

        /// <summary>
        /// Instantiates a new Graph token
        /// </summary>
        /// <param name="accesstoken">Accesstoken of which to instantiate a new token</param>
        public GraphToken(string accesstoken) : base(accesstoken)
        {
            TokenAudience = Enums.TokenAudience.MicrosoftGraph;
        }

        /// <summary>
        /// Tries to acquire an application Microsoft Graph Access Token
        /// </summary>
        /// <param name="tenant">Name of the tenant to acquire the token for (i.e. contoso.onmicrosoft.com). Required.</param>
        /// <param name="clientId">ClientId to use to acquire the token. Required.</param>
        /// <param name="certificate">Certificate to use to acquire the token. Required.</param>
        /// <returns><see cref="GraphToken"/> instance with the token</returns>
        public static async Task<GraphToken> AcquireApplicationTokenAsync(string tenant, string clientId, X509Certificate2 certificate, AzureEnvironment azureEnvironment)
        {
            var endPoint = GenericToken.GetAzureADLoginEndPoint(azureEnvironment);
            var graphToken = await GenericToken.AcquireApplicationTokenAsync(tenant, clientId, $"{endPoint}/{tenant}", new[] { $"{ResourceIdentifier}/{DefaultScope}" }, certificate);
            return new GraphToken(graphToken.AccessToken);
        }

        /// <summary>
        /// Tries to acquire an application Microsoft Graph Access Token
        /// </summary>
        /// <param name="tenant">Name of the tenant to acquire the token for (i.e. contoso.onmicrosoft.com). Required.</param>
        /// <param name="clientId">ClientId to use to acquire the token. Required.</param>
        /// <param name="clientSecret">Client Secret to use to acquire the token. Required.</param>
        /// <returns><see cref="GraphToken"/> instance with the token</returns>
        public static async Task<GraphToken> AcquireApplicationTokenAsync(string tenant, string clientId, string clientSecret, AzureEnvironment azureEnvironment)
        {
            var endPoint = GenericToken.GetAzureADLoginEndPoint(azureEnvironment);
            var graphToken = await GenericToken.AcquireApplicationTokenAsync(tenant, clientId, $"{endPoint}/{tenant}", new[] { $"{ResourceIdentifier}/{DefaultScope}" }, clientSecret);
            return new GraphToken(graphToken.AccessToken);
        }

        /// <summary>
        /// Tries to acquire an application Microsoft Graph Access Token for the provided scopes interactively by allowing the user to log in
        /// </summary>
        /// <param name="clientId">ClientId to use to acquire the token. Required.</param>
        /// <param name="scopes">Array with scopes that should be requested access to. Required.</param>
        /// <returns><see cref="GraphToken"/> instance with the token</returns>
        public static new async Task<GraphToken> AcquireApplicationTokenInteractiveAsync(string clientId, string[] scopes, AzureEnvironment azureEnvironment)
        {
            var graphToken = await GenericToken.AcquireApplicationTokenInteractiveAsync(clientId, scopes.Select(s => $"{ResourceIdentifier}/{s}").ToArray(), azureEnvironment);
            return new GraphToken(graphToken.AccessToken);
        }

        // public static async Task<GraphToken> AcquireApplicationTokenDeviceLoginAsync(string clientId, string[] scopes, Action<DeviceCodeResult> callBackAction, AzureEnvironment azureEnvironment, CancellationToken cancellationToken)
        // {
        //     var endPoint = GenericToken.GetAzureADLoginEndPoint(azureEnvironment);
        //     var officeManagementApiScopes = Enum.GetNames(typeof(OfficeManagementApiPermission)).Select(s => s.Replace("_", ".")).Intersect(scopes).ToArray();
        //     // Take the remaining scopes and try requesting them from the Microsoft Graph API
        //     scopes = scopes.Except(officeManagementApiScopes).ToArray();
        //     var graphToken = await AcquireApplicationTokenDeviceLoginAsync(clientId, scopes, $"{endPoint}/organizations", callBackAction, cancellationToken);
        //     return new GraphToken(graphToken.AccessToken);
        // }

        // public static async Task<GraphToken> AcquireApplicationTokenDeviceLoginAsync(string clientId, string[] scopes, Action<DeviceCodeResult> callBackAction, AzureEnvironment azureEnvironment)
        // {
        //     var endPoint = GenericToken.GetAzureADLoginEndPoint(azureEnvironment);
        //     var officeManagementApiScopes = Enum.GetNames(typeof(OfficeManagementApiPermission)).Select(s => s.Replace("_", ".")).Intersect(scopes).ToArray();
        //     // Take the remaining scopes and try requesting them from the Microsoft Graph API
        //     scopes = scopes.Except(officeManagementApiScopes).ToArray();
        //     var token = default(CancellationToken);
        //     var graphToken = await AcquireApplicationTokenDeviceLoginAsync(clientId, scopes, $"{endPoint}/organizations", callBackAction, token);
        //     return new GraphToken(graphToken.AccessToken);
        // }

        /// <summary>
        /// Tries to acquire a delegated Microsoft Graph Access Token for the provided scopes using the provided credentials
        /// </summary>
        /// <param name="clientId">ClientId to use to acquire the token. Required.</param>
        /// <param name="scopes">Array with scopes that should be requested access to. Required.</param>
        /// <param name="username">The username to authenticate with. Required.</param>
        /// <param name="securePassword">The password to authenticate with. Required.</param>
        /// <returns><see cref="GraphToken"/> instance with the token</returns>
        // public static async Task<GraphToken> AcquireDelegatedTokenWithCredentialsAsync(string clientId, string[] scopes, string username, SecureString securePassword, AzureEnvironment azureEnvironment)
        // {
        //     var endPoint = GenericToken.GetAzureADLoginEndPoint(azureEnvironment);
        //     var officeManagementApiScopes = Enum.GetNames(typeof(OfficeManagementApiPermission)).Select(s => s.Replace("_", ".")).Intersect(scopes).ToArray();
        //     // Take the remaining scopes and try requesting them from the Microsoft Graph API
        //     scopes = scopes.Except(officeManagementApiScopes).ToArray();
        //     var graphToken = await AcquireDelegatedTokenWithCredentialsAsync(clientId, scopes.Select(s => $"{ResourceIdentifier}/{s}").ToArray(), $"{endPoint}/organizations/", username, securePassword);
        //     return new GraphToken(graphToken.AccessToken);
        // }
    }
}
