using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPStructuralNavigationCacheWebState")]
    [OutputType(typeof(bool))]
    public class GetStructuralNavigationCacheWebState : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string WebUrl;

        protected override void ExecuteCmdlet()
        {
            var url = Connection.Url;
            if (ParameterSpecified(nameof(WebUrl)))
            {
                url = WebUrl;
            }
            var state = this.Tenant.GetSPOStructuralNavigationCacheWebState(url);
            AdminContext.ExecuteQueryRetry();
            WriteObject(state);
        }
    }
}
