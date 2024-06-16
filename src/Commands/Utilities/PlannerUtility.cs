using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class PlannerUtility
    {
        #region Plans
        public static IEnumerable<PlannerPlan> GetPlans(Cmdlet cmdlet, PnPConnection connection, string accessToken, string groupId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerPlan>();
            var collection = GraphHelper.GetResultCollection<PlannerPlan>(cmdlet, connection, $"v1.0/groups/{groupId}/planner/plans", accessToken);
            if (collection != null && collection.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var plan in collection)
                    {
                        var fullIdentity = ResolveIdentity(cmdlet, connection, accessToken, plan.CreatedBy.User);
                        plan.CreatedBy.User = fullIdentity;
                        var owner = ResolveGroupName(cmdlet, connection, accessToken, plan.Owner);
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

        public static PlannerPlan GetPlan(Cmdlet cmdlet, PnPConnection connection, string accessToken, string planId, bool resolveDisplayNames)
        {
            var plan = GraphHelper.Get<PlannerPlan>(cmdlet, connection, $"v1.0/planner/plans/{planId}", accessToken);
            if (resolveDisplayNames)
            {
                plan.CreatedBy.User = ResolveIdentity(cmdlet, connection, accessToken, plan.CreatedBy.User);
            }
            return plan;
        }

        public static PlannerPlan CreatePlan(Cmdlet cmdlet, PnPConnection connection, string accessToken, string groupId, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { owner = groupId, title = title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return GraphHelper.Post<PlannerPlan>(cmdlet, connection, "v1.0/planner/plans", stringContent, accessToken);
        }

        public static PlannerPlan UpdatePlan(Cmdlet cmdlet, PnPConnection connection, string accessToken, PlannerPlan plan, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var responseMessage = GraphHelper.Patch(cmdlet, connection, accessToken, stringContent, $"v1.0/planner/plans/{plan.Id}", new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            while (responseMessage.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
            {
                // retrieve the plan again
                plan = GraphHelper.Get<PlannerPlan>(cmdlet, connection, $"v1.0/planner/plans/{plan.Id}", accessToken);
                responseMessage = GraphHelper.Patch(cmdlet, connection, accessToken, stringContent, $"v1.0/planner/plans/{plan.Id}", new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<PlannerPlan>(responseContent);
            }
            return null;
        }

        public static void DeletePlan(Cmdlet cmdlet, PnPConnection connection, string accessToken, string planId)
        {
            var plan = GetPlan(cmdlet, connection, accessToken, planId, false);
            if (plan != null)
            {
                GraphHelper.Delete(cmdlet, connection, $"v1.0/planner/plans/{planId}", accessToken, new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            }
        }

        #endregion

        #region Tasks

        public static IEnumerable<PlannerTask> GetTasks(Cmdlet cmdlet, PnPConnection connection, string accessToken, string planId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerTask>();
            var collection = GraphHelper.GetResultCollection<PlannerTask>(cmdlet, connection, $"v1.0/planner/plans/{planId}/tasks", accessToken);
            if (collection != null && collection.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var task in collection)
                    {
                        var fullIdentity = ResolveIdentity(cmdlet, connection, accessToken, task.CreatedBy.User);
                        task.CreatedBy.User = fullIdentity;
                        if (task.Assignments != null)
                        {
                            foreach (var assignment in task.Assignments)
                            {
                                assignment.Value.AssignedBy.User = ResolveIdentity(cmdlet, connection, accessToken, assignment.Value.AssignedBy.User);
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

        public static PlannerTask GetTask(Cmdlet cmdlet, PnPConnection connection, string accessToken, string taskId, bool resolveDisplayNames, bool includeDetails)
        {
            var task = GraphHelper.Get<PlannerTask>(cmdlet, connection, $"v1.0/planner/tasks/{taskId}", accessToken);
            if (resolveDisplayNames)
            {
                task.CreatedBy.User = ResolveIdentity(cmdlet, connection, accessToken, task.CreatedBy.User);
            }
            if (includeDetails)
            {
                var taskDetails = GetTaskDetails(cmdlet,connection, accessToken, taskId, resolveDisplayNames);
                task.Details = taskDetails;
            }
            return task;
        }

        public static PlannerTaskDetails GetTaskDetails(Cmdlet cmdlet, PnPConnection connection, string accessToken, string taskId, bool resolveDisplayNames)
        {
            var taskDetails = GraphHelper.Get<PlannerTaskDetails>(cmdlet, connection, $"v1.0/planner/tasks/{taskId}/details", accessToken);
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
                        User = ResolveIdentity(cmdlet, connection, accessToken, checklistItem.Value.LastModifiedBy.User)
                    };
                }
                newItems.Add(checklistItem.Key, newCheckListItem);
            }
            taskDetails.Checklist = newItems;

            return taskDetails;
        }

        public static PlannerTask AddTask(Cmdlet cmdlet, PnPConnection connection, string accessToken, PlannerTask task)
        {
            return GraphHelper.Post(cmdlet, connection, "v1.0/planner/tasks", task, accessToken);
        }

        public static void DeleteTask(Cmdlet cmdlet, PnPConnection connection, string accessToken, string taskId)
        {
            var task = GraphHelper.Get<PlannerTask>(cmdlet, connection, $"v1.0/planner/tasks/{taskId}", accessToken);
            if (task != null)
            {
                GraphHelper.Delete(cmdlet, connection, $"v1.0/planner/tasks/{taskId}", accessToken, new Dictionary<string, string>() { { "IF-MATCH", task.ETag } });
            }
        }

        public static PlannerTask UpdateTask(Cmdlet cmdlet, PnPConnection connection, string accessToken, PlannerTask taskToUpdate, PlannerTask task)
        {
            return GraphHelper.Patch(cmdlet, connection, accessToken, $"v1.0/planner/tasks/{taskToUpdate.Id}", task, new Dictionary<string, string> { { "IF-MATCH", taskToUpdate.ETag } });
        }

        public static void UpdateTaskDetails(Cmdlet cmdlet, PnPConnection connection, string accessToken, PlannerTaskDetails taskToUpdate, string description)
        {
            var body = new PlannerTaskDetails
            {
                Description = description,
            };
            GraphHelper.Patch(cmdlet, connection, accessToken, $"v1.0/planner/tasks/{taskToUpdate.Id}/details", body, new Dictionary<string, string> { { "IF-MATCH", taskToUpdate.ETag } });
        }

        #endregion

        #region Rosters

        /// <summary>
        /// Creates a new Planner Roster
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static PlannerRoster CreateRoster(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var stringContent = new StringContent("{ \"@odata.type\": \"#microsoft.graph.plannerRoster\" }");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return GraphHelper.Post<PlannerRoster>(cmdlet, connection, "beta/planner/rosters", stringContent, accessToken);
        }

        /// <summary>
        /// Gets a Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static PlannerRoster GetRoster(Cmdlet cmdlet, PnPConnection connection, string accessToken, string rosterId)
        {
            return GraphHelper.Get<PlannerRoster>(cmdlet, connection, $"beta/planner/rosters/{rosterId}", accessToken);
        }        

        /// <summary>
        /// Deletes a Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage DeleteRoster(Cmdlet cmdlet, PnPConnection connection, string accessToken, string rosterId)
        {
            return GraphHelper.Delete(cmdlet, connection, $"beta/planner/rosters/{rosterId}", accessToken);
        }

        /// <summary>
        /// Adds a member to an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to add the member to</param>
        /// <param name="userId">Identifier of the user to add as a member</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static PlannerRoster AddRosterMember(Cmdlet cmdlet, PnPConnection connection, string accessToken, string rosterId, string userId)
        {
            var stringContent = new StringContent("{ \"@odata.type\": \"#microsoft.graph.plannerRosterMember\", \"userId\": \"" + userId + "\" }");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return GraphHelper.Post<PlannerRoster>(cmdlet, connection, $"beta/planner/rosters/{rosterId}/members", stringContent, accessToken);
        }

        /// <summary>
        /// Removes a member from an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to remove the member from</param>
        /// <param name="userId">Identifier of the user to remove as a member</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage RemoveRosterMember(Cmdlet cmdlet, PnPConnection connection, string accessToken, string rosterId, string userId)
        {
            return GraphHelper.Delete(cmdlet, connection, $"beta/planner/rosters/{rosterId}/members/{userId}", accessToken);
        } 

        /// <summary>
        /// Returns all current members of an existing Planner Roster
        /// </summary>
        /// <param name="rosterId">Identifier of the roster to retrieve the members of</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>IEnumerable<PlannerRosterMember></returns>
        public static IEnumerable<PlannerRosterMember> GetRosterMembers(Cmdlet cmdlet, PnPConnection connection, string accessToken, string rosterId)
        {
            var returnCollection = new List<PlannerRosterMember>();
            var collection = GraphHelper.GetResultCollection<PlannerRosterMember>(cmdlet, connection, $"beta/planner/rosters/{rosterId}/members", accessToken);
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
        public static PlannerRoster GetRosterPlansByUser(Cmdlet cmdlet, PnPConnection connection, string accessToken, string userId)
        {
            return GraphHelper.Get<PlannerRoster>(cmdlet, connection, $"beta/users/{userId}/planner/rosterPlans", accessToken);
        }

        /// <summary>
        /// Gets the Planner Plans in a specific Planner Roster
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerRoster</returns>
        public static PlannerRoster GetRosterPlansByRoster(Cmdlet cmdlet, PnPConnection connection, string accessToken, string rosterId)
        {
            return GraphHelper.Get<PlannerRoster>(cmdlet, connection, $"beta/planner/rosters/{rosterId}/plans", accessToken);
        }         

        #endregion

        #region Admin Tasks

        /// <summary>
        /// Retrieves the Planner tenant configuration
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerTenantConfig</returns>
        public static PlannerTenantConfig GetPlannerConfig(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var result = GraphHelper.Get<PlannerTenantConfig>(cmdlet, connection, "https://tasks.office.com/taskAPI/tenantAdminSettings/Settings", accessToken);
            return result;
        }

        /// <summary>
        /// Sets the Planner tenant configuration
        /// </summary>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerTenantConfig</returns>
        public static PlannerTenantConfig SetPlannerConfig(Cmdlet cmdlet, PnPConnection connection, string accessToken, bool? isPlannerAllowed, bool? allowCalendarSharing, bool? allowTenantMoveWithDataLoss, bool? allowTenantMoveWithDataMigration, bool? allowRosterCreation, bool? allowPlannerMobilePushNotifications)
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
            var result = GraphHelper.Patch(cmdlet, connection, accessToken, "https://tasks.office.com/taskAPI/tenantAdminSettings/Settings", content);
            return result;
        }

        /// <summary>
        /// Retrieves the Planner User Policy for the provided user
        /// </summary>
        /// <param name="userId">Azure Active Directory User Identifier or User Principal name</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerUserPolicy</returns>
        public static PlannerUserPolicy GetPlannerUserPolicy(Cmdlet cmdlet, PnPConnection connection, string accessToken, string userId)
        {
            var result = GraphHelper.Get<PlannerUserPolicy>(cmdlet, connection, $"https://tasks.office.com/taskAPI/tenantAdminSettings/UserPolicy('{userId}')", accessToken);
            return result;
        }        

        /// <summary>
        /// Sets the Planner User Policy for the provided user
        /// </summary>
        /// <param name="userId">Azure Active Directory User Identifier or User Principal name</param>
        /// <param name="httpClient">HttpClient instance to use to send out requests</param>
        /// <param name="accessToken">AccessToken to use to authenticate the request</param>
        /// <returns>PlannerUserPolicy</returns>
        public static PlannerUserPolicy SetPlannerUserPolicy(Cmdlet cmdlet, PnPConnection connection, string accessToken, string userId, bool? blockDeleteTasksNotCreatedBySelf)
        {
            var content = new PlannerUserPolicy
            {
                BlockDeleteTasksNotCreatedBySelf = blockDeleteTasksNotCreatedBySelf
            };
            var result = GraphHelper.Put<PlannerUserPolicy>(cmdlet, connection, $"https://tasks.office.com/taskAPI/tenantAdminSettings/UserPolicy('{userId}')", content, accessToken);
            return result;
        }

        #endregion

        private static Identity ResolveIdentity(Cmdlet cmdlet, PnPConnection connection, string accessToken, Identity identity)
        {
            if (identity == null)
            {
                return null;
            }
            if (identity.DisplayName == null)
            {
                return GraphHelper.Get<Identity>(cmdlet, connection, $"v1.0/users/{identity.Id}", accessToken);
            }
            else
            {
                return identity;
            }
        }

        private static string ResolveGroupName(Cmdlet cmdlet, PnPConnection connection, string accessToken, string id)
        {
            var group = GraphHelper.Get<Group>(cmdlet, connection, $"v1.0/groups/{id}?$select=displayName", accessToken);
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

        public static IEnumerable<PlannerBucket> GetBuckets(Cmdlet cmdlet, PnPConnection connection, string accessToken, string planId)
        {
            return GraphHelper.GetResultCollection<PlannerBucket>(cmdlet, connection, $"v1.0/planner/plans/{planId}/buckets", accessToken); 
        }

        public static PlannerBucket CreateBucket(Cmdlet cmdlet, PnPConnection connection, string accessToken, string name, string planId)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name, planId = planId, orderHint = " !" }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return GraphHelper.Post<PlannerBucket>(cmdlet, connection, $"v1.0/planner/buckets", stringContent, accessToken);
        }

        public static void RemoveBucket(Cmdlet cmdlet, PnPConnection connection, string accessToken, string bucketId)
        {
            var bucket = GraphHelper.Get<PlannerBucket>(cmdlet, connection, $"v1.0/planner/buckets/{bucketId}", accessToken);
            if (bucket != null)
            {
                GraphHelper.Delete(cmdlet, connection, $"v1.0/planner/buckets/{bucketId}", accessToken, new Dictionary<string, string>() { { "IF-MATCH", bucket.ETag } });
            }
        }


        public static IEnumerable<PlannerTask> GetBucketTasks(Cmdlet cmdlet, PnPConnection connection, string accessToken, string bucketId, bool resolveDisplayNames)
        {
            var returnCollection = new List<PlannerTask>();
            var collection = GraphHelper.Get<RestResultCollection<PlannerTask>>(cmdlet, connection, $"v1.0/planner/buckets/{bucketId}/tasks", accessToken);
            if (collection != null && collection.Items.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var task in collection.Items)
                    {
                        var fullIdentity = ResolveIdentity(cmdlet, connection, accessToken, task.CreatedBy.User);
                        task.CreatedBy.User = fullIdentity;
                        if (task.Assignments != null)
                        {
                            foreach (var assignment in task.Assignments)
                            {
                                assignment.Value.AssignedBy.User = ResolveIdentity(cmdlet, connection, accessToken, assignment.Value.AssignedBy.User);
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
                            var fullIdentity = ResolveIdentity(cmdlet, connection, accessToken, task.CreatedBy.User);
                            task.CreatedBy.User = fullIdentity;
                            if (task.Assignments != null)
                            {
                                foreach (var assignment in task.Assignments)
                                {
                                    assignment.Value.AssignedBy.User = ResolveIdentity(cmdlet, connection, accessToken, assignment.Value.AssignedBy.User);
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

        public static PlannerBucket UpdateBucket(Cmdlet cmdlet, PnPConnection connection, string accessToken, string name, string bucketId)
        {
            var bucket = GraphHelper.Get<PlannerBucket>(cmdlet, connection, $"v1.0/planner/buckets/{bucketId}", accessToken);
            if (bucket != null)
            {
                var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name }));
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return GraphHelper.Patch<PlannerBucket>(cmdlet, connection, accessToken, $"v1.0/planner/buckets/{bucketId}", stringContent, new Dictionary<string, string>() { { "IF-MATCH", bucket.ETag } });
            }
            return null;
        }
        #endregion
    }
}
