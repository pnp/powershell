using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection.Metadata;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Get, "PnPPage", DefaultParameterSetName = ParameterSet_ALL)]
    [Alias("Get-PnPClientSidePage")]
    [OutputType(typeof(Core.Model.SharePoint.IPage), ParameterSetName = new[] { ParameterSet_SPECIFICPAGE })]
    [OutputType(typeof(IEnumerable<SPSitePage>), ParameterSetName = new[] { ParameterSet_ALL })]
    public class GetPage : PnPWebCmdlet
    {
        private const string ParameterSet_SPECIFICPAGE = "Specific page";
        private const string ParameterSet_ALL = "All pages";

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_SPECIFICPAGE)]
        [ArgumentCompleter(typeof(PageCompleter))]
        public PagePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (!ParameterSpecified(nameof(Identity)))
            {
                var tenantUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);

                using var tenantContext = ClientContext.Clone(tenantUrl);
                var tenant = new Tenant(tenantContext);

                CurrentWeb.EnsureProperty(w => w.Url);
                var pages = tenant.GetSPSitePages(CurrentWeb.Url);
                tenantContext.ExecuteQueryRetry();

                WriteObject(pages, true);
            }
            else
            {
                var clientSidePage = Identity.GetPage(Connection);

                if (clientSidePage == null)
                    throw new Exception($"Page '{Identity?.Name}' does not exist");

                WriteObject(clientSidePage);
            }
        }
    }
}