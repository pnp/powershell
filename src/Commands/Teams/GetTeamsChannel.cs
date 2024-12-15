using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannel")]
    [RequiredApiApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]

    public class GetTeamsChannel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = false)]
        public TeamsChannelPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeModerationSettings;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(RequestHelper) ?? throw new PSArgumentException("Team not found", nameof(Team));

            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Identity.GetChannel(RequestHelper, groupId, useBeta: IncludeModerationSettings.ToBool()));
            }
            else
            {
                WriteObject(TeamsUtility.GetChannels(RequestHelper, groupId, useBeta: IncludeModerationSettings.ToBool()), true);
            }
        }
    }
}