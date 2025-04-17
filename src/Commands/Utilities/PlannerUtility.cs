using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class PlannerUtility
    {
        #region Plans
        public static IEnumerable<PlannerPlan> GetPlans(ApiRequestHelper requestHelper, string groupId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerPlan>();
            var collection = requestHelper.GetResultCollection<PlannerPlan>($"v1.0/groups/{groupId}/planner/plans");
            if (collection != null && collection.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var plan in collection)
                    {
                        var fullIdentity = ResolveIdentity(requestHelper, plan.CreatedBy.User);
                        plan.CreatedBy.User = fullIdentity;
                        var owner = ResolveGroupName(requestHelper, plan.Owner);
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

        public static PlannerPlan GetPlan(ApiRequestHelper requestHelper, string planId, bool resolveDisplayNames)
        {
            var plan = requestHelper.Get<PlannerPlan>($"v1.0/planner/plans/{planId}");
            if (resolveDisplayNames)
            {
                plan.CreatedBy.User = ResolveIdentity(requestHelper, plan.CreatedBy.User);
            }
            return plan;
        }

        public static PlannerPlanDetails GetPlanDetails(ApiRequestHelper requestHelper, string planId)
        {
            var plan = requestHelper.Get<PlannerPlanDetails>($"v1.0/planner/plans/{planId}/details");

            return plan;
        }

        public static PlannerPlan CreatePlan(ApiRequestHelper requestHelper, string groupId, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { owner = groupId, title = title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return requestHelper.Post<PlannerPlan>("v1.0/planner/plans", stringContent);
        }

        public static PlannerPlan UpdatePlan(ApiRequestHelper requestHelper, PlannerPlan plan, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var responseMessage = requestHelper.Patch(stringContent, $"v1.0/planner/plans/{plan.Id}", new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            while (responseMessage.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
            {
                // retrieve the plan again
                plan = requestHelper.Get<PlannerPlan>($"v1.0/planner/plans/{plan.Id}");
                responseMessage = requestHelper.Patch(stringContent, $"v1.0/planner/plans/{plan.Id}", new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<PlannerPlan>(responseContent);
            }
            return null;
        }

        public static void DeletePlan(ApiRequestHelper requestHelper, string planId)
        {
            var plan = GetPlan(requestHelper, planId, false);
            if (plan != null)
            {
                requestHelper.Delete($"v1.0/planner/plans/{planId}", new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            }
        }

        #endregion

        #region Tasks

        public static IEnumerable<PlannerTask> GetTasks(ApiRequestHelper requestHelper, string planId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerTask>();
            var collection = requestHelper.GetResultCollection<PlannerTask>($"v1.0/planner/plans/{planId}/tasks");
            if (collection != null && collection.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var task in collection)
                    {
                        var fullIdentity = ResolveIdentity(requestHelper, task.CreatedBy.User);
                        task.CreatedBy.User = fullIdentity;
                        if (task.Assignments != null)
                        {
                            foreach (var assignment in task.Assignments)
                            {
                                assignment.Value.AssignedBy.User = ResolveIdentity(requestHelper, assignment.Value.AssignedBy.User);
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

        public static PlannerTask GetTask(ApiRequestHelper requestHelper, string taskId, bool resolveDisplayNames, bool includeDetails)
        {
            var task = requestHelper.Get<PlannerTask>($"v1.0/planner/tasks/{taskId}");
            if (resolveDisplayNames)
            {
                task.CreatedBy.User = ResolveIdentity(requestHelper, task.CreatedBy.User);
            }
            if (includeDetails)
            {
                var taskDetails = GetTaskDetails(requestHelper, taskId, resolveDisplayNames);
                task.Details = taskDetails;
            }
            return task;
        }

        public static PlannerTaskDetails GetTaskDetails(ApiRequestHelper requestHelper, string taskId, bool resolveDisplayNames)
        {
            var taskDetails = requestHelper.Get<PlannerTaskDetails>($"v1.0/planner/tasks/{taskId}/details");
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
                        User = ResolveIdentity(requestHelper, checklistItem.Value.LastModifiedBy.User)
                    };
                }
                newItems.Add(checklistItem.Key, newCheckListItem);
            }
            taskDetails.Checklist = newItems;

            return taskDetails;
        }

        public static PlannerTask AddTask(ApiRequestHelper requestHelper, PlannerTask task)
        {
            return requestHelper.Post("v1.0/planner/tasks", task);
        }

        public static void DeleteTask(ApiRequestHelper requestHelper, string taskId)
        {
            var task = requestHelper.Get<PlannerTask>($"v1.0/planner/tasks/{taskId}");
            if (task != null)
            {
                requestHelper.Delete($"v1.0/planner/tasks/{taskId}", new Dictionary<string, string>() { { "IF-MATCH", task.ETag } });
            }
        }

        public static PlannerTask UpdateTask(ApiRequestHelper requestHelper, PlannerTask taskToUpdate, PlannerTask task)
        {
            return requestHelper.Patch($"v1.0/planner/tasks/{taskToUpdate.Id}", task, new Dictionary<string, string> { { "IF-MATCH", taskToUpdate.ETag } });
        }

        public static void UpdateTaskDetails(ApiRequestHelper requestHelper, PlannerTaskDetails taskToUpdate, string description)
        {
            var body = new PlannerTaskDetails
            {
                Description = description,
            };
            requestHelper.Patch($"v1.0/planner/tasks/{taskToUpdate.Id}/details", body, new Dictionary<string, string> { { "IF-MATCH", taskToUpdate.ETag } });
        }

        #endregion

        #region Rosters

        /// <summary>
        /// Creates a new Planner Roster
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static PlannerRoster CreateRoster(ApiRequestHelper requestHelper)
        {
            var stringContent = new StringContent("{ \"@odata.type\": \"#microsoft.graph.plannerRoster\" }");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return requestHelper.Post<PlannerRoster>("beta/planner/rosters", stringContent);
        }

        /// <summary>
        /// Gets a Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static PlannerRoster GetRoster(ApiRequestHelper requestHelper, string rosterId)
        {
            return requestHelper.Get<PlannerRoster>($"beta/planner/rosters/{rosterId}");
        }

        /// <summary>
        /// Deletes a Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage DeleteRoster(ApiRequestHelper requestHelper, string rosterId)
        {
            return requestHelper.Delete($"beta/planner/rosters/{rosterId}");
        }

        /// <summary>
        /// Adds a member to an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to add the member to</param>
        /// <param name="userId">Identifier of the user to add as a member</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static PlannerRoster AddRosterMember(ApiRequestHelper requestHelper, string rosterId, string userId)
        {
            var stringContent = new StringContent("{ \"@odata.type\": \"#microsoft.graph.plannerRosterMember\", \"userId\": \"" + userId + "\" }");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return requestHelper.Post<PlannerRoster>($"beta/planner/rosters/{rosterId}/members", stringContent);
        }

        /// <summary>
        /// Removes a member from an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to remove the member from</param>
        /// <param name="userId">Identifier of the user to remove as a member</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage RemoveRosterMember(ApiRequestHelper requestHelper, string rosterId, string userId)
        {
            return requestHelper.Delete($"beta/planner/rosters/{rosterId}/members/{userId}");
        }

        /// <summary>
        /// Returns all current members of an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to retrieve the members of</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>IEnumerable<PlannerRosterMember></returns>
        public static IEnumerable<PlannerRosterMember> GetRosterMembers(ApiRequestHelper requestHelper, string rosterId)
        {
            var returnCollection = new List<PlannerRosterMember>();
            var collection = requestHelper.GetResultCollection<PlannerRosterMember>($"beta/planner/rosters/{rosterId}/members");
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
        public static PlannerRoster GetRosterPlansByUser(ApiRequestHelper requestHelper, string userId)
        {
            return requestHelper.Get<PlannerRoster>($"beta/users/{userId}/planner/rosterPlans");
        }

        /// <summary>
        /// Gets the Planner Plans in a specific Planner Roster
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static PlannerRoster GetRosterPlansByRoster(ApiRequestHelper requestHelper, string rosterId)
        {
            return requestHelper.Get<PlannerRoster>($"beta/planner/rosters/{rosterId}/plans");
        }

        #endregion

        #region Admin Tasks

        /// <summary>
        /// Retrieves the Planner tenant configuration
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerTenantConfig</returns>
        public static PlannerTenantConfig GetPlannerConfig(ApiRequestHelper requestHelper)
        {
            var result = requestHelper.Get<PlannerTenantConfig>("https://tasks.office.com/taskAPI/tenantAdminSettings/Settings");
            return result;
        }

        /// <summary>
        /// Sets the Planner tenant configuration
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerTenantConfig</returns>
        public static PlannerTenantConfig SetPlannerConfig(ApiRequestHelper requestHelper, bool? isPlannerAllowed, bool? allowCalendarSharing, bool? allowTenantMoveWithDataLoss, bool? allowTenantMoveWithDataMigration, bool? allowRosterCreation, bool? allowPlannerMobilePushNotifications)
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
            var result = requestHelper.Patch("https://tasks.office.com/taskAPI/tenantAdminSettings/Settings", content);
            return result;
        }

        /// <summary>
        /// Retrieves the Planner User Policy for the provided user
        /// </summary>
        /// <param name="userId">Azure Active Directory User Identifier or User Principal name</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerUserPolicy</returns>
        public static PlannerUserPolicy GetPlannerUserPolicy(ApiRequestHelper requestHelper, string userId)
        {
            var result = requestHelper.Get<PlannerUserPolicy>($"https://tasks.office.com/taskAPI/tenantAdminSettings/UserPolicy('{userId}')");
            return result;
        }

        /// <summary>
        /// Sets the Planner User Policy for the provided user
        /// </summary>
        /// <param name="userId">Azure Active Directory User Identifier or User Principal name</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerUserPolicy</returns>
        public static PlannerUserPolicy SetPlannerUserPolicy(ApiRequestHelper requestHelper, string userId, bool? blockDeleteTasksNotCreatedBySelf)
        {
            var content = new PlannerUserPolicy
            {
                BlockDeleteTasksNotCreatedBySelf = blockDeleteTasksNotCreatedBySelf
            };
            var result = requestHelper.Put<PlannerUserPolicy>($"https://tasks.office.com/taskAPI/tenantAdminSettings/UserPolicy('{userId}')", content);
            return result;
        }

        #endregion

        private static Identity ResolveIdentity(ApiRequestHelper requestHelper, Identity identity)
        {
            if (identity == null)
            {
                return null;
            }
            if (identity.DisplayName == null)
            {
                return requestHelper.Get<Identity>($"v1.0/users/{identity.Id}");
            }
            else
            {
                return identity;
            }
        }

        private static string ResolveGroupName(ApiRequestHelper requestHelper, string id)
        {
            var group = requestHelper.Get<Group>($"v1.0/groups/{id}?$select=displayName");
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

        public static IEnumerable<PlannerBucket> GetBuckets(ApiRequestHelper requestHelper, string planId)
        {
            return requestHelper.GetResultCollection<PlannerBucket>($"v1.0/planner/plans/{planId}/buckets");
        }

        public static PlannerBucket CreateBucket(ApiRequestHelper requestHelper, string name, string planId)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name, planId = planId, orderHint = " !" }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return requestHelper.Post<PlannerBucket>($"v1.0/planner/buckets", stringContent);
        }

        public static void RemoveBucket(ApiRequestHelper requestHelper, string bucketId)
        {
            var bucket = requestHelper.Get<PlannerBucket>($"v1.0/planner/buckets/{bucketId}");
            if (bucket != null)
            {
                requestHelper.Delete($"v1.0/planner/buckets/{bucketId}", new Dictionary<string, string>() { { "IF-MATCH", bucket.ETag } });
            }
        }


        public static IEnumerable<PlannerTask> GetBucketTasks(ApiRequestHelper requestHelper, string bucketId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerTask>();
            var collection = requestHelper.Get<RestResultCollection<PlannerTask>>($"v1.0/planner/buckets/{bucketId}/tasks");
            if (collection != null && collection.Items.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var task in collection.Items)
                    {
                        var fullIdentity = ResolveIdentity(requestHelper, task.CreatedBy.User);
                        task.CreatedBy.User = fullIdentity;
                        if (task.Assignments != null)
                        {
                            foreach (var assignment in task.Assignments)
                            {
                                assignment.Value.AssignedBy.User = ResolveIdentity(requestHelper, assignment.Value.AssignedBy.User);
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
                            var fullIdentity = ResolveIdentity(requestHelper, task.CreatedBy.User);
                            task.CreatedBy.User = fullIdentity;
                            if (task.Assignments != null)
                            {
                                foreach (var assignment in task.Assignments)
                                {
                                    assignment.Value.AssignedBy.User = ResolveIdentity(requestHelper, assignment.Value.AssignedBy.User);
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

        public static PlannerBucket UpdateBucket(ApiRequestHelper requestHelper, string name, string bucketId)
        {
            var bucket = requestHelper.Get<PlannerBucket>($"v1.0/planner/buckets/{bucketId}");
            if (bucket != null)
            {
                var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name }));
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return requestHelper.Patch<PlannerBucket>($"v1.0/planner/buckets/{bucketId}", stringContent, new Dictionary<string, string>() { { "IF-MATCH", bucket.ETag } });
            }
            return null;
        }
        #endregion
    }
}
