using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Remove, "PnPPlannerPlan", SupportsShouldProcess = true)]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite.All")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
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
                var planId = Identity.GetId(this, Connection, AccessToken, groupId);
                if (!string.IsNullOrEmpty(planId))
                {
                    if (ShouldContinue($"Delete plan with id {planId}", Properties.Resources.Confirm))
                    {
                        PlannerUtility.DeletePlan(this, Connection, AccessToken, planId);
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