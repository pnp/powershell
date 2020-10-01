
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsUser")]
    [MicrosoftGraphApiPermissionCheckAttribute(MicrosoftGraphApiPermission.Group_Read_All)]
    [MicrosoftGraphApiPermissionCheckAttribute(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]
    public class GetTeamsUser : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = false)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = false)]
        [ValidateSet(new[] { "Owner", "Member", "Guest" })]
        public string Role;
        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            {
                try
                {
                    if (ParameterSpecified(nameof(Channel)))
                    {
                        var channelId = Channel.GetId(HttpClient, AccessToken, groupId);
                        if (!string.IsNullOrEmpty(channelId))
                        {
                            WriteObject(TeamsUtility.GetUsersAsync(HttpClient, AccessToken, groupId, channelId, Role).GetAwaiter().GetResult(), true);
                        }
                    }
                    else
                    {
                        WriteObject(TeamsUtility.GetUsersAsync(HttpClient, AccessToken, groupId, Role).GetAwaiter().GetResult(), true);
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