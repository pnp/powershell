
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTab")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
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
            var groupId = Team.GetGroupId(this, Connection, AccessToken);
            if (groupId != null)
            {
                var channelId = Channel.GetId(this, Connection, AccessToken, groupId);
                if (!string.IsNullOrEmpty(channelId))
                {
                    if (ParameterSpecified(nameof(Identity)))
                    {
                        WriteObject(Identity.GetTab(this,Connection, AccessToken, groupId, channelId));
                    }
                    else
                    {
                        WriteObject(TeamsUtility.GetTabs(this, AccessToken, Connection, groupId, channelId), true);
                    }
                }
                else
                {
                    this.WriteError(new PSArgumentException("Channel not found"), ErrorCategory.ObjectNotFound);
                }
            }
            else
            {
                this.WriteError(new PSArgumentException("Team not found"), ErrorCategory.ObjectNotFound);
            }
        }
    }
}