using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPSiteDesign", DefaultParameterSetName = ParameterSet_BYSITEDESIGN)]
    [OutputType(typeof(ClientObjectList<TenantSiteScriptActionResult>), ParameterSetName = new[] { ParameterSet_BYSITEDESIGN })]
    [OutputType(typeof(IEnumerable<InvokeSiteScriptActionResponse>), ParameterSetName = new[] { ParameterSet_BYBUILTINTEMPLATE })]
    public class InvokeSiteDesign : PnPWebCmdlet
    {
        private const string ParameterSet_BYSITEDESIGN = "By Site Design";
        private const string ParameterSet_BYBUILTINTEMPLATE = "By Built-in Template";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYSITEDESIGN)]
        public TenantSiteDesignPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYBUILTINTEMPLATE)]
        public BuiltInSiteTemplates Template;

        [Parameter(Mandatory = false)]
        public string WebUrl;

        protected override void ExecuteCmdlet()
        {
            var url = CurrentWeb.EnsureProperty(w => w.Url);
            var webUrl = string.IsNullOrEmpty(WebUrl) ? url : WebUrl;

            if (!string.IsNullOrEmpty(WebUrl))
            {
                try { _ = new System.Uri(WebUrl); }
                catch
                {
                    ThrowTerminatingError(new ErrorRecord(new System.Exception("Invalid URL"), "INVALIDURL", ErrorCategory.InvalidArgument, WebUrl));
                }
            }

            switch (ParameterSetName)
            {
                case ParameterSet_BYBUILTINTEMPLATE:
                    InvokeBuiltInTemplate(webUrl);
                    break;

                default:
                    InvokeCustomDesign(webUrl);
                    break;
            }
        }

        private void InvokeBuiltInTemplate(string webUrl)
        {
            var entry = BuiltInSiteTemplateSettings.BuiltInSiteTemplateMappings.FirstOrDefault(kv => kv.Value == Template);
            if (entry.Key == System.Guid.Empty)
            {
                ThrowTerminatingError(new ErrorRecord(
                    new System.ArgumentException($"Template '{Template}' could not be resolved to a known built-in site design GUID"),
                    "UNKNOWNTEMPLATE", ErrorCategory.InvalidArgument, Template));
            }

            var designId = entry.Key;

            LogDebug($"Invoking built-in site design '{Template}' ({designId}) on {webUrl}");

            var results = SiteTemplates.ApplyBuiltInSiteDesign(SharePointRequestHelper, designId, webUrl);

            if (results?.Items != null)
            {
                var items = results.Items.ToList();
                LogDebug($"Built-in site design result: {items.Count(r => r.ErrorCode == 0)} actions successful, {items.Count(r => r.ErrorCode != 0)} failed");
                WriteObject(items, true);
            }
        }

        private void InvokeCustomDesign(string webUrl)
        {
            var tenantUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
            using (var tenantContext = ClientContext.Clone(tenantUrl))
            {
                var tenant = new Tenant(tenantContext);

                var designs = Identity.GetTenantSiteDesign(tenant);

                if (designs == null || designs.Length == 0)
                {
                    throw new PSArgumentException("No site designs found matching the identity provided through Identity", nameof(Identity));
                }

                foreach (var design in designs)
                {
                    LogDebug($"Invoking site design '{design.Title}' ({design.Id})");

                    var results = tenant.ApplySiteDesign(webUrl, design.Id);
                    tenantContext.Load(results);
                    tenantContext.ExecuteQueryRetry();
                    WriteObject(results, true);
                }
            }
        }
    }
}
