using PnP.PowerShell.Commands.Model;
using System;
using System.Globalization;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities.MultiGeo
{
	internal static class UserAndContentMoveStateFormatter
	{
		private static readonly DateTime MinSpecificDate = new(1900, 1, 1);
		private static readonly DateTime MaxSpecificDate = new(9000, 1, 1);

		internal static PSObject ConvertToPSObject(UserAndContentMoveState moveState, bool includeVerboseProperties)
		{
			return ConvertToPSObject(moveState, includeVerboseProperties, JobType.UserMove);
		}

		internal static PSObject ConvertGroupMoveStateToPSObject(UserAndContentMoveState moveState, bool includeVerboseProperties)
		{
			return ConvertToPSObject(moveState, includeVerboseProperties, JobType.GroupMove);
		}

		private static PSObject ConvertToPSObject(UserAndContentMoveState moveState, bool includeVerboseProperties, JobType moveJobType)
		{
			var result = new PSObject();
			AddMoveJobIdentityProperty(result, moveState, moveJobType);
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
				AddVerboseProperties(result, moveState, moveJobType);
			}

			return result;
		}

		private static void AddMoveJobIdentityProperty(PSObject result, UserAndContentMoveState moveState, JobType moveJobType)
		{
			if (moveJobType == JobType.GroupMove)
			{
				AddProperty(result, "GroupName", moveState.GroupName);
				return;
			}

			AddProperty(result, "UserPrincipalName", moveState.UserPrincipalName);
		}

		private static void AddVerboseProperties(PSObject result, UserAndContentMoveState moveState, JobType moveJobType)
		{
			if (moveJobType == JobType.UserMove)
			{
				AddProperty(result, "IsValidPDL", moveState.ValidationResult == PreferredDataLocationValidationResult.Valid);
				AddProperty(result, "HasODBInCurrentLocation", moveState.HasOdbInSourceDataLocation);
			}

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
