using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPWebAlert")]
    [OutputType(typeof(void))]
    public class RemoveWebAlert : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public AlertPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var alertId = Identity.Id;

            if (alertId == Guid.Empty)
            {
                throw new PSArgumentException("A valid alert ID must be specified.", nameof(Identity));
            }

            var webUrl = CurrentWeb.EnsureProperty(w => w.Url);

            if (Force || ShouldContinue($"Remove alert with ID '{alertId}' from site '{webUrl}'?", Properties.Resources.Confirm))
            {
                try
                {
                    WriteVerbose($"Removing alert with ID '{alertId}' from site '{webUrl}'...");

                    // Use the REST API to delete the alert
                    var requestUrl = $"{webUrl}/_api/web/Alerts/DeleteAlert('{alertId}')";
                    
                    RestHelper.ExecuteDeleteRequest(ClientContext, requestUrl);

                    WriteVerbose($"Alert with ID '{alertId}' has been successfully removed.");
                }
                catch (Exception ex)
                {
                    throw new PSInvalidOperationException($"Failed to remove alert with ID '{alertId}': {ex.Message}", ex);
                }
            }
        }
    }
}
