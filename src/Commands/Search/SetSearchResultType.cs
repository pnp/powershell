using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.Set, "PnPSearchResultType", DefaultParameterSetName = ParameterSet_PROPERTIES)]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	public class SetSearchResultType : PnPGcsCmdlet
	{
		private const string ParameterSet_PROPERTIES = "Properties";
		private const string ParameterSet_PAYLOAD = "Payload";

		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Alias("LogicalId")]
		public string Identity;

		[Parameter(Mandatory = false)]
		public SearchVerticalScope Scope = SearchVerticalScope.Site;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
		public string Name;

		[Parameter(Mandatory = false, ParameterSetName = ParameterSet_PROPERTIES)]
		public int? Priority;

		[Parameter(Mandatory = true, ParameterSetName = ParameterSet_PAYLOAD)]
		public SearchResultTypePayload Payload;

		[Parameter(Mandatory = false)]
		public SwitchParameter Validate;

		protected override void ExecuteCmdlet()
		{
			var headers = GetGcsHeaders();
			var siteId = Scope != SearchVerticalScope.Organization ? (Guid?)GetCurrentSiteId() : null;

			var resolved = ResolveResultType(Identity, Scope, siteId, headers);
			var logicalId = resolved.LogicalId;

			string url;
			if (Scope == SearchVerticalScope.Organization)
				url = GetGcsOrgMrtUrl($"/{logicalId}");
			else
				url = GetGcsMrtUrl(siteId.Value, $"/{logicalId}");

			SearchResultTypePayload payload;

			if (ParameterSetName == ParameterSet_PAYLOAD)
			{
				payload = Payload;
			}
			else
			{
				if (resolved.Payload == null)
				{
					throw new PSArgumentException($"Search result type '{Identity}' has no payload.", nameof(Identity));
				}

				payload = resolved.Payload;

				if (ParameterSpecified(nameof(Name)))
					payload.Name = Name;

				payload.LastModifiedBy = null;
			}

			if (Validate.IsPresent)
			{
				if (payload.ContentSourceId?.ContentSourceApplication == "Connectors")
				{
					var validationSiteId = siteId ?? GetCurrentSiteId();
					ValidateConnectorProperties(validationSiteId, payload.ContentSourceId.SystemId, payload.Rules, payload.DisplayProperties, nameof(Payload));
				}
				else
				{
					ValidateSharePointProperties(payload.Rules, payload.DisplayProperties);
				}

				if (!string.IsNullOrEmpty(payload.DisplayTemplate))
				{
					ValidateAdaptiveCardVersion(payload.DisplayTemplate, nameof(Payload));
				}
			}

			// Handle priority resequencing — shift other result types to make room
			if (ParameterSpecified(nameof(Priority)))
			{
				var desiredPriority = Math.Max(1, Priority.Value);
				ResequenceResultTypePriorities(logicalId, desiredPriority, siteId, headers);
				payload.Priority = desiredPriority;
			}

			var jsonOptions = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
			};
			var json = JsonSerializer.Serialize(payload, jsonOptions);

			LogDebug($"Updating search result type '{logicalId}': {json}");

			PutWithRetry(url, () => new StringContent(json, System.Text.Encoding.UTF8, "application/json"), headers, verifySuccess: () =>
			{
				try
				{
					var updated = GetWithRetry<SearchResultType>(url, headers);
					return updated != null;
				}
				catch
				{
					return false;
				}
			});
		}

		/// <summary>
		/// Resequences result type priorities when one is moved to a new position.
		/// Fetches all result types, removes the target, inserts at the desired position,
		/// renumbers 1..N, and PUTs any that changed.
		/// </summary>
		private void ResequenceResultTypePriorities(string targetLogicalId, int desiredPriority, Guid? siteId, IDictionary<string, string> headers)
		{
			string listUrl = Scope == SearchVerticalScope.Organization
				? GetGcsOrgMrtUrl(null)
				: GetGcsMrtUrl(siteId.Value, null);

			var collection = GetWithRetry<SearchResultTypeCollection>(listUrl, headers);
			var allRts = collection?.ResultTypes ?? new List<SearchResultType>();

			// Sort others by current priority, excluding the target
			var others = allRts
				.Where(rt => !string.Equals(rt.LogicalId, targetLogicalId, StringComparison.OrdinalIgnoreCase))
				.OrderBy(rt => rt.Payload?.Priority ?? 0)
				.ToList();

			// Insert a placeholder for the target at the desired position
			var insertIdx = Math.Max(0, Math.Min(desiredPriority - 1, others.Count));
			others.Insert(insertIdx, null);

			var jsonOptions = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
			};

			// Renumber and PUT any others whose priority changed
			for (int i = 0; i < others.Count; i++)
			{
				var rt = others[i];
				if (rt == null) continue; // placeholder for target

				var newPriority = i + 1;
				if (rt.Payload != null && rt.Payload.Priority != newPriority)
				{
					rt.Payload.Priority = newPriority;
					rt.Payload.LastModifiedBy = null;

					var rtUrl = Scope == SearchVerticalScope.Organization
						? GetGcsOrgMrtUrl($"/{rt.LogicalId}")
						: GetGcsMrtUrl(siteId.Value, $"/{rt.LogicalId}");
					var rtJson = JsonSerializer.Serialize(rt.Payload, jsonOptions);

					WriteVerbose($"Updating priority of '{rt.Payload.Name}' to {newPriority}");
					PutWithRetry(rtUrl, () => new StringContent(rtJson, System.Text.Encoding.UTF8, "application/json"), headers, verifySuccess: () =>
					{
						try
						{
							var check = GetWithRetry<SearchResultType>(rtUrl, headers);
							return check != null;
						}
						catch
						{
							return false;
						}
					});
				}
			}
		}

	}
}
