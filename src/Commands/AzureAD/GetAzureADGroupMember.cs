using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADGroupMember")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    [Alias("Get-PnPEntraIDGroupMember")]
    public class GetAzureADGroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(Connection, AccessToken);
            }

            if (group != null)
            {
                // Get members of the group
                var members = Microsoft365GroupsUtility.GetMembersAsync(Connection, new Guid(group.Id), AccessToken).GetAwaiter().GetResult();
                WriteObject(members?.OrderBy(m => m.DisplayName), true);
            }
        }
    }
}