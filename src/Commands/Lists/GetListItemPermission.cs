using System.Collections.Generic;
using System.Management.Automation;
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
            var list = List.GetList(PnPContext);

            if (list == null)
            {
                throw new PSArgumentException($"Cannot find list provided through - {nameof(List)}", nameof(List));
            }

            var item = Identity.GetListItem(list);

            if (item == null)
            {
                throw new PSArgumentException($"Cannot find list item provided through -{nameof(Identity)}", nameof(Identity));
            }

            item.LoadAsync(w => w.RoleAssignments.QueryProperties(p => p.RoleDefinitions, p => p.PrincipalId)).GetAwaiter().GetResult();

            var listItemPermissions = new List<ListItemPermissions>();

            foreach (var roleAssignment in item.RoleAssignments.AsRequested())
            {
                var listItemPermission = new ListItemPermissions
                {
                    RoleDefinitions = roleAssignment.RoleDefinitions.AsRequested(),
                    PrincipalId = roleAssignment.PrincipalId
                };

                listItemPermissions.Add(listItemPermission);
            }

            WriteObject(listItemPermissions, true);
        }
    }
}