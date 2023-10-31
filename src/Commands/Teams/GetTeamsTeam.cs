using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTeam", DefaultParameterSetName = ParameterSet_Identity)]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetTeamsTeam : PnPGraphCmdlet
    {
        private const string ParameterSet_Identity = "Identity";
        private const string ParameterSet_Filter = "Filter";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Identity)]
        public TeamsTeamPipeBind Identity;

        /// <summary>
        /// Filter supports whatever you can pass to $filter. 
        /// For details on which operators are supported for which properties, see this:
        /// https://learn.microsoft.com/en-us/graph/aad-advanced-queries?tabs=http#group-properties
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Filter)]
        public string Filter = null;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var groupId = Identity.GetGroupId(Connection, AccessToken);
                if(groupId == null)
                {
                    throw new PSArgumentException("Team not found", nameof(Identity));
                }
                else
                {                
                    WriteObject(TeamsUtility.GetTeamAsync(AccessToken, Connection, groupId).GetAwaiter().GetResult());
                }                
            }
            else
            {
                WriteObject(TeamsUtility.GetTeamsAsync(AccessToken, Connection, Filter).GetAwaiter().GetResult(), true);
            }
        }
    }
}
