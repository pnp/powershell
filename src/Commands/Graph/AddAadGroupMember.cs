using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPAadGroupMember")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class AddAadGroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AadGroupPipeBind Identity;

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

            GroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                GroupsUtility.AddGroupMembers(group.GroupId, Users, AccessToken, RemoveExisting.ToBool());
            }
        }
    }
}