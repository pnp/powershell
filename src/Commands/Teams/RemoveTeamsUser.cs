using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsUser")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/TeamMember.ReadWrite.All")]
    public class RemoveTeamsUser : PnPGraphCmdlet
    {
        const string ParamSet_ByUser = "By User";
        const string ParamSet_ByMultipleUsers = "By Multiple Users";

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByUser)]
        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByMultipleUsers)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByUser)]
        public string User;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByMultipleUsers)]
        public string[] Users;

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_ByUser)]
        public string Role = "Member";

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_ByUser)]
        [Parameter(Mandatory = false, ParameterSetName = ParamSet_ByMultipleUsers)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(RequestHelper);
            if (groupId != null)
            {
                try
                {
                    if (ParameterSetName == ParamSet_ByUser)
                    {
                        if (Force || ShouldContinue($"Remove user with UPN {User}?", Properties.Resources.Confirm))
                        {
                            TeamsUtility.DeleteUser(RequestHelper, groupId, User, Role);
                        }
                    }
                    else
                    {
                        if (Force || ShouldContinue($"Remove specifed users from the team ?", Properties.Resources.Confirm))
                        {
                            TeamsUtility.DeleteUsers(RequestHelper, groupId, Users, Role);
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
                throw new PSArgumentException("Group not found");
            }

        }
    }
}