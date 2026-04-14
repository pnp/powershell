using Microsoft.Identity.Client;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Concurrent;
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
        private const string IdTypeClaimType = "idtyp";
        private const string ObjectIdClaimType = "oid";
        private const string RolesClaimType = "roles";
        private const string ScopeClaimType = "scp";

        // Keyed by "{clientId}:{tenantId}:{authorityHost}" — one app per workload-identity config.
        // Reusing the same IConfidentialClientApplication lets MSAL's shared token cache do its job
        // without rebuilding the HTTP client, authority metadata, and builder overhead on every call.
        private static readonly ConcurrentDictionary<string, IConfidentialClientApplication> _confidentialClientAppCache = new();

        /// <summary>
        /// Returns the type of oAuth JWT token being passed in (Delegate/AppOnly)
        /// </summary>
        /// <param name="accessToken">The oAuth JWT token</param>
        /// <returns>Enum indicating the type of oAuth JWT token</returns>
        internal static Enums.IdType RetrieveTokenType(string accessToken)
        {
            var decodedToken = new JsonWebToken(accessToken);

            var idType = GetClaimValue(decodedToken, IdTypeClaimType);

            if (string.IsNullOrWhiteSpace(idType)) return Enums.IdType.Unknown;

            return idType.ToLowerInvariant() switch
            {
                "user" => Enums.IdType.Delegate,
                "app" => Enums.IdType.Application,
                _ => Enums.IdType.Unknown
            };
        }

        private static Enums.IdType RetrieveTokenType(JsonWebToken decodedToken)
        {
            var idType = GetClaimValue(decodedToken, IdTypeClaimType);

            if (string.IsNullOrWhiteSpace(idType)) return Enums.IdType.Unknown;

            return idType.ToLowerInvariant() switch
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
            var decodedToken = new JsonWebToken(accessToken);
            return RetrieveTokenUser(decodedToken);
        }

        private static Guid? RetrieveTokenUser(JsonWebToken decodedToken)
        {
            var objectId = GetClaimValue(decodedToken, ObjectIdClaimType);
            return Guid.TryParse(objectId, out Guid objectIdGuid) ? objectIdGuid : null;
        }

        /// <summary>
        /// Returns the permission scopes of the oAuth JWT token being passed in
        /// </summary>
        /// <param name="accessToken">The oAuth JWT token</param>
        /// <returns><see cref="RequiredApiPermission"/> array containing the scopes</returns>
        internal static RequiredApiPermission[] ReturnScopes(string accessToken)
        {
            var decodedToken = new JsonWebToken(accessToken);
            return ReturnScopes(decodedToken);
        }

        internal static RequiredApiPermission[] ReturnScopes(JsonWebToken decodedToken)
        {
            var audience = decodedToken.Audiences.FirstOrDefault();
            var resourceType = DefineResourceTypeFromAudience(audience);

            return decodedToken.Claims
                .Where(c => string.Equals(c.Type, RolesClaimType, StringComparison.OrdinalIgnoreCase) || string.Equals(c.Type, ScopeClaimType, StringComparison.OrdinalIgnoreCase))
                .SelectMany(c => c.Value.Split(' ').Select(scope => new RequiredApiPermission(resourceType, scope)))
                .ToArray();
        }

        /// <summary>
        /// Defines the type of resource based on a passed in audience
        /// </summary>
        /// <param name="audience">Audience, i.e. https://graph.microsoft.com, which could be localized, i.e. https://graph.microsoft.us</param>
        /// <returns>Type of resource it represents</returns>
        internal static Enums.ResourceTypeName DefineResourceTypeFromAudience(string audience)
        {
            var sanitizedAudience = SanitizeAudience(audience);
            if (string.IsNullOrWhiteSpace(sanitizedAudience))
            {
                return Enums.ResourceTypeName.Unknown;
            }

            // TODO: Extend with all options
            Enums.ResourceTypeName resource = sanitizedAudience switch
            {
                "graph" or "graph.microsoft.com" or "graph.microsoft.us" or "graph.microsoft.de" or "microsoftgraph.chinacloudapi.cn" or "dod-graph.microsoft.us" or "00000003-0000-0000-c000-000000000000" => Enums.ResourceTypeName.Graph,
                "azure" or "management.azure.com" or "management.chinacloudapi.cn" or "management.usgovcloudapi.net" => Enums.ResourceTypeName.AzureManagementApi,
                "exchangeonline" or "outlook.office.com" or "outlook.office365.com" => Enums.ResourceTypeName.ExchangeOnline,
                "flow" or "service.flow.microsoft.com" => Enums.ResourceTypeName.PowerAutomate,
                "powerapps" or "api.powerapps.com" => Enums.ResourceTypeName.PowerApps,
                "dynamics" or "admin.services.crm.dynamics.com" or "api.crm.dynamics.com" => Enums.ResourceTypeName.DynamicsCRM,
                "gcs" or "gcs.office.com" => Enums.ResourceTypeName.Gcs,

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
            if (!TryReadToken(accessToken, out var decodedToken))
            {
                PnP.Framework.Diagnostics.Log.Debug("TokenHandler", "Unable to validate access token permissions because the provided access token was null, empty, or not a valid JWT.");
                return;
            }

            var audience = decodedToken.Audiences.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(audience))
            {
                PnP.Framework.Diagnostics.Log.Debug("TokenHandler", "Unable to validate access token permissions because the access token does not contain an audience claim.");
                return;
            }

            var resourceType = DefineResourceTypeFromAudience(audience);

            var tokenType = RetrieveTokenType(decodedToken);
            if (tokenType == Enums.IdType.Unknown)
            {
                PnP.Framework.Diagnostics.Log.Debug("TokenHandler", "Unable to validate access token permissions because the access token type could not be determined.");
                return;
            }

            var permissionEvaluationResponses = AccessTokenPermissionValidationResponse.EvaluatePermissions(cmdletType, decodedToken, resourceType, tokenType);

            if (permissionEvaluationResponses == null) return;

            if (permissionEvaluationResponses.Any(p => p.RequiredPermissionsPresent)) return;

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

            // Log an error that the permission check failed. Deliberately not throwing an exception here, as the permission attributes might be wrong, thus will try to execute anyway.
            PnP.Framework.Diagnostics.Log.Error("TokenHandler", exceptionTextBuilder.ToString().Replace(Environment.NewLine, " "));
        }

        /// <summary>
        /// Returns an oAuth JWT access token
        /// </summary>
        /// <param name="audience">Audience to retrieve the token for</param>
        /// <param name="connection">The connection to use to make the token calls</param>
        /// <returns>oAuth JWT token</returns>
        /// <exception cref="PSInvalidOperationException">Thrown if retrieval of the token fails</exception>
        internal static string GetAccessToken(string audience, PnPConnection connection)
        {
            if (connection == null) return null;
            if (string.IsNullOrWhiteSpace(audience))
            {
                throw new PSInvalidOperationException("An audience must be provided to acquire an access token.");
            }

            var requestedAudience = audience.TrimEnd('/');

            string accessToken = null;
            if (connection.ConnectionMethod == ConnectionMethod.AzureADWorkloadIdentity)
            {
                PnP.Framework.Diagnostics.Log.Debug("TokenHandler", $"Acquiring token for resource {requestedAudience} using Azure AD Workload Identity");
                accessToken = GetAzureADWorkloadIdentityTokenAsync(requestedAudience).GetAwaiter().GetResult();
            }
            else if (connection.ConnectionMethod == ConnectionMethod.FederatedIdentity)
            {
                PnP.Framework.Diagnostics.Log.Debug("TokenHandler", $"Acquiring token for resource {requestedAudience} using Federated Identity");
                accessToken = GetFederatedIdentityTokenAsync(connection.ClientId, connection.Tenant, requestedAudience).GetAwaiter().GetResult();
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

                        accessToken = authManager.GetAccessToken(requestedAudience);
                    }
                }
            }
            if (string.IsNullOrEmpty(accessToken))
            {
                PnP.Framework.Diagnostics.Log.Debug("TokenHandler", $"Unable to acquire token for resource {requestedAudience}");
                return null;
            }

            return accessToken;
        }

        /// <summary>
        /// Returns an access token based on a Azure AD Workload Identity. Only works within Azure components supporting workload identities.
        /// </summary>
        /// <param name="requiredScope">The permission scope to be requested, in the format https://<resource>/<scope>, i.e. https://graph.microsoft.com/Group.Read.All</param>
        /// <returns>Access token</returns>
        /// <exception cref="PSInvalidOperationException">Thrown if unable to retrieve an access token through a managed identity</exception>
        internal static async Task<string> GetAzureADWorkloadIdentityTokenAsync(string requiredScope)
        {
            if (string.IsNullOrWhiteSpace(requiredScope))
            {
                throw new PSInvalidOperationException("A required scope must be provided to acquire an access token using Azure AD Workload Identity.");
            }

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

            var missingEnvironmentVariables = new List<string>();
            if (string.IsNullOrWhiteSpace(clientID)) missingEnvironmentVariables.Add("AZURE_CLIENT_ID");
            if (string.IsNullOrWhiteSpace(tokenPath)) missingEnvironmentVariables.Add("AZURE_FEDERATED_TOKEN_FILE");
            if (string.IsNullOrWhiteSpace(tenantID)) missingEnvironmentVariables.Add("AZURE_TENANT_ID");
            if (string.IsNullOrWhiteSpace(host)) missingEnvironmentVariables.Add("AZURE_AUTHORITY_HOST");

            if (missingEnvironmentVariables.Count > 0)
            {
                var message = $"Azure AD Workload Identity expects Azure workload identity environment variables to be available. Missing: {string.Join(", ", missingEnvironmentVariables)}.";

                throw new PSInvalidOperationException(message);
            }

            if (!System.IO.File.Exists(tokenPath))
            {
                throw new PSInvalidOperationException($"Azure AD Workload Identity expects AZURE_FEDERATED_TOKEN_FILE to point to an existing token file. Current value: '{tokenPath}'.");
            }

            string federatedToken;
            try
            {
                federatedToken = System.IO.File.ReadAllText(tokenPath);
            }
            catch (Exception ex) when (ex is System.IO.IOException || ex is UnauthorizedAccessException || ex is ArgumentException || ex is NotSupportedException)
            {
                throw new PSInvalidOperationException($"Failed to read the Azure AD Workload Identity token file configured through AZURE_FEDERATED_TOKEN_FILE ('{tokenPath}'). {ex.Message}", ex);
            }

            // tokenPath is included in the key so that the cached app is invalidated if the path
            // changes (e.g. in tests or multi-identity sessions). clientID/tenantID/host are stable
            // per workload-identity configuration; tokenPath is also stable in production but may
            // differ across runs in other contexts.
            var cacheKey = $"{clientID}:{tenantID}:{host}:{tokenPath}";
            var confidentialClientApp = _confidentialClientAppCache.GetOrAdd(cacheKey, _ =>
                ConfidentialClientApplicationBuilder.Create(clientID)
                    .WithAuthority(host, tenantID)
                    // Always read the token file fresh so that MSAL picks up the latest Kubernetes
                    // service account token when it needs to acquire or refresh an Azure AD token.
                    // Kubernetes rotates the file before the current token expires; capturing the
                    // string value once would cause silent-refresh failures after the first rotation.
                    .WithClientAssertion(() => System.IO.File.ReadAllText(tokenPath))
                    .WithCacheOptions(CacheOptions.EnableSharedCacheOptions)
                    .Build());

            AuthenticationResult result = null;
            try
            {
                result = await confidentialClientApp
                            .AcquireTokenForClient(new string[] { requiredScope })
                            .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                throw new PSInvalidOperationException(FormatMsalErrorMessage("Unable to acquire an Azure AD Workload Identity access token because the application does not have sufficient permissions", ex), ex);
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011", StringComparison.OrdinalIgnoreCase) || string.Equals(ex.ErrorCode, "invalid_scope", StringComparison.OrdinalIgnoreCase))
            {
                throw new PSInvalidOperationException($"Unable to acquire an Azure AD Workload Identity access token because the requested scope '{requiredScope}' is invalid. The scope must be in the form 'https://resourceurl/.default'. {ex.Message}", ex);
            }
            catch (MsalServiceException ex)
            {
                throw new PSInvalidOperationException(FormatMsalErrorMessage("Unable to acquire an Azure AD Workload Identity access token", ex), ex);
            }
            catch (MsalClientException ex)
            {
                throw new PSInvalidOperationException(FormatMsalErrorMessage("Unable to acquire an Azure AD Workload Identity access token", ex), ex);
            }

            if (string.IsNullOrWhiteSpace(result?.AccessToken))
            {
                throw new PSInvalidOperationException("Unable to acquire an Azure AD Workload Identity access token because the identity provider response did not contain an access token.");
            }

            return result.AccessToken;
        }

        /// <summary>
        /// Returns an access token based on a Federated Identity. Only works within Azure components supporting federated identities like GitHub/AzureDevOps.
        /// </summary>
        /// <param name="clientId">The client Id of the Federated Identity application</param>
        /// <param name="tenant">The tenant Id of the Federated Identity application</param>
        /// <param name="requiredScope">The permission scope to be requested, in the format https://<resource>/<scope>, i.e. https://graph.microsoft.com/Group.Read.All</param>
        /// <returns>Access token</returns>
        /// <exception cref="PSInvalidOperationException">Thrown if unable to retrieve an access token through a managed identity</exception>
        internal static async Task<string> GetFederatedIdentityTokenAsync(string clientId, string tenant, string requiredScope)
        {
            if (string.IsNullOrWhiteSpace(requiredScope))
            {
                throw new PSInvalidOperationException("A required scope must be provided to acquire an access token using Federated Identity.");
            }

            var actionsIdTokenRequestUrl = Environment.GetEnvironmentVariable("ACTIONS_ID_TOKEN_REQUEST_URL");
            var actionsIdTokenRequestToken = Environment.GetEnvironmentVariable("ACTIONS_ID_TOKEN_REQUEST_TOKEN");
            var systemOidcRequestUri = Environment.GetEnvironmentVariable("SYSTEM_OIDCREQUESTURI");

            if (!string.IsNullOrWhiteSpace(actionsIdTokenRequestUrl) && !string.IsNullOrWhiteSpace(actionsIdTokenRequestToken))
            {
                if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(tenant))
                {
                    throw new PSInvalidOperationException("ClientId and Tenant must be provided when using Federated Identity in GitHub Actions.");
                }

                Framework.Diagnostics.Log.Debug("TokenHandler", "ACTIONS_ID_TOKEN_REQUEST_URL and ACTIONS_ID_TOKEN_REQUEST_TOKEN env variables found. The context is GitHub Actions...");

                var federationToken = await GetFederationTokenFromGithubAsync(actionsIdTokenRequestUrl, actionsIdTokenRequestToken);
                return await GetAccessTokenWithFederatedTokenAsync(clientId, tenant, requiredScope, federationToken);
            }
            else if (!string.IsNullOrWhiteSpace(systemOidcRequestUri))
            {
                Framework.Diagnostics.Log.Debug("TokenHandler", "SYSTEM_OIDCREQUESTURI env variable found. The context is Azure DevOps...");

                var systemAccessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN");
                if (string.IsNullOrWhiteSpace(systemAccessToken))
                {
                    throw new PSInvalidOperationException("The SYSTEM_ACCESSTOKEN environment variable is not available. Please check the Azure DevOps pipeline task configuration. It should contain 'SYSTEM_ACCESSTOKEN: $(System.AccessToken)' in the env section.");
                }

                var serviceConnectionId = Environment.GetEnvironmentVariable("AZURESUBSCRIPTION_SERVICE_CONNECTION_ID");
                var serviceConnectionAppId = Environment.GetEnvironmentVariable("AZURESUBSCRIPTION_CLIENT_ID");
                var serviceConnectionTenantId = Environment.GetEnvironmentVariable("AZURESUBSCRIPTION_TENANT_ID");
                var useServiceConnection = !string.IsNullOrWhiteSpace(serviceConnectionId) &&
                                          !string.IsNullOrWhiteSpace(serviceConnectionAppId) &&
                                          !string.IsNullOrWhiteSpace(serviceConnectionTenantId);

                if (!useServiceConnection)
                {
                    throw new PSInvalidOperationException("The Azure DevOps pipeline task is not configured to use a service connection. Please check the pipeline configuration and ensure that the service connection is set up correctly.");
                }

                Framework.Diagnostics.Log.Debug("TokenHandler", $"Using service connection '{serviceConnectionId}' with app Id '{serviceConnectionAppId}' and tenant Id '{serviceConnectionTenantId}'...");

                var federationToken = await GetFederationTokenFromAzureDevOpsAsync(systemOidcRequestUri, systemAccessToken, serviceConnectionId);
                return await GetAccessTokenWithFederatedTokenAsync(serviceConnectionAppId, serviceConnectionTenantId, requiredScope, federationToken);
            }
            else
            {
                throw new PSInvalidOperationException("Federated identity is currently only supported in GitHub Actions and Azure DevOps.");
            }
        }

        private static async Task<string> GetFederationTokenFromGithubAsync(string requestUrlBase, string requestToken)
        {
            try
            {
                Framework.Diagnostics.Log.Debug("TokenHandler", "Retrieving GitHub federation token...");

                var requestUrl = $"{requestUrlBase}&audience={UrlUtilities.UrlEncode("api://AzureADTokenExchange")}";

                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", requestToken);
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("x-anonymous", "true");

                using var response = await httpClient.SendAsync(requestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    responseContent = responseContent.Replace("{", "{{").Replace("}", "}}");
                    throw new HttpRequestException($"Failed to retrieve GitHub federation token. HTTP Error {response.StatusCode}: {responseContent}");
                }

                Framework.Diagnostics.Log.Debug("TokenHandler", "Successfully retrieved GitHub federation token...");
                return GetRequiredJsonStringProperty(responseContent, "value", "GitHub federation token response");
            }
            catch (PSInvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Framework.Diagnostics.Log.Error("TokenHandler GitHub", ex.Message);
                throw new PSInvalidOperationException($"Failed to retrieve GitHub federation token: {ex.Message}", ex);
            }
        }

        private static async Task<string> GetFederationTokenFromAzureDevOpsAsync(string requestUri, string systemAccessToken, string serviceConnectionId)
        {
            try
            {
                Framework.Diagnostics.Log.Debug("TokenHandler", "Retrieving Azure DevOps federation token...");

                var requestUrl = $"{requestUri}?api-version=7.1&serviceConnectionId={serviceConnectionId}";

                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                requestMessage.Content = new StringContent("", Encoding.UTF8, "application/json");
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", systemAccessToken);
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("x-anonymous", "true");
                // Prevents the service from responding with a redirect HTTP status code (useful for automation).
                requestMessage.Headers.Add("X-TFS-FedAuthRedirect", "Suppress");

                using var response = await httpClient.SendAsync(requestMessage);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    responseContent = responseContent.Replace("{", "{{").Replace("}", "}}");
                    throw new HttpRequestException($"Failed to retrieve Azure DevOps federation token. HTTP Error {response.StatusCode}: {responseContent}");
                }

                Framework.Diagnostics.Log.Debug("TokenHandler", "Successfully retrieved Azure DevOps federation token...");
                return GetRequiredJsonStringProperty(responseContent, "oidcToken", "Azure DevOps federation token response");
            }
            catch (PSInvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Framework.Diagnostics.Log.Error("TokenHandler AzureDevOps", ex.Message);
                throw new PSInvalidOperationException($"Failed to retrieve Azure DevOps federation token: {ex.Message}", ex);
            }
        }

        private static async Task<string> GetAccessTokenWithFederatedTokenAsync(string clientId, string tenant, string resource, string federatedToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(clientId)) throw new PSInvalidOperationException("A client Id must be provided to acquire a federated access token.");
                if (string.IsNullOrWhiteSpace(tenant)) throw new PSInvalidOperationException("A tenant Id must be provided to acquire a federated access token.");
                if (string.IsNullOrWhiteSpace(resource)) throw new PSInvalidOperationException("A resource scope must be provided to acquire a federated access token.");
                if (string.IsNullOrWhiteSpace(federatedToken)) throw new PSInvalidOperationException("A federation token must be provided to acquire a federated access token.");

                Framework.Diagnostics.Log.Debug("TokenHandler", "Retrieving Entra ID access token with federated token...");
                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();

                var requestData = new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials",
                    ["scope"] = resource,
                    ["client_id"] = clientId,
                    ["client_assertion_type"] = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer",
                    ["client_assertion"] = federatedToken
                };

                var requestUrl = $"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token";

                using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                request.Content = new FormUrlEncodedContent(requestData);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("x-anonymous", "true");

                using var response = await httpClient.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    responseContent = responseContent.Replace("{", "{{").Replace("}", "}}");
                    throw new HttpRequestException($"Failed to retrieve federated access token. HTTP Error {response.StatusCode}: {responseContent}");
                }

                Framework.Diagnostics.Log.Debug("TokenHandler", "Successfully retrieved federated access token...");
                return GetRequiredJsonStringProperty(responseContent, "access_token", "Entra ID access token response");
            }
            catch (PSInvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Framework.Diagnostics.Log.Error("TokenHandler", ex.Message);
                throw new PSInvalidOperationException($"Failed to retrieve federated access token: {ex.Message}", ex);
            }
        }

        private static string SanitizeAudience(string audience)
        {
            if (string.IsNullOrWhiteSpace(audience))
            {
                return null;
            }

            var sanitizedAudience = audience.Trim().TrimEnd('/').ToLowerInvariant();
            if (sanitizedAudience.StartsWith("http://", StringComparison.Ordinal))
            {
                return sanitizedAudience["http://".Length..];
            }

            if (sanitizedAudience.StartsWith("https://", StringComparison.Ordinal))
            {
                return sanitizedAudience["https://".Length..];
            }

            return sanitizedAudience;
        }

        private static string GetClaimValue(JsonWebToken decodedToken, string claimType)
        {
            return decodedToken.Claims.FirstOrDefault(c => c.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase))?.Value;
        }

        private static bool TryReadToken(string accessToken, out JsonWebToken decodedToken)
        {
            decodedToken = null;
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return false;
            }

            try
            {
                decodedToken = new JsonWebToken(accessToken);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }

        private static string GetRequiredJsonStringProperty(string responseContent, string propertyName, string responseDescription)
        {
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                throw new PSInvalidOperationException($"Unable to parse the {responseDescription} because the response body was empty.");
            }

            try
            {
                using var jsonDocument = JsonDocument.Parse(responseContent);
                if (!jsonDocument.RootElement.TryGetProperty(propertyName, out var propertyValue) || propertyValue.ValueKind != JsonValueKind.String)
                {
                    throw new PSInvalidOperationException($"Unable to parse the {responseDescription} because the '{propertyName}' property was missing.");
                }

                var value = propertyValue.GetString();
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new PSInvalidOperationException($"Unable to parse the {responseDescription} because the '{propertyName}' property was empty.");
                }

                return value;
            }
            catch (JsonException ex)
            {
                throw new PSInvalidOperationException($"Unable to parse the {responseDescription} because the response body was not valid JSON. {ex.Message}", ex);
            }
        }

        private static string FormatMsalErrorMessage(string message, MsalException exception)
        {
            var errorCode = string.IsNullOrWhiteSpace(exception.ErrorCode) ? string.Empty : $" ({exception.ErrorCode})";
            return $"{message}{errorCode}. {exception.Message}";
        }
    }
}
