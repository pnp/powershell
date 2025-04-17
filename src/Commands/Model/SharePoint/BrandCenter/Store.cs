using System;

namespace PnP.PowerShell.Commands.Model.SharePoint.BrandCenter
{
    /// <summary>
    /// Contains the types of stores where fonts can reside in the Brand Center
    /// </summary>
    public enum Store
    {
        /// <summary>
        /// Tenant Brand Center
        /// </summary>
        Tenant = 0,

        /// <summary>
        /// Provided by Microsoft
        /// </summary>
        OutOfBox = 1,

        /// <summary>
        /// Site collection Brand Center
        /// </summary>
        Site = 2,

        /// <summary>
        /// Indicates that any font should be retrieved, regardless of the store
        /// </summary>
        All = 99
    }
}