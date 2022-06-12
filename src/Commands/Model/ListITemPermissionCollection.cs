using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Model to contain a collection of list item permissions
    /// </summary>
    public class ListItemPermissionCollection
    {
        /// <summary>
        /// Boolean indicating if the list item has unique item level permissions (true) or if it inherits from its parent (false)
        /// </summary>
        public bool? HasUniqueRoleAssignments { get; set; }

        /// <summary>
        /// Permissions that are set for the list item
        /// </summary>
        public List<ListItemPermission> Permissions { get; set; }
    }
}