using PnP.PowerShell.Commands.Model;
using System;
using System.Globalization;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities.MultiGeo
{
	internal static class GroupMoveJobFormatter
	{
		private static readonly DateTime MinSpecificDate = new(1900, 1, 1);
		private static readonly DateTime MaxSpecificDate = new(9000, 1, 1);

		internal static PSObject ConvertToPSObject(GroupMoveJob moveJob, bool includeVerboseProperties, bool includeTimeStamp, bool useStateName)
		{
			var result = new PSObject();
			AddProperty(result, "GroupName", moveJob.GroupName);
			AddProperty(result, "MoveJobId", moveJob.Id);
			AddProperty(result, "SourceDataLocation", moveJob.SourceDataLocation);
			AddProperty(result, "DestinationDataLocation", moveJob.DestinationDataLocation);

			if (includeTimeStamp && !moveJob.Option.HasFlag(MoveOption.ValidationOnly))
			{
				AddProperty(result, "TimeStamp", moveJob.LastModified.ToLocalTime());
			}

			if (useStateName)
			{
				AddProperty(result, moveJob.Option.HasFlag(MoveOption.ValidationOnly) ? "ValidationState" : "MoveState", moveJob.Option.HasFlag(MoveOption.ValidationOnly) ? "Success" : moveJob.StateName);
			}
			else if (moveJob.Option.HasFlag(MoveOption.ValidationOnly))
			{
				AddProperty(result, "ValidationState", "Success");
			}
			else
			{
				AddProperty(result, "MoveState", GetMoveStateDisplayValue(moveJob));
			}

			if (includeVerboseProperties)
			{
				AddVerboseProperties(result, moveJob);
			}

			return result;
		}

		private static void AddVerboseProperties(PSObject result, GroupMoveJob moveJob)
		{
			if (moveJob.State == MoveState.Success)
			{
				AddProperty(result, "IsContentMoved", moveJob.IsContentMoved);
			}

			AddProperty(result, "ErrorMessage", moveJob.ErrorMessage);
			AddProperty(result, "SiteId", moveJob.SiteId);
			AddProperty(result, "MoveDirection", moveJob.Direction);
			AddProperty(result, "MoveJobPhase", moveJob.JobPhase);
			AddProperty(result, "MoveJobType", moveJob.Type);
			AddProperty(result, "PreferredMoveBeginDate", FormatSpecificDate(moveJob.PreferredMoveBeginDateInUtc));
			AddProperty(result, "PreferredMoveEndDate", FormatSpecificDate(moveJob.PreferredMoveEndDateInUtc));
			AddProperty(result, "StartedDate", FormatSpecificDate(moveJob.StartedDateInUtc));
			AddProperty(result, "FinishedDate", FormatSpecificDate(moveJob.FinishedDateInUtc));
			AddProperty(result, "Option", moveJob.Option);
			AddProperty(result, "TriggeredBy", moveJob.TriggeredBy);
		}

		private static string GetMoveStateDisplayValue(GroupMoveJob moveJob)
		{
			return moveJob.State switch
			{
				MoveState.NotStarted => "ReadyToTrigger",
				MoveState.Queued => "Scheduled",
				MoveState.InProgress => string.Format(CultureInfo.InvariantCulture, "{0}({1}/4)", moveJob.State, (int)moveJob.JobPhase),
				_ => moveJob.State.ToString()
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
