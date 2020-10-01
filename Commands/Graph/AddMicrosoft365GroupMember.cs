using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPMicrosoft365GroupMember")]
    [Alias("Add-PnPUnifiedGroupMember")]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.None, MicrosoftGraphApiPermission.User_ReadWrite_All | MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]
    public class AddMicrosoft365GroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Users;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveExisting;

        protected override void ExecuteCmdlet()
        {
            if (PnPConnection.CurrentConnection.ClientId == PnPConnection.PnPManagementShellClientId)
            {
                PnPConnection.CurrentConnection.Scopes = new[] { "Group.ReadWrite.All" };
            }

            UnifiedGroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                UnifiedGroupsUtility.AddUnifiedGroupMembers(group.GroupId, Users, AccessToken, RemoveExisting.ToBool());
            }
        }
    }
}