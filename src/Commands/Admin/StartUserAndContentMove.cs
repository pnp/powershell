using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsLifecycle.Start, "PnPUserAndContentMove")]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(PSObject))]
	public class StartUserAndContentMove : PnPSharePointOnlineAdminCmdlet
	{
		private static readonly DateTime MinimumSpecifiedMoveDate = DateTime.MinValue.AddDays(1);

		[Parameter(Mandatory = true, Position = 1)]
		public string UserPrincipalName { get; set; }

		[Parameter(Mandatory = true, Position = 2)]
		public string DestinationDataLocation { get; set; }

		[Parameter(Mandatory = false, Position = 3)]
		public DateTime PreferredMoveBeginDate { get; set; }

		[Parameter(Mandatory = false, Position = 4)]
		public DateTime PreferredMoveEndDate { get; set; }

		[Parameter(Mandatory = false, Position = 5)]
		public string Notify { get; set; }

		[Parameter(Mandatory = false, Position = 6)]
		public string Reserved { get; set; }

		[Parameter(Mandatory = false, Position = 7)]
		public SwitchParameter ValidationOnly { get; set; }

		protected override void ExecuteCmdlet()
		{
			var moveJob = new UserMoveJobEntityData
			{
				UserPrincipalName = UserPrincipalName,
				DestinationDataLocation = DestinationDataLocation,
				Reserve = Reserved,
				Notify = Notify
			};

			if (PreferredMoveBeginDate > MinimumSpecifiedMoveDate)
			{
				moveJob.PreferredMoveBeginDateInUtc = PreferredMoveBeginDate.ToUniversalTime();
			}

			if (PreferredMoveEndDate > MinimumSpecifiedMoveDate)
			{
				moveJob.PreferredMoveEndDateInUtc = PreferredMoveEndDate.ToUniversalTime();
			}

			if (ValidationOnly.ToBool())
			{
				moveJob.Option |= MoveOption.ValidationOnly;
			}

			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			var moveState = multiGeoRestApiClient.CreateUserMoveJob(moveJob);
			if (moveState == null)
			{
				throw new PSInvalidOperationException("The user and content move job could not be created. SharePoint Online did not return a response.");
			}

			WriteObject(UserAndContentMoveStateFormatter.ConvertToPSObject(moveState, IsVerboseMode()));
		}

		private bool IsVerboseMode()
		{
			return MyInvocation.BoundParameters.TryGetValue("Verbose", out var verboseValue) && verboseValue is SwitchParameter verbose && verbose.ToBool();
		}
	}
}
