using System.Management.Automation;
using Microsoft.SharePoint.Client;

using System.Linq.Expressions;
using System;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Data;
using System.Text;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPSitePermissions", DefaultParameterSetName = PARAMETERSET_IDENTITY)]
    public class GetSitePermissions : PnPWebCmdlet
    {
        private const string PARAMETERSET_IDENTITY = "Specific user request";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = PARAMETERSET_IDENTITY)]
        public UserPipeBind Identity;

        /// <summary>
        /// Enumerator with the types of securable items this cmdlet reports on
        /// </summary>
        public enum UserRightsResourceType
        {
            /// <summary>
            /// Permissions granted to an entire Microsoft 365 Group
            /// </summary>
            Microsoft365Group,

            /// <summary>
            /// A SharePoint Site
            /// </summary>
            Web,

            /// <summary>
            /// A SharePoint List
            /// </summary>
            List,

            /// <summary>
            /// A SharePoint Document Library
            /// </summary>
            DocumentLibrary,

            /// <summary>
            /// A SharePoint List Item
            /// </summary>
            ListItem
        }

        /// <summary>
        /// Defines how the permissions have been assigned
        /// </summary>
        public enum UserRightsGivenThroughType
        {
            /// <summary>
            /// Permissions granted via Microsoft 365 Group membership
            /// </summary>
            Microsoft365Group,

            /// <summary>
            /// Permissions granted by direct rights assignment
            /// </summary>
            DirectAssignment,

            /// <summary>
            /// Permissions granted via SharePoint Group membership
            /// </summary>
            SharePointGroup
        }

        /// <summary>
        /// Output type for this cmdlet containing one user right result
        /// </summary>
        public class UserRightItem
        {
            /// <summary>
            /// Path to the securable item
            /// </summary>
            public string ResourcePath { get; set; }

            /// <summary>
            /// Type of securable resource
            /// </summary>
            public UserRightsResourceType ResourceType { get; set; }

            /// <summary>
            /// How the rights have been assigned
            /// </summary>
            public UserRightsGivenThroughType  UserRightsGivenThrough{ get; set;}

            /// <summary>
            /// The type of permission assigned
            /// </summary>
            public string Permission { get; set; }

            /// <summary>
            /// Boolean indicating if the user is a guest
            /// </summary>
            public bool IsGuest { get; set; }

            /// <summary>
            /// Name of the user to which this permission set applies
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// E-mail address of the user to which this permission set applies
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// Universal Principal Name (UPN) of the user to which this permission set applies
            /// </summary>
            public string Upn { get; set; }
        }

        // TODO: fix to better way to get a Graph token without having to make it a graph cmdlet
        public string GraphAccessToken
        {
            get
            {
                if (PnPConnection.CurrentConnection?.ConnectionMethod == ConnectionMethod.ManagedIdentity)
                {
                    return TokenHandler.GetManagedIdentityTokenAsync(this,HttpClient, "https://graph.microsoft.com/").GetAwaiter().GetResult();
                }
                else
                {
                    if (PnPConnection.CurrentConnection?.Context != null)
                    {
                        return TokenHandler.GetAccessToken(GetType(), "https://graph.microsoft.com/.default");
                    }
                }

                return null;
            }
        }

        protected override void ExecuteCmdlet()
        {
            var userExpressions = new Expression<Func<User, object>>[]
            {
                u => u.Id,
                u => u.Title,
                u => u.LoginName,
                u => u.AadObjectId,
                u => u.Email,
                u => u.UserPrincipalName,
                u => u.IsShareByEmailGuestUser,
                u => u.IsSiteAdmin,
                u => u.UserId,
                u => u.IsHiddenInUI,
                u => u.PrincipalType,
                u => u.Alerts.Include(
                    a => a.Title,
                    a => a.Status),
                u => u.Groups.Include(
                    g => g.Id,
                    g => g.Title,
                    g => g.LoginName)
            };

            CurrentWeb.Context.Load(CurrentWeb.SiteUsers, u => u.Include(userExpressions));

            var results = new List<UserRightItem>();

            // Get all the role assignments and role definition bindings to be able to see which users have been given rights directly on the site level
            CurrentWeb.Context.Load(CurrentWeb.RoleAssignments, ac => ac.Include(a => a.RoleDefinitionBindings, a => a.Member.LoginName, a => a.Member.PrincipalType));
            CurrentWeb.Context.Load(CurrentWeb.SiteGroups, sg => sg.Include(u => u.LoginName, u => u.Users.Include(userExpressions)));
            //CurrentWeb.Context.Load(CurrentWeb, w => w.ServerRelativeUrl);
            CurrentWeb.Context.ExecuteQueryRetry();

            CurrentWeb.Context.Load(CurrentWeb, w => w.Url);
            CurrentWeb.Context.ExecuteQueryRetry();

            // Direct rights assignments
            foreach (var siteUser in CurrentWeb.SiteUsers)
            {
                var roleAssignment = CurrentWeb.RoleAssignments.FirstOrDefault(ra => ra.Member.LoginName == siteUser.LoginName);
                if (roleAssignment != null)
                {
                    var user = roleAssignment.Member as User;
                    foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                    {
                        if (roleDefinition.Name == "Limited Access") continue;

                        results.Add(new UserRightItem
                        {
                            ResourcePath = CurrentWeb.Url,
                            UserRightsGivenThrough = UserRightsGivenThroughType.DirectAssignment,
                            ResourceType = UserRightsResourceType.Web,
                            Upn = siteUser.UserPrincipalName,
                            Email = siteUser.Email,
                            Name = siteUser.Title,
                            Permission = siteUser.IsSiteAdmin ? "Site collection administrator" : roleDefinition.Name,
                            IsGuest = siteUser.IsShareByEmailGuestUser
                        });
                    }
                }
            }

            // SharePoint Group memberships
            foreach (var group in CurrentWeb.SiteGroups)
            {
                var roleAssignment = CurrentWeb.RoleAssignments.FirstOrDefault(ra => ra.Member.LoginName == group.LoginName);
                if (roleAssignment != null)
                {
                    foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                    {
                        if (roleDefinition.Name == "Limited Access") continue;

                        foreach (var groupUser in group.Users)
                        {
                            switch (roleAssignment.Member.PrincipalType)
                            {
                                case Microsoft.SharePoint.Client.Utilities.PrincipalType.User:
                                    results.Add(new UserRightItem
                                    {
                                        ResourcePath = CurrentWeb.Url,
                                        UserRightsGivenThrough = UserRightsGivenThroughType.DirectAssignment,
                                        ResourceType = UserRightsResourceType.Web,
                                        Upn = groupUser.UserPrincipalName,
                                        Email = groupUser.Email,
                                        Name = groupUser.Title,
                                        Permission = groupUser.IsSiteAdmin ? "Site collection administrator" : roleDefinition.Name,
                                        IsGuest = groupUser.IsShareByEmailGuestUser
                                    });
                                    break;

                                case Microsoft.SharePoint.Client.Utilities.PrincipalType.SharePointGroup:
                                    if (groupUser.AadObjectId != null)
                                    {
                                        var microsoft365Group = UnifiedGroupsUtility.GetUnifiedGroup(groupUser.AadObjectId.NameId, GraphAccessToken);

                                        if (groupUser.Title.EndsWith("Members"))
                                        {
                                            var microsoft365GroupMembers = UnifiedGroupsUtility.GetUnifiedGroupMembers(microsoft365Group, GraphAccessToken);

                                            foreach (var microsoft365GroupMember in microsoft365GroupMembers)
                                            {
                                                results.Add(new UserRightItem
                                                {
                                                    ResourcePath = CurrentWeb.Url,
                                                    UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                    ResourceType = UserRightsResourceType.Web,
                                                    Upn = microsoft365GroupMember.UserPrincipalName,
                                                    Email = null,
                                                    Name = microsoft365GroupMember.DisplayName,
                                                    Permission = "Member",
                                                    IsGuest = microsoft365GroupMember.UserPrincipalName.Contains("#EXT#")
                                                });
                                            }
                                        }
                                        else
                                        {
                                            var microsoft365GroupOwners = UnifiedGroupsUtility.GetUnifiedGroupOwners(microsoft365Group, GraphAccessToken);

                                            foreach (var microsoft365GroupOwner in microsoft365GroupOwners)
                                            {
                                                results.Add(new UserRightItem
                                                {
                                                    ResourcePath = CurrentWeb.Url,
                                                    UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                    ResourceType = UserRightsResourceType.Web,
                                                    Upn = microsoft365GroupOwner.UserPrincipalName,
                                                    Email = null,
                                                    Name = microsoft365GroupOwner.DisplayName,
                                                    Permission = "Owner",
                                                    IsGuest = microsoft365GroupOwner.UserPrincipalName.Contains("#EXT#")
                                                });
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            //var usersWithDirectPermissions = CurrentWeb.SiteUsers.Where(u => CurrentWeb.RoleAssignments.Any(ra => ra.Member.LoginName == u.LoginName));

            //GetPermissions(CurrentWeb.RoleAssignments, CurrentWeb.Url, UserRightsResourceType.Web, UserRightsGivenThroughType.DirectAssignment);

            // Get all the users contained in SharePoint Groups
            
            

            // Get all SharePoint groups that have been assigned access
            // var usersWithGroupPermissions = new List<User>();
            // foreach (var group in CurrentWeb.SiteGroups.Where(g => CurrentWeb.RoleAssignments.Any(ra => ra.Member.LoginName == g.LoginName)))
            // {
            //     usersWithGroupPermissions.AddRange(group.Users);
            // }

            // // Merge the users with rights directly on the site level and those assigned rights through SharePoint Groups
            // var allUsersWithPermissions = new List<User>(usersWithDirectPermissions.Count() + usersWithGroupPermissions.Count());
            // allUsersWithPermissions.AddRange(usersWithDirectPermissions);
            // allUsersWithPermissions.AddRange(usersWithGroupPermissions);

            // // Add the found users and add them to the custom object
            // CurrentWeb.Context.Load(CurrentWeb, s => s.ServerRelativeUrl);
            // CurrentWeb.Context.ExecuteQueryRetry();

            // results.AddRange(GetPermissions(CurrentWeb.RoleAssignments, CurrentWeb.Url, UserRightsResourceType.Web, UserRightsGivenThroughType.DirectAssignment));

            WriteObject(results, true);
            // foreach (var user in allUsersWithPermissions)
            // {
            //     results.Add(new UserRightItem
            //     {
            //         Groups = user.Groups,
            //         User = user,
            //         Url = CurrentWeb.ServerRelativeUrl
            //     });
            // }

            //     CurrentWeb.Context.Load(CurrentWeb.Lists, l => l.Include(li => li.ItemCount, li => li.IsSystemList, li=>li.IsCatalog, li => li.RootFolder.ServerRelativeUrl, li => li.RoleAssignments, li => li.Title, li => li.HasUniqueRoleAssignments));
            //     CurrentWeb.Context.ExecuteQueryRetry();

            //     var progress = new ProgressRecord(0, $"Getting lists for {CurrentWeb.ServerRelativeUrl}", "Enumerating through lists");
            //     var progressCounter = 0;

            //     foreach (var list in CurrentWeb.Lists)
            //     {
            //         WriteProgress(progress, $"Getting list {list.RootFolder.ServerRelativeUrl}", progressCounter++, CurrentWeb.Lists.Count);

            //         // ignoring the system lists
            //         if (list.IsSystemList || list.IsCatalog)
            //         {
            //             continue;
            //         }

            //         // if a list or a library has unique permissions then proceed
            //         if (list.HasUniqueRoleAssignments)
            //         {
            //             WriteVerbose(string.Format("List found with HasUniqueRoleAssignments {0}", list.RootFolder.ServerRelativeUrl));
            //             string url = list.RootFolder.ServerRelativeUrl;

            //             CurrentWeb.Context.Load(list.RoleAssignments, r => r.Include(
            //                 ra => ra.RoleDefinitionBindings,
            //                 ra => ra.Member.LoginName,
            //                 ra => ra.Member.Title,
            //                 ra => ra.Member.PrincipalType));
            //             CurrentWeb.Context.ExecuteQueryRetry();

            //             results.AddRange(GetPermissions(list.RoleAssignments, url));

            //             // if the list with unique permissions also has items, check every item which is uniquely permissioned
            //             if (list.ItemCount > 0)
            //             {
            //                 WriteVerbose(string.Format("Enumerating through all listitems of {0}", list.RootFolder.ServerRelativeUrl));

            //                 CamlQuery query = CamlQuery.CreateAllItemsQuery();
            //                 var queryElement = XElement.Parse(query.ViewXml);

            //                 var rowLimit = queryElement.Descendants("RowLimit").FirstOrDefault();
            //                 if (rowLimit != null)
            //                 {
            //                     rowLimit.RemoveAll();
            //                 }
            //                 else
            //                 {
            //                     rowLimit = new XElement("RowLimit");
            //                     queryElement.Add(rowLimit);
            //                 }

            //                 rowLimit.SetAttributeValue("Paged", "TRUE");
            //                 rowLimit.SetValue(1000);

            //                 query.ViewXml = queryElement.ToString();

            //                 List<ListItemCollection> items = new List<ListItemCollection>();

            //                 do
            //                 {
            //                     var listItems = list.GetItems(query);
            //                     CurrentWeb.Context.Load(listItems);
            //                     CurrentWeb.Context.ExecuteQueryRetry();
            //                     query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;

            //                     items.Add(listItems);

            //                 } while (query.ListItemCollectionPosition != null);

            //                 // Progress bar for item enumerations
            //                 var itemProgress = new ProgressRecord(0, $"Getting items for {list.RootFolder.ServerRelativeUrl}", "Enumerating through items");
            //                 var itemProgressCounter = 0;

            //                 foreach (var item in items)
            //                 {
            //                     WriteProgress(itemProgress, $"Retrieving items", itemProgressCounter++, items.Count);

            //                     WriteVerbose(string.Format("Enumerating though listitemcollections"));
            //                     foreach (var listItem in item)
            //                     {
            //                         WriteVerbose(string.Format("Enumerating though listitems"));
            //                         listItem.EnsureProperty(i => i.HasUniqueRoleAssignments);

            //                         if (listItem.HasUniqueRoleAssignments)
            //                         {
            //                             string listItemUrl = listItem["FileRef"].ToString();
            //                             WriteVerbose(string.Format("List item {0} HasUniqueRoleAssignments", listItemUrl));

            //                             CurrentWeb.Context.Load(listItem.RoleAssignments, r => r.Include(
            //                                 ra => ra.RoleDefinitionBindings,
            //                                 ra => ra.Member.LoginName,
            //                                 ra => ra.Member.Title,
            //                                 ra => ra.Member.PrincipalType));
            //                             CurrentWeb.Context.ExecuteQueryRetry();

            //                             results.AddRange(GetPermissions(listItem.RoleAssignments, listItemUrl));
            //                         }
            //                     }
            //                 }
            //                 itemProgress.RecordType = ProgressRecordType.Completed;
            //                 WriteProgress(itemProgress);
            //             }
            //         }
            //         progress.RecordType = ProgressRecordType.Completed;
            //         WriteProgress(progress);
            //     }

            //     // Fetch all the unique users from everything that has been collected
            //     var uniqueUsers = (from u in results
            //                         select u.User.LoginName).Distinct();

            //     // Looping through each user, getting all the details like specific permissions and groups an user belongs to
            //     foreach (var uniqueUser in uniqueUsers)
            //     {
            //         // Getting all the assigned permissions per user
            //         var userPermissions = (from u in results
            //                             where u.User.LoginName == uniqueUser && u.Permissions != null
            //                             select u).ToList();

            //         // Making the permissions readable by getting the name of the permission and the URL of the artifact
            //         Dictionary<string, string> Permissions = new Dictionary<string, string>();
            //         foreach (var userPermission in userPermissions)
            //         {
            //             StringBuilder stringBuilder = new StringBuilder();
            //             foreach (var permissionMask in userPermission.Permissions)
            //             {
            //                 stringBuilder.Append(permissionMask);
            //             }
            //             Permissions.Add(userPermission.Url, stringBuilder.ToString());
            //         }

            //         // Getting all the groups where the user is added to
            //         var groupsMemberships = (from u in results
            //                         where u.User.LoginName == uniqueUser && u.Groups != null
            //                         select u.Groups).ToList();

            //         // Getting the titles of the all the groups
            //         List<string> Groups = new List<string>();
            //         foreach (var groupMembership in groupsMemberships)
            //         {
            //             foreach (var group in groupMembership)
            //             {
            //                 Groups.Add(group.Title);
            //             }
            //         }

            //         // Getting the User object of the user so we can get to the title, loginname, etc
            //         var userInformation = (from u in results
            //                                 where u.Upn == uniqueUser
            //                                 select u.User).FirstOrDefault();

            //         WriteObject(new { userInformation.Title, userInformation.LoginName, userInformation.Email, Groups, Permissions }, true);
            //     }
        }

        private void WriteProgress(ProgressRecord record, string message, int step, int count)
        {
            var percentage = Convert.ToInt32((100 / Convert.ToDouble(count)) * Convert.ToDouble(step));
            record.StatusDescription = message;
            record.PercentComplete = percentage;
            record.RecordType = ProgressRecordType.Processing;
            WriteProgress(record);
        }

        private static IEnumerable<UserRightItem> GetPermissions(RoleAssignmentCollection roleAssignments, string url, UserRightsResourceType resourceType, UserRightsGivenThroughType rightsGivenThrough)
        {
            List<UserRightItem> userRightItems = new List<UserRightItem>();
            foreach (var roleAssignment in roleAssignments)
            {
                if (roleAssignment.Member.PrincipalType != Microsoft.SharePoint.Client.Utilities.PrincipalType.User) continue;

                var user = roleAssignment.Member as User;
                foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                {
                    if (roleDefinition.Name == "Limited Access") continue;

                    userRightItems.Add(new UserRightItem
                    {
                        ResourcePath = url,
                        UserRightsGivenThrough = rightsGivenThrough,
                        ResourceType = resourceType,
                        Upn = user.UserPrincipalName,
                        Email = user.Email,
                        Name = user.Title,
                        Permission = roleDefinition.Name
                    });
                }
            }
            return userRightItems;
        }
    }
}
