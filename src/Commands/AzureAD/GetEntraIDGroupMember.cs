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
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [Alias("Get-PnPEntraIDGroupMember")]
    public class GetAzureADGroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter Transitive;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(GraphRequestHelper);
            }

            if (group != null)
            {
                // Get members of the group
                var members = Transitive
                    ? Microsoft365GroupsUtility.GetTransitiveMembers(GraphRequestHelper, new Guid(group.Id))
                    : Microsoft365GroupsUtility.GetMembers(GraphRequestHelper, new Guid(group.Id));
                    WriteObject(members?.OrderBy(m => m.DisplayName), true);

            }
        }
    }
}