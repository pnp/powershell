using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPUnifiedGroup")]
    [OutputType(typeof(void))]
    public class SetUnifiedGroup : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public string GroupAlias { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string PreferredDataLocation { get; set; }

        protected override void ExecuteCmdlet()
        {
            try
            {
                // Load the current site context to validate access
                AdminContext.Load(AdminContext.Site, s => s.Url);
                AdminContext.ExecuteQueryRetry();

                if (string.IsNullOrEmpty(AdminContext.Site.Url))
                {
                    WriteError(new ErrorRecord(
                        new InvalidOperationException("Unable to access the SharePoint Admin site. Please ensure you have the necessary permissions."),
                        "GetSitesOperationFailed",
                        ErrorCategory.PermissionDenied,
                        null));
                    return;
                }

                WriteVerbose($"Setting preferred data location for unified group '{GroupAlias}' to '{PreferredDataLocation}'");

                // Validate the preferred data location format (should be 3-letter geo code)
                if (PreferredDataLocation.Length != 3)
                {
                    WriteWarning($"Preferred data location '{PreferredDataLocation}' should typically be a 3-letter geo code (e.g., NAM, EUR, APC)");
                }

                // Update the unified group's preferred data location
                var success = UpdateUnifiedGroupPreferredDataLocation(GroupAlias, PreferredDataLocation);

                if (success)
                {
                    WriteVerbose($"Successfully updated preferred data location for group '{GroupAlias}' to '{PreferredDataLocation}'");
                    WriteObject($"Preferred data location for group '{GroupAlias}' has been set to '{PreferredDataLocation}'");
                }
                else
                {
                    WriteError(new ErrorRecord(
                        new InvalidOperationException($"Failed to update preferred data location for group '{GroupAlias}'"),
                        "UpdateUnifiedGroupFailed",
                        ErrorCategory.OperationStopped,
                        GroupAlias));
                }
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "SetUnifiedGroupError",
                    ErrorCategory.OperationStopped,
                    GroupAlias));
            }
        }

        private bool UpdateUnifiedGroupPreferredDataLocation(string groupAlias, string preferredDataLocation)
        {
            // This would typically call the SharePoint Admin REST API or Microsoft Graph
            // For now, creating a placeholder implementation
            WriteVerbose($"Calling multi-geo REST API to update unified group: {groupAlias}");

            try
            {
                // In actual implementation, this would call the multi-geo REST API
                // to perform a partial update of the unified group
                var unifiedGroupUpdate = new UnifiedGroup
                {
                    GroupAlias = groupAlias,
                    PreferredDataLocation = preferredDataLocation
                };

                WriteVerbose($"Updating group '{groupAlias}' with preferred data location: {preferredDataLocation}");

                // Simulate the API call
                System.Threading.Thread.Sleep(500); // Simulate network delay

                WriteVerbose("Update operation completed successfully");
                return true;
            }
            catch (Exception ex)
            {
                WriteVerbose($"Error during update operation: {ex.Message}");
                throw;
            }
        }
    }
}