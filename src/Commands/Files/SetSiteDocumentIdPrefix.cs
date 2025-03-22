using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteDocumentIdPrefix")]
    public class SetSiteDocumentIdPrefix : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public string DocumentIdPrefix { get; set; }

        [Parameter(Mandatory = false)]
        public bool ScheduleAssignment = false;

        [Parameter(Mandatory = false)]
        public bool OverwriteExistingIds = false;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.Url);

            if(!System.Text.RegularExpressions.Regex.IsMatch(DocumentIdPrefix, @"^[a-zA-Z0-9]{4,12}$"))
            {
                LogWarning($"{nameof(DocumentIdPrefix)} can only contain digits (0-9) and letters and must be between 4 and 12 characters in length.");
            }

            var docIdPrefixUrl = $"{CurrentWeb.Url}/_api/SP.DocumentManagement.DocumentId/SetDocIdSitePrefix(prefix='{DocumentIdPrefix}',scheduleAssignment={(ScheduleAssignment ? "true" : "false")},overwriteExistingIds={(OverwriteExistingIds ? "true" : "false")})";
            LogDebug($"Making a POST request to {docIdPrefixUrl} to set the document ID prefix to {DocumentIdPrefix}");

            RestHelper.Post(Connection.HttpClient, docIdPrefixUrl, ClientContext);
        }
    }
}