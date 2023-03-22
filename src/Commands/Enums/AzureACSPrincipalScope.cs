namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Possible scopes to retrieve ACS Principals of
    /// </summary>
    public enum AzureACSPrincipalScope
    {
        /// <summary>
        /// Retrieves the list scoped Azure ACS principal
        /// </summary>
        List,
        /// <summary>
        /// Retrieves the web scoped Azure ACS principal
        /// </summary>
        Web,
        /// <summary>
        /// Retrieves the site collection scoped Azure ACS principal
        /// </summary>
        Site,
        /// <summary>
        /// Retrieves the Tenant scoped Azure ACS principal
        /// </summary>
        Tenant,
        /// <summary>
        /// Retrieves the all Azure ACS principals
        /// </summary>
        All
    }
}
