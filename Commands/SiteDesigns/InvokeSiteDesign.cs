using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPSiteDesign", SupportsShouldProcess = true)]
    public class InvokeSiteDesign : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantSiteDesignPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string WebUrl;

        protected override void ExecuteCmdlet()
        {
            var url = SelectedWeb.EnsureProperty(w => w.Url);
            var tenantUrl = UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
            using (var tenantContext = ClientContext.Clone(tenantUrl))
            {
                var tenant = new Tenant(tenantContext);
                var webUrl = url;
                if (!string.IsNullOrEmpty(WebUrl))
                {
                    try
                    {
                        var uri = new System.Uri(WebUrl);
                        webUrl = WebUrl;
                    }
                    catch
                    {
                        ThrowTerminatingError(new ErrorRecord(new System.Exception("Invalid URL"), "INVALIDURL", ErrorCategory.InvalidArgument, WebUrl));
                    }
                }
                TenantSiteDesign design = Identity.GetTenantSiteDesign(tenant);
                if (design != null)
                {
                    var results = tenant.ApplySiteDesign(webUrl, design.Id);
                    tenantContext.Load(results);
                    tenantContext.ExecuteQueryRetry();
                    WriteObject(results, true);
                }
            }
        }
    }
}