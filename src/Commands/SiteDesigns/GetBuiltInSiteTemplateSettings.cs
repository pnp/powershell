﻿using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPBuiltInSiteTemplateSettings")]
    public class GetBuiltInSiteTemplateSettings : PnPSharePointOnlineAdminCmdlet
    {
        private const string ByIdentityParamSet = "ByIdentity";
        private const string ByTemplateParamSet = "ByTemplate";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ByIdentityParamSet)]
        public BuiltInSiteTemplateSettingsPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ByTemplateParamSet)]
        public BuiltInSiteTemplates Template;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)) || ParameterSpecified(nameof(Template)))
            {
                ClientResult<TenantOutOfBoxSiteTemplateSettings> templateSetting = null;
                if (ParameterSpecified(nameof(Identity)))
                {
                    if (Identity == null || !Identity.Id.HasValue) throw new PSArgumentException($"Identity contains an invalid {nameof(BuiltInSiteTemplateSettingsPipeBind)} value", nameof(Identity));

                    templateSetting = Tenant.GetOutOfBoxSiteTemplateSettings(AdminContext, Identity.Id.Value);
                }
                else
                {
                    var template = BuiltInSiteTemplateSettings.BuiltInSiteTemplateMappings.FirstOrDefault(tm => tm.Value == Template);
                    templateSetting = Tenant.GetOutOfBoxSiteTemplateSettings(AdminContext, template.Key);
                }
                AdminContext.ExecuteQueryRetry();

                if(templateSetting == null || templateSetting.Value == null)
                {
                    LogDebug("No out of the box SharePoint site template setting with the identity provided through Identity has been found");
                    return;
                }

                var response = new BuiltInSiteTemplateSettings
                {
                    Id = templateSetting.Value.Id,
                    IsHidden = templateSetting.Value.IsHidden
                };

                WriteObject(response, false);
            }
            else
            {
                LogDebug("Retrieving all out of the box SharePoint site template settings");

                var templateSettings = Tenant.GetAllOutOfBoxSiteTemplateSettings();
                AdminContext.ExecuteQueryRetry();

                LogDebug($"{templateSettings.Count} returned");

                var responses = templateSettings.Select(ts => new BuiltInSiteTemplateSettings
                {
                    Id = ts.Id,
                    IsHidden = ts.IsHidden
                });

                WriteObject(responses, true);
            }
        }
    }
}