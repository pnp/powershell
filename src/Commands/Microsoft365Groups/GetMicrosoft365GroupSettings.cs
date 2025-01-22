using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupSettings")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.ReadWrite.All")]
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
                var groupId = Identity.GetGroupId(RequestHelper);
                var groupSettingId = GroupSetting.GetGroupSettingId(RequestHelper);
                var groupSettings = Microsoft365GroupsUtility.GetGroupSettings(RequestHelper, groupSettingId.ToString(), groupId.ToString());
                WriteObject(groupSettings, true);
            }
            else if (Identity != null && GroupSetting == null)
            {
                var groupId = Identity.GetGroupId(RequestHelper);
                var groupSettings = Microsoft365GroupsUtility.GetGroupSettings(RequestHelper, groupId.ToString());
                WriteObject(groupSettings?.Value, true);
            }
            else if (Identity == null && GroupSetting != null)
            {
                var groupSettingId = GroupSetting.GetGroupSettingId(RequestHelper);
                var groupSettings = Microsoft365GroupsUtility.GetGroupTenantSettings(RequestHelper, groupSettingId.ToString());
                WriteObject(groupSettings, true);
            }
            else if (Identity == null && GroupSetting == null)
            {
                var groupSettings = Microsoft365GroupsUtility.GetGroupSettings(RequestHelper);
                WriteObject(groupSettings?.Value, true);
            }
        }
    }
}