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
                var groupId = Identity.GetGroupId(GraphRequestHelper);
                var groupSettingId = GroupSetting.GetGroupSettingId(GraphRequestHelper);
                var groupSettings = Microsoft365GroupsUtility.GetGroupSettings(GraphRequestHelper, groupSettingId.ToString(), groupId.ToString());
                WriteObject(groupSettings, true);
            }
            else if (Identity != null && GroupSetting == null)
            {
                var groupId = Identity.GetGroupId(GraphRequestHelper);
                var groupSettings = Microsoft365GroupsUtility.GetGroupSettings(GraphRequestHelper, groupId.ToString());
                WriteObject(groupSettings?.Value, true);
            }
            else if (Identity == null && GroupSetting != null)
            {
                var groupSettingId = GroupSetting.GetGroupSettingId(GraphRequestHelper);
                var groupSettings = Microsoft365GroupsUtility.GetGroupTenantSettings(GraphRequestHelper, groupSettingId.ToString());
                WriteObject(groupSettings, true);
            }
            else if (Identity == null && GroupSetting == null)
            {
                var groupSettings = Microsoft365GroupsUtility.GetGroupSettings(GraphRequestHelper);
                WriteObject(groupSettings?.Value, true);
            }
        }
    }
}