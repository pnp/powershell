using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupOwner")]
    [Alias("Get-PnPMicrosoft365GroupOwners")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetMicrosoft365GroupOwner : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var owners = ClearOwners.GetOwners(this, Connection, Identity.GetGroupId(this, Connection, AccessToken), AccessToken);
            WriteObject(owners?.OrderBy(o => o.DisplayName), true);
        }
    }
}