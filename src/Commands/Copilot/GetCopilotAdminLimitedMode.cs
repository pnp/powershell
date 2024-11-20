using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Copilot
{
    [Cmdlet(VerbsCommon.Get, "PnPCopilotAdminLimitedMode")]
    [RequiredApiDelegatedPermissions("graph/CopilotSettings-LimitedMode.Read")]
    [RequiredApiDelegatedPermissions("graph/CopilotSettings-LimitedMode.ReadWrite")]
    [ApiNotAvailableUnderApplicationPermissions]
    [OutputType(typeof(Model.Graph.Copilot.CopilotAdminLimitedMode))]
    public class GetCopilotAdminLimitedMode : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var result = Utilities.REST.GraphHelper.Get<Model.Graph.Copilot.CopilotAdminLimitedMode>(this, Connection, "beta/copilot/admin/settings/limitedMode", AccessToken);
            WriteObject(result, false);
        }
    }
}