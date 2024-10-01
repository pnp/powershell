using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Remove, "PnPMicrosoft365GroupSettings")]
    [RequiredApiApplicationPermissions("graph/Directory.ReadWrite.All")]
    public class RemoveMicrosoft365GroupSettings : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Group;

        protected override void ExecuteCmdlet()
        {
            if (Group != null)
            {
                var groupId = Group.GetGroupId(this, Connection, AccessToken);
                ClearOwners.RemoveGroupSetting(this, Connection, AccessToken, Identity, groupId.ToString());
            }
            else
            {
                ClearOwners.RemoveGroupSetting(this, Connection, AccessToken, Identity);
            }
        }
    }
}