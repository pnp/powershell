using Microsoft.Graph;
using Microsoft.SharePoint.Client;
using PnP.Core.Services;
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
        public string AccessToken => TokenHandler.GetAccessToken(this, $"https://{Connection.GraphEndPoint}/.default", Connection);

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