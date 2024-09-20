using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.New, "PnPMicrosoft365GroupSettings")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Directory.ReadWrite.All")]
    public class NewPnPMicrosoft365GroupSettings : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public Microsoft365GroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string DisplayName;

        [Parameter(Mandatory = true)]
        public string TemplateId;

        [Parameter(Mandatory = false)]
        public Hashtable Values;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var groupId = Identity.GetGroupId(this, Connection, AccessToken);
                var groupSettingObject = GroupSettingsObject();

                var responseValue = ClearOwners.CreateGroupSetting(this, Connection, AccessToken, groupId.ToString(), groupSettingObject);
                WriteObject(responseValue);
            }
            else
            {
                var groupSettingObject = GroupSettingsObject();

                var responseValue = ClearOwners.CreateGroupSetting(this, Connection, AccessToken, groupSettingObject);
                WriteObject(responseValue);
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
                displayName = DisplayName,
                templateId = TemplateId,
                values = groupSettingItemValues.ToArray()
            };

            return groupSettingObject;
        }
    }
}