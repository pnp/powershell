using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Reset, "PnPMicrosoft365GroupExpiration")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class ResetMicrosoft365GroupExpiration : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(AccessToken, false);
            UnifiedGroupsUtility.RenewUnifiedGroup(group.GroupId, AccessToken);
        }
    }
}