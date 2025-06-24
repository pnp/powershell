using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Helper classes around oAuth Token Evaluation
    /// </summary>
    internal static class TokenHandler
    {
        /// <summary>
        /// Returns the type of oAuth JWT token being passed in (Delegate/AppOnly)
        /// </summary>
        /// <param name="accessToken">The oAuth JWT token</param>
        /// <returns>Enum indicating the type of oAuth JWT token</returns>
        internal static Enums.IdType RetrieveTokenType(string accessToken)
        {
            var decodedToken = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(accessToken);

            // The idType is stored in the token as a claim
            var idType = decodedToken.Claims.FirstOrDefault(c => c.Type == "idtyp");

            // Check if the token contains an idType
            if (idType == null) return Enums.IdType.Unknown;

            // Parse the idType to the corresponding enum value
            return idType.Value.ToLowerInvariant() switch
            {
                "user" => Enums.IdType.Delegate,
                "app" => Enums.IdType.Application,
                _ => Enums.IdType.Unknown
            };
        }

        /// <summary>
        /// Returns the userId of the user who's token is being passed in
        /// </summary>
        /// <param name="accessToken">The oAuth JWT token</param>
        /// <returns>The userId of the user for which the passed in delegate token is for</returns>
        internal static Guid? RetrieveTokenUser(string accessToken)
        {
            var decodedToken = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(accessToken);

            // The objectId is stored in the token as a claim
            var objectId = decodedToken.Claims.FirstOrDefault(c => c.Type == "oid");

            // Check if the token contains an objectId and if its a valid Guid
            return objectId == null || !Guid.TryParse(objectId.Value, out Guid objectIdGuid) ? null : objectIdGuid;
        }

        /// <summary>
        /// Returns the permission scopes of the oAuth JWT token being passed in
        /// </summary>
        /// <param name="accessToken">The oAuth JWT token</param>
        /// <returns><see cref="RequiredApiPermission"/> array containing the scopes</returns>
        internal static RequiredApiPermission[] ReturnScopes(string accessToken)
        {
            var decodedToken = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(accessToken);

            // Retrieve the audience the token was issued for
            var audience = decodedToken.Audiences.FirstOrDefault();
            var resourceType = DefineResourceTypeFromAudience(audience);

            // The scopes can either be stored in the roles or scp claim, so we examine both
            var scopes = decodedToken.Claims.Where(c => c.Type == "roles" || c.Type == "scp").SelectMany(r => r.Value.Split(" ").Select(r => new RequiredApiPermission(resourceType, r))).ToArray();
            return scopes;
        }

        /// <summary>
        /// Defines the type of resource based on a passed in audience
        /// </summary>
        /// <param name="audience">Audience, i.e. https://graph.microsoft.com, which could be localized, i.e. https://graph.microsoft.us</param>
        /// <returns>Type of resource it represents</returns>
        internal static Enums.ResourceTypeName DefineResourceTypeFromAudience(string audience)
        {
            // Clean up the audience to only leave the main part which allows our switch below to be cleaner and more readable
            var sanitizedAudience = audience?.TrimEnd('/').ToLowerInvariant();
            if (sanitizedAudience.StartsWith("http://")) sanitizedAudience = sanitizedAudience.Substring(7);
            if (sanitizedAudience.StartsWith("https://")) sanitizedAudience = sanitizedAudience.Substring(8);

            // TODO: Extend with all options
            Enums.ResourceTypeName resource = sanitizedAudience switch
            {
                "graph" or "graph.microsoft.com" or "graph.microsoft.us" or "graph.microsoft.de" or "microsoftgraph.chinacloudapi.cn" or "dod-graph.microsoft.us" or "00000003-0000-0000-c000-000000000000" => Enums.ResourceTypeName.Graph,
                "azure" or "management.azure.com" or "management.chinacloudapi.cn" or "management.usgovcloudapi.net" or "management.usgovcloudapi.net" or "management.usgovcloudapi.net" => Enums.ResourceTypeName.AzureManagementApi,
                "exchangeonline" or "outlook.office.com" or "outlook.office365.com" => Enums.ResourceTypeName.ExchangeOnline,
                "flow" or "service.flow.microsoft.com" => Enums.ResourceTypeName.PowerAutomate,
                "powerapps" or "api.powerapps.com" => Enums.ResourceTypeName.PowerApps,
                "dynamics" or "admin.services.crm.dynamics.com" or "api.crm.dynamics.com" => Enums.ResourceTypeName.DynamicsCRM,

                // We assume SharePoint as the default as vanity domains cause no fixed structure to be present in the audience name
                _ => Enums.ResourceTypeName.SharePoint
            };
            return resource;
        }

        /// <summary>
        /// Ensures the oAuth JWT token holds the permissions in it (roles) that match with with the required permissions for the cmdlet provided through the attributes on the cmdlet that have the same audience
        /// </summary>
        /// <param name="cmdletType">The cmdlet that will be executed. Used to check for the permissions attribute.</param>
        /// <param name="accessToken">The oAuth JWT token that needs to be validated for its roles</param>
        /// <exception cref="PSArgumentException">Thrown if the permissions set through the permissions attribute do not match the roles in the JWT token</exception>
        internal static void EnsureRequiredPermissionsAvailableInAccessTokenAudience(Type cmdletType, string accessToken)
        {
            // Decode the JWT token
            var decodedToken = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(accessToken);

            // Retrieve the audience the token was issued for
            var audience = decodedToken.Audiences.FirstOrDefault();
            var resourceType = DefineResourceTypeFromAudience(audience);

            // Determine the type of token (delegate or app-only)
            var tokenType = RetrieveTokenType(accessToken);

            // Validate the permissions in the access token against the permissions required for the cmdlet through the attributes provided at the class level of the cmdlet
            var permissionEvaluationResponses = AccessTokenPermissionValidationResponse.EvaluatePermissions(cmdletType, accessToken, resourceType, tokenType);

            // If the permission evaluation returns null, it was unable to determine the permissions and it should stop
            if (permissionEvaluationResponses == null) return;

            // If any of the permission evaluations has the required permissions present, we can return and are good to go permission-wise
            if (permissionEvaluationResponses.Any(p => p.RequiredPermissionsPresent)) return;

            // Retrieve the scopes we have in our AccessToken
            var scopes = ReturnScopes(accessToken);

            // None of the permission attributes matched the permissions in the access token, so we throw an exception
            var exceptionTextBuilder = new StringBuilder();
            exceptionTextBuilder.AppendLine($"Current access token lacks {(permissionEvaluationResponses.Length != 1 ? "one of " : "")}the following required {tokenType.GetDescription()} permission scope{(permissionEvaluationResponses.Length != 1 ? "s" : "")} on the resource {resourceType.GetDescription()}:");

            for (int i = 0; i < permissionEvaluationResponses.Length; i++)
            {
                exceptionTextBuilder.Append($"{string.Join(" and ", permissionEvaluationResponses[i].MissingPermissions.Select(s => s.Scope))}");

                if (i < permissionEvaluationResponses.Length - 1)
                {
                    exceptionTextBuilder.Append(" or ");
                }
            }

            // Log a warning that the permission check failed. Deliberately not throwing an exception here, as the permission attributes might be wrong, thus will try to execute anyway.
            PnP.Framework.Diagnostics.Log.Error("TokenHandler", exceptionTextBuilder.ToString().Replace(Environment.NewLine, " "));
            //cmdlet.LogWarning(exceptionTextBuilder.ToString());
        }

        /// <summary>
        /// Returns an oAuth JWT access token
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which the token is requested</param>
        /// <param name="audience">Audience to retrieve the token for</param>
        /// <param name="connection">The connection to use to make the token calls</param>
        /// <returns>oAuth JWT token</returns>
        /// <exception cref="PSInvalidOperationException">Thrown if retrieval of the token fails</exception>
        internal static string GetAccessToken(string audience, PnPConnection connection)
        {
            if (connection == null) return null;

            string accessToken = null;
            if (connection.ConnectionMethod == ConnectionMethod.AzureADWorkloadIdentity)
            {
                PnP.Framework.Diagnostics.Log.Debug("TokenHandler", $"Acquiring token for resource {connection.GraphEndPoint} using Azure AD Workload Identity");
                //cmdlet.LogDebug("Acquiring token for resource " + connection.GraphEndPoint + " using Azure AD Workload Identity");
                accessToken = GetAzureADWorkloadIdentityTokenAsync($"{audience.TrimEnd('/')}").GetAwaiter().GetResult();
            }
            else if (connection.ConnectionMethod == ConnectionMethod.FederatedIdentity)
            {
                PnP.Framework.Diagnostics.Log.Debug("TokenHandler", $"Acquiring token for resource {connection.GraphEndPoint} using Federated Identity");
                //cmdlet.LogDebug("Acquiring token for resource " + connection.GraphEndPoint + " using Federated Identity");
                accessToken = GetFederatedIdentityTokenAsync(connection.ClientId, connection.Tenant, $"{audience.TrimEnd('/')}").GetAwaiter().GetResult();
            }
            else
            {
                if (connection.Context != null)
                {
                    var contextSettings = connection.Context.GetContextSettings();
                    var authManager = contextSettings.AuthenticationManager;
                    if (authManager != null)
                    {
                        if (contextSettings.Type == Framework.Utilities.Context.ClientContextType.SharePointACSAppOnly)
                        {
                            // When connected using ACS, we cannot get a token for another endpoint
                            throw new PSInvalidOperationException("Trying to get a token for a different endpoint while being connected through an ACS token is not possible. Please connect differently.");
                        }

                        accessToken = authManager.GetAccessToken(audience);
                    }
                }
            }
            if (string.IsNullOrEmpty(accessToken))
            {
                PnP.Framework.Diagnostics.Log.Debug("TokenHandler", $"Unable to acquire token for resource {connection.GraphEndPoint}");
                //cmdlet.LogDebug($"Unable to acquire token for resource {connection.GraphEndPoint}");
                return null;
            }

            return accessToken;
        }

        /// <summary>
        /// Returns an access token based on a Azure AD Workload Identity. Only works within Azure components supporting workload identities.
        /// </summary>
        /// <param name="cmdlet">The cmdlet scope in which this code runs. Used to write logging to.</param>
        /// <param name="httpClient">The HttpClient that will be reused to fetch the token to avoid port exhaustion</param>
        /// <param name="requiredScope">The permission scope to be requested, in the format https://<resource>/<scope>, i.e. https://graph.microsoft.com/Group.Read.All</param>
        /// <returns>Access token</returns>
        /// <exception cref="PSInvalidOperationException">Thrown if unable to retrieve an access token through a managed identity</exception>
        internal static async Task<string> GetAzureADWorkloadIdentityTokenAsync(string requiredScope)
        {
            // <authentication>
            // Azure AD Workload Identity webhook will inject the following env vars
            // 	AZURE_CLIENT_ID with the clientID set in the service account annotation
            // 	AZURE_TENANT_ID with the tenantID set in the service account annotation.
            // 	If not defined, then the tenantID provided via azure-wi-webhook-config for the webhook will be used.
            // 	AZURE_FEDERATED_TOKEN_FILE is the service account token path
            var clientID = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
            var tokenPath = Environment.GetEnvironmentVariable("AZURE_FEDERATED_TOKEN_FILE");
            var tenantID = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
            var host = Environment.GetEnvironmentVariable("AZURE_AUTHORITY_HOST");

            var _confidentialClientApp = ConfidentialClientApplicationBuilder.Create(clientID)
                .WithAuthority(host, tenantID)
                .WithClientAssertion(() => System.IO.File.ReadAllText(tokenPath))
                .WithCacheOptions(CacheOptions.EnableSharedCacheOptions)
                .Build();

            AuthenticationResult result = null;
            try
            {
                result = await _confidentialClientApp
                            .AcquireTokenForClient(new string[] { requiredScope })
                            .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // The application doesn't have sufficient permissions.
                // - Did you declare enough app permissions during app creation?
                // - Did the tenant admin grant permissions to the application?
                throw new PSInvalidOperationException(ex.Message);
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be in the form "https://resourceurl/.default"
                // Mitigation: Change the scope to be as expected.
                throw new PSInvalidOperationException(ex.Message);
            }
            catch (MsalServiceException ex)
            {
                // Some other generic exception
                throw new PSInvalidOperationException(ex.Message);
            }
            return result.AccessToken;
        }

        /// <summary>
        /// Returns an access token based on a Federated Identity. Only works within Azure components supporting federated identities like GitHub/AzureDevOps.
        /// </summary>
        /// <param name="cmdlet">The cmdlet scope in which this code runs. Used to write logging to.</param>
        /// <param name="httpClient">The HttpClient that will be reused to fetch the token to avoid port exhaustion</param>
        /// <param name="requiredScope">The permission scope to be requested, in the format https://<resource>/<scope>, i.e. https://graph.microsoft.com/Group.Read.All</param>
        /// <returns>Access token</returns>
        /// <exception cref="PSInvalidOperationException">Thrown if unable to retrieve an access token through a managed identity</exception>
        internal static async Task<string> GetFederatedIdentityTokenAsync(string clientId, string tenant, string requiredScope)
        {
            var actionsIdTokenRequestUrl = Environment.GetEnvironmentVariable("ACTIONS_ID_TOKEN_REQUEST_URL");
            var actionsIdTokenRequestToken = Environment.GetEnvironmentVariable("ACTIONS_ID_TOKEN_REQUEST_TOKEN");
            var systemOidcRequestUri = Environment.GetEnvironmentVariable("SYSTEM_OIDCREQUESTURI");

            if (!string.IsNullOrEmpty(actionsIdTokenRequestUrl) && !string.IsNullOrEmpty(actionsIdTokenRequestToken))
            {
                Framework.Diagnostics.Log.Debug("TokenHandler", "ACTIONS_ID_TOKEN_REQUEST_URL and ACTIONS_ID_TOKEN_REQUEST_TOKEN env variables found. The context is GitHub Actions...");

                var federationToken = await GetFederationTokenFromGithubAsync();
                return await GetAccessTokenWithFederatedTokenAsync(clientId, tenant, requiredScope, federationToken);
            }
            else if (!string.IsNullOrEmpty(systemOidcRequestUri))
            {
                Framework.Diagnostics.Log.Debug("TokenHandler", "SYSTEM_OIDCREQUESTURI env variable found. The context is Azure DevOps...");

                var systemAccessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN");
                if (string.IsNullOrEmpty(systemAccessToken))
                {
                    throw new PSInvalidOperationException("The SYSTEM_ACCESSTOKEN environment variable is not available. Please check the Azure DevOps pipeline task configuration. It should contain 'SYSTEM_ACCESSTOKEN: $(System.AccessToken)' in the env section.");
                }

                var serviceConnectionId = Environment.GetEnvironmentVariable("AZURESUBSCRIPTION_SERVICE_CONNECTION_ID");
                var serviceConnectionAppId = Environment.GetEnvironmentVariable("AZURESUBSCRIPTION_CLIENT_ID");
                var serviceConnectionTenantId = Environment.GetEnvironmentVariable("AZURESUBSCRIPTION_TENANT_ID");
                var useServiceConnection = !string.IsNullOrEmpty(serviceConnectionId) &&
                                          !string.IsNullOrEmpty(serviceConnectionAppId) &&
                                          !string.IsNullOrEmpty(serviceConnectionTenantId);

                if (!useServiceConnection)
                {
                    throw new PSInvalidOperationException("The Azure DevOps pipeline task is not configured to use a service connection. Please check the pipeline configuration and ensure that the service connection is set up correctly.");
                }

                var federationToken = await GetFederationTokenFromAzureDevOpsAsync(serviceConnectionId);
                return await GetAccessTokenWithFederatedTokenAsync(clientId, tenant, requiredScope, federationToken);
            }
            else
            {
                throw new PSInvalidOperationException("Federated identity is currently only supported in GitHub Actions and Azure DevOps.");
            }
        }

        private static async Task<string> GetFederationTokenFromGithubAsync()
        {
            try
            {
                Framework.Diagnostics.Log.Debug("TokenHandler", "Retrieving GitHub federation token...");

                var requestUrl = $"{Environment.GetEnvironmentVariable("ACTIONS_ID_TOKEN_REQUEST_URL")}&audience={Uri.EscapeDataString("api://AzureADTokenExchange")}";

                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("ACTIONS_ID_TOKEN_REQUEST_TOKEN"));
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("x-anonymous", "true");
                var response = await httpClient.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

                return tokenResponse["value"].ToString();
            }
            catch (Exception ex)
            {
                throw new PSInvalidOperationException($"Failed to retrieve GitHub federation token: {ex.Message}", ex);
            }
        }

        private static async Task<string> GetFederationTokenFromAzureDevOpsAsync(string serviceConnectionId = null)
        {
            try
            {
                Framework.Diagnostics.Log.Debug("TokenHandler", "Retrieving Azure DevOps federation token...");

                var urlSuffix = !string.IsNullOrEmpty(serviceConnectionId) ? $"&serviceConnectionId={serviceConnectionId}" : "";
                var requestUrl = $"{Environment.GetEnvironmentVariable("SYSTEM_OIDCREQUESTURI")}?api-version=7.1{urlSuffix}";

                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                requestMessage.Content = new StringContent("", Encoding.UTF8, "application/json");
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN"));
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("x-anonymous", "true");

                var response = await httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

                return tokenResponse["oidcToken"].ToString();
            }
            catch (Exception ex)
            {
                throw new PSInvalidOperationException($"Failed to retrieve Azure DevOps federation token: {ex.Message}", ex);
            }
        }

        private static async Task<string> GetAccessTokenWithFederatedTokenAsync(string clientId, string tenant, string resource, string federatedToken)
        {
            try
            {
                Framework.Diagnostics.Log.Debug("TokenHandler", "Retrieving Entra ID access token with federated token...");
                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();

                var queryParams = new List<string>
    {
        "grant_type=client_credentials",
        $"scope={resource}",
        $"client_id={clientId}",
        $"client_assertion_type=urn:ietf:params:oauth:client-assertion-type:jwt-bearer",
        $"client_assertion={federatedToken}"
    };

                var requestData = string.Join("&", queryParams);
                var requestUrl = $"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token";

                using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                request.Content = new StringContent(requestData, Encoding.UTF8, "application/x-www-form-urlencoded");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("x-anonymous", "true");

                var response = await httpClient.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to retrieve federated access token: HTTP Error {response.StatusCode}: {responseContent}");
                }
                var tokenResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);

                return tokenResponse["access_token"].ToString();
            }
            catch (Exception ex)
            {
                Framework.Diagnostics.Log.Error("TokenHandler", ex.Message);
                throw new PSInvalidOperationException(ex.Message, ex);
            }
        }
    }
}
