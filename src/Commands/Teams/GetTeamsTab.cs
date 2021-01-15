
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTab")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetTeamsTab : PnPGraphCmdlet
    {

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = false)]
        public TeamsTabPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            {
                var channelId = Channel.GetId(HttpClient, AccessToken, groupId);
                if (!string.IsNullOrEmpty(channelId))
                {
                    if (ParameterSpecified(nameof(Identity)))
                    {
                        WriteObject(Identity.GetTab(HttpClient, AccessToken, groupId, channelId));
                    }
                    else
                    {
                        WriteObject(TeamsUtility.GetTabsAsync(AccessToken, HttpClient, groupId, channelId).GetAwaiter().GetResult(), true);
                    }
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