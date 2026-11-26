using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Add, "PnPTeamsChannelApp")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/TeamsAppInstallation.ManageSelectedForTeam")]
    [RequiredApiApplicationPermissions("graph/TeamsAppInstallation.ManageSelectedForTeam.All")]
    public class AddTeamsChannelApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true,ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true,ValueFromPipeline = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = true)]
        public string App;

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

            try
            {
                TeamsUtility.AddChannelApp(GraphRequestHelper, groupId, channelId, App);
            }
            catch (GraphException ex)
            {
                if (ex.Error != null)
                {
                    throw new PSInvalidOperationException(ex.Error.Message);
                }

                throw;
            }
        }
    }
}
