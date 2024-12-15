using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupSettingTemplates")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.ReadWrite.All")]
    public class GetMicrosoft365GroupSettingTemplates : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var groupSettingTemplate = ClearOwners.GetGroupTemplateSettings(RequestHelper, Identity);
                WriteObject(groupSettingTemplate);
            }
            else
            {
                var groupSettingTemplates = ClearOwners.GetGroupTemplateSettings(RequestHelper);
                WriteObject(groupSettingTemplates?.Value, true);
            }
        }
    }
}