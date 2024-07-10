using System;

namespace PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement
{
    /// <summary>
    /// A role definition in Entra ID Priviledged Identity Management
    /// </summary>
    public class RoleDefinition
    {
        /// <summary>
        /// Id of the role definition
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Name of the role
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description of the role
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Is the role enabled
        /// </summary>
        public bool? IsEnabled { get; set; }

        /// <summary>
        /// Is the role built in
        /// </summary>
        public bool? IsBuiltIn { get; set; }
    }
}