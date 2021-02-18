using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Set, "PnPStructuralNavigationCacheWebState")]
    public class SetStructuralNavigationCacheWebState : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string WebUrl;

        [Parameter(Mandatory = true)]
        public bool IsEnabled;

        protected override void ExecuteCmdlet()
        {
            var url = PnPConnection.Current.Url;
            if (ParameterSpecified(nameof(WebUrl)))
            {
                url = WebUrl;
            }
            this.Tenant.SetSPOStructuralNavigationCacheWebState(url,IsEnabled);
            ClientContext.ExecuteQueryRetry();
        }
    }
}
