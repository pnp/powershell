using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Set, "PnPGroupPermissions")]
    public class SetGroupPermissions : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "By Identity")]
        public GroupPipeBind Identity = new GroupPipeBind();

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public string[] AddRole = null;

        [Parameter(Mandatory = false)]
        public string[] RemoveRole = null;

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(CurrentWeb);

            List list = List.GetListOrThrow(nameof(List), CurrentWeb);

            if (AddRole != null)
            {
                foreach (var role in AddRole)
                {
                    var roleDefinition = CurrentWeb.RoleDefinitions.GetByName(role);
                    var roleDefinitionBindings = new RoleDefinitionBindingCollection(ClientContext) { roleDefinition };

                    RoleAssignmentCollection roleAssignments;
                    if (list != null)
                    {
                        roleAssignments = list.RoleAssignments;
                    }
                    else
                    {
                        roleAssignments = CurrentWeb.RoleAssignments;
                    }

                    roleAssignments.Add(group, roleDefinitionBindings);
                    ClientContext.Load(roleAssignments);
                    ClientContext.ExecuteQueryRetry();
                }
            }
            if (RemoveRole != null)
            {
                foreach (var role in RemoveRole)
                {
                    RoleAssignment roleAssignment;
                    if (list != null)
                    {
                        roleAssignment = list.RoleAssignments.GetByPrincipal(group);
                    }
                    else
                    {
                        roleAssignment = CurrentWeb.RoleAssignments.GetByPrincipal(group);
                    }
                    var roleDefinitionBindings = roleAssignment.RoleDefinitionBindings;
                    ClientContext.Load(roleDefinitionBindings);
                    ClientContext.ExecuteQueryRetry();
                    foreach (var roleDefinition in roleDefinitionBindings.Where(roleDefinition => roleDefinition.Name == role))
                    {
                        roleDefinitionBindings.Remove(roleDefinition);
                        roleAssignment.Update();
                        ClientContext.ExecuteQueryRetry();
                        break;
                    }
                }
            }
        }
    }
}
