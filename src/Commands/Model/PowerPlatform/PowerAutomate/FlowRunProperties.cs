using System;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains additional information of one Microsoft Power Automate Flow run.
    /// </summary>
    public class FlowRunProperties
    {
        /// <summary>
        /// Start time of the Flow run.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End time of the Flow run.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Flow run status. Succeeded, failed, cancelled.
        /// </summary>
        public string Status { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// Contains additional information about the trigger.
        /// </summary>
        public FlowRunTrigger Trigger { get; set; }
    }
}
