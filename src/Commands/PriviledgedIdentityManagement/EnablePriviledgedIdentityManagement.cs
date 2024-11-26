using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPPriviledgedIdentityManagement")]
    [OutputType(typeof(bool))]
    [RequiredApiDelegatedOrApplicationPermissions("graph/RoleAssignmentSchedule.ReadWrite.Directory")]
    public class EnablePriviledgedIdentityManagement : PnPGraphCmdlet
    {
        private const string ParameterName_BYELIGIBLEROLEASSIGNMENT = "By Eligible Role Assignment";
        private const string ParameterName_BYROLENAMEANDPRINCIPAL = "By Role Name And Principal";
        private const string ParameterName_BYROLENAMEANDUSER = "By Role Name And User";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterName_BYELIGIBLEROLEASSIGNMENT)]
        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind EligibleAssignment;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterName_BYROLENAMEANDPRINCIPAL)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterName_BYROLENAMEANDUSER)]
        public PriviledgedIdentityManagementRolePipeBind Role;

        [Parameter(Mandatory = true, ParameterSetName = ParameterName_BYROLENAMEANDUSER)]
        public AzureADUserPipeBind User;

        [Parameter(Mandatory = false, ParameterSetName = ParameterName_BYROLENAMEANDPRINCIPAL)]
        public Guid? PrincipalId;

        [Parameter(Mandatory = false)]
        public string Justification;

        [Parameter(Mandatory = false)]
        public DateTime? StartAt;

        [Parameter(Mandatory = false)]
        public short? ExpireInHours;

        protected override void ExecuteCmdlet()
        {
            RoleEligibilitySchedule roleEligibilitySchedule = null;

            switch (ParameterSetName)
            {
                case ParameterName_BYELIGIBLEROLEASSIGNMENT:
                    roleEligibilitySchedule = EligibleAssignment.GetInstance(this, Connection, AccessToken);
                    break;

                case ParameterName_BYROLENAMEANDUSER:
                    // Check if we have a principal to elevate
                    var user = User.GetUser(AccessToken);
                    if (user == null || !user.Id.HasValue)
                    {
                        throw new PSArgumentException("Provided user cannot be found", nameof(User));
                    }

                    // Check for the role to which elevation needs to take place
                    var role = Role.GetInstance(this, Connection, AccessToken);

                    if (role == null)
                    {
                        throw new PSArgumentException("Provided role cannot be found", nameof(Role));
                    }

                    // Look for an eligible role assignment for the user and role
                    roleEligibilitySchedule = PriviledgedIdentityManagamentUtility.GetRoleEligibilityScheduleByPrincipalIdAndRoleName(this, user.Id.Value, role, Connection, AccessToken);
                    break;

                case ParameterName_BYROLENAMEANDPRINCIPAL:
                    // Check if we have a principal to elevate
                    if (!PrincipalId.HasValue)
                    {
                        // A principal was not provided, check the type of access token
                        if (TokenHandler.RetrieveTokenType(AccessToken) == IdType.Delegate)
                        {
                            // Access token is a delegate, we're going to use the currently connected user to elevate
                            WriteVerbose("Currently connected user will be used to elevate the role assignment");
                            PrincipalId = TokenHandler.RetrieveTokenUser(AccessToken);
                        }
                        else
                        {
                            // Access token is an app only token, we don't know who to elevate, so cancel the operation
                            throw new PSArgumentException($"{nameof(PrincipalId)} is required when using Application permissions", nameof(PrincipalId));
                        }
                    }

                    // Check for the role to which elevation needs to take place
                    var role2 = Role.GetInstance(this, Connection, AccessToken);

                    if (role2 == null)
                    {
                        throw new PSArgumentException("Provided role cannot be found", nameof(Role));
                    }

                    // Look for an eligible role assignment for the principal and role
                    roleEligibilitySchedule = PriviledgedIdentityManagamentUtility.GetRoleEligibilityScheduleByPrincipalIdAndRoleName(this, PrincipalId.Value, role2, Connection, AccessToken);
                    break;
            }

            if (roleEligibilitySchedule == null)
            {
                throw new PSInvalidOperationException("No eligible role assignment found");
            }

            WriteVerbose($"Creating role assignment schedule request");
            var response = PriviledgedIdentityManagamentUtility.CreateRoleAssignmentScheduleRequest(this, roleEligibilitySchedule, Connection, AccessToken, Justification, StartAt, ExpireInHours);
            WriteObject(response.IsSuccessStatusCode);
        }
    }
}
