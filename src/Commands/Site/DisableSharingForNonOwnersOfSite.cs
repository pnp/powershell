using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsLifecycle.Disable, "PnPSharingForNonOwnersOfSite")]
    [OutputType(typeof(void))]
    public class DisableSharingForNonOwnersOfSite : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [Alias("Url")]
        public SitePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var context = ClientContext;
            var site = ClientContext.Site;
            var siteUrl = ClientContext.Url;

            if (ParameterSpecified(nameof(Identity)))
            {
                context = ClientContext.Clone(Identity.Url);
                site = context.Site;
                siteUrl = context.Url;
            }

            Office365Tenant office365Tenant = new Office365Tenant(context);
            context.Load(office365Tenant);
            office365Tenant.DisableSharingForNonOwnersOfSite(siteUrl);
            context.ExecuteQueryRetry();
        }
    }
}