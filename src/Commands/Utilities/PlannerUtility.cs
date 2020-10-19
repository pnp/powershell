using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Task = PnP.PowerShell.Commands.Model.Planner.Task;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class PlannerUtility
    {
        #region Plans
        public static async Task<IEnumerable<Plan>> GetPlansAsync(HttpClient httpClient, string accessToken, string groupId, bool resolveDisplayNames)
        {
            var returnCollection = new List<Plan>();
            var collection = await GraphHelper.GetAsync<RestResultCollection<Plan>>(httpClient, $"v1.0/groups/{groupId}/planner/plans", accessToken);
            if (collection != null && collection.Items.Any())
            {
                if (resolveDisplayNames)
                {
                    foreach (var plan in collection.Items)
                    {
                        var fullIdentity = await ResolveIdentityAsync(httpClient, accessToken, plan.CreatedBy.User);

                        returnCollection.Add(plan);
                    }
                }
                else
                {
                    returnCollection = collection.Items.ToList();
                }
            }
            return returnCollection;
        }

        public static async Task<Plan> GetPlanAsync(HttpClient httpClient, string accessToken, string planId, bool resolveDisplayNames)
        {
            var plan = await GraphHelper.GetAsync<Plan>(httpClient, $"v1.0/planner/plans/{planId}", accessToken);
            if (resolveDisplayNames)
            {
                plan.CreatedBy.User = await ResolveIdentityAsync(httpClient, accessToken, plan.CreatedBy.User);
            }
            return plan;
        }

        public static async Task<Plan> CreatePlanAsync(HttpClient httpClient, string accessToken, string groupId, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { owner = groupId, title = title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<Plan>(httpClient, "v1.0/planner/plans", stringContent, accessToken);
        }

        public static async Task<Plan> UpdatePlanAsync(HttpClient httpClient, string accessToken, Plan plan, string title)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var responseMessage = await GraphHelper.PatchAsync(httpClient, accessToken, $"v1.0/planner/plans/{plan.Id}", stringContent, new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            while (responseMessage.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
            {
                // retrieve the plan again
                plan = await GraphHelper.GetAsync<Plan>(httpClient, $"v1.0/planner/plans/{plan.Id}", accessToken);
                responseMessage = await GraphHelper.PatchAsync(httpClient, accessToken, $"v1.0/planner/plans/{plan.Id}", stringContent, new Dictionary<string, string>() { { "IF-MATCH", plan.ETag } });
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Plan>(responseContent);
            }
            return null;
        }
        #endregion


        #region Add Tasks
        public static async Task<IEnumerable<Task>> GetTasksAsync(HttpClient httpClient, string accessToken, string planId, bool resolveDisplayNames)
        {
            var returnCollection = new List<Task>();
            var collection = await GraphHelper.GetAsync<RestResultCollection<Task>>(httpClient, $"v1.0/planner/plans/{planId}/tasks", accessToken);
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
            }
            return returnCollection;
        }

        public static async Task<Task> AddTaskAsync(HttpClient httpClient, string accessToken, string planId, string bucketId, string title)
        {

            var stringContent = new StringContent(JsonSerializer.Serialize(new { planId = planId, bucketId = bucketId, title = title }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync<Task>(httpClient, "v1.0/planner/tasks", stringContent, accessToken);
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

        #region Buckets

        public static IEnumerable<Bucket> GetBuckets(HttpClient httpClient, string accessToken, string planId)
        {
            var collection = GraphHelper.GetAsync<RestResultCollection<Bucket>>(httpClient, $"v1.0/planner/plans/{planId}/buckets", accessToken).GetAwaiter().GetResult();
            if (collection.Items.Any())
            {
                return collection.Items.OrderBy(p => p.OrderHint);
            }
            return null;
        }

        public static Bucket CreateBucket(HttpClient httpClient, string accessToken, string name, string planId)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name, planId = planId, orderHint = " !" }));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return GraphHelper.PostAsync<Bucket>(httpClient, $"v1.0/planner/buckets", stringContent, accessToken).GetAwaiter().GetResult();
        }

        public static IEnumerable<Task> GetBucketTasks(HttpClient httpClient, string accessToken, string bucketId, bool resolveDisplayNames)
        {
            var returnCollection = new List<Task>();
            var collection = GraphHelper.GetAsync<RestResultCollection<Task>>(httpClient, $"v1.0/planner/buckets/{bucketId}/tasks", accessToken).GetAwaiter().GetResult();
            if (collection != null && collection.Items.Any())
            {
                if (resolveDisplayNames)
                {
                    System.Threading.Tasks.Task.Run(async () =>
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
                    }).GetAwaiter().GetResult();
                }
                else
                {
                    returnCollection = collection.Items.ToList();
                }
            }
            return returnCollection;
        }

        public static Bucket UpdateBucket(HttpClient httpClient, string accessToken, string name, string bucketId)
        {
            var bucket = GraphHelper.GetAsync<Bucket>(httpClient, $"v1.0/planner/buckets/{bucketId}", accessToken).GetAwaiter().GetResult();
            if (bucket != null)
            {
                var stringContent = new StringContent(JsonSerializer.Serialize(new { name = name }));
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return GraphHelper.PatchAsync<Bucket>(httpClient, accessToken, $"v1.0/planner/buckets/{bucketId}", stringContent, new Dictionary<string, string>() { { "IF-MATCH", bucket.ETag } }).GetAwaiter().GetResult();
            }
            return null;
        }
        #endregion
    }
}
