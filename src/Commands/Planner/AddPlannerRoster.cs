using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Add, "PnPPlannerRoster")]
    [RequiredApiDelegatedPermissions("graph/Tasks.ReadWrite")]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite.All")]
    public class AddPlannerRoster : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            PlannerUtility.CreateRoster(GraphRequestHelper);
        }
    }
}