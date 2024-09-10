using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsLifecycle.Submit, "PnPTeamsChannelMessage")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Group.ReadWrite.All")]
    public class SubmitTeamsChannelMessage : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = true)]
        public string Message;

        [Parameter(Mandatory = false)]
        public TeamChannelMessageContentType ContentType = TeamChannelMessageContentType.Html;

        [Parameter(Mandatory = false)]
        public SwitchParameter Important;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(this, Connection, AccessToken);
            if (groupId != null)
            {
                var channel = Channel.GetChannel(this, Connection, AccessToken, groupId);
                if (channel != null)
                {
                    var channelMessage = new TeamChannelMessage();
                    channelMessage.Importance = Important ? "high" : "normal";
                    channelMessage.Body.Content = Message;
                    channelMessage.Body.ContentType = ContentType == TeamChannelMessageContentType.Html ? "html" : "text";

                    TeamsUtility.PostMessage(this, Connection, AccessToken, groupId, channel.Id, channelMessage);
                }
                else
                {
                    throw new PSArgumentException("Channel not found");
                }
            }
            else
            {
                throw new PSArgumentException("Team not found");
            }

        }
    }
}