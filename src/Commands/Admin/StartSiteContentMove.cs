using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsLifecycle.Start, "PnPSiteContentMove", DefaultParameterSetName = ParameterSetUrlAndDestinationDataLocation)]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(PSObject))]
	public class StartSiteContentMove : PnPSharePointOnlineAdminCmdlet
	{
		private const string ParameterSetUrlAndDestinationDataLocation = "UrlAndDestinationDataLocation";
		private const string ParameterSetUrlAndDestinationUrl = "UrlAndDestinationUrl";
		private const string TimeStampMinimumApiVersion = "1.3.2";
		private const string StateNameMinimumApiVersion = "1.4.3";
		private static readonly DateTime MinimumSpecifiedMoveDate = DateTime.MinValue.AddDays(1);

		[Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSetUrlAndDestinationDataLocation)]
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSetUrlAndDestinationUrl)]
		public string SourceSiteUrl { get; set; }

		[Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSetUrlAndDestinationDataLocation)]
		public string DestinationDataLocation { get; set; }

		[Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSetUrlAndDestinationUrl)]
		public string DestinationUrl { get; set; }

		[Parameter(Mandatory = false, Position = 2)]
		public DateTime PreferredMoveBeginDate { get; set; }

		[Parameter(Mandatory = false, Position = 3)]
		public DateTime PreferredMoveEndDate { get; set; }

		[Parameter(Mandatory = false, Position = 4)]
		public string Reserved { get; set; }

		[Parameter(Mandatory = false, Position = 5)]
		public SwitchParameter ValidationOnly { get; set; }

		[Parameter(Mandatory = false, Position = 6)]
		public SwitchParameter Force { get; set; }

		[Parameter(Mandatory = false, Position = 7)]
		public SwitchParameter SuppressMarketplaceAppCheck { get; set; }

		[Parameter(Mandatory = false, Position = 8)]
		public SwitchParameter SuppressWorkflow2013Check { get; set; }

		[Parameter(Mandatory = false, Position = 9)]
		public SwitchParameter SuppressAllWarnings { get; set; }

		[Parameter(Mandatory = false, Position = 10)]
		public SwitchParameter SuppressBcsCheck { get; set; }

		protected override void ExecuteCmdlet()
		{
			var moveJob = new SiteMoveJobEntityData
			{
				SourceSiteUrl = SourceSiteUrl,
				DestinationDataLocation = DestinationDataLocation,
				TargetSiteUrl = DestinationUrl,
				Reserve = Reserved
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

			if (Force.ToBool() || SuppressAllWarnings.ToBool())
			{
				moveJob.Option |= MoveOption.Force;
			}

			if (SuppressMarketplaceAppCheck.ToBool())
			{
				moveJob.Option |= MoveOption.SuppressMarketplaceAppCheck;
			}

			if (SuppressWorkflow2013Check.ToBool())
			{
				moveJob.Option |= MoveOption.SuppressWorkflow2013Check;
			}

			if (SuppressBcsCheck.ToBool())
			{
				moveJob.Option |= MoveOption.SuppressBcsCheck;
			}

			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
			var createdMoveJob = multiGeoRestApiClient.CreateSiteMoveJob(moveJob);
			if (createdMoveJob == null)
			{
				throw new PSInvalidOperationException("The site content move job could not be created. SharePoint Online did not return a response.");
			}

			WriteObject(UserAndContentMoveStateFormatter.ConvertSiteMoveStateToPSObject(
				createdMoveJob,
				IsVerboseMode(),
				multiGeoRestApiClient.IsCurrentApiVersionSupported(TimeStampMinimumApiVersion),
				multiGeoRestApiClient.IsCurrentApiVersionSupported(StateNameMinimumApiVersion)));
		}

		private bool IsVerboseMode()
		{
			return MyInvocation.BoundParameters.TryGetValue("Verbose", out var verboseValue) && verboseValue is SwitchParameter verbose && verbose.ToBool();
		}
	}
}
