namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Available scopes for disabling SharePoint Spaces functionality
    /// </summary>
    public enum DisableSpacesScope
    {
        /// <summary>
        /// Disable on the entire tenant
        /// </summary>
        Tenant = 0,

        /// <summary>
        /// Disable on a specific site
        /// </summary>
        Site = 1
    }
}
