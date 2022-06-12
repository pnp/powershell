using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Model to contain one list item level permission
    /// </summary>
    public class ListItemPermission
    {        
        /// <summary>
        /// Permission levels set for this list item
        /// </summary>
        public List<RoleDefinition> PermissionLevels { get; set; }

        /// <summary>
        /// Name of the principical that has been granted permissions
        /// </summary>
        public string PrincipalName { get; set; }

        /// <summary>
        /// Type of principal that has been granted permissions
        /// </summary>
        public PrincipalType? PrincipalType { get; set; }

        /// <summary>
        /// Id of the principal on the list item
        /// </summary>
        public int? PrincipalId { get; set; }
    }
}
