using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph.Purview;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Set, "PnPFileRetentionLabel")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.ReadWrite.All")]
    [OutputType(typeof(FileRetentionLabel))]
    public class SetFileRetentionLabel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url = string.Empty;

        [Parameter(Mandatory = false)]
        public string retentionLabel = string.Empty;

        [Parameter(Mandatory = false)]
        public bool? RecordLocked;

        protected override void ExecuteCmdlet()
        {
            // Check if both parameters are provided
            if (!string.IsNullOrEmpty(retentionLabel) && RecordLocked.HasValue)
            {
                throw new PSArgumentException("Only one of the parameters 'RecordLocked' or 'retentionLabel' can be provided.");
            }

            var serverRelativeUrl = string.Empty;

            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
            {
                // We can't deal with absolute URLs
                Url = UrlUtility.MakeRelativeUrl(Url);
            }

            // Remove URL decoding from the Url as that will not work. We will encode the + character specifically, because if that is part of the filename, it needs to stay and not be decoded.
            Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));

            Connection.PnPContext.Web.EnsureProperties(w => w.ServerRelativeUrl);

            var webUrl = Connection.PnPContext.Web.ServerRelativeUrl;

            if (!Url.ToLower().StartsWith(webUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(webUrl, Url);
            }
            else
            {
                serverRelativeUrl = Url;
            }

            var file = Connection.PnPContext.Web.GetFileByServerRelativeUrl(Url);
            file.EnsureProperties(f => f.VroomDriveID, f => f.VroomItemID);

            var requestUrl = $"v1.0/drives/{file.VroomDriveID}/items/{file.VroomItemID}/retentionLabel";

            object payload;

            if (!string.IsNullOrEmpty(retentionLabel))
            {
                payload = new
                {
                    name = retentionLabel
                };
            }
            else if (RecordLocked.HasValue)
            {
                payload = new
                {
                    retentionSettings = new
                    {
                        isRecordLocked = RecordLocked
                    }
                };
            }
            else
            {
                throw new PSArgumentException("Either retentionLabel or isRecordLocked must be provided");
            }
            var jsonPayload = JsonSerializer.Serialize(payload);
            var httpContent = new StringContent(jsonPayload,Encoding.UTF8, "application/json");
            var results = RequestHelper.Patch<FileRetentionLabel>(requestUrl, httpContent);
            WriteObject(results, true);
        }
    }
}
