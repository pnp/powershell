using System.Management.Automation;
using Microsoft.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Get, "PnPPlannerPlan")]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.Group_Read_All)]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    [PnPManagementShellScopes("Group.ReadWrite.All")]
    public class GetPlannerPlan : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public PlannerGroupPipeBind Group;

        [Parameter(Mandatory = false)]
        public PlannerPlanPipeBind Identity;


        [Parameter(Mandatory = false)]
        public SwitchParameter ResolveIdentities;
        protected override void ExecuteCmdlet()
        {
            var groupId = Group.GetGroupId(HttpClient, AccessToken);
            if (groupId != null)
            {
                if (ParameterSpecified(nameof(Identity)))
                {
                    WriteObject(Identity.GetPlanAsync(HttpClient, AccessToken, groupId, ResolveIdentities).GetAwaiter().GetResult());
                }
                else
                {
                    WriteObject(PlannerUtility.GetPlansAsync(HttpClient, AccessToken, groupId, ResolveIdentities).GetAwaiter().GetResult(), true);
                }
            }
            else
            {
                throw new PSArgumentException("Group not found");
            }
        }
    }
}