using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPSiteScript")]
    public class InvokeSiteScript : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantSiteScriptPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string WebUrl;

        protected override void ExecuteCmdlet()
        {
            var url = CurrentWeb.EnsureProperty(w => w.Url);
            var tenantUrl = UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
            using (var tenantContext = ClientContext.Clone(tenantUrl))
            {
                var tenant = new Tenant(tenantContext);
                
                // Retrieve the site scripts
                var scripts = Identity.GetTenantSiteScript(tenant);

                if (scripts == null || scripts.Length == 0)
                {
                    throw new PSArgumentException("No site scripts found matching the identity provided through Identity", nameof(Identity));
                }

                foreach (var script in scripts)
                {
                    WriteVerbose($"Invoking site script '{script.Title}' ({script.Id})");

                    //var results = tenant.ApplySiteDesign(webUrl, design.Id);
                    //tenantContext.Load(results);
                    //tenantContext.ExecuteQueryRetry();
                    //WriteObject(results, true);
                }
            }
        }
    }
}