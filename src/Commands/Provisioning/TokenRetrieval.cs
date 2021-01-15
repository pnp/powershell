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
                var settings = PnPConnection.CurrentConnection.Context.GetContextSettings();
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
                            scope = $"{resource}/.default";
                        }
                        return await authManager.GetAccessTokenAsync(new string[] { scope });
                    }
                }
            }
            return null;
        }
    }
}