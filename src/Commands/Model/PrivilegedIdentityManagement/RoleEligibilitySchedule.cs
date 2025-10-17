using System;

namespace PnP.PowerShell.Commands.Model.PrivilegedIdentityManagement
{
    /// <summary>
    /// An eligible role in Entra ID Privileged Identity Management
    /// </summary>
    public class RoleEligibilitySchedule
    {
        /// <summary>
        /// Id of the eligible role
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Id of the principal the role can be assigned to
        /// </summary>
        public Guid? PrincipalId { get; set; }

        /// <summary>
        /// Definition of the role that is eligible
        /// </summary>
        public RoleDefinition RoleDefinition { get; set; }

        /// <summary>
        /// The scope at which this role will be applied. For example, the role can be applied to a specific directory object such as a user or group, or to the entire directory.
        /// </summary>
        public string DirectoryScopeId { get; set; }
    }
}