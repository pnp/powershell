using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System;
using System.Globalization;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Get, "PnPUserAndContentMoveState", DefaultParameterSetName = ParameterSetMoveReport)]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(PSObject))]
	public class GetUserAndContentMoveState : PnPSharePointOnlineAdminCmdlet
	{
		private const string ParameterSetMoveReport = "MoveReport";
		private const string ParameterSetUserPrincipalName = "UserPrincipalName";
		private const string ParameterSetOdbMoveId = "OdbMoveId";
		private static readonly DateTime MinSpecificDate = new(1900, 1, 1);
		private static readonly DateTime MaxSpecificDate = new(9000, 1, 1);

		[Parameter(Mandatory = true, ParameterSetName = ParameterSetUserPrincipalName)]
		[ValidateNotNullOrEmpty]
		public string UserPrincipalName { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = ParameterSetOdbMoveId)]
		[ValidateNotNullOrEmpty]
		public Guid OdbMoveId { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = ParameterSetMoveReport)]
		[ValidateRange(1, 1000)]
		public uint Limit { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = ParameterSetMoveReport)]
		public DateTime MoveStartTime { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = ParameterSetMoveReport)]
		public DateTime MoveEndTime { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = ParameterSetMoveReport)]
		public MoveState MoveState { get; set; } = MoveState.All;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSetMoveReport)]
		public MoveDirection MoveDirection { get; set; } = MoveDirection.All;

		protected override void ExecuteCmdlet()
		{
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			var includeVerboseProperties = IsVerboseMode();

			if (ParameterSetName == ParameterSetUserPrincipalName)
			{
				WriteMoveState(multiGeoRestApiClient.GetUserAndContentMoveState(UserPrincipalName), includeVerboseProperties);
				return;
			}

			if (ParameterSetName == ParameterSetOdbMoveId)
			{
				WriteMoveState(multiGeoRestApiClient.GetUserAndContentMoveState(OdbMoveId), includeVerboseProperties);
				return;
			}

			var moveStartTimeInUtc = MoveStartTime == DateTime.MinValue ? DateTime.MinValue : MoveStartTime.ToUniversalTime();
			var moveEndTimeInUtc = MoveEndTime == DateTime.MinValue ? DateTime.MinValue : MoveEndTime.ToUniversalTime();
			var moveStates = multiGeoRestApiClient.GetUserAndContentMoveStates(MoveState, MoveDirection, moveStartTimeInUtc, moveEndTimeInUtc, Limit)
				.Where(moveState => moveState != null)
				.OrderByDescending(moveState => moveState.LastModified)
				.Select(moveState => ConvertToPSObject(moveState, includeVerboseProperties));

			WriteObject(moveStates, true);
		}

		private void WriteMoveState(UserAndContentMoveState moveState, bool includeVerboseProperties)
		{
			if (moveState != null)
			{
				WriteObject(ConvertToPSObject(moveState, includeVerboseProperties));
			}
		}

		private bool IsVerboseMode()
		{
			return MyInvocation.BoundParameters.TryGetValue("Verbose", out var verboseValue) && verboseValue is SwitchParameter verbose && verbose.ToBool();
		}

		private static PSObject ConvertToPSObject(UserAndContentMoveState moveState, bool includeVerboseProperties)
		{
			var result = new PSObject();
			AddProperty(result, "UserPrincipalName", moveState.UserPrincipalName);
			AddProperty(result, "MoveJobId", moveState.Id);
			AddProperty(result, "SourceDataLocation", moveState.SourceDataLocation);
			AddProperty(result, "DestinationDataLocation", moveState.DestinationDataLocation);

			if (moveState.Option.HasFlag(MoveOption.ValidationOnly))
			{
				AddProperty(result, "ValidationState", "Success");
			}
			else
			{
				AddProperty(result, "TimeStamp", moveState.LastModified.ToLocalTime());
				AddProperty(result, "MoveState", GetMoveStateDisplayValue(moveState));
			}

			if (includeVerboseProperties)
			{
				AddVerboseProperties(result, moveState);
			}

			return result;
		}

		private static void AddVerboseProperties(PSObject result, UserAndContentMoveState moveState)
		{
			AddProperty(result, "IsValidPDL", moveState.ValidationResult == PreferredDataLocationValidationResult.Valid);
			AddProperty(result, "HasODBInCurrentLocation", moveState.HasOdbInSourceDataLocation);

			if (moveState.State == MoveState.Success)
			{
				AddProperty(result, "IsContentMoved", moveState.IsContentMoved);
			}

			AddProperty(result, "ErrorMessage", moveState.ErrorMessage);
			AddProperty(result, "SiteId", moveState.SiteId);
			AddProperty(result, "MoveDirection", moveState.Direction);
			AddProperty(result, "MoveJobPhase", moveState.JobPhase);
			AddProperty(result, "MoveJobType", moveState.Type);
			AddProperty(result, "PreferredMoveBeginDate", FormatSpecificDate(moveState.PreferredMoveBeginDateInUtc));
			AddProperty(result, "PreferredMoveEndDate", FormatSpecificDate(moveState.PreferredMoveEndDateInUtc));
			AddProperty(result, "StartedDate", FormatSpecificDate(moveState.StartedDateInUtc));
			AddProperty(result, "FinishedDate", FormatSpecificDate(moveState.FinishedDateInUtc));
			AddProperty(result, "Option", moveState.Option);
			AddProperty(result, "TriggeredBy", moveState.TriggeredBy);
		}

		private static string GetMoveStateDisplayValue(UserAndContentMoveState moveState)
		{
			if (!string.IsNullOrWhiteSpace(moveState.StateName))
			{
				return moveState.StateName;
			}

			return moveState.State switch
			{
				MoveState.NotStarted => "ReadyToTrigger",
				MoveState.Queued => "Scheduled",
				MoveState.InProgress => string.Format(CultureInfo.InvariantCulture, "{0}({1}/4)", moveState.State, (int)moveState.JobPhase),
				_ => moveState.State.ToString()
			};
		}

		private static DateTime? FormatSpecificDate(DateTime dateTime)
		{
			return dateTime > MinSpecificDate && dateTime < MaxSpecificDate ? dateTime.ToLocalTime() : null;
		}

		private static void AddProperty(PSObject result, string name, object value)
		{
			result.Properties.Add(new PSNoteProperty(name, value));
		}
	}
}