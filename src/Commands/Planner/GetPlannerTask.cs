using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Get, "PnPPlannerTask")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetPlannerTask : PnPGraphCmdlet
    {
        private const string ParameterSetName_BYGROUP = "By Group";
        private const string ParameterSetName_BYPLANID = "By Plan Id";
        private const string ParameterSetName_BYBUCKET = "By Bucket";
        private const string ParameterSetName_BYTASKID = "By Task Id";

        [Parameter(Mandatory = true, HelpMessage = "Specify the group id of group owning the plan.", ParameterSetName = ParameterSetName_BYGROUP)]
        public PlannerGroupPipeBind Group;

        [Parameter(Mandatory = true, HelpMessage = "Specify the id or name of the plan to retrieve the tasks for.", ParameterSetName = ParameterSetName_BYGROUP)]
        public PlannerPlanPipeBind Plan;

        [Parameter(Mandatory = true, HelpMessage = "Specify the bucket or bucket id to retrieve the tasks for.", ParameterSetName = ParameterSetName_BYBUCKET)]
        public PlannerBucketPipeBind Bucket;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetName_BYPLANID)]
        public string PlanId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter ResolveUserDisplayNames;

        [Parameter(Mandatory = false, HelpMessage = "If specified this specific task will be retrieved", ParameterSetName = ParameterSetName_BYTASKID)]
        public string TaskId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_BYTASKID)]
        public SwitchParameter IncludeDetails;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSetName_BYGROUP)
            {
                var groupId = Group.GetGroupId(this, Connection, AccessToken);
                if (groupId != null)
                {
                    var planId = Plan.GetIdAsync(this, Connection, AccessToken, groupId).GetAwaiter().GetResult();
                    if (planId != null)
                    {
                        WriteObject(PlannerUtility.GetTasksAsync(this, Connection, AccessToken, planId, ResolveUserDisplayNames).GetAwaiter().GetResult(), true);
                    }
                    else
                    {
                        throw new PSArgumentException("Plan not found");
                    }
                }
                else
                {
                    throw new PSArgumentException("Group not found");
                }
            }
            else if (ParameterSetName == ParameterSetName_BYPLANID)
            {
                WriteObject(PlannerUtility.GetTasksAsync(this, Connection, AccessToken, PlanId, ResolveUserDisplayNames).GetAwaiter().GetResult(), true);
            }
            else if (ParameterSetName == ParameterSetName_BYBUCKET)
            {
                WriteObject(PlannerUtility.GetBucketTasksAsync(this, Connection, AccessToken, Bucket.GetId(), ResolveUserDisplayNames).GetAwaiter().GetResult(), true);
            }
            else if (ParameterSetName == ParameterSetName_BYTASKID)
            {
                WriteObject(PlannerUtility.GetTaskAsync(this, Connection, AccessToken, TaskId, ResolveUserDisplayNames, IncludeDetails).GetAwaiter().GetResult());
            }
        }
    }
}