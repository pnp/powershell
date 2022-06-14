using System;
using System.Management.Automation;
using System.Net.Http;

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

        /// <summary>
        /// Returns a reusable HTTPClient that can be used to make HTTP calls
        /// </summary>
        public HttpClient HttpClient => PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();

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

            // Check if we should ensure that we are connected
            if(skipConnectedValidation) return;

            // Ensure there is an active connection
            if (Connection == null)
            {
                throw new InvalidOperationException(Properties.Resources.NoConnection);
            }
        }
    }
}
