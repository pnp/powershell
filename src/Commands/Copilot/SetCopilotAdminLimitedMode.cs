using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System.Net.Http.Json;

namespace PnP.PowerShell.Commands.Copilot
{
    [Cmdlet(VerbsCommon.Set, "PnPCopilotAdminLimitedMode")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/CopilotSettings-LimitedMode.ReadWrite")]
    [OutputType(typeof(Model.Graph.Copilot.CopilotAdminLimitedMode))]
    public class SetCopilotAdminLimitedMode : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public bool IsEnabledForGroup;

        [Parameter(Mandatory = false)]
        public string GroupId;

        protected override void ExecuteCmdlet()
        {
            if(IsEnabledForGroup && string.IsNullOrEmpty(GroupId))
            {
                throw new PSArgumentException($"{nameof(GroupId)} is required when {nameof(IsEnabledForGroup)} is set to true", nameof(GroupId));
            }

            var bodyContent = new Model.Graph.Copilot.CopilotAdminLimitedMode
            {
                GroupId = GroupId,
                IsEnabledForGroup = IsEnabledForGroup
            };
            var jsonContent = JsonContent.Create(bodyContent);
            WriteVerbose($"Payload: {jsonContent.ReadAsStringAsync().GetAwaiter().GetResult()}");

            var result = GraphRequestHelper.Patch<Model.Graph.Copilot.CopilotAdminLimitedMode>("beta/copilot/admin/settings/limitedMode", jsonContent);
            WriteObject(result, false);
        }
    }
}
