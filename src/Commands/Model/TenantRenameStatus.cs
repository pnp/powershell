using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Status of the SharePoint Online tenant rename job, as returned by Get-PnPTenantRenameStatus.
    /// </summary>
    public class TenantRenameStatus
    {
        /// <summary>
        /// Identifier of the tenant rename job.
        /// </summary>
        public Guid RenameJobId { get; set; }

        /// <summary>
        /// Current state of the tenant rename job, for example Queued or Success.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Date and time, in UTC, at which the tenant rename was requested.
        /// </summary>
        public DateTime RequestedAt { get; set; }

        /// <summary>
        /// User or process that requested the tenant rename.
        /// </summary>
        public string RequestedBy { get; set; }

        /// <summary>
        /// Date and time, in UTC, at which the tenant rename is scheduled to start.
        /// </summary>
        public DateTime ScheduledAt { get; set; }

        /// <summary>
        /// The new domain prefix the tenant is being renamed to.
        /// </summary>
        public string NewDomainName { get; set; }

        /// <summary>
        /// Total number of sites included in the tenant rename.
        /// </summary>
        public int TotalSites { get; set; }

        /// <summary>
        /// Number of sites currently being renamed. Empty when SharePoint Online could not return the site rename progress.
        /// </summary>
        public int? InProgress { get; set; }

        /// <summary>
        /// Number of sites renamed successfully. Empty when SharePoint Online could not return the site rename progress.
        /// </summary>
        public int? Renamed { get; set; }

        /// <summary>
        /// Number of site renames that failed or are suspended. Empty when SharePoint Online could not return the site rename progress.
        /// </summary>
        public int? AttentionRequired { get; set; }
    }
}
