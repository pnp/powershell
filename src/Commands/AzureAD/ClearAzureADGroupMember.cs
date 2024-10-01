using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Clear, "PnPAzureADGroupMember")]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/Group.ReadWrite.All")]
    [Alias("Clear-PnPEntraIDGroupMember")]
    public class ClearAzureADGroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(this, Connection, AccessToken);
            }

            if (group != null)
            {
                var members = ClearOwners.GetMembers(this, Connection, new System.Guid(group.Id), AccessToken);

                var membersToBeRemoved = members?.Select(p => p.UserPrincipalName).ToArray();

                ClearOwners.RemoveMembers(this, Connection, new System.Guid(group.Id), membersToBeRemoved, AccessToken);
            }
        }
    }
}