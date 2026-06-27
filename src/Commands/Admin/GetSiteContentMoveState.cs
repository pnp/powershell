using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsCommon.Get, "PnPSiteContentMoveState", DefaultParameterSetName = ParameterSetMoveReport)]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(PSObject))]
	public class GetSiteContentMoveState : PnPSharePointOnlineAdminCmdlet
	{
		private const string ParameterSetMoveReport = "MoveReport";
		private const string ParameterSetSourceSiteUrl = "SourceSiteUrl";
		private const string ParameterSetSiteMoveId = "SiteMoveId";

		[Parameter(Mandatory = true, ParameterSetName = ParameterSetSourceSiteUrl)]
		[ValidateNotNullOrEmpty]
		public string SourceSiteUrl { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = ParameterSetSiteMoveId)]
		[ValidateNotNullOrEmpty]
		public Guid SiteMoveId { get; set; }

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

			if (ParameterSetName == ParameterSetSourceSiteUrl)
			{
				WriteMoveState(multiGeoRestApiClient.GetSiteMoveJob(SourceSiteUrl), includeVerboseProperties);
				return;
			}

			if (ParameterSetName == ParameterSetSiteMoveId)
			{
				WriteMoveState(multiGeoRestApiClient.GetSiteMoveJob(SiteMoveId), includeVerboseProperties);
				return;
			}

			var moveStartTimeInUtc = MoveStartTime == DateTime.MinValue ? DateTime.MinValue : MoveStartTime.ToUniversalTime();
			var moveEndTimeInUtc = MoveEndTime == DateTime.MinValue ? DateTime.MinValue : MoveEndTime.ToUniversalTime();
			var moveStates = multiGeoRestApiClient.GetSiteMoveJobs(MoveState, MoveDirection, moveStartTimeInUtc, moveEndTimeInUtc, Limit)
				.Where(moveState => moveState != null)
				.OrderByDescending(moveState => moveState.LastModified)
				.Select(moveState => UserAndContentMoveStateFormatter.ConvertSiteMoveStateToPSObject(moveState, includeVerboseProperties));

			WriteObject(moveStates, true);
		}

		private void WriteMoveState(SiteMoveJob moveState, bool includeVerboseProperties)
		{
			if (moveState != null)
			{
				WriteObject(UserAndContentMoveStateFormatter.ConvertSiteMoveStateToPSObject(moveState, includeVerboseProperties));
			}
		}

		private bool IsVerboseMode()
		{
			return MyInvocation.BoundParameters.TryGetValue("Verbose", out var verboseValue) && verboseValue is SwitchParameter verbose && verbose.ToBool();
		}
	}
}
