using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Utility class to work with Entra ID Priviledged Identity Management
    /// </summary>
    internal static class PriviledgedIdentityManagamentUtility
    {
        /// <summary>
        /// Returns all available priviledged identity management role schedules
        /// </summary>
        public static List<RoleEligibilitySchedule> GetRoleEligibilitySchedules(GraphHelper requestHelper)
        {
            string requestUrl = $"v1.0/roleManagement/directory/roleEligibilitySchedules?$expand=RoleDefinition";
            var result = requestHelper.GetResultCollection<RoleEligibilitySchedule>(requestUrl);
            return result.ToList();
        }

        /// <summary>
        /// Returns all available priviledged identity management roles
        /// </summary>
        public static List<RoleDefinition> GetRoleDefinitions(GraphHelper requestHelper)
        {
            string requestUrl = $"v1.0/roleManagement/directory/roleDefinitions";
            var result = requestHelper.GetResultCollection<RoleDefinition>(requestUrl);
            return result.ToList();
        }

        /// <summary>
        /// Returns a priviledged identity management role by its displayname
        /// </summary>
        /// <param name="roleName">Displayname of the role to return. Case sensitive.</param>
        public static RoleDefinition GetRoleDefinitionByName(GraphHelper requestHelper, string roleName)
        {
            string requestUrl = $"v1.0/roleManagement/directory/roleDefinitions?$filter=displayName eq '{roleName}'";
            var result = requestHelper.GetResultCollection<RoleDefinition>(requestUrl);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns a priviledged identity management role by its Id
        /// </summary>
        /// <param name="roleId">Id of the role to return</param>
        public static RoleDefinition GetRoleDefinitionById(GraphHelper requestHelper, Guid roleId)
        {
            string requestUrl = $"v1.0/roleManagement/directory/roleDefinitions/{roleId}";
            var result = requestHelper.Get<RoleDefinition>(requestUrl);
            return result;
        }

        /// <summary>
        /// Returns the priviledged identity management role schedule with the provided Id
        /// </summary>
        public static RoleEligibilitySchedule GetRoleEligibilityScheduleById(GraphHelper requestHelper, Guid id)
        {
            string requestUrl = $"v1.0/roleManagement/directory/roleEligibilitySchedules/{id}?$expand=RoleDefinition";
            var result = requestHelper.Get<RoleEligibilitySchedule>(requestUrl);
            return result;
        }

        /// <summary>
        /// Returns the priviledged identity management role schedule for the provided principal and role
        /// </summary>
        public static RoleEligibilitySchedule GetRoleEligibilityScheduleByPrincipalIdAndRoleName(GraphHelper requestHelper, Guid principalId, RoleDefinition role)
        {
            string requestUrl = $"v1.0/roleManagement/directory/roleEligibilitySchedules?$filter=principalId eq '{principalId}' and roleDefinitionId eq '{role.Id}'&$expand=RoleDefinition";
            var result = requestHelper.GetResultCollection<RoleEligibilitySchedule>(requestUrl);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Creates a scheduled assignment for a role to be activated at a certain time
        /// </summary>
        public static HttpResponseMessage CreateRoleAssignmentScheduleRequest(GraphHelper requestHelper, RoleEligibilitySchedule roleEligibilitySchedule,string justification = null, DateTime? startDateTime = null, short? expiratonHours = null)
        {
            string requestUrl = $"v1.0/roleManagement/directory/roleAssignmentScheduleRequests";
            var postData = new RoleAssignmentScheduleRequest
            {
                DirectoryScopeId = roleEligibilitySchedule.DirectoryScopeId,
                PrincipalId = roleEligibilitySchedule.PrincipalId,
                RoleDefinitionId = roleEligibilitySchedule.RoleDefinition.Id,
                Justification = justification ?? "Elevated by PnP PowerShell",
                ScheduleInfo = new ScheduleInfo
                {
                    StartDateTime = startDateTime ?? DateTime.UtcNow,
                    Expiration = new Expiration
                    {
                        Duration = $"PT{expiratonHours.GetValueOrDefault(1)}H"
                    }
                }
            };
            var stringContent = new StringContent(JsonSerializer.Serialize(postData), System.Text.Encoding.UTF8, "application/json");
            var result = requestHelper.PostHttpContent(requestUrl, stringContent);
            return result;
        }
    }
}