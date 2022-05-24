using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPListItemPermissions")]
    public class GetListItemPermission : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
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
                    roleAsg => roleAsg.RoleDefinitionBindings.Include(roleDef => roleDef.Name,
                    roleDef => roleDef.Description)));
            ClientContext.ExecuteQueryRetry();

            var listItemPermissions = new List<ListItemPermissions>();

            foreach (var roleAssignment in item.RoleAssignments)
            {
                roleAssignment.EnsureProperties(r => r.Member, r => r.Member.PrincipalType, r => r.Member.Id, r => r.Member.Id);
                var listItemPermission = new ListItemPermissions
                {
                    Principal = roleAssignment.Member.LoginName,
                    PrincipalType = roleAssignment.Member.PrincipalType,
                    PrincipalId = roleAssignment.Member.Id
                };

                List<RoleDefinition> roles = new List<RoleDefinition>();
                foreach (var role in roleAssignment.RoleDefinitionBindings)
                {
                    roles.Add(role);
                }

                listItemPermission.Permissions = roles;
                listItemPermissions.Add(listItemPermission);
            }

            WriteObject(listItemPermissions, true);
        }
    }
}