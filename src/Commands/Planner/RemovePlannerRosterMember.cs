using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace SharePointPnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPPlannerRosterMember")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Tasks.ReadWrite")]
    public class RemovePlannerRosterMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public PlannerRosterPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string User;

        protected override void ExecuteCmdlet()
        {
            var roster = Identity.GetPlannerRoster(this, Connection, AccessToken);

            if(roster == null)
            {
                throw new PSArgumentException("Provided Planner Roster could not be found", nameof(Identity));
            }

            PlannerUtility.RemoveRosterMember(this, Connection, AccessToken, roster.Id, User);
        }
    }
}