using System;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    public class FlowRunTrigger
    {
        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime ScheduledTime { get; set; }

        public string OriginHistoryName { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }
    }
}
