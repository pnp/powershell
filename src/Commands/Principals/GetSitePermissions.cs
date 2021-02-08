using System.Management.Automation;
using Microsoft.SharePoint.Client;

using System.Linq.Expressions;
using System;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPSitePermissions")]
    public class GetSitePermissions : PnPWebCmdlet
    {
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
            ListItem,

            /// <summary>
            /// A SharePoint document in a document library
            /// </summary>
            Document       
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
            SharePointGroup,

            /// <summary>
            /// Permissions granted via an Azure Active Directory security group
            /// </summary>
            SecurityGroup,

            /// <summary>
            /// Permissions granted via an Azure Active Directory distribution list
            /// </summary>
            DistributionList,

            /// <summary>
            /// Permissions granted via a sharing link
            /// </summary>
            SharingLink                   
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
            public UserRightsGivenThroughType UserRightsGivenThrough { get; set; }

            /// <summary>
            /// Name of the group through which permissions have been given, if applicable
            /// </summary>
            public string GroupName { get; set; }            

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
                    return TokenHandler.GetManagedIdentityTokenAsync(this, HttpClient, "https://graph.microsoft.com/").GetAwaiter().GetResult();
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
                u => u.PrincipalType,
                u => u.Groups.Include(
                    g => g.Id,
                    g => g.Title,
                    g => g.LoginName)
            };

            CurrentWeb.Context.Load(CurrentWeb.SiteUsers, u => u.Include(userExpressions));

            var results = new List<UserRightItem>();

            // Get all the role assignments and role definition bindings to be able to see which users have been given rights directly on the site level
            WriteVerbose("Retrieving permissions of current site");
            CurrentWeb.Context.Load(CurrentWeb.RoleAssignments, ac => ac.Include(a => a.RoleDefinitionBindings, a => a.Member.LoginName, a => a.Member.PrincipalType));
            CurrentWeb.Context.Load(CurrentWeb.SiteGroups, sg => sg.Include(u => u.Id, u => u.Title, u => u.LoginName, u => u.Users.Include(userExpressions)));
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
                        // Skip the Limited Access role in the reports as they merely mean specific roles are defined at a lower level
                        if (roleDefinition.Name == "Limited Access") continue;

                        foreach (var groupUser in group.Users)
                        {
                            // Skip the SharePoint System user in the reports
                            if (groupUser.LoginName == "SHAREPOINT\\system") continue;

                            switch (roleAssignment.Member.PrincipalType)
                            {
                                case Microsoft.SharePoint.Client.Utilities.PrincipalType.User:
                                    results.Add(new UserRightItem
                                    {
                                        ResourcePath = CurrentWeb.Url,
                                        UserRightsGivenThrough = UserRightsGivenThroughType.DirectAssignment,
                                        ResourceType = UserRightsResourceType.Web,
                                        GroupName = group.Title,
                                        Upn = groupUser.UserPrincipalName,
                                        Email = groupUser.Email,
                                        Name = groupUser.Title,
                                        Permission = groupUser.IsSiteAdmin ? "Site collection administrator" : roleDefinition.Name,
                                        IsGuest = groupUser.IsShareByEmailGuestUser
                                    });
                                    break;

                                case Microsoft.SharePoint.Client.Utilities.PrincipalType.SharePointGroup:
                                    if (groupUser.AadObjectId != null && groupUser.LoginName.Contains("|federateddirectoryclaimprovider|"))
                                    {
                                        // Microsoft 365 Group
                                        var microsoft365Group = UnifiedGroupsUtility.GetUnifiedGroup(groupUser.AadObjectId.NameId, GraphAccessToken, includeSite: false);

                                        if (groupUser.Title.EndsWith("Members"))
                                        {
                                            // Microsoft 365 Group Members
                                            var microsoft365GroupMembers = UnifiedGroupsUtility.GetUnifiedGroupMembers(microsoft365Group, GraphAccessToken);

                                            foreach (var microsoft365GroupMember in microsoft365GroupMembers)
                                            {
                                                results.Add(new UserRightItem
                                                {
                                                    ResourcePath = CurrentWeb.Url,
                                                    UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                    ResourceType = UserRightsResourceType.Web,
                                                    GroupName = microsoft365Group.DisplayName,
                                                    Upn = microsoft365GroupMember.UserPrincipalName,
                                                    Email = microsoft365GroupMember.Email,
                                                    Name = microsoft365GroupMember.DisplayName,
                                                    Permission = "Member",
                                                    IsGuest = microsoft365GroupMember.UserPrincipalName.Contains("#EXT#")
                                                });
                                            }
                                        }
                                        else
                                        {
                                            // Microsoft 365 Group Owners
                                            var microsoft365GroupOwners = UnifiedGroupsUtility.GetUnifiedGroupOwners(microsoft365Group, GraphAccessToken);

                                            foreach (var microsoft365GroupOwner in microsoft365GroupOwners)
                                            {
                                                results.Add(new UserRightItem
                                                {
                                                    ResourcePath = CurrentWeb.Url,
                                                    UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                    ResourceType = UserRightsResourceType.Web,
                                                    GroupName = microsoft365Group.DisplayName,
                                                    Upn = microsoft365GroupOwner.UserPrincipalName,
                                                    Email = microsoft365GroupOwner.Email,
                                                    Name = microsoft365GroupOwner.DisplayName,
                                                    Permission = "Owner",
                                                    IsGuest = microsoft365GroupOwner.UserPrincipalName.Contains("#EXT#")
                                                });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // User in a SharePoint Group
                                        results.Add(new UserRightItem
                                        {
                                            ResourcePath = CurrentWeb.Url,
                                            UserRightsGivenThrough = UserRightsGivenThroughType.SharePointGroup,
                                            ResourceType = UserRightsResourceType.Web,
                                            GroupName = group.Title,
                                            Upn = groupUser.UserPrincipalName,
                                            Email = groupUser.LoginName,
                                            Name = groupUser.Title,
                                            Permission = groupUser.IsSiteAdmin ? "Site collection administrator" : roleDefinition.Name,
                                            IsGuest = groupUser.IsShareByEmailGuestUser
                                        });
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            // SharePoint Lists
            WriteVerbose("Retrieving permissions of the lists in the current site");
            CurrentWeb.Context.Load(CurrentWeb.Lists, l => l.Include(li => li.ItemCount, li => li.IsSystemList, li => li.BaseType, li => li.IsCatalog, li => li.RoleAssignments.Include(ra => ra.RoleDefinitionBindings, ra => ra.Member), li => li.Title, li => li.HasUniqueRoleAssignments));
            CurrentWeb.Context.ExecuteQueryRetry();

            foreach (var list in CurrentWeb.Lists)
            {
                // Ignoring the system lists
                if (list.IsSystemList || list.IsCatalog) continue;

                // If a list or a library does not have unique permissions, thus inherits from its parent, then proceed with the next list
                if (!list.HasUniqueRoleAssignments) continue;

                // Construct the full URL to the SharePoint List
                var fullListUrl = $"{CurrentWeb.Context.Url}/{list.GetWebRelativeUrl()}";

                WriteVerbose($"Retrieving permissions for the list at {fullListUrl}");

                foreach (var roleAssignment in list.RoleAssignments)
                {
                    switch (roleAssignment.Member.PrincipalType)
                    {
                        case Microsoft.SharePoint.Client.Utilities.PrincipalType.User:
                            var user = roleAssignment.Member as User;
                            foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                            {
                                if (roleDefinition.Name == "Limited Access") continue;

                                // User with direct assignments
                                results.Add(new UserRightItem
                                {
                                    ResourcePath = fullListUrl,
                                    UserRightsGivenThrough = UserRightsGivenThroughType.DirectAssignment,
                                    ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.DocumentLibrary : UserRightsResourceType.List,
                                    Upn = user.UserPrincipalName,
                                    Email = user.LoginName,
                                    Name = user.Title,
                                    Permission = user.IsSiteAdmin ? "Site collection administrator" : roleDefinition.Name,
                                    IsGuest = user.IsShareByEmailGuestUser
                                });
                            }
                            break;

                            case Microsoft.SharePoint.Client.Utilities.PrincipalType.SharePointGroup:
                                var group = roleAssignment.Member as Group;
                                if (group.LoginName.Contains("|federateddirectoryclaimprovider|"))
                                {
                                    // Microsoft 365 Group
                                    var groupGuid = group.LoginName.Remove(0, group.LoginName.LastIndexOf('|') + 1);

                                    var microsoft365Group = UnifiedGroupsUtility.GetUnifiedGroup(groupGuid, GraphAccessToken, includeSite: false);

                                    if (group.Title.EndsWith("Members"))
                                    {
                                        // Microsoft 365 Group Members
                                        var microsoft365GroupMembers = UnifiedGroupsUtility.GetUnifiedGroupMembers(microsoft365Group, GraphAccessToken);

                                        foreach (var microsoft365GroupMember in microsoft365GroupMembers)
                                        {
                                            results.Add(new UserRightItem
                                            {
                                                ResourcePath = fullListUrl,
                                                UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.DocumentLibrary : UserRightsResourceType.List,
                                                GroupName = microsoft365Group.DisplayName,
                                                Upn = microsoft365GroupMember.UserPrincipalName,
                                                Email = microsoft365GroupMember.Email,
                                                Name = microsoft365GroupMember.DisplayName,
                                                Permission = "Member",
                                                IsGuest = microsoft365GroupMember.UserPrincipalName.Contains("#EXT#")
                                            });
                                        }
                                    }
                                    else
                                    {
                                        // Microsoft 365 Group Owners
                                        var microsoft365GroupOwners = UnifiedGroupsUtility.GetUnifiedGroupOwners(microsoft365Group, GraphAccessToken);

                                        foreach (var microsoft365GroupOwner in microsoft365GroupOwners)
                                        {
                                            results.Add(new UserRightItem
                                            {
                                                ResourcePath = fullListUrl,
                                                UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.DocumentLibrary : UserRightsResourceType.List,
                                                GroupName = microsoft365Group.DisplayName,
                                                Upn = microsoft365GroupOwner.UserPrincipalName,
                                                Email = microsoft365GroupOwner.Email,
                                                Name = microsoft365GroupOwner.DisplayName,
                                                Permission = "Owner",
                                                IsGuest = microsoft365GroupOwner.UserPrincipalName.Contains("#EXT#")
                                            });
                                        }
                                    }
                                }
                                else
                                {
                                    // User in a SharePoint Group
                                    foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                                    {
                                        if (roleDefinition.Name == "Limited Access") continue;

                                        // Get the SharePoint Group
                                        var siteGroup = CurrentWeb.SiteGroups.FirstOrDefault(sg => sg.Id == group.Id);
                                        if(siteGroup == null) continue;

                                        foreach(var groupUser in siteGroup.Users)
                                        {
                                            // Skip the SharePoint System user in the reports
                                            if (groupUser.LoginName == "SHAREPOINT\\system") continue;
                                           
                                            if (groupUser.AadObjectId != null && groupUser.LoginName.Contains("|federateddirectoryclaimprovider|"))
                                            {
                                                // Microsoft 365 Group
                                                var microsoft365Group = UnifiedGroupsUtility.GetUnifiedGroup(groupUser.AadObjectId.NameId, GraphAccessToken, includeSite: false);

                                                if (groupUser.Title.EndsWith("Members"))
                                                {
                                                    // Microsoft 365 Group Members
                                                    var microsoft365GroupMembers = UnifiedGroupsUtility.GetUnifiedGroupMembers(microsoft365Group, GraphAccessToken);

                                                    foreach (var microsoft365GroupMember in microsoft365GroupMembers)
                                                    {
                                                        results.Add(new UserRightItem
                                                        {
                                                            ResourcePath = fullListUrl,
                                                            UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                            ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.DocumentLibrary : UserRightsResourceType.List,
                                                            GroupName = microsoft365Group.DisplayName,
                                                            Upn = microsoft365GroupMember.UserPrincipalName,
                                                            Email = microsoft365GroupMember.Email,
                                                            Name = microsoft365GroupMember.DisplayName,
                                                            Permission = "Member",
                                                            IsGuest = microsoft365GroupMember.UserPrincipalName.Contains("#EXT#")
                                                        });
                                                    }
                                                }
                                                else
                                                {
                                                    // Microsoft 365 Group Owners
                                                    var microsoft365GroupOwners = UnifiedGroupsUtility.GetUnifiedGroupOwners(microsoft365Group, GraphAccessToken);

                                                    foreach (var microsoft365GroupOwner in microsoft365GroupOwners)
                                                    {
                                                        results.Add(new UserRightItem
                                                        {
                                                            ResourcePath = fullListUrl,
                                                            UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                            ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.DocumentLibrary : UserRightsResourceType.List,
                                                            GroupName = microsoft365Group.DisplayName,
                                                            Upn = microsoft365GroupOwner.UserPrincipalName,
                                                            Email = microsoft365GroupOwner.Email,
                                                            Name = microsoft365GroupOwner.DisplayName,
                                                            Permission = "Owner",
                                                            IsGuest = microsoft365GroupOwner.UserPrincipalName.Contains("#EXT#")
                                                        });
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // User in a SharePoint Group
                                                results.Add(new UserRightItem
                                                {
                                                    ResourcePath = fullListUrl,
                                                    UserRightsGivenThrough = UserRightsGivenThroughType.SharePointGroup,
                                                    ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.DocumentLibrary : UserRightsResourceType.List,
                                                    GroupName = group.Title,
                                                    Upn = groupUser.UserPrincipalName,
                                                    Email = groupUser.LoginName,
                                                    Name = groupUser.Title,
                                                    Permission = groupUser.IsSiteAdmin ? "Site collection administrator" : roleDefinition.Name,
                                                    IsGuest = groupUser.IsShareByEmailGuestUser
                                                });
                                            }                                            
                                        }
                                    }
                                }
                                break;
                        case Microsoft.SharePoint.Client.Utilities.PrincipalType.SecurityGroup:
                        case Microsoft.SharePoint.Client.Utilities.PrincipalType.DistributionList:
                            var groupId = roleAssignment.Member.LoginName.Remove(0, roleAssignment.Member.LoginName.LastIndexOf('|') + 1);

                            // Group
                            var securityGroup = UnifiedGroupsUtility.GetUnifiedGroup(groupId, GraphAccessToken, includeSite: false);

                            // Group Members
                            var groupMembers = UnifiedGroupsUtility.GetUnifiedGroupMembers(securityGroup, GraphAccessToken);

                            foreach (var groupMember in groupMembers)
                            {
                                foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                                {
                                    if (roleDefinition.Name == "Limited Access") continue;

                                    results.Add(new UserRightItem
                                    {
                                        ResourcePath = fullListUrl,
                                        UserRightsGivenThrough = roleAssignment.Member.PrincipalType == Microsoft.SharePoint.Client.Utilities.PrincipalType.SecurityGroup ? UserRightsGivenThroughType.SecurityGroup : UserRightsGivenThroughType.DistributionList,
                                        ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.DocumentLibrary : UserRightsResourceType.List,
                                        GroupName = securityGroup.DisplayName,
                                        Upn = groupMember.UserPrincipalName,
                                        Email = groupMember.Email,
                                        Name = groupMember.DisplayName,
                                        Permission = roleDefinition.Name,
                                        IsGuest = groupMember.UserPrincipalName.Contains("#EXT#")
                                    });
                                }
                            }
                            break;

                    }
                }

                // List Item Permissions
                if (list.ItemCount == 0) continue;

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

                var items = new List<ListItemCollection>();

                do
                {
                    var listItems = list.GetItems(query);
                    CurrentWeb.Context.Load(listItems);
                    CurrentWeb.Context.ExecuteQueryRetry();
                    query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;

                    items.Add(listItems);

                } while (query.ListItemCollectionPosition != null);

                foreach (var item in items)
                {
                    foreach (var listItem in item)
                    {
                        listItem.EnsureProperty(i => i.HasUniqueRoleAssignments);

                        if (listItem.HasUniqueRoleAssignments)
                        {
                            // Construct the full URL to the DispForm of the list item or to the document in a document library
                            var listItemUrl = $"{fullListUrl}/{(list.BaseType == BaseType.DocumentLibrary ? listItem["FileRef"].ToString().Remove(0, listItem["FileRef"].ToString().LastIndexOf('/') + 1) : $"DispForm.aspx?ID={listItem.Id}")}";
                            
                            WriteVerbose($"Retrieving permissions for list item at {listItemUrl}");

                            CurrentWeb.Context.Load(listItem, li => li["EncodedAbsUrl"]);
                            CurrentWeb.Context.Load(listItem.RoleAssignments, r => r.Include(ra => ra.RoleDefinitionBindings, ra => ra.Member));
                            CurrentWeb.Context.ExecuteQueryRetry();

                            foreach (var roleAssignment in listItem.RoleAssignments)
                            {
                                switch (roleAssignment.Member.PrincipalType)
                                {
                                    case Microsoft.SharePoint.Client.Utilities.PrincipalType.User:
                                        var user = roleAssignment.Member as User;
                                        foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                                        {
                                            if (roleDefinition.Name == "Limited Access") continue;

                                            // User with direct assignments
                                            results.Add(new UserRightItem
                                            {
                                                ResourcePath = listItemUrl,
                                                UserRightsGivenThrough = UserRightsGivenThroughType.DirectAssignment,
                                                ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.Document : UserRightsResourceType.ListItem,
                                                Upn = user.UserPrincipalName,
                                                Email = user.LoginName,
                                                Name = user.Title,
                                                Permission = user.IsSiteAdmin ? "Site collection administrator" : roleDefinition.Name,
                                                IsGuest = user.IsShareByEmailGuestUser
                                            });
                                        }
                                        break;

                                        case Microsoft.SharePoint.Client.Utilities.PrincipalType.SharePointGroup:
                                            var group = roleAssignment.Member as Group;
                                            if (group.LoginName.Contains("|federateddirectoryclaimprovider|"))
                                            {
                                                // Microsoft 365 Group
                                                var groupGuid = group.LoginName.Remove(0, group.LoginName.LastIndexOf('|') + 1);

                                                var microsoft365Group = UnifiedGroupsUtility.GetUnifiedGroup(groupGuid, GraphAccessToken, includeSite: false);

                                                if (group.Title.EndsWith("Members"))
                                                {
                                                    // Microsoft 365 Group Members
                                                    var microsoft365GroupMembers = UnifiedGroupsUtility.GetUnifiedGroupMembers(microsoft365Group, GraphAccessToken);

                                                    foreach (var microsoft365GroupMember in microsoft365GroupMembers)
                                                    {
                                                        results.Add(new UserRightItem
                                                        {
                                                            ResourcePath = listItemUrl,
                                                            UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                            ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.Document : UserRightsResourceType.ListItem,
                                                            GroupName = microsoft365Group.DisplayName,
                                                            Upn = microsoft365GroupMember.UserPrincipalName,
                                                            Email = microsoft365GroupMember.Email,
                                                            Name = microsoft365GroupMember.DisplayName,
                                                            Permission = "Member",
                                                            IsGuest = microsoft365GroupMember.UserPrincipalName.Contains("#EXT#")
                                                        });
                                                    }
                                                }
                                                else
                                                {
                                                    // Microsoft 365 Group Owners
                                                    var microsoft365GroupOwners = UnifiedGroupsUtility.GetUnifiedGroupOwners(microsoft365Group, GraphAccessToken);

                                                    foreach (var microsoft365GroupOwner in microsoft365GroupOwners)
                                                    {
                                                        results.Add(new UserRightItem
                                                        {
                                                            ResourcePath = listItemUrl,
                                                            UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                            ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.Document : UserRightsResourceType.ListItem,
                                                            GroupName = microsoft365Group.DisplayName,
                                                            Upn = microsoft365GroupOwner.UserPrincipalName,
                                                            Email = microsoft365GroupOwner.Email,
                                                            Name = microsoft365GroupOwner.DisplayName,
                                                            Permission = "Owner",
                                                            IsGuest = microsoft365GroupOwner.UserPrincipalName.Contains("#EXT#")
                                                        });
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // User in a SharePoint Group
                                                foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                                                {
                                                    if (roleDefinition.Name == "Limited Access") continue;

                                                    // Get the SharePoint Group
                                                    var siteGroup = CurrentWeb.SiteGroups.FirstOrDefault(sg => sg.Id == group.Id);
                                                    if(siteGroup == null) continue;

                                                    foreach(var groupUser in siteGroup.Users)
                                                    {
                                                        // Skip the SharePoint System user in the reports
                                                        if (groupUser.LoginName == "SHAREPOINT\\system") continue;
                                                    
                                                        if (groupUser.AadObjectId != null && groupUser.LoginName.Contains("|federateddirectoryclaimprovider|"))
                                                        {
                                                            // Microsoft 365 Group
                                                            var microsoft365Group = UnifiedGroupsUtility.GetUnifiedGroup(groupUser.AadObjectId.NameId, GraphAccessToken, includeSite: false);

                                                            if (groupUser.Title.EndsWith("Members"))
                                                            {
                                                                // Microsoft 365 Group Members
                                                                var microsoft365GroupMembers = UnifiedGroupsUtility.GetUnifiedGroupMembers(microsoft365Group, GraphAccessToken);

                                                                foreach (var microsoft365GroupMember in microsoft365GroupMembers)
                                                                {
                                                                    results.Add(new UserRightItem
                                                                    {
                                                                        ResourcePath = listItemUrl,
                                                                        UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                                        ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.Document : UserRightsResourceType.ListItem,
                                                                        GroupName = microsoft365Group.DisplayName,
                                                                        Upn = microsoft365GroupMember.UserPrincipalName,
                                                                        Email = microsoft365GroupMember.Email,
                                                                        Name = microsoft365GroupMember.DisplayName,
                                                                        Permission = "Member",
                                                                        IsGuest = microsoft365GroupMember.UserPrincipalName.Contains("#EXT#")
                                                                    });
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // Microsoft 365 Group Owners
                                                                var microsoft365GroupOwners = UnifiedGroupsUtility.GetUnifiedGroupOwners(microsoft365Group, GraphAccessToken);

                                                                foreach (var microsoft365GroupOwner in microsoft365GroupOwners)
                                                                {
                                                                    results.Add(new UserRightItem
                                                                    {
                                                                        ResourcePath = listItemUrl,
                                                                        UserRightsGivenThrough = UserRightsGivenThroughType.Microsoft365Group,
                                                                        ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.Document : UserRightsResourceType.ListItem,
                                                                        GroupName = microsoft365Group.DisplayName,
                                                                        Upn = microsoft365GroupOwner.UserPrincipalName,
                                                                        Email = microsoft365GroupOwner.Email,
                                                                        Name = microsoft365GroupOwner.DisplayName,
                                                                        Permission = "Owner",
                                                                        IsGuest = microsoft365GroupOwner.UserPrincipalName.Contains("#EXT#")
                                                                    });
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // User in a SharePoint Group
                                                            results.Add(new UserRightItem
                                                            {
                                                                ResourcePath = listItemUrl,
                                                                UserRightsGivenThrough = group.Title.StartsWith("SharingLink") ? UserRightsGivenThroughType.SharingLink : UserRightsGivenThroughType.SharePointGroup,
                                                                ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.Document : UserRightsResourceType.ListItem,
                                                                GroupName = group.Title,
                                                                Upn = groupUser.UserPrincipalName,
                                                                Email = groupUser.Email,
                                                                Name = groupUser.Title,
                                                                Permission = groupUser.IsSiteAdmin ? "Site collection administrator" : roleDefinition.Name,
                                                                IsGuest = groupUser.IsShareByEmailGuestUser
                                                            });
                                                        }                                            
                                                    }
                                                }
                                            }
                                            break;
                                    case Microsoft.SharePoint.Client.Utilities.PrincipalType.SecurityGroup:
                                    case Microsoft.SharePoint.Client.Utilities.PrincipalType.DistributionList:
                                        var groupId = roleAssignment.Member.LoginName.Remove(0, roleAssignment.Member.LoginName.LastIndexOf('|') + 1);
                                        
                                        // Group
                                        var securityGroup = UnifiedGroupsUtility.GetUnifiedGroup(groupId, GraphAccessToken, includeSite: false);

                                        // Group Members
                                        var groupMembers = UnifiedGroupsUtility.GetUnifiedGroupMembers(securityGroup, GraphAccessToken);

                                        foreach (var groupMember in groupMembers)
                                        {
                                            foreach (var roleDefinition in roleAssignment.RoleDefinitionBindings)
                                            {
                                                if (roleDefinition.Name == "Limited Access") continue;

                                                results.Add(new UserRightItem
                                                {
                                                    ResourcePath = listItemUrl,
                                                    UserRightsGivenThrough = roleAssignment.Member.PrincipalType == Microsoft.SharePoint.Client.Utilities.PrincipalType.SecurityGroup ? UserRightsGivenThroughType.SecurityGroup : UserRightsGivenThroughType.DistributionList,
                                                    ResourceType = list.BaseType == BaseType.DocumentLibrary ? UserRightsResourceType.Document : UserRightsResourceType.ListItem,
                                                    GroupName = securityGroup.DisplayName,
                                                    Upn = groupMember.UserPrincipalName,
                                                    Email = groupMember.Email,
                                                    Name = groupMember.DisplayName,
                                                    Permission = roleDefinition.Name,
                                                    IsGuest = groupMember.UserPrincipalName.Contains("#EXT#")
                                                });
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                //itemProgress.RecordType = ProgressRecordType.Completed;
                //WriteProgress(itemProgress);
            }

            WriteObject(results, true);
        }
    }
}