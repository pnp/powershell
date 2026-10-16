namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines the permissions an application can be granted on the containers of a SharePoint Embedded container type
    /// See <a href="https://learn.microsoft.com/sharepoint/dev/embedded/development/auth#container-type-application-permissions">SharePoint Embedded authorization</a>
    /// </summary>
    public enum ContainerApplicationPermission
    {
        /// <summary>
        /// No permissions
        /// </summary>
        None,

        /// <summary>
        /// Can read the content of containers
        /// </summary>
        ReadContent,

        /// <summary>
        /// Can write content to containers
        /// </summary>
        WriteContent,

        /// <summary>
        /// Can read, write and manage the content of containers
        /// </summary>
        ManageContent,

        /// <summary>
        /// Can create containers
        /// </summary>
        Create,

        /// <summary>
        /// Can delete containers
        /// </summary>
        Delete,

        /// <summary>
        /// Can read the properties of containers
        /// </summary>
        Read,

        /// <summary>
        /// Can update the properties of containers
        /// </summary>
        Write,

        /// <summary>
        /// Can list the members of containers and their roles
        /// </summary>
        EnumeratePermissions,

        /// <summary>
        /// Can add members to containers
        /// </summary>
        AddPermissions,

        /// <summary>
        /// Can change the roles of the members of containers
        /// </summary>
        UpdatePermissions,

        /// <summary>
        /// Can remove members from containers
        /// </summary>
        DeletePermissions,

        /// <summary>
        /// Can remove its own membership from containers
        /// </summary>
        DeleteOwnPermission,

        /// <summary>
        /// Can add, change and remove the members of containers
        /// </summary>
        ManagePermissions,

        /// <summary>
        /// All permissions
        /// </summary>
        Full
    }
}
