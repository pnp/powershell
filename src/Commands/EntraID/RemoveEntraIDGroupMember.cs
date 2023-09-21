using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Remove, "PnPEntraIDGroupMember")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    [Alias("Remove-PnPAzureADGroupMember")]
    public class RemoveEntraIDGroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public EntraIDGroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Users;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(Connection, AccessToken);
            }

            if (group != null)
            {
                Microsoft365GroupsUtility.RemoveMembersAsync(Connection, new System.Guid(group.Id), Users, AccessToken).GetAwaiter().GetResult();
            }
        }
    }
}