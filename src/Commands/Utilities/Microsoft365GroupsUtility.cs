using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Text.Json;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System.Threading;
using System.Net;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class ClearOwners
    {
        internal static IEnumerable<Microsoft365Group> GetGroups(Cmdlet cmdlet, PnPConnection connection, string accessToken, bool includeSiteUrl, bool includeOwners, string filter = null, bool includeSensitivityLabels = false)
        {
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
            var result = GraphHelper.GetResultCollection<Microsoft365Group>(cmdlet, connection, requestUrl, accessToken, additionalHeaders: additionalHeaders);
            if (result != null && result.Any())
            {
                items.AddRange(result);
            }
            if (includeSiteUrl || includeOwners || includeSensitivityLabels)
            {
                var chunks = BatchUtility.Chunk(items.Select(g => g.Id.ToString()), 20);
                if (includeOwners)
                {
                    foreach (var chunk in chunks)
                    {
                        var ownerResults = BatchUtility.GetObjectCollectionBatched<Microsoft365User>(cmdlet, connection, accessToken, chunk.ToArray(), "/groups/{0}/owners");
                        foreach (var ownerResult in ownerResults)
                        {
                            items.First(i => i.Id.ToString() == ownerResult.Key).Owners = ownerResult.Value;
                        }
                    }
                }

                if (includeSiteUrl)
                {
                    foreach (var chunk in chunks)
                    {
                        var results = BatchUtility.GetPropertyBatched(cmdlet, connection, accessToken, chunk.ToArray(), "/groups/{0}/sites/root", "webUrl");                        
                        foreach (var batchResult in results)
                        {
                            items.First(i => i.Id.ToString() == batchResult.Key).SiteUrl = batchResult.Value;
                        }
                    }
                }
                if (includeSensitivityLabels)
                {
                    foreach (var chunk in chunks)
                    {
                        var sensitivityLabelResults = BatchUtility.GetObjectCollectionBatched<AssignedLabels>(cmdlet, connection, accessToken, chunk.ToArray(), "/groups/{0}/assignedLabels");
                        foreach (var sensitivityLabel in sensitivityLabelResults)
                        {
                            items.First(i => i.Id.ToString() == sensitivityLabel.Key).AssignedLabels = sensitivityLabel.Value?.ToList();
                        }
                    }
                }
            }
            return items;
        }

        internal static Microsoft365Group GetGroup(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken, bool includeSiteUrl, bool includeOwners, bool detailed, bool includeSensitivityLabels)
        {
            var results = GraphHelper.Get<RestResultCollection<Microsoft365Group>>(cmdlet, connection, $"v1.0/groups?$filter=groupTypes/any(c:c+eq+'Unified') and id eq '{groupId}'", accessToken);

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
                            var siteUrlResult = GraphHelper.Get(cmdlet, connection, $"v1.0/groups/{group.Id}/sites/root?$select=webUrl", accessToken);
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
                    group.Owners = GetGroupMembers(cmdlet, "owners", connection, group.Id.Value, accessToken);
                }
                if (detailed)
                {
                    var exchangeOnlineProperties = GetGroupExchangeOnlineSettings(cmdlet, connection, group.Id.Value, accessToken);
                    group.AllowExternalSenders = exchangeOnlineProperties.AllowExternalSenders;
                    group.AutoSubscribeNewMembers = exchangeOnlineProperties.AutoSubscribeNewMembers;
                    group.IsSubscribedByMail = exchangeOnlineProperties.IsSubscribedByMail;
                }
                if (includeSensitivityLabels)
                {
                    var sensitivityLabels = GetGroupSensitivityLabels(cmdlet, connection, group.Id.Value, accessToken);
                    group.AssignedLabels = sensitivityLabels.AssignedLabels;
                }
                return group;
            }
            return null;
        }

        internal static Microsoft365Group GetGroup(Cmdlet cmdlet, PnPConnection connection, string displayName, string accessToken, bool includeSiteUrl, bool includeOwners, bool detailed, bool includeSensitivityLabels)
        {
            displayName = WebUtility.UrlEncode(displayName.Replace("'", "''"));
            var results = GraphHelper.Get<RestResultCollection<Microsoft365Group>>(cmdlet, connection, $"v1.0/groups?$filter=groupTypes/any(c:c+eq+'Unified') and (displayName eq '{displayName}' or mailNickName eq '{displayName}')", accessToken);
            if (results != null && results.Items.Any())
            {
                var group = results.Items.First();
                if (includeSiteUrl)
                {
                    var siteUrlResult = GraphHelper.Get(cmdlet, connection, $"v1.0/groups/{group.Id}/sites/root?$select=webUrl", accessToken);
                    var resultElement = JsonSerializer.Deserialize<JsonElement>(siteUrlResult);
                    if (resultElement.TryGetProperty("webUrl", out JsonElement webUrlElement))
                    {
                        group.SiteUrl = webUrlElement.GetString();
                    }
                }
                if (includeOwners)
                {
                    group.Owners = GetGroupMembers(cmdlet, "owners", connection, group.Id.Value, accessToken);
                }
                if (detailed)
                {
                    var exchangeOnlineProperties = GetGroupExchangeOnlineSettings(cmdlet, connection, group.Id.Value, accessToken);
                    group.AllowExternalSenders = exchangeOnlineProperties.AllowExternalSenders;
                    group.AutoSubscribeNewMembers = exchangeOnlineProperties.AutoSubscribeNewMembers;
                    group.IsSubscribedByMail = exchangeOnlineProperties.IsSubscribedByMail;
                }
                if (includeSensitivityLabels)
                {
                    var sensitivityLabels = GetGroupSensitivityLabels(cmdlet, connection, group.Id.Value, accessToken);
                    group.AssignedLabels = sensitivityLabels.AssignedLabels;
                }
                return group;
            }
            return null;
        }

        internal static IEnumerable<Microsoft365Group> GetExpiringGroup(Cmdlet cmdlet, PnPConnection connection, string accessToken, int limit, bool includeSiteUrl, bool includeOwners)
        {
            var items = new List<Microsoft365Group>();

            var dateLimit = DateTime.UtcNow;
            var dateStr = dateLimit.AddDays(limit).ToString("yyyy-MM-ddTHH:mm:ssZ");

            // This query requires ConsistencyLevel header to be set.
            var additionalHeaders = new Dictionary<string, string>();
            additionalHeaders.Add("ConsistencyLevel", "eventual");

            // $count=true needs to be here for reasons
            // see this for some additional details: https://learn.microsoft.com/en-us/graph/aad-advanced-queries?tabs=http#group-properties
            var result = GraphHelper.GetResultCollection<Microsoft365Group>(cmdlet, connection, $"v1.0/groups?$filter=groupTypes/any(c:c+eq+'Unified') and expirationDateTime le {dateStr}&$count=true", accessToken, additionalHeaders: additionalHeaders);
            if (result != null && result.Any())
            {
                items.AddRange(result);
            }
            if (includeSiteUrl || includeOwners)
            {
                var chunks = BatchUtility.Chunk(items.Select(g => g.Id.ToString()), 20);
                if (includeOwners)
                {
                    foreach (var chunk in chunks)
                    {
                        var ownerResults = BatchUtility.GetObjectCollectionBatched<Microsoft365User>(cmdlet, connection, accessToken, chunk.ToArray(), "/groups/{0}/owners");
                        foreach (var ownerResult in ownerResults)
                        {
                            items.First(i => i.Id.ToString() == ownerResult.Key).Owners = ownerResult.Value;
                        }
                    }
                }

                if (includeSiteUrl)
                {
                    foreach (var chunk in chunks)
                    {
                        var results = BatchUtility.GetPropertyBatched(cmdlet, connection, accessToken, chunk.ToArray(), "/groups/{0}/sites/root", "webUrl");
                        //var results = await GetSiteUrlBatchedAsync(connection, accessToken, chunk.ToArray());
                        foreach (var batchResult in results)
                        {
                            items.First(i => i.Id.ToString() == batchResult.Key).SiteUrl = batchResult.Value;
                        }
                    }
                }
            }
            return items;
        }

        internal static Microsoft365Group GetDeletedGroup(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            return GraphHelper.Get<Microsoft365Group>(cmdlet, connection, $"v1.0/directory/deleteditems/microsoft.graph.group/{groupId}", accessToken);
        }

        internal static Microsoft365Group GetDeletedGroup(Cmdlet cmdlet, PnPConnection connection, string groupName, string accessToken)
        {
            var results = GraphHelper.Get<RestResultCollection<Microsoft365Group>>(cmdlet, connection, $"v1.0/directory/deleteditems/microsoft.graph.group?$filter=displayName eq '{groupName}' or mailNickname eq '{groupName}'", accessToken);
            if (results != null && results.Items.Any())
            {
                return results.Items.First();
            }
            return null;
        }

        internal static IEnumerable<Microsoft365Group> GetDeletedGroups(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var result = GraphHelper.GetResultCollection<Microsoft365Group>(cmdlet, connection, "v1.0/directory/deleteditems/microsoft.graph.group", accessToken);
            return result;
        }

        internal static Microsoft365Group RestoreDeletedGroup(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            return GraphHelper.Post<Microsoft365Group>(cmdlet, connection, $"v1.0/directory/deleteditems/microsoft.graph.group/{groupId}/restore", accessToken);
        }

        internal static void PermanentlyDeleteDeletedGroup(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            GraphHelper.Delete(cmdlet, connection, $"v1.0/directory/deleteditems/microsoft.graph.group/{groupId}", accessToken);
        }

        internal static void AddOwners(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string[] users, string accessToken, bool removeExisting)
        {
            AddUsersToGroup(cmdlet, "owners", connection, groupId, users, accessToken, removeExisting);
        }

        internal static void AddDirectoryOwners(Cmdlet cmdlet, PnPConnection connection, Guid groupId, Guid[] users, string accessToken, bool removeExisting)
        {
            AddDirectoryObjectsToGroup(cmdlet, "owners", connection, groupId, users, accessToken, removeExisting);
        }

        internal static void AddMembers(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string[] users, string accessToken, bool removeExisting)
        {
            AddUsersToGroup(cmdlet, "members", connection, groupId, users, accessToken, removeExisting);
        }

        internal static void AddDirectoryMembers(Cmdlet cmdlet, PnPConnection connection, Guid groupId, Guid[] users, string accessToken, bool removeExisting)
        {
            AddDirectoryObjectsToGroup(cmdlet, "members", connection, groupId, users, accessToken, removeExisting);
        }

        internal static string GetUserGraphUrlForUPN(string upn)
        {
            var escapedUpn = upn.Replace("#", "%23");

            if (escapedUpn.StartsWith("$")) return $"users('{escapedUpn}')";

            return $"users/{escapedUpn}";
        }

        private static void AddUsersToGroup(Cmdlet cmdlet, string groupName, PnPConnection connection, Guid groupId, string[] users, string accessToken, bool removeExisting)
        {
            foreach (var user in users)
            {
                var userIdResult = GraphHelper.Get(cmdlet, connection, $"v1.0/{GetUserGraphUrlForUPN(user)}?$select=Id", accessToken);
                var resultElement = JsonSerializer.Deserialize<JsonElement>(userIdResult);
                if (resultElement.TryGetProperty("id", out JsonElement idProperty))
                {

                    var postData = new Dictionary<string, string>() {
                    {
                        "@odata.id", $"https://{connection.GraphEndPoint}/v1.0/users/{idProperty.GetString()}"
                    }
                };
                    var stringContent = new StringContent(JsonSerializer.Serialize(postData));
                    stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    GraphHelper.Post(cmdlet, connection, $"v1.0/groups/{groupId}/{groupName}/$ref", accessToken, stringContent);
                }
            }
        }

        private static void AddDirectoryObjectsToGroup(Cmdlet cmdlet, string groupName, PnPConnection connection, Guid groupId, Guid[] directoryObjects, string accessToken, bool removeExisting)
        {
            foreach (var dirObject in directoryObjects)
            {
                var postData = new Dictionary<string, string>() {
                    {
                        "@odata.id", $"https://{connection.GraphEndPoint}/v1.0/directoryObjects/{dirObject}"
                    }
                };

                var stringContent = new StringContent(JsonSerializer.Serialize(postData));
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                GraphHelper.Post(cmdlet, connection, $"v1.0/groups/{groupId}/{groupName}/$ref", accessToken, stringContent);
            }
        }

        internal static void RemoveOwners(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string[] users, string accessToken)
        {
            RemoveUserFromGroup(cmdlet, "owners", connection, groupId, users, accessToken);
        }

        internal static void RemoveMembers(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string[] users, string accessToken)
        {
            RemoveUserFromGroup(cmdlet, "members", connection, groupId, users, accessToken);
        }

        private static void RemoveUserFromGroup(Cmdlet cmdlet, string groupName, PnPConnection connection, Guid groupId, string[] users, string accessToken)
        {
            foreach (var user in users)
            {
                var resultString = GraphHelper.Get(cmdlet, connection, $"v1.0/{GetUserGraphUrlForUPN(user)}?$select=Id", accessToken);
                var resultElement = JsonSerializer.Deserialize<JsonElement>(resultString);
                if (resultElement.TryGetProperty("id", out JsonElement idElement))
                {
                    GraphHelper.Delete(cmdlet, connection, $"v1.0/groups/{groupId}/{groupName}/{idElement.GetString()}/$ref", accessToken);
                }
            }
        }

        internal static void RemoveGroup(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            GraphHelper.Delete(cmdlet, connection, $"v1.0/groups/{groupId}", accessToken);
        }

        internal static IEnumerable<Microsoft365User> GetOwners(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            return GetGroupMembers(cmdlet, "owners", connection, groupId, accessToken);
        }

        internal static IEnumerable<Microsoft365User> GetMembers(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            return GetGroupMembers(cmdlet, "members", connection, groupId, accessToken);
        }

        private static IEnumerable<Microsoft365User> GetGroupMembers(Cmdlet cmdlet, string userType, PnPConnection connection, Guid groupId, string accessToken)
        {
            var results = GraphHelper.GetResultCollection<Microsoft365User>(cmdlet, connection, $"v1.0/groups/{groupId}/{userType}?$select=*", accessToken);
            return results;
        }

        private static Microsoft365Group GetGroupExchangeOnlineSettings(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            var results = GraphHelper.Get<Microsoft365Group>(cmdlet, connection, $"v1.0/groups/{groupId}?$select=allowExternalSenders,isSubscribedByMail,autoSubscribeNewMembers", accessToken);
            return results;
        }

        private static Microsoft365Group GetGroupSensitivityLabels(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            var results = GraphHelper.Get<Microsoft365Group>(cmdlet, connection, $"v1.0/groups/{groupId}?$select=assignedLabels", accessToken);
            return results;
        }

        internal static void ClearMembers(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            var members = GetMembers(cmdlet, connection, groupId, accessToken);

            foreach (var member in members)
            {
                GraphHelper.Delete(cmdlet, connection, $"v1.0/groups/{groupId}/members/{member.Id}/$ref", accessToken);
            }
        }

        internal static void ClearOwnersAsync(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken)
        {
            var members = GetOwners(cmdlet, connection, groupId, accessToken);

            foreach (var member in members)
            {
                GraphHelper.Delete(cmdlet, connection, $"v1.0/groups/{groupId}/owners/{member.Id}/$ref", accessToken);
            }
        }

        internal static void UpdateOwners(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken, string[] owners)
        {
            var existingOwners = GetOwners(cmdlet, connection, groupId, accessToken);
            foreach (var owner in owners)
            {
                if (existingOwners.FirstOrDefault(o => o.UserPrincipalName == owner) == null)
                {
                    AddOwners(cmdlet, connection, groupId, new string[] { owner }, accessToken, false);
                }
            }
            foreach (var existingOwner in existingOwners)
            {
                if (!owners.Contains(existingOwner.UserPrincipalName))
                {
                    GraphHelper.Delete(cmdlet, connection, $"v1.0/groups/{groupId}/owners/{existingOwner.Id}/$ref", accessToken);
                }
            }
        }

        internal static void UpdateMembersAsync(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken, string[] members)
        {
            var existingMembers = GetMembers(cmdlet, connection, groupId, accessToken);
            foreach (var member in members)
            {
                if (existingMembers.FirstOrDefault(o => o.UserPrincipalName == member) == null)
                {
                    AddMembers(cmdlet, connection, groupId, new string[] { member }, accessToken, false);
                }
            }
            foreach (var existingMember in existingMembers)
            {
                if (!members.Contains(existingMember.UserPrincipalName))
                {
                    GraphHelper.Delete(cmdlet, connection, $"v1.0/groups/{groupId}/members/{existingMember.Id}/$ref", accessToken);
                }
            }
        }

        internal static Microsoft365Group UpdateExchangeOnlineSetting(Cmdlet cmdlet, PnPConnection connection, Guid groupId, string accessToken, Microsoft365Group group)
        {
            var patchData = new
            {
                group.AllowExternalSenders,
                group.AutoSubscribeNewMembers
            };

            var result = GraphHelper.Patch(cmdlet, connection, accessToken, $"v1.0/groups/{groupId}", patchData);

            group.AllowExternalSenders = result.AllowExternalSenders;
            group.AutoSubscribeNewMembers = result.AutoSubscribeNewMembers;

            return group;
        }        

        internal static Dictionary<string, string> GetSiteUrlBatched(Cmdlet cmdlet, PnPConnection connection, string accessToken, string[] groupIds)
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
            var result = GraphHelper.Post<GraphBatchResponse>(cmdlet, connection, "v1.0/$batch", stringContent, accessToken);
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

        internal static Dictionary<string, string> GetUserIdsBatched(Cmdlet cmdlet, PnPConnection connection, string accessToken, string[] userPrincipalNames)
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
            var result = GraphHelper.Post<GraphBatchResponse>(cmdlet, connection, "v1.0/$batch", stringContent, accessToken);
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

        internal static string[] GetUsersDataBindValue(Cmdlet cmdlet, PnPConnection connection, string accessToken, string[] users)
        {
            var userids = GetUserIdsBatched(cmdlet, connection, accessToken, users);
            if (userids.Any())
            {
                return userids.Select(u => $"https://{connection.GraphEndPoint}/v1.0/users/{u.Value}").ToArray();
            }
            return null;
        }

        internal static Microsoft365Group Create(Cmdlet cmdlet, PnPConnection connection, string accessToken, Microsoft365Group group, bool createTeam, string logoPath, string[] owners, string[] members, bool? hideFromAddressLists, bool? hideFromOutlookClients, List<string> sensitivityLabels)
        {
            if (owners != null && owners.Length > 0)
            {
                group.OwnersODataBind = GetUsersDataBindValue(cmdlet, connection, accessToken, owners);
            }

            if (members != null && members.Length > 0)
            {
                group.MembersODataBind = GetUsersDataBindValue(cmdlet, connection, accessToken, members);
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

            var newGroup = GraphHelper.Post(cmdlet, connection, "v1.0/groups", group, accessToken);

            if (hideFromAddressLists.HasValue || hideFromOutlookClients.HasValue)
            {
                SetVisibility(cmdlet, connection, accessToken, newGroup.Id.Value, hideFromAddressLists, hideFromOutlookClients);
            }

            if (!string.IsNullOrEmpty(logoPath))
            {
                UploadLogoAsync(cmdlet, connection, accessToken, newGroup.Id.Value, logoPath);
            }

            if (createTeam)
            {
                CreateTeam(cmdlet, connection, accessToken, newGroup.Id.Value);
            }

            return newGroup;
        }

        internal static void UploadLogoAsync(Cmdlet cmdlet, PnPConnection connection, string accessToken, Guid groupId, string logoPath)
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
                    var responseMessage = GraphHelper.Put(cmdlet, connection, $"/v1.0/groups/{groupId}/photo/$value", accessToken, content);
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

        internal static void CreateTeam(Cmdlet cmdlet, PnPConnection connection, string accessToken, Guid groupId)
        {
            var createTeamEndPoint = $"v1.0/groups/{groupId}/team";
            bool wait = true;
            var iterations = 0;

            while (wait)
            {
                iterations++;
                try
                {
                    var teamId = GraphHelper.Put<object>(cmdlet, connection, createTeamEndPoint, new { }, accessToken);
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

        internal static void Renew(Cmdlet cmdlet, PnPConnection connection, string accessToken, Guid groupId)
        {
            GraphHelper.Post(cmdlet, connection, $"v1.0/groups/{groupId}/renew", new { }, accessToken);
        }

        internal static Microsoft365Group Update(Cmdlet cmdlet, PnPConnection connection, string accessToken, Microsoft365Group group)
        {
            return GraphHelper.Patch(cmdlet, connection, accessToken, $"v1.0/groups/{group.Id}", group);
        }
        
        internal static void SetVisibility(Cmdlet cmdlet, PnPConnection connection, string accessToken, Guid groupId, bool? hideFromAddressLists, bool? hideFromOutlookClients)
        {
            var patchData = new
            {
                hideFromAddressLists = hideFromAddressLists,
                hideFromOutlookClients = hideFromOutlookClients
            };

            var retry = true;
            var iteration = 0;
            while (retry)
            {
                try
                {
                    GraphHelper.Patch<dynamic>(cmdlet, connection, accessToken, $"v1.0/groups/{groupId}", patchData);
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

        internal static Microsoft365GroupSettingValueCollection GetGroupSettings(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var result = GraphHelper.Get<Microsoft365GroupSettingValueCollection>(cmdlet, connection, "v1.0/groupSettings", accessToken, propertyNameCaseInsensitive: true);
            return result;
        }

        internal static Microsoft365GroupSettingValueCollection GetGroupTenantSettings(Cmdlet cmdlet, PnPConnection connection, string accessToken, string groupSettingId)
        {
            var result = GraphHelper.Get<Microsoft365GroupSettingValueCollection>(cmdlet, connection, $"v1.0/groupSettings/{groupSettingId}", accessToken, propertyNameCaseInsensitive: true);
            return result;
        }

        internal static Microsoft365GroupSettingValueCollection GetGroupSettings(Cmdlet cmdlet, PnPConnection connection, string accessToken, string groupSettingId,string groupId)
        {
            var result = GraphHelper.Get<Microsoft365GroupSettingValueCollection>(cmdlet, connection, $"v1.0/groups/{groupId}/settings/{groupSettingId}", accessToken, propertyNameCaseInsensitive: true);
            return result;
        }

        internal static Microsoft365GroupSetting CreateGroupSetting(Cmdlet cmdlet, PnPConnection connection, string accessToken, dynamic groupSettingObject)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(groupSettingObject));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = GraphHelper.Post<Microsoft365GroupSetting>(cmdlet, connection, "v1.0/groupSettings", stringContent, accessToken, propertyNameCaseInsensitive: true);
            return result;
        }

        internal static Microsoft365GroupSetting CreateGroupSetting(Cmdlet cmdlet, PnPConnection connection, string accessToken, string groupId, dynamic groupSettingObject)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(groupSettingObject));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = GraphHelper.Post<Microsoft365GroupSetting>(cmdlet, connection, $"v1.0/groups/{groupId}/settings", stringContent, accessToken, propertyNameCaseInsensitive: true);
            return result;
        }

        internal static void UpdateGroupSetting(Cmdlet cmdlet, PnPConnection connection, string accessToken, string id, dynamic groupSettingObject)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(groupSettingObject));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            GraphHelper.Patch(cmdlet, connection, accessToken, stringContent, $"v1.0/groupSettings/{id}");
        }

        internal static void UpdateGroupSetting(Cmdlet cmdlet, PnPConnection connection, string accessToken, string id, string groupId, dynamic groupSettingObject)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(groupSettingObject));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            GraphHelper.Patch(cmdlet, connection, accessToken, stringContent, $"v1.0/groups/{groupId}/settings/{id}");
        }

        internal static void RemoveGroupSetting(Cmdlet cmdlet, PnPConnection connection, string accessToken, string id)
        {
            GraphHelper.Delete(cmdlet, connection, $"v1.0/groupSettings/{id}", accessToken);
        }

        internal static void RemoveGroupSetting(Cmdlet cmdlet, PnPConnection connection, string accessToken, string id, string groupId)
        {
            GraphHelper.Delete(cmdlet, connection, $"v1.0/groups/{groupId}/settings/{id}", accessToken);
        }

        internal static Microsoft365GroupTemplateSettingValueCollection GetGroupTemplateSettings(Cmdlet cmdlet, PnPConnection connection, string accessToken)
        {
            var result = GraphHelper.Get<Microsoft365GroupTemplateSettingValueCollection>(cmdlet, connection, "v1.0/groupSettingTemplates", accessToken, propertyNameCaseInsensitive: true);
            return result;
        }

        internal static Microsoft365GroupSettingTemplate GetGroupTemplateSettings(Cmdlet cmdlet, PnPConnection connection, string accessToken, string id)
        {
            var result = GraphHelper.Get<Microsoft365GroupSettingTemplate>(cmdlet, connection, $"v1.0/groupSettingTemplates/{id}", accessToken, propertyNameCaseInsensitive: true);
            return result;
        }

        internal static void SetSensitivityLabels(Cmdlet cmdlet, PnPConnection connection, string accessToken, Guid groupId, List<AssignedLabels> assignedLabels)
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
                    GraphHelper.Patch<dynamic>(cmdlet, connection, accessToken, $"v1.0/groups/{groupId}", patchData);
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
        
        internal static HttpResponseMessage DeletePhoto(Cmdlet cmdlet, PnPConnection connection, string accessToken, Guid groupId)
        {
            return GraphHelper.Delete(cmdlet, connection, $"v1.0/groups/{groupId}/photo/$value", accessToken);
        }
    }
}