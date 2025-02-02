using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Get, "PnPPlannerRosterMember")]
    [RequiredApiApplicationPermissions("graph/Tasks.ReadWrite")]
    public class GetPlannerRosterMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public PlannerRosterPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var roster = Identity.GetPlannerRoster(GraphRequestHelper);

            if(roster == null)
            {
                throw new PSArgumentException("Provided Planner Roster could not be found", nameof(Identity));
            }

            PlannerUtility.GetRosterMembers(GraphRequestHelper, roster.Id);
        }
    }
}