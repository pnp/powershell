using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace SharePointPnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPPlannerBucket")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class AddPlannerBucket : PnPGraphCmdlet
    {
        private const string ParameterName_BYGROUP = "By Group";
        private const string ParameterName_BYPLANID = "By Plan Id";

        [Parameter(Mandatory = true, HelpMessage = "Specify the group id of group owning the plan.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerGroupPipeBind Group;

        [Parameter(Mandatory = true, HelpMessage = "Specify the id or name of the plan to retrieve the tasks for.", ParameterSetName = ParameterName_BYGROUP)]
        public PlannerPlanPipeBind Plan;

        [Parameter(Mandatory = true, ParameterSetName = ParameterName_BYPLANID)]
        public string PlanId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Name;
        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterName_BYGROUP)
            {
                var groupId = Group.GetGroupId(Connection, AccessToken);
                if (groupId != null)
                {
                    var planId = Plan.GetIdAsync(Connection, AccessToken, groupId).GetAwaiter().GetResult();

                    if (planId != null)
                    {
                        WriteObject(PlannerUtility.CreateBucketAsync(Connection, AccessToken, Name, planId).GetAwaiter().GetResult(), true);
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
                WriteObject(PlannerUtility.CreateBucketAsync(Connection, AccessToken, Name, PlanId).GetAwaiter().GetResult(), true);
            }
        }
    }
}