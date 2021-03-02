using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Text.Json.Serialization;
using System.Text.Json;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class Microsoft365GroupsUtility
    {
        internal static async Task<IEnumerable<Microsoft365Group>> GetGroupsAsync(HttpClient httpClient, string accessToken, bool includeSiteUrl)
        {
            var items = new List<Microsoft365Group>();
            var result = await GraphHelper.GetAsync<RestResultCollection<Microsoft365Group>>(httpClient, "v1.0/groups", accessToken);
            if (result != null && result.Items.Any())
            {
                items.AddRange(result.Items);
                while (!string.IsNullOrEmpty(result.NextLink))
                {
                    result = await GraphHelper.GetAsync<RestResultCollection<Microsoft365Group>>(httpClient, result.NextLink, accessToken);

                    if (result != null && result.Items.Any())
                    {
                        items.AddRange(result.Items);
                    }
                }
            }
            if (includeSiteUrl)
            {
                var chunks = BatchUtility.Chunk(items.Select(g => g.Id.ToString()), 20);
                foreach (var chunk in chunks)
                {
                    var results = await BatchUtility.GetPropertyBatchedAsync(httpClient, accessToken, chunk.ToArray(), "/groups/{0}/sites/root","webUrl");
                    //var results = await GetSiteUrlBatchedAsync(httpClient, accessToken, chunk.ToArray());
                    foreach (var batchResult in results)
                    {
                        items.First(i => i.Id.ToString() == batchResult.Key).SiteUrl = batchResult.Value;
                    }
                }
            }
            return items;
        }

        internal static async Task<Microsoft365Group> GetGroupAsync(HttpClient httpClient, Guid groupId, string accessToken, bool includeSiteUrl)
        {
            return await GraphHelper.GetAsync<Microsoft365Group>(httpClient, $"v1.0/groups/{groupId}", accessToken);
        }

        internal static async Task<Microsoft365Group> GetGroupAsync(HttpClient httpClient, string displayName, string accessToken, bool includeSiteUrl)
        {
            var results = await GraphHelper.GetAsync<RestResultCollection<Microsoft365Group>>(httpClient, $"v1.0/groups?$filter=displayName eq '{displayName}' or mailNickName eq '{displayName}'", accessToken);
            if (results != null && results.Items.Any())
            {
                return results.Items.First();
            }
            return null;
        }

        internal static async Task<Microsoft365Group> GetDeletedGroupAsync(HttpClient httpClient, Guid groupId, string accessToken)
        {
            return await GraphHelper.GetAsync<Microsoft365Group>(httpClient, $"v1.0/directory/deleteditems/microsoft.graph.group/{groupId}", accessToken);
        }

        internal static async Task<Microsoft365Group> GetDeletedGroupAsync(HttpClient httpClient, string groupName, string accessToken)
        {
            var results = await GraphHelper.GetAsync<RestResultCollection<Microsoft365Group>>(httpClient, $"v1.0/directory/deleteditems/microsoft.graph.group?$filter=displayName eq '{groupName}' or mailNickname eq '{groupName}'", accessToken);
            if (results != null && results.Items.Any())
            {
                return results.Items.First();
            }
            return null;
        }

        internal static async Task<IEnumerable<Microsoft365Group>> GetDeletedGroupsAsync(HttpClient httpClient, string accessToken)
        {
            var items = new List<Microsoft365Group>();
            var result = await GraphHelper.GetAsync<RestResultCollection<Microsoft365Group>>(httpClient, "v1.0/directory/deleteditems/microsoft.graph.group", accessToken);
            if (result != null && result.Items.Any())
            {
                items.AddRange(result.Items);
                while (!string.IsNullOrEmpty(result.NextLink))
                {
                    result = await GraphHelper.GetAsync<RestResultCollection<Microsoft365Group>>(httpClient, result.NextLink, accessToken);

                    if (result != null && result.Items.Any())
                    {
                        items.AddRange(result.Items);
                    }
                }
            }
            return items;
        }

        internal static async Task<Microsoft365Group> RestoreDeletedGroupAsync(HttpClient httpClient, Guid groupId, string accessToken)
        {
            return await GraphHelper.PostAsync<Microsoft365Group>(httpClient, $"v1.0/directory/deleteditems/microsoft.graph.group/{groupId}/restore", accessToken);
        }

        internal static async Task PermanentlyDeleteDeletedGroupAsync(HttpClient httpClient, Guid groupId, string accessToken)
        {
            await GraphHelper.DeleteAsync(httpClient, $"v1.0/directory/deleteditems/microsoft.graph.group/{groupId}", accessToken);
        }

        internal static async Task AddOwnersAsync(HttpClient httpClient, Guid groupId, string[] users, string accessToken, bool removeExisting)
        {
            await AddUsersToGroupAsync("owners", httpClient, groupId, users, accessToken, removeExisting);
        }

        internal static async Task AddMembersAsync(HttpClient httpClient, Guid groupId, string[] users, string accessToken, bool removeExisting)
        {
            await AddUsersToGroupAsync("members", httpClient, groupId, users, accessToken, removeExisting);
        }

        private static async Task AddUsersToGroupAsync(string groupName, HttpClient httpClient, Guid groupId, string[] users, string accessToken, bool removeExisting)
        {
            foreach (var user in users)
            {
                var userIdResult = await GraphHelper.GetAsync(httpClient, $"v1.0/users/{user}?$select=Id", accessToken);
                var resultElement = JsonSerializer.Deserialize<JsonElement>(userIdResult);
                if (resultElement.TryGetProperty("id", out JsonElement idProperty))
                {

                    var postData = new Dictionary<string, string>() {
                    {
                        "@odata.id", $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/users/{idProperty.GetString()}"
                    }
                };
                    var stringContent = new StringContent(JsonSerializer.Serialize(postData));
                    stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    await GraphHelper.PostAsync(httpClient, $"v1.0/groups/{groupId}/{groupName}/$ref", accessToken, stringContent);
                }
            }
        }

        internal static async Task RemoveOwnersAsync(HttpClient httpClient, Guid groupId, string[] users, string accessToken)
        {
            await RemoveUserFromGroupAsync("owners", httpClient, groupId, users, accessToken);
        }

        internal static async Task RemoveMembersAsync(HttpClient httpClient, Guid groupId, string[] users, string accessToken)
        {
            await RemoveUserFromGroupAsync("members", httpClient, groupId, users, accessToken);
        }

        private static async Task RemoveUserFromGroupAsync(string groupName, HttpClient httpClient, Guid groupId, string[] users, string accessToken)
        {
            foreach (var user in users)
            {
                var resultString = await GraphHelper.GetAsync(httpClient, $"v1.0/users/{user}?$select=Id", accessToken);
                var resultElement = JsonSerializer.Deserialize<JsonElement>(resultString);
                if (resultElement.TryGetProperty("id", out JsonElement idElement))
                {
                    await GraphHelper.DeleteAsync(httpClient, $"v1.0/groups/{groupId}/{groupName}/{idElement.GetString()}/$ref", accessToken);
                }
            }
        }

        internal static async Task RemoveGroupAsync(HttpClient httpClient, Guid groupId, string accessToken)
        {
            await GraphHelper.DeleteAsync(httpClient, $"v1.0/groups/{groupId}", accessToken);
        }

        internal static async Task<IEnumerable<Microsoft365User>> GetOwnersAsync(HttpClient httpClient, Guid groupId, string accessToken)
        {
            return await GetGroupMembers("owners", httpClient, groupId, accessToken);
        }

        internal static async Task<IEnumerable<Microsoft365User>> GetMembersAsync(HttpClient httpClient, Guid groupId, string accessToken)
        {
            return await GetGroupMembers("members", httpClient, groupId, accessToken);
        }

        private static async Task<IEnumerable<Microsoft365User>> GetGroupMembers(string groupName, HttpClient httpClient, Guid groupId, string accessToken)
        {
            var returnValue = new List<Microsoft365User>();
            var results = await GraphHelper.GetAsync<RestResultCollection<Microsoft365User>>(httpClient, $"v1.0/groups/{groupId}/{groupName}", accessToken);
            if (results != null && results.Items.Any())
            {
                returnValue.AddRange(results.Items);
                while (!string.IsNullOrEmpty(results.NextLink))
                {
                    results = await GraphHelper.GetAsync<RestResultCollection<Microsoft365User>>(httpClient, results.NextLink, accessToken);
                    if (results != null && results.Items.Any())
                    {
                        returnValue.AddRange(results.Items);
                    }
                }
            }
            return returnValue;
        }

        internal static async Task ClearMembersAsync(HttpClient httpClient, Guid groupId, string accessToken)
        {
            var members = await GetMembersAsync(httpClient, groupId, accessToken);

            foreach (var member in members)
            {
                await GraphHelper.DeleteAsync(httpClient, $"v1.0/groups/{groupId}/members/{member.Id}/$ref", accessToken);
            }
        }

        internal static async Task ClearOwnersAsync(HttpClient httpClient, Guid groupId, string accessToken)
        {
            var members = await GetOwnersAsync(httpClient, groupId, accessToken);

            foreach (var member in members)
            {
                await GraphHelper.DeleteAsync(httpClient, $"v1.0/groups/{groupId}/owners/{member.Id}/$ref", accessToken);
            }
        }

        internal static async Task UpdateOwnersAsync(HttpClient httpClient, Guid groupId, string accessToken, string[] owners)
        {
            var existingOwners = await GetOwnersAsync(httpClient, groupId, accessToken);
            foreach (var owner in owners)
            {
                if (existingOwners.FirstOrDefault(o => o.UserPrincipalName == owner) == null)
                {
                    await AddOwnersAsync(httpClient, groupId, new string[] { owner }, accessToken, false);
                }
            }
            foreach (var existingOwner in existingOwners)
            {
                if (!owners.Contains(existingOwner.UserPrincipalName))
                {
                    await GraphHelper.DeleteAsync(httpClient, $"v1.0/groups/{groupId}/owners/{existingOwner.Id}/$ref", accessToken);
                }
            }
        }

         internal static async Task UpdateMembersAsync(HttpClient httpClient, Guid groupId, string accessToken, string[] members)
        {
            var existingMembers = await GetMembersAsync(httpClient, groupId, accessToken);
            foreach (var member in members)
            {
                if (existingMembers.FirstOrDefault(o => o.UserPrincipalName == member) == null)
                {
                    await AddMembersAsync(httpClient, groupId, new string[] { member }, accessToken, false);
                }
            }
            foreach (var existingMember in existingMembers)
            {
                if (!members.Contains(existingMember.UserPrincipalName))
                {
                    await GraphHelper.DeleteAsync(httpClient, $"v1.0/groups/{groupId}/members/{existingMember.Id}/$ref", accessToken);
                }
            }
        }

        internal static async Task<Dictionary<string, string>> GetSiteUrlBatchedAsync(HttpClient httpClient, string accessToken, string[] groupIds)
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
            var result = await GraphHelper.PostAsync<GraphBatchResponse>(httpClient, "v1.0/$batch", stringContent, accessToken);
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

        internal static async Task<Dictionary<string, string>> GetUserIdsBatched(HttpClient httpClient, string accessToken, string[] userPrincipalNames)
        {
            Dictionary<string, string> returnValue = new Dictionary<string, string>();

            Dictionary<string, string> requests = new Dictionary<string, string>();
            var batch = new GraphBatch();
            int id = 0;
            foreach (var upn in userPrincipalNames)
            {
                id++;
                batch.Requests.Add(new GraphBatchRequest() { Id = id.ToString(), Method = "GET", Url = $"/users/{upn}?$select=Id" });
                requests.Add(id.ToString(), upn);
            }
            var stringContent = new StringContent(JsonSerializer.Serialize(batch));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = await GraphHelper.PostAsync<GraphBatchResponse>(httpClient, "v1.0/$batch", stringContent, accessToken);
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

        internal static async Task<string[]> GetUsersDataBindValueAsync(HttpClient httpClient, string accessToken, string[] users)
        {
            var userids = await GetUserIdsBatched(httpClient, accessToken, users);
            if (userids.Any())
            {
                return userids.Select(u => $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/users/{u.Value}").ToArray();
            }
            return null;
        }

        internal static async Task<Microsoft365Group> CreateAsync(HttpClient httpClient, string accessToken, Microsoft365Group group, bool createTeam, string logoPath, string[] owners, string[] members, bool? hideFromAddressLists, bool? hideFromOutlookClients)
        {
            if (owners != null && owners.Length > 0)
            {
                group.OwnersODataBind = await GetUsersDataBindValueAsync(httpClient, accessToken, owners);
            }

            if (members != null && members.Length > 0)
            {
                group.MembersODataBind = await GetUsersDataBindValueAsync(httpClient, accessToken, members);
            }

            var newGroup = await GraphHelper.PostAsync(httpClient, "v1.0/groups", group, accessToken);

            if (hideFromAddressLists.HasValue || hideFromOutlookClients.HasValue)
            {
                await SetVisibilityAsync(httpClient, accessToken, newGroup.Id.Value, hideFromAddressLists, hideFromOutlookClients);
            }

            if (!string.IsNullOrEmpty(logoPath))
            {
                await UploadLogoAsync(httpClient, accessToken, newGroup.Id.Value, logoPath);
            }

            if (createTeam)
            {
                await CreateTeamAsync(httpClient, accessToken, newGroup.Id.Value);
            }

            return newGroup;
        }

        internal static async Task UploadLogoAsync(HttpClient httpClient, string accessToken, Guid groupId, string logoPath)
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
                case "png":
                    {
                        contentType = "image/jpeg";
                        break;
                    }
            }
            if (!string.IsNullOrEmpty(contentType))
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                var updated = false;
                var retryCount = 10;
                while (retryCount > 0)
                {
                    var responseMessage = await GraphHelper.PutAsync(httpClient, $"/v1.0/groups/{groupId}/photo/$value", accessToken, content);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        updated = true;
                    }
                    if (!updated)
                    {
                        await Task.Delay(500 * (10 - retryCount));
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
                throw new Exception("Unrecognized image format. Supported formats are .png, .jpg and .gif");
            }
        }

        internal static async Task CreateTeamAsync(HttpClient httpClient, string accessToken, Guid groupId)
        {
            var createTeamEndPoint = $"v1.0/groups/{groupId}/team";
            bool wait = true;
            var iterations = 0;
            while (wait)
            {
                iterations++;
                try
                {
                    var teamId = await GraphHelper.PutAsync<string>(httpClient, createTeamEndPoint, string.Empty, accessToken);
                    if (!string.IsNullOrEmpty(teamId))
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
                        await Task.Delay(TimeSpan.FromSeconds(30));
                    }
                }
            }
        }

        internal static async Task RenewAsync(HttpClient httpClient, string accessToken, Guid groupId)
        {
            await GraphHelper.PostAsync(httpClient, $"v1.0/groups/{groupId}/renew", new { }, accessToken);
        }

        

        internal static async Task<Microsoft365Group> UpdateAsync(HttpClient httpClient, string accessToken, Microsoft365Group group)
        {
            return await GraphHelper.PatchAsync(httpClient, accessToken, $"v1.0/groups/{group.Id}", group);
        }

        internal static async Task SetVisibilityAsync(HttpClient httpClient, string accessToken, Guid groupId, bool? hideFromAddressLists, bool? hideFromOutlookClients)
        {
            var patchData = new
            {
                hideFromAddressLists = hideFromAddressLists,
                hideFromOutlookClients = hideFromOutlookClients
            };
            await GraphHelper.PatchAsync(httpClient, accessToken, $"v1.0/groups/{groupId}", patchData);
        }
    }
}