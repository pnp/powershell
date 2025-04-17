using PnP.Framework.Diagnostics;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class Microsoft365GroupsUtility
    {
        internal class GroupsResult
        {
            public IEnumerable<Microsoft365Group> Groups;
            public List<Exception> Errors;
        }

        internal static GroupsResult GetGroups(ApiRequestHelper requestHelper, bool includeSiteUrl, bool includeOwners, string filter = null, bool includeSensitivityLabels = false)
        {
            var errors = new List<Exception>();
            var items = new List<Microsoft365Group>();
            string requestUrl = "v1.0/groups";
            Dictionary<string, string> additionalHeaders = null;
            if (string.IsNullOrEmpty(filter))
            {
                filter = "groupTypes/any(c:c+eq+'Unified')";
                requestUrl = $"v1.0/groups?$filter={filter}";
            }
            else
            {
                filter = $"({filter}) and groupTypes/any(c:c+eq+'Unified')";
                requestUrl = $"v1.0/groups?$filter={filter}";
                additionalHeaders = new Dictionary<string, string>
                    {
                        { "ConsistencyLevel", "eventual" }
                    };
            }
            var result = requestHelper.GetResultCollection<Microsoft365Group>(requestUrl, additionalHeaders: additionalHeaders);
            if (result != null && result.Any())
            {
                items.AddRange(result);
            }
            if (includeSiteUrl || includeOwners || includeSensitivityLabels)
            {
                var chunks = GraphBatchUtility.Chunk(items.Select(g => g.Id.ToString()), 20);
                if (includeOwners)
                {
                    foreach (var chunk in chunks)
                    {
                        var ownerResults = GraphBatchUtility.GetObjectCollectionBatched<Microsoft365User>(requestHelper, chunk.ToArray(), "/groups/{0}/owners");
                        foreach (var ownerResult in ownerResults.Results)
                        {
                            items.First(i => i.Id.ToString() == ownerResult.Key).Owners = ownerResult.Value;
                        }
                        if (ownerResults.Errors.Any())
                        {
                            errors.AddRange(ownerResults.Errors);
                        }
                    }
                }

                if (includeSiteUrl)
                {
                    foreach (var chunk in chunks)
                    {
                        var results = GraphBatchUtility.GetPropertyBatched(requestHelper, chunk.ToArray(), "/groups/{0}/sites/root", "webUrl");
                        foreach (var batchResult in results.Results)
                        {
                            items.First(i => i.Id.ToString() == batchResult.Key).SiteUrl = batchResult.Value;
                        }
                        if (results.Errors.Any())
                        {
                            errors.AddRange(results.Errors);
                        }
                    }
                }
                if (includeSensitivityLabels)
                {
                    foreach (var chunk in chunks)
                    {
                        var sensitivityLabelResults = GraphBatchUtility.GetObjectCollectionBatched<AssignedLabels>(requestHelper, chunk.ToArray(), "/groups/{0}/assignedLabels");
                        foreach (var sensitivityLabel in sensitivityLabelResults.Results)
                        {
                            items.First(i => i.Id.ToString() == sensitivityLabel.Key).AssignedLabels = sensitivityLabel.Value?.ToList();
                        }
                        if (sensitivityLabelResults.Errors.Any())
                        {
                            errors.AddRange(sensitivityLabelResults.Errors);
                        }
                    }
                }
            }
            // if(errors.Any())
            // {
            //     throw new AggregateException($"{errors.Count} error(s) occurred in a Graph batch request", errors);
            // }
            return new GroupsResult { Groups = items, Errors = errors };
        }

        internal static Microsoft365Group GetGroup(ApiRequestHelper requestHelper, Guid groupId, bool includeSiteUrl, bool includeOwners, bool detailed, bool includeSensitivityLabels)
        {
            var results = requestHelper.Get<RestResultCollection<Microsoft365Group>>($"v1.0/groups?$filter=groupTypes/any(c:c+eq+'Unified') and id eq '{groupId}'");

            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                if (includeSiteUrl)
                {
                    bool wait = true;
                    var iterations = 0;

                    while (wait)
                    {
                        iterations++;
                        try
                        {
                            var siteUrlResult = requestHelper.Get($"v1.0/groups/{group.Id}/sites/root?$select=webUrl");
                            if (!string.IsNullOrEmpty(siteUrlResult))
                            {
                                wait = false;
                                var resultElement = JsonSerializer.Deserialize<JsonElement>(siteUrlResult);
                                if (resultElement.TryGetProperty("webUrl", out JsonElement webUrlElement))
                                {
                                    group.SiteUrl = webUrlElement.GetString();
                                }
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            if (iterations * 30 >= 300)
                            {
                                throw;
                            }
                            else
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(30));
                            }
                        }
                    }
                }
                if (includeOwners)
                {
                    group.Owners = GetGroupMembers(requestHelper, "owners", group.Id.Value);
                }
                if (detailed)
                {
                    var exchangeOnlineProperties = GetGroupExchangeOnlineSettings(requestHelper, group.Id.Value);
                    group.AllowExternalSenders = exchangeOnlineProperties.AllowExternalSenders;
                    group.AutoSubscribeNewMembers = exchangeOnlineProperties.AutoSubscribeNewMembers;
                    group.IsSubscribedByMail = exchangeOnlineProperties.IsSubscribedByMail;
                }
                if (includeSensitivityLabels)
                {
                    var sensitivityLabels = GetGroupSensitivityLabels(requestHelper, group.Id.Value);
                    group.AssignedLabels = sensitivityLabels.AssignedLabels;
                }
                return group;
            }
            return null;
        }

        internal static Microsoft365Group GetGroup(ApiRequestHelper requestHelper, string displayName, bool includeSiteUrl, bool includeOwners, bool detailed, bool includeSensitivityLabels)
        {
            displayName = WebUtility.UrlEncode(displayName.Replace("'", "''"));
            var results = requestHelper.Get<RestResultCollection<Microsoft365Group>>($"v1.0/groups?$filter=groupTypes/any(c:c+eq+'Unified') and (displayName eq '{displayName}' or mailNickName eq '{displayName}')");
            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                if (includeSiteUrl)
                {
                    var siteUrlResult = requestHelper.Get($"v1.0/groups/{group.Id}/sites/root?$select=webUrl");
                    var resultElement = JsonSerializer.Deserialize<JsonElement>(siteUrlResult);
                    if (resultElement.TryGetProperty("webUrl", out JsonElement webUrlElement))
                    {
                        group.SiteUrl = webUrlElement.GetString();
                    }
                }
                if (includeOwners)
                {
                    group.Owners = GetGroupMembers(requestHelper, "owners", group.Id.Value);
                }
                if (detailed)
                {
                    var exchangeOnlineProperties = GetGroupExchangeOnlineSettings(requestHelper, group.Id.Value);
                    group.AllowExternalSenders = exchangeOnlineProperties.AllowExternalSenders;
                    group.AutoSubscribeNewMembers = exchangeOnlineProperties.AutoSubscribeNewMembers;
                    group.IsSubscribedByMail = exchangeOnlineProperties.IsSubscribedByMail;
                }
                if (includeSensitivityLabels)
                {
                    var sensitivityLabels = GetGroupSensitivityLabels(requestHelper, group.Id.Value);
                    group.AssignedLabels = sensitivityLabels.AssignedLabels;
                }
                return group;
            }
            return null;
        }

        internal static GroupsResult GetExpiringGroup(ApiRequestHelper requestHelper, int limit, bool includeSiteUrl, bool includeOwners)
        {
            var items = new List<Microsoft365Group>();
            var errors = new List<Exception>();
            var dateLimit = DateTime.UtcNow;
            var dateStr = dateLimit.AddDays(limit).ToString("yyyy-MM-ddTHH:mm:ssZ");

            // This query requires ConsistencyLevel header to be set.
            var additionalHeaders = new Dictionary<string, string>();
            additionalHeaders.Add("ConsistencyLevel", "eventual");

            // $count=true needs to be here for reasons
            // see this for some additional details: https://learn.microsoft.com/en-us/graph/aad-advanced-queries?tabs=http#group-properties
            var result = requestHelper.GetResultCollection<Microsoft365Group>($"v1.0/groups?$filter=groupTypes/any(c:c+eq+'Unified') and expirationDateTime le {dateStr}&$count=true", additionalHeaders: additionalHeaders);
            if (result != null && result.Any())
            {
                items.AddRange(result);
            }
            if (includeSiteUrl || includeOwners)
            {
                var chunks = GraphBatchUtility.Chunk(items.Select(g => g.Id.ToString()), 20);
                if (includeOwners)
                {
                    foreach (var chunk in chunks)
                    {
                        var ownerResults = GraphBatchUtility.GetObjectCollectionBatched<Microsoft365User>(requestHelper, chunk.ToArray(), "/groups/{0}/owners");
                        foreach (var ownerResult in ownerResults.Results)
                        {
                            items.First(i => i.Id.ToString() == ownerResult.Key).Owners = ownerResult.Value;
                        }
                        if (ownerResults.Errors.Any())
                        {
                            errors.AddRange(ownerResults.Errors);
                        }
                    }
                }

                if (includeSiteUrl)
                {
                    foreach (var chunk in chunks)
                    {
                        var results = GraphBatchUtility.GetPropertyBatched(requestHelper, chunk.ToArray(), "/groups/{0}/sites/root", "webUrl");
                        //var results = await GetSiteUrlBatchedAsync(connection, accessToken, chunk.ToArray());
                        foreach (var batchResult in results.Results)
                        {
                            items.First(i => i.Id.ToString() == batchResult.Key).SiteUrl = batchResult.Value;
                        }
                        if (results.Errors.Any())
                        {
                            errors.AddRange(results.Errors);
                        }
                    }
                }
            }
            return new GroupsResult { Groups = items, Errors = errors };
        }

        internal static Microsoft365Group GetDeletedGroup(ApiRequestHelper requestHelper, Guid groupId)
        {
            return requestHelper.Get<Microsoft365Group>($"v1.0/directory/deleteditems/microsoft.graph.group/{groupId}");
        }

        internal static Microsoft365Group GetDeletedGroup(ApiRequestHelper requestHelper, string groupName)
        {
            var results = requestHelper.Get<RestResultCollection<Microsoft365Group>>($"v1.0/directory/deleteditems/microsoft.graph.group?$filter=displayName eq '{groupName}' or mailNickname eq '{groupName}'");
            if (results != null && results.Items.Any())
            {
                return results.Items.First();
            }
            return null;
        }

        internal static IEnumerable<Microsoft365Group> GetDeletedGroups(ApiRequestHelper requestHelper)
        {
            var result = requestHelper.GetResultCollection<Microsoft365Group>("v1.0/directory/deleteditems/microsoft.graph.group");
            return result;
        }

        internal static Microsoft365Group RestoreDeletedGroup(ApiRequestHelper requestHelper, Guid groupId)
        {
            return requestHelper.Post<Microsoft365Group>($"v1.0/directory/deleteditems/microsoft.graph.group/{groupId}/restore");
        }

        internal static void PermanentlyDeleteDeletedGroup(ApiRequestHelper requestHelper, Guid groupId)
        {
            requestHelper.Delete($"v1.0/directory/deleteditems/microsoft.graph.group/{groupId}");
        }

        internal static void AddOwners(ApiRequestHelper requestHelper, Guid groupId, string[] users, bool removeExisting)
        {
            AddUsersToGroup(requestHelper, "owners", groupId, users, removeExisting);
        }

        internal static void AddDirectoryOwners(ApiRequestHelper requestHelper, Guid groupId, Guid[] users, bool removeExisting)
        {
            AddDirectoryObjectsToGroup(requestHelper, "owners", groupId, users, removeExisting);
        }

        internal static void AddMembers(ApiRequestHelper requestHelper, Guid groupId, string[] users, bool removeExisting)
        {
            AddUsersToGroup(requestHelper, "members", groupId, users, removeExisting);
        }

        internal static void AddDirectoryMembers(ApiRequestHelper requestHelper, Guid groupId, Guid[] users, bool removeExisting)
        {
            AddDirectoryObjectsToGroup(requestHelper, "members", groupId, users, removeExisting);
        }

        internal static string GetUserGraphUrlForUPN(string upn)
        {
            var escapedUpn = upn.Replace("#", "%23");

            if (escapedUpn.StartsWith("$")) return $"users('{escapedUpn}')";

            return $"users/{escapedUpn}";
        }

        private static void AddUsersToGroup(ApiRequestHelper requestHelper, string groupName, Guid groupId, string[] users, bool removeExisting)
        {
            foreach (var user in users)
            {
                var userIdResult = requestHelper.Get($"v1.0/{GetUserGraphUrlForUPN(user)}?$select=Id");
                var resultElement = JsonSerializer.Deserialize<JsonElement>(userIdResult);
                if (resultElement.TryGetProperty("id", out JsonElement idProperty))
                {

                    var postData = new Dictionary<string, string>() {
                    {
                        "@odata.id", $"https://{requestHelper.GraphEndPoint}/v1.0/users/{idProperty.GetString()}"
                    }
                };

                    requestHelper.Post($"v1.0/groups/{groupId}/{groupName}/$ref", postData);
                }
            }
        }

        private static void AddDirectoryObjectsToGroup(ApiRequestHelper requestHelper, string groupName, Guid groupId, Guid[] directoryObjects, bool removeExisting)
        {
            foreach (var dirObject in directoryObjects)
            {
                var postData = new Dictionary<string, string>() {
                    {
                        "@odata.id", $"https://{requestHelper.GraphEndPoint}/v1.0/directoryObjects/{dirObject}"
                    }
                };

                requestHelper.Post($"v1.0/groups/{groupId}/{groupName}/$ref", postData);
            }
        }

        internal static void RemoveOwners(ApiRequestHelper requestHelper, Guid groupId, string[] users)
        {
            RemoveUserFromGroup(requestHelper, "owners", groupId, users);
        }

        internal static void RemoveMembers(ApiRequestHelper requestHelper, Guid groupId, string[] users)
        {
            RemoveUserFromGroup(requestHelper, "members", groupId, users);
        }

        private static void RemoveUserFromGroup(ApiRequestHelper requestHelper, string groupName, Guid groupId, string[] users)
        {
            foreach (var user in users)
            {
                var resultString = requestHelper.Get($"v1.0/{GetUserGraphUrlForUPN(user)}?$select=Id");
                var resultElement = JsonSerializer.Deserialize<JsonElement>(resultString);
                if (resultElement.TryGetProperty("id", out JsonElement idElement))
                {
                    requestHelper.Delete($"v1.0/groups/{groupId}/{groupName}/{idElement.GetString()}/$ref");
                }
            }
        }

        internal static void RemoveGroup(ApiRequestHelper requestHelper, Guid groupId)
        {
            requestHelper.Delete($"v1.0/groups/{groupId}");
        }

        internal static IEnumerable<Microsoft365User> GetOwners(ApiRequestHelper requestHelper, Guid groupId)
        {
            return GetGroupMembers(requestHelper, "owners", groupId);
        }

        internal static IEnumerable<Microsoft365User> GetMembers(ApiRequestHelper requestHelper, Guid groupId)
        {
            return GetGroupMembers(requestHelper, "members", groupId);
        }
        
        internal static IEnumerable<Microsoft365User> GetTransitiveMembers(ApiRequestHelper requestHelper, Guid groupId)
        {
            return GetGroupMembers(requestHelper, "transitiveMembers", groupId);
        }

        private static IEnumerable<Microsoft365User> GetGroupMembers(ApiRequestHelper requestHelper, string userType, Guid groupId)
        {
            var results = requestHelper.GetResultCollection<Microsoft365User>($"v1.0/groups/{groupId}/{userType}?$select=*");
            return results;
        }

        private static Microsoft365Group GetGroupExchangeOnlineSettings(ApiRequestHelper requestHelper, Guid groupId)
        {
            var results = requestHelper.Get<Microsoft365Group>($"v1.0/groups/{groupId}?$select=allowExternalSenders,isSubscribedByMail,autoSubscribeNewMembers");
            return results;
        }

        private static Microsoft365Group GetGroupSensitivityLabels(ApiRequestHelper requestHelper, Guid groupId)
        {
            var results = requestHelper.Get<Microsoft365Group>($"v1.0/groups/{groupId}?$select=assignedLabels");
            return results;
        }

        internal static void ClearMembers(ApiRequestHelper requestHelper, Guid groupId)
        {
            var members = GetMembers(requestHelper, groupId);

            foreach (var member in members)
            {
                requestHelper.Delete($"v1.0/groups/{groupId}/members/{member.Id}/$ref");
            }
        }

        internal static void ClearOwnersAsync(ApiRequestHelper requestHelper, Guid groupId)
        {
            var members = GetOwners(requestHelper, groupId);

            foreach (var member in members)
            {
                requestHelper.Delete($"v1.0/groups/{groupId}/owners/{member.Id}/$ref");
            }
        }

        internal static void UpdateOwners(ApiRequestHelper requestHelper, Guid groupId, string[] owners)
        {
            var existingOwners = GetOwners(requestHelper, groupId);
            foreach (var owner in owners)
            {
                if (existingOwners.FirstOrDefault(o => o.UserPrincipalName == owner) == null)
                {
                    AddOwners(requestHelper, groupId, new string[] { owner }, false);
                }
            }
            foreach (var existingOwner in existingOwners)
            {
                if (!owners.Contains(existingOwner.UserPrincipalName))
                {
                    requestHelper.Delete($"v1.0/groups/{groupId}/owners/{existingOwner.Id}/$ref");
                }
            }
        }

        internal static void UpdateMembersAsync(ApiRequestHelper requestHelper, Guid groupId, string[] members)
        {
            var existingMembers = GetMembers(requestHelper, groupId);
            foreach (var member in members)
            {
                if (existingMembers.FirstOrDefault(o => o.UserPrincipalName == member) == null)
                {
                    AddMembers(requestHelper, groupId, new string[] { member }, false);
                }
            }
            foreach (var existingMember in existingMembers)
            {
                if (!members.Contains(existingMember.UserPrincipalName))
                {
                    requestHelper.Delete($"v1.0/groups/{groupId}/members/{existingMember.Id}/$ref");
                }
            }
        }

        internal static Microsoft365Group UpdateExchangeOnlineSetting(ApiRequestHelper requestHelper, Guid groupId, Microsoft365Group group)
        {
            var patchData = new
            {
                group.AllowExternalSenders,
                group.AutoSubscribeNewMembers
            };

            var result = requestHelper.Patch($"v1.0/groups/{groupId}", patchData);

            group.AllowExternalSenders = result.AllowExternalSenders;
            group.AutoSubscribeNewMembers = result.AutoSubscribeNewMembers;

            return group;
        }

        internal static Dictionary<string, string> GetSiteUrlBatched(ApiRequestHelper requestHelper, string[] groupIds)
        {
            Dictionary<string, string> returnValue = new Dictionary<string, string>();

            Dictionary<string, string> requests = new Dictionary<string, string>();
            var batch = new GraphBatch();
            int id = 0;
            foreach (var groupId in groupIds)
            {
                id++;
                batch.Requests.Add(new GraphBatchRequest() { Id = id.ToString(), Method = "GET", Url = $"/groups/{groupId}/sites/root?$select=webUrl" });
                requests.Add(id.ToString(), groupId);
            }
            var stringContent = new StringContent(JsonSerializer.Serialize(batch));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = requestHelper.Post<GraphBatchResponse>("v1.0/$batch", stringContent);
            if (result.Responses != null && result.Responses.Any())
            {
                foreach (var response in result.Responses)
                {
                    var groupId = requests.First(r => r.Key == response.Id).Value;
                    if (response.Body.TryGetValue("webUrl", out object webUrlObject))
                    {
                        var element = (JsonElement)webUrlObject;
                        returnValue.Add(groupId, element.GetString());
                    }
                }
            }
            return returnValue;
        }

        internal static Dictionary<string, string> GetUserIdsBatched(ApiRequestHelper requestHelper, string[] userPrincipalNames)
        {
            Dictionary<string, string> returnValue = new Dictionary<string, string>();

            Dictionary<string, string> requests = new Dictionary<string, string>();
            var batch = new GraphBatch();
            int id = 0;
            foreach (var upn in userPrincipalNames)
            {
                id++;
                batch.Requests.Add(new GraphBatchRequest() { Id = id.ToString(), Method = "GET", Url = $"/{GetUserGraphUrlForUPN(upn)}?$select=Id" });
                requests.Add(id.ToString(), upn);
            }
            var stringContent = new StringContent(JsonSerializer.Serialize(batch));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = requestHelper.Post<GraphBatchResponse>("v1.0/$batch", stringContent);
            if (result.Responses != null && result.Responses.Any())
            {
                foreach (var response in result.Responses)
                {
                    var groupId = requests.First(r => r.Key == response.Id).Value;
                    if (response.Body.TryGetValue("id", out object propertyObject))
                    {
                        var element = (JsonElement)propertyObject;
                        returnValue.Add(groupId, element.GetString());
                    }
                }
            }
            return returnValue;
        }

        internal static string[] GetUsersDataBindValue(ApiRequestHelper requestHelper, string[] users)
        {
            var userids = GetUserIdsBatched(requestHelper, users);
            if (userids.Any())
            {
                return userids.Select(u => $"https://{requestHelper.GraphEndPoint}/v1.0/users/{u.Value}").ToArray();
            }
            return null;
        }

        internal static Microsoft365Group Create(ApiRequestHelper requestHelper, Microsoft365Group group, bool createTeam, string logoPath, string[] owners, string[] members, bool? hideFromAddressLists, bool? hideFromOutlookClients, List<string> sensitivityLabels)
        {
            if (owners != null && owners.Length > 0)
            {
                group.OwnersODataBind = GetUsersDataBindValue(requestHelper, owners);
            }

            if (members != null && members.Length > 0)
            {
                group.MembersODataBind = GetUsersDataBindValue(requestHelper, members);
            }

            if (sensitivityLabels.Count > 0)
            {
                var assignedLabels = new List<AssignedLabels>();
                foreach (var label in sensitivityLabels)
                {
                    if (!Guid.Empty.Equals(label))
                    {
                        assignedLabels.Add(new AssignedLabels
                        {
                            labelId = label
                        });
                    }
                }

                group.AssignedLabels = assignedLabels;
            }

            var newGroup = requestHelper.Post("v1.0/groups", group);

            if (hideFromAddressLists.HasValue || hideFromOutlookClients.HasValue)
            {
                SetVisibility(requestHelper, newGroup.Id.Value, hideFromAddressLists, hideFromOutlookClients);
            }

            if (!string.IsNullOrEmpty(logoPath))
            {
                UploadLogoAsync(requestHelper, newGroup.Id.Value, logoPath);
            }

            if (createTeam)
            {
                CreateTeam(requestHelper, newGroup.Id.Value);
            }

            return newGroup;
        }

        internal static void UploadLogoAsync(ApiRequestHelper requestHelper, Guid groupId, string logoPath)
        {
            var fileBytes = System.IO.File.ReadAllBytes(logoPath);

            var content = new ByteArrayContent(fileBytes);
            var fileInfo = new System.IO.FileInfo(logoPath);
            var contentType = string.Empty;
            switch (fileInfo.Extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    {
                        contentType = "image/jpeg";
                        break;
                    }
                case ".gif":
                    {
                        contentType = "image/gif";
                        break;
                    }
                case ".png":
                    {
                        contentType = "image/png";
                        break;
                    }
            }
            if (!string.IsNullOrEmpty(contentType))
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                var updated = false;
                var retryCount = 10;
                while (retryCount > 0)
                {
                    var responseMessage = requestHelper.PutHttpContent($"/v1.0/groups/{groupId}/photo/$value", content);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        updated = true;
                    }
                    if (!updated)
                    {
                        Thread.Sleep(500 * (10 - retryCount));
                        retryCount--;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                throw new Exception("Unrecognized image format. Supported formats are .png, .jpg, .jpeg and .gif");
            }
        }

        internal static void CreateTeam(ApiRequestHelper requestHelper, Guid groupId)
        {
            var createTeamEndPoint = $"v1.0/groups/{groupId}/team";
            bool wait = true;
            var iterations = 0;

            while (wait)
            {
                iterations++;
                try
                {
                    var teamId = requestHelper.Put<object>(createTeamEndPoint, new { });
                    if (teamId != null)
                    {
                        wait = false;
                    }
                }
                catch (Exception)
                {
                    if (iterations * 30 >= 300)
                    {
                        wait = false;
                        throw;
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(30));
                    }
                }
            }
        }

        internal static void Renew(ApiRequestHelper requestHelper, Guid groupId)
        {
            requestHelper.Post($"v1.0/groups/{groupId}/renew", new { });
        }

        internal static Microsoft365Group Update(ApiRequestHelper requestHelper, Microsoft365Group group)
        {
            return requestHelper.Patch($"v1.0/groups/{group.Id}", group);
        }

        /// <summary>
        /// Allows to set the visibility of a group to hideFromAddressLists and hideFromOutlookClients.
        /// </summary>
        internal static void SetVisibility(ApiRequestHelper requestHelper, Guid groupId, bool? hideFromAddressLists, bool? hideFromOutlookClients)
        {
            var attempt = 1;
            var maxRetries = 10;
            var retryAfterSeconds = 5;

            while (true)
            {
                try
                {
                    requestHelper.Patch<dynamic>($"v1.0/groups/{groupId}", new
                    {
                        hideFromAddressLists,
                        hideFromOutlookClients
                    });

                    // Request successful, exit the loop
                    break;
                }
                catch (Exception e)
                {
                    if (attempt == maxRetries)
                    {
                        Log.Warning("Microsoft365GroupsUtility.SetVisibility", $"Failed to set the visibility of the group {groupId} to hideFromAddressLists: {hideFromAddressLists} and hideFromOutlookClients: {hideFromOutlookClients}. Exception: {e.Message}. Giving up after {maxRetries} attempts.");
                        break;
                    }
                    else
                    {
                        Log.Debug("Microsoft365GroupsUtility.SetVisibility", $"Failed to set the visibility of the group {groupId} to hideFromAddressLists: {hideFromAddressLists} and hideFromOutlookClients: {hideFromOutlookClients}. Exception: {e.Message}. Retrying in {retryAfterSeconds} seconds. Attempt {attempt} out of {maxRetries}.");
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(retryAfterSeconds));
                    attempt++;
                }
            }
        }

        internal static Microsoft365GroupSettingValueCollection GetGroupSettings(ApiRequestHelper requestHelper)
        {
            var result = requestHelper.Get<Microsoft365GroupSettingValueCollection>("v1.0/groupSettings", propertyNameCaseInsensitive: true);
            return result;
        }

        internal static Microsoft365GroupSetting GetGroupTenantSettings(ApiRequestHelper requestHelper, string groupSettingId)
        {
            var result = requestHelper.Get<Microsoft365GroupSetting>($"v1.0/groupSettings/{groupSettingId}", propertyNameCaseInsensitive: true);
            return result;
        }

        internal static Microsoft365GroupSettingValueCollection GetGroupSettings(ApiRequestHelper requestHelper, string groupId)
        {
            var result = requestHelper.Get<Microsoft365GroupSettingValueCollection>($"v1.0/groups/{groupId}/settings", propertyNameCaseInsensitive: true);
            return result;
        }
        internal static Microsoft365GroupSetting GetGroupSettings(ApiRequestHelper requestHelper, string groupSettingId, string groupId)
        {
            var result = requestHelper.Get<Microsoft365GroupSetting>($"v1.0/groups/{groupId}/settings/{groupSettingId}", propertyNameCaseInsensitive: true);
            return result;
        }
        internal static Microsoft365GroupSetting CreateGroupSetting(ApiRequestHelper requestHelper, dynamic groupSettingObject)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(groupSettingObject));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = requestHelper.Post<Microsoft365GroupSetting>("v1.0/groupSettings", stringContent, propertyNameCaseInsensitive: true);
            return result;
        }

        internal static Microsoft365GroupSetting CreateGroupSetting(ApiRequestHelper requestHelper, string groupId, dynamic groupSettingObject)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(groupSettingObject));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = requestHelper.Post<Microsoft365GroupSetting>($"v1.0/groups/{groupId}/settings", stringContent, propertyNameCaseInsensitive: true);
            return result;
        }

        internal static void UpdateGroupSetting(ApiRequestHelper requestHelper, string id, dynamic groupSettingObject)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(groupSettingObject));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            requestHelper.Patch(stringContent, $"v1.0/groupSettings/{id}");
        }

        internal static void UpdateGroupSetting(ApiRequestHelper requestHelper, string id, string groupId, dynamic groupSettingObject)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(groupSettingObject));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            requestHelper.Patch(stringContent, $"v1.0/groups/{groupId}/settings/{id}");
        }

        internal static void RemoveGroupSetting(ApiRequestHelper requestHelper, string id)
        {
            requestHelper.Delete($"v1.0/groupSettings/{id}");
        }

        internal static void RemoveGroupSetting(ApiRequestHelper requestHelper, string id, string groupId)
        {
            requestHelper.Delete($"v1.0/groups/{groupId}/settings/{id}");
        }

        internal static Microsoft365GroupTemplateSettingValueCollection GetGroupTemplateSettings(ApiRequestHelper requestHelper)
        {
            var result = requestHelper.Get<Microsoft365GroupTemplateSettingValueCollection>("v1.0/groupSettingTemplates", propertyNameCaseInsensitive: true);
            return result;
        }

        internal static Microsoft365GroupSettingTemplate GetGroupTemplateSettings(ApiRequestHelper requestHelper, string id)
        {
            var result = requestHelper.Get<Microsoft365GroupSettingTemplate>($"v1.0/groupSettingTemplates/{id}", propertyNameCaseInsensitive: true);
            return result;
        }

        internal static void SetSensitivityLabels(ApiRequestHelper requestHelper, Guid groupId, List<AssignedLabels> assignedLabels)
        {
            var patchData = new
            {
                assignedLabels,
            };

            var retry = true;
            var iteration = 0;
            while (retry)
            {
                try
                {
                    requestHelper.Patch<dynamic>($"v1.0/groups/{groupId}", patchData);
                    retry = false;
                }

                catch (Exception)
                {
                    Thread.Sleep(5000);
                    iteration++;
                }

                if (iteration > 10) // don't try more than 10 times
                {
                    retry = false;
                }
            }
        }

        internal static HttpResponseMessage DeletePhoto(ApiRequestHelper requestHelper, Guid groupId)
        {
            return requestHelper.Delete($"v1.0/groups/{groupId}/photo/$value");
        }

        internal static void UploadProfilePhotoAsync(ApiRequestHelper requestHelper, Guid userId, string logoPath)
        {
            var fileBytes = System.IO.File.ReadAllBytes(logoPath);

            var content = new ByteArrayContent(fileBytes);
            var fileInfo = new System.IO.FileInfo(logoPath);
            var contentType = string.Empty;
            switch (fileInfo.Extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    {
                        contentType = "image/jpeg";
                        break;
                    }
                case ".png":
                    {
                        contentType = "image/png";
                        break;
                    }
            }
            if (!string.IsNullOrEmpty(contentType))
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                var updated = false;
                var retryCount = 10;
                while (retryCount > 0)
                {
                    var responseMessage = requestHelper.PutHttpContent($"/v1.0/users/{userId}/photo/$value", content);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        updated = true;
                    }
                    if (!updated)
                    {
                        Thread.Sleep(500 * (10 - retryCount));
                        retryCount--;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                throw new Exception("Unrecognized image format. Supported formats are .png, .jpg and .jpeg");
            }
        }
    }
}
