using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Remove, "PnPPlannerRoster", SupportsShouldProcess = true)]
    [RequiredMinimalApiPermissions("Tasks.ReadWrite")]
    public class RemovePlannerRoster : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public PlannerRosterPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var roster = Identity.GetPlannerRosterAsync(this, Connection, AccessToken).GetAwaiter().GetResult();

            if(roster == null)
            {
                throw new PSArgumentException("Provided Planner Roster could not be found", nameof(Identity));
            }

            PlannerUtility.DeleteRosterAsync(this, Connection, AccessToken, roster.Id).GetAwaiter().GetResult();
        }
    }
}