using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;
using TeamChannel = PnP.PowerShell.Commands.Model.Teams.TeamChannel;
using User = PnP.PowerShell.Commands.Model.Teams.User;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class TeamsUtility
    {
        private const int PageSize = 100;

        #region Team
        public static async Task<List<Group>> GetGroupsWithTeamAsync(HttpClient httpClient, string accessToken)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<Group>(httpClient, $"beta/groups?$filter=resourceProvisioningOptions/Any(x:x eq 'Team')&$select=Id,DisplayName,MailNickName,Description,Visibility&$top={PageSize}", accessToken);
            return collection.ToList();
        }

        public static async Task<Group> GetGroupWithTeamAsync(HttpClient httpClient, string accessToken, string mailNickname)
        {
            return await GraphHelper.GetAsync<Group>(httpClient, $"beta/groups?$filter=(resourceProvisioningOptions/Any(x:x eq 'Team') and mailNickname eq '{mailNickname}')&$select=Id,DisplayName,MailNickName,Description,Visibility", accessToken);
        }

        public static async Task<List<Team>> GetTeamsAsync(string accessToken, HttpClient httpClient)
        {
            List<Team> teams = new List<Team>();

            var groups = await GetGroupsWithTeamAsync(httpClient, accessToken);
            foreach (var group in groups)
            {
                Team team = await ParseTeamJsonAsync(accessToken, httpClient, group.Id);

                if (team != null)
                {
                    team.DisplayName = group.DisplayName;
                    team.MailNickname = group.MailNickname;
                    team.Visibility = group.Visibility;
                    teams.Add(team);
                }
            }
            return teams;
        }

        public static async Task<Team> GetTeamAsync(string accessToken, HttpClient httpClient, string groupId)
        {
            // get the group
            var group = await GraphHelper.GetAsync<Group>(httpClient, $"v1.0/groups/{groupId}?$select=Id,DisplayName,MailNickName,Description,Visibility", accessToken);

            Team team = await ParseTeamJsonAsync(accessToken, httpClient, group.Id);
            if (team != null)
            {
                team.DisplayName = group.DisplayName;
                team.MailNickname = group.MailNickname;
                team.Visibility = group.Visibility;
                return team;
            }
            else
            {
                return null;
            }
        }

        public static async Task<HttpResponseMessage> DeleteTeamAsync(string accessToken, HttpClient httpClient, string groupId)
        {
            return await GraphHelper.DeleteAsync(httpClient, $"v1.0/groups/{groupId}", accessToken);
        }
        public static async Task<HttpResponseMessage> CloneTeamAsync(string accessToken, HttpClient httpClient, string groupId, TeamCloneInformation teamClone)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize( new { displayName = teamClone.DisplayName , 
                    classification = teamClone.Classification , 
                    description = teamClone.Description, 
                    mailNickname= teamClone.MailNickName , 
                    visibility = teamClone.Visibility.ToString(),
                    partsToClone = String.Join(",", teamClone.PartsToClone)
            }));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await GraphHelper.PostAsync(httpClient, $"v1.0/teams/{groupId}/clone", accessToken, content);
        }
        private static async Task<Team> ParseTeamJsonAsync(string accessToken, HttpClient httpClient, string groupId)
        {
            // Get Settings
            try
            {
                var team = await GraphHelper.GetAsync<Team>(httpClient, $"v1.0/teams/{groupId}", accessToken, false, true);
                if (team != null)
                {
                    team.GroupId = groupId;
                    return team;
                }
                else
                {
                    return null;
                }
            }
            catch (ApplicationException ex)
            {
                // untested change
                if (ex.Message.StartsWith("404"))
                {
                    // no team, swallow
                }
                else
                {
                    throw;
                }
                return null;
            }
        }

        public static async Task<Team> NewTeamAsync(string accessToken, HttpClient httpClient, string groupId, string displayName, string description, string classification, string mailNickname, GroupVisibility visibility, TeamCreationInformation teamCI, string[] owners, string[] members, TeamsTemplateType templateType = TeamsTemplateType.None, TeamResourceBehaviorOptions?[] resourceBehaviorOptions = null)
        {
            Group group = null;
            Team returnTeam = null;

            // Create the Group
            if (string.IsNullOrEmpty(groupId))
            {
                group = await CreateGroupAsync(accessToken, httpClient, displayName, description, classification, mailNickname, visibility, owners, templateType, resourceBehaviorOptions);
                bool wait = true;
                int iterations = 0;
                while (wait)
                {
                    iterations++;

                    try
                    {
                        var createdGroup = await GraphHelper.GetAsync<Group>(httpClient, $"v1.0/groups/{group.Id}", accessToken);
                        if (!string.IsNullOrEmpty(createdGroup.DisplayName))
                        {
                            wait = false;
                        }
                    }
                    catch (Exception)
                    {
                        // In case of exception wait for 5 secs
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }

                    // Don't wait more than 1 minute
                    if (iterations > 12)
                    {
                        wait = false;
                    }
                }
            }
            else
            {
                group = await GraphHelper.GetAsync<Group>(httpClient, $"v1.0/groups/{groupId}", accessToken);
                if (group == null)
                {
                    throw new PSArgumentException($"Cannot find group with id {groupId}");
                }
                teamCI.Visibility = group.Visibility;
                teamCI.Description = group.Description;
            }
            if (group != null)
            {
                Team team = teamCI.ToTeam(group.Visibility);
                var retry = true;
                var iteration = 0;
                while (retry)
                {
                    try
                    {
                        var teamSettings = await GraphHelper.PutAsync(httpClient, $"v1.0/groups/{group.Id}/team", team, accessToken);
                        if (teamSettings != null)
                        {
                            returnTeam = await GetTeamAsync(accessToken, httpClient, group.Id);
                        }
                        retry = false;
                    }

                    catch (Exception)
                    {
                        await Task.Delay(5000);
                        iteration++;
                    }

                    if (iteration > 10) // don't try more than 10 times
                    {
                        retry = false;
                    }
                }

                // Construct a list of all owners and members to add
                var teamOwnersAndMembers = new List<TeamChannelMember>();
                if (owners != null && owners.Length > 0)
                {
                    foreach (var owner in owners)
                    {
                        teamOwnersAndMembers.Add(new TeamChannelMember { Roles = new List<string> { "owner" }, UserIdentifier = $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/users('{owner}')" });
                    }
                }

                if (members != null && members.Length > 0)
                {
                    foreach (var member in members)
                    {
                        teamOwnersAndMembers.Add(new TeamChannelMember { Roles = new List<string>(), UserIdentifier = $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/users('{member}')" });
                    }                    
                }

                if (teamOwnersAndMembers.Count > 0)
                {
                    var ownersAndMembers = BatchUtility.Chunk(teamOwnersAndMembers, 200);
                    foreach (var chunk in ownersAndMembers)
                    {
                        await GraphHelper.PostAsync(httpClient, $"v1.0/teams/{group.Id}/members/add", new { values = chunk.ToList() }, accessToken);
                    }
                }

            }
            return returnTeam;
        }

        internal static string GetUserGraphUrlForUPN(string upn)
        {

            var escapedUpn = upn.Replace("#", "%23");

            if (escapedUpn.StartsWith("$")) return $"users('{escapedUpn}')";

            return $"users/{escapedUpn}";
        }

        private static async Task<Group> CreateGroupAsync(string accessToken, HttpClient httpClient, string displayName, string description, string classification, string mailNickname, GroupVisibility visibility, string[] owners, TeamsTemplateType templateType = TeamsTemplateType.None, TeamResourceBehaviorOptions?[] resourceBehaviorOptions = null)
        {
            // When creating a group, we always need an owner, thus we'll try to define it from the passed in owners array
            string ownerId = null;
            if (owners != null && owners.Length > 0)
            {
                // Owner(s) have been provided, use the first owner as the initial owner. The other owners will be added later.
                var user = await GraphHelper.GetAsync<User>(httpClient, $"v1.0/{GetUserGraphUrlForUPN(owners[0])}?$select=Id", accessToken);

                if (user != null)
                {
                    // User Id of the first owner has been found
                    ownerId = user.Id;
                }
                else
                {
                    // Unable to find the owner by its user principal name, try looking for it on its email address
                    var collection = await GraphHelper.GetResultCollectionAsync<User>(httpClient, $"v1.0/users?$filter=mail eq '{owners[0]}'&$select=Id", accessToken);
                    if (collection != null && collection.Any())
                    {
                        // User found on its email address
                        ownerId = collection.First().Id;
                    }
                }
            }

            // Check if by now we've identified a user Id to become the owner
            if (!string.IsNullOrEmpty(ownerId))
            {
                var contextSettings = PnPConnection.Current.Context.GetContextSettings();

                // Still no owner identified, see if we can make the current user executing this cmdlet the owner
                if (contextSettings.Type != Framework.Utilities.Context.ClientContextType.AzureADCertificate)
                {
                    // A delegate context is available, make the user part of the delegate token the owner
                    var user = await GraphHelper.GetAsync<User>(httpClient, "v1.0/me?$select=Id", accessToken);

                    if (user != null)
                    {
                        // User executing the cmdlet will become the owner
                        ownerId = user.Id;
                    }
                }
            }

            // Construct the new group
            Group group = new Group
            {
                DisplayName = displayName,
                Description = description,
                Classification = classification,
                MailEnabled = true,
                MailNickname = mailNickname ?? await CreateAliasAsync(httpClient, accessToken),
                GroupTypes = new List<string>() { "Unified" },
                SecurityEnabled = false,
                Visibility = visibility == GroupVisibility.NotSpecified ? GroupVisibility.Private : visibility
            };

            // Check if we managed to define an owner for the group. If not, we'll revert to not providing an owner, which will mean that the app principal will become the owner of the Group
            if (!string.IsNullOrEmpty(ownerId))
            {
                group.Owners = new List<string>() { $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/users/{ownerId}" };
                group.Members = new List<string>() { $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/users/{ownerId}" };
            }

            if (resourceBehaviorOptions != null && resourceBehaviorOptions.Length > 0)
            {
                var teamResourceBehaviorOptionsValue = new List<string>();
                for (int i = 0; i < resourceBehaviorOptions.Length; i++)
                {
                    teamResourceBehaviorOptionsValue.Add(resourceBehaviorOptions[i].ToString());
                }
                group.ResourceBehaviorOptions = teamResourceBehaviorOptionsValue;
            }

            switch (templateType)
            {
                case TeamsTemplateType.EDU_Class:
                    group.Visibility = GroupVisibility.HiddenMembership;
                    group.CreationOptions = new List<string> { "ExchangeProvisioningFlags:461", "classAssignments" };
                    group.EducationObjectType = "Section";
                    break;

                case TeamsTemplateType.EDU_PLC:
                    group.CreationOptions = new List<string> { "PLC" };
                    break;

                default:
                    group.CreationOptions = new List<string> { "ExchangeProvisioningFlags:3552" };
                    break;
            }
            try
            {
                return await GraphHelper.PostAsync<Group>(httpClient, "v1.0/groups", group, accessToken);
            }
            catch (GraphException ex)
            {
                if (ex.Error.Message.Contains("extension_fe2174665583431c953114ff7268b7b3_Education_ObjectType"))
                {
                    throw new PSInvalidOperationException("Invalid EDU license type");
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task<string> CreateAliasAsync(HttpClient httpClient, string accessToken)
        {
            var guid = Guid.NewGuid().ToString();
            var teamName = string.Empty;
            // check if the group exists
            do
            {
                var teamNameTemp = $"msteams_{guid.Substring(0, 8)}{guid.Substring(9, 4)}";
                var collection = await GraphHelper.GetAsync<RestResultCollection<Group>>(httpClient, $"v1.0/groups?$filter=groupTypes/any(c:c+eq+'Unified') and (mailNickname eq '{teamNameTemp}')", accessToken);
                if (collection != null)
                {
                    if (!collection.Items.Any()) teamName = teamNameTemp;
                }

            } while (teamName == string.Empty);
            return teamName;
        }

        public static async Task<Team> UpdateTeamAsync(HttpClient httpClient, string accessToken, string groupId, Team team)
        {
            return await GraphHelper.PatchAsync<Team>(httpClient, accessToken, $"v1.0/teams/{groupId}", team);
        }

        public static async Task<Group> UpdateGroupAsync(HttpClient httpClient, string accessToken, string groupId, Group group)
        {
            return await GraphHelper.PatchAsync<Group>(httpClient, accessToken, $"v1.0/groups/{groupId}", group);
        }

        public static async Task SetTeamPictureAsync(HttpClient httpClient, string accessToken, string groupId, byte[] bytes, string contentType)
        {
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            await GraphHelper.PutAsync<string>(httpClient, $"v1.0/groups/{groupId}/photo/$value", accessToken, byteArrayContent);
        }

        public static async Task<HttpResponseMessage> SetTeamArchivedStateAsync(HttpClient httpClient, string accessToken, string groupId, bool archived, bool? setSiteReadOnly)
        {
            if (archived)
            {
                StringContent content = new StringContent(JsonSerializer.Serialize(setSiteReadOnly.HasValue ? new { shouldSetSpoSiteReadOnlyForMembers = setSiteReadOnly } : null));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return await GraphHelper.PostAsync(httpClient, $"v1.0/teams/{groupId}/archive", accessToken, content);
            }
            else
            {
                StringContent content = new StringContent("");
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return await GraphHelper.PostAsync(httpClient, $"v1.0/teams/{groupId}/unarchive", accessToken, content);
            }
        }
        #endregion

        #region Users
        public static async Task AddUserAsync(HttpClient httpClient, string accessToken, string groupId, string upn, string role)
        {
            var userIdResult = await GraphHelper.GetAsync(httpClient, $"v1.0/{GetUserGraphUrlForUPN(upn)}?$select=Id", accessToken);
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

                await GraphHelper.PostAsync(httpClient, $"v1.0/groups/{groupId}/{role.ToLower()}s/$ref", accessToken, stringContent);
            }
        }

        public static async Task AddUsersAsync(HttpClient httpClient, string accessToken, string groupId, string[] upn, string role)
        {
            var teamChannelMember = new List<TeamChannelMember>();
            if(upn != null && upn.Length > 0)
            {
                foreach (var user in upn)
                {
                    teamChannelMember.Add(new TeamChannelMember() { Roles = new List<string> { role }, UserIdentifier = $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/users('{user}')" });
                }
                if (teamChannelMember.Count > 0)
                {
                    var chunks = BatchUtility.Chunk(teamChannelMember, 200);
                    foreach (var chunk in chunks.ToList())
                    {
                        await GraphHelper.PostAsync(httpClient, $"v1.0/teams/{groupId}/members/add", new { values = chunk.ToList() }, accessToken);
                    }
                }
            }            
        }

        public static async Task<List<User>> GetUsersAsync(HttpClient httpClient, string accessToken, string groupId, string role)
        {
            var selectedRole = role != null ? role.ToLower() : null;
            var owners = new List<User>();
            var guests = new List<User>();
            var members = new List<User>();
            if (selectedRole != "guest")
            {
                owners = (await GraphHelper.GetResultCollectionAsync<User>(httpClient, $"v1.0/groups/{groupId}/owners?$select=Id,displayName,userPrincipalName,userType", accessToken)).Select(t => new User()
                {
                    Id = t.Id,
                    DisplayName = t.DisplayName,
                    UserPrincipalName = t.UserPrincipalName,
                    UserType = "Owner"
                }).ToList();
            }
            if (selectedRole != "owner")
            {
                var users = (await GraphHelper.GetResultCollectionAsync<User>(httpClient, $"v1.0/groups/{groupId}/members?$select=Id,displayName,userPrincipalName,userType", accessToken));
                HashSet<string> hashSet = new HashSet<string>(owners.Select(u => u.Id));
                foreach (var user in users)
                {
                    if (!hashSet.Contains(user.Id))
                    {
                        if (user.UserType != null && user.UserType.ToLower().Equals("guest"))
                        {
                            guests.Add(new User() { DisplayName = user.DisplayName, Id = user.Id, UserPrincipalName = user.UserPrincipalName, UserType = "Guest" });
                        }
                        else
                        {
                            members.Add(new User() { DisplayName = user.DisplayName, Id = user.Id, UserPrincipalName = user.UserPrincipalName, UserType = "Member" });
                        }
                    }
                }
            }
            var finalList = new List<User>();
            if (string.IsNullOrEmpty(selectedRole))
            {
                finalList.AddRange(owners);
                finalList.AddRange(members);
                finalList.AddRange(guests);
            }
            else if (selectedRole == "owner")
            {
                finalList.AddRange(owners);
            }
            else if (selectedRole == "member")
            {
                finalList.AddRange(members);
            }
            else if (selectedRole == "guest")
            {
                finalList.AddRange(guests);
            }
            return finalList;
        }

        public static async Task<IEnumerable<User>> GetUsersAsync(HttpClient httpClient, string accessToken, string groupId, string channelId, string role)
        {
            List<User> users = new List<User>();
            var selectedRole = role != null ? role.ToLower() : null;

            var collection = await GraphHelper.GetResultCollectionAsync<TeamChannelMember>(httpClient, $"v1.0/teams/{groupId}/channels/{channelId}/members", accessToken);
            if (collection != null && collection.Any())
            {
                users.AddRange(collection.Select(m => new User() { DisplayName = m.DisplayName, Id = m.UserId, UserPrincipalName = m.email, UserType = m.Roles.Count > 0 ? m.Roles[0].ToLower() : "" }));
            }

            if (selectedRole != null)
            {
                return users.Where(u => u.UserType == selectedRole);
            }
            else
            {
                return users;
            }
        }

        public static async Task DeleteUserAsync(HttpClient httpClient, string accessToken, string groupId, string upn, string role)
        {
            var user = await GraphHelper.GetAsync<User>(httpClient, $"v1.0/{GetUserGraphUrlForUPN(upn)}?$select=Id", accessToken);
            if (user != null)
            {
                // check if the user is an owner
                var owners = await GraphHelper.GetResultCollectionAsync<User>(httpClient, $"v1.0/groups/{groupId}/owners?$select=Id", accessToken);
                if (owners.Any() && owners.FirstOrDefault(u => u.Id.Equals(user.Id, StringComparison.OrdinalIgnoreCase)) != null)
                {
                    if (owners.Count() == 1)
                    {
                        throw new PSInvalidOperationException("Last owner cannot be removed");
                    }
                    await GraphHelper.DeleteAsync(httpClient, $"v1.0/groups/{groupId}/owners/{user.Id}/$ref", accessToken);
                }
                if (!role.Equals("owner", StringComparison.OrdinalIgnoreCase))
                {
                    await GraphHelper.DeleteAsync(httpClient, $"v1.0/groups/{groupId}/members/{user.Id}/$ref", accessToken);
                }
            }
        }

        public static async Task<List<TeamUser>> GetTeamUsersWithDisplayNameAsync(HttpClient httpClient, string accessToken, string groupId, string userDisplayName)
        {
            // multiple users can have same display name, so using list
            var teamUserWithDisplayName = new List<TeamUser>();

            teamUserWithDisplayName = (await GraphHelper.GetResultCollectionAsync<TeamUser>(httpClient, $"v1.0/teams/{groupId}/members?$filter=displayname eq '{userDisplayName}'", accessToken)).Select(t => new TeamUser()
            {
                Id = t.Id,
                DisplayName = t.DisplayName,
                email = t.email,
                UserId = t.UserId
            }).ToList();
            
            return teamUserWithDisplayName;
        }

        public static async Task<TeamUser> UpdateTeamUserRole(HttpClient httpClient, string accessToken, string groupId, string teamMemberId, string role)
        {
            var teamUser = new TeamUser
            {
                Type = "#microsoft.graph.aadUserConversationMember",
                Roles = new List<string>() { role }
            };

            var updateUserEndpoint = $"v1.0/teams/{groupId}/members/{teamMemberId}";

            var result = await GraphHelper.PatchAsync(httpClient, accessToken, updateUserEndpoint, teamUser);
            
            return result;
        }

        #endregion

        #region Channel
        public static async Task<IEnumerable<TeamChannel>> GetChannelsAsync(string accessToken, HttpClient httpClient, string groupId)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<TeamChannel>(httpClient, $"v1.0/teams/{groupId}/channels", accessToken);
            return collection;
        }
        public static async Task<TeamChannel> GetPrimaryChannelAsync(string accessToken, HttpClient httpClient, string groupId)
        {
            var collection = await GraphHelper.GetAsync<TeamChannel>(httpClient, $"v1.0/teams/{groupId}/primaryChannel", accessToken);
            return collection;
        }
        public static async Task<HttpResponseMessage> DeleteChannelAsync(string accessToken, HttpClient httpClient, string groupId, string channelId)
        {
            return await GraphHelper.DeleteAsync(httpClient, $"v1.0/teams/{groupId}/channels/{channelId}", accessToken);
        }

        public static async Task<TeamChannel> AddChannelAsync(string accessToken, HttpClient httpClient, string groupId, string displayName, string description, bool isPrivate, string ownerUPN, bool isFavoriteByDefault)
        {
            var channel = new TeamChannel()
            {
                Description = description,
                DisplayName = displayName,
            };
            if (isPrivate)
            {
                channel.MembershipType = "private";
            }
            if (isPrivate)
            {
                channel.Type = "#Microsoft.Graph.channel";
                var user = await GraphHelper.GetAsync<User>(httpClient, $"v1.0/{GetUserGraphUrlForUPN(ownerUPN)}", accessToken);
                channel.Members = new List<TeamChannelMember>();
                channel.Members.Add(new TeamChannelMember() { Roles = new List<string> { "owner" }, UserIdentifier = $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/users('{user.Id}')" });
                return await GraphHelper.PostAsync<TeamChannel>(httpClient, $"v1.0/teams/{groupId}/channels", channel, accessToken);
            }
            else
            {
                channel.IsFavoriteByDefault = isFavoriteByDefault;
                return await GraphHelper.PostAsync<TeamChannel>(httpClient, $"v1.0/teams/{groupId}/channels", channel, accessToken);
            }
        }

        public static async Task PostMessageAsync(HttpClient httpClient, string accessToken, string groupId, string channelId, TeamChannelMessage message)
        {
            await GraphHelper.PostAsync(httpClient, $"v1.0/teams/{groupId}/channels/{channelId}/messages", message, accessToken);
        }

        public static async Task<List<TeamChannelMessage>> GetMessagesAsync(HttpClient httpClient, string accessToken, string groupId, string channelId, bool includeDeleted = false)
        {
            List<TeamChannelMessage> messages = new List<TeamChannelMessage>();
            var collection = await GraphHelper.GetResultCollectionAsync<TeamChannelMessage>(httpClient, $"beta/teams/{groupId}/channels/{channelId}/messages", accessToken);
            messages.AddRange(collection);

            if (includeDeleted)
            {
                return messages;
            }
            else
            {
                return messages.Where(m => !m.DeletedDateTime.HasValue).ToList();
            }
        }

        public static async Task<TeamChannel> UpdateChannelAsync(HttpClient httpClient, string accessToken, string groupId, string channelId, TeamChannel channel)
        {
            return await GraphHelper.PatchAsync(httpClient, accessToken, $"beta/teams/{groupId}/channels/{channelId}", channel);
        }
        #endregion

        #region Tabs
        public static async Task<IEnumerable<TeamTab>> GetTabsAsync(string accessToken, HttpClient httpClient, string groupId, string channelId)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<TeamTab>(httpClient, $"v1.0/teams/{groupId}/channels/{channelId}/tabs", accessToken);
            return collection;
        }

        public static async Task<TeamTab> GetTabAsync(string accessToken, HttpClient httpClient, string groupId, string channelId, string tabId)
        {
            return await GraphHelper.GetAsync<TeamTab>(httpClient, $"v1.0/teams/{groupId}/channels/{channelId}/tabs/{tabId}?$expand=teamsApp", accessToken, propertyNameCaseInsensitive: true);
        }

        public static async Task<HttpResponseMessage> DeleteTabAsync(string accessToken, HttpClient httpClient, string groupId, string channelId, string tabId)
        {
            return await GraphHelper.DeleteAsync(httpClient, $"v1.0/teams/{groupId}/channels/{channelId}/tabs/{tabId}", accessToken);
        }

        public static async Task UpdateTabAsync(HttpClient httpClient, string accessToken, string groupId, string channelId, TeamTab tab)
        {
            tab.Configuration = null;
            await GraphHelper.PatchAsync(httpClient, accessToken, $"v1.0/teams/{groupId}/channels/{channelId}/tabs/{tab.Id}", tab);
        }

        public static async Task<TeamTab> AddTabAsync(HttpClient httpClient, string accessToken, string groupId, string channelId, string displayName, TeamTabType tabType, string teamsAppId, string entityId, string contentUrl, string removeUrl, string websiteUrl)
        {
            TeamTab tab = new TeamTab();
            switch (tabType)
            {
                case TeamTabType.Custom:
                    {
                        tab.TeamsAppId = teamsAppId;
                        tab.Configuration = new TeamTabConfiguration();
                        tab.Configuration.EntityId = entityId;
                        tab.Configuration.ContentUrl = contentUrl;
                        tab.Configuration.RemoveUrl = removeUrl;
                        tab.Configuration.WebsiteUrl = websiteUrl;
                        break;
                    }
                case TeamTabType.DocumentLibrary:
                    {
                        tab.TeamsAppId = "com.microsoft.teamspace.tab.files.sharepoint";
                        tab.Configuration = new TeamTabConfiguration();
                        tab.Configuration.EntityId = "";
                        tab.Configuration.ContentUrl = contentUrl;
                        tab.Configuration.RemoveUrl = null;
                        tab.Configuration.WebsiteUrl = null;
                        break;
                    }
                case TeamTabType.WebSite:
                    {
                        tab.TeamsAppId = "com.microsoft.teamspace.tab.web";
                        tab.Configuration = new TeamTabConfiguration();
                        tab.Configuration.EntityId = null;
                        tab.Configuration.ContentUrl = contentUrl;
                        tab.Configuration.RemoveUrl = null;
                        tab.Configuration.WebsiteUrl = contentUrl;
                        break;
                    }
                case TeamTabType.Word:
                    {
                        tab.TeamsAppId = "com.microsoft.teamspace.tab.file.staticviewer.word";
                        tab.Configuration = new TeamTabConfiguration();
                        tab.Configuration.EntityId = entityId;
                        tab.Configuration.ContentUrl = contentUrl;
                        tab.Configuration.RemoveUrl = null;
                        tab.Configuration.WebsiteUrl = null;
                        break;
                    }
                case TeamTabType.Excel:
                    {
                        tab.TeamsAppId = "com.microsoft.teamspace.tab.file.staticviewer.excel";
                        tab.Configuration = new TeamTabConfiguration();
                        tab.Configuration.EntityId = entityId;
                        tab.Configuration.ContentUrl = contentUrl;
                        tab.Configuration.RemoveUrl = null;
                        tab.Configuration.WebsiteUrl = null;
                        break;
                    }
                case TeamTabType.PowerPoint:
                    {
                        tab.TeamsAppId = "com.microsoft.teamspace.tab.file.staticviewer.powerpoint";
                        tab.Configuration = new TeamTabConfiguration();
                        tab.Configuration.EntityId = entityId;
                        tab.Configuration.ContentUrl = contentUrl;
                        tab.Configuration.RemoveUrl = null;
                        tab.Configuration.WebsiteUrl = null;
                        break;
                    }
                case TeamTabType.PDF:
                    {
                        tab.TeamsAppId = "com.microsoft.teamspace.tab.file.staticviewer.pdf";
                        tab.Configuration = new TeamTabConfiguration();
                        tab.Configuration.EntityId = entityId;
                        tab.Configuration.ContentUrl = contentUrl;
                        tab.Configuration.RemoveUrl = null;
                        tab.Configuration.WebsiteUrl = null;
                        break;
                    }
                case TeamTabType.Wiki:
                    {
                        tab.TeamsAppId = "com.microsoft.teamspace.tab.wiki";
                        break;
                    }
                case TeamTabType.Planner:
                    {
                        tab.TeamsAppId = "com.microsoft.teamspace.tab.planner";
                        break;
                    }
                case TeamTabType.MicrosoftStream:
                    {
                        tab.TeamsAppId = "com.microsoftstream.embed.skypeteamstab";
                        break;
                    }
                case TeamTabType.MicrosoftForms:
                    {
                        tab.TeamsAppId = "81fef3a6-72aa-4648-a763-de824aeafb7d";
                        break;
                    }
                case TeamTabType.OneNote:
                    {
                        tab.TeamsAppId = "0d820ecd-def2-4297-adad-78056cde7c78";
                        break;
                    }
                case TeamTabType.PowerBI:
                    {
                        tab.TeamsAppId = "com.microsoft.teamspace.tab.powerbi";
                        break;
                    }
                case TeamTabType.SharePointPageAndList:
                    {
                        tab.TeamsAppId = "2a527703-1f6f-4559-a332-d8a7d288cd88";
                        break;
                    }
            }
            tab.DisplayName = displayName;
            tab.TeamsAppOdataBind = $"https://{PnPConnection.Current.GraphEndPoint}/v1.0/appCatalogs/teamsApps/{tab.TeamsAppId}";
            return await GraphHelper.PostAsync<TeamTab>(httpClient, $"v1.0/teams/{groupId}/channels/{channelId}/tabs", tab, accessToken);
        }
        #endregion

        #region Apps
        public static async Task<IEnumerable<TeamApp>> GetAppsAsync(string accessToken, HttpClient httpClient)
        {
            var collection = await GraphHelper.GetResultCollectionAsync<TeamApp>(httpClient, $"v1.0/appCatalogs/teamsApps", accessToken);
            return collection;
        }

        public static async Task<TeamApp> AddAppAsync(HttpClient httpClient, string accessToken, byte[] bytes)
        {
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/zip");
            var response = await GraphHelper.PostAsync(httpClient, "v1.0/appCatalogs/teamsApps", accessToken, byteArrayContent);
            if (!response.IsSuccessStatusCode)
            {
                if (GraphHelper.TryGetGraphException(response, out GraphException exception))
                {
                    throw exception;
                }
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<TeamApp>(content, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            return null;
        }

        public static async Task<HttpResponseMessage> UpdateAppAsync(HttpClient httpClient, string accessToken, byte[] bytes, string appId)
        {
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/zip");
            return await GraphHelper.PutAsync(httpClient, $"v1.0/appCatalogs/teamsApps/{appId}", accessToken, byteArrayContent);
        }

        public static async Task<HttpResponseMessage> DeleteAppAsync(HttpClient httpClient, string accessToken, string appId)
        {
            return await GraphHelper.DeleteAsync(httpClient, $"v1.0/appCatalogs/teamsApps/{appId}", accessToken);
        }
        #endregion
    }
}