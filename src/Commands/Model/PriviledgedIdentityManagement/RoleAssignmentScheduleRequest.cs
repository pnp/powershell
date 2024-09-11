using System;

namespace PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement
{
    /// <summary>
    /// Requests enabling a role assignment
    /// </summary>
    public class RoleAssignmentScheduleRequest
    {
        /// <summary>
        /// Type of activation to apply
        /// </summary>
        public string Action { get; set; } = "selfActivate";
        
        /// <summary>
        /// Id of the principal to enable the role on
        /// </summary>
        public Guid? PrincipalId { get; set; }

        /// <summary>
        /// Id of the role definition to enable
        /// </summary>
        public Guid? RoleDefinitionId { get; set; }

        /// <summary>
        /// The scope at which the role will be applied
        /// </summary>
        public string DirectoryScopeId { get; set; }

        /// <summary>
        /// Justification for enabling the role assignment
        /// </summary>
        public string Justification { get; set; }

        /// <summary>
        /// Details on when the role assignment should start and end
        /// </summary>
        public ScheduleInfo ScheduleInfo { get; set; }
    }
}
