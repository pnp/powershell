using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "GroupPermissions")]
    public class GetGroupPermissions : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "ByName")]
        public GroupPipeBind Identity = new GroupPipeBind();
        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(SelectedWeb);
            var roleAssignment = SelectedWeb.RoleAssignments.GetByPrincipal(group);
            var roleDefinitionBindings = roleAssignment.RoleDefinitionBindings;
            ClientContext.Load(roleDefinitionBindings);
            ClientContext.ExecuteQueryRetry();

            WriteObject(roleDefinitionBindings, true);
        }
    }
}
