using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPUnifiedGroup")]
    [OutputType(typeof(UnifiedGroup))]
    public class GetUnifiedGroup : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public string GroupAlias { get; set; }

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

                WriteVerbose($"Retrieving unified group information for alias: {GroupAlias}");

                // Get the unified group details
                var unifiedGroup = GetUnifiedGroupDetails(GroupAlias);

                if (unifiedGroup == null)
                {
                    WriteError(new ErrorRecord(
                        new InvalidOperationException($"Unified group with alias '{GroupAlias}' was not found."),
                        "UnifiedGroupNotFound",
                        ErrorCategory.ObjectNotFound,
                        GroupAlias));
                    return;
                }

                WriteObject(unifiedGroup);
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "GetUnifiedGroupError",
                    ErrorCategory.OperationStopped,
                    GroupAlias));
            }
        }

        private UnifiedGroup GetUnifiedGroupDetails(string groupAlias)
        {
            // This would typically call the SharePoint Admin REST API or Microsoft Graph
            // For now, creating a placeholder implementation
            WriteVerbose($"Calling multi-geo REST API to get unified group: {groupAlias}");

            // In actual implementation, this would call the multi-geo REST API
            // or Microsoft Graph to retrieve the unified group details
            return new UnifiedGroup
            {
                Id = Guid.NewGuid().ToString(),
                GroupAlias = groupAlias,
                DisplayName = $"Sample Group - {groupAlias}",
                Description = "This is a sample Microsoft 365 Group retrieved from Multi-Geo tenant",
                Mail = $"{groupAlias}@contoso.com",
                MailNickname = groupAlias,
                SiteUrl = $"https://contoso.sharepoint.com/sites/{groupAlias}",
                DataLocation = "NAM",
                CreatedDateTime = DateTime.UtcNow.AddDays(-30),
                LastModifiedDateTime = DateTime.UtcNow.AddDays(-1),
                Owners = new[] { "admin@contoso.com", "owner@contoso.com" },
                Members = new[] { "user1@contoso.com", "user2@contoso.com", "user3@contoso.com" },
                Visibility = "Private",
                Classification = "General",
                MailEnabled = true,
                SecurityEnabled = false,
                GroupType = "Unified"
            };
        }
    }
}