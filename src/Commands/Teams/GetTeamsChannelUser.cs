using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannelUser")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ChannelMember.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ChannelMember.ReadWrite.All")]
    public class GetTeamsChannelUser : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = false)]
        public TeamsChannelMemberPipeBind Identity;

        [Parameter(Mandatory = false)]
        [ValidateSet("Owner", "Member", "Guest")]
        public string Role;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(RequestHelper);
            if (groupId == null)
            {
                throw new PSArgumentException("Group not found");
            }

            var channelId = Channel.GetId(RequestHelper, groupId);
            if (channelId == null)
            {
                throw new PSArgumentException("Channel not found");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Identity.GetMembership(RequestHelper, groupId, channelId));
            }
            else
            {
                WriteObject(TeamsUtility.GetChannelMembers(RequestHelper, groupId, channelId, Role), true);
            }
        }
    }
}
