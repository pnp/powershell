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

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPUser", DefaultParameterSetName = PARAMETERSET_IDENTITY)]
    [OutputType(typeof(User), ParameterSetName = new[] { PARAMETERSET_IDENTITY, PARAMETERSET_WITHRIGHTSASSIGNED })]
    [OutputType(typeof(Model.UserWithRightsAssignedDetailed), ParameterSetName = new[] { PARAMETERSET_WITHRIGHTSASSIGNEDDETAILED })]
    public class GetUser : PnPWebRetrievalsCmdlet<User>
    {
        private const string PARAMETERSET_IDENTITY = "Identity based request";
        private const string PARAMETERSET_WITHRIGHTSASSIGNED = "With rights assigned";
        private const string PARAMETERSET_WITHRIGHTSASSIGNEDDETAILED = "With rights assigned detailed";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = PARAMETERSET_IDENTITY)]
        public UserPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_WITHRIGHTSASSIGNED)]
        public SwitchParameter WithRightsAssigned;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_WITHRIGHTSASSIGNEDDETAILED)]
        public SwitchParameter WithRightsAssignedDetailed;

        /// <summary>
        /// Output type used with parameter WithRightsAssignedDetailed
        /// </summary>
        public class DetailedUser
        {
            public User User { get; set; }
            public string Url { get; set; }
            public List<string> Permissions { get; set; }
            public GroupCollection Groups { get; set; }
        }

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<User, object>>[]
            {
                u => u.Id,
                u => u.Title,
                u => u.LoginName,
                u => u.Email,
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

            if (Identity == null)
            {
                CurrentWeb.Context.Load(CurrentWeb.SiteUsers, u => u.Include(RetrievalExpressions));

                List<DetailedUser> users = new List<DetailedUser>();

                if (WithRightsAssigned
                    || WithRightsAssignedDetailed
                    )
                {
                    // Get all the role assignments and role definition bindings to be able to see which users have been given rights directly on the site level
                    CurrentWeb.Context.Load(CurrentWeb.RoleAssignments, ac => ac.Include(a => a.RoleDefinitionBindings, a => a.Member));
                    var usersWithDirectPermissions = CurrentWeb.SiteUsers.Where(u => CurrentWeb.RoleAssignments.Any(ra => ra.Member.LoginName == u.LoginName));

                    // Get all the users contained in SharePoint Groups
                    CurrentWeb.Context.Load(CurrentWeb.SiteGroups, sg => sg.Include(u => u.Users.Include(RetrievalExpressions), u => u.LoginName));
                    CurrentWeb.Context.ExecuteQueryRetry();

                    // Get all SharePoint groups that have been assigned access
                    var usersWithGroupPermissions = new List<User>();
                    foreach (var group in CurrentWeb.SiteGroups.Where(g => CurrentWeb.RoleAssignments.Any(ra => ra.Member.LoginName == g.LoginName)))
                    {
                        usersWithGroupPermissions.AddRange(group.Users);
                    }

                    // Merge the users with rights directly on the site level and those assigned rights through SharePoint Groups
                    var allUsersWithPermissions = new List<User>(usersWithDirectPermissions.Count() + usersWithGroupPermissions.Count());
                    allUsersWithPermissions.AddRange(usersWithDirectPermissions);
                    allUsersWithPermissions.AddRange(usersWithGroupPermissions);

                    // Add the found users and add them to the custom object
                    if (WithRightsAssignedDetailed)
                    {
                        CurrentWeb.Context.Load(CurrentWeb, s => s.ServerRelativeUrl);
                        CurrentWeb.Context.ExecuteQueryRetry();

                        LogWarning("Using the -WithRightsAssignedDetailed parameter will cause the script to take longer than normal because of the all enumerations that take place");
                        users.AddRange(GetPermissions(CurrentWeb.RoleAssignments, CurrentWeb.ServerRelativeUrl));
                        foreach (var user in allUsersWithPermissions)
                        {
                            users.Add(new DetailedUser()
                            {
                                Groups = user.Groups,
                                User = user,
                                Url = CurrentWeb.ServerRelativeUrl
                            });
                        }
                    }
                    else
                    {
                        // Filter out the users that have been given rights at both places so they will only be returned once
                        WriteObject(allUsersWithPermissions.GroupBy(u => u.Id).Select(u => u.First()), true);
                    }
                }
                else
                {
                    CurrentWeb.Context.ExecuteQueryRetry();
                    WriteObject(CurrentWeb.SiteUsers, true);
                }

                if (WithRightsAssignedDetailed)
                {
                    CurrentWeb.Context.Load(CurrentWeb.Lists, l => l.Include(li => li.ItemCount, li => li.IsSystemList, li => li.IsCatalog, li => li.RootFolder.ServerRelativeUrl, li => li.RoleAssignments, li => li.Title, li => li.HasUniqueRoleAssignments));
                    CurrentWeb.Context.ExecuteQueryRetry();

                    var progress = new ProgressRecord(0, $"Getting lists for {CurrentWeb.ServerRelativeUrl}", "Enumerating through lists");
                    var progressCounter = 0;

                    foreach (var list in CurrentWeb.Lists)
                    {
                        WriteProgress(progress, $"Getting list {list.RootFolder.ServerRelativeUrl}", progressCounter++, CurrentWeb.Lists.Count);

                        // ignoring the system lists
                        if (list.IsSystemList || list.IsCatalog)
                        {
                            continue;
                        }

                        // if a list or a library has unique permissions then proceed
                        if (list.HasUniqueRoleAssignments)
                        {
                            LogDebug(string.Format("List found with HasUniqueRoleAssignments {0}", list.RootFolder.ServerRelativeUrl));
                            string url = list.RootFolder.ServerRelativeUrl;

                            CurrentWeb.Context.Load(list.RoleAssignments, r => r.Include(
                                ra => ra.RoleDefinitionBindings,
                                ra => ra.Member.LoginName,
                                ra => ra.Member.Title,
                                ra => ra.Member.PrincipalType));
                            CurrentWeb.Context.ExecuteQueryRetry();

                            users.AddRange(GetPermissions(list.RoleAssignments, url));

                            // if the list with unique permissions also has items, check every item which is uniquely permissioned
                            if (list.ItemCount > 0)
                            {
                                LogDebug(string.Format("Enumerating through all listitems of {0}", list.RootFolder.ServerRelativeUrl));

                                CamlQuery query = CamlQuery.CreateAllItemsQuery();
                                var queryElement = XElement.Parse(query.ViewXml);

                                var rowLimit = queryElement.Descendants("RowLimit").FirstOrDefault();
                                if (rowLimit != null)
                                {
                                    rowLimit.RemoveAll();
                                }
                                else
                                {
                                    rowLimit = new XElement("RowLimit");
                                    queryElement.Add(rowLimit);
                                }

                                rowLimit.SetAttributeValue("Paged", "TRUE");
                                rowLimit.SetValue(1000);

                                query.ViewXml = queryElement.ToString();

                                List<ListItemCollection> items = new List<ListItemCollection>();

                                do
                                {
                                    var listItems = list.GetItems(query);
                                    CurrentWeb.Context.Load(listItems);
                                    CurrentWeb.Context.ExecuteQueryRetry();
                                    query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;

                                    items.Add(listItems);

                                } while (query.ListItemCollectionPosition != null);

                                // Progress bar for item enumerations
                                var itemProgress = new ProgressRecord(0, $"Getting items for {list.RootFolder.ServerRelativeUrl}", "Enumerating through items");
                                var itemProgressCounter = 0;

                                foreach (var item in items)
                                {
                                    WriteProgress(itemProgress, $"Retrieving items", itemProgressCounter++, items.Count);

                                    LogDebug(string.Format("Enumerating though listitemcollections"));
                                    foreach (var listItem in item)
                                    {
                                        LogDebug(string.Format("Enumerating though listitems"));
                                        listItem.EnsureProperty(i => i.HasUniqueRoleAssignments);

                                        if (listItem.HasUniqueRoleAssignments)
                                        {
                                            string listItemUrl = listItem["FileRef"].ToString();
                                            LogDebug(string.Format("List item {0} HasUniqueRoleAssignments", listItemUrl));

                                            CurrentWeb.Context.Load(listItem.RoleAssignments, r => r.Include(
                                                ra => ra.RoleDefinitionBindings,
                                                ra => ra.Member.LoginName,
                                                ra => ra.Member.Title,
                                                ra => ra.Member.PrincipalType));
                                            CurrentWeb.Context.ExecuteQueryRetry();

                                            users.AddRange(GetPermissions(listItem.RoleAssignments, listItemUrl));
                                        }
                                    }
                                }
                                itemProgress.RecordType = ProgressRecordType.Completed;
                                WriteProgress(itemProgress);
                            }
                        }
                        progress.RecordType = ProgressRecordType.Completed;
                        WriteProgress(progress);
                    }

                    // Fetch all the unique users from everything that has been collected
                    var uniqueUsers = (from u in users
                                       select u.User.LoginName).Distinct();

                    // Looping through each user, getting all the details like specific permissions and groups an user belongs to
                    foreach (var uniqueUser in uniqueUsers)
                    {
                        // Getting all the assigned permissions per user
                        var userPermissions = (from u in users
                                               where u.User.LoginName == uniqueUser && u.Permissions != null
                                               select u).ToList();

                        // Making the permissions readable by getting the name of the permission and the URL of the artifact
                        Dictionary<string, string> Permissions = new Dictionary<string, string>();
                        foreach (var userPermission in userPermissions)
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            foreach (var permissionMask in userPermission.Permissions)
                            {
                                stringBuilder.Append(permissionMask);
                            }
                            Permissions.Add(userPermission.Url, stringBuilder.ToString());
                        }

                        // Getting all the groups where the user is added to
                        var groupsMemberships = (from u in users
                                                 where u.User.LoginName == uniqueUser && u.Groups != null
                                                 select u.Groups).ToList();

                        // Getting the titles of the all the groups
                        List<string> Groups = new List<string>();
                        foreach (var groupMembership in groupsMemberships)
                        {
                            foreach (var group in groupMembership)
                            {
                                Groups.Add(group.Title);
                            }
                        }

                        // Getting the User object of the user so we can get to the title, loginname, etc
                        var userInformation = (from u in users
                                               where u.User.LoginName == uniqueUser
                                               select u.User).FirstOrDefault();

                        WriteObject(new Model.UserWithRightsAssignedDetailed
                        {
                            Title = userInformation.Title,
                            LoginName = userInformation.LoginName,
                            Email = userInformation.Email,
                            Groups = Groups,
                            Permissions = Permissions
                        });
                    }
                }
            }
            else
            {
                var user = Identity.GetUser(ClientContext, retrievalOptions: RetrievalExpressions);
                WriteObject(user);
            }
        }

        private void WriteProgress(ProgressRecord record, string message, int step, int count)
        {
            var percentage = Convert.ToInt32((100 / Convert.ToDouble(count)) * Convert.ToDouble(step));
            record.StatusDescription = message;
            record.PercentComplete = percentage;
            record.RecordType = ProgressRecordType.Processing;
            WriteProgress(record);
        }

        private static List<DetailedUser> GetPermissions(RoleAssignmentCollection roleAssignments, string url)
        {
            List<DetailedUser> users = new List<DetailedUser>();
            foreach (var roleAssignment in roleAssignments)
            {
                if (roleAssignment.Member.PrincipalType == Microsoft.SharePoint.Client.Utilities.PrincipalType.User)
                {
                    var detailedUser = new DetailedUser();
                    detailedUser.Url = url;
                    detailedUser.User = roleAssignment.Member as User;
                    detailedUser.Permissions = new List<string>();

                    foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                    {
                        if (roleDefinition.Name == "Limited Access")
                            continue;

                        detailedUser.Permissions.Add(roleDefinition.Name);
                    }

                    // if no permissions are recorded (hence, limited access, skip the adding of the permissions)
                    if (detailedUser.Permissions.Count == 0)
                        continue;

                    users.Add(detailedUser);
                }
            }
            return users;
        }
    }
}
