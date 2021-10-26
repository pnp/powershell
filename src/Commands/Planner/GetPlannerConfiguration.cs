using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPPlannerConfiguration")]
    [RequiredMinimalApiPermissions("https://tasks.office.com/.default")]
    public class GetPlannerConfiguration : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var result = PlannerUtility.GetPlannerConfigAsync(HttpClient, AccessToken).GetAwaiter().GetResult();
            WriteObject(result);
        }
    }
}