
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamsTeamArchivedState")]
    [TokenType(TokenType = TokenType.Delegate)]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class SetTeamsTeamArchivedState : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Identity;

        [Parameter(Mandatory = true)]
        public bool Archived;

        [Parameter(Mandatory = false)]
        public bool? SetSiteReadOnlyForMembers;

        protected override void ExecuteCmdlet()
        {
            if (!Archived && SetSiteReadOnlyForMembers.HasValue)
            {
                throw new PSArgumentException("You can only modify the read only state of a site when archiving a team");
            }
            var groupId = Identity.GetGroupId(this, Connection, AccessToken);
            if (groupId != null)
            {
                var team = Identity.GetTeam(this, Connection, AccessToken);
                if (Archived == team.IsArchived)
                {
                    throw new PSInvalidOperationException($"Team {team.DisplayName} {(Archived ? "has already been" : "is not")} archived");
                }
                var response = TeamsUtility.SetTeamArchivedState(this, Connection, AccessToken, groupId, Archived, SetSiteReadOnlyForMembers);
                if (!response.IsSuccessStatusCode)
                {
                    if (GraphHelper.TryGetGraphException(response, out GraphException ex))
                    {
                        if (ex.Error != null)
                        {
                            throw new PSInvalidOperationException(ex.Error.Message);
                        }
                    }
                    else
                    {
                        throw new PSInvalidOperationException("Setting archived state failed.");
                    }
                }
                else
                {
                    WriteObject($"Team {(Archived ? "archived" : "unarchived")}.");

                }
            }
            else
            {
                throw new PSArgumentException("Team not found");
            }
        }
    }
}