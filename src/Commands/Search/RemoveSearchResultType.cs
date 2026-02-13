using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.Remove, "PnPSearchResultType", SupportsShouldProcess = true)]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	public class RemoveSearchResultType : PnPGcsCmdlet
	{
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Alias("LogicalId")]
		public string Identity;

		[Parameter(Mandatory = false)]
		public SearchVerticalScope Scope = SearchVerticalScope.Site;

		[Parameter(Mandatory = false)]
		public SwitchParameter Force;

		protected override void ExecuteCmdlet()
		{
			if (!Force && !ShouldProcess($"Search result type '{Identity}' at {Scope} scope", "Remove"))
				return;

			var headers = GetGcsHeaders();
			var siteId = Scope != SearchVerticalScope.Organization ? (Guid?)GetCurrentSiteId() : null;

			var resolved = ResolveResultType(Identity, Scope, siteId, headers);
			var logicalId = resolved.LogicalId;

			string url;
			if (Scope == SearchVerticalScope.Organization)
				url = GetGcsOrgMrtUrl($"/{logicalId}");
			else
				url = GetGcsMrtUrl(siteId.Value, $"/{logicalId}");

			LogDebug($"Removing search result type '{logicalId}' at {Scope} scope");

			DeleteWithRetry(url, headers, verifySuccess: () =>
			{
				try
				{
					GcsRequestHelper.Get<SearchResultType>(url, additionalHeaders: headers);
					return false; // Still exists
				}
				catch
				{
					return true; // Gone
				}
			});
		}
	}
}
