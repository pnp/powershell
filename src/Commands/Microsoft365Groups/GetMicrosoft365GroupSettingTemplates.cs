using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupSettingTemplates")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Directory.Read.All")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Directory.ReadWrite.All")]
    public class GetMicrosoft365GroupSettingTemplates : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;
        
        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var groupSettingTemplate = ClearOwners.GetGroupTemplateSettings(this, Connection, AccessToken, Identity);
                WriteObject(groupSettingTemplate);
            }
            else
            {
                var groupSettingTemplates = ClearOwners.GetGroupTemplateSettings(this, Connection, AccessToken);
                WriteObject(groupSettingTemplates?.Value, true);
            }
        }
    }
}