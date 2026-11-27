using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.Graph.FileStorage
{
    /// <summary>
    /// The permissions an application has on the containers of a SharePoint Embedded container type registered in the tenant, as returned by Microsoft Graph
    /// See <a href="https://learn.microsoft.com/graph/api/resources/filestoragecontainertypeapppermissiongrant">Graph Reference</a>
    /// </summary>
    public class FileStorageContainerTypeAppPermissionGrant
    {
        /// <summary>
        /// Id of the application the permissions are granted to
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Permissions of the application when it calls without a user
        /// </summary>
        public List<string> ApplicationPermissions { get; set; }

        /// <summary>
        /// Permissions of the application when it calls on behalf of a user
        /// </summary>
        public List<string> DelegatedPermissions { get; set; }
    }
}
