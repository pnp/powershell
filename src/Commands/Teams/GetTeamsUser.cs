using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsUser")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class GetTeamsUser : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = false)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = false)]
        [ValidateSet(new[] { "Owner", "Member", "Guest" })]
        public string Role;
        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(RequestHelper);
            if (groupId != null)
            {
                try
                {
                    if (ParameterSpecified(nameof(Channel)))
                    {
                        var teamChannels = TeamsUtility.GetChannels(RequestHelper, groupId);

                        var channelId = Channel.GetId(RequestHelper, groupId);

                        var requestedChannel = teamChannels.FirstOrDefault(c => c.Id == channelId);

                        if (!string.IsNullOrEmpty(channelId) && requestedChannel != null && requestedChannel.MembershipType.ToLower() == TeamChannelType.Private.ToString().ToLower())
                        {
                            WriteObject(TeamsUtility.GetUsers(RequestHelper, groupId, channelId, Role), true);
                        }
                        else
                        {
                            WriteWarning("Specified channel is not a private channel. Please specify a private channel name to fetch its users.");
                        }
                    }
                    else
                    {
                        WriteObject(TeamsUtility.GetUsers(RequestHelper, groupId, Role), true);
                    }
                }
                catch (GraphException ex)
                {
                    if (ex.Error != null)
                    {
                        throw new PSInvalidOperationException(ex.Error.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                throw new PSArgumentException("Group not found");
            }

        }
    }
}