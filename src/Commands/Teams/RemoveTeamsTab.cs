using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsTab")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    public class RemoveTeamsTab : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = true)]
        public TeamsTabPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {

            var groupId = Team.GetGroupId(RequestHelper);
            if (groupId != null)
            {
                var channelId = Channel.GetId(RequestHelper, groupId);
                if (channelId != null)
                {
                    var tab = Identity.GetTab(RequestHelper, groupId, channelId);
                    if (tab != null)
                    {
                        if (Force || ShouldContinue("Removing the tab will remove the settings of this tab too.", Properties.Resources.Confirm))
                        {
                            var response = TeamsUtility.DeleteTab(RequestHelper, groupId, channelId, tab.Id);
                            if (!response.IsSuccessStatusCode)
                            {
                                if (RequestHelper.TryGetGraphException(response, out GraphException ex))
                                {
                                    if (ex.Error != null)
                                    {
                                        throw new PSInvalidOperationException(ex.Error.Message);
                                    }
                                }
                                else
                                {
                                    throw new PSInvalidOperationException("Tab remove failed");
                                }
                            }
                        }
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
                throw new PSArgumentException("Team not found", nameof(Team));
            }
        }
    }
}