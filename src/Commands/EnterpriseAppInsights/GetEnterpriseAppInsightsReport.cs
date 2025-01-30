using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPEnterpriseAppInsightsReport", DefaultParameterSetName = ALLREPORTS)]
    [OutputType(typeof(List<Model.EnterpriseAppInsights.ReportMetadata>), ParameterSetName = new string[] { ALLREPORTS })]
    [OutputType(typeof(Model.EnterpriseAppInsights.ReportMetadata), ParameterSetName = new string[] { SPECIFICREPORT })]
    [OutputType(typeof(string), ParameterSetName = new string[] { DOWNLOADREPORT })]
    public class GetEnterpriseAppInsightsReport : PnPSharePointOnlineAdminCmdlet
    {
        private const string ALLREPORTS = "Details on all available reports";
        private const string SPECIFICREPORT = "Details on a specific report";
        private const string DOWNLOADREPORT = "Download a report";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = SPECIFICREPORT)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = DOWNLOADREPORT)]
        public string ReportId;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = DOWNLOADREPORT)]
        [ValidateSet("Download")]
        public string Action { get; set; }

        protected override void ExecuteCmdlet()
        {
            switch(ParameterSetName)
            {
                case DOWNLOADREPORT:
                    var downloadResponse = SharePointOnlineAdminRequestHelper.Get($"{AdminContext.Url}_api/v2.1/tenants/default/analyticsReports/{ReportId}/content");
                    WriteObject(downloadResponse, false);
                    break;
                case SPECIFICREPORT:
                    var specificReportResponse = SharePointOnlineAdminRequestHelper.Get<Model.EnterpriseAppInsights.ReportMetadata>($"{AdminContext.Url}_api/v2.1/tenants/default/analyticsReports/{ReportId}/topRecordsAppInsights");
                    WriteObject(specificReportResponse, false);
                    break;
                default:
                    var allReportsResponse = SharePointOnlineAdminRequestHelper.GetResultCollection<Model.EnterpriseAppInsights.ReportMetadata>($"{AdminContext.Url}_api/v2.1/tenants/default/analyticsReports/?filter=Entity eq 'AppInsights' and Format eq 'FullDetails'");
                    WriteObject(allReportsResponse, true);
                    break;
            }
        }
    }
}