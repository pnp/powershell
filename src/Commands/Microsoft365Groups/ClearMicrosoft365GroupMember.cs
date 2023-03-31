using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Clear, "PnPMicrosoft365GroupMember")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class ClearMicrosoft365GroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
           Microsoft365GroupsUtility.ClearMembersAsync(Connection, Identity.GetGroupId(Connection, AccessToken), AccessToken).GetAwaiter().GetResult();
        }
    }
}