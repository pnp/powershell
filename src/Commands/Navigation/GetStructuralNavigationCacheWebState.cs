using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPStructuralNavigationCacheWebState")]
    public class GetStructuralNavigationCacheWebState : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string WebUrl;

        protected override void ExecuteCmdlet()
        {
            var url = PnPConnection.CurrentConnection.Url;
            if (ParameterSpecified(nameof(WebUrl)))
            {
                url = WebUrl;
            }
            var state = this.Tenant.GetSPOStructuralNavigationCacheWebState(url);
            ClientContext.ExecuteQueryRetry();
            WriteObject(state);
        }
    }
}
