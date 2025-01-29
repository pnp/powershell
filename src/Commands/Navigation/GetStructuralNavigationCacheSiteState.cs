using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPStructuralNavigationCacheSiteState")]
    [OutputType(typeof(bool))]
    public class GetStructuralNavigationCacheSiteState : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string SiteUrl;

        protected override void ExecuteCmdlet()
        {
            var url = Connection.Url;
            if (ParameterSpecified(nameof(SiteUrl)))
            {
                url = SiteUrl;
            }
            var state = this.Tenant.GetSPOStructuralNavigationCacheSiteState(url);
            AdminContext.ExecuteQueryRetry();
            WriteObject(state);
        }
    }
}
