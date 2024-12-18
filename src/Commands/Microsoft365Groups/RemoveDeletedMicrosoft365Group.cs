using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Remove, "PnPDeletedMicrosoft365Group")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class RemoveDeletedMicrosoft365Group : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            ClearOwners.PermanentlyDeleteDeletedGroup(RequestHelper, Identity.GetDeletedGroupId(RequestHelper));
        }
    }
}