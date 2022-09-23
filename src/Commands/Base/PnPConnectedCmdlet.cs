using System;
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
            if(Connection == null)
            {
                Connection = PnPConnection.Current;
            }

            // Track the execution of the cmdlet in Azure Application Insights
            if (Connection != null && Connection.ApplicationInsights != null)
            {
                Connection.ApplicationInsights.TrackEvent(MyInvocation.MyCommand.Name);
            }

            // Check if we should ensure that we are connected
            if(skipConnectedValidation) return;

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
                    case PnP.PowerShell.Commands.Model.Graph.GraphException gex:
                        errorMessage = gex.Message;
                        break;

                    case PnP.Core.SharePointRestServiceException rex:
                        errorMessage = (rex.Error as PnP.Core.SharePointRestError).Message;
                        break;

                    default:
                        errorMessage = ex.Message;
                        break;
                }

                // For backwards compatibility we will throw the exception as a PSInvalidOperationException if -ErrorAction:Stop has NOT been specified
                if (!ParameterSpecified("ErrorAction") || MyInvocation.BoundParameters["ErrorAction"].ToString().ToLowerInvariant() != "stop")
                {
                    throw new PSInvalidOperationException(errorMessage);
                }

                Connection.RestoreCachedContext(Connection.Url);
                ex.Data["CorrelationId"] = Connection.Context.TraceCorrelationId;
                ex.Data["TimeStampUtc"] = DateTime.UtcNow;
                var errorDetails = new ErrorDetails(errorMessage);

                errorDetails.RecommendedAction = "Use Get-PnPException for more details.";
                var errorRecord = new ErrorRecord(ex, "EXCEPTION", ErrorCategory.WriteError, null);
                errorRecord.ErrorDetails = errorDetails;

                WriteError(errorRecord);
            }
        }
    }
}
