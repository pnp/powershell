using System.Management.Automation;
using Microsoft.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Remove, "PnPPlannerPlan", SupportsShouldProcess = true)]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class RemovePlannerPlan : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public PlannerGroupPipeBind Group;

        [Parameter(Mandatory = true)]
        public PlannerPlanPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var groupId = Group.GetGroupId(this, Connection, AccessToken);
            if (groupId != null)
            {
                var planId = Identity.GetIdAsync(this, Connection, AccessToken, groupId).GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(planId))
                {
                    if (ShouldProcess($"Delete plan with id {planId}"))
                    {
                        PlannerUtility.DeletePlanAsync(this, Connection, AccessToken, planId).GetAwaiter().GetResult();
                    }
                }
                else
                {
                    throw new PSArgumentException("Plan not found", nameof(Identity));
                }
            }
            else
            {
                throw new PSArgumentException("Group not found", nameof(Group));
            }
        }
    }
}