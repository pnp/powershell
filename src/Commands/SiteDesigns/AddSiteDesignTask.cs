using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.SiteDesigns
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteDesignTask")]
    [OutputType(typeof(TenantSiteDesignTask))]
    public class AddSiteDesignTask : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public Guid SiteDesignId;

        [Parameter(Mandatory = false)]
        public string WebUrl;

        protected override void ExecuteCmdlet()
        {
            var url = CurrentWeb.EnsureProperty(w => w.Url);
            var tenantUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
            using (var tenantContext = ClientContext.Clone(tenantUrl))
            {
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
                try
                {
                    var designTask = Tenant.AddSiteDesignTask(tenantContext, webUrl, SiteDesignId);
                    tenantContext.Load(designTask);
                    tenantContext.ExecuteQueryRetry();
                    WriteObject(designTask);
                }
                catch (System.Net.WebException ex)
                {
                    if (ex.Status == System.Net.WebExceptionStatus.ProtocolError)
                    {
                        var webResponse = ex.Response as System.Net.HttpWebResponse;
                        if (null != webResponse && 
                            webResponse.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            WriteObject("The server threw an exception (see below). It seems you may not have access to the server or you are executing this script outside of the tenant admin URL eg. yourtenant-admin.sharepoint.com. Please connect to the tenant admin URL first and try again.");
                        }
                    }
                    WriteObject( ex );
                }
            }
        }
    }
}