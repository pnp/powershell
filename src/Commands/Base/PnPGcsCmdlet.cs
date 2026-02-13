using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace PnP.PowerShell.Commands.Base
{
	/// <summary>
	/// Base class for cmdlets that interact with the Graph Connector Service (GCS) API for Microsoft Search administration
	/// </summary>
	public abstract class PnPGcsCmdlet : PnPConnectedCmdlet
	{
		private const string GcsBaseUrl = "https://gcs.office.com";

		/// <summary>
		/// Static cache of site IDs keyed by connection URL, persists across cmdlet invocations within the same session.
		/// </summary>
		private static readonly Dictionary<string, Guid> _siteIdCache = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);

		/// <summary>
		/// Reference to the SharePoint context on the current connection. If NULL it means there is no SharePoint context available on the current connection.
		/// </summary>
		public ClientContext ClientContext => Connection?.Context;

		public PnPContext PnPContext => Connection?.PnPContext;

		public ApiRequestHelper GcsRequestHelper { get; private set; }

		protected override void BeginProcessing()
		{
			base.BeginProcessing();

			if (Connection?.Context != null)
			{
				var contextSettings = Connection.Context.GetContextSettings();
				if (contextSettings?.Type == Framework.Utilities.Context.ClientContextType.Cookie || contextSettings?.Type == Framework.Utilities.Context.ClientContextType.SharePointACSAppOnly)
				{
					var typeString = contextSettings?.Type == Framework.Utilities.Context.ClientContextType.Cookie ? "WebLogin/Cookie" : "ACS";
					throw new PSInvalidOperationException($"This cmdlet does not work with a {typeString} based connection towards SharePoint.");
				}
			}
			GcsRequestHelper = new ApiRequestHelper(this.GetType(), Connection, $"{GcsBaseUrl}/.default");
		}

		/// <summary>
		/// Returns an Access Token for the GCS API
		/// </summary>
		public string AccessToken => TokenHandler.GetAccessToken($"{GcsBaseUrl}/.default", Connection);

		/// <summary>
		/// Returns the site ID (GUID) for the currently connected site.
		/// Loaded once via CSOM EnsureProperties and cached per connection URL for the session.
		/// </summary>
		protected Guid GetCurrentSiteId()
		{
			var url = Connection?.Url;
			if (url != null && _siteIdCache.TryGetValue(url, out var cached))
				return cached;

			if (ClientContext == null)
				throw new PSInvalidOperationException("No SharePoint context available on the current connection.");

			ClientContext.Site.EnsureProperties(s => s.Id);
			var siteId = ClientContext.Site.Id;
			if (url != null)
				_siteIdCache[url] = siteId;
			return siteId;
		}

		/// <summary>
		/// Returns the standard headers required by GCS API calls, including x-siteurl
		/// </summary>
		protected IDictionary<string, string> GetGcsHeaders()
		{
			return new Dictionary<string, string>
			{
				{ "x-siteurl", Connection.Url }
			};
		}

		/// <summary>
		/// Builds a GCS API URL for the given path relative to the site's verticals endpoint
		/// </summary>
		protected string GetGcsVerticalUrl(Guid siteId, string suffix = null)
		{
			var url = $"{GcsBaseUrl}/v1.0/admin/sites/{siteId}/verticals";
			if (!string.IsNullOrEmpty(suffix))
			{
				url += suffix;
			}
			return url;
		}

		/// <summary>
		/// Builds a GCS API URL for the site connections endpoint
		/// </summary>
		protected string GetGcsSiteConnectionsUrl(Guid siteId)
		{
			return $"{GcsBaseUrl}/v1.0/admin/sites/{siteId}/siteconnections";
		}

		/// <summary>
		/// Builds a GCS API URL for organization-scoped verticals
		/// </summary>
		protected string GetGcsOrgVerticalUrl(string suffix = null)
		{
			var url = $"{GcsBaseUrl}/v1.0/admin/verticals";
			if (!string.IsNullOrEmpty(suffix))
			{
				url += suffix;
			}
			return url;
		}

		/// <summary>
		/// Builds a GCS API URL for site-scoped result types (mrts)
		/// </summary>
		protected string GetGcsMrtUrl(Guid siteId, string suffix = null)
		{
			var url = $"{GcsBaseUrl}/v1.0/admin/sites/{siteId}/mrts";
			if (!string.IsNullOrEmpty(suffix))
			{
				url += suffix;
			}
			return url;
		}

		/// <summary>
		/// Builds a GCS API URL for organization-scoped result types (mrts)
		/// </summary>
		protected string GetGcsOrgMrtUrl(string suffix = null)
		{
			var url = $"{GcsBaseUrl}/v1.0/admin/mrts";
			if (!string.IsNullOrEmpty(suffix))
			{
				url += suffix;
			}
			return url;
		}

		/// <summary>
		/// Builds a GCS API URL for site connection properties
		/// </summary>
		protected string GetGcsSiteConnectionPropertiesUrl(Guid siteId, string connectionId)
		{
			return $"{GcsBaseUrl}/v1.0/admin/sites/{siteId}/siteconnections/{connectionId}/properties";
		}

		/// <summary>
		/// Resolves a result type identity (logical ID or name) to a SearchResultType object.
		/// First tries a direct GET by logical ID; on 404, falls back to listing all result types and matching by name.
		/// </summary>
		protected SearchResultType ResolveResultType(string identity, SearchVerticalScope scope, Guid? siteId, IDictionary<string, string> headers)
		{
			// Try direct lookup by logical ID
			string getUrl;
			if (scope == SearchVerticalScope.Organization)
				getUrl = GetGcsOrgMrtUrl($"/{identity}");
			else
				getUrl = GetGcsMrtUrl(siteId.Value, $"/{identity}");

			try
			{
				return GetWithRetry<SearchResultType>(getUrl, headers);
			}
			catch (GraphException ex) when (ex.HttpResponse?.StatusCode == HttpStatusCode.NotFound) { }

			// Fall back to name-based search
			string listUrl;
			if (scope == SearchVerticalScope.Organization)
				listUrl = GetGcsOrgMrtUrl(null);
			else
				listUrl = GetGcsMrtUrl(siteId.Value, null);

			var collection = GetWithRetry<SearchResultTypeCollection>(listUrl, headers);
			if (collection?.ResultTypes != null)
			{
				var match = collection.ResultTypes.Find(rt =>
					string.Equals(rt.Payload?.Name, identity, StringComparison.OrdinalIgnoreCase));
				if (match != null) return match;
			}

			throw new PSArgumentException($"Search result type '{identity}' not found.");
		}

		#region Shared utilities

		/// <summary>
		/// Generates a logical ID in the format {timestamp}_{randomId}
		/// </summary>
		protected static string GenerateLogicalId()
		{
			var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			var random = GenerateRandomId(9);
			return $"{timestamp}_{random}";
		}

		private static string GenerateRandomId(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			var result = new char[length];
			for (int i = 0; i < length; i++)
			{
				result[i] = chars[random.Next(chars.Length)];
			}
			return new string(result);
		}

		/// <summary>
		/// Normalizes a query template by ensuring it starts with {searchTerms}
		/// </summary>
		protected static string NormalizeQueryTemplate(string queryTemplate)
		{
			if (string.IsNullOrEmpty(queryTemplate))
				return "";

			if (!queryTemplate.StartsWith("{searchTerms}", StringComparison.OrdinalIgnoreCase))
				queryTemplate = "{searchTerms} " + queryTemplate;

			return queryTemplate;
		}

		/// <summary>
		/// POSTs a resource and returns the created object. If the POST response body is empty,
		/// falls back to a GET by URL to retrieve the created resource.
		/// </summary>
		protected T PostAndGet<T>(string postUrl, string getUrl, string json, IDictionary<string, string> headers)
		{
			var response = PostWithRetry(postUrl, () => new StringContent(json, System.Text.Encoding.UTF8, "application/json"), headers, verifySuccess: () =>
			{
				try
				{
					var check = GcsRequestHelper.Get<T>(getUrl, additionalHeaders: headers);
					return check != null;
				}
				catch
				{
					return false;
				}
			});

			var responseContent = response?.Content?.ReadAsStringAsync().GetAwaiter().GetResult();

			if (!string.IsNullOrEmpty(responseContent))
			{
				return JsonSerializer.Deserialize<T>(responseContent);
			}
			else
			{
				return GetWithRetry<T>(getUrl, headers);
			}
		}

		#endregion

		#region Validation helpers

		protected static readonly HashSet<string> StandardSearchProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"title", "titleUrl", "modifiedBy", "modifiedTime", "description"
		};

		/// <summary>
		/// Validates connector property names in rules and display properties against the connector's schema.
		/// Rule properties throw an error (they would never match). Display properties produce a warning.
		/// </summary>
		protected void ValidateConnectorProperties(Guid siteId, string systemId, List<SearchResultTypeRule> rules, List<string> displayProperties, string rulesParamName)
		{
			var headers = GetGcsHeaders();
			var url = GetGcsSiteConnectionPropertiesUrl(siteId, systemId);

			LogDebug($"Fetching connector properties from: {url}");
			var schema = GetWithRetry<SearchSiteConnectionProperties>(url, headers);
			var validNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (schema?.Properties != null)
			{
				foreach (var prop in schema.Properties)
				{
					validNames.Add(prop.Name);
				}
			}

			var available = string.Join(", ", validNames);

			if (rules != null)
			{
				foreach (var rule in rules)
				{
					if (!string.IsNullOrEmpty(rule.PropertyName) && !validNames.Contains(rule.PropertyName))
					{
						throw new PSArgumentException(
							$"Rule property '{rule.PropertyName}' does not exist in the connector schema. Available properties: {available}",
							rulesParamName);
					}
				}
			}

			if (displayProperties != null)
			{
				foreach (var prop in displayProperties)
				{
					if (!StandardSearchProperties.Contains(prop) && !validNames.Contains(prop))
					{
						WriteWarning($"Display property '{prop}' does not exist in the connector schema. Available connector properties: {available}");
					}
				}
			}
		}

		/// <summary>
		/// Validates SharePoint property names in rules and display properties against known managed properties.
		/// Produces warnings only (not errors) since the known list is not exhaustive.
		/// </summary>
		protected void ValidateSharePointProperties(List<SearchResultTypeRule> rules, List<string> displayProperties)
		{
			if (rules != null)
			{
				foreach (var rule in rules)
				{
					if (!string.IsNullOrEmpty(rule.PropertyName) && !KnownSharePointManagedProperties.IsKnown(rule.PropertyName))
					{
						WriteWarning($"Rule property '{rule.PropertyName}' is not a known SharePoint managed property. It may be a custom property, or it may be misspelled.");
					}
				}
			}

			if (displayProperties != null)
			{
				foreach (var prop in displayProperties)
				{
					if (!StandardSearchProperties.Contains(prop) && !KnownSharePointManagedProperties.IsKnown(prop))
					{
						WriteWarning($"Display property '{prop}' is not a known SharePoint managed property. It may be a custom property, or it may be misspelled.");
					}
				}
			}
		}

		/// <summary>
		/// Validates that a display template is valid JSON with Adaptive Card version 1.3.
		/// Throws on invalid JSON. Warns on wrong version or missing version property.
		/// </summary>
		protected void ValidateAdaptiveCardVersion(string displayTemplate, string paramName)
		{
			if (string.IsNullOrEmpty(displayTemplate)) return;

			try
			{
				using var doc = JsonDocument.Parse(displayTemplate);
				var root = doc.RootElement;

				if (root.TryGetProperty("version", out var versionElement))
				{
					var version = versionElement.GetString();
					if (version != "1.3")
					{
						WriteWarning($"Adaptive Card version '{version}' detected. Microsoft Search result types require version 1.3.");
					}
				}
				else
				{
					WriteWarning("Adaptive Card template is missing the 'version' property. Microsoft Search result types require version 1.3.");
				}
			}
			catch (JsonException ex)
			{
				throw new PSArgumentException($"DisplayTemplate is not valid JSON: {ex.Message}", paramName);
			}
		}

		#endregion

		#region Retry logic

		private const int DefaultMaxRetries = 5;
		private const int DefaultRetryInitialDelayMs = 10000;

		/// <summary>
		/// Checks if an exception from the GCS API is a transient server error worth retrying.
		/// Only retries on InternalServerError (500) and ServiceUnavailable (503).
		/// Handles both GraphException (thrown by GET/SendMessage) and PSInvalidOperationException (thrown by POST/PUT/GetResponseMessage).
		/// </summary>
		private static bool IsTransientError(Exception ex)
		{
			// GraphException carries the HTTP response directly — check status code
			if (ex is GraphException graphEx && graphEx.HttpResponse != null)
			{
				var statusCode = graphEx.HttpResponse.StatusCode;
				return statusCode == HttpStatusCode.InternalServerError || statusCode == HttpStatusCode.ServiceUnavailable;
			}

			// PSInvalidOperationException from GetResponseMessage — check message string
			var message = ex.Message;
			return message.Contains("InternalServerError") || message.Contains("Internal Server Error")
				|| message.Contains("(500)")
				|| message.Contains("ServiceUnavailable") || message.Contains("Service Unavailable")
				|| message.Contains("(503)");
		}

		/// <summary>
		/// GETs a resource from a GCS API URL with retry on transient server errors (500/503).
		/// Retries with exponential backoff (10s, 20s, 40s, 80s). Client errors (400, 404, etc.) are not retried.
		/// </summary>
		protected T GetWithRetry<T>(string url, IDictionary<string, string> headers)
		{
			return ExecuteWithRetry<T>("GET", () => GcsRequestHelper.Get<T>(url, additionalHeaders: headers));
		}

		/// <summary>
		/// Posts content to a GCS API URL with retry on transient server errors (500/503).
		/// Retries with exponential backoff (10s, 20s, 40s, 80s). Client errors (400, 404, etc.) are not retried.
		/// </summary>
		protected HttpResponseMessage PostWithRetry(string url, Func<HttpContent> contentFactory, IDictionary<string, string> headers, Func<bool> verifySuccess = null)
		{
			return ExecuteWithRetry<HttpResponseMessage>("POST", () => GcsRequestHelper.PostHttpContent(url, contentFactory(), headers), verifySuccess);
		}

		/// <summary>
		/// Puts content to a GCS API URL with retry on transient server errors (500/503).
		/// Retries with exponential backoff (10s, 20s, 40s, 80s). Client errors (400, 404, etc.) are not retried.
		/// An optional verifySuccess callback can be provided — when the API returns a transient error,
		/// the callback is invoked first; if it returns true the operation is treated as successful
		/// (the GCS API sometimes returns 500 while actually completing the operation).
		/// </summary>
		protected HttpResponseMessage PutWithRetry(string url, Func<HttpContent> contentFactory, IDictionary<string, string> headers, Func<bool> verifySuccess = null)
		{
			return ExecuteWithRetry<HttpResponseMessage>("PUT", () => GcsRequestHelper.PutHttpContent(url, contentFactory(), headers), verifySuccess);
		}

		/// <summary>
		/// Deletes a resource at a GCS API URL with retry on transient server errors (500/503).
		/// Retries with exponential backoff (10s, 20s, 40s, 80s). Client errors (400, 404, etc.) are not retried.
		/// An optional verifySuccess callback can be provided — when the API returns a transient error,
		/// the callback is invoked first; if it returns true the operation is treated as successful.
		/// </summary>
		protected void DeleteWithRetry(string url, IDictionary<string, string> headers, Func<bool> verifySuccess = null)
		{
			ExecuteWithRetry<HttpResponseMessage>("DELETE", () => GcsRequestHelper.Delete(url, headers), verifySuccess);
		}

		/// <summary>
		/// Executes an API call with retry logic for transient server errors (500/503).
		/// Uses exponential backoff: 10s, 20s, 40s, 80s (total ~150s).
		/// When verifySuccess is provided, it is called after a transient error — if it returns true,
		/// the operation is treated as successful without retrying (the GCS API sometimes returns 500
		/// while actually completing the operation).
		/// </summary>
		private T ExecuteWithRetry<T>(string method, Func<T> action, Func<bool> verifySuccess = null)
		{
			for (int attempt = 1; attempt <= DefaultMaxRetries; attempt++)
			{
				try
				{
					var response = action();
					if (attempt > 1)
					{
						WriteVerbose($"{method} succeeded on attempt {attempt}");
					}
					return response;
				}
				catch (Exception ex) when (attempt < DefaultMaxRetries && IsTransientError(ex))
				{
					var errorMessage = ex is GraphException gex ? (gex.Error?.Message ?? ex.Message) : ex.Message;
					WriteVerbose($"{method} failed (attempt {attempt}/{DefaultMaxRetries}): {errorMessage}");

					// Check if the operation actually succeeded despite the error
					if (verifySuccess != null)
					{
						WriteVerbose("Verifying if operation succeeded despite the error...");
						try
						{
							if (verifySuccess())
							{
								WriteVerbose($"{method} verified as successful on attempt {attempt}");
								return default;
							}
						}
						catch
						{
							// Verification failed — proceed with retry
						}
					}

					var delay = DefaultRetryInitialDelayMs * (int)Math.Pow(2, attempt - 1);
					WriteVerbose($"Retrying in {delay / 1000}s...");
					Thread.Sleep(delay);
				}
			}

			// This line is not reached - the last attempt throws without the when filter matching
			return default;
		}

		#endregion
	}
}
