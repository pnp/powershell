namespace PnP.PowerShell.Commands.Model
{
    public class TenantInstance
    {
        public string DataLocation { get; set; }
        public bool IsDefaultDataLocation { get; set; }
        public string MySiteHostUrl { get; set; }
        public string PortalUrl { get; set; }
        public string RootSiteUrl { get; set; }
        public string TenantAdminUrl { get; set; }

        internal static TenantInstance Convert(Microsoft.Online.SharePoint.TenantAdministration.SPOTenantInstance sPOTenantInstance)
        {
            return new TenantInstance
            {
                DataLocation = sPOTenantInstance.DataLocation,
                IsDefaultDataLocation = sPOTenantInstance.IsDefaultDataLocation,
                MySiteHostUrl = sPOTenantInstance.MySiteHostUrl,
                PortalUrl = sPOTenantInstance.PortalUrl,
                RootSiteUrl = sPOTenantInstance.RootSiteUrl,
                TenantAdminUrl = sPOTenantInstance.TenantAdminUrl
            };
        }
    }
}