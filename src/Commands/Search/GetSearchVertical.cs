using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.Get, "PnPSearchVertical")]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	[OutputType(typeof(IEnumerable<SearchVertical>))]
	[OutputType(typeof(SearchVertical))]
	public class GetSearchVertical : PnPGcsCmdlet
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
				string url;
				if (Scope == SearchVerticalScope.Organization)
				{
					url = GetGcsOrgVerticalUrl($"/{Identity}");
				}
				else
				{
					var siteId = GetCurrentSiteId();
					url = GetGcsVerticalUrl(siteId, $"/{Identity}");
				}

				LogDebug($"Retrieving search vertical with Identity '{Identity}' at {Scope} scope");

				var result = GetWithRetry<SearchVertical>(url, headers);
				WriteObject(result, false);
			}
			else
			{
				string url;
				if (Scope == SearchVerticalScope.Organization)
				{
					url = GetGcsOrgVerticalUrl("?oob=true");
				}
				else
				{
					var siteId = GetCurrentSiteId();
					url = GetGcsVerticalUrl(siteId, "?oob=true");
				}

				LogDebug($"Retrieving all search verticals at {Scope} scope");

				var collection = GetWithRetry<SearchVerticalCollection>(url, headers);
				WriteObject(collection?.Verticals, true);
			}
		}
	}
}
