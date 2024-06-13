using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;

namespace SharePointPnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPPlannerRoster")]
    [RequiredMinimalApiPermissions("Tasks.ReadWrite")]
    public class AddPlannerRoster : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            PlannerUtility.CreateRosterAsync(this, Connection, AccessToken).GetAwaiter().GetResult();
        }
    }
}