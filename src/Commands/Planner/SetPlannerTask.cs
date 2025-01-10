using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Planner;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Set, "PnPPlannerTask")]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite.All")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
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
        public int Priority;

        [Parameter(Mandatory = false)]
        public DateTime DueDateTime;

        [Parameter(Mandatory = false)]
        public DateTime StartDateTime;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string[] AssignedTo;

        protected override void ExecuteCmdlet()
        {
            var existingTask = PlannerUtility.GetTask(RequestHelper, TaskId, false, false);
            if (existingTask != null)
            {
                var plannerTask = new PlannerTask();
                if (ParameterSpecified(nameof(Title)))
                {
                    plannerTask.Title = Title;
                }
                if (ParameterSpecified(nameof(Bucket)))
                {
                    var bucket = Bucket.GetBucket(RequestHelper, existingTask.PlanId);
                    if (bucket != null)
                    {
                        plannerTask.BucketId = bucket.Id;
                    }
                }
                if (ParameterSpecified(nameof(PercentComplete)))
                {
                    plannerTask.PercentComplete = PercentComplete;
                }

                if (ParameterSpecified(nameof(Priority)))
                {
                    if (Priority < 0 || Priority > 10)
                    {
                        throw new PSArgumentException($"Parameter '{nameof(Priority)}' must be a number between 0 and 10.");
                    }

                    plannerTask.Priority = Priority;
                }

                if (ParameterSpecified(nameof(DueDateTime)))
                {
                    plannerTask.DueDateTime = DueDateTime.ToUniversalTime();
                }

                if (ParameterSpecified(nameof(StartDateTime)))
                {
                    plannerTask.StartDateTime = StartDateTime.ToUniversalTime();
                }

                if (ParameterSpecified(nameof(AssignedTo)))
                {
                    var errors = new List<Exception>();

                    plannerTask.Assignments = new System.Collections.Generic.Dictionary<string, TaskAssignment>();
                    var chunks = BatchUtility.Chunk(AssignedTo, 20);
                    foreach (var chunk in chunks)
                    {
                        var userIds = BatchUtility.GetPropertyBatched(RequestHelper, chunk.ToArray(), "/users/{0}", "id");
                        foreach (var userId in userIds.Results)
                        {
                            plannerTask.Assignments.Add(userId.Value, new TaskAssignment());
                        }
                        if(userIds.Errors.Any())
                        {
                            errors.AddRange(userIds.Errors);
                        }
                    }
                    if(errors.Any())
                    {
                        throw new AggregateException($"{errors.Count} error(s) occurred in a Graph batch request", errors);
                    }
                    foreach (var existingAssignment in existingTask.Assignments)
                    {
                        if (plannerTask.Assignments.FirstOrDefault(t => t.Key == existingAssignment.Key).Key == null)
                        {
                            plannerTask.Assignments.Add(existingAssignment.Key, null);
                        }
                    }
                }


                PlannerUtility.UpdateTask(RequestHelper, existingTask, plannerTask);

                if (ParameterSpecified(nameof(Description)))
                {
                    var existingTaskDetails = PlannerUtility.GetTaskDetails(RequestHelper, TaskId, false);
                    PlannerUtility.UpdateTaskDetails(RequestHelper, existingTaskDetails, Description);
                }
            }
            else
            {
                throw new PSArgumentException("Task not found", nameof(TaskId));
            }
        }
    }
}