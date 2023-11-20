using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsUser")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class RemoveTeamsUser : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public string User;

        [Parameter(Mandatory = false)]
        public string Role = "Member";

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(Connection, AccessToken);
            if (groupId != null)
            {
                try
                {
                    if (Force || ShouldContinue($"Remove user with UPN {User}?", Properties.Resources.Confirm))
                    {
                        TeamsUtility.DeleteUserAsync(Connection, AccessToken, groupId, User, Role).GetAwaiter().GetResult();
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
                throw new PSArgumentException("Group not found");
            }

        }
    }
}