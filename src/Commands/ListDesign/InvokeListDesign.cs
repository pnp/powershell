using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPListDesign")]
    [OutputType(typeof(TenantSiteScriptActionResult))]
    public class InvokeListDesign : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantListDesignPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string WebUrl;

        protected override void ExecuteCmdlet()
        {
            var url = CurrentWeb.EnsureProperty(w => w.Url);
            var tenantUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
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

                // Retrieve the list designs
                var designs = Identity.GetTenantListDesign(tenant);

                if (designs == null || designs.Length == 0)
                {
                    throw new PSArgumentException("No list designs found matching the identity provided through Identity", nameof(Identity));
                }

                foreach (var design in designs)
                {
                    WriteVerbose($"Invoking list design '{design.Title}' ({design.Id})");

                    var results = tenant.ApplyListDesign(webUrl, design.Id);
                    tenantContext.Load(results);
                    tenantContext.ExecuteQueryRetry();
                    WriteObject(results, true);
                }
            }
        }
    }
}