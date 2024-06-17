using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPBuiltInSiteTemplateSettings")]
    [OutputType(typeof(BuiltInSiteTemplateSettings))]
    public class SetBuiltInSiteTemplateSettings : PnPAdminCmdlet
    {
        private const string ByIdentityParamSet = "ByIdentity";
        private const string ByTemplateParamSet = "ByTemplate";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ByIdentityParamSet)]
        public BuiltInSiteTemplateSettingsPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ByTemplateParamSet)]
        public BuiltInSiteTemplates Template;

        [Parameter(Mandatory = true, ParameterSetName = ByIdentityParamSet)]
        [Parameter(Mandatory = true, ParameterSetName = ByTemplateParamSet)]
        public bool IsHidden;

        [Parameter(Mandatory = false, ParameterSetName = ByIdentityParamSet)]
        [Parameter(Mandatory = false, ParameterSetName = ByTemplateParamSet)]

        public SwitchParameter WhatIf;

        protected override void ExecuteCmdlet()
        {
            ClientResult<TenantOutOfBoxSiteTemplateSettings> templateSetting = null;
            if (ParameterSpecified(nameof(Identity)))
            {
                if (Identity == null || !Identity.Id.HasValue) throw new PSArgumentException($"Identity contains an invalid {nameof(BuiltInSiteTemplateSettingsPipeBind)} value", nameof(Identity));                

                if (!ParameterSpecified(nameof(WhatIf)))
                {
                    WriteVerbose($"Setting built in site template settings for template with Id {Identity.Id.Value} to become {(IsHidden ? "hidden" : "visible")}");

                    templateSetting = Tenant.SetTenantOutOfBoxSiteTemplateSettings(new TenantOutOfBoxSiteTemplateSettings
                    {
                        Id = Identity.Id.Value,
                        IsHidden = IsHidden
                    });
                }
                else
                {
                    WriteVerbose($"Omitting setting built in site template settings for template with Id {Identity.Id.Value} to become {(IsHidden ? "hidden" : "visible")} as {nameof(WhatIf)} has been provided");
                }
            }
            
            if(ParameterSpecified(nameof(Template)))
            {
                var template = BuiltInSiteTemplateSettings.BuiltInSiteTemplateMappings.FirstOrDefault(tm => tm.Value == Template);                

                if (!ParameterSpecified(nameof(WhatIf)))
                {
                    WriteVerbose($"Setting built in site template settings for template with Id {template.Key} to become {(IsHidden ? "hidden" : "visible")}");

                    templateSetting = Tenant.SetTenantOutOfBoxSiteTemplateSettings(new TenantOutOfBoxSiteTemplateSettings
                    {
                        Id = template.Key,
                        IsHidden = IsHidden
                    });
                }
                else
                {
                    WriteVerbose($"Omitting setting built in site template settings for template with Id {template.Key} to become {(IsHidden ? "hidden" : "visible")} as {nameof(WhatIf)} has been provided");
                }
            }

            if (ParameterSpecified(nameof(WhatIf))) return;

            AdminContext.ExecuteQueryRetry();

            if(templateSetting == null || templateSetting.Value == null)
            {
                WriteVerbose("Invalid response received");
                return;
            }

            WriteVerbose("Mapping response to BuiltInSiteTemplateSettings result");

            var response = new BuiltInSiteTemplateSettings
            {
                Id = templateSetting.Value.Id,
                IsHidden = templateSetting.Value.IsHidden
            };
            WriteObject(response, false);
        }
    }
}