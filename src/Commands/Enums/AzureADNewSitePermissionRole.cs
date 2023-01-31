namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the roles that can be chosen when setting up a new site permission
    /// See <a href="https://learn.microsoft.com/en-us/graph/api/resources/permission#roles-property-values">Graph Reference</a>
    /// </summary>
    public enum AzureADNewSitePermissionRole
    {
        /// <summary>
        /// Provides the ability to read the metadata and contents of the item
        /// </summary>
        Read,
        
        /// <summary>
        /// Provides the ability to read and modify the metadata and contents of the item
        /// </summary>
        Write
    }
}
