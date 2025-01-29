using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamsChannelUser")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ChannelMember.ReadWrite.All")]
    public class SetTeamsChannelUser : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = true)]
        public TeamsChannelMemberPipeBind Identity;

        [Parameter(Mandatory = true)]
        [ValidateSet("Owner", "Member")]
        public string Role;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(GraphRequestHelper);
            if (groupId == null)
            {
                throw new PSArgumentException("Group not found");
            }

            var channelId = Channel.GetId(GraphRequestHelper, groupId);
            if (channelId == null)
            {
                throw new PSArgumentException("Channel not found");
            }

            var membershipId = Identity.GetId(GraphRequestHelper, groupId, channelId);
            if (string.IsNullOrEmpty(membershipId))
            {
                throw new PSArgumentException("User was not found in the specified Teams channel");
            }

            try
            {
                var updatedMember = TeamsUtility.UpdateChannelMember(GraphRequestHelper, groupId, channelId, membershipId, Role);
                WriteObject(updatedMember);
            }
            catch (GraphException ex)
            {
                if (ex.Error != null)
                {
                    throw new PSInvalidOperationException(ex.Error.Message);
                }

                throw;
            }
        }
    }
}
