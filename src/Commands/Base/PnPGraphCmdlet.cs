using Microsoft.Graph;
using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Model;
using System.Management.Automation;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Microsoft Graph related cmdlets
    /// </summary>
    public abstract class PnPGraphCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// Reference the the SharePoint context on the current connection. If NULL it means there is no SharePoint context available on the current connection.
        /// </summary>
        public ClientContext ClientContext => Connection?.Context;

        public PnPContext PnPContext => Connection?.PnPContext;

        private GraphServiceClient serviceClient;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (Connection?.Context != null)
            {
                var contextSettings = Connection.Context.GetContextSettings();
                if (contextSettings?.Type == Framework.Utilities.Context.ClientContextType.Cookie || contextSettings?.Type == Framework.Utilities.Context.ClientContextType.SharePointACSAppOnly)
                {
                    var typeString = contextSettings?.Type == Framework.Utilities.Context.ClientContextType.Cookie ? "WebLogin/Cookie" : "ACS";
                    throw new PSInvalidOperationException($"This cmdlet does not work with a {typeString} based connection towards SharePoint.");
                }
            }
        }

        /// <summary>
        /// Returns an Access Token for the Microsoft Graph API, if available, otherwise NULL
        /// </summary>
        public string AccessToken
        {
            get
            {
                if (Connection != null)
                {
                    if (Connection.ConnectionMethod == ConnectionMethod.ManagedIdentity)
                    {
                        WriteVerbose("Acquiring token for resource " + Connection.GraphEndPoint + " using Managed Identity");
                        var accessToken = TokenHandler.GetManagedIdentityTokenAsync(this, Connection.HttpClient, $"https://{Connection.GraphEndPoint}/", Connection.UserAssignedManagedIdentityObjectId, Connection.UserAssignedManagedIdentityClientId, Connection.UserAssignedManagedIdentityAzureResourceId).GetAwaiter().GetResult();

                        return accessToken;
                    }
                    else if (Connection.ConnectionMethod == ConnectionMethod.AzureADWorkloadIdentity)
                    {
                        WriteVerbose("Acquiring token for resource " + Connection.GraphEndPoint + " using Entra ID Workload Identity");
                        var accessToken = TokenHandler.GetEntraIDWorkloadIdentityTokenAsync(this, $"https://{Connection.GraphEndPoint}/.default").GetAwaiter().GetResult();

                        return accessToken;
                    }
                    else
                    {
                        if (Connection.Context != null)
                        {
                            var accessToken = TokenHandler.GetAccessToken(this, $"https://{Connection.GraphEndPoint}/.default", Connection);
                            return accessToken;
                        }
                    }
                }
                WriteVerbose("Unable to acquire token for resource " + Connection.GraphEndPoint);
                return null;
            }
        }

        internal GraphServiceClient ServiceClient
        {
            get
            {
                if (serviceClient == null)
                {
                    var baseUrl = $"https://{Connection.GraphEndPoint}/v1.0";
                    serviceClient = new GraphServiceClient(baseUrl, new DelegateAuthenticationProvider(
                            async (requestMessage) =>
                            {
                                await Task.Run(() =>
                                {
                                    if (!string.IsNullOrEmpty(AccessToken))
                                    {
                                        // Configure the HTTP bearer Authorization Header
                                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
                                    }
                                });
                            }), new HttpProvider());
                }
                return serviceClient;
            }
        }
    }
}