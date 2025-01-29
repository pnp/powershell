using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsData.Update, "PnPTeamsUser")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class UpdateTeamsUser : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public string User;

        [Parameter(Mandatory = true)]
        [ValidateSet(new[] { "Owner", "Member" })]
        public string Role;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(GraphRequestHelper);
            if (groupId != null)
            {
                try
                {
                    if (Force || ShouldContinue($"Update role for user with UPN {User} ?", Properties.Resources.Confirm))
                    {
                        var teamsUser = TeamsUtility.GetUsers(GraphRequestHelper, groupId, string.Empty);

                        var specifiedUser = teamsUser.Find(u => u.UserPrincipalName.ToLower() == User.ToLower());
                        if (specifiedUser != null)
                        {
                            // No easy way to get member Id for teams endpoint, need to rely on display name filter to fetch memberId of the specified user and then update
                            var teamUserWithDisplayName = TeamsUtility.GetTeamUsersWithDisplayName(GraphRequestHelper, groupId, specifiedUser.DisplayName);
                            var userToUpdate = teamUserWithDisplayName.Find(u => u.UserId == specifiedUser.Id) ?? throw new PSArgumentException($"User found in the M365 group but not in the team ");

                            // Pass the member id of the user whose role we are changing
                            WriteObject(TeamsUtility.UpdateTeamUserRole(GraphRequestHelper, groupId, userToUpdate.Id, Role));
                        }
                        else
                        {
                            throw new PSArgumentException("User not found in the team");
                        }
                    }
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
                throw new PSArgumentException("Team not found");
            }
        }
    }
}