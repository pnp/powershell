using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPListItemPermission", DefaultParameterSetName = ParameterSet_USER)]
    [OutputType(typeof(void))]
    public class SetListItemPermission : PnPWebCmdlet
    {
        private const string ParameterSet_GROUP = "Group";
        private const string ParameterSet_USER = "User";
        private const string ParameterSet_INHERIT = "Inherit";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_GROUP)]
        public GroupPipeBind Group;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USER)]
        public string User;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_GROUP)]
        public RoleDefinitionPipeBind AddRole;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_GROUP)]
        public RoleDefinitionPipeBind RemoveRole;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_GROUP)]
        public SwitchParameter ClearExisting;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INHERIT)]
        public SwitchParameter InheritPermissions;

        [Parameter(Mandatory = false)]
        public SwitchParameter SystemUpdate;

        protected override void ExecuteCmdlet()
        {
            // Retrieve the list
            List list = null;
            if (List != null)
            {
                list = List.GetList(CurrentWeb);
            }

            // Ensure the list exists
            if (list == null)
            {
                throw new PSArgumentException("The provided List through the List parameter could not be found", nameof(List));
            }

            // Retrieve the list item
            var item = Identity.GetListItem(list);

            // Ensure the list item exists
            if (item == null)
            {
                throw new PSArgumentException("The provided list item through the Identity parameter could not be found", nameof(Identity));
            }

            item.EnsureProperties(i => i.HasUniqueRoleAssignments);

            if (ParameterSetName == ParameterSet_INHERIT)
            {
                if (item.HasUniqueRoleAssignments && InheritPermissions.IsPresent)
                {
                    item.ResetRoleInheritance();
                    if (SystemUpdate)
                    {
                        item.SystemUpdate();
                    }
                    else
                    {
                        item.Update();
                    }
                    ClientContext.ExecuteQueryRetry();
                }
            }
            else
            {
                if (!item.HasUniqueRoleAssignments)
                {
                    item.BreakRoleInheritance(!ClearExisting.IsPresent, true);
                }
                else if (ClearExisting.IsPresent)
                {
                    item.ResetRoleInheritance();
                    item.BreakRoleInheritance(!ClearExisting.IsPresent, true);
                }

                if (SystemUpdate.IsPresent)
                {
                    item.SystemUpdate();
                }
                else
                {
                    item.Update();
                }

                ClientContext.ExecuteQueryRetry();

                Principal principal = null;
                if (ParameterSetName == ParameterSet_GROUP)
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
                if (principal == null)
                {
                    throw new PSArgumentException("The provided principal through the Principal parameter could not be found", nameof(Principal));
                }
                if (ParameterSpecified(nameof(AddRole)))
                {
                    var roleDefinition = AddRole.GetRoleDefinition(ClientContext.Site);
                    var roleDefinitionBindings = new RoleDefinitionBindingCollection(ClientContext)
                            {
                                roleDefinition
                            };
                    var roleAssignments = item.RoleAssignments;
                    roleAssignments.Add(principal, roleDefinitionBindings);
                    ClientContext.Load(roleAssignments);
                    ClientContext.ExecuteQueryRetry();
                }
                if (ParameterSpecified(nameof(RemoveRole)))
                {
                    var roleDefinition = RemoveRole.GetRoleDefinition(ClientContext.Site);
                    var roleAssignment = item.RoleAssignments.GetByPrincipal(principal);
                    var roleDefinitionBindings = roleAssignment.RoleDefinitionBindings;
                    ClientContext.Load(roleDefinitionBindings);
                    ClientContext.ExecuteQueryRetry();
                    foreach (var roleDefinitionBinding in roleDefinitionBindings.Where(rd => rd.Name == roleDefinition.Name))
                    {
                        roleDefinitionBindings.Remove(roleDefinitionBinding);
                        roleAssignment.Update();
                        ClientContext.ExecuteQueryRetry();
                        break;
                    }
                }
            }
        }
    }
}