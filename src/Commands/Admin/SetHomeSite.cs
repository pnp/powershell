using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPHomeSite")]
    public class SetHomeSite : PnPAdminCmdlet
    {
        [Alias("Url")]
        [Parameter(Mandatory = true)]
        public string HomeSiteUrl;

        [Parameter(Mandatory = false)]
        public bool VivaConnectionsDefaultStart;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(VivaConnectionsDefaultStart)))
            {
                Tenant.SetSPHSiteWithConfigurations(HomeSiteUrl, VivaConnectionsDefaultStart);
            }
            else
            {
                Tenant.SetSPHSite(HomeSiteUrl);
            }
            ClientContext.ExecuteQueryRetry();
        }
    }
}