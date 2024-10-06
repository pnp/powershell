using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Set, "PnPGroupPermissions")]
    [OutputType(typeof(void))]
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
            var pnpContext = Connection.PnPContext;
            var group = Identity.GetGroup(pnpContext);

            if (group == null)
                throw new PSArgumentException("Site group not found", nameof(Identity));

            PnP.Core.Model.SharePoint.IList list = null;
            if (ParameterSpecified(nameof(List)))
            {
                list = List.GetListOrThrow(nameof(List), pnpContext);
            }
            if (AddRole != null)
            {
                if (ParameterSpecified(nameof(List)))
                {
                    list.AddRoleDefinitions(group.Id, AddRole);
                }
                else
                {
                    group.AddRoleDefinitions(AddRole);
                }

            }
            if (RemoveRole != null)
            {
                if (ParameterSpecified(nameof(List)))
                {
                    list.RemoveRoleDefinitions(group.Id, RemoveRole);
                }
                else
                {
                    group.RemoveRoleDefinitions(RemoveRole);
                }
            }
        }
    }
}
