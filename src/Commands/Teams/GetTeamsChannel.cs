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

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(Connection, AccessToken);
            if (groupId != null)
            {
                if (ParameterSpecified(nameof(Identity)))
                {
                    WriteObject(Identity.GetChannel(Connection, AccessToken, groupId));
                }
                else
                {
                    WriteObject(TeamsUtility.GetChannelsAsync(AccessToken, Connection, groupId).GetAwaiter().GetResult(), true);
                }
            } else
            {
                throw new PSArgumentException("Team not found", nameof(Team));
            }
        }
    }
}