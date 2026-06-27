using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the state of a SharePoint Online unified group multi-geo move job.
	/// </summary>
	internal class GroupMoveJob
	{
		public string ApiVersion { get; set; }

		public Guid Id { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public JobType Type { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public MoveOption Option { get; set; }

		public string Reserve { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public MoveState State { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public MoveJobPhase JobPhase { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public MoveDirection Direction { get; set; }

		public Guid SiteId { get; set; }

		public string SourceDataLocation { get; set; }

		public string DestinationDataLocation { get; set; }

		public DateTime PreferredMoveBeginDateInUtc { get; set; }

		public DateTime PreferredMoveEndDateInUtc { get; set; }

		public DateTime FinishedDateInUtc { get; set; }

		public string TriggeredBy { get; set; }

		public string CancelTriggeredBy { get; set; }

		public string ErrorMessage { get; set; }

		public string Notify { get; set; }

		public string GroupName { get; set; }

		public DateTime StartedDateInUtc { get; set; }

		public DateTime LastModified { get; set; }

		public bool IsContentMoved { get; set; }

		public string StateName { get; set; }
	}
}
