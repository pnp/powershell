using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.SiteDesigns
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteDesignRun")]
    public class GetSiteDesignRun : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public Guid SiteDesignId;

        [Parameter(Mandatory = false)]
        public string WebUrl;

        protected override void ExecuteCmdlet()
        {
            var url = CurrentWeb.EnsureProperty(w => w.Url);
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

                var designRun = tenant.GetSiteDesignRun(webUrl, SiteDesignId);
                tenantContext.Load(designRun);
                tenantContext.ExecuteQueryRetry();
                WriteObject(designRun, true);
            }
        }
    }
}