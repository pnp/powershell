using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace SharePointPnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPPlannerTask")]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]
    public class AddPlannerTask : PnPGraphCmdlet
    {
        private const string ParameterName_BYGROUP = "By Group";
        private const string ParameterName_BYPLANID = "By Plan Id";

        [Parameter(Mandatory = true, HelpMessage = "Specify the group id of group owning the plan.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerGroupPipeBind Group;

        [Parameter(Mandatory = true, HelpMessage = "Specify the id or name of the plan to retrieve the tasks for.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerPlanPipeBind Plan;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public PlannerBucketPipeBind Bucket;

        [Parameter(Mandatory = true, ParameterSetName = ParameterName_BYPLANID)]
        public string PlanId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Title;
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
                        var bucket = Bucket.GetBucket(HttpClient, AccessToken, planId);
                        if (bucket != null)
                        {
                            PlannerUtility.AddTaskAsync(HttpClient, AccessToken, planId, bucket.Id, Title).GetAwaiter().GetResult();
                        }
                        else
                        {
                            throw new PSArgumentException("Bucket not found", nameof(Bucket));
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
            else if (ParameterSetName == ParameterName_BYPLANID)
            {
                var bucket = Bucket.GetBucket(HttpClient, AccessToken, PlanId);
                if (bucket != null)
                {
                    PlannerUtility.AddTaskAsync(HttpClient, AccessToken, PlanId, bucket.Id, Title).GetAwaiter().GetResult();
                }
                else
                {
                    throw new PSArgumentException("Bucket not found", nameof(Bucket));
                }
            }
        }
    }
}