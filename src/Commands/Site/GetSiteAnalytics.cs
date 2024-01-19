using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteAnalyticsData")]
    [OutputType(typeof(System.Collections.Generic.List<IActivityStat>))]
    public class GetSiteAnalytics : PnPSharePointCmdlet
    {
        private const string ParameterSetName_ANALYTICS_BY_DATE_RANGE = "Analytics by date range";
        private const string ParameterSetName_ALL = "All analytics data";
        private const string ParameterSetName_LAST_SEVEN_DAYS = "Analytics by specific intervals";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_ALL, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_ANALYTICS_BY_DATE_RANGE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_LAST_SEVEN_DAYS, ValueFromPipeline = true)]
        [Alias("Url")]
        public string Identity;

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
            var analyticsSite = PnPContext.Site;
            if (!string.IsNullOrEmpty(Identity))
            {
                var pnpClonedContext = PnPContext.Clone(new Uri(Identity));
                analyticsSite = pnpClonedContext.Site;
            }
                
            switch (ParameterSetName)
            {
                case ParameterSetName_ALL:
                    // Get analytics for all time
                    var analytics = analyticsSite.GetAnalytics();
                    WriteObject(analytics, true);
                    break;

                case ParameterSetName_LAST_SEVEN_DAYS:
                    // Get analytics for last seven days
                    var analyticsLastSevenDays = analyticsSite.GetAnalytics(new AnalyticsOptions { Interval = AnalyticsInterval.LastSevenDays, CustomAggregationInterval = AnalyticsAggregationInterval.Week });
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

                    var analyticsCustomData = analyticsSite.GetAnalytics(new AnalyticsOptions
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
                    var allAnalytics = analyticsSite.GetAnalytics();
                    WriteObject(allAnalytics, true);
                    break;
            }
        }
    }
}
