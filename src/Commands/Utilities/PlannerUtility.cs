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
        public static async Task<IEnumerable<PlannerPlan>> GetPlansAsync(HttpClient httpClient, string accessToken, string groupId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerPlan>();
            var collection = await GraphHelper.GetResultCollectionAsync<PlannerPlan>(httpClient, $"v1.0/groups/{groupId}/planner/plans", accessToken);
            if (collection != null && collection.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var plan in collection)
                    {
                        var fullIdentity = await ResolveIdentityAsync(httpClient, accessToken, plan.CreatedBy.User);
                        plan.CreatedBy.User = fullIdentity;
                        var owner = await ResolveGroupName(httpClient, accessToken, plan.Owner);
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

        public static async Task<PlannerPlan> GetPlanAsync(HttpClient httpClient, string accessToken, string planId, bool resolveDisplayNames)
        {
            var plan = await GraphHelper.GetAsync<PlannerPlan>(httpClient, $"v1.0/planner/plans/{planId}", accessToken);
            if (resolveDisplayNames)
            {
                plan.CreatedBy.User = await ResolveIdentityAsync(httpClient, accessToken, plan.CreatedBy.User);
            }
            return plan;
        }

        public static async Task<PlannerPlan> CreatePlanAsync(HttpClient httpClient, string accessToken, string groupId, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { owner = groupId, title = title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<PlannerPlan>(httpClient, "v1.0/planner/plans", stringContent, accessToken);
        }

        public static async Task<PlannerPlan> UpdatePlanAsync(HttpClient httpClient, string accessToken, PlannerPlan plan, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var responseMessage = await GraphHelper.PatchAsync(httpClient, accessToken, stringContent, $"v1.0/planner/plans/{plan.Id}", new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            while (responseMessage.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
            {
                // retrieve the plan again
                plan = await GraphHelper.GetAsync<PlannerPlan>(httpClient, $"v1.0/planner/plans/{plan.Id}", accessToken);
                responseMessage = await GraphHelper.PatchAsync(httpClient, accessToken, stringContent, $"v1.0/planner/plans/{plan.Id}", new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PlannerPlan>(responseContent);
            }
            return null;
        }

        public static async Task DeletePlanAsync(HttpClient httpClient, string accessToken, string planId)
        {
            var plan = await GetPlanAsync(httpClient, accessToken, planId, false);
            if (plan != null)
            {
                await GraphHelper.DeleteAsync(httpClient, $"v1.0/planner/plans/{planId}", accessToken, new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            }
        }

        #endregion

        #region Tasks

        public static async Task<IEnumerable<PlannerTask>> GetTasksAsync(HttpClient httpClient, string accessToken, string planId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerTask>();
            var collection = await GraphHelper.GetResultCollectionAsync<PlannerTask>(httpClient, $"v1.0/planner/plans/{planId}/tasks", accessToken);
            if (collection != null && collection.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var task in collection)
                    {
                        var fullIdentity = await ResolveIdentityAsync(httpClient, accessToken, task.CreatedBy.User);
                        task.CreatedBy.User = fullIdentity;
                        if (task.Assignments != null)
                        {
                            foreach (var assignment in task.Assignments)
                            {
                                assignment.Value.AssignedBy.User = await ResolveIdentityAsync(httpClient, accessToken, assignment.Value.AssignedBy.User);
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

        public static async Task<PlannerTask> GetTaskAsync(HttpClient httpClient, string accessToken, string taskId, bool resolveDisplayNames, bool includeDetails)
        {
            var task = await GraphHelper.GetAsync<PlannerTask>(httpClient, $"v1.0/planner/tasks/{taskId}", accessToken);
            if (resolveDisplayNames)
            {
                task.CreatedBy.User = await ResolveIdentityAsync(httpClient, accessToken, task.CreatedBy.User);
            }
            if (includeDetails)
            {
                var taskDetails = await GraphHelper.GetAsync<PlannerTaskDetails>(httpClient, $"v1.0/planner/tasks/{taskId}/details", accessToken);
                if (resolveDisplayNames)
                {
                    Dictionary<string, PlannerTaskCheckListItem> newItems = new Dictionary<string, PlannerTaskCheckListItem>();
                    foreach (var checklistItem in taskDetails.Checklist)
                    {
                        var newCheckListItem = new PlannerTaskCheckListItem();
                        newCheckListItem.IsChecked = checklistItem.Value.IsChecked;
                        newCheckListItem.LastModifiedDateTime = checklistItem.Value.LastModifiedDateTime;
                        newCheckListItem.OrderHint = checklistItem.Value.OrderHint;
                        newCheckListItem.Title = checklistItem.Value.Title;
                        if (checklistItem.Value.LastModifiedBy != null)
                        {
                            newCheckListItem.LastModifiedBy = new IdentitySet();
                            newCheckListItem.LastModifiedBy.User = await ResolveIdentityAsync(httpClient, accessToken, checklistItem.Value.LastModifiedBy.User);
                        }
                        newItems.Add(checklistItem.Key, newCheckListItem);
                    }
                    taskDetails.Checklist = newItems;
                }
                task.Details = taskDetails;
            }
            return task;
        }

        public static async Task<PlannerTask> AddTaskAsync(HttpClient httpClient, string accessToken, string planId, string bucketId, string title, string[] assignedTo = null)
        {
            StringContent stringContent = null;
            if (assignedTo != null)
            {
                var assignments = new Dictionary<string, object>();
                var chunks = BatchUtility.Chunk(assignedTo, 20);
                foreach (var chunk in chunks)
                {
                    var results = await BatchUtility.GetPropertyBatchedAsync(httpClient, accessToken, chunk.ToArray(), "/users/{0}", "id");
                    foreach (var userid in results.Select(r => r.Value))
                    {
                        assignments.Add(userid, new Model.Planner.PlannerAssignedToUser());
                    }
                }
                stringContent = new StringContent(JsonSerializer.Serialize(new { planId = planId, bucketId = bucketId, title = title, assignments = assignments }));
            }
            else
            {
                stringContent = new StringContent(JsonSerializer.Serialize(new { planId = planId, bucketId = bucketId, title = title }));
            }
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<PlannerTask>(httpClient, "v1.0/planner/tasks", stringContent, accessToken);
        }

        public static async Task DeleteTaskAsync(HttpClient httpClient, string accessToken, string taskId)
        {
            var task = await GraphHelper.GetAsync<PlannerTask>(httpClient, $"v1.0/planner/tasks/{taskId}", accessToken);
            if (task != null)
            {
                await GraphHelper.DeleteAsync(httpClient, $"v1.0/planner/tasks/{taskId}", accessToken, new Dictionary<string, string>() { { "IF-MATCH", task.ETag } });
            }
        }

        public static async Task<PlannerTask> UpdateTaskAsync(HttpClient httpClient, string accessToken, PlannerTask taskToUpdate, PlannerTask task)
        {
            
            return await GraphHelper.PatchAsync<PlannerTask>(httpClient, accessToken, $"v1.0/planner/tasks/{taskToUpdate.Id}", task, new Dictionary<string, string>() { { "IF-MATCH", taskToUpdate.ETag } });
        }
        #endregion

        #region Rosters

        /// <summary>
        /// Creates a new Planner Roster
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static async Task<PlannerRoster> CreateRosterAsync(HttpClient httpClient, string accessToken)
        {
            var stringContent = new StringContent("{ \"@odata.type\": \"#microsoft.graph.plannerRoster\" }");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<PlannerRoster>(httpClient, "beta/planner/rosters", stringContent, accessToken);
        }

        /// <summary>
        /// Gets a Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static async Task<PlannerRoster> GetRosterAsync(HttpClient httpClient, string accessToken, string rosterId)
        {
            return await GraphHelper.GetAsync<PlannerRoster>(httpClient, $"beta/planner/rosters/{rosterId}", accessToken);
        }        

        /// <summary>
        /// Deletes a Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> DeleteRosterAsync(HttpClient httpClient, string accessToken, string rosterId)
        {
            return await GraphHelper.DeleteAsync(httpClient, $"beta/planner/rosters/{rosterId}", accessToken);
        }

        /// <summary>
        /// Adds a member to an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to add the member to</param>
        /// <param name="userId">Identifier of the user to add as a member</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static async Task<PlannerRoster> AddRosterMemberAsync(HttpClient httpClient, string accessToken, string rosterId, string userId)
        {
            var stringContent = new StringContent("{ \"@odata.type\": \"#microsoft.graph.plannerRosterMember\", \"userId\": \"" + userId + "\" }");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<PlannerRoster>(httpClient, $"beta/planner/rosters/{rosterId}/members", stringContent, accessToken);
        }

        /// <summary>
        /// Removes a member from an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to remove the member from</param>
        /// <param name="userId">Identifier of the user to remove as a member</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> RemoveRosterMemberAsync(HttpClient httpClient, string accessToken, string rosterId, string userId)
        {
            return await GraphHelper.DeleteAsync(httpClient, $"beta/planner/rosters/{rosterId}/members/{userId}", accessToken);
        } 

        /// <summary>
        /// Returns all current members of an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to retrieve the members of</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>IEnumerable<PlannerRosterMember></returns>
        public static async Task<IEnumerable<PlannerRosterMember>> GetRosterMembersAsync(HttpClient httpClient, string accessToken, string rosterId)
        {
            var returnCollection = new List<PlannerRosterMember>();
            var collection = await GraphHelper.GetResultCollectionAsync<PlannerRosterMember>(httpClient, $"beta/planner/rosters/{rosterId}/members", accessToken);
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
        public static async Task<PlannerRoster> GetRosterPlansByUserAsync(HttpClient httpClient, string accessToken, string userId)
        {
            return await GraphHelper.GetAsync<PlannerRoster>(httpClient, $"beta/users/{userId}/planner/rosterPlans", accessToken);
        }

        /// <summary>
        /// Gets the Planner Plans in a specific Planner Roster
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static async Task<PlannerRoster> GetRosterPlansByRosterAsync(HttpClient httpClient, string accessToken, string rosterId)
        {
            return await GraphHelper.GetAsync<PlannerRoster>(httpClient, $"beta/planner/rosters/{rosterId}/plans", accessToken);
        }         

        #endregion

        #region Admin Tasks

        /// <summary>
        /// Retrieves the Planner tenant configuration
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerTenantConfig</returns>
        public static async Task<PlannerTenantConfig> GetPlannerConfigAsync(HttpClient httpClient, string accessToken)
        {
            var result = await GraphHelper.GetAsync<PlannerTenantConfig>(httpClient, "https://tasks.office.com/taskAPI/tenantAdminSettings/Settings", accessToken);
            return result;
        }

        /// <summary>
        /// Sets the Planner tenant configuration
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerTenantConfig</returns>
        public static async Task<PlannerTenantConfig> SetPlannerConfigAsync(HttpClient httpClient, string accessToken, bool? isPlannerAllowed, bool? allowCalendarSharing, bool? allowTenantMoveWithDataLoss, bool? allowRosterCreation, bool? allowPlannerMobilePushNotifications)
        {
            var content = new PlannerTenantConfig
            {
                IsPlannerAllowed = isPlannerAllowed,
                AllowCalendarSharing = allowCalendarSharing,
                AllowTenantMoveWithDataLoss = allowTenantMoveWithDataLoss,
                AllowRosterCreation = allowRosterCreation,
                AllowPlannerMobilePushNotifications = allowPlannerMobilePushNotifications
            };
            var result = await GraphHelper.PatchAsync(httpClient, accessToken, "https://tasks.office.com/taskAPI/tenantAdminSettings/Settings", content);
            return result;
        }

        /// <summary>
        /// Retrieves the Planner User Policy for the provided user
        /// </summary>
        /// <param name="userId">Azure Active Directory User Identifier or User Principal name</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerUserPolicy</returns>
        public static async Task<PlannerUserPolicy> GetPlannerUserPolicyAsync(HttpClient httpClient, string accessToken, string userId)
        {
            var result = await GraphHelper.GetAsync<PlannerUserPolicy>(httpClient, $"https://tasks.office.com/taskAPI/tenantAdminSettings/UserPolicy('{userId}')", accessToken);
            return result;
        }        

        /// <summary>
        /// Sets the Planner User Policy for the provided user
        /// </summary>
        /// <param name="userId">Azure Active Directory User Identifier or User Principal name</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerUserPolicy</returns>
        public static async Task<PlannerUserPolicy> SetPlannerUserPolicyAsync(HttpClient httpClient, string accessToken, string userId, bool? blockDeleteTasksNotCreatedBySelf)
        {
            var content = new PlannerUserPolicy
            {
                BlockDeleteTasksNotCreatedBySelf = blockDeleteTasksNotCreatedBySelf
            };
            var result = await GraphHelper.PutAsync<PlannerUserPolicy>(httpClient, $"https://tasks.office.com/taskAPI/tenantAdminSettings/UserPolicy('{userId}')", content, accessToken);
            return result;
        }

        #endregion

        private static async Task<Identity> ResolveIdentityAsync(HttpClient httpClient, string accessToken, Identity identity)
        {
            if (identity == null)
            {
                return null;
            }
            if (identity.DisplayName == null)
            {
                return await GraphHelper.GetAsync<Identity>(httpClient, $"v1.0/users/{identity.Id}", accessToken);
            }
            else
            {
                return identity;
            }
        }

        private static async Task<string> ResolveGroupName(HttpClient httpClient, string accessToken, string id)
        {
            var group = await GraphHelper.GetAsync<Group>(httpClient, $"v1.0/groups/{id}?$select=displayName", accessToken);
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

        public static async Task<IEnumerable<PlannerBucket>> GetBucketsAsync(HttpClient httpClient, string accessToken, string planId)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<PlannerBucket>(httpClient, $"v1.0/planner/plans/{planId}/buckets", accessToken);
            return collection.OrderBy(p => p.OrderHint);
        }

        public static async Task<PlannerBucket> CreateBucketAsync(HttpClient httpClient, string accessToken, string name, string planId)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name, planId = planId, orderHint = " !" }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<PlannerBucket>(httpClient, $"v1.0/planner/buckets", stringContent, accessToken);
        }

        public static async System.Threading.Tasks.Task RemoveBucketAsync(HttpClient httpClient, string accessToken, string bucketId)
        {
            var bucket = GraphHelper.GetAsync<PlannerBucket>(httpClient, $"v1.0/planner/buckets/{bucketId}", accessToken).GetAwaiter().GetResult();
            if (bucket != null)
            {
                await GraphHelper.DeleteAsync(httpClient, $"v1.0/planner/buckets/{bucketId}", accessToken, new Dictionary<string, string>() { { "IF-MATCH", bucket.ETag } });
            }
        }


        public static async Task<IEnumerable<PlannerTask>> GetBucketTasksAsync(HttpClient httpClient, string accessToken, string bucketId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerTask>();
            var collection = await GraphHelper.GetAsync<RestResultCollection<PlannerTask>>(httpClient, $"v1.0/planner/buckets/{bucketId}/tasks", accessToken);
            if (collection != null && collection.Items.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var task in collection.Items)
                    {
                        var fullIdentity = await ResolveIdentityAsync(httpClient, accessToken, task.CreatedBy.User);
                        task.CreatedBy.User = fullIdentity;
                        if (task.Assignments != null)
                        {
                            foreach (var assignment in task.Assignments)
                            {
                                assignment.Value.AssignedBy.User = await ResolveIdentityAsync(httpClient, accessToken, assignment.Value.AssignedBy.User);
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
                            var fullIdentity = await ResolveIdentityAsync(httpClient, accessToken, task.CreatedBy.User);
                            task.CreatedBy.User = fullIdentity;
                            if (task.Assignments != null)
                            {
                                foreach (var assignment in task.Assignments)
                                {
                                    assignment.Value.AssignedBy.User = await ResolveIdentityAsync(httpClient, accessToken, assignment.Value.AssignedBy.User);
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

        public static async Task<PlannerBucket> UpdateBucketAsync(HttpClient httpClient, string accessToken, string name, string bucketId)
        {
            var bucket = await GraphHelper.GetAsync<PlannerBucket>(httpClient, $"v1.0/planner/buckets/{bucketId}", accessToken);
            if (bucket != null)
            {
                var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name }));
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return await GraphHelper.PatchAsync<PlannerBucket>(httpClient, accessToken, $"v1.0/planner/buckets/{bucketId}", stringContent, new Dictionary<string, string>() { { "IF-MATCH", bucket.ETag } });
            }
            return null;
        }
        #endregion
    }
}
