namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the roles that can be chosen when updating an existing site permission
    /// See <a href="https://learn.microsoft.com/en-us/graph/api/resources/permission#roles-property-values">Graph Reference</a>
    /// </summary>
    public enum AzureADUpdateSitePermissionRole
    {        
        /// <summary>
        /// Provides the ability to read the metadata and contents of the item
        /// </summary>
        Read,
        
        /// <summary>
        /// Provides the ability to read and modify the metadata and contents of the item
        /// </summary>
        Write,

        /// <summary>
        /// Applies the SharePoint manage permissions
        /// </summary>
        Manage,

        /// <summary>
        /// Applies Full Control permissions
        /// </summary>
        FullControl
    }
}
