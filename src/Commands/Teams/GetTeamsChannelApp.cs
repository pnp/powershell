
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannelApp")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/TeamsAppInstallation.ReadForTeam")]
    [RequiredApiApplicationPermissions("graph/TeamsAppInstallation.Read.Group")]
    public class GetTeamsChannelApp : PnPGraphCmdlet
    {

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        public TeamsChannelAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(GraphRequestHelper);
            if (groupId != null)
            {
                var channelId = Channel.GetId(GraphRequestHelper, groupId);
                if (!string.IsNullOrEmpty(channelId))
                {
                    if (ParameterSpecified(nameof(Identity)))
                    {
                        var ChannelApp = Identity.Get(GraphRequestHelper, groupId, channelId);
                        if (ChannelApp != null)
                        {
                            WriteObject(ChannelApp);
                        }
                        else
                        {
                            LogError(new PSArgumentException("Cannot find ChannelApp"));
                        }
                    }
                    else
                    {
                        WriteObject(TeamsUtility.GetChannelApps(GraphRequestHelper, groupId, channelId), true);
                    }
                }
                else
                {
                    this.LogError(new PSArgumentException("Channel not found"));
                }
            }
            else
            {
                this.LogError(new PSArgumentException("Team not found"));
            }
        }
    }
}