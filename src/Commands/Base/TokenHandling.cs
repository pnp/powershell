using Microsoft.SharePoint.Client;
using Newtonsoft.Json.Linq;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Web;

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

        internal static string GetAccessToken(Type cmdletType, string appOnlyDefaultScope)
        {
            var contextSettings = PnPConnection.CurrentConnection.Context.GetContextSettings();
            var authManager = contextSettings.AuthenticationManager;
            if (authManager != null)
            {
                string[] requiredScopes = null;
                var requiredScopesAttribute = (RequiredMinimalApiPermissions)Attribute.GetCustomAttribute(cmdletType, typeof(RequiredMinimalApiPermissions));
                if (requiredScopesAttribute != null)
                {
                    requiredScopes = requiredScopesAttribute.PermissionScopes;
                }
                if (contextSettings.Type == Framework.Utilities.Context.ClientContextType.AzureADCertificate)
                {
                    requiredScopes = new[] { appOnlyDefaultScope }; // override for app only
                }
                var accessToken = authManager.GetAccessTokenAsync(requiredScopes).GetAwaiter().GetResult();
                return accessToken;
            }
            return null;
        }
    }
}
