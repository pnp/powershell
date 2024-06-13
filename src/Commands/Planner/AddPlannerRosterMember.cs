using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace SharePointPnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPPlannerRosterMember")]
    [RequiredMinimalApiPermissions("Tasks.ReadWrite")]
    public class AddPlannerRosterMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public PlannerRosterPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string User;

        protected override void ExecuteCmdlet()
        {
            var roster = Identity.GetPlannerRosterAsync(this, Connection, AccessToken).GetAwaiter().GetResult();

            if(roster == null)
            {
                throw new PSArgumentException("Provided Planner Roster could not be found", nameof(Identity));
            }

            PlannerUtility.AddRosterMemberAsync(this, Connection, AccessToken, roster.Id, User).GetAwaiter().GetResult();
        }
    }
}