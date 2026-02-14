using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Administration;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Search
{
    public enum LogLevel
    {
        All = -1,
        Success = 0,
        Warning = 1,
        Error = 2
    }

    public enum ContentSource
    {
        Sites,
        UserProfiles
    }

    public class CrawlEntry
    {
        public string Url { get; set; }
        public DateTime CrawlTime { get; set; }
        public DateTime ItemTime { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Status { get; set; }
        public int ItemId { get; set; }
        public int ContentSourceId { get; set; }
    }

    [Cmdlet(VerbsCommon.Get, "PnPSearchCrawlLog", DefaultParameterSetName = "Xml")]
    [ApiNotAvailableUnderApplicationPermissions]
    [Obsolete("The underlying API for this cmdlet has been deprecated by Microsoft and no longer returns results. This cmdlet will be removed in a future version.")]
    public class GetSearchCrawlLog : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public LogLevel LogLevel = LogLevel.All;

        [Parameter(Mandatory = false)]
        public int RowLimit = 100;

        [Parameter(Mandatory = false)]
        public string Filter;

        [Parameter(Mandatory = false)]
        public ContentSource ContentSource = ContentSource.Sites;

        [Parameter(Mandatory = false)]
        public DateTime StartDate = DateTime.MinValue;

        [Parameter(Mandatory = false)]
        public DateTime EndDate = DateTime.UtcNow.AddDays(1);

        [Parameter(Mandatory = false)]
        public SwitchParameter RawFormat;

        private const int MaxRows = 100000;

        protected override void ExecuteCmdlet()
        {
            throw new NotSupportedException("The underlying API for Get-PnPSearchCrawlLog has been deprecated by Microsoft and no longer returns results. This cmdlet will be removed in a future version.");
        }

    }
}
