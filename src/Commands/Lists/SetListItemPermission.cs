using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPListItemPermission", DefaultParameterSetName = "User")]
    public class SetListItemPermission : PnPWebCmdlet
    {
        private const string ParameterSet_GROUP = "Group";
        private const string ParameterSet_USER = "User";
        private const string ParameterSet_INHERIT = "Inherit";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_GROUP)]
        public GroupPipeBind Group;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USER)]
        public string User;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_GROUP)]
        public string AddRole = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_GROUP)]
        public string RemoveRole = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_GROUP)]
        public SwitchParameter ClearExisting;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INHERIT)]
        public SwitchParameter InheritPermissions;

        [Parameter(Mandatory = false)]
        public SwitchParameter SystemUpdate;

        protected override void ExecuteCmdlet()
        {
            List list = null;
            if (List != null)
            {
                list = List.GetList(CurrentWeb);
            }
            if (list != null)
            {
                var item = Identity.GetListItem(list);
                if (item != null)
                {
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
                        if (principal != null)
                        {
                            if (!string.IsNullOrEmpty(AddRole))
                            {
                                var roleDefinition = CurrentWeb.RoleDefinitions.GetByName(AddRole);
                                var roleDefinitionBindings = new RoleDefinitionBindingCollection(ClientContext)
                            {
                                roleDefinition
                            };
                                var roleAssignments = item.RoleAssignments;
                                roleAssignments.Add(principal, roleDefinitionBindings);
                                ClientContext.Load(roleAssignments);
                                ClientContext.ExecuteQueryRetry();
                            }
                            if (!string.IsNullOrEmpty(RemoveRole))
                            {
                                var roleAssignment = item.RoleAssignments.GetByPrincipal(principal);
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
                            WriteError(new ErrorRecord(new Exception("Principal not found"), "1", ErrorCategory.ObjectNotFound, null));
                        }
                    }
                }
            }
        }
    }
}