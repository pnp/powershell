using System;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the request data for a SharePoint Online unified group multi-geo move job.
	/// </summary>
	internal class GroupMoveJobEntityData
	{
		public string ApiVersion { get; set; }

		public Guid Id { get; set; }

		public JobType Type { get; set; }

		public MoveOption Option { get; set; }

		public string Reserve { get; set; }

		public MoveState State { get; set; }

		public MoveJobPhase JobPhase { get; set; }

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
	}
}
