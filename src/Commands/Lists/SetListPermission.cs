using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    //TODO: Create Test
    [Cmdlet(VerbsCommon.Set, "PnPListPermission")]
    [OutputType(typeof(void))]
    public class SetListPermission : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = "Group")]
        public GroupPipeBind Group;

        [Parameter(Mandatory = true, ParameterSetName = "User")]
        public string User;

        [Parameter(Mandatory = false)]
        public string AddRole = string.Empty;

        [Parameter(Mandatory = false)]
        public string RemoveRole = string.Empty;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);

            if (list == null)
            {
                throw new PSArgumentException($"No list found with id, title or url '{Identity}'", "Identity");
            }

            Principal principal = null;
            if (ParameterSetName == "Group")
            {
                if (Group.Id != -1)
                {
                    principal = CurrentWeb.SiteGroups.GetById(Group.Id);
                }
                else if (!string.IsNullOrEmpty(Group.Name))
                {
                    principal = CurrentWeb.SiteGroups.GetByName(Group.Name);
                }
                else if (Group.Group != null)
                {
                    principal = Group.Group;
                }
            }
            else
            {
                principal = CurrentWeb.EnsureUser(User);
                ClientContext.ExecuteQueryRetry();
            }
            if (principal != null)
            {
                if (!string.IsNullOrEmpty(AddRole))
                {
                    var roleDefinition = CurrentWeb.RoleDefinitions.GetByName(AddRole);
                    var roleDefinitionBindings = new RoleDefinitionBindingCollection(ClientContext);
                    roleDefinitionBindings.Add(roleDefinition);
                    var roleAssignments = list.RoleAssignments;
                    roleAssignments.Add(principal, roleDefinitionBindings);
                    ClientContext.Load(roleAssignments);
                    ClientContext.ExecuteQueryRetry();
                }
                if (!string.IsNullOrEmpty(RemoveRole))
                {
                    var roleAssignment = list.RoleAssignments.GetByPrincipal(principal);
                    var roleDefinitionBindings = roleAssignment.RoleDefinitionBindings;
                    ClientContext.Load(roleDefinitionBindings);
                    ClientContext.ExecuteQueryRetry();
                    foreach (var roleDefinition in roleDefinitionBindings.Where(roleDefinition => roleDefinition.Name == RemoveRole))
                    {
                        roleDefinitionBindings.Remove(roleDefinition);
                        roleAssignment.Update();
                        ClientContext.ExecuteQueryRetry();
                        break;
                    }
                }
            }
            else
            {
                LogError(new Exception("Principal not found"));
            }
        }
    }
}
