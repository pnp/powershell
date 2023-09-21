using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class PlannerUtility
    {
        #region Plans
        public static async Task<IEnumerable<PlannerPlan>> GetPlansAsync(PnPConnection connection, string accessToken, string groupId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerPlan>();
            var collection = await GraphHelper.GetResultCollectionAsync<PlannerPlan>(connection, $"v1.0/groups/{groupId}/planner/plans", accessToken);
            if (collection != null && collection.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var plan in collection)
                    {
                        var fullIdentity = await ResolveIdentityAsync(connection, accessToken, plan.CreatedBy.User);
                        plan.CreatedBy.User = fullIdentity;
                        var owner = await ResolveGroupName(connection, accessToken, plan.Owner);
                        plan.Owner = owner;
                        returnCollection.Add(plan);
                    }
                }
                else
                {
                    returnCollection = collection.ToList();
                }
            }
            return returnCollection;
        }

        public static async Task<PlannerPlan> GetPlanAsync(PnPConnection connection, string accessToken, string planId, bool resolveDisplayNames)
        {
            var plan = await GraphHelper.GetAsync<PlannerPlan>(connection, $"v1.0/planner/plans/{planId}", accessToken);
            if (resolveDisplayNames)
            {
                plan.CreatedBy.User = await ResolveIdentityAsync(connection, accessToken, plan.CreatedBy.User);
            }
            return plan;
        }

        public static async Task<PlannerPlan> CreatePlanAsync(PnPConnection connection, string accessToken, string groupId, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { owner = groupId, title = title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<PlannerPlan>(connection, "v1.0/planner/plans", stringContent, accessToken);
        }

        public static async Task<PlannerPlan> UpdatePlanAsync(PnPConnection connection, string accessToken, PlannerPlan plan, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var responseMessage = await GraphHelper.PatchAsync(connection, accessToken, stringContent, $"v1.0/planner/plans/{plan.Id}", new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            while (responseMessage.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
            {
                // retrieve the plan again
                plan = await GraphHelper.GetAsync<PlannerPlan>(connection, $"v1.0/planner/plans/{plan.Id}", accessToken);
                responseMessage = await GraphHelper.PatchAsync(connection, accessToken, stringContent, $"v1.0/planner/plans/{plan.Id}", new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PlannerPlan>(responseContent);
            }
            return null;
        }

        public static async Task DeletePlanAsync(PnPConnection connection, string accessToken, string planId)
        {
            var plan = await GetPlanAsync(connection, accessToken, planId, false);
            if (plan != null)
            {
                await GraphHelper.DeleteAsync(connection, $"v1.0/planner/plans/{planId}", accessToken, new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            }
        }

        #endregion

        #region Tasks

        public static async Task<IEnumerable<PlannerTask>> GetTasksAsync(PnPConnection connection, string accessToken, string planId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerTask>();
            var collection = await GraphHelper.GetResultCollectionAsync<PlannerTask>(connection, $"v1.0/planner/plans/{planId}/tasks", accessToken);
            if (collection != null && collection.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var task in collection)
                    {
                        var fullIdentity = await ResolveIdentityAsync(connection, accessToken, task.CreatedBy.User);
                        task.CreatedBy.User = fullIdentity;
                        if (task.Assignments != null)
                        {
                            foreach (var assignment in task.Assignments)
                            {
                                assignment.Value.AssignedBy.User = await ResolveIdentityAsync(connection, accessToken, assignment.Value.AssignedBy.User);
                            }
                        }
                        returnCollection.Add(task);
                    }
                }
                else
                {
                    returnCollection = collection.ToList();
                }                
            }
            return returnCollection;
        }

        public static async Task<PlannerTask> GetTaskAsync(PnPConnection connection, string accessToken, string taskId, bool resolveDisplayNames, bool includeDetails)
        {
            var task = await GraphHelper.GetAsync<PlannerTask>(connection, $"v1.0/planner/tasks/{taskId}", accessToken);
            if (resolveDisplayNames)
            {
                task.CreatedBy.User = await ResolveIdentityAsync(connection, accessToken, task.CreatedBy.User);
            }
            if (includeDetails)
            {
                var taskDetails = await GetTaskDetailsAsync(connection, accessToken, taskId, resolveDisplayNames);
                task.Details = taskDetails;
            }
            return task;
        }

        public static async Task<PlannerTaskDetails> GetTaskDetailsAsync(PnPConnection connection, string accessToken, string taskId, bool resolveDisplayNames)
        {
            var taskDetails = await GraphHelper.GetAsync<PlannerTaskDetails>(connection, $"v1.0/planner/tasks/{taskId}/details", accessToken);
            if (!resolveDisplayNames) 
                return taskDetails;

            var newItems = new Dictionary<string, PlannerTaskCheckListItem>();
            foreach (var checklistItem in taskDetails.Checklist)
            {
                var newCheckListItem = new PlannerTaskCheckListItem
                {
                    IsChecked = checklistItem.Value.IsChecked,
                    LastModifiedDateTime = checklistItem.Value.LastModifiedDateTime,
                    OrderHint = checklistItem.Value.OrderHint,
                    Title = checklistItem.Value.Title
                };
                if (checklistItem.Value.LastModifiedBy != null)
                {
                    newCheckListItem.LastModifiedBy = new IdentitySet
                    {
                        User = await ResolveIdentityAsync(connection, accessToken, checklistItem.Value.LastModifiedBy.User)
                    };
                }
                newItems.Add(checklistItem.Key, newCheckListItem);
            }
            taskDetails.Checklist = newItems;

            return taskDetails;
        }

        public static async Task<PlannerTask> AddTaskAsync(PnPConnection connection, string accessToken, PlannerTask task)
        {
            return await GraphHelper.PostAsync(connection, "v1.0/planner/tasks", task, accessToken);
        }

        public static async Task DeleteTaskAsync(PnPConnection connection, string accessToken, string taskId)
        {
            var task = await GraphHelper.GetAsync<PlannerTask>(connection, $"v1.0/planner/tasks/{taskId}", accessToken);
            if (task != null)
            {
                await GraphHelper.DeleteAsync(connection, $"v1.0/planner/tasks/{taskId}", accessToken, new Dictionary<string, string>() { { "IF-MATCH", task.ETag } });
            }
        }

        public static async Task<PlannerTask> UpdateTaskAsync(PnPConnection connection, string accessToken, PlannerTask taskToUpdate, PlannerTask task)
        {
            return await GraphHelper.PatchAsync(connection, accessToken, $"v1.0/planner/tasks/{taskToUpdate.Id}", task, new Dictionary<string, string> { { "IF-MATCH", taskToUpdate.ETag } });
        }

        public static async Task UpdateTaskDetailsAsync(PnPConnection connection, string accessToken, PlannerTaskDetails taskToUpdate, string description)
        {
            var body = new PlannerTaskDetails
            {
                Description = description,
            };
            await GraphHelper.PatchAsync(connection, accessToken, $"v1.0/planner/tasks/{taskToUpdate.Id}/details", body, new Dictionary<string, string> { { "IF-MATCH", taskToUpdate.ETag } });
        }

        #endregion

        #region Rosters

        /// <summary>
        /// Creates a new Planner Roster
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static async Task<PlannerRoster> CreateRosterAsync(PnPConnection connection, string accessToken)
        {
            var stringContent = new StringContent("{ \"@odata.type\": \"#microsoft.graph.plannerRoster\" }");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<PlannerRoster>(connection, "beta/planner/rosters", stringContent, accessToken);
        }

        /// <summary>
        /// Gets a Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static async Task<PlannerRoster> GetRosterAsync(PnPConnection connection, string accessToken, string rosterId)
        {
            return await GraphHelper.GetAsync<PlannerRoster>(connection, $"beta/planner/rosters/{rosterId}", accessToken);
        }        

        /// <summary>
        /// Deletes a Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> DeleteRosterAsync(PnPConnection connection, string accessToken, string rosterId)
        {
            return await GraphHelper.DeleteAsync(connection, $"beta/planner/rosters/{rosterId}", accessToken);
        }

        /// <summary>
        /// Adds a member to an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to add the member to</param>
        /// <param name="userId">Identifier of the user to add as a member</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static async Task<PlannerRoster> AddRosterMemberAsync(PnPConnection connection, string accessToken, string rosterId, string userId)
        {
            var stringContent = new StringContent("{ \"@odata.type\": \"#microsoft.graph.plannerRosterMember\", \"userId\": \"" + userId + "\" }");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<PlannerRoster>(connection, $"beta/planner/rosters/{rosterId}/members", stringContent, accessToken);
        }

        /// <summary>
        /// Removes a member from an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to remove the member from</param>
        /// <param name="userId">Identifier of the user to remove as a member</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> RemoveRosterMemberAsync(PnPConnection connection, string accessToken, string rosterId, string userId)
        {
            return await GraphHelper.DeleteAsync(connection, $"beta/planner/rosters/{rosterId}/members/{userId}", accessToken);
        } 

        /// <summary>
        /// Returns all current members of an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to retrieve the members of</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>IEnumerable<PlannerRosterMember></returns>
        public static async Task<IEnumerable<PlannerRosterMember>> GetRosterMembersAsync(PnPConnection connection, string accessToken, string rosterId)
        {
            var returnCollection = new List<PlannerRosterMember>();
            var collection = await GraphHelper.GetResultCollectionAsync<PlannerRosterMember>(connection, $"beta/planner/rosters/{rosterId}/members", accessToken);
            if (collection != null && collection.Any())
            {
                returnCollection = collection.ToList();
            }
            return returnCollection;
        }

        /// <summary>
        /// Gets the Planner Rosters for a specific user
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static async Task<PlannerRoster> GetRosterPlansByUserAsync(PnPConnection connection, string accessToken, string userId)
        {
            return await GraphHelper.GetAsync<PlannerRoster>(connection, $"beta/users/{userId}/planner/rosterPlans", accessToken);
        }

        /// <summary>
        /// Gets the Planner Plans in a specific Planner Roster
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static async Task<PlannerRoster> GetRosterPlansByRosterAsync(PnPConnection connection, string accessToken, string rosterId)
        {
            return await GraphHelper.GetAsync<PlannerRoster>(connection, $"beta/planner/rosters/{rosterId}/plans", accessToken);
        }         

        #endregion

        #region Admin Tasks

        /// <summary>
        /// Retrieves the Planner tenant configuration
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerTenantConfig</returns>
        public static async Task<PlannerTenantConfig> GetPlannerConfigAsync(PnPConnection connection, string accessToken)
        {
            var result = await GraphHelper.GetAsync<PlannerTenantConfig>(connection, "https://tasks.office.com/taskAPI/tenantAdminSettings/Settings", accessToken);
            return result;
        }

        /// <summary>
        /// Sets the Planner tenant configuration
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerTenantConfig</returns>
        public static async Task<PlannerTenantConfig> SetPlannerConfigAsync(PnPConnection connection, string accessToken, bool? isPlannerAllowed, bool? allowCalendarSharing, bool? allowTenantMoveWithDataLoss, bool? allowTenantMoveWithDataMigration, bool? allowRosterCreation, bool? allowPlannerMobilePushNotifications)
        {
            var content = new PlannerTenantConfig
            {
                IsPlannerAllowed = isPlannerAllowed,
                AllowCalendarSharing = allowCalendarSharing,
                AllowTenantMoveWithDataLoss = allowTenantMoveWithDataLoss,
                AllowTenantMoveWithDataMigration = allowTenantMoveWithDataMigration,
                AllowRosterCreation = allowRosterCreation,
                AllowPlannerMobilePushNotifications = allowPlannerMobilePushNotifications
            };
            var result = await GraphHelper.PatchAsync(connection, accessToken, "https://tasks.office.com/taskAPI/tenantAdminSettings/Settings", content);
            return result;
        }

        /// <summary>
        /// Retrieves the Planner User Policy for the provided user
        /// </summary>
        /// <param name="userId">Entra ID User Identifier or User Principal name</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerUserPolicy</returns>
        public static async Task<PlannerUserPolicy> GetPlannerUserPolicyAsync(PnPConnection connection, string accessToken, string userId)
        {
            var result = await GraphHelper.GetAsync<PlannerUserPolicy>(connection, $"https://tasks.office.com/taskAPI/tenantAdminSettings/UserPolicy('{userId}')", accessToken);
            return result;
        }        

        /// <summary>
        /// Sets the Planner User Policy for the provided user
        /// </summary>
        /// <param name="userId">Entra ID User Identifier or User Principal name</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerUserPolicy</returns>
        public static async Task<PlannerUserPolicy> SetPlannerUserPolicyAsync(PnPConnection connection, string accessToken, string userId, bool? blockDeleteTasksNotCreatedBySelf)
        {
            var content = new PlannerUserPolicy
            {
                BlockDeleteTasksNotCreatedBySelf = blockDeleteTasksNotCreatedBySelf
            };
            var result = await GraphHelper.PutAsync<PlannerUserPolicy>(connection, $"https://tasks.office.com/taskAPI/tenantAdminSettings/UserPolicy('{userId}')", content, accessToken);
            return result;
        }

        #endregion

        private static async Task<Identity> ResolveIdentityAsync(PnPConnection connection, string accessToken, Identity identity)
        {
            if (identity == null)
            {
                return null;
            }
            if (identity.DisplayName == null)
            {
                return await GraphHelper.GetAsync<Identity>(connection, $"v1.0/users/{identity.Id}", accessToken);
            }
            else
            {
                return identity;
            }
        }

        private static async Task<string> ResolveGroupName(PnPConnection connection, string accessToken, string id)
        {
            var group = await GraphHelper.GetAsync<Group>(connection, $"v1.0/groups/{id}?$select=displayName", accessToken);
            if (group != null)
            {
                return group.DisplayName;
            }
            else
            {
                return null;
            }
        }

        #region Buckets

        public static async Task<IEnumerable<PlannerBucket>> GetBucketsAsync(PnPConnection connection, string accessToken, string planId)
        {
            return await GraphHelper.GetResultCollectionAsync<PlannerBucket>(connection, $"v1.0/planner/plans/{planId}/buckets", accessToken); 
        }

        public static async Task<PlannerBucket> CreateBucketAsync(PnPConnection connection, string accessToken, string name, string planId)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name, planId = planId, orderHint = " !" }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<PlannerBucket>(connection, $"v1.0/planner/buckets", stringContent, accessToken);
        }

        public static async System.Threading.Tasks.Task RemoveBucketAsync(PnPConnection connection, string accessToken, string bucketId)
        {
            var bucket = GraphHelper.GetAsync<PlannerBucket>(connection, $"v1.0/planner/buckets/{bucketId}", accessToken).GetAwaiter().GetResult();
            if (bucket != null)
            {
                await GraphHelper.DeleteAsync(connection, $"v1.0/planner/buckets/{bucketId}", accessToken, new Dictionary<string, string>() { { "IF-MATCH", bucket.ETag } });
            }
        }


        public static async Task<IEnumerable<PlannerTask>> GetBucketTasksAsync(PnPConnection connection, string accessToken, string bucketId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerTask>();
            var collection = await GraphHelper.GetAsync<RestResultCollection<PlannerTask>>(connection, $"v1.0/planner/buckets/{bucketId}/tasks", accessToken);
            if (collection != null && collection.Items.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var task in collection.Items)
                    {
                        var fullIdentity = await ResolveIdentityAsync(connection, accessToken, task.CreatedBy.User);
                        task.CreatedBy.User = fullIdentity;
                        if (task.Assignments != null)
                        {
                            foreach (var assignment in task.Assignments)
                            {
                                assignment.Value.AssignedBy.User = await ResolveIdentityAsync(connection, accessToken, assignment.Value.AssignedBy.User);
                            }
                        }
                        returnCollection.Add(task);
                    }

                }
                else
                {
                    returnCollection = collection.Items.ToList();
                }
                while (!string.IsNullOrEmpty(collection.NextLink))
                {
                    if (resolveDisplayNames)
                    {
                        foreach (var task in collection.Items)
                        {
                            var fullIdentity = await ResolveIdentityAsync(connection, accessToken, task.CreatedBy.User);
                            task.CreatedBy.User = fullIdentity;
                            if (task.Assignments != null)
                            {
                                foreach (var assignment in task.Assignments)
                                {
                                    assignment.Value.AssignedBy.User = await ResolveIdentityAsync(connection, accessToken, assignment.Value.AssignedBy.User);
                                }
                            }
                            returnCollection.Add(task);
                        }

                    }
                    else
                    {
                        returnCollection.AddRange(collection.Items);
                    }
                }
            }
            return returnCollection;
        }

        public static async Task<PlannerBucket> UpdateBucketAsync(PnPConnection connection, string accessToken, string name, string bucketId)
        {
            var bucket = await GraphHelper.GetAsync<PlannerBucket>(connection, $"v1.0/planner/buckets/{bucketId}", accessToken);
            if (bucket != null)
            {
                var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name }));
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return await GraphHelper.PatchAsync<PlannerBucket>(connection, accessToken, $"v1.0/planner/buckets/{bucketId}", stringContent, new Dictionary<string, string>() { { "IF-MATCH", bucket.ETag } });
            }
            return null;
        }
        #endregion
    }
}
