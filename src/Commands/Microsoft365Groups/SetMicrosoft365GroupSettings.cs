using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Set, "PnPMicrosoft365GroupSettings")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.ReadWrite.All")]
    public class SetMicrosoft365GroupSettings : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Identity;

        [Parameter(Mandatory = true)]
        public Hashtable Values;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Group;

        protected override void ExecuteCmdlet()
        {
            if (Group != null)
            {
                var groupId = Group.GetGroupId(RequestHelper);
                var groupSettingObject = GroupSettingsObject();

                ClearOwners.UpdateGroupSetting(RequestHelper, Identity, groupId.ToString(), groupSettingObject);
            }
            else
            {
                var groupSettingObject = GroupSettingsObject();
                ClearOwners.UpdateGroupSetting(RequestHelper, Identity, groupSettingObject);
            }
        }

        private dynamic GroupSettingsObject()
        {
            var groupSettingItemValues = new List<Microsoft365GroupSettingItemValues>();
            var groupSettingValues = Values ?? new Hashtable();

            foreach (var key in groupSettingValues.Keys)
            {
                var value = groupSettingValues[key];
                groupSettingItemValues.Add(new Microsoft365GroupSettingItemValues
                {
                    Name = key.ToString(),
                    Value = value
                });
            }

            var groupSettingObject = new
            {
                values = groupSettingItemValues.ToArray()
            };

            return groupSettingObject;
        }
    }
}