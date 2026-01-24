using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Clear, "PnPEntraIDGroupOwner")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [Alias("Clear-PnPAzureADGroupOwner")]
    public class ClearAzureADGroupOwner : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public EntraIDGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(GraphRequestHelper);
            }

            if (group != null)
            {
                var owners = Microsoft365GroupsUtility.GetOwners(GraphRequestHelper, new System.Guid(group.Id));

                var ownersToBeRemoved = owners?.Select(p => p.UserPrincipalName).ToArray();

                Microsoft365GroupsUtility.RemoveOwners(GraphRequestHelper, new System.Guid(group.Id), ownersToBeRemoved);
            }
        }
    }
}