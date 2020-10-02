using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Set, "GroupPermissions")]
    public class SetGroupPermissions : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "By Identity")]
        public GroupPipeBind Identity = new GroupPipeBind();

        [Parameter(Mandatory = false)]
        public ListPipeBind List = new ListPipeBind();

        [Parameter(Mandatory = false)]
        public string[] AddRole = null;

        [Parameter(Mandatory = false)]
        public string[] RemoveRole = null;

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(SelectedWeb);
            
            List list = List.GetList(SelectedWeb);
            if (list == null && !string.IsNullOrEmpty(List.Title))
            {
                throw new Exception($"List with Title {List.Title} not found");
            }
            else if (list == null && List.Id != Guid.Empty )
            {
                throw new Exception($"List with Id {List.Id} not found");
            }

            if (AddRole != null)
            {
                foreach (var role in AddRole)
                {
                    var roleDefinition = SelectedWeb.RoleDefinitions.GetByName(role);
                    var roleDefinitionBindings = new RoleDefinitionBindingCollection(ClientContext) { roleDefinition };

                    RoleAssignmentCollection roleAssignments;
                    if (list != null)
                    {
                        roleAssignments = list.RoleAssignments;
                    }
                    else
                    {
                        roleAssignments = SelectedWeb.RoleAssignments;
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
                        roleAssignment = SelectedWeb.RoleAssignments.GetByPrincipal(group);
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
