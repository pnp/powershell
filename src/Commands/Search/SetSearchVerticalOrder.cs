using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

namespace PnP.PowerShell.Commands.Search
{
	[Cmdlet(VerbsCommon.Set, "PnPSearchVerticalOrder", SupportsShouldProcess = true)]
	[RequiredApiDelegatedPermissions("https://gcs.office.com/ExternalConnection.ReadWrite.All")]
	public class SetSearchVerticalOrder : PnPGcsCmdlet
	{
		private const int MaxVerifyAttempts = 10;
		private const int VerifyDelayMs = 1000;
		private const int PhaseCooldownMs = 10000;
		private const int CreateCooldownMs = 3000;

		[Parameter(Mandatory = true, Position = 0)]
		public string[] Identity;

		[Parameter(Mandatory = false)]
		public SearchVerticalScope Scope = SearchVerticalScope.Site;

		protected override void ExecuteCmdlet()
		{
			var headers = GetGcsHeaders();

			// 1. GET all verticals
			string listUrl;
			if (Scope == SearchVerticalScope.Organization)
			{
				listUrl = GetGcsOrgVerticalUrl("?oob=true");
			}
			else
			{
				var siteId = GetCurrentSiteId();
				listUrl = GetGcsVerticalUrl(siteId, "?oob=true");
			}

			WriteVerbose("Retrieving current verticals to validate order");
			var collection = GetWithRetry<SearchVerticalCollection>(listUrl, headers);
			var allVerticals = collection?.Verticals ?? new List<SearchVertical>();

			// 2. Build lookup of custom verticals
			var customVerticals = allVerticals
				.Where(v => v.Payload?.VerticalType == 1)
				.ToDictionary(v => v.LogicalId, v => v, StringComparer.OrdinalIgnoreCase);

			// 3. Validate all provided IDs are custom verticals
			foreach (var id in Identity)
			{
				if (!customVerticals.ContainsKey(id))
				{
					var builtIn = allVerticals.Any(v => v.LogicalId.Equals(id, StringComparison.OrdinalIgnoreCase) && v.Payload?.VerticalType == 0);
					if (builtIn)
					{
						throw new PSArgumentException($"'{id}' is a built-in vertical and cannot be reordered. Only provide custom vertical IDs.", nameof(Identity));
					}
					throw new PSArgumentException($"Custom vertical '{id}' not found.", nameof(Identity));
				}
			}

			// 4. Check for duplicates
			var duplicates = Identity.GroupBy(id => id, StringComparer.OrdinalIgnoreCase).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
			if (duplicates.Count > 0)
			{
				throw new PSArgumentException($"Duplicate logical IDs: {string.Join(", ", duplicates)}", nameof(Identity));
			}

			// 5. Check all custom verticals are accounted for
			var missing = customVerticals.Keys.Where(k => !Identity.Contains(k, StringComparer.OrdinalIgnoreCase)).ToList();
			if (missing.Count > 0)
			{
				throw new PSArgumentException($"All custom verticals must be included. Missing: {string.Join(", ", missing)}", nameof(Identity));
			}

			// 6. Check if order is actually different (preserve API-returned order)
			var currentOrder = allVerticals
				.Where(v => v.Payload?.VerticalType == 1)
				.Select(v => v.LogicalId)
				.ToList();
			var alreadyInOrder = currentOrder.SequenceEqual(Identity, StringComparer.OrdinalIgnoreCase);
			if (alreadyInOrder)
			{
				WriteWarning("Verticals are already in the requested order.");
				return;
			}

			// 7. Find the longest prefix of the desired order that is already a subsequence
			// of the current order. Those verticals can stay — only the remaining suffix
			// needs to be deleted and recreated at the end.
			// Example: current [A,B,C,D,E], desired [A,B,C,E,D]
			//   prefix subsequence = [A,B,C,E] (length 4), only D needs delete+recreate.
			var keepCount = 0;
			var currentPos = 0;
			for (int i = 0; i < Identity.Length; i++)
			{
				var found = false;
				for (int j = currentPos; j < currentOrder.Count; j++)
				{
					if (currentOrder[j].Equals(Identity[i], StringComparison.OrdinalIgnoreCase))
					{
						currentPos = j + 1;
						keepCount = i + 1;
						found = true;
						break;
					}
				}
				if (!found)
					break;
			}

			// Only the verticals after the kept prefix need to be deleted and recreated
			var verticalsToMove = Identity.Skip(keepCount).Select(id => customVerticals[id]).ToList();
			var skippedCount = keepCount;

			if (!ShouldProcess(
				$"Reorder {verticalsToMove.Count} of {Identity.Length} custom verticals at {Scope} scope",
				$"This will delete and recreate {verticalsToMove.Count} custom verticals (skipping {skippedCount} already in position). Continue?",
				"Set-PnPSearchVerticalOrder"))
			{
				return;
			}

			if (skippedCount > 0)
			{
				WriteVerbose($"Skipping first {skippedCount} vertical(s) already in correct position");
			}

			var jsonOptions = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
			};

			// 8. Delete verticals that need to move and verify each is gone before proceeding
			WriteVerbose($"Phase 1: Deleting {verticalsToMove.Count} custom verticals");
			for (int idx = 0; idx < verticalsToMove.Count; idx++)
			{
				var vertical = verticalsToMove[idx];
				var verticalUrl = GetVerticalUrl($"/{vertical.LogicalId}");

				WriteVerbose($"[{idx + 1}/{verticalsToMove.Count}] Deleting '{vertical.LogicalId}' ({vertical.Payload?.DisplayName})");
				DeleteWithRetry(verticalUrl, headers, verifySuccess: () =>
				{
					// Check if the vertical is actually gone despite the 500 error
					try
					{
						GcsRequestHelper.Get<SearchVertical>(verticalUrl, additionalHeaders: headers);
						return false; // Still exists
					}
					catch (GraphException ex) when (ex.HttpResponse?.StatusCode == HttpStatusCode.NotFound)
					{
						return true; // 404 = Gone
					}
				});

				// Verify the delete fully propagated before moving on
				WaitUntilDeleted(vertical.LogicalId, verticalUrl, headers);
			}
			WriteVerbose("Phase 1 complete: All verticals deleted and verified");

			// Cooldown between phases — give the GCS backend time to fully settle after bulk deletes
			WriteVerbose($"Waiting {PhaseCooldownMs / 1000}s for API to settle before recreating...");
			Thread.Sleep(PhaseCooldownMs);

			// 9. Recreate in the desired order, verifying each one exists before continuing
			WriteVerbose($"Phase 2: Recreating {verticalsToMove.Count} custom verticals in new order");
			var recreated = new List<string>();
			try
			{
				var createUrl = GetVerticalUrl("/");
				for (int idx = 0; idx < verticalsToMove.Count; idx++)
				{
					var vertical = verticalsToMove[idx];

					// Clear server-only fields
					if (vertical.Payload != null)
					{
						vertical.Payload.LastModifiedBy = null;
					}

					var body = new SearchVertical
					{
						LogicalId = vertical.LogicalId,
						Payload = vertical.Payload
					};

					var json = JsonSerializer.Serialize(body, jsonOptions);
					var verticalUrl = GetVerticalUrl($"/{vertical.LogicalId}");

					WriteVerbose($"[{idx + 1}/{verticalsToMove.Count}] Creating '{vertical.LogicalId}' ({vertical.Payload?.DisplayName})");
					PostWithRetry(createUrl, () => new StringContent(json, System.Text.Encoding.UTF8, "application/json"), headers, verifySuccess: () =>
					{
						// Check if the vertical was actually created despite the 500 error
						try
						{
							var result = GcsRequestHelper.Get<SearchVertical>(verticalUrl, additionalHeaders: headers);
							return result != null;
						}
						catch
						{
							return false;
						}
					});

					// Verify the create fully propagated before moving on
					WaitUntilCreated(vertical.LogicalId, verticalUrl, headers);

					recreated.Add(vertical.LogicalId);

					// Small cooldown between consecutive creates
					if (idx < verticalsToMove.Count - 1)
					{
						Thread.Sleep(CreateCooldownMs);
					}
				}
			}
			catch (Exception ex)
			{
				var remaining = verticalsToMove.Skip(recreated.Count).Select(v => v.LogicalId).ToList();
				throw new PSInvalidOperationException(
					$"Error recreating verticals. Successfully recreated: [{string.Join(", ", recreated)}]. " +
					$"Failed to recreate: [{string.Join(", ", remaining)}]. Error: {ex.Message}", ex);
			}

			WriteVerbose($"Phase 2 complete: All {verticalsToMove.Count} verticals recreated and verified");
		}

		/// <summary>
		/// Builds the GCS vertical URL for the current scope
		/// </summary>
		private string GetVerticalUrl(string suffix)
		{
			if (Scope == SearchVerticalScope.Organization)
			{
				return GetGcsOrgVerticalUrl(suffix);
			}
			var siteId = GetCurrentSiteId();
			return GetGcsVerticalUrl(siteId, suffix);
		}

		/// <summary>
		/// Polls until a GET for the vertical returns an error (confirming deletion)
		/// </summary>
		private void WaitUntilDeleted(string logicalId, string url, IDictionary<string, string> headers)
		{
			for (int i = 0; i < MaxVerifyAttempts; i++)
			{
				Thread.Sleep(VerifyDelayMs);
				try
				{
					GcsRequestHelper.Get<SearchVertical>(url, additionalHeaders: headers);
					// Still exists - keep waiting
					WriteVerbose($"Vertical '{logicalId}' still exists, waiting... ({i + 1}/{MaxVerifyAttempts})");
				}
				catch (GraphException ex) when (ex.HttpResponse?.StatusCode == HttpStatusCode.NotFound)
				{
					// 404 means the vertical is gone
					WriteVerbose($"Verified vertical '{logicalId}' deleted");
					return;
				}
				catch (Exception ex)
				{
					// Non-404 error — log but keep retrying
					WriteVerbose($"Unexpected error checking vertical '{logicalId}': {ex.Message}. Retrying... ({i + 1}/{MaxVerifyAttempts})");
				}
			}

			// Proceed anyway after max attempts - the delete API returned success
			WriteWarning($"Could not confirm deletion of '{logicalId}' after {MaxVerifyAttempts} attempts. Proceeding.");
		}

		/// <summary>
		/// Polls until a GET for the vertical succeeds (confirming creation)
		/// </summary>
		private void WaitUntilCreated(string logicalId, string url, IDictionary<string, string> headers)
		{
			for (int i = 0; i < MaxVerifyAttempts; i++)
			{
				Thread.Sleep(VerifyDelayMs);
				try
				{
					var result = GcsRequestHelper.Get<SearchVertical>(url, additionalHeaders: headers);
					if (result != null)
					{
						WriteVerbose($"Verified vertical '{logicalId}' created");
						return;
					}
				}
				catch
				{
					// Not yet available - keep waiting
					WriteVerbose($"Vertical '{logicalId}' not yet available, waiting... ({i + 1}/{MaxVerifyAttempts})");
				}
			}

			// Proceed anyway - the create API returned success
			WriteWarning($"Could not confirm creation of '{logicalId}' after {MaxVerifyAttempts} attempts. Proceeding.");
		}
	}
}
