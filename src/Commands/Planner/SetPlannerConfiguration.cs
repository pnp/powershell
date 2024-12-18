using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Planner
{
    [Cmdlet(VerbsCommon.Set, "PnPPlannerConfiguration")]
    [RequiredApiDelegatedOrApplicationPermissions("https://tasks.office.com/.default")]
    public class SetPlannerConfiguration : PnPTasksCmdlet
    {
        [Parameter(Mandatory = false)]
        public bool? IsPlannerAllowed;

        [Parameter(Mandatory = false)]
        public bool? AllowRosterCreation;

        [Parameter(Mandatory = false)]
        public bool? AllowTenantMoveWithDataLoss;

        [Parameter(Mandatory = false)]
        public bool? AllowTenantMoveWithDataMigration;

        [Parameter(Mandatory = false)]
        public bool? AllowPlannerMobilePushNotifications;

        [Parameter(Mandatory = false)]
        public bool? AllowCalendarSharing;

        protected override void ExecuteCmdlet()
        {
            var result = PlannerUtility.SetPlannerConfig(RequestHelper, IsPlannerAllowed, AllowCalendarSharing, AllowTenantMoveWithDataLoss, AllowTenantMoveWithDataMigration, AllowRosterCreation, AllowPlannerMobilePushNotifications);
            WriteObject(result);
        }
    }
}