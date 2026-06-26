namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the roles that can be chosen when updating an existing list, list item, or file permission
    /// See <a href="https://learn.microsoft.com/graph/api/resources/permission#roles-property-values">Graph Reference</a>
    /// </summary>
    public enum AzureADUpdateListPermissionRole
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
        /// Provides owner-level access to the item
        /// </summary>
        Owner,

        /// <summary>
        /// Provides full control of the resource
        /// </summary>
        FullControl
    }
}
