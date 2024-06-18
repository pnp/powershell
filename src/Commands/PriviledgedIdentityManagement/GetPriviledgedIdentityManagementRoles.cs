using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPPriviledgedIdentityManagementRoles")]
    [OutputType(typeof(List<RoleEligibilitySchedule>))]
    [RequiredMinimalApiPermissions("RoleAssignmentSchedule.Read.Directory")]
    public class GetPriviledgedIdentityManagementRoles : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var roles = PriviledgedIdentityManagamentUtility.GetRoleEligibilitySchedules(Connection, AccessToken);
            WriteObject(roles, true);
        }
    }
}
