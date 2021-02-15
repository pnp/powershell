using Microsoft.Graph;
using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Properties;
using System;
using System.Collections.Generic;
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
        public ClientContext ClientContext => Connection?.Context ?? PnPConnection.CurrentConnection.Context;

        public PnPContext PnPContext => Connection?.PnPContext ?? PnPConnection.CurrentConnection.PnPContext;

        // do not remove '#!#99'
        [Parameter(Mandatory = false, HelpMessage = "Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.")]
        public PnPConnection Connection = null;
        // do not remove '#!#99'

        private GraphServiceClient serviceClient;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (PnPConnection.CurrentConnection?.Context != null)
            {
                var contextSettings = PnPConnection.CurrentConnection.Context.GetContextSettings();
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
                if (PnPConnection.CurrentConnection?.ConnectionMethod == ConnectionMethod.ManagedIdentity)
                {
                    return TokenHandler.GetManagedIdentityTokenAsync(this, HttpClient, "https://graph.microsoft.com/").GetAwaiter().GetResult();
                }
                else
                {
                    if (PnPConnection.CurrentConnection?.Context != null)
                    {
                        return TokenHandler.GetAccessToken(GetType(), "https://graph.microsoft.com/.default");
                    }
                }

                return null;
            }
        }

        internal GraphServiceClient ServiceClient
        {
            get
            {
                if (serviceClient == null)
                {
                    serviceClient = new GraphServiceClient(new DelegateAuthenticationProvider(
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