using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Get, "PnPPlannerBucket")]
    [RequiredApiApplicationPermissions("graph/Tasks.Read")]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.Read.All")]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite.All")]
    [RequiredApiApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    public class GetPlannerBucket : PnPGraphCmdlet
    {
        private const string ParameterName_BYGROUP = "By Group";
        private const string ParameterName_BYPLANID = "By Plan Id";

        [Parameter(Mandatory = true, HelpMessage = "Specify the group id of group owning the plan.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerGroupPipeBind Group;

        [Parameter(Mandatory = true, HelpMessage = "Specify the name of the plan to retrieve the tasks for.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerPlanPipeBind Plan;

        [Parameter(Mandatory = true, ParameterSetName = ParameterName_BYPLANID)]
        public string PlanId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public PlannerBucketPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterName_BYGROUP)
            {
                var groupId = Group.GetGroupId(RequestHelper);
                if (groupId != null)
                {
                    var planId = Plan.GetId(RequestHelper, groupId);
                    if (planId != null)
                    {
                        if (!ParameterSpecified(nameof(Identity)))
                        {
                            WriteObject(PlannerUtility.GetBuckets(RequestHelper, planId), true);
                        }
                        else
                        {
                            WriteObject(Identity.GetBucket(RequestHelper, planId));
                        }
                    }
                    else
                    {
                        throw new PSArgumentException("Plan not found", nameof(Plan));
                    }
                }
                else
                {
                    throw new PSArgumentException("Group not found", nameof(Group));
                }
            }
            else
            {
                WriteObject(PlannerUtility.GetBuckets(RequestHelper, PlanId), true);
            }
        }
    }
}