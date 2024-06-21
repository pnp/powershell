using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPPriviledgedIdentityManagement")]
    [OutputType(typeof(bool))]
    [RequiredMinimalApiPermissions("RoleAssignmentSchedule.ReadWrite.Directory")]
    public class EnablePriviledgedIdentityManagement : PnPGraphCmdlet
    {
        private const string ParameterName_BYELIGIBLEROLEASSIGNMENT = "By Eligible Role Assignment";
        private const string ParameterName_BYROLENAME = "By Role Name";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterName_BYELIGIBLEROLEASSIGNMENT)]
        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind EligibleAssignment;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterName_BYROLENAME)]
        public PriviledgedIdentityManagementRolePipeBind Role;

        [Parameter(Mandatory = false)]
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
                    roleEligibilitySchedule = EligibleAssignment.GetInstance(Connection, AccessToken);
                    break;

                case ParameterName_BYROLENAME:
                    var role = Role.GetInstance(Connection, AccessToken);

                    if(role == null)
                    {
                        WriteWarning("Provided role cannot be found");
                        WriteObject(false);
                        return;
                    }

                    roleEligibilitySchedule = PriviledgedIdentityManagamentUtility.GetRoleEligibilityScheduleByPrincipalIdAndRoleName(PrincipalId.Value, role, Connection, AccessToken);
                    break;
            }

            if (roleEligibilitySchedule == null)
            {
                WriteWarning("No eligible role assignment found");
                WriteObject(false);
                return;
            }

            WriteVerbose($"Creating role assignment schedule request");
            var response = PriviledgedIdentityManagamentUtility.CreateRoleAssignmentScheduleRequest(roleEligibilitySchedule, Connection, AccessToken, Justification, StartAt, ExpireInHours);
            WriteObject(response.IsSuccessStatusCode);
        }
    }
}
