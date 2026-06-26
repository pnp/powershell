using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.New, "PnPSearchResultType", DefaultParameterSetName = ParameterSet_DEFAULT)]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	[OutputType(typeof(SearchResultType))]
	public class NewSearchResultType : PnPGcsCmdlet
	{
		private const string ParameterSet_DEFAULT = "Default";
		private const string ParameterSet_PAYLOAD = "Payload";

		[Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULT)]
		public string Name;

		[Parameter(Mandatory = false)]
		public string Identity;

		[Parameter(Mandatory = false)]
		public SearchVerticalScope Scope = SearchVerticalScope.Site;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
		public SearchResultTypeRule[] Rules;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
		public string DisplayTemplate;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
		public string[] DisplayProperties;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
		public object ContentSource;

		[Parameter(Mandatory = false)]
		public SwitchParameter Validate;

		[Parameter(Mandatory = true, ParameterSetName = ParameterSet_PAYLOAD)]
		public SearchResultTypePayload Payload;

		protected override void ExecuteCmdlet()
		{
			var headers = GetGcsHeaders();

			SearchResultTypePayload payload;

			if (ParameterSetName == ParameterSet_PAYLOAD)
			{
				payload = Payload;

				// Clear server-only fields that shouldn't be sent when creating
				payload.LastModifiedBy = null;

				if (Validate.IsPresent)
				{
					if (payload.ContentSourceId?.ContentSourceApplication == "Connectors")
					{
						var siteId = GetCurrentSiteId();
						ValidateConnectorProperties(siteId, payload.ContentSourceId.SystemId, payload.Rules, payload.DisplayProperties, nameof(Payload));
					}
					else
					{
						ValidateSharePointProperties(payload.Rules, payload.DisplayProperties);
					}
				}
			}
			else
			{
				var rulesList = Rules != null && Rules.Length > 0
					? new List<SearchResultTypeRule>(Rules)
					: new List<SearchResultTypeRule>();

				var ruleProps = new List<string>();
				foreach (var rule in rulesList)
				{
					ruleProps.Add(rule.PropertyName);
				}

				var contentSource = ResolveContentSource(out var contentSourceName);

				var displayProps = DisplayProperties != null && DisplayProperties.Length > 0
					? new List<string>(DisplayProperties)
					: new List<string> { "title", "titleUrl", "modifiedBy", "modifiedTime", "description" };

				if (Validate.IsPresent)
				{
					if (contentSource.ContentSourceApplication == "Connectors")
					{
						var siteId = GetCurrentSiteId();
						ValidateConnectorProperties(siteId, contentSource.SystemId, rulesList, displayProps, nameof(Rules));
					}
					else
					{
						ValidateSharePointProperties(rulesList, displayProps);
					}
				}

				payload = new SearchResultTypePayload
				{
					Name = Name,
					IsActive = true,
					ContentSourceId = contentSource,
					ContentSourceName = contentSourceName,
					Rules = rulesList,
					RuleProperties = ruleProps,
					DisplayTemplate = DisplayTemplate ?? GetDefaultDisplayTemplate(),
					DisplayProperties = displayProps,
					DisplaySampleData = ""
				};
			}

			if (Validate.IsPresent && !string.IsNullOrEmpty(payload.DisplayTemplate))
			{
				ValidateAdaptiveCardVersion(payload.DisplayTemplate, nameof(DisplayTemplate));
			}

			var logicalId = !string.IsNullOrEmpty(Identity) ? Identity : GenerateLogicalId();
			var body = new SearchResultType
			{
				LogicalId = logicalId,
				Payload = payload
			};

			var jsonOptions = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
			};
			var json = JsonSerializer.Serialize(body, jsonOptions);

			LogDebug($"Creating search result type: {json}");

			string url;
			string getUrl;
			if (Scope == SearchVerticalScope.Organization)
			{
				url = GetGcsOrgMrtUrl("/");
				getUrl = GetGcsOrgMrtUrl($"/{logicalId}");
			}
			else
			{
				var siteId = GetCurrentSiteId();
				url = GetGcsMrtUrl(siteId, "/");
				getUrl = GetGcsMrtUrl(siteId, $"/{logicalId}");
			}

			var result = PostAndGet<SearchResultType>(url, getUrl, json, headers);
			WriteObject(result, false);
		}

		private SearchResultTypeContentSource ResolveContentSource(out string contentSourceName)
		{
			if (!ParameterSpecified(nameof(ContentSource)))
			{
				contentSourceName = "SharePoint and OneDrive";
				return new SearchResultTypeContentSource
				{
					ContentSourceApplication = "SharePoint",
					Identity = "SharePoint",
					SystemId = "SharePoint"
				};
			}

			SearchSiteConnection conn;

			// Unwrap PSObject if PowerShell wrapped the value
			var unwrapped = ContentSource is PSObject psObj ? psObj.BaseObject : ContentSource;

			if (unwrapped is SearchSiteConnection siteConnection)
			{
				conn = siteConnection;
			}
			else
			{
				var identity = unwrapped?.ToString();
				if (string.IsNullOrEmpty(identity))
				{
					throw new PSArgumentException("ContentSource identity cannot be empty.", nameof(ContentSource));
				}

				var siteId = GetCurrentSiteId();
				var headers = GetGcsHeaders();
				var url = GetGcsSiteConnectionsUrl(siteId);
				var collection = GetWithRetry<SearchSiteConnectionCollection>(url, headers);
				conn = collection?.Connections?.Find(c => string.Equals(c.Id, identity, StringComparison.OrdinalIgnoreCase));
				if (conn == null)
				{
					throw new PSArgumentException($"Site connection '{identity}' not found.", nameof(ContentSource));
				}
			}

			contentSourceName = conn.Name;
			return new SearchResultTypeContentSource
			{
				ContentSourceApplication = "Connectors",
				Identity = conn.Id,
				SystemId = conn.SystemId
			};
		}

		private static string GetDefaultDisplayTemplate()
		{
			return "{\n   \"type\": \"AdaptiveCard\",\n   \"version\": \"1.3\",\n   \"body\": [\n      {\n         \"type\": \"ColumnSet\",\n         \"columns\": [\n            {\n               \"type\": \"Column\",\n               \"width\": \"auto\",\n               \"items\": [\n                  {\n                     \"type\": \"Image\",\n                     \"url\": \"https://res.cdn.office.net/midgard/versionless/defaultmrticon.png\",\n                     \"horizontalAlignment\": \"Center\",\n                     \"size\": \"Small\"\n                  }\n               ],\n               \"horizontalAlignment\": \"Center\"\n            },\n            {\n               \"type\": \"Column\",\n               \"width\": \"stretch\",\n               \"items\": [\n                  {\n                     \"type\": \"ColumnSet\",\n                     \"columns\": [\n                        {\n                           \"type\": \"Column\",\n                           \"width\": \"auto\",\n                           \"items\": [\n                              {\n                                 \"type\": \"TextBlock\",\n                                 \"text\": \"[${title}](${titleUrl})\",\n                                 \"weight\": \"Bolder\",\n                                 \"size\": \"Medium\",\n                                 \"maxLines\": 3,\n                                 \"color\": \"Accent\"\n                              }\n                           ],\n                           \"spacing\": \"None\"\n                        }\n                     ],\n                     \"spacing\": \"Small\"\n                  },\n                  {\n                     \"type\": \"TextBlock\",\n                     \"text\": \"[${titleUrl}](${titleUrl})\",\n                     \"spacing\": \"Small\",\n                     \"weight\": \"Bolder\",\n                     \"color\": \"Dark\"\n                  },\n                  {\n                     \"type\": \"Container\",\n                     \"items\": [\n                        {\n                           \"type\": \"TextBlock\",\n                           \"text\": \"**${modifiedBy}** modified {{DATE(${modifiedTime})}}\",\n                           \"spacing\": \"Small\",\n                           \"$when\": \"${modifiedBy!='' && modifiedTime!=''}\"\n                        },\n                        {\n                           \"type\": \"TextBlock\",\n                           \"text\": \"Modified on {{DATE(${modifiedTime})}}\",\n                           \"spacing\": \"Small\",\n                           \"$when\": \"${modifiedBy=='' && modifiedTime!=''}\"\n                        },\n                        {\n                           \"type\": \"TextBlock\",\n                           \"text\": \"Modified by __${modifiedBy}__\",\n                           \"spacing\": \"Small\",\n                           \"$when\": \"${modifiedBy!='' && modifiedTime==''}\"\n                        }\n                     ],\n                     \"spacing\": \"Small\"\n                  },\n                  {\n                     \"type\": \"TextBlock\",\n                     \"text\": \"${description}\",\n                     \"maxLines\": 2,\n                     \"wrap\": true,\n                     \"spacing\": \"Small\"\n                  }\n               ],\n               \"spacing\": \"Medium\"\n            }\n         ]\n      }\n   ],\n   \"$schema\": \"http://adaptivecards.io/schemas/adaptive-card.json\"\n}";
		}
	}
}
