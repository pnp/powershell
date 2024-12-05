using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPHomeSite")]
    public class AddHomeSite : PnPAdminCmdlet
    {
        [Alias("Url")]
        [Parameter(Mandatory = true)]
        public string HomeSiteUrl;

        protected override void ExecuteCmdlet()
        {
            Tenant.AddHomeSite(HomeSiteUrl, 999999, new System.Guid[0]);
            AdminContext.ExecuteQueryRetry();
        }
    }
}
