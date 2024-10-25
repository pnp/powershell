using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupSettings")]
    [RequiredApiApplicationPermissions("graph/Directory.Read.All")]
    [RequiredApiApplicationPermissions("graph/Directory.ReadWrite.All")]
    public class GetMicrosoft365GroupSettings : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public Microsoft365GroupPipeBind Group;
        
        [Parameter(Mandatory = false)]
        public Microsoft365GroupSettingsPipeBind Identity;
        
        protected override void ExecuteCmdlet()
        {
            if (Identity != null && Group !=null)
            {
                var groupId = Group.GetGroupId(this, Connection, AccessToken);
                var groupSettings = ClearOwners.GetGroupSettings(this, Connection, AccessToken,Identity.Id.ToString ,groupId.ToString());
                WriteObject(groupSettings?.Value, true);
            }
            elseif(Identity != null && Group == null)
            {
                var groupSettings = ClearOwners.GetGroupSettings(this, Connection, AccessToken,Identity.Id.ToString());
                WriteObject(groupSettings?.Value, true);
            }
            elseif(Identity == null && Group ==null)
            {
                var groupSettings = ClearOwners.GetGroupSettings(this, Connection, AccessToken);
                WriteObject(groupSettings?.Value, true);
            }
        }
    }
}