using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Get, "PlannerTask")]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.Group_Read_All)]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]
    public class GetPlannerTask : PnPGraphCmdlet
    {
        private const string ParameterName_BYGROUP = "By Group";
        private const string ParameterName_BYPLANID = "By Plan Id";
        private const string ParameterName_BYBUCKET = "By Bucket";
        [Parameter(Mandatory = true, HelpMessage = "Specify the group id of group owning the plan.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerGroupPipeBind Group;

        [Parameter(Mandatory = true, HelpMessage = "Specify the id or name of the plan to retrieve the tasks for.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerPlanPipeBind Plan;

        [Parameter(Mandatory = true, HelpMessage = "Specify the bucket or bucket id to retrieve the tasks for.", ParameterSetName = ParameterName_BYBUCKET)]
        public PlannerBucketPipeBind Bucket;

        [Parameter(Mandatory = true, ParameterSetName = ParameterName_BYPLANID)]
        public string PlanId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter ResolveUserDisplayNames;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterName_BYGROUP)
            {
                var groupId = Group.GetGroupId(HttpClient, AccessToken);
                if (groupId != null)
                {
                    var planId = Plan.GetIdAsync(HttpClient, AccessToken, groupId).GetAwaiter().GetResult();
                    if (planId != null)
                    {
                        WriteObject(PlannerUtility.GetTasksAsync(HttpClient, AccessToken, planId, ResolveUserDisplayNames).GetAwaiter().GetResult(), true);
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
            else if (ParameterSetName == ParameterName_BYPLANID)
            {
                WriteObject(PlannerUtility.GetTasksAsync(HttpClient, AccessToken, PlanId, ResolveUserDisplayNames).GetAwaiter().GetResult(), true);
            }
            else if (ParameterSetName == ParameterName_BYBUCKET)
            {
                WriteObject(PlannerUtility.GetBucketTasks(HttpClient, AccessToken, Bucket.GetId(), ResolveUserDisplayNames), true);
            }
        }
    }
}