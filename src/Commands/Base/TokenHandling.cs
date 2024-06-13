using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Base
{
    internal static class TokenHandler
    {
        /// <summary>
        /// Returns the type of oAuth JWT token being passed in (Delegate/AppOnly)
        /// </summary>
        /// <param name="token">The oAuth JWT token</param>
        /// <returns>Enum indicating the type of oAuth JWT token</returns>
        internal static Enums.IdType RetrieveTokenType(string token)
        {
            var decodedToken = new JwtSecurityToken(token);

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
        /// Extracts the oAuth JWT token to compare the permissions in it (roles) with the required permissions for the cmdlet provided through an attribute
        /// </summary>
        /// <param name="cmdletType">The cmdlet that will be executed. Used to check for the permissions attribute.</param>
        /// <param name="token">The oAuth JWT token that needs to be validated for its roles</param>
        /// <exception cref="PSArgumentException">Thrown if the permissions set through the permissions attribute do not match the roles in the JWT token</exception>
        internal static void ValidateTokenForPermissions(Type cmdletType, string token)
        {
            var decodedToken = new JwtSecurityToken(accessToken);

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
        /// Returns the permission scopes of the oAuth JWT token being passed in
        /// </summary>
        /// <param name="accessToken">The oAuth JWT token</param>
        /// <returns>String array containing the scopes</returns>
        internal static string[] ReturnScopes(string accessToken)
        {
            var decodedToken = new JwtSecurityToken(accessToken);

            // The scopes can either be stored in the roles or scp claim, so we examine both
            var scopes = decodedToken.Claims.Where(c => c.Type == "roles" || c.Type == "scp").SelectMany(r => r.Value.Split(" "));
            return scopes.ToArray();
        }

        /// <summary>
        /// Extracts the oAuth JWT token to compare the permissions in it (roles) with the required permissions for the cmdlet provided through an attribute
        /// </summary>
        /// <param name="cmdletType">The cmdlet that will be executed. Used to check for the permissions attribute.</param>
        /// <param name="accessToken">The oAuth JWT token that needs to be validated for its roles</param>
        /// <exception cref="PSArgumentException">Thrown if the permissions set through the permissions attribute do not match the roles in the JWT token</exception>
        internal static void ValidateTokenForPermissions(Type cmdletType, string accessToken)
        {
            string[] requiredScopes = null;
            var requiredScopesAttribute = (RequiredMinimalApiPermissions)Attribute.GetCustomAttribute(cmdletType, typeof(RequiredMinimalApiPermissions));
            if (requiredScopesAttribute != null)
            {
                requiredScopes = requiredScopesAttribute.PermissionScopes;
            }

            // Ensure there are permission attributes present on the cmdlet, otherwise we have nothing to compare it against
            if (requiredScopes == null || requiredScopes.Length == 0) return;

            // Retrieve the scopes we have in our AccessToken
            var scopes = ReturnScopes(accessToken);

            var missingScopes = requiredScopes.Where(rs => !scopes.Contains(rs)).ToArray();

            if (missingScopes.Length == 0) return;

            throw new PSArgumentException($"Authorization Denied: Token used does not contain permission scope{(missingScopes.Length != 1 ? "s" : "")} {string.Concat(missingScopes, ", ")}");
        }

        /// <summary>
        /// Returns an oAuth JWT access token
        /// </summary>
        /// <param name="cmdlet">Cmdlet for which the token is requested</param>
        /// <param name="appOnlyDefaultScope">Audience to retrieve the token for</param>
        /// <param name="connection">The connection to use to make the token calls</param>
        /// <returns>oAuth JWT token</returns>
        /// <exception cref="PSInvalidOperationException">Thrown if retrieval of the token fails</exception>
        internal static string GetAccessToken(Cmdlet cmdlet, string appOnlyDefaultScope, PnPConnection connection)
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

                string[] requiredScopes = null;
                RequiredMinimalApiPermissions requiredScopesAttribute = null;
                if (cmdlet != null && cmdlet.GetType() != null)
                {
                    requiredScopesAttribute = (RequiredMinimalApiPermissions)Attribute.GetCustomAttribute(cmdlet.GetType(), typeof(RequiredMinimalApiPermissions));
                }
                if (requiredScopesAttribute != null)
                {
                    requiredScopes = requiredScopesAttribute.PermissionScopes;
                }
                if (contextSettings.Type == Framework.Utilities.Context.ClientContextType.AzureADCertificate)
                {
                    requiredScopes = new[] { appOnlyDefaultScope }; // override for app only
                }
                if (requiredScopes == null && !string.IsNullOrEmpty(appOnlyDefaultScope))
                {
                    requiredScopes = new[] { appOnlyDefaultScope };
                }

                cmdlet.WriteVerbose($"Acquiring oAuth token for {(requiredScopes.Length != 1 ? requiredScopes.Length + " " : "")}permission scope{(requiredScopes.Length != 1 ? "s" : "")} {string.Join(",", requiredScopes)}");
                var accessToken = authManager.GetAccessTokenAsync(requiredScopes).GetAwaiter().GetResult();                

                // Retrieve the scopes from the access token
                var scopes = ReturnScopes(accessToken);

                cmdlet.WriteVerbose($"Access token acquired containing the following {(scopes.Length != 1 ? scopes.Length + " " : "")}{RetrieveTokenType(accessToken)} scope{(scopes.Length == 1 ? "" : "s")}: {string.Join(", ", scopes)}");
                
                return accessToken;
            }
            return null;
        }

        internal static string GetAccessTokenforPowerPlatformSolutions(Cmdlet cmdlet, PnPConnection connection, string enviormentBaseUrl)
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
                string[] requiredScopes = new string[1] { enviormentBaseUrl + "/.default" };
                cmdlet.WriteVerbose($"Acquiring oAuth token for {(requiredScopes.Length != 1 ? requiredScopes.Length + " " : "")}permission scope{(requiredScopes.Length != 1 ? "s" : "")} {string.Join(",", requiredScopes)}");
                var accessToken = authManager.GetAccessTokenAsync(requiredScopes).GetAwaiter().GetResult();
                cmdlet.WriteVerbose($"Access token acquired for PowerPlatformSolutions: {accessToken}");
                return accessToken;
            }
            return null;
        }

        /// <summary>
        /// Returns an access token based on a Managed Identity. Only works within Azure components supporting managed identities such as Azure Functions and Azure Runbooks.
        /// </summary>
        /// <param name="cmdlet">The cmdlet scope in which this code runs. Used to write logging to.</param>
        /// <param name="httpClient">The HttpClient that will be reused to fetch the token to avoid port exhaustion</param>
        /// <param name="defaultResource">If the cmdlet being executed does not have an attribute to indicate the required permissions, this permission will be requested instead. Optional.</param>
        /// <param name="userAssignedManagedIdentityObjectId">The object/principal Id of the user assigned managed identity to be used. If userAssignedManagedIdentityObjectId. userAssignedManagedIdentityClientId and userAssignedManagedIdentityAzureResourceId are omitted, a system assigned managed identity will be used.</param>
        /// <param name="userAssignedManagedIdentityClientId">The client Id of the user assigned managed identity to be used. If userAssignedManagedIdentityObjectId, userAssignedManagedIdentityClientId and userAssignedManagedIdentityAzureResourceId are omitted, a system assigned managed identity will be used.</param>
        /// <param name="userAssignedManagedIdentityAzureResourceId">The Azure Resource Id of the user assigned managed identity to be used. If userAssignedManagedIdentityObjectId, userAssignedManagedIdentityClientId and userAssignedManagedIdentityAzureResourceId are omitted, a system assigned managed identity will be used.</param>
        /// <returns>Access token</returns>
        /// <exception cref="PSInvalidOperationException">Thrown if unable to retrieve an access token through a managed identity</exception>
        internal static async Task<string> GetManagedIdentityTokenAsync(Cmdlet cmdlet, HttpClient httpClient, string defaultResource, string userAssignedManagedIdentityObjectId = null, string userAssignedManagedIdentityClientId = null, string userAssignedManagedIdentityAzureResourceId = null)
        {
            string requiredScope = null;
            var requiredScopesAttribute = (RequiredMinimalApiPermissions)Attribute.GetCustomAttribute(cmdlet.GetType(), typeof(RequiredMinimalApiPermissions));
            if (requiredScopesAttribute != null)
            {
                requiredScope = requiredScopesAttribute.PermissionScopes.First();
                if (requiredScope.ToLower().StartsWith("https://"))
                {
                    var uri = new Uri(requiredScope);
                    requiredScope = $"https://{uri.Host}/";
                }
                else
                {
                    requiredScope = defaultResource;
                }

                cmdlet.WriteVerbose($"Using scope {requiredScope} for managed identity token coming from the cmdlet permission attribute");
            }
            else
            {
                requiredScope = defaultResource;

                cmdlet.WriteVerbose($"Using scope {requiredScope} for managed identity token coming from the passed in default resource");
            }

            var endPoint = Environment.GetEnvironmentVariable("IDENTITY_ENDPOINT");
            cmdlet.WriteVerbose($"Using identity endpoint: {endPoint}");

            var identityHeader = Environment.GetEnvironmentVariable("IDENTITY_HEADER");
            cmdlet.WriteVerbose($"Using identity header: {identityHeader}");

            if (string.IsNullOrEmpty(endPoint))
            {
                endPoint = Environment.GetEnvironmentVariable("MSI_ENDPOINT");
                identityHeader = Environment.GetEnvironmentVariable("MSI_SECRET");
            }
            if (string.IsNullOrEmpty(endPoint))
            {
                // additional fallback
                // using well-known endpoint for Instance Metadata Service, useful in Azure VM scenario.
                // https://learn.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/how-to-use-vm-token#get-a-token-using-http
                endPoint = "http://169.254.169.254/metadata/identity/oauth2/token";
            }
            if (!string.IsNullOrEmpty(endPoint))
            {
                var tokenRequestUrl = $"{endPoint}?resource={requiredScope}&api-version=2019-08-01";

                // Check if we're using a user assigned managed identity
                if (!string.IsNullOrEmpty(userAssignedManagedIdentityClientId))
                {
                    // User assigned managed identity will be used, provide the client Id of the user assigned managed identity to use
                    cmdlet.WriteVerbose($"Using the user assigned managed identity with client ID: {userAssignedManagedIdentityClientId}");
                    tokenRequestUrl += $"&client_id={userAssignedManagedIdentityClientId}";
                }
                else if (!string.IsNullOrEmpty(userAssignedManagedIdentityObjectId))
                {
                    // User assigned managed identity will be used, provide the object/pricipal Id of the user assigned managed identity to use
                    // Note 16-02-2023: principal_id is an alias of object_id, but does not work on Azure Automation at the time of writing, while object_id works on both.
                    cmdlet.WriteVerbose($"Using the user assigned managed identity with object/principal ID: {userAssignedManagedIdentityObjectId}");
                    tokenRequestUrl += $"&object_id={userAssignedManagedIdentityObjectId}";
                }
                else if (!string.IsNullOrEmpty(userAssignedManagedIdentityAzureResourceId))
                {
                    // User assigned managed identity will be used, provide the Azure Resource Id of the user assigned managed identity to use
                    cmdlet.WriteVerbose($"Using the user assigned managed identity with Azure Resource ID: {userAssignedManagedIdentityAzureResourceId}");
                    tokenRequestUrl += $"&mi_res_id={userAssignedManagedIdentityAzureResourceId}";
                }
                else
                {
                    cmdlet.WriteVerbose("Using the system assigned managed identity");
                }

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, tokenRequestUrl))
                {
                    requestMessage.Version = new Version(2, 0);
                    requestMessage.Headers.Add("Metadata", "true");
                    if (!string.IsNullOrEmpty(identityHeader))
                    {
                        requestMessage.Headers.Add("X-IDENTITY-HEADER", identityHeader);
                    }

                    cmdlet.WriteVerbose($"Sending token request to {tokenRequestUrl}");

                    var response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var responseElement = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        if (responseElement.TryGetProperty("access_token", out JsonElement accessTokenElement))
                        {
                            var accessToken = accessTokenElement.GetString();
                            return accessToken;
                        }
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new PSInvalidOperationException(errorMessage);
                    }
                }
            }
            else
            {
                throw new PSInvalidOperationException("Cannot determine Managed Identity Endpoint URL to acquire token.");
            }
            return null;
        }

        /// <summary>
        /// Returns an access token based on a Azure AD Workload Identity. Only works within Azure components supporting workload identities.
        /// </summary>
        /// <param name="cmdlet">The cmdlet scope in which this code runs. Used to write logging to.</param>
        /// <param name="httpClient">The HttpClient that will be reused to fetch the token to avoid port exhaustion</param>
        /// <param name="defaultResource">If the cmdlet being executed does not have an attribute to indicate the required permissions, this permission will be requested instead. Optional.</param>
        /// <returns>Access token</returns>
        /// <exception cref="PSInvalidOperationException">Thrown if unable to retrieve an access token through a managed identity</exception>
        internal static async Task<string> GetAzureADWorkloadIdentityTokenAsync(Cmdlet cmdlet, string defaultResource)
        {
            string requiredScope = null;
            var requiredScopesAttribute = (RequiredMinimalApiPermissions)Attribute.GetCustomAttribute(cmdlet.GetType(), typeof(RequiredMinimalApiPermissions));
            if (requiredScopesAttribute != null)
            {
                requiredScope = requiredScopesAttribute.PermissionScopes.First();
                if (requiredScope.ToLower().StartsWith("https://"))
                {
                    var uri = new Uri(requiredScope);
                    requiredScope = $"https://{uri.Host}/.default";
                }
                else
                {
                    requiredScope = defaultResource;
                }

                cmdlet.WriteVerbose($"Using scope {requiredScope} for Azure AD Workload identity token coming from the cmdlet permission attribute");
            }
            else
            {
                requiredScope = defaultResource;
                cmdlet.WriteVerbose($"Using scope {requiredScope} for Azure AD Workload identity token coming from the passed in default resource");
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

            var _confidentialClientApp = ConfidentialClientApplicationBuilder.Create(clientID)
                .WithClientAssertion(ReadJWTFromFS(tokenPath))
                .WithTenantId(tenantID).Build();

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

        private static string ReadJWTFromFS(string tokenPath)
        {
            string text = System.IO.File.ReadAllText(tokenPath);
            return text;
        }
    }
}