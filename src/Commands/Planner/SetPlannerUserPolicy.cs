using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Set, "PnPPlannerUserPolicy")]
    [RequiredMinimalApiPermissions("https://tasks.office.com/.default")]
    public class SetPlannerUserPolicy : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public bool? BlockDeleteTasksNotCreatedBySelf;

        protected override void ExecuteCmdlet()
        {
            var result = PlannerUtility.SetPlannerUserPolicyAsync(this, Connection, AccessToken, Identity, BlockDeleteTasksNotCreatedBySelf).GetAwaiter().GetResult();
            WriteObject(result);
        }
    }
}