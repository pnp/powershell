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
        public Microsoft365GroupPipeBind Identity;
        
        [Parameter(Mandatory = false)]
        public Microsoft365GroupSettingsPipeBind GroupSetting;
        
        protected override void ExecuteCmdlet()
        {
            if (Identity != null && GroupSetting != null)
            {
                var groupId = Identity.GetGroupId(this, Connection, AccessToken);
                var groupSettingId = GroupSetting.GetGroupSettingId(this, Connection, AccessToken);
                var groupSettings = ClearOwners.GetGroupSettings(this, Connection, AccessToken, groupSettingId.ToString(), groupId.ToString());
                WriteObject(groupSettings, true);
            }
            else if (Identity != null && GroupSetting == null)
            {
                var groupId = Identity.GetGroupId(this, Connection, AccessToken);
                var groupSettings = ClearOwners.GetGroupSettings(this, Connection, AccessToken, groupId.ToString());
                WriteObject(groupSettings?.Value, true);
            }
            else if (Identity == null && GroupSetting != null) 
            {
                var groupSettingId = GroupSetting.GetGroupSettingId(this, Connection, AccessToken);
                var groupSettings = ClearOwners.GetGroupTenantSettings(this, Connection, AccessToken, groupSettingId.ToString());
                WriteObject(groupSettings, true);
            }
            else if(Identity == null && GroupSetting == null)
            {
                var groupSettings = ClearOwners.GetGroupSettings(this, Connection, AccessToken);
                WriteObject(groupSettings?.Value, true);
            }
        }
    }
}