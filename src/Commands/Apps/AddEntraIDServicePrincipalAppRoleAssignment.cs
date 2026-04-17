using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using GraphGroup = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Add, "PnPEntraIDServicePrincipalAppRoleAssignment", DefaultParameterSetName = ParameterSet_USER)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/AppRoleAssignment.ReadWrite.All", "graph/Application.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/AppRoleAssignment.ReadWrite.All", "graph/Application.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/AppRoleAssignment.ReadWrite.All", "graph/Directory.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/AppRoleAssignment.ReadWrite.All", "graph/Directory.ReadWrite.All")]
    [OutputType(typeof(AzureADServicePrincipalAppRoleAssignment))]
    [Alias("Add-PnPAzureADServicePrincipalAppRoleAssignment")]
    public class AddAzureADServicePrincipalAppRoleAssignment : PnPGraphCmdlet
    {
        private const string ParameterSet_USER = "User";
        private const string ParameterSet_GROUP = "Group";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USER)]
        [ValidateNotNull]
        public EntraIDUserPipeBind User;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_GROUP)]
        [ValidateNotNull]
        public EntraIDGroupPipeBind Group;

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_USER)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_GROUP)]
        public ServicePrincipalAvailableAppRoleBind AppRole;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_GROUP)]
        public ServicePrincipalPipeBind Resource;

        protected override void ExecuteCmdlet()
        {
            var resource = GetResourceServicePrincipal();
            var appRole = ResolveAppRole(resource);

            LogDebug($"Adding app role assignment {appRole.Value ?? appRole.DisplayName} on service principal {resource.DisplayName}");

            if (ParameterSetName == ParameterSet_USER)
            {
                var user = User.GetUser(AccessToken, Connection.AzureEnvironment);

                if (user?.Id == null)
                {
                    throw new PSArgumentException("User not found", nameof(User));
                }

                var response = ServicePrincipalUtility.AddServicePrincipalAppRoleAssignment(GraphRequestHelper, user.Id.Value, resource, appRole);
                if (response == null)
                {
                    throw new PSInvalidOperationException("Microsoft Graph did not return an app role assignment for the request. Verify the user, resource, and app role and try again.");
                }
                EnrichResponse(response, appRole, resource, user.DisplayName ?? user.UserPrincipalName, "User");
                WriteObject(response, false);
            }
            else
            {
                var group = Group.GetGroup(GraphRequestHelper);

                if (group == null)
                {
                    throw new PSArgumentException("Group not found", nameof(Group));
                }

                if (!Guid.TryParse(group.Id, out var groupId))
                {
                    throw new PSArgumentException("Group id is invalid", nameof(Group));
                }

                EnsureGroupSupportsAppRoleAssignments(group);

                var response = ServicePrincipalUtility.AddServicePrincipalAppRoleAssignment(GraphRequestHelper, groupId, resource, appRole);
                if (response == null)
                {
                    throw new PSInvalidOperationException("Microsoft Graph did not return an app role assignment for the request. Verify the group, resource, and app role and try again.");
                }
                EnrichResponse(response, appRole, resource, group.DisplayName ?? group.Id, "Group");
                WriteObject(response, false);
            }
        }

        private AzureADServicePrincipal GetResourceServicePrincipal()
        {
            AzureADServicePrincipal resource = null;

            if (ParameterSpecified(nameof(Resource)) && Resource != null)
            {
                resource = Resource.GetServicePrincipal(GraphRequestHelper);
            }

            if (AppRole?.AppRole?.ServicePrincipal != null)
            {
                if (resource != null && !string.Equals(resource.Id, AppRole.AppRole.ServicePrincipal.Id, StringComparison.OrdinalIgnoreCase))
                {
                    throw new PSArgumentException("The provided Resource does not match the service principal associated with the AppRole", nameof(Resource));
                }

                resource ??= AppRole.AppRole.ServicePrincipal;
            }

            if (resource == null)
            {
                throw new PSArgumentException("Resource service principal not found. Provide Resource or pipe in an AppRole instance associated with a service principal", nameof(Resource));
            }

            return resource;
        }

        private AzureADServicePrincipalAppRole ResolveAppRole(AzureADServicePrincipal resource)
        {
            var userTargetedAppRoles = resource.AppRoles?.Where(IsUserTargetedAppRole).ToList() ?? new List<AzureADServicePrincipalAppRole>();
            var enabledUserAssignableAppRoles = userTargetedAppRoles.Where(IsEnabledUserAssignableAppRole).ToList();
            AzureADServicePrincipalAppRole appRole = null;

            if (ParameterSpecified(nameof(AppRole)))
            {
                if (AppRole.AppRole != null)
                {
                    appRole = AppRole.AppRole;
                    appRole.ServicePrincipal ??= resource;
                }
                else
                {
                    appRole = AppRole.GetAvailableAppRole(Connection, AccessToken, resource);
                }

                if (appRole == null)
                {
                    throw new PSArgumentException("AppRole not found", nameof(AppRole));
                }

                if (!IsUserTargetedAppRole(appRole))
                {
                    throw new PSArgumentException("The provided AppRole cannot be assigned to a user or group", nameof(AppRole));
                }

                if (!IsEnabledUserAssignableAppRole(appRole))
                {
                    throw new PSArgumentException("The provided AppRole is disabled and cannot be assigned to a user or group", nameof(AppRole));
                }

                return appRole;
            }

            if (!userTargetedAppRoles.Any())
            {
                return new AzureADServicePrincipalAppRole
                {
                    Id = Guid.Empty,
                    DisplayName = "Default Access",
                    Value = "Default Access",
                    AllowedMemberTypes = new[] { "User" },
                    ServicePrincipal = resource
                };
            }

            if (!enabledUserAssignableAppRoles.Any())
            {
                throw new PSArgumentException("The resource service principal exposes user-targeted app roles, but none of them are currently enabled for assignment", nameof(AppRole));
            }

            throw new PSArgumentException("AppRole is required because the resource service principal exposes one or more user-assignable app roles", nameof(AppRole));
        }

        private static void EnsureGroupSupportsAppRoleAssignments(GraphGroup group)
        {
            var isSecurityEnabledGroup = group.SecurityEnabled == true;
            var isMicrosoft365Group = group.GroupTypes?.Any(groupType => groupType.Equals("Unified", StringComparison.OrdinalIgnoreCase)) == true;

            if (!isSecurityEnabledGroup && !isMicrosoft365Group)
            {
                throw new PSArgumentException("Only security-enabled groups and Microsoft 365 groups can be assigned to enterprise applications", nameof(Group));
            }
        }

        private static bool IsUserTargetedAppRole(AzureADServicePrincipalAppRole appRole)
        {
            return appRole?.AllowedMemberTypes?.Any(memberType => memberType.Equals("User", StringComparison.OrdinalIgnoreCase)) == true;
        }

        private static bool IsEnabledUserAssignableAppRole(AzureADServicePrincipalAppRole appRole)
        {
            return appRole?.IsEnabled != false && IsUserTargetedAppRole(appRole);
        }

        private static void EnrichResponse(AzureADServicePrincipalAppRoleAssignment response, AzureADServicePrincipalAppRole appRole, AzureADServicePrincipal resource, string principalDisplayName, string principalType)
        {
            if (response == null)
            {
                return;
            }

            response.AppRoleName ??= appRole.Value ?? appRole.DisplayName;
            response.PrincipalDisplayName ??= principalDisplayName;
            response.PrincipalType ??= principalType;
            response.ResourceDisplayName ??= resource.DisplayName;

            if (!response.ResourceId.HasValue && Guid.TryParse(resource.Id, out var resourceId))
            {
                response.ResourceId = resourceId;
            }
        }
    }
}
