using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.New, "PnPSearchVertical", DefaultParameterSetName = ParameterSet_DEFAULT)]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	[OutputType(typeof(SearchVertical))]
	public class NewSearchVertical : PnPGcsCmdlet
	{
		private const string ParameterSet_DEFAULT = "Default";
		private const string ParameterSet_PAYLOAD = "Payload";

		[Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULT)]
		public string DisplayName;

		[Parameter(Mandatory = false)]
		public string Identity;

		[Parameter(Mandatory = false)]
		public SearchVerticalScope Scope = SearchVerticalScope.Site;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
		public bool Enabled;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
		public string QueryTemplate;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
		public object[] ContentSources;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
		public bool? IncludeConnectorResults;

		[Parameter(Mandatory = true, ParameterSetName = ParameterSet_PAYLOAD)]
		public SearchVerticalPayload Payload;

		protected override void ExecuteCmdlet()
		{
			if (ParameterSpecified(nameof(IncludeConnectorResults)) && ParameterSetName == ParameterSet_DEFAULT)
			{
				WriteWarning("IncludeConnectorResults is ignored for custom verticals. It can only be used on built-in verticals (SITEALL at site scope, ALL at organization scope). Use Set-PnPSearchVertical to modify built-in verticals.");
				IncludeConnectorResults = null;
			}

			var headers = GetGcsHeaders();

			SearchVerticalPayload payload;

			if (ParameterSetName == ParameterSet_PAYLOAD)
			{
				payload = Payload;

				// Clear server-only fields that shouldn't be sent when creating
				payload.LastModifiedBy = null;
			}
			else
			{
				var sources = ResolveContentSources(out var entityType);

				payload = new SearchVerticalPayload
				{
					DisplayName = DisplayName,
					State = Enabled ? 1 : 0,
					VerticalType = 1,
					QueryTemplate = NormalizeQueryTemplate(QueryTemplate),
					ExtendedQueryTemplate = "",
					TemplateType = "Custom",
					Entities = new List<SearchVerticalEntity>
					{
						new SearchVerticalEntity
						{
							EntityType = entityType,
							ContentSources = sources,
							RefinerIds = new List<string>()
						}
					},
					Refiners = new List<SearchVerticalRefiner>(),
					Scope = (this.Scope == SearchVerticalScope.Organization) ? 0 : 1,
					AllowedActions = 247,
					IncludeConnectorResults = IncludeConnectorResults
				};
			}

			var logicalId = !string.IsNullOrEmpty(Identity) ? Identity : GenerateLogicalId();
			var body = new SearchVertical
			{
				LogicalId = logicalId,
				Payload = payload
			};

			var jsonOptions = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
			};
			var json = JsonSerializer.Serialize(body, jsonOptions);

			LogDebug($"Creating search vertical: {json}");

			string url;
			string getUrl;
			if (this.Scope == SearchVerticalScope.Organization)
			{
				url = GetGcsOrgVerticalUrl("/");
				getUrl = GetGcsOrgVerticalUrl($"/{logicalId}");
			}
			else
			{
				var siteId = GetCurrentSiteId();
				url = GetGcsVerticalUrl(siteId, "/");
				getUrl = GetGcsVerticalUrl(siteId, $"/{logicalId}");
			}

			var result = PostAndGet<SearchVertical>(url, getUrl, json, headers);
			WriteObject(result, false);
		}

		private List<SearchVerticalContentSource> ResolveContentSources(out string entityType)
		{
			if (ContentSources == null || ContentSources.Length == 0)
			{
				entityType = "File";
				return new List<SearchVerticalContentSource> { new SearchVerticalContentSource { Id = "SharePoint", Name = "SharePoint" } };
			}

			var sources = new List<SearchVerticalContentSource>();
			SearchSiteConnectionCollection connectionCache = null;

			foreach (var item in ContentSources)
			{
				var unwrapped = item is PSObject psObj ? psObj.BaseObject : item;

				if (unwrapped is SearchVerticalContentSource svcs)
				{
					sources.Add(svcs);
				}
				else if (unwrapped is SearchSiteConnection conn)
				{
					sources.Add(new SearchVerticalContentSource { Id = conn.Id, Name = conn.Name });
				}
				else
				{
					var value = unwrapped?.ToString();
					if (string.IsNullOrEmpty(value))
						throw new PSArgumentException("ContentSources contains an empty value.", nameof(ContentSources));

					if (string.Equals(value, "SharePoint", StringComparison.OrdinalIgnoreCase))
					{
						sources.Add(new SearchVerticalContentSource { Id = "SharePoint", Name = "SharePoint" });
					}
					else
					{
						if (connectionCache == null)
						{
							var siteId = GetCurrentSiteId();
							var headers = GetGcsHeaders();
							var url = GetGcsSiteConnectionsUrl(siteId);
							connectionCache = GetWithRetry<SearchSiteConnectionCollection>(url, headers);
						}
						var found = connectionCache?.Connections?.Find(c => string.Equals(c.Id, value, StringComparison.OrdinalIgnoreCase));
						if (found == null)
							throw new PSArgumentException($"Site connection '{value}' not found. Use Get-PnPSearchSiteConnection to list available connections.", nameof(ContentSources));
						sources.Add(new SearchVerticalContentSource { Id = found.Id, Name = found.Name });
					}
				}
			}

			var hasFileSource = sources.Any(s =>
				string.Equals(s.Id, "SharePoint", StringComparison.OrdinalIgnoreCase));
			entityType = hasFileSource ? "File" : "External";

			return sources;
		}

	}
}
