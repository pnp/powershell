using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the state of a SharePoint Online site content move job.
	/// </summary>
	public class SiteMoveJob
	{
		public string ApiVersion { get; set; }

		public Guid Id { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public MoveOption Option { get; set; }

		public string Reserve { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public JobSubType SubType { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public JobType Type { get; set; }

		public Guid BatchId { get; set; }

		public string CancelTriggeredBy { get; set; }

		public string DestinationDataLocation { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public MoveDirection Direction { get; set; }

		public string ErrorMessage { get; set; }

		public DateTime FinishedDateInUtc { get; set; }

		public bool IsReadOnlyAlertRaised { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public MoveJobPhase JobPhase { get; set; }

		public string Notify { get; set; }

		public DateTime PreferredMoveBeginDateInUtc { get; set; }

		public DateTime PreferredMoveEndDateInUtc { get; set; }

		public Guid SiteId { get; set; }

		public string SourceDataLocation { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public MoveState State { get; set; }

		public string TriggeredBy { get; set; }

		public bool EnableRestoreOnSiteToMove { get; set; }

		public bool EnableSiteToMoveDatastore { get; set; }

		public Guid SourceCompanyId { get; set; }

		public Guid SourceInstanceId { get; set; }

		public string SourceMySiteHostUrl { get; set; }

		public Guid SourceSiteSubscriptionId { get; set; }

		public string SourceSiteUrl { get; set; }

		public Guid TargetCompanyId { get; set; }

		public string TargetFarmId { get; set; }

		public Guid TargetInstanceId { get; set; }

		public Guid TargetSiteSubscriptionId { get; set; }

		public string TargetSiteUrl { get; set; }

		public string TenantMergeSourceMySiteHostUrl { get; set; }

		public string TenantMergeTargetMySiteHostUrl { get; set; }

		public bool IsContentMoved { get; set; }

		public DateTime LastModified { get; set; }

		public DateTime StartedDateInUtc { get; set; }

		public string StateName { get; set; }
	}
}
