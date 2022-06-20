using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsCommon.Add, "PnPAzureADGroupMember")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class AddAzureADGroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Users;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveExisting;

        protected override void ExecuteCmdlet()
        {
            if (Connection.ClientId == PnPConnection.PnPManagementShellClientId)
            {
                Connection.Scopes = new[] { "Group.ReadWrite.All" };
            }

            AzureADGroup group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                GroupsUtility.AddGroupMembers(group.Id, Users, AccessToken, RemoveExisting.ToBool());
            }
        }
    }
}