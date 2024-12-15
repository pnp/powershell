using System;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Provisioning
{
    public static class TokenRetrieval
    {
        public async static Task<string> GetAccessTokenAsync(string resource, string scope, PnPConnection connection)
        {
            if (resource.ToLower().StartsWith("https://"))
            {
                var uri = new Uri(resource);
                resource = uri.Authority;
            }
            if (connection?.Context != null)
            {
                var settings = connection.Context.GetContextSettings();
                var authManager = settings.AuthenticationManager;
                if (authManager != null)
                {
                    if (resource.ToLower().Contains(".sharepoint."))
                    {
                        return await authManager.GetAccessTokenAsync($"https://{resource}");
                    }
                    else
                    {
                        if (settings.Type == Framework.Utilities.Context.ClientContextType.AzureADCertificate)
                        {
                            scope = $"https://{resource}/.default";
                        }
                        return scope is null ? await authManager.GetAccessTokenAsync($"https://{resource}") : await authManager.GetAccessTokenAsync(new string[] { scope });
                    }
                }
            }
            return null;
        }
    }
}