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

            var docIdPrefixUrl = $"{CurrentWeb.Url}/_api/SP.DocumentManagement.DocumentId/SetDocIdSitePrefix(prefix='{DocumentIdPrefix}',scheduleAssignment={(ScheduleAssignment ? "true" : "false")},overwriteExistingIds={(OverwriteExistingIds ? "true" : "false")})";
            WriteVerbose($"Making a POST request to {docIdPrefixUrl} to set the document ID prefix to {DocumentIdPrefix}");

            RestHelper.Post(Connection.HttpClient, docIdPrefixUrl, ClientContext);
        }
    }
}