using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Remove, "PnPEntraIDServicePrincipalAppRoleAssignment", DefaultParameterSetName = ParameterSet_USER, SupportsShouldProcess = true)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/AppRoleAssignment.ReadWrite.All")]
    [OutputType(typeof(void))]
    [Alias("Remove-PnPAzureADServicePrincipalAppRoleAssignment")]
    public class RemoveAzureADServicePrincipalAppRoleAssignment : PnPGraphCmdlet
    {
        private const string ParameterSet_BYINSTANCE = "By instance";
        private const string ParameterSet_USER = "User";
        private const string ParameterSet_GROUP = "Group";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYINSTANCE)]
        [ValidateNotNull]
        public AzureADServicePrincipalAppRoleAssignment Identity;

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

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        // Passing -Confirm either way leaves the asking to ShouldProcess, so the prompt below would be a second one for the same removal
        private bool ConfirmHandledByShouldProcess => MyInvocation.BoundParameters.ContainsKey("Confirm");

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_BYINSTANCE)
            {
                var target = Identity.Id ?? "app role assignment";
                var resourceName = Identity.ResourceDisplayName ?? Identity.ResourceId?.ToString() ?? "service principal";
                if (!ShouldProcess($"app role assignment {target} on {resourceName}", "Remove"))
                {
                    return;
                }

                if (!Force && !ConfirmHandledByShouldProcess && !ShouldContinue($"Remove app role assignment {target} on {resourceName}?", Properties.Resources.Confirm))
                {
                    return;
                }

                ServicePrincipalUtility.RemoveServicePrincipalAppRoleAssignment(GraphRequestHelper, Identity);
                return;
            }

            var resource = GetResourceServicePrincipal();
            var appRole = ResolveAppRole(resource);
            var principalId = ResolvePrincipalId();

            LogDebug($"Removing app role assignments from service principal {resource.DisplayName}");

            var assignments = ServicePrincipalUtility.GetServicePrincipalAppRoleAssignedToByServicePrincipalObjectId(GraphRequestHelper, resource.Id);
            if (assignments == null)
            {
                throw new PSInvalidOperationException($"Unable to retrieve app role assignments for service principal {resource.DisplayName}. Ensure the current connection has the required Microsoft Graph permissions and try again.");
            }

            var matchingAssignments = assignments.Where(assignment => string.Equals(assignment.PrincipalId, principalId.ToString(), StringComparison.OrdinalIgnoreCase));

            if (appRole != null)
            {
                matchingAssignments = matchingAssignments.Where(assignment => assignment.AppRoleId == appRole.Id.GetValueOrDefault());
            }

            var toRemove = matchingAssignments.ToList();
            if (toRemove.Count == 0)
            {
                LogDebug("No matching app role assignments were found to remove");
                return;
            }

            var description = appRole != null
                ? $"app role '{appRole.Value ?? appRole.DisplayName}' assignment for principal {principalId} on service principal {resource.DisplayName}"
                : $"all {toRemove.Count} app role assignment(s) for principal {principalId} on service principal {resource.DisplayName}";

            if (!ShouldProcess(description, "Remove"))
            {
                return;
            }

            if (!Force && !ConfirmHandledByShouldProcess && !ShouldContinue($"Remove {description}?", Properties.Resources.Confirm))
            {
                return;
            }

            foreach (var assignment in toRemove)
            {
                ServicePrincipalUtility.RemoveServicePrincipalAppRoleAssignment(GraphRequestHelper, assignment);
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
            if (!ParameterSpecified(nameof(AppRole)))
            {
                return null;
            }

            AzureADServicePrincipalAppRole appRole;
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

            return appRole;
        }

        private Guid ResolvePrincipalId()
        {
            if (ParameterSetName == ParameterSet_USER)
            {
                var user = User.GetUser(AccessToken, Connection.AzureEnvironment);

                if (user?.Id == null)
                {
                    throw new PSArgumentException("User not found", nameof(User));
                }

                return user.Id.Value;
            }

            var group = Group.GetGroup(GraphRequestHelper);
            if (group == null)
            {
                throw new PSArgumentException("Group not found", nameof(Group));
            }

            if (!Guid.TryParse(group.Id, out var groupId))
            {
                throw new PSArgumentException("Group id is invalid", nameof(Group));
            }

            return groupId;
        }

        private static bool IsUserTargetedAppRole(AzureADServicePrincipalAppRole appRole)
        {
            return appRole?.AllowedMemberTypes?.Any(memberType => memberType.Equals("User", StringComparison.OrdinalIgnoreCase)) == true;
        }
    }
}
