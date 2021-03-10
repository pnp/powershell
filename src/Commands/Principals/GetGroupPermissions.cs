using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPGroupPermissions")]
    public class GetGroupPermissions : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public GroupPipeBind Identity = new GroupPipeBind();

        protected override void ExecuteCmdlet()
        {
            var g = Identity.GetGroup(PnPContext);
            var r = g.GetRoleDefinitions();
            if (r != null)
            {
                WriteObject(r.RequestedItems, true);
            }
        }
    }
}
