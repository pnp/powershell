using System;
using System.Net.Http;
using System.Threading;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using TokenHandler = PnP.PowerShell.Commands.Base.TokenHandler;

namespace PnP.PowerShell.Commands
{
    /// <summary>
    /// Base class for all the PnP SharePoint related cmdlets
    /// </summary>
    public abstract class PnPSharePointCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// Reference the the SharePoint context on the current connection. If NULL it means there is no SharePoint context available on the current connection.
        /// </summary>
        public ClientContext ClientContext => Connection?.Context;

        /// <summary>
        /// Reference the the PnP context on the current connection. If NULL it means there is no PnP context available on the current connection.
        /// </summary>
        public PnPContext PnPContext => Connection?.PnPContext ?? Connection.PnPContext;

        /// <summary>
        /// HttpClient based off of the ClientContext that can be used to make raw HTTP calls to SharePoint Online
        /// </summary>
        public HttpClient HttpClient => Framework.Http.PnPHttpClient.Instance.GetHttpClient(ClientContext);

        /// <summary>
        /// The current Bearer access token for SharePoint Online
        /// </summary>
        protected string AccessToken
        {
            get
            {
                if (Connection != null)
                {
                    if (Connection.ConnectionMethod == ConnectionMethod.AzureADWorkloadIdentity)
                    {
                        var resourceUri = new Uri(Connection.Url);
                        var defaultResource = $"{resourceUri.Scheme}://{resourceUri.Authority}/.default";
                        return TokenHandler.GetAzureADWorkloadIdentityTokenAsync(this, defaultResource).GetAwaiter().GetResult();
                    }
                    else
                    {
                        if (Connection.Context != null)
                        {
                            var settings = Microsoft.SharePoint.Client.InternalClientContextExtensions.GetContextSettings(Connection.Context);
                            if (settings != null)
                            {
                                var authManager = settings.AuthenticationManager;
                                if (authManager != null)
                                {
                                    return authManager.GetAccessTokenAsync(Connection.Context.Url).GetAwaiter().GetResult();
                                }
                            }
                        }
                    }
                }
                WriteVerbose("Unable to acquire token for resource " + Connection.Url);
                return null;
            }
        }

        /// <summary>
        /// The current Bearer access token for Microsoft Graph
        /// </summary>
        public string GraphAccessToken
        {
            get
            {
                if (Connection?.ConnectionMethod == ConnectionMethod.AzureADWorkloadIdentity)
                {
                    return TokenHandler.GetAzureADWorkloadIdentityTokenAsync(this, $"https://{Connection.GraphEndPoint}/.default").GetAwaiter().GetResult();
                }
                else
                {
                    if (Connection?.Context != null)
                    {
                        return TokenHandler.GetAccessToken(this, $"https://{Connection.GraphEndPoint}/.default", Connection);
                    }
                }
                WriteVerbose("Unable to acquire token for resource " + Connection.GraphEndPoint);
                return null;
            }
        }

        protected override void BeginProcessing()
        {
            // Call the base but instruct it not to check if there's an active connection as we will do that in this method already
            base.BeginProcessing(true);            

            // Ensure there is an active connection to work with
            if (Connection == null || ClientContext == null)
            {
                if (ParameterSpecified(nameof(Connection)))
                {
                    throw new InvalidOperationException(Resources.NoSharePointConnectionInProvidedConnection);
                }
                else
                {
                    throw new InvalidOperationException(Resources.NoDefaultSharePointConnection);
                }
            }
        }

        protected override void ProcessRecord()
        {
            var tag = Connection.PnPVersionTag + ":" + MyInvocation.MyCommand.Name;
            if (tag.Length > 32)
            {
                tag = tag.Substring(0, 32);
            }
            ClientContext.ClientTag = tag;
            
            WriteVerbose("Making call to SharePoint Online using the Client Side Object Model (CSOM)");

            // Client Credentials based connections do not have an access token, so we can't validate permissions
            if(Connection.ConnectionMethod != ConnectionMethod.Credentials)
            {
                // Validate the permissions in the access token for SharePoint Online
                TokenHandler.EnsureRequiredPermissionsAvailableInAccessTokenAudience(this, AccessToken);
            }

            base.ProcessRecord();
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        /// <summary>
        /// Waits for the SpoOperation to complete
        /// </summary>
        /// <param name="spoOperation">The operation to wait for to be completed</param>
        /// <exception cref="TimeoutException">Exception thrown when the waiting operation takes too long and times out</exception>
        protected void PollOperation(SpoOperation spoOperation)
        {
            while (true)
            {
                if (spoOperation.IsComplete)
                {
                    WriteVerbose("Operation completed");
                    return;
                }
                if (spoOperation.HasTimedout)
                {
                    WriteVerbose("Operation timed out");
                    throw new TimeoutException("SharePoint Operation Timeout");
                }

                Thread.Sleep(spoOperation.PollingInterval);

                if (Stopping)
                {
                    break;
                }

                WriteVerbose("Checking for operation status");
                ClientContext.Load(spoOperation);
                ClientContext.ExecuteQueryRetry();
            }
            WriteWarning("SharePoint Operation Wait Interrupted");
        }
    }
}
