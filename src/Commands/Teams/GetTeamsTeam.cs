using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTeam")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetTeamsTeam : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public TeamsTeamPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var groupId = Identity.GetGroupId(Connection, AccessToken);
                if (groupId != null)
                {
                    WriteObject(TeamsUtility.GetTeamAsync(AccessToken, Connection, groupId).GetAwaiter().GetResult());
                }
            }
            else
            {
                WriteObject(TeamsUtility.GetTeamsAsync(AccessToken, Connection).GetAwaiter().GetResult(), true);
            }
        }
    }
}