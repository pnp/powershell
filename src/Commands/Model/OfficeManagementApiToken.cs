using Microsoft.Identity.Client;
using PnP.Framework;
using System;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Contains an Office 365 Management API JWT oAuth token
    /// </summary>
    public class OfficeManagementApiToken : GenericToken
    {
        /// <summary>
        /// The resource identifier for Microsoft Office 365 Management API tokens
        /// </summary>
        public const string ResourceIdentifier = "https://manage.office.com";

        /// <summary>
        /// Instantiates a new Office 365 Management API token
        /// </summary>
        /// <param name="accesstoken">Accesstoken of which to instantiate a new token</param>
        public OfficeManagementApiToken(string accesstoken) : base(accesstoken)
        {
            TokenAudience = Enums.TokenAudience.OfficeManagementApi;
        }

        /// <summary>
        /// Tries to acquire an application Office 365 Management API Access Token
        /// </summary>
        /// <param name="tenant">Name or id of the tenant to acquire the token for (i.e. contoso.onmicrosoft.com). Required.</param>
        /// <param name="clientId">ClientId to use to acquire the token. Required.</param>
        /// <param name="certificate">Certificate to use to acquire the token. Required.</param>
        /// <returns><see cref="OfficeManagementApiToken"/> instance with the token</returns>
        public static async Task<GenericToken> AcquireApplicationTokenAsync(string tenant, string clientId, X509Certificate2 certificate, AzureEnvironment azureEnvironment)
        {
            var endPoint = GenericToken.GetAzureADLoginEndPoint(azureEnvironment);
            var token = await GenericToken.AcquireApplicationTokenAsync(tenant, clientId, $"{endPoint}/{tenant}", new[] { $"{ResourceIdentifier}/{DefaultScope}" }, certificate);
            return new OfficeManagementApiToken(token.AccessToken);
        }

        /// <summary>
        /// Tries to acquire an application Office 365 Management API Access Token
        /// </summary>
        /// <param name="tenant">Name or id of the tenant to acquire the token for (i.e. contoso.onmicrosoft.com). Required.</param>
        /// <param name="clientId">ClientId to use to acquire the token. Required.</param>
        /// <param name="clientSecret">Client Secret to use to acquire the token. Required.</param>
        /// <returns><see cref="OfficeManagementApiToken"/> instance with the token</returns>
        public static async Task<GenericToken> AcquireApplicationTokenAsync(string tenant, string clientId, string clientSecret, AzureEnvironment azureEnvironment)
        {
            var endPoint = GenericToken.GetAzureADLoginEndPoint(azureEnvironment);
            var token = await GenericToken.AcquireApplicationTokenAsync(tenant, clientId, $"{endPoint}/{tenant}", new[] { $"{ResourceIdentifier}/{DefaultScope}" }, clientSecret);
            return new OfficeManagementApiToken(token.AccessToken);
        }

        /// <summary>
        /// Tries to acquire an application Office 365 Management API Access Token for the provided scopes interactively by allowing the user to log in
        /// </summary>
        /// <param name="clientId">ClientId to use to acquire the token. Required.</param>
        /// <param name="scopes">Array with scopes that should be requested access to. Required.</param>
        /// <returns><see cref="OfficeManagementApiToken"/> instance with the token</returns>
        public static async new Task<GenericToken> AcquireApplicationTokenInteractiveAsync(string clientId, string[] scopes, AzureEnvironment azureEnvironment)
        {
            var token = await GenericToken.AcquireApplicationTokenInteractiveAsync(clientId, scopes.Select(s => $"{ResourceIdentifier}/{s}").ToArray(), azureEnvironment);
            return new OfficeManagementApiToken(token.AccessToken);
        }

        public static async Task<GraphToken> AcquireApplicationTokenDeviceLoginAsync(string clientId, string[] scopes, Action<DeviceCodeResult> callBackAction, AzureEnvironment azureEnvironment, CancellationToken cancellationToken)
        {
            var endPoint = GenericToken.GetAzureADLoginEndPoint(azureEnvironment);
            var token = await AcquireApplicationTokenDeviceLoginAsync(clientId, scopes.Select(s => $"{ResourceIdentifier}/{s}").ToArray(), $"{endPoint}/organizations", callBackAction, cancellationToken);
            return new GraphToken(token.AccessToken);
        }

        /// <summary>
        /// Tries to acquire a delegated Office 365 Management API Access Token for the provided scopes using the provided credentials
        /// </summary>
        /// <param name="clientId">ClientId to use to acquire the token. Required.</param>
        /// <param name="scopes">Array with scopes that should be requested access to. Required.</param>
        /// <param name="username">The username to authenticate with. Required.</param>
        /// <param name="securePassword">The password to authenticate with. Required.</param>
        /// <returns><see cref="OfficeManagementApiToken"/> instance with the token</returns>
        public static async Task<GenericToken> AcquireDelegatedTokenWithCredentialsAsync(string clientId, string[] scopes, string username, SecureString securePassword, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            var endPoint = GenericToken.GetAzureADLoginEndPoint(azureEnvironment);
            var token = await GenericToken.AcquireDelegatedTokenWithCredentialsAsync(clientId, scopes.Select(s => $"{ResourceIdentifier}/{s}").ToArray(), $"{endPoint}/organizations/", username, securePassword);
            return new OfficeManagementApiToken(token.AccessToken);
        }
    }
}

