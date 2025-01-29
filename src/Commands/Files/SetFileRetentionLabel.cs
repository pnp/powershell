using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph.Purview;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Set, "PnPFileRetentionLabel", DefaultParameterSetName = ParameterSet_LOCKUNLOCK)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.ReadWrite.All")]
    [OutputType(typeof(FileRetentionLabel))]
    public class SetFileRetentionLabel : PnPGraphCmdlet
    {
        private const string ParameterSet_LOCKUNLOCK = "Lock or unlock a file";
        private const string ParameterSet_SETLABEL = "Set a retention label on a file";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public FilePipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SETLABEL)]
        public string RetentionLabel = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_LOCKUNLOCK)]
        public bool? RecordLocked;

        protected override void ExecuteCmdlet()
        {
            var file = Identity.GetFile(ClientContext);
            file.EnsureProperties(f => f.VroomDriveID, f => f.VroomItemID);

            var requestUrl = $"v1.0/drives/{file.VroomDriveID}/items/{file.VroomItemID}/retentionLabel";

            object payload = null;

            switch(ParameterSetName)
            {
                case ParameterSet_LOCKUNLOCK:
                    payload = new
                    {
                        retentionSettings = new
                        {
                            isRecordLocked = RecordLocked
                        }
                    };
                    break;
                case ParameterSet_SETLABEL:
                    if (string.IsNullOrEmpty(RetentionLabel))
                    {
                        WriteVerbose("Removing retention label");
                        GraphRequestHelper.Delete(requestUrl);
                    }
                    else
                    {
                        WriteVerbose($"Setting retention label to '{RetentionLabel}'");
                        payload = new
                        {
                            name = RetentionLabel
                        };
                    }
                    break;
            }

            if (payload != null)
            {
                var jsonPayload = JsonSerializer.Serialize(payload);
                var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var results = GraphRequestHelper.Patch<FileRetentionLabel>(requestUrl, httpContent);
                WriteObject(results, true);
            }
        }
    }
}
