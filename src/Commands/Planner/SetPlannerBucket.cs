using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Set, "PnPPlannerBucket")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class SetPlannerBucket : PnPGraphCmdlet
    {
        private const string ParameterName_BYGROUP = "By Group";
        private const string ParameterName_BYPLANID = "By Plan Id";

        [Parameter(Mandatory = true, HelpMessage = "Specify the bucket or bucket id to update.")]
        public PlannerBucketPipeBind Bucket;

        [Parameter(Mandatory = true, HelpMessage = "Specify the group id of group owning the plan.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerGroupPipeBind Group;

        [Parameter(Mandatory = true, HelpMessage = "Specify the name of the plan to retrieve the buckets for.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerPlanPipeBind Plan;

        [Parameter(Mandatory = true, ParameterSetName = ParameterName_BYPLANID)]
        public string PlanId;


        [Parameter(Mandatory = true, HelpMessage = "Specify the new name of the bucket")]
        public string Name;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterName_BYGROUP)
            {
                var groupId = Group.GetGroupId(this, Connection, AccessToken);
                if (groupId != null)
                {
                    var planId = Plan.GetId(this, Connection, AccessToken, groupId);
                    if (planId != null)
                    {

                        var bucket = Bucket.GetBucket(this, Connection, AccessToken, planId);
                        if (bucket != null)
                        {
                            WriteObject(PlannerUtility.UpdateBucket(this, Connection, AccessToken, Name, bucket.Id));
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
            else
            {
                var bucket = Bucket.GetBucket(this, Connection, AccessToken, PlanId);
                if (bucket != null)
                {
                    WriteObject(PlannerUtility.UpdateBucket(this, Connection, AccessToken, Name, bucket.Id));
                }
                else
                {
                    throw new PSArgumentException("Bucket not found", nameof(Bucket));
                }
            }
        }
    }
}

