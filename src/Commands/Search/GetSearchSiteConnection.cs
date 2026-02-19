using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.Get, "PnPSearchSiteConnection")]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	[OutputType(typeof(IEnumerable<SearchSiteConnection>))]
	[OutputType(typeof(SearchSiteConnection))]
	public class GetSearchSiteConnection : PnPGcsCmdlet
	{
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
		public string Identity;

		protected override void ExecuteCmdlet()
		{
			var siteId = GetCurrentSiteId();
			var headers = GetGcsHeaders();
			var url = GetGcsSiteConnectionsUrl(siteId);

			LogDebug("Retrieving site connections");

			var collection = GetWithRetry<SearchSiteConnectionCollection>(url, headers);
			var connections = collection?.Connections;

			if (ParameterSpecified(nameof(Identity)) && connections != null)
			{
				var match = connections.Find(c => string.Equals(c.Id, Identity, System.StringComparison.OrdinalIgnoreCase));
				WriteObject(match, false);
			}
			else
			{
				WriteObject(connections, true);
			}
		}
	}
}
