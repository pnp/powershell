using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsTeam")]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_Read_All)]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    public class GetTeamsTeam : PnPGraphCmdlet
    {
        private const string ParameterSet_GroupId = "Retrieve a specific Team";

        [Parameter(Mandatory = false)]
        public TeamsTeamPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var groupId = Identity.GetGroupId(HttpClient, AccessToken);
                if (groupId != null)
                {
                    WriteObject(TeamsUtility.GetTeamAsync(AccessToken, HttpClient, groupId).GetAwaiter().GetResult());
                }
                else
                {
                    throw new PSArgumentException("Team not found");
                }
            }
            else
            {
                WriteObject(TeamsUtility.GetTeamsAsync(AccessToken, HttpClient).GetAwaiter().GetResult(), true);
            }
        }
    }
}