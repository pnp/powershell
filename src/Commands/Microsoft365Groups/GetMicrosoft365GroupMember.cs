using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupMember")]
    [Alias("Get-PnPMicrosoft365GroupMembers")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetMicrosoft365GroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var members = ClearOwners.GetMembers(this, Connection, Identity.GetGroupId(this, Connection, AccessToken), AccessToken);
            WriteObject(members?.OrderBy(m => m.DisplayName), true);
        }
    }
}