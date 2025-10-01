using PnP.Core.Model;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPFileSensitivityLabel", DefaultParameterSetName = ParameterSet_SINGLE)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.ReadWrite.All")]

    public class AddFileSensitivityLabel : PnPGraphCmdlet
    {
        private const string ParameterSet_SINGLE = "Single";
        private const string ParameterSet_BATCH = "Batch";

        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BATCH)]
        public FilePipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BATCH)]
        [AllowNull]
        [AllowEmptyString]
        public string SensitivityLabelId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCH)]
        public Enums.SensitivityLabelAssignmentMethod AssignmentMethod = Enums.SensitivityLabelAssignmentMethod.Privileged;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCH)]
        public string JustificationText = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BATCH)]
        public PnPBatch Batch;

        protected override void ExecuteCmdlet()
        {
            var context = ParameterSpecified(nameof(Batch)) ? Batch.Context : Connection.PnPContext;

            IFile file = Identity.GetCoreFile(context, this);
            file.EnsureProperties(f => f.VroomDriveID, f => f.VroomItemID, f => f.Name);

            var requestUrl = GetRequestUrl(file);
            var payloadJson = SerializePayload();

            if (ParameterSpecified(nameof(Batch)))
            {
                QueueBatchRequest(requestUrl, payloadJson, file);
            }
            else
            {
                AssignLabelImmediately(requestUrl, payloadJson, file);
            }
        }

        private void AssignLabelImmediately(string requestUrl, string payloadJson, IFile file)
        {
            using var content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
            using var response = GraphRequestHelper.PostHttpContent(requestUrl, content);

            LogDebug($"File sensitivity label assigned to {file.Name}");
            WriteObject(response?.Headers?.Location);
        }

        private void QueueBatchRequest(string requestUrl, string payloadJson, IFile file)
        {
            Batch.Context.Web.ExecuteRequestBatch(
                Batch.Batch,
                new ApiRequest(HttpMethod.Post, ApiRequestType.Graph, requestUrl, payloadJson));

            LogDebug($"Queued file sensitivity label assignment for {file.Name}");
        }

        private static string GetRequestUrl(IFile file)
        {
            return $"v1.0/drives/{file.VroomDriveID}/items/{file.VroomItemID}/assignSensitivityLabel";
        }

        private string SerializePayload()
        {
            var payload = new
            {
                sensitivityLabelId = SensitivityLabelId,
                assignmentMethod = AssignmentMethod.ToString(),
                justificationText = JustificationText
            };

            var serializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(payload, serializerOptions);
        }
    }
}
