namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines to what type of site collection in SharePoint Online a connection has been made
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>
        /// Normal SharePoint Online site collection
        /// </summary>
        O365 = 1,

        /// <summary>
        /// SharePoint Online Admin site collection
        /// </summary>
        TenantAdmin = 2,
    }
}
