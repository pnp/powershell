using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.Set, "PnPSearchVertical", DefaultParameterSetName = ParameterSet_PROPERTIES)]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	public class SetSearchVertical : PnPGcsCmdlet
	{
		private const string ParameterSet_PROPERTIES = "Properties";
		private const string ParameterSet_PAYLOAD = "Payload";

		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Alias("LogicalId")]
		public string Identity;

		[Parameter(Mandatory = false)]
		public SearchVerticalScope Scope = SearchVerticalScope.Site;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
		public string DisplayName;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
		public bool Enabled;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
		public string QueryTemplate;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
		public bool? IncludeConnectorResults;

		[Parameter(Mandatory = true, ParameterSetName = ParameterSet_PAYLOAD)]
		public SearchVerticalPayload Payload;

		protected override void ExecuteCmdlet()
		{
			var headers = GetGcsHeaders();
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

			SearchVerticalPayload payload;

			if (ParameterSetName == ParameterSet_PAYLOAD)
			{
				payload = Payload;
			}
			else
			{
				// GET the current vertical, merge changes, PUT
				LogDebug($"Retrieving current vertical '{Identity}' for update");
				var current = GetWithRetry<SearchVertical>(url, headers);
				if (current?.Payload == null)
				{
					throw new PSArgumentException($"Search vertical '{Identity}' not found or has no payload.", nameof(Identity));
				}

				payload = current.Payload;

				if (ParameterSpecified(nameof(DisplayName)))
					payload.DisplayName = DisplayName;

				if (ParameterSpecified(nameof(Enabled)))
					payload.State = Enabled ? 1 : 0;

				if (ParameterSpecified(nameof(QueryTemplate)))
					payload.QueryTemplate = NormalizeQueryTemplate(QueryTemplate);

				if (ParameterSpecified(nameof(IncludeConnectorResults)))
				{
					if (payload.VerticalType == 1) // Custom vertical
					{
						WriteWarning("-IncludeConnectorResults only applies to built-in verticals (SITEALL/ALL). Ignoring for custom vertical.");
					}
					else
					{
						payload.IncludeConnectorResults = IncludeConnectorResults;
					}
				}

				// Clear server-only fields that shouldn't be sent in the PUT
				payload.LastModifiedBy = null;
			}

			var jsonOptions = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
			};
			var json = JsonSerializer.Serialize(payload, jsonOptions);

			LogDebug($"Updating search vertical '{Identity}': {json}");

			PutWithRetry(url, () => new StringContent(json, System.Text.Encoding.UTF8, "application/json"), headers, verifySuccess: () =>
			{
				try
				{
					var updated = GetWithRetry<SearchVertical>(url, headers);
					return updated != null;
				}
				catch
				{
					return false;
				}
			});
		}

	}
}
