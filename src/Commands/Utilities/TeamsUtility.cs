﻿using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;
using Team = PnP.PowerShell.Commands.Model.Teams.Team;
using TeamChannel = PnP.PowerShell.Commands.Model.Teams.TeamChannel;
using User = PnP.PowerShell.Commands.Model.Teams.User;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class TeamsUtility
    {
        private const int PageSize = 100;

        #region Team
        public static List<Group> GetGroupsWithTeam(ApiRequestHelper requestHelper, string filter = null)
        {
            Dictionary<string, string> additionalHeaders = null;
            string requestUrl;

            if (String.IsNullOrEmpty(filter))
            {
                filter = "resourceProvisioningOptions/Any(x:x eq 'Team')";

                requestUrl = $"v1.0/groups?$filter={filter}&$select=Id,DisplayName,MailNickName,Description,Visibility&$top={PageSize}";

            }
            else
            {
                filter = $"({filter}) and resourceProvisioningOptions/Any(x:x eq 'Team')";

                // This query requires ConsistencyLevel header to be set, since "Filter" could have some advanced queries supplied by the user.
                additionalHeaders = new Dictionary<string, string>();
                additionalHeaders.Add("ConsistencyLevel", "eventual");

                // $count=true needs to be here for reasons
                // see this for some additional details: https://learn.microsoft.com/en-us/graph/aad-advanced-queries?tabs=http#group-properties
                requestUrl = $"v1.0/groups?$filter={filter}&$select=Id,DisplayName,MailNickName,Description,Visibility&$top={PageSize}&$count=true";
            }

            var collection = requestHelper.GetResultCollection<Group>(requestUrl, additionalHeaders: additionalHeaders);
            return collection.ToList();
        }

        public static Group GetGroupWithTeam(ApiRequestHelper requestHelper, string mailNickname)
        {
            return requestHelper.Get<Group>($"v1.0/groups?$filter=(resourceProvisioningOptions/Any(x:x eq 'Team') and mailNickname eq '{mailNickname}')&$select=Id,DisplayName,MailNickName,Description,Visibility");
        }

        public static List<Team> GetTeamUsingFilter(ApiRequestHelper requestHelper, String filter)
        {
            List<Team> teams = new List<Team>();

            var groups = GetGroupsWithTeam(requestHelper, filter);
            foreach (var group in groups)
            {
                Team team = ParseTeamJson(requestHelper, group.Id);

                if (team != null)
                {
                    team.DisplayName = group.DisplayName;
                    team.MailNickname = group.MailNickname;
                    team.Visibility = group.Visibility.Value;
                    teams.Add(team);
                }
            }
            return teams;
        }

        public static Team GetTeam(ApiRequestHelper requestHelper, string groupId)
        {
            // get the group
            var group = requestHelper.Get<Group>($"v1.0/groups/{groupId}?$select=Id,DisplayName,MailNickName,Description,Visibility");

            Team team = ParseTeamJson(requestHelper, group.Id);
            if (team != null)
            {
                team.DisplayName = group.DisplayName;
                team.MailNickname = group.MailNickname;
                team.Visibility = group.Visibility.Value;
                return team;
            }
            else
            {
                return null;
            }
        }

        public static HttpResponseMessage DeleteTeam(ApiRequestHelper requestHelper, string groupId)
        {
            return requestHelper.Delete($"v1.0/groups/{groupId}");
        }
        public static HttpResponseMessage CloneTeam(ApiRequestHelper requestHelper, string groupId, TeamCloneInformation teamClone)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(new
            {
                displayName = teamClone.DisplayName,
                classification = teamClone.Classification,
                description = teamClone.Description,
                mailNickname = teamClone.MailNickName,
                visibility = teamClone.Visibility.ToString(),
                partsToClone = String.Join(",", teamClone.PartsToClone)
            }));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return requestHelper.PostHttpContent($"v1.0/teams/{groupId}/clone", content);
        }
        private static Team ParseTeamJson(ApiRequestHelper requestHelper, string groupId)
        {
            // Get Settings
            try
            {
                var team = requestHelper.Get<Team>($"v1.0/teams/{groupId}", false, true);
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

        public static Team NewTeam(ApiRequestHelper requestHelper, string groupId, string displayName, string description, string classification, string mailNickname, GroupVisibility visibility, TeamCreationInformation teamCI, string[] owners, string[] members, Guid[] sensitivityLabels, TeamsTemplateType templateType = TeamsTemplateType.None, TeamResourceBehaviorOptions?[] resourceBehaviorOptions = null)
        {
            Group group = null;
            Team returnTeam = null;
            Random random = new();
            // Maximum number of retries
            const int maxRetries = 12;
            // Create the Group
            if (string.IsNullOrEmpty(groupId))
            {
                group = CreateGroup(requestHelper, displayName, description, classification, mailNickname, visibility, owners, sensitivityLabels, templateType, resourceBehaviorOptions);
                bool wait = true;
                int iterations = 0;

                // Initial backoff time in seconds
                const int initialBackoffSeconds = 5;

                while (wait && iterations < maxRetries)
                {
                    iterations++;

                    try
                    {
                        var createdGroup = requestHelper.Get<Group>($"v1.0/groups/{group.Id}");
                        if (!string.IsNullOrEmpty(createdGroup.DisplayName))
                        {
                            wait = false;
                        }
                    }
                    catch (Exception)
                    {
                        // Calculate exponential backoff with a minimum of initialBackoffSeconds
                        int backoffSeconds = initialBackoffSeconds * (int)Math.Pow(2, iterations - 1);
                        // Cap at a maximum backoff (e.g., 30 seconds)
                        backoffSeconds = Math.Min(backoffSeconds, 30);

                        // Add random jitter between 0-1 second to avoid thundering herd
                        int jitterMs = random.Next(0, 1000);

                        // Sleep for the calculated time
                        Thread.Sleep(TimeSpan.FromSeconds(backoffSeconds) + TimeSpan.FromMilliseconds(jitterMs));
                    }
                }
            }
            else
            {
                group = requestHelper.Get<Group>($"v1.0/groups/{groupId}");
                if (group == null)
                {
                    throw new PSArgumentException($"Cannot find group with id {groupId}");
                }
                teamCI.Visibility = group.Visibility.Value;
                teamCI.Description = group.Description;
            }
            if (group != null)
            {
                Team team = teamCI.ToTeam(group.Visibility.Value);

                const int initialBackoffMs = 1000;
                var retryCount = 0;
                bool success = false;

                while (!success && retryCount < maxRetries)
                {
                    try
                    {
                        var teamSettings = requestHelper.Put($"v1.0/groups/{group.Id}/team", team);
                        if (teamSettings != null)
                        {
                            returnTeam = GetTeam(requestHelper, group.Id);
                            success = true;
                        }
                    }
                    catch (GraphException ge) when (ge.HttpResponse.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        // Handle conflict exceptions as if it succeeded, as it means a previous request succeeded enabling teams
                        returnTeam = GetTeam(requestHelper, group.Id);
                        success = true;
                    }
                    catch
                    {
                        retryCount++;

                        if (retryCount >= maxRetries)
                        {
                            // If we've reached max retries, rethrow the exception
                            throw;
                        }

                        // Exponential backoff with jitter to avoid thundering herd problem
                        int backoffMs = initialBackoffMs * (int)Math.Pow(2, retryCount - 1);
                        // Add up to 1 second of random jitter
                        int jitterMs = random.Next(0, 1000);
                        // Cap at 30 seconds max
                        int delayMs = Math.Min(backoffMs + jitterMs, 30000);
                        Thread.Sleep(delayMs);
                    }
                }

                // Construct a list of all owners and members to add
                var teamOwnersAndMembers = new List<TeamChannelMember>();
                if (owners != null && owners.Length > 0)
                {
                    foreach (var owner in owners)
                    {
                        teamOwnersAndMembers.Add(new TeamChannelMember { Roles = new List<string> { "owner" }, UserIdentifier = $"https://{requestHelper.GraphEndPoint}/v1.0/users('{owner}')" });
                    }
                }

                if (members != null && members.Length > 0)
                {
                    foreach (var member in members)
                    {
                        teamOwnersAndMembers.Add(new TeamChannelMember { Roles = new List<string>(), UserIdentifier = $"https://{requestHelper.GraphEndPoint}/v1.0/users('{member}')" });
                    }
                }

                if (teamOwnersAndMembers.Count > 0)
                {
                    var ownersAndMembers = GraphBatchUtility.Chunk(teamOwnersAndMembers, 200);
                    foreach (var chunk in ownersAndMembers)
                    {
                        requestHelper.Post($"v1.0/teams/{group.Id}/members/add", new { values = chunk.ToList() });
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

        private static Group CreateGroup(ApiRequestHelper requestHelper, string displayName, string description, string classification, string mailNickname, GroupVisibility visibility, string[] owners, Guid[] sensitivityLabels, TeamsTemplateType templateType = TeamsTemplateType.None, TeamResourceBehaviorOptions?[] resourceBehaviorOptions = null)
        {
            // When creating a group, we always need an owner, thus we'll try to define it from the passed in owners array
            string ownerId = null;
            if (owners != null && owners.Length > 0)
            {
                // Owner(s) have been provided, use the first owner as the initial owner. The other owners will be added later.
                var user = requestHelper.Get<User>($"v1.0/{GetUserGraphUrlForUPN(owners[0])}?$select=Id");

                if (user != null)
                {
                    // User Id of the first owner has been found
                    ownerId = user.Id;
                }
                else
                {
                    // Unable to find the owner by its user principal name, try looking for it on its email address
                    var collection = requestHelper.GetResultCollection<User>($"v1.0/users?$filter=mail eq '{owners[0]}'&$select=Id");
                    if (collection != null && collection.Any())
                    {
                        // User found on its email address
                        ownerId = collection.First().Id;
                    }
                }
            }

            // Check if by now we've identified a user Id to become the owner
            if (string.IsNullOrEmpty(ownerId))
            {
                var contextSettings = requestHelper.Connection.Context.GetContextSettings();

                // Still no owner identified, see if we can make the current user executing this cmdlet the owner
                if (contextSettings.Type != Framework.Utilities.Context.ClientContextType.AzureADCertificate)
                {
                    // A delegate context is available, make the user part of the delegate token the owner
                    var user = requestHelper.Get<User>("v1.0/me?$select=Id");

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
                MailNickname = mailNickname ?? CreateAlias(requestHelper),
                GroupTypes = new List<string>() { "Unified" },
                SecurityEnabled = false,
                Visibility = visibility == GroupVisibility.NotSpecified ? GroupVisibility.Private : visibility
            };

            // Check if we managed to define an owner for the group. If not, we'll revert to not providing an owner, which will mean that the app principal will become the owner of the Group
            if (!string.IsNullOrEmpty(ownerId))
            {
                group.Owners = new List<string>() { $"https://{requestHelper.GraphEndPoint}/v1.0/users/{ownerId}" };
                group.Members = new List<string>() { $"https://{requestHelper.GraphEndPoint}/v1.0/users/{ownerId}" };
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

            if (sensitivityLabels != null && sensitivityLabels.Length > 0)
            {
                var assignedLabels = new List<AssignedLabels>();
                foreach (var label in sensitivityLabels)
                {
                    if (!Guid.Empty.Equals(label))
                    {
                        assignedLabels.Add(new AssignedLabels
                        {
                            labelId = label.ToString()
                        });
                    }
                }

                group.AssignedLabels = assignedLabels;
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
                return requestHelper.Post<Group>("v1.0/groups", group);
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

        private static string CreateAlias(ApiRequestHelper requestHelper)
        {
            var guid = Guid.NewGuid().ToString();
            var teamName = string.Empty;
            // check if the group exists
            do
            {
                var teamNameTemp = $"msteams_{guid.Substring(0, 8)}{guid.Substring(9, 4)}";
                var collection = requestHelper.Get<RestResultCollection<Group>>($"v1.0/groups?$filter=groupTypes/any(c:c+eq+'Unified') and (mailNickname eq '{teamNameTemp}')");
                if (collection != null)
                {
                    if (!collection.Items.Any()) teamName = teamNameTemp;
                }

            } while (teamName == string.Empty);
            return teamName;
        }

        public static Team UpdateTeam(ApiRequestHelper requestHelper, string groupId, Team team)
        {
            return requestHelper.Patch<Team>($"v1.0/teams/{groupId}", team);
        }

        public static Group UpdateGroup(ApiRequestHelper requestHelper, string groupId, Group group)
        {
            return requestHelper.Patch<Group>($"v1.0/groups/{groupId}", group);
        }

        public static void SetTeamPictureAsync(ApiRequestHelper requestHelper, string teamId, byte[] bytes, string contentType)
        {
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            requestHelper.Put<string>($"v1.0/teams/{teamId}/photo/$value", byteArrayContent);
        }

        public static HttpResponseMessage SetTeamArchivedState(ApiRequestHelper requestHelper, string groupId, bool archived, bool? setSiteReadOnly)
        {
            if (archived)
            {
                StringContent content = new StringContent(JsonSerializer.Serialize(setSiteReadOnly.HasValue ? new { shouldSetSpoSiteReadOnlyForMembers = setSiteReadOnly } : null));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return requestHelper.PostHttpContent($"v1.0/teams/{groupId}/archive", content);
            }
            else
            {
                StringContent content = new StringContent("");
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return requestHelper.PostHttpContent($"v1.0/teams/{groupId}/unarchive", content);
            }
        }

        public static IEnumerable<DeletedTeam> GetDeletedTeam(ApiRequestHelper requestHelper)
        {
            // get the deleted team
            var deletedTeams = requestHelper.GetResultCollection<DeletedTeam>($"beta/teamwork/deletedTeams");
            if (deletedTeams != null && deletedTeams.Any())
            {
                return deletedTeams;
            }
            return null;
        }

        public static List<JoinedTeam> GetJoinedTeams(ApiRequestHelper requestHelper, Guid userId)
        {
            string requestUrl = $"v1.0/users/{userId}/joinedTeams";
            var collection = requestHelper.GetResultCollection<JoinedTeam>(requestUrl);
            return collection.ToList();

        }
        #endregion

        #region Users
        public static void AddUser(ApiRequestHelper requestHelper, string groupId, string upn, string role)
        {
            var userIdResult = requestHelper.Get($"v1.0/{GetUserGraphUrlForUPN(upn)}?$select=Id");
            var resultElement = JsonSerializer.Deserialize<JsonElement>(userIdResult);
            if (resultElement.TryGetProperty("id", out JsonElement idProperty))
            {
                var postData = new Dictionary<string, string>() {
                    {
                        "@odata.id", $"https://{requestHelper.GraphEndPoint}/v1.0/users/{idProperty.GetString()}"
                    }
                };

                requestHelper.Post($"v1.0/groups/{groupId}/{role.ToLower()}s/$ref", postData);
            }
        }

        public static void AddUsers(ApiRequestHelper requestHelper, string groupId, string[] upn, string role)
        {
            var teamChannelMember = new List<TeamChannelMember>();
            if (upn != null && upn.Length > 0)
            {
                foreach (var user in upn)
                {
                    teamChannelMember.Add(new TeamChannelMember() { Roles = new List<string> { role }, UserIdentifier = $"https://{requestHelper.GraphEndPoint}/v1.0/users('{user}')" });
                }
                if (teamChannelMember.Count > 0)
                {
                    var chunks = GraphBatchUtility.Chunk(teamChannelMember, 200);
                    foreach (var chunk in chunks.ToList())
                    {
                        requestHelper.Post($"v1.0/teams/{groupId}/members/add", new { values = chunk.ToList() });
                    }
                }
            }
        }

        public static List<User> GetUsers(ApiRequestHelper requestHelper, string groupId, string role)
        {
            var selectedRole = role != null ? role.ToLower() : null;
            var owners = new List<User>();
            var guests = new List<User>();
            var members = new List<User>();
            if (selectedRole != "guest")
            {
                owners = (requestHelper.GetResultCollection<User>($"v1.0/groups/{groupId}/owners?$select=Id,displayName,userPrincipalName,userType")).Select(t => new User()
                {
                    Id = t.Id,
                    DisplayName = t.DisplayName,
                    UserPrincipalName = t.UserPrincipalName,
                    UserType = "Owner"
                }).ToList();
            }
            if (selectedRole != "owner")
            {
                var users = (requestHelper.GetResultCollection<User>($"v1.0/groups/{groupId}/members?$select=Id,displayName,userPrincipalName,userType"));
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

        public static IEnumerable<User> GetUsers(ApiRequestHelper requestHelper, string groupId, string channelId, string role)
        {
            List<User> users = new List<User>();
            var selectedRole = role != null ? role.ToLower() : null;

            var collection = requestHelper.GetResultCollection<TeamChannelMember>($"v1.0/teams/{groupId}/channels/{channelId}/members");
            if (collection != null && collection.Any())
            {
                users.AddRange(collection.Select(m => new User() { DisplayName = m.DisplayName, Id = m.UserId, UserPrincipalName = m.Email, UserType = m.Roles.Count > 0 ? m.Roles[0].ToLower() : "" }));
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

        public static void DeleteUser(ApiRequestHelper requestHelper, string groupId, string upn, string role)
        {
            var user = requestHelper.Get<User>($"v1.0/{GetUserGraphUrlForUPN(upn)}?$select=Id");
            if (user != null)
            {
                // check if the user is an owner
                var owners = requestHelper.GetResultCollection<User>($"v1.0/groups/{groupId}/owners?$select=Id");
                if (owners.Any() && owners.FirstOrDefault(u => u.Id.Equals(user.Id, StringComparison.OrdinalIgnoreCase)) != null)
                {
                    if (owners.Count() == 1)
                    {
                        throw new PSInvalidOperationException("Last owner cannot be removed");
                    }
                    requestHelper.Delete($"v1.0/groups/{groupId}/owners/{user.Id}/$ref");
                }
                if (!role.Equals("owner", StringComparison.OrdinalIgnoreCase))
                {
                    requestHelper.Delete($"v1.0/groups/{groupId}/members/{user.Id}/$ref");
                }
            }
        }

        public static List<TeamUser> GetTeamUsersWithDisplayName(ApiRequestHelper requestHelper, string groupId, string userDisplayName)
        {
            // multiple users can have same display name, so using list
            var teamUserWithDisplayName = new List<TeamUser>();

            teamUserWithDisplayName = (requestHelper.GetResultCollection<TeamUser>($"v1.0/teams/{groupId}/members?$filter=displayname eq '{userDisplayName}'")).Select(t => new TeamUser()
            {
                Id = t.Id,
                DisplayName = t.DisplayName,
                email = t.email,
                UserId = t.UserId
            }).ToList();

            return teamUserWithDisplayName;
        }

        public static TeamUser UpdateTeamUserRole(ApiRequestHelper requestHelper, string groupId, string teamMemberId, string role)
        {
            var teamUser = new TeamUser
            {
                Type = "#microsoft.graph.aadUserConversationMember",
                Roles = new List<string>() { role }
            };

            var updateUserEndpoint = $"v1.0/teams/{groupId}/members/{teamMemberId}";

            var result = requestHelper.Patch(updateUserEndpoint, teamUser);

            return result;
        }

        public static void DeleteUsers(ApiRequestHelper requestHelper, string groupId, string[] upn, string role)
        {
            var teamChannelMember = new List<TeamChannelMember>();
            if (upn != null && upn.Length > 0)
            {
                foreach (var user in upn)
                {
                    teamChannelMember.Add(new TeamChannelMember() { Roles = null, UserIdentifier = $"https://{requestHelper.GraphEndPoint}/v1.0/users('{user}')" });
                }
                if (teamChannelMember.Count > 0)
                {
                    var chunks = GraphBatchUtility.Chunk(teamChannelMember, 200);
                    foreach (var chunk in chunks.ToList())
                    {
                        requestHelper.Post($"v1.0/teams/{groupId}/members/remove", new { values = chunk.ToList() });
                    }
                }
            }
        }

        #endregion

        #region Channel

        public static TeamChannel GetChannel(ApiRequestHelper requestHelper, string groupId, string channelId, bool useBeta = false)
        {
            var additionalHeaders = new Dictionary<string, string>()
            {
                { "Prefer", "include-unknown-enum-members" }
            };

            var channel = requestHelper.Get<TeamChannel>($"{(useBeta ? "beta" : "v1.0")}/teams/{groupId}/channels/{channelId}", additionalHeaders: additionalHeaders);
            return channel;
        }

        public static IEnumerable<TeamChannel> GetChannels(ApiRequestHelper requestHelper, string groupId, bool useBeta = false)
        {
            var additionalHeaders = new Dictionary<string, string>()
            {
                { "Prefer", "include-unknown-enum-members" }
            };

            var collection = requestHelper.GetResultCollection<TeamChannel>($"{(useBeta ? "beta" : "v1.0")}/teams/{groupId}/channels", additionalHeaders: additionalHeaders);
            return collection;
        }

        public static TeamChannel GetPrimaryChannel(ApiRequestHelper requestHelper, string groupId, bool useBeta = false)
        {
            var additionalHeaders = new Dictionary<string, string>()
            {
                { "Prefer", "include-unknown-enum-members" }
            };

            var collection = requestHelper.Get<TeamChannel>($"{(useBeta ? "beta" : "v1.0")}/teams/{groupId}/primaryChannel", additionalHeaders: additionalHeaders);
            return collection;
        }

        public static HttpResponseMessage DeleteChannel(ApiRequestHelper requestHelper, string groupId, string channelId, bool useBeta = false)
        {
            return requestHelper.Delete($"{(useBeta ? "beta" : "v1.0")}/teams/{groupId}/channels/{channelId}");
        }

        public static TeamChannel AddChannel(ApiRequestHelper requestHelper, string groupId, string displayName, string description, TeamsChannelType channelType, string ownerUPN, bool isFavoriteByDefault)
        {
            var channel = new TeamChannel()
            {
                Description = description,
                DisplayName = displayName,
            };
            if (channelType == TeamsChannelType.Private)
            {
                channel.MembershipType = "private";
            }
            if (channelType == TeamsChannelType.Shared)
            {
                channel.MembershipType = "shared";
            }
            if (channelType == TeamsChannelType.Private || channelType == TeamsChannelType.Shared)
            {
                channel.Type = "#Microsoft.Graph.channel";
                var user = requestHelper.Get<User>($"v1.0/{GetUserGraphUrlForUPN(ownerUPN)}");
                channel.Members = new List<TeamChannelMember>();
                channel.Members.Add(new TeamChannelMember() { Roles = new List<string> { "owner" }, UserIdentifier = $"https://{requestHelper.GraphEndPoint}/v1.0/users('{user.Id}')" });
                return requestHelper.Post<TeamChannel>($"v1.0/teams/{groupId}/channels", channel);
            }
            else
            {
                channel.IsFavoriteByDefault = null;
                return requestHelper.Post<TeamChannel>($"v1.0/teams/{groupId}/channels", channel);
            }
        }

        public static void PostMessage(ApiRequestHelper requestHelper, string groupId, string channelId, TeamChannelMessage message)
        {
            requestHelper.Post($"v1.0/teams/{groupId}/channels/{channelId}/messages", message);
        }

        public static TeamChannelMessage GetMessage(ApiRequestHelper requestHelper, string groupId, string channelId, string messageId)
        {
            return requestHelper.Get<TeamChannelMessage>($"v1.0/teams/{groupId}/channels/{channelId}/messages/{messageId}");
        }

        public static List<TeamChannelMessage> GetMessages(ApiRequestHelper requestHelper, string groupId, string channelId, bool includeDeleted = false)
        {
            List<TeamChannelMessage> messages = new List<TeamChannelMessage>();
            var collection = requestHelper.GetResultCollection<TeamChannelMessage>($"v1.0/teams/{groupId}/channels/{channelId}/messages");
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

        /// <summary>
        /// List all the replies to a message in a channel of a team.
        /// </summary>
        public static List<TeamChannelMessageReply> GetMessageReplies(ApiRequestHelper requestHelper, string groupId, string channelId, string messageId, bool includeDeleted = false)
        {
            var replies = requestHelper.GetResultCollection<TeamChannelMessageReply>($"v1.0/teams/{groupId}/channels/{channelId}/messages/{messageId}/replies");

            return includeDeleted ? replies.ToList() : replies.Where(r => !r.DeletedDateTime.HasValue).ToList();
        }

        /// <summary>
        /// Get a specific reply of a message in a channel of a team.
        /// </summary>
        public static TeamChannelMessageReply GetMessageReply(ApiRequestHelper requestHelper, string groupId, string channelId, string messageId, string replyId)
        {
            return requestHelper.Get<TeamChannelMessageReply>($"v1.0/teams/{groupId}/channels/{channelId}/messages/{messageId}/replies/{replyId}");
        }

        /// <summary>
        /// Updates a Teams Channel
        /// </summary>
        public static TeamChannel UpdateChannel(ApiRequestHelper requestHelper, string groupId, string channelId, TeamChannel channel, bool useBeta = false)
        {
            return requestHelper.Patch($"{(useBeta ? "beta" : "v1.0")}/teams/{groupId}/channels/{channelId}", channel);
        }
        #endregion

        #region Channel member

        /// <summary>
        /// Get specific memberbership of user who has access to a certain Microsoft Teams channel.
        /// </summary>
        /// <returns>User channel membership.</returns>
        public static TeamChannelMember GetChannelMember(ApiRequestHelper requestHelper, string groupId, string channelId, string membershipId)
        {
            // Currently the Graph request to get a membership by id fails (v1.0/teams/{groupId}/channels/{channelId}/members/{membershipId}).
            // This is why the method below is used.

            var memberships = GetChannelMembers(requestHelper, groupId, channelId);
            return memberships.FirstOrDefault(m => membershipId.Equals(m.Id));
        }

        /// <summary>
        /// Get list of all memberships of a certain Microsoft Teams channel.
        /// </summary>
        /// <returns>List of memberships.</returns>
        public static IEnumerable<TeamChannelMember> GetChannelMembers(ApiRequestHelper requestHelper, string groupId, string channelId, string role = null)
        {
            var collection = requestHelper.GetResultCollection<TeamChannelMember>($"v1.0/teams/{groupId}/channels/{channelId}/members");

            if (!string.IsNullOrEmpty(role))
            {
                // Members have no role value
                collection = role.Equals("member", StringComparison.OrdinalIgnoreCase) ? collection.Where(i => !i.Roles.Any()) : collection.Where(i => i.Roles.Any(r => role.Equals(r, StringComparison.OrdinalIgnoreCase)));
            }

            return collection;
        }

        /// <summary>
        /// Add specified member to a specified Microsoft Teams channel with a certain role.
        /// </summary>
        /// <param name="role">User role, valid values: Owner, Member</param>
        /// <returns>Added membership.</returns>
        public static TeamChannelMember AddChannelMember(ApiRequestHelper requestHelper, string groupId, string channelId, string upn, string role)
        {
            var channelMember = new TeamChannelMember
            {
                UserIdentifier = $"https://{requestHelper.GraphEndPoint}/v1.0/users('{upn}')",
            };

            // The role for the user. Must be owner or empty.
            if (role.Equals("owner", StringComparison.OrdinalIgnoreCase))
                channelMember.Roles.Add("owner");

            return requestHelper.Post($"v1.0/teams/{groupId}/channels/{channelId}/members", channelMember);
        }

        /// <summary>
        /// Remove specified member of a specified Microsoft Teams channel.
        /// </summary>
        /// <returns>True when removal succeeded, else false.</returns>
        public static HttpResponseMessage DeleteChannelMember(ApiRequestHelper requestHelper, string groupId, string channelId, string membershipId)
        {
            return requestHelper.Delete($"v1.0/teams/{groupId}/channels/{channelId}/members/{membershipId}");
        }

        /// <summary>
        /// Update the role of a specific member of a Microsoft Teams channel.
        /// </summary>
        /// <returns>Updated membership object.</returns>
        public static TeamChannelMember UpdateChannelMember(ApiRequestHelper requestHelper, string groupId, string channelId, string membershipId, string role)
        {
            var channelMember = new TeamChannelMember();

            // User role. Empty for member, 'owner' for owner.
            if (role.Equals("owner", StringComparison.OrdinalIgnoreCase))
                channelMember.Roles.Add("owner");

            return requestHelper.Patch($"v1.0/teams/{groupId}/channels/{channelId}/members/{membershipId}", channelMember);
        }

        public static TeamsChannelFilesFolder GetChannelsFilesFolder(ApiRequestHelper requestHelper, string groupId, string channelId)
        {
            var collection = requestHelper.Get<TeamsChannelFilesFolder>($"v1.0/teams/{groupId}/channels/{channelId}/filesFolder");
            return collection;
        }

        #endregion

        #region Tabs
        public static IEnumerable<TeamTab> GetTabs(ApiRequestHelper requestHelper, string groupId, string channelId)
        {
            var collection = requestHelper.GetResultCollection<TeamTab>($"v1.0/teams/{groupId}/channels/{channelId}/tabs");
            return collection;
        }

        public static TeamTab GetTab(ApiRequestHelper requestHelper, string groupId, string channelId, string tabId)
        {
            return requestHelper.Get<TeamTab>($"v1.0/teams/{groupId}/channels/{channelId}/tabs/{tabId}?$expand=teamsApp", propertyNameCaseInsensitive: true);
        }

        public static HttpResponseMessage DeleteTab(ApiRequestHelper requestHelper, string groupId, string channelId, string tabId)
        {
            return requestHelper.Delete($"v1.0/teams/{groupId}/channels/{channelId}/tabs/{tabId}");
        }

        public static void UpdateTab(ApiRequestHelper requestHelper, string groupId, string channelId, TeamTab tab)
        {
            tab.Configuration = null;
            requestHelper.Patch($"v1.0/teams/{groupId}/channels/{channelId}/tabs/{tab.Id}", tab);
        }

        public static TeamTab AddTab(ApiRequestHelper requestHelper, string groupId, string channelId, string displayName, TeamTabType tabType, string teamsAppId, string entityId, string contentUrl, string removeUrl, string websiteUrl)
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
                        tab.Configuration = new TeamTabConfiguration();
                        tab.Configuration.ContentUrl = contentUrl;
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
                        tab.Configuration = new TeamTabConfiguration();
                        tab.Configuration.ContentUrl = contentUrl;
                        tab.Configuration.WebsiteUrl = websiteUrl;
                        break;
                    }
            }
            tab.DisplayName = displayName;
            tab.TeamsAppOdataBind = $"https://{requestHelper.GraphEndPoint}/v1.0/appCatalogs/teamsApps/{tab.TeamsAppId}";
            return requestHelper.Post<TeamTab>($"v1.0/teams/{groupId}/channels/{channelId}/tabs", tab);
        }
        #endregion

        #region Apps
        public static IEnumerable<TeamApp> GetApps(ApiRequestHelper requestHelper)
        {
            var collection = requestHelper.GetResultCollection<TeamApp>($"v1.0/appCatalogs/teamsApps");
            return collection;
        }

        public static TeamApp AddApp(ApiRequestHelper requestHelper, byte[] bytes)
        {
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/zip");
            var response = requestHelper.PostHttpContent("v1.0/appCatalogs/teamsApps", byteArrayContent);
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonSerializer.Deserialize<TeamApp>(content, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public static HttpResponseMessage UpdateApp(ApiRequestHelper requestHelper, byte[] bytes, string appId)
        {
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/zip");
            return requestHelper.PutHttpContent($"v1.0/appCatalogs/teamsApps/{appId}", byteArrayContent);
        }

        public static HttpResponseMessage DeleteApp(ApiRequestHelper requestHelper, string appId)
        {
            return requestHelper.Delete($"v1.0/appCatalogs/teamsApps/{appId}");
        }
        #endregion

        #region Tags

        public static IEnumerable<TeamTag> GetTags(ApiRequestHelper requestHelper, string groupId)
        {
            var collection = requestHelper.GetResultCollection<TeamTag>($"v1.0/teams/{groupId}/tags");
            return collection;
        }

        public static TeamTag GetTagsWithId(ApiRequestHelper requestHelper, string groupId, string tagId)
        {
            var tagInformation = requestHelper.Get<TeamTag>($"v1.0/teams/{groupId}/tags/{tagId}");
            return tagInformation;
        }

        public static void UpdateTag(ApiRequestHelper requestHelper, string groupId, string tagId, string displayName)
        {
            var body = new { displayName = displayName };
            requestHelper.Patch($"v1.0/teams/{groupId}/tags/{tagId}", body);
        }

        public static HttpResponseMessage DeleteTag(ApiRequestHelper requestHelper, string groupId, string tagId)
        {
            return requestHelper.Delete($"v1.0/teams/{groupId}/tags/{tagId}");
        }

        #endregion
    }
}