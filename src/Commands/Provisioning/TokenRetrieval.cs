using System;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;
using System.Threading.Tasks;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning
{
    public static class TokenRetrieval
    {
        public async static Task<string> GetAccessTokenAsync(string resource, string scope)
        {
            if (resource.ToLower().StartsWith("https://"))
            {
                var uri = new Uri(resource);
                resource = uri.Authority;
            }
            if (PnPConnection.CurrentConnection?.Context != null)
            {
                var authManager = PnPConnection.CurrentConnection.Context.GetContextSettings().AuthenticationManager;
                if (authManager != null)
                {
                    if (resource.ToLower().Contains(".sharepoint."))
                    {
                        return await authManager.GetAccessTokenAsync($"https://{resource}");
                    }
                    else
                    {
                        return await authManager.GetAccessTokenAsync(new string[] { scope });
                    }
                }
            }
            // Get Azure AD Token
            if (PnPConnection.CurrentConnection != null)
            {
                var graphAccessToken = await PnPConnection.CurrentConnection.TryGetAccessTokenAsync(Enums.TokenAudience.MicrosoftGraph);
                if (graphAccessToken != null)
                {
                    // Authenticated using -Graph or using another way to retrieve the accesstoken with Connect-PnPOnline
                    return graphAccessToken;
                }
            }

            if (PnPConnection.CurrentConnection.PSCredential != null)
            {
                // Using normal credentials
                return await TokenHandler.AcquireTokenAsync(resource, null);
            }
            else
            {
                // No token...
                if (resource.ToLower().Contains(".sharepoint."))
                {
                    return null;
                }
                else
                {
                    throw new PSInvalidOperationException($"Your template contains artifacts that require an access token for {resource}. Either connect with a clientid which the appropriate permissions, or use credentials with Connect-PnPOnline after providing consent to the PnP Management Shell application first by executing: Register-PnPManagementShellAccess. See https://pnp.github.io/powershell/articles/authentication.html");
                }
            }
        }
    }
}