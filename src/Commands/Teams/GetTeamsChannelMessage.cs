using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannelMessage")]
    [RequiredApiApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]

    public class GetTeamsChannelMessage : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = false)]
        public TeamsChannelMessagePipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeDeleted;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(RequestHelper);
            if (groupId == null)
            {
                throw new PSArgumentException("Team not found");
            }

            var channelId = Channel.GetId(RequestHelper, groupId);
            if (channelId == null)
            {
                throw new PSArgumentException("Channel not found");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                if (ParameterSpecified(nameof(IncludeDeleted)))
                {
                    throw new PSArgumentException($"Don't specify {nameof(IncludeDeleted)} when using the {nameof(Identity)} parameter.");
                }

                var message = TeamsUtility.GetMessage(RequestHelper, groupId, channelId, Identity.GetId());
                WriteObject(message);
            }
            else
            {
                var messages = TeamsUtility.GetMessages(RequestHelper, groupId, channelId, IncludeDeleted);
                WriteObject(messages, true);
            }
        }
    }
}