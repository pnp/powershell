using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPListItemPermission")]
    public class GetListItemPermission : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ListItemPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

            var item = Identity.GetListItem(list);

            if (item == null)
            {
                throw new PSArgumentException($"Cannot find list item provided through -{nameof(Identity)}", nameof(Identity));
            }

            ClientContext.Load(item, a => a.RoleAssignments.Include(roleAsg => roleAsg.Member.LoginName,
                    roleAsg => roleAsg.Member.PrincipalType, roleAsg => roleAsg.Member.Id,
                    roleAsg => roleAsg.RoleDefinitionBindings.Include(roleDef => roleDef.Name,
                    roleDef => roleDef.Description, roleDef => roleDef.Id, roleDef => roleDef.RoleTypeKind)), a => a.HasUniqueRoleAssignments);
            ClientContext.ExecuteQueryRetry();

            var listItemPermissions = new List<ListItemPermission>();
            var listItemPermissionCollection = new ListItemPermissionCollection
            {
                HasUniqueRoleAssignments = item.HasUniqueRoleAssignments
            };

            foreach (var roleAssignment in item.RoleAssignments)
            {
                var listItemPermission = new ListItemPermission
                {
                    PrincipalName = roleAssignment.Member.LoginName,
                    PrincipalType = roleAssignment.Member.PrincipalType,
                    PrincipalId = roleAssignment.Member.Id
                };

                List<RoleDefinition> roles = new List<RoleDefinition>();
                foreach (var role in roleAssignment.RoleDefinitionBindings)
                {
                    roles.Add(role);
                }

                listItemPermission.PermissionLevels = roles;
                listItemPermissions.Add(listItemPermission);

                listItemPermissionCollection.Permissions = listItemPermissions;
            }

            WriteObject(listItemPermissionCollection, true);
        }
    }
}