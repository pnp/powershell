using System;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{

    /// <summary>
    /// Base class for all the PnP Cmdlets which require a connection to have been made
    /// </summary>
    public abstract class PnPConnectedCmdlet : BasePSCmdlet
    {
        // do not remove '#!#99'
        [Parameter(Mandatory = false, HelpMessage = "Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.")]
        public PnPConnection Connection = null;
        // do not remove '#!#99'

        protected override void BeginProcessing()
        {
            BeginProcessing(false);
        }

        protected void BeginProcessing(bool skipConnectedValidation)
        {
            base.BeginProcessing();

            // If a specific connection has been provided, use that, otherwise use the current connection
            if (Connection == null)
            {
                Connection = PnPConnection.Current;
            }

            // Track the execution of the cmdlet in Azure Application Insights
            if (Connection != null && Connection.ApplicationInsights != null)
            {
                Connection.ApplicationInsights.TrackEvent(MyInvocation.MyCommand.Name);
            }

            // Check if we should ensure that we are connected
            if (skipConnectedValidation) return;

            // Ensure there is an active connection
            if (Connection == null)
            {
                throw new InvalidOperationException(Properties.Resources.NoConnection);
            }

        }

        protected override void ProcessRecord()
        {
            try
            {
                ExecuteCmdlet();
            }
            catch (PipelineStoppedException)
            {
                // Don't swallow pipeline stopped exception, it makes select-object work weird
                throw;
            }
            catch (Exception ex)
            {
                string errorMessage;
                switch (ex)
                {
                    case Model.Graph.GraphException gex:
                        errorMessage = $"{gex.HttpResponse.ReasonPhrase} ({(int)gex.HttpResponse.StatusCode}): {(gex.Error != null ? gex.Error.Message : gex.HttpResponse.Content.ReadAsStringAsync().Result)}";
                        break;
                    case Core.CsomServiceException cex:
                        errorMessage = (cex.Error as Core.CsomError).Message;
                        break;
                    case Core.SharePointRestServiceException rex:
                        errorMessage = (rex.Error as Core.SharePointRestError).Message;
                        break;
                    case System.Reflection.TargetInvocationException tex:
                        Exception innermostException = tex;
                        while (innermostException.InnerException != null) innermostException = innermostException.InnerException;

                        if (innermostException is System.Net.WebException wex)
                        {
                            using (var streamReader = new StreamReader(wex.Response.GetResponseStream()))
                            {
                                errorMessage = $"{wex.Status}: {wex.Message} Response received: {streamReader.ReadToEnd()}";
                            }
                        }
                        else
                        {
                            errorMessage = innermostException.Message;
                        }
                        break;
                    case Core.MicrosoftGraphServiceException pgex:
                        var pgexError = pgex.Error as Core.MicrosoftGraphError;
                        errorMessage = $"{pgex.Message} - {pgexError.Code} {pgexError.HttpResponseCode} {pgexError.Message}";
                        break;
                    default:
                        errorMessage = ex.Message;
                        break;
                }

                // If the ErrorAction is not set to Stop, Ignore or SilentlyContinue throw an exception, otherwise just continue
                if (!(new[] { "stop", "ignore", "silentlycontinue" }.Contains(ErrorActionSetting.ToLowerInvariant())))
                {
                    throw new PSInvalidOperationException(errorMessage);
                }

                if (Connection.Context.Url != Connection.Url)
                {
                    Connection.RestoreCachedContext(Connection.Url);
                }

                // With ErrorAction:Ignore, the $Error variable should not be populated with the error, otherwise it should
                if (!new[] { "ignore" }.Contains(ErrorActionSetting.ToLowerInvariant()))
                {
                    ex.Data["CorrelationId"] = Connection.Context.TraceCorrelationId;
                    ex.Data["TimeStampUtc"] = DateTime.UtcNow;

                    LogError(errorMessage);
                }

            }
        }
    }
}
