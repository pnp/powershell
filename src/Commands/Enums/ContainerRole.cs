namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the roles a user can have on a SharePoint Embedded container
    /// See <a href="https://learn.microsoft.com/graph/api/resources/filestoragecontainer#roles-property-values">Graph Reference</a>
    /// </summary>
    public enum ContainerRole
    {
        /// <summary>
        /// Can read the metadata and contents of the container
        /// </summary>
        Reader,

        /// <summary>
        /// Can read and modify the metadata and contents of the container
        /// </summary>
        Writer,

        /// <summary>
        /// Can read and modify the metadata and contents of the container, and manage its permissions
        /// </summary>
        Manager,

        /// <summary>
        /// Can read and modify the metadata and contents of the container, manage its permissions, and delete and restore it
        /// </summary>
        Owner
    }
}
