using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTeam", DefaultParameterSetName = ParameterSet_Identity)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class GetTeamsTeam : PnPGraphCmdlet
    {
        private const string ParameterSet_Identity = "Identity";
        private const string ParameterSet_Filter = "Filter";
        private const string ParameterSet_User = "User";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Identity)]
        public TeamsTeamPipeBind Identity;

        /// <summary>
        /// Filter supports whatever you can pass to $filter. 
        /// For details on which operators are supported for which properties, see this:
        /// https://learn.microsoft.com/en-us/graph/aad-advanced-queries?tabs=http#group-properties
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Filter)]
        public string Filter = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_User)]
        public AzureADUserPipeBind User;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var groupId = Identity.GetGroupId(GraphRequestHelper);
                if (groupId == null)
                {
                    throw new PSArgumentException("Team not found", nameof(Identity));
                }
                else
                {
                    WriteObject(TeamsUtility.GetTeam(GraphRequestHelper, groupId));
                }
            }
            else
            {
                if (ParameterSpecified(nameof(User)))
                {
                    var user = User.GetUser(AccessToken, Connection.AzureEnvironment);
                    if (user == null)
                    {
                        LogWarning("Provided user not found");
                        return;
                    }
                    WriteObject(TeamsUtility.GetJoinedTeams(GraphRequestHelper, user.Id.Value));
                }
                else
                {
                    WriteObject(TeamsUtility.GetTeamUsingFilter(GraphRequestHelper, Filter));
                }
            }
        }
    }
}
