using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamsTab")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    public class SetTeamsTab : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = false)]
        public TeamsTabPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string DisplayName;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(this, Connection, AccessToken);
            if (groupId != null)
            {
                var channelId = Channel.GetId(this, Connection, AccessToken, groupId);
                if (channelId != null)
                {
                    var tab = Identity.GetTab(this,Connection, AccessToken, groupId, channelId);
                    if (tab != null)
                    {
                        if (ParameterSpecified(nameof(DisplayName)) && tab.DisplayName != DisplayName)
                        {
                            tab.DisplayName = DisplayName;
                        }
                        TeamsUtility.UpdateTab(this, Connection, AccessToken, groupId, channelId, tab);
                    }
                    else
                    {
                        throw new PSArgumentException("Tab not found");
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