using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Add, "PnPMicrosoft365GroupMember")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class AddMicrosoft365GroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Users;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveExisting;

        protected override void ExecuteCmdlet()
        {
            Microsoft365GroupsUtility.AddMembersAsync(this, Connection, Identity.GetGroupId(this, Connection, AccessToken), Users, AccessToken, RemoveExisting).GetAwaiter().GetResult();
        }
    }
}