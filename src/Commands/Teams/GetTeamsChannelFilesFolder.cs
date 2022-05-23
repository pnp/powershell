using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannelFilesFolder")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetTeamsChannelFilesFolder : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Channel;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            {

                var channelId = Channel.GetId(HttpClient, AccessToken, groupId);
                if (channelId == null)
                {
                    throw new PSArgumentException("Channel not found");
                }
                              
                WriteObject(Utilities.TeamsUtility.GetChannelsFilesFolderAsync(HttpClient, AccessToken, groupId, channelId).GetAwaiter().GetResult());
                                
            }
            else
            {
                throw new PSArgumentException("Team not found", nameof(Team));
            }
        }
    }
}
