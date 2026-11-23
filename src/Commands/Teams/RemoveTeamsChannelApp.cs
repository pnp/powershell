using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsChannelApp")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/TeamsAppInstallation.ManageSelectedForTeam")]
    [RequiredApiApplicationPermissions("graph/TeamsAppInstallation.ManageSelectedForTeam.All")]
    public class RemoveTeamsChannelApp : PnPGraphCmdlet
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

            var groupId = Team.GetGroupId(GraphRequestHelper);
            if (groupId != null)
            {
                var channelId = Channel.GetId(GraphRequestHelper, groupId);
                if (channelId != null)
                {
                    var app = Identity.GetTab(GraphRequestHelper, groupId, channelId);
                    if (app != null)
                    {
                        if (Force || ShouldContinue("Removing the app will remove any corresponding app settings.", Properties.Resources.Confirm))
                        {
                            var response = TeamsUtility.DeleteTab(GraphRequestHelper, groupId, channelId, app.Id);
                            if (!response.IsSuccessStatusCode)
                            {
                                if (GraphRequestHelper.TryGetGraphException(response, out GraphException ex))
                                {
                                    if (ex.Error != null)
                                    {
                                        throw new PSInvalidOperationException(ex.Error.Message);
                                    }
                                }
                                else
                                {
                                    throw new PSInvalidOperationException("app remove failed");
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new PSArgumentException("App not found");
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