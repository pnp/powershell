using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;
using System.Net.Http.Headers;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPFileSensitivityLabel")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Files.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Sites.ReadWrite.All")]

    public class AddFileSensitivityLabel : PnPGraphCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public FilePipeBind Identity;

        [Parameter(Mandatory = true)]
        public string SensitivityLabelId;

        [Parameter(Mandatory = false)]
        public Enums.SensitivityLabelAssignmentMethod AssignmentMethod = Enums.SensitivityLabelAssignmentMethod.Privileged;

        [Parameter(Mandatory = false)]
        public string JustificationText = string.Empty;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;

            IFile file = Identity.GetCoreFile(Connection.PnPContext, this);
            file.EnsureProperties(f => f.VroomDriveID, f => f.VroomItemID);

            var requestUrl = $"https://{Connection.GraphEndPoint}/v1.0/drives/{file.VroomDriveID}/items/{file.VroomItemID}/assignSensitivityLabel";

            var payload = new
            {
                sensitivityLabelId = SensitivityLabelId,
                assignmentMethod = AssignmentMethod.ToString(),
                justificationText = JustificationText
            };

            HttpResponseHeaders responseHeader = RestHelper.PostGetResponseHeader<string>(Connection.HttpClient, requestUrl, AccessToken, payload: payload);

            WriteVerbose($"File sensitivity label assigned to {file.Name}");
            WriteObject(responseHeader.Location);
        }
    }
}
