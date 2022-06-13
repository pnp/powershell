using System;
using System.Management.Automation;
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

        public PnPContext PnPContext => Connection?.PnPContext ?? Connection.PnPContext;

        public new HttpClient HttpClient => PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient(ClientContext);

        // do not remove '#!#99'
        [Parameter(Mandatory = false, HelpMessage = "Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.")]
        public PnPConnection Connection = null;
        // do not remove '#!#99'

        protected override void BeginProcessing()
        {
            // Call the base but instruct it not to check if there's an active connection as we will do that in this method already
            base.BeginProcessing(true);

            // If a specific connection has been provided, use that, otherwise use the current connection
            if(Connection == null)
            {
                Connection = PnPConnection.Current;
            }

            // Track the execution of the cmdlet
            if (Connection != null && Connection.ApplicationInsights != null)
            {
                Connection.ApplicationInsights.TrackEvent(MyInvocation.MyCommand.Name);
            }

            // Ensure there is an active connection to work with
            if (Connection == null || ClientContext == null)
            {
                throw new InvalidOperationException(Resources.NoSharePointConnection);
            }
        }

        protected override void ProcessRecord()
        {
            try
            {
                var tag = Connection.PnPVersionTag + ":" + MyInvocation.MyCommand.Name;
                if (tag.Length > 32)
                {
                    tag = tag.Substring(0, 32);
                }
                ClientContext.ClientTag = tag;

                ExecuteCmdlet();
            }
            catch (PipelineStoppedException)
            {
                //don't swallow pipeline stopped exception
                //it makes select-object work weird
                throw;
            }
            catch (PnP.Core.SharePointRestServiceException ex)
            {
                throw new PSInvalidOperationException((ex.Error as PnP.Core.SharePointRestError).Message);
            }
            catch (PnP.PowerShell.Commands.Model.Graph.GraphException gex)
            {
                throw new PSInvalidOperationException((gex.Message));
            }
            catch (Exception ex)
            {
                Connection.RestoreCachedContext(Connection.Url);
                ex.Data["CorrelationId"] = Connection.Context.TraceCorrelationId;
                ex.Data["TimeStampUtc"] = DateTime.UtcNow;
                var errorDetails = new ErrorDetails(ex.Message);

                errorDetails.RecommendedAction = "Use Get-PnPException for more details.";
                var errorRecord = new ErrorRecord(ex, "EXCEPTION", ErrorCategory.WriteError, null);
                errorRecord.ErrorDetails = errorDetails;

                WriteError(errorRecord);
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        protected string AccessToken
        {
            get
            {
                if (Connection != null)
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
                return null;
            }
        }

        public string GraphAccessToken
        {
            get
            {
                if (Connection?.ConnectionMethod == ConnectionMethod.ManagedIdentity)
                {
                    return TokenHandler.GetManagedIdentityTokenAsync(this, HttpClient, $"https://{Connection.GraphEndPoint}/").GetAwaiter().GetResult();
                }
                else
                {
                    if (Connection?.Context != null)
                    {
                        return TokenHandler.GetAccessToken(GetType(), $"https://{Connection.GraphEndPoint}/.default");
                    }
                }

                return null;
            }
        }

        protected void PollOperation(SpoOperation spoOperation)
        {
            while (true)
            {
                if (!spoOperation.IsComplete)
                {
                    if (spoOperation.HasTimedout)
                    {
                        throw new TimeoutException("SharePoint Operation Timeout");
                    }
                    Thread.Sleep(spoOperation.PollingInterval);
                    if (Stopping)
                    {
                        break;
                    }
                    ClientContext.Load(spoOperation);
                    ClientContext.ExecuteQueryRetry();
                    continue;
                }
                return;
            }
            WriteWarning("SharePoint Operation Wait Interrupted");
        }

    }
}
