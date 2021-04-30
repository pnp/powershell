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

        protected override void ExecuteCmdlet()
        {
            Tenant.SetSPHSite(HomeSiteUrl);
            ClientContext.ExecuteQueryRetry();
        }
    }
}