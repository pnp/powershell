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
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
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
                group = Identity.GetGroup(RequestHelper);
            }

            if (group != null)
            {
                var members = Microsoft365GroupsUtility.GetMembers(RequestHelper, new System.Guid(group.Id));

                var membersToBeRemoved = members?.Select(p => p.UserPrincipalName).ToArray();

                Microsoft365GroupsUtility.RemoveMembers(RequestHelper, new System.Guid(group.Id), membersToBeRemoved);
            }
        }
    }
}