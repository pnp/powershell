using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.RecycleBin
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantRecycleBinItem", DefaultParameterSetName = "All")]
    [OutputType(typeof(DeletedSiteProperties))]
    public class GetTenantRecycleBinItems : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var deletedSites = Tenant.GetDeletedSitePropertiesFromSharePoint("0");
            AdminContext.Load(deletedSites, c => c.IncludeWithDefaultProperties(s => s.Url, s => s.SiteId, s => s.DaysRemaining, s => s.Status));
            AdminContext.ExecuteQueryRetry();
            if (deletedSites.AreItemsAvailable)
            {
                WriteObject(deletedSites, true);
            }
        }
    }
}