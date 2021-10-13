using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Set, "PnPPlannerConfiguration")]
    [RequiredMinimalApiPermissions("https://tasks.office.com/.default")]
    public class SetPlannerConfiguration : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public bool? IsPlannerAllowed;

        [Parameter(Mandatory = false)]
        public bool? AllowRosterCreation;
        
        [Parameter(Mandatory = false)]
        public bool? AllowTenantMoveWithDataLoss;

        [Parameter(Mandatory = false)]
        public bool? AllowPlannerMobilePushNotifications;

        [Parameter(Mandatory = false)]
        public bool? AllowCalendarSharing;

        protected override void ExecuteCmdlet()
        {
            var result = PlannerUtility.SetPlannerConfigAsync(HttpClient, AccessToken, IsPlannerAllowed, AllowCalendarSharing, AllowTenantMoveWithDataLoss, AllowRosterCreation, AllowPlannerMobilePushNotifications).GetAwaiter().GetResult();
            WriteObject(result);
        }
    }
}