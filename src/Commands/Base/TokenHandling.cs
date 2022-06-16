using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using System;
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
        internal static void ValidateTokenForPermissions(Type cmdletType, string token)
        {
            string[] requiredScopes = null;
            var requiredScopesAttribute = (RequiredMinimalApiPermissions)Attribute.GetCustomAttribute(cmdletType, typeof(RequiredMinimalApiPermissions));
            if (requiredScopesAttribute != null)
            {
                requiredScopes = requiredScopesAttribute.PermissionScopes;
            }
            if (requiredScopes.Length > 0)
            {
                var decodedToken = new JwtSecurityToken(token);
                var roles = decodedToken.Claims.FirstOrDefault(c => c.Type == "roles");
                if (roles != null)
                {
                    foreach (var permission in requiredScopes)
                    {
                        if (!roles.Value.ToLower().Contains(permission.ToLower()))
                        {
                            throw new PSArgumentException($"Authorization Denied: Token used does not contain permission scope '{permission}'");
                        }
                    }
                }
                roles = decodedToken.Claims.FirstOrDefault(c => c.Type == "scp");
                if (roles != null)
                {
                    foreach (var permission in requiredScopes)
                    {
                        if (!roles.Value.ToLower().Contains(permission.ToLower()))
                        {
                            throw new PSArgumentException($"Authorization Denied: Token used does not contain permission scope '{permission}'");
                        }
                    }
                }
            }
        }

        internal static string GetAccessToken(Type cmdletType, string appOnlyDefaultScope, PnPConnection connection)
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
                if (cmdletType != null)
                {
                    requiredScopesAttribute = (RequiredMinimalApiPermissions)Attribute.GetCustomAttribute(cmdletType, typeof(RequiredMinimalApiPermissions));
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
                var accessToken = authManager.GetAccessTokenAsync(requiredScopes).GetAwaiter().GetResult();
                return accessToken;
            }
            return null;
        }

        internal static async Task<string> GetManagedIdentityTokenAsync(Cmdlet cmdlet, HttpClient httpClient, string defaultResource)
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
            }
            else
            {
                requiredScope = defaultResource;
            }

            var endPoint = Environment.GetEnvironmentVariable("IDENTITY_ENDPOINT");
            var identityHeader = Environment.GetEnvironmentVariable("IDENTITY_HEADER");
            if (string.IsNullOrEmpty(endPoint))
            {
                endPoint = Environment.GetEnvironmentVariable("MSI_ENDPOINT");
                identityHeader = Environment.GetEnvironmentVariable("MSI_SECRET");
            }
            if (!string.IsNullOrEmpty(endPoint))
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{endPoint}?resource={requiredScope}&api-version=2019-08-01"))
                {
                    requestMessage.Headers.Add("Metadata", "true");
                    if (!string.IsNullOrEmpty(identityHeader))
                    {
                        requestMessage.Headers.Add("X-IDENTITY-HEADER", identityHeader);

                    }
                    var response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var responseElement = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        if (responseElement.TryGetProperty("access_token", out JsonElement accessTokenElement))
                        {
                            return accessTokenElement.GetString();
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
    }
}
