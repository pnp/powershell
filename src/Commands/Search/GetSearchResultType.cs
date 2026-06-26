using System;
using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.Get, "PnPSearchResultType")]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	[OutputType(typeof(IEnumerable<SearchResultType>))]
	[OutputType(typeof(SearchResultType))]
	public class GetSearchResultType : PnPGcsCmdlet
	{
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Alias("LogicalId")]
		public string Identity;

		[Parameter(Mandatory = false)]
		public SearchVerticalScope Scope = SearchVerticalScope.Site;

		protected override void ExecuteCmdlet()
		{
			var headers = GetGcsHeaders();

			if (ParameterSpecified(nameof(Identity)))
			{
				var siteId = Scope != SearchVerticalScope.Organization ? (Guid?)GetCurrentSiteId() : null;
				var result = ResolveResultType(Identity, Scope, siteId, headers);
				WriteObject(result, false);
			}
			else
			{
				string url;
				if (Scope == SearchVerticalScope.Organization)
				{
					url = GetGcsOrgMrtUrl(null);
				}
				else
				{
					var siteId = GetCurrentSiteId();
					url = GetGcsMrtUrl(siteId, null);
				}

				LogDebug($"Retrieving all search result types at {Scope} scope");

				var collection = GetWithRetry<SearchResultTypeCollection>(url, headers);
				WriteObject(collection?.ResultTypes, true);
			}
		}
	}
}
