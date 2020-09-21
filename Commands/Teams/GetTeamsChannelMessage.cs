using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannelMessage")]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    public class GetTeamsChannelMessage : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeDeleted;
        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            {
                var channel = Channel.GetChannel(HttpClient, AccessToken, groupId);
                if (channel != null)
                {
                    WriteObject(TeamsUtility.GetMessagesAsync(HttpClient, AccessToken, groupId, channel.Id, IncludeDeleted).GetAwaiter().GetResult(), true);
                } else
                {
                    throw new PSArgumentException("Channel not found");
                }
            } else
            {
                throw new PSArgumentException("Team not found");
            }

        }
    }
}