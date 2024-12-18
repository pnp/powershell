
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTab")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
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
            var groupId = Team.GetGroupId(RequestHelper);
            if (groupId != null)
            {
                var channelId = Channel.GetId(RequestHelper, groupId);
                if (!string.IsNullOrEmpty(channelId))
                {
                    if (ParameterSpecified(nameof(Identity)))
                    {
                        var tab = Identity.GetTab(RequestHelper, groupId, channelId);
                        if (tab != null)
                        {
                            WriteObject(tab);
                        }
                        else
                        {
                            WriteError(new PSArgumentException("Cannot find tab"), ErrorCategory.ObjectNotFound);
                        }
                    }
                    else
                    {
                        WriteObject(TeamsUtility.GetTabs(RequestHelper, groupId, channelId), true);
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