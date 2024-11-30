using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsData.Restore, "PnPDeletedMicrosoft365Group")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    public class RestoreDeletedMicrosoft365Group : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            WriteObject(ClearOwners.RestoreDeletedGroup(RequestHelper, Identity.GetDeletedGroupId(RequestHelper)));
        }
    }
}