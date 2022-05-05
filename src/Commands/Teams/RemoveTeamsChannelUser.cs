﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Properties;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsChannelUser")]
    [RequiredMinimalApiPermissions("ChannelMember.ReadWrite.All")]
    public class RemoveTeamsChannelUser : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = true)]
        public TeamsChannelMemberPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(HttpClient, AccessToken);
            if (string.IsNullOrEmpty(groupId))
            {
                throw new PSArgumentException("Group not found");
            }

            var channelId = Channel.GetId(HttpClient, AccessToken, groupId);
            if (string.IsNullOrEmpty(channelId))
            {
                throw new PSArgumentException("Channel not found in the specified team");
            }

            var memberId = Identity.GetIdAsync(HttpClient, AccessToken, groupId, channelId).GetAwaiter().GetResult();
            if (string.IsNullOrEmpty(memberId))
            {
                throw new PSArgumentException("User was not found in the specified Teams channel");
            }

            if (Force || ShouldContinue("Remove specified member from the Microsoft Teams channel?", Resources.Confirm))
            {
                var response = TeamsUtility.DeleteChannelMemberAsync(HttpClient, AccessToken, groupId, channelId, memberId).GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    if (GraphHelper.TryGetGraphException(response, out var ex) && !string.IsNullOrEmpty(ex.Error.Message))
                    {
                        throw new PSInvalidOperationException(ex.Error.Message);
                    }

                    throw new PSInvalidOperationException("Failed to remove user from channel.");
                }
            }
        }
    }
}
