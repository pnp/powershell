using System.Management.Automation;
using PnP.Core.Model.Security;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPGroupPermissions")]
    [OutputType(typeof(IRoleDefinition))]
    public class GetGroupPermissions : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public GroupPipeBind Identity = new GroupPipeBind();

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(Connection.PnPContext);

            if (group == null)
                throw new PSArgumentException("Site group not found", nameof(Identity));

            var roleDefinitions = group.GetRoleDefinitions();
            if (roleDefinitions != null)
            {
                WriteObject(roleDefinitions.RequestedItems, true);
            }
        }
    }
}
