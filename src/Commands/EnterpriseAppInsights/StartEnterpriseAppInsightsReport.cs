using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System.Net.Http.Json;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsLifecycle.Start, "PnPEnterpriseAppInsightsReport")]
    [OutputType(typeof(void))]
    public class StartEnterpriseAppInsightsReport : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        [ValidateSet("1", "7", "14", "28")]
        public short ReportPeriodInDays = 1;

        protected override void ExecuteCmdlet()
        {
            SharePointOnlineAdminRequestHelper.Post($"{AdminContext.Url}_api/v2.1/tenants/default/analyticsReports/createAppInsightsReport", JsonContent.Create(new { format = "FullDetails", appInsightsParameters = new { reportDuration = ReportPeriodInDays }}));
        }
    }
}