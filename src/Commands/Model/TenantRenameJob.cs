using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains information about a SharePoint Online tenant rename job.
	/// </summary>
	public class TenantRenameJob
	{
		/// <summary>
		/// Current state of the tenant rename job.
		/// </summary>
		public string JobState { get; set; }

		/// <summary>
		/// User or process that initiated the tenant rename job.
		/// </summary>
		public string TriggeredBy { get; set; }

		/// <summary>
		/// Number of site rename jobs queued for the tenant rename.
		/// </summary>
		public int QueuedSitesCount { get; set; }

		/// <summary>
		/// Number of site rename jobs currently in progress for the tenant rename.
		/// </summary>
		public int InprogressSitesCount { get; set; }

		/// <summary>
		/// Number of suspended site rename jobs for the tenant rename.
		/// </summary>
		public int SuspendedSitesCount { get; set; }

		/// <summary>
		/// Number of successfully completed site rename jobs for the tenant rename.
		/// </summary>
		public int SuccessSitesCount { get; set; }

		/// <summary>
		/// Number of failed site rename jobs for the tenant rename.
		/// </summary>
		public int FailedSitesCount { get; set; }

		/// <summary>
		/// Total number of site rename jobs for the tenant rename.
		/// </summary>
		public int TotalSitesCount { get; set; }

		/// <summary>
		/// Indicates whether the V2 tenant rename status endpoint should be used.
		/// </summary>
		public bool UseGetSpoTenantRenameStatusV2 { get; set; }

		/// <summary>
		/// Scheduled start time for the tenant rename in UTC.
		/// </summary>
		public DateTime ScheduledDateTimeInUtc { get; set; }

		/// <summary>
		/// Time at which the tenant rename was requested.
		/// </summary>
		public DateTime RequestedAt { get; set; }

		/// <summary>
		/// Messages returned by the tenant rename service.
		/// </summary>
		public List<string> ResponseMessages { get; set; }

		/// <summary>
		/// Date and time format used by the tenant rename service messages.
		/// </summary>
		public string DateTimeFormat { get; set; }
	}
}
