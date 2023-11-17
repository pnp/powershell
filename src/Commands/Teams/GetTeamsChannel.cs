using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannel")]
    [RequiredMinimalApiPermissions("Group.Read.All")]

    public class GetTeamsChannel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = false)]
        public TeamsChannelPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter UseBeta;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(Connection, AccessToken) ?? throw new PSArgumentException("Team not found", nameof(Team));

            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Identity.GetChannel(Connection, AccessToken, groupId, useBeta: UseBeta.ToBool()));
            }
            else
            {
                WriteObject(TeamsUtility.GetChannelsAsync(AccessToken, Connection, groupId, useBeta: UseBeta.ToBool()).GetAwaiter().GetResult(), true);
            }
        }
    }
}