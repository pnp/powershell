using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Set, "PlannerTask")]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]
    public class SetPlannerTask : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string TaskId;

        [Parameter(Mandatory = false)]
        public string Title;

        [Parameter(Mandatory = false)]
        public PlannerBucketPipeBind Bucket;

        [Parameter(Mandatory = false)]
        public int PercentComplete;

        [Parameter(Mandatory = false)]
        public DateTime DueDateTime;

        [Parameter(Mandatory = false)]
        public DateTime StartDateTime;

        protected override void ExecuteCmdlet()
        {
            var existingTask = PlannerUtility.GetTaskAsync(HttpClient, AccessToken, TaskId, false, false).GetAwaiter().GetResult();
            if (existingTask != null)
            {
                var plannerTask = new PlannerTask();
                if (ParameterSpecified(nameof(Title)))
                {
                    plannerTask.Title = Title;
                }
                if (ParameterSpecified(nameof(Bucket)))
                {
                    var bucket = Bucket.GetBucket(HttpClient, AccessToken, existingTask.PlanId);
                    if (bucket != null)
                    {
                        plannerTask.BucketId = bucket.Id;
                    }
                }
                if (ParameterSpecified(nameof(PercentComplete)))
                {
                    plannerTask.PercentComplete = PercentComplete;
                }
                if (ParameterSpecified(nameof(DueDateTime)))
                {
                    plannerTask.DueDateTime = DueDateTime.ToUniversalTime();
                }
                if (ParameterSpecified(nameof(StartDateTime)))
                {
                    plannerTask.StartDateTime = StartDateTime.ToUniversalTime();
                }
                PlannerUtility.UpdateTaskAsync(HttpClient, AccessToken, existingTask, plannerTask).GetAwaiter().GetResult();
            }
            else
            {
                throw new PSArgumentException("Task not found", nameof(TaskId));
            }
        }
    }
}