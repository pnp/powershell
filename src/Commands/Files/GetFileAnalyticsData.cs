using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFileAnalyticsData", DefaultParameterSetName = ParameterSetName_ALL)]
    [OutputType(typeof(List<IActivityStat>))]
    public class GetFileAnalyticsData : PnPWebCmdlet
    {
        private const string ParameterSetName_ANALYTICS_BY_DATE_RANGE = "Analytics by date range";
        private const string ParameterSetName_ALL = "All analytics data";
        private const string ParameterSetName_LAST_SEVEN_DAYS = "Analytics by specific intervals";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetName_ALL, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSetName_ANALYTICS_BY_DATE_RANGE, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSetName_LAST_SEVEN_DAYS, Position = 0, ValueFromPipeline = true)]
        [Alias("ServerRelativeUrl", "SiteRelativeUrl")]
        public string Url;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_ALL)]
        public SwitchParameter All;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_LAST_SEVEN_DAYS)]
        public SwitchParameter LastSevenDays;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_ANALYTICS_BY_DATE_RANGE)]
        public DateTime StartDate;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_ANALYTICS_BY_DATE_RANGE)]
        public DateTime EndDate;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_ANALYTICS_BY_DATE_RANGE)]
        public AnalyticsAggregationInterval AnalyticsAggregationInterval = AnalyticsAggregationInterval.Day;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;

            // Remove URL decoding from the Url as that will not work. We will encode the + character specifically, because if that is part of the filename, it needs to stay and not be decoded.
            Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));

            var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!Url.ToLower().StartsWith(webUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(webUrl, Url);
            }
            else
            {
                serverRelativeUrl = Url;
            }

            IFile analyticsFile = Connection.PnPContext.Web.GetFileByServerRelativeUrl(serverRelativeUrl, p => p.VroomItemID, p => p.VroomDriveID);

            switch (ParameterSetName)
            {
                case ParameterSetName_ALL:
                    // Get analytics for all time
                    var analytics = analyticsFile.GetAnalytics();
                    WriteObject(analytics, true);
                    break;

                case ParameterSetName_LAST_SEVEN_DAYS:
                    // Get analytics for last seven days
                    var analyticsLastSevenDays = analyticsFile.GetAnalytics(new AnalyticsOptions { Interval = AnalyticsInterval.LastSevenDays, CustomAggregationInterval = AnalyticsAggregationInterval.Week });
                    WriteObject(analyticsLastSevenDays, true);
                    break;

                case ParameterSetName_ANALYTICS_BY_DATE_RANGE:

                    if (EndDate == DateTime.MinValue)
                    {
                        EndDate = DateTime.UtcNow;
                    }

                    if (StartDate == DateTime.MinValue)
                    {
                        StartDate = EndDate.AddDays(-90.0);
                    }

                    if (EndDate < StartDate)
                    {
                        throw new PSArgumentException("Invalid Date Range");
                    }

                    if ((EndDate.Date - StartDate.Date).TotalDays > 90)
                    {
                        throw new PSArgumentException("The maximum allowed difference between start and end date is 90 days");
                    }

                    var analyticsCustomData = analyticsFile.GetAnalytics(new AnalyticsOptions
                    {
                        Interval = AnalyticsInterval.Custom,
                        CustomAggregationInterval = AnalyticsAggregationInterval,
                        CustomEndDate = EndDate,
                        CustomStartDate = StartDate,
                    });
                    WriteObject(analyticsCustomData, true);
                    break;
                default:
                    // Get analytics for all time
                    var allAnalytics = analyticsFile.GetAnalytics();
                    WriteObject(allAnalytics, true);
                    break;
            }
        }
    }
}
