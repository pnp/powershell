using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPTeamsChannel")]
    [MicrosoftGraphApiPermissionCheckAttribute(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]

    public class AddTeamsChannel : PnPGraphCmdlet
    {
        private const string ParameterSET_PRIVATE = "Private channel";
        private const string ParameterSET_PUBLIC = "Public channel";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PUBLIC)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PRIVATE)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PUBLIC)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PRIVATE)]
        public string DisplayName;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSET_PUBLIC)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSET_PRIVATE)]
        public string Description;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PRIVATE)]
        public SwitchParameter Private;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PRIVATE)]
        public string OwnerUPN;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            {
                try
                {
                    var channel = TeamsUtility.AddChannelAsync(AccessToken, HttpClient, groupId, DisplayName, Description, Private, OwnerUPN).GetAwaiter().GetResult();
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
                        throw ex;
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