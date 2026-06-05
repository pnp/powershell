using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the state of a SharePoint Online user and OneDrive content move job.
	/// </summary>
	public class UserAndContentMoveState
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

		public bool HasOdbInSourceDataLocation { get; set; }

		public string UserPrincipalName { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public PreferredDataLocationValidationResult ValidationResult { get; set; }

		public bool IsContentMoved { get; set; }

		public DateTime LastModified { get; set; }

		public DateTime StartedDateInUtc { get; set; }

		public string StateName { get; set; }
	}

	public enum MoveState
	{
		All = -1,
		NotStarted = 0,
		InProgress = 1,
		Success = 2,
		Failed = 3,
		Stopped = 4,
		Queued = 5,
		NotSupported = 6,
		Rescheduled = 8
	}

	public enum MoveDirection
	{
		MoveOut = 0,
		MoveIn = 1,
		All = 2
	}

	public enum PreferredDataLocationValidationResult
	{
		Invalid = 0,
		Valid = 1
	}

	public enum MoveJobPhase
	{
		InitialStage = 0,
		SourceStage = 1,
		TargetStage = 2,
		PostMoveStage = 3,
		FinalStage = 255
	}

	[Flags]
	public enum MoveOption
	{
		None = 0,
		OverwriteOdb = 1,
		ValidationOnly = 2,
		SuppressMarketplaceAppCheck = 8,
		SuppressWorkflow2013Check = 16,
		ContinueFromConfirmation = 32,
		ValidationOnlySource = 64,
		SuppressBcsCheck = 128,
		EnableGLSSupportForXGeoMove = 256,
		Force = int.MinValue
	}

	public enum JobType
	{
		UserMove = 0,
		GroupMove = 1,
		SiteMove = 2
	}

	public enum JobSubType
	{
		None = 0,
		CSPSiteMove = 1
	}
}