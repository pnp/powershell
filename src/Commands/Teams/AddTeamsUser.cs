using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPTeamsUser")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class AddTeamsUser : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public string User;

        [Parameter(Mandatory = true)]
        [ValidateSet(new[] { "Owner", "Member" })]
        public string Role;
        protected override void ExecuteCmdlet()
        {
            Model.Teams.TeamChannel channel = null;

            var groupId = Team.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            {
                try
                {
                    TeamsUtility.AddUserAsync(HttpClient, AccessToken, groupId, User, Role).GetAwaiter().GetResult();
                    WriteObject(channel);
                }
                catch (GraphException ex)
                {
                    if (ex.Error != null)
                    {
                        throw new PSInvalidOperationException(ex.Error.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                throw new PSArgumentException("Group not found");
            }

        }
    }
}