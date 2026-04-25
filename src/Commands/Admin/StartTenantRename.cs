using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using PnP.PowerShell.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
	[Cmdlet(VerbsLifecycle.Start, "PnPTenantRename", DefaultParameterSetName = ParameterSetFullRename, SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
	[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
	[OutputType(typeof(string))]
	public class StartTenantRename : PnPSharePointOnlineAdminCmdlet
	{
		private const string ParameterSetFullRename = "FullRename";

		[Parameter(Mandatory = true, ParameterSetName = ParameterSetFullRename)]
		[ValidateNotNullOrEmpty]
		public string DomainName { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = ParameterSetFullRename)]
		[ValidateNotNullOrEmpty]
		public DateTime ScheduledDateTime { get; set; }

		protected override void ExecuteCmdlet()
		{
			var targetDomainPrefix = DomainName.Trim();
			var scheduledDateTimeInUtc = GetValidatedScheduledDateTimeInUtc(ScheduledDateTime);
			var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);

			WriteResponse(multiGeoRestApiClient.GetTenantRenameWarningMessages());

			if (!ShouldProcess(targetDomainPrefix, $"Schedule SharePoint tenant rename for {scheduledDateTimeInUtc:u}"))
			{
				return;
			}

			var tenantRenameJob = new TenantRenameJobEntityData
			{
				TargetDomainPrefix = targetDomainPrefix,
				ScheduledDateTimeInUtc = scheduledDateTimeInUtc
			};

			var tenantRenameJobResponse = multiGeoRestApiClient.CreateTenantRenameJob(tenantRenameJob);
			if (tenantRenameJobResponse?.ResponseMessages == null)
			{
				throw new PSInvalidOperationException("The tenant rename job could not be created. SharePoint Online did not return a response message.");
			}

			WriteResponse(tenantRenameJobResponse.ResponseMessages);
		}

		private static DateTime GetValidatedScheduledDateTimeInUtc(DateTime scheduledDateTime)
		{
			var scheduledDateTimeInUtc = scheduledDateTime.ToUniversalTime();
			var utcNow = DateTime.UtcNow;
			var minimumScheduledDateTimeInUtc = utcNow.AddHours(24);
			var maximumScheduledDateTimeInUtc = utcNow.AddDays(30);

			if (scheduledDateTimeInUtc < minimumScheduledDateTimeInUtc)
			{
				throw new PSArgumentException("ScheduledDateTime must be at least 24 hours in the future.", nameof(ScheduledDateTime));
			}

			if (scheduledDateTimeInUtc > maximumScheduledDateTimeInUtc)
			{
				throw new PSArgumentException("ScheduledDateTime must be no more than 30 days in the future.", nameof(ScheduledDateTime));
			}

			return scheduledDateTimeInUtc;
		}

		private void WriteResponse(IEnumerable<string> messages)
		{
			if (messages == null)
			{
				return;
			}

			foreach (var message in messages)
			{
				if (!string.IsNullOrWhiteSpace(message))
				{
					WriteObject(message);
				}
			}
		}
	}
}
