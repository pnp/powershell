using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static List<RoleEligibilitySchedule> GetRoleEligibilitySchedules(PnPConnection connection, string accesstoken)
        {
            string requestUrl = $"roleManagement/directory/roleEligibilitySchedules?$expand=RoleDefinition";
            var result = REST.GraphHelper.GetResultCollectionAsync<RoleEligibilitySchedule>(connection, requestUrl, accesstoken).GetAwaiter().GetResult();
            return result.ToList();
        }

        /// <summary>
        /// Returns the priviledged identity management role schedule with the provided Id
        /// </summary>
        public static RoleEligibilitySchedule GetRoleEligibilityScheduleById(Guid id, PnPConnection connection, string accesstoken)
        {
            string requestUrl = $"roleManagement/directory/roleEligibilitySchedules/{id}?$expand=RoleDefinition";
            var result = REST.GraphHelper.GetAsync<RoleEligibilitySchedule>(connection, requestUrl, accesstoken).GetAwaiter().GetResult();
            return result;
        }

        /// <summary>
        /// Creates a scheduled assignment for a role to be activated at a certain time
        /// </summary>
        public static HttpResponseMessage CreateRoleAssignmentScheduleRequest(RoleEligibilitySchedule role, PnPConnection connection, string accesstoken, string justification = null, DateTime? startDateTime = null, short? expiratonHours = null)
        {
            string requestUrl = $"roleManagement/directory/roleAssignmentScheduleRequests";
            var postData = new RoleAssignmentScheduleRequest
            {
                DirectoryScopeId = role.DirectoryScopeId,
                PrincipalId = role.PrincipalId,
                RoleDefinitionId = role.RoleDefinition.Id,
                Justification = justification ?? "Elevated by PnP PowerShell",
                ScheduleInfo = new ScheduleInfo
                {
                    StartDateTime = startDateTime ?? DateTime.UtcNow,
                    Expiration = new Expiration
                    {
                        Duration = $"PT{expiratonHours.GetValueOrDefault(8)}H"
                    }
                }
            };
            var stringContent = new StringContent(JsonSerializer.Serialize(postData), System.Text.Encoding.UTF8, "application/json");
            var result = REST.GraphHelper.PostAsync(connection, requestUrl, accesstoken, stringContent).GetAwaiter().GetResult();
            return result;
        }
    }
}