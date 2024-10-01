using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace SharePointPnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPPlannerBucket", SupportsShouldProcess = true)]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/Tasks.ReadWrite.All")]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/Group.ReadWrite.All")]
    public class RemovePlannerBucket : PnPGraphCmdlet
    {
        private const string ParameterName_BYNAME = "By Name";
        private const string ParameterName_BYBUCKETID = "By Bucket Id";

        [Parameter(Mandatory = true, HelpMessage = "Specify the group id of group owning the plan.", ParameterSetName = ParameterName_BYNAME)]
        public PlannerGroupPipeBind Group;

        [Parameter(Mandatory = true, HelpMessage = "Specify the id or name of the plan to retrieve the tasks for.", ParameterSetName = ParameterName_BYNAME)]
        public PlannerPlanPipeBind Plan;

        [Parameter(Mandatory = true, ParameterSetName = ParameterName_BYBUCKETID)]
        public string BucketId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public PlannerBucketPipeBind Identity;
        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterName_BYNAME)
            {
                var groupId = Group.GetGroupId(this, Connection, AccessToken);
                if (groupId != null)
                {
                    var planId = Plan.GetId(this, Connection, AccessToken, groupId);

                    if (planId != null)
                    {
                        var bucket = Identity.GetBucket(this, Connection, AccessToken, planId);
                        if (bucket != null)
                        {
                            if (ShouldProcess($"Remove bucket '{bucket.Name}'"))
                            {
                                PlannerUtility.RemoveBucket(this, Connection, AccessToken, bucket.Id);
                            }
                        }
                        else
                        {
                            throw new PSArgumentException("Bucket not found", nameof(Identity));
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
            else if (ParameterSetName == ParameterName_BYBUCKETID)
            {
                var bucket = Identity.GetBucket(this, Connection, AccessToken, BucketId);
                if (bucket != null)
                {
                    if (ShouldProcess($"Remove bucket '{bucket.Name}'"))
                    {
                        PlannerUtility.RemoveBucket(this, Connection, AccessToken, BucketId);
                    }
                }
                else
                {
                    throw new PSArgumentException("Bucket not found", nameof(Identity));
                }
            }
        }
    }
}