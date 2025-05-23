﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannelMessageReply")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ChannelMessage.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ChannelMessage.ReadWrite.All")]
    public class GetTeamsChannelMessageReply : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = true)]
        public TeamsChannelMessagePipeBind Message;

        [Parameter(Mandatory = false)]
        public TeamsChannelMessageReplyPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeDeleted;

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

            var messageId = Message.GetId();
            if (messageId == null)
            {
                throw new PSArgumentException("Message not found");
            }

            try
            {
                if (ParameterSpecified(nameof(Identity)))
                {
                    if (ParameterSpecified(nameof(IncludeDeleted)))
                    {
                        throw new PSArgumentException($"Don't specify {nameof(IncludeDeleted)} when using the {nameof(Identity)} parameter.");
                    }

                    var reply = TeamsUtility.GetMessageReply(GraphRequestHelper, groupId, channelId, messageId, Identity.GetId());
                    WriteObject(reply);
                }
                else
                {
                    var replies = TeamsUtility.GetMessageReplies(GraphRequestHelper, groupId, channelId, messageId, IncludeDeleted);
                    WriteObject(replies, true);
                }
            }
            catch
            {
                // Exception thrown by Graph is quite unclear.
                var message = ParameterSpecified(nameof(Identity)) ? "Failed to retrieve reply." : "Failed to retrieve replies.";
                throw new PSArgumentException(message);
            }
        }
    }
}
