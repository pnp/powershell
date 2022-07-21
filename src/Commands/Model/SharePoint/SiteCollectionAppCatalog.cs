using System;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// All properties regarding a site collection scoped App Catalog
    /// </summary>
    public class SiteCollectionAppCatalog
    {
        /// <summary>
        /// The full Url to the location of the App Catalog in the tenant
        /// </summary>
        public string AbsoluteUrl { get; set; }

        /// <summary>
        /// Informational message regarding the provisioning of the App Catalog
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Unique identifier of the site on which this App Catalog is located
        /// </summary>
        public Guid? SiteID { get; set; }

    }
}