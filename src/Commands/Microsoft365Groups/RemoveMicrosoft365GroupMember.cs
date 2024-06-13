using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Remove, "PnPMicrosoft365GroupMember")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class RemoveMicrosoft365GroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Users;

        protected override void ExecuteCmdlet()
        {
            Microsoft365GroupsUtility.RemoveMembersAsync(this, Connection, Identity.GetGroupId(this, Connection, AccessToken), Users, AccessToken).GetAwaiter().GetResult();
        }
    }
}