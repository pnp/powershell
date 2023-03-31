namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the different types of available information barrier modes as documented at https://learn.microsoft.com/sharepoint/information-barriers#information-barriers-modes-and-sharepoint-sites
    /// </summary>
    public enum InformationBarriersMode
    {
        /// <summary>
        /// When a SharePoint site is created for collaboration between incompatible segments moderated by the site owner, the site's IB mode should be set as Owner Moderated. This mode is currently supported only for sites that are not connected to Microsoft365 group. See https://learn.microsoft.com/sharepoint/information-barriers#owner-moderated-mode-scenario-preview for details on managing Owner Moderated site.
        /// </summary>
        OwnerModerated,

        /// <summary>
        /// When a SharePoint site does not have segments, the site's IB mode is automatically set as Open. See https://learn.microsoft.com/sharepoint/information-barriers#view-and-manage-segments-as-an-administrator for details on managing segments with the Open mode configuration.
        /// </summary>
        Open,

        /// <summary>
        /// When a site is provisioned by Microsoft Teams, the site's IB mode is set as Implicit by default. A SharePoint admin or global admin cannot manage segments with the Implicit mode configuration.
        /// </summary>
        Implicit,
        
        /// <summary>
        /// When segment is added to a SharePoint site either via end-user site creation experience or by a SharePoint admin adding segment to a site, the site's IB mode is set as Explicit. See https://learn.microsoft.com/sharepoint/information-barriers#view-and-manage-segments-as-an-administrator for details on managing segments with the Explicit mode configuration.
        /// </summary>
        Explicit
    }
}
