using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Set, "PnPStructuralNavigationCacheSiteState")]
    [OutputType(typeof(void))]
    public class SetStructuralNavigationCacheSiteState : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string SiteUrl;

        [Parameter(Mandatory = true)]
        public bool IsEnabled;

        protected override void ExecuteCmdlet()
        {
            var url = Connection.Url;
            if (ParameterSpecified(nameof(SiteUrl)))
            {
                url = SiteUrl;
            }
            this.Tenant.SetSPOStructuralNavigationCacheSiteState(url,IsEnabled);
            AdminContext.ExecuteQueryRetry();
        }
    }
}
