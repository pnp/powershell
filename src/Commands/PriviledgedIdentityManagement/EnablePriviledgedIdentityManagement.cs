using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPPriviledgedIdentityManagement")]
    [OutputType(typeof(bool))]
    [RequiredMinimalApiPermissions("RoleAssignmentSchedule.ReadWrite.Directory")]
    public class EnablePriviledgedIdentityManagement : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind Role;

        [Parameter(Mandatory = false)]
        public string Justification;

        [Parameter(Mandatory = false)]
        public DateTime? StartAt;

        [Parameter(Mandatory = false)]
        public short? ExpireInHours;

        protected override void ExecuteCmdlet()
        {
            var roleEligibilitySchedule = Role.GetInstance(Connection, AccessToken);
            var response = PriviledgedIdentityManagamentUtility.CreateRoleAssignmentScheduleRequest(roleEligibilitySchedule, Connection, AccessToken, Justification, StartAt, ExpireInHours);
            WriteObject(response.IsSuccessStatusCode);
        }
    }
}
