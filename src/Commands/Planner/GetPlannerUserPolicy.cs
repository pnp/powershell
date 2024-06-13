using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPPlannerUserPolicy")]
    [RequiredMinimalApiPermissions("https://tasks.office.com/.default")]
    public class GetPlannerUserPolicy : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Identity;
        protected override void ExecuteCmdlet()
        {
            var result = PlannerUtility.GetPlannerUserPolicyAsync(this, Connection, AccessToken, Identity).GetAwaiter().GetResult();
            WriteObject(result);
        }
    }
}