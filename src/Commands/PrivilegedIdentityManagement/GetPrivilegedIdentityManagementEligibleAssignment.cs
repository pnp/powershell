using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.PrivilegedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPPrivilegedIdentityManagementEligibleAssignment")]
    [OutputType(typeof(List<RoleEligibilitySchedule>))]
    [OutputType(typeof(RoleEligibilitySchedule))]
    [RequiredApiDelegatedOrApplicationPermissions("graph/RoleAssignmentSchedule.Read.Directory")]
    public class GetPrivilegedIdentityManagementEligibleAssignment : PnPGraphCmdlet
    {
        /// <summary>
        /// Specific eligible role to retrieve
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public PrivilegedIdentityManagementRoleEligibilitySchedulePipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                LogDebug("Retrieving specific eligible role assignment");
                var role = Identity.GetInstance(GraphRequestHelper);
                WriteObject(role, false);
            }
            else
            {
                LogDebug("Retrieving all eligible role assignments");
                var roles = PrivilegedIdentityManagementUtility.GetRoleEligibilitySchedules(GraphRequestHelper);
                WriteObject(roles, true);
            }
        }
    }
}
