using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System;
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
				.Select(moveState => UserAndContentMoveStateFormatter.ConvertToPSObject(moveState, includeVerboseProperties));

			WriteObject(moveStates, true);
		}

		private void WriteMoveState(UserAndContentMoveState moveState, bool includeVerboseProperties)
		{
			if (moveState != null)
			{
				WriteObject(UserAndContentMoveStateFormatter.ConvertToPSObject(moveState, includeVerboseProperties));
			}
		}

		private bool IsVerboseMode()
		{
			return MyInvocation.BoundParameters.TryGetValue("Verbose", out var verboseValue) && verboseValue is SwitchParameter verbose && verbose.ToBool();
		}
	}
}