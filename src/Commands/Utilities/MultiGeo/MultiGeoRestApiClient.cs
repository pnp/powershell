using Microsoft.SharePoint.Client;
using PnP.Framework.Http;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities.MultiGeo
{
	internal class MultiGeoRestApiClient
	{
		private const string TenantRenameApiVersion = "1.5.3";
		private const string TenantRenameCancelApiVersion = "1.5.5";
		private const string TenantRenameStatusV2ApiVersion = "1.5.18";
		private const string TenantRenameJobsPath = "TenantRenameJobs";
		private const string TenantRenameJobsPathToGetWarningMessages = "TenantRenameJobs/GetWarningMessages";
		private const string TenantRenameJobsPathToGetStatus = "TenantRenameJobs/Get";
		private const string TenantRenameJobsPathToGetStatusV2 = "TenantRenameJobs/GetV2";
		private const string TenantRenameJobsPathToCancelAJob = "TenantRenameJobs/Cancel";
		private const string GeoMoveCompatibilityChecksApiVersion = "1.3.6";
		private const string GeoMoveCompatibilityChecksPath = "GeoMoveCompatibilityChecks";
		private const string AllowedDataLocationsApiVersion = "1.3.11";
		private const string AllowedDataLocationsPath = "AllowedDataLocations";
		private const string UserMoveJobsApiVersion = "1.0";
		private const string UserMoveJobsByMoveIdApiVersion = "1.2.2";
		private const string UserMoveJobsReportApiVersion = "1.3.2";
		private const string UserMoveJobPathByUpn = "UserMoveJobs(upn='{0}')";
		private const string UserMoveJobPathByMoveId = "UserMoveJobs/GetByMoveId(odbMoveId='{0}')";
		private const string UserMoveJobsPathForMoveReport = "UserMoveJobs/GetMoveReport(moveState={0},moveDirection={1},startTime='{2:u}',endTime='{3:u}',limit='{4}')";
		private const int MaximumPagination = 10;
		private static readonly TimeSpan CreateTenantRenameJobTimeout = TimeSpan.FromSeconds(300);
		private static readonly JsonSerializerOptions SerializerOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};

		private readonly ClientContext adminContext;
		private readonly HttpClient httpClient;

		internal MultiGeoRestApiClient(ClientContext adminContext)
		{
			this.adminContext = adminContext ?? throw new ArgumentNullException(nameof(adminContext));
			httpClient = PnPHttpClient.Instance.GetHttpClient(adminContext);
		}

		internal TenantRenameJob CreateTenantRenameJob(TenantRenameJobEntityData job)
		{
			return Post<TenantRenameJob>(TenantRenameJobsPath, job, CreateTenantRenameJobTimeout);
		}

		internal TenantRenameJob GetTenantRenameJob()
		{
			return Get<TenantRenameJob>(TenantRenameJobsPathToGetStatus);
		}

		internal TenantRenameJob GetTenantRenameJobV2()
		{
			return Get<TenantRenameJob>(TenantRenameJobsPathToGetStatusV2, TenantRenameStatusV2ApiVersion);
		}

		internal IEnumerable<string> GetTenantRenameWarningMessages()
		{
			return GetFeed<string>(TenantRenameJobsPathToGetWarningMessages, TenantRenameApiVersion);
		}

		internal IEnumerable<GeoMoveTenantCompatibilityCheck> GetGeoMoveCompatibilityChecks()
		{
			return Get<GeoMoveCompatibilityChecks>(GeoMoveCompatibilityChecksPath, GeoMoveCompatibilityChecksApiVersion)?.GeoMoveTenantCompatibilityChecks ?? Array.Empty<GeoMoveTenantCompatibilityCheck>();
		}

		internal IEnumerable<MultiGeoCompanyAllowedDataLocation> GetAllowedDataLocations()
		{
			return GetFeed<MultiGeoCompanyAllowedDataLocation>(AllowedDataLocationsPath, AllowedDataLocationsApiVersion);
		}

		internal UserAndContentMoveState GetUserAndContentMoveState(string userPrincipalName)
		{
			var path = string.Format(CultureInfo.InvariantCulture, UserMoveJobPathByUpn, ProcessSpecialChars(userPrincipalName));
			return Get<UserAndContentMoveState>(path, UserMoveJobsApiVersion);
		}

		internal UserAndContentMoveState GetUserAndContentMoveState(Guid odbMoveId)
		{
			var path = string.Format(CultureInfo.InvariantCulture, UserMoveJobPathByMoveId, odbMoveId);
			return Get<UserAndContentMoveState>(path, UserMoveJobsByMoveIdApiVersion);
		}

		internal IEnumerable<UserAndContentMoveState> GetUserAndContentMoveStates(MoveState moveState, MoveDirection moveDirection, DateTime startTime, DateTime endTime, uint limit)
		{
			var path = string.Format(CultureInfo.InvariantCulture, UserMoveJobsPathForMoveReport, (int)moveState, (int)moveDirection, startTime, endTime, limit);
			return GetFeed<UserAndContentMoveState>(path, UserMoveJobsReportApiVersion);
		}

		internal void CancelTenantRenameJob()
		{
			Post<string>(TenantRenameJobsPathToCancelAJob, payload: null, apiVersion: TenantRenameCancelApiVersion);
		}

		private T Get<T>(string path, string apiVersion = TenantRenameApiVersion)
		{
			var responseText = Send(() => CreateRequest(HttpMethod.Get, path, apiVersion), timeout: null, allowRetries: true);
			return DeserializeResponse<T>(responseText);
		}

		private IEnumerable<T> GetFeed<T>(string path, string apiVersion)
		{
			var results = new List<T>();
			var requestUri = CreateApiUri(path, apiVersion);
			var pages = 0;

			while (requestUri != null && pages < MaximumPagination)
			{
				var responseText = Send(() => CreateRequest(HttpMethod.Get, requestUri), timeout: null, allowRetries: true);
				var collection = DeserializeFeed<T>(responseText);
				if (collection.Value != null)
				{
					results.AddRange(collection.Value);
				}

				if (!string.IsNullOrWhiteSpace(collection.NextLink))
				{
					requestUri = new Uri(requestUri, collection.NextLink);
					checked
					{
						pages++;
					}
				}
				else
				{
					requestUri = null;
				}
			}

			if (requestUri != null)
			{
				throw new InvalidOperationException("SharePoint Online REST request returned too many pages.");
			}

			return results;
		}

		private T Post<T>(string path, object payload, TimeSpan? timeout = null, string apiVersion = TenantRenameApiVersion)
		{
			var jsonPayload = payload == null ? null : JsonSerializer.Serialize(payload, SerializerOptions);
			var responseText = Send(() => CreateRequest(HttpMethod.Post, path, apiVersion, jsonPayload), timeout, allowRetries: false);
			return DeserializeResponse<T>(responseText);
		}

		private HttpRequestMessage CreateRequest(HttpMethod method, string path, string apiVersion, string jsonPayload = null)
		{
			return CreateRequest(method, CreateApiUri(path, apiVersion), jsonPayload);
		}

		private HttpRequestMessage CreateRequest(HttpMethod method, Uri requestUri, string jsonPayload = null)
		{
			var request = new HttpRequestMessage(method, requestUri)
			{
				Version = new Version(2, 0)
			};
			request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json;odata=nometadata"));
			PnPHttpClient.AuthenticateRequestAsync(request, adminContext).GetAwaiter().GetResult();

			if (method == HttpMethod.Post)
			{
				request.Headers.TryAddWithoutValidation("X-RequestDigest", adminContext.GetRequestDigestAsync().GetAwaiter().GetResult());
				if (jsonPayload != null)
				{
					request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
				}
			}

			return request;
		}

		private Uri CreateApiUri(string path, string apiVersion)
		{
			var normalizedPath = path.TrimStart('/');
			var separator = normalizedPath.Contains('?') ? "&" : "?";
			return new Uri($"{adminContext.Url.TrimEnd('/')}/_api/{normalizedPath}{separator}api-version={apiVersion}");
		}

		private string Send(Func<HttpRequestMessage> requestFactory, TimeSpan? timeout, bool allowRetries)
		{
			var retryAttempt = 0;
			while (true)
			{
				using var request = requestFactory();
				using var cancellationTokenSource = timeout.HasValue ? new CancellationTokenSource(timeout.Value) : null;
				using var response = httpClient.SendAsync(request, cancellationTokenSource?.Token ?? CancellationToken.None).GetAwaiter().GetResult();
				var responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				if (allowRetries && IsTransient(response.StatusCode) && retryAttempt < 10)
				{
					retryAttempt++;
					Task.Delay(GetRetryDelay(response, retryAttempt)).GetAwaiter().GetResult();
					continue;
				}

				if (!response.IsSuccessStatusCode)
				{
					throw new InvalidOperationException(GetErrorMessage(response.StatusCode, responseText));
				}

				return responseText;
			}
		}

		private static bool IsTransient(HttpStatusCode statusCode)
		{
			return statusCode == (HttpStatusCode)429 || statusCode == HttpStatusCode.ServiceUnavailable || statusCode == HttpStatusCode.GatewayTimeout;
		}

		private static TimeSpan GetRetryDelay(HttpResponseMessage response, int retryAttempt)
		{
			if (response.Headers.RetryAfter?.Delta != null)
			{
				return response.Headers.RetryAfter.Delta.Value;
			}

			if (response.Headers.RetryAfter?.Date != null)
			{
				var retryAfter = response.Headers.RetryAfter.Date.Value - DateTimeOffset.UtcNow;
				if (retryAfter > TimeSpan.Zero)
				{
					return retryAfter;
				}
			}

			return TimeSpan.FromSeconds(Math.Min(Math.Pow(2, retryAttempt), 30));
		}

		private static T DeserializeResponse<T>(string responseText)
		{
			if (string.IsNullOrWhiteSpace(responseText))
			{
				return default;
			}

			if (typeof(T) == typeof(string))
			{
				return (T)(object)responseText;
			}

			using var jsonDocument = JsonDocument.Parse(responseText);
			var responseElement = UnwrapODataResponse(jsonDocument.RootElement);
			return JsonSerializer.Deserialize<T>(responseElement.GetRawText(), SerializerOptions);
		}

		private static ODataFeed<T> DeserializeFeed<T>(string responseText)
		{
			if (string.IsNullOrWhiteSpace(responseText))
			{
				return new ODataFeed<T>();
			}

			using var jsonDocument = JsonDocument.Parse(responseText);
			var responseElement = jsonDocument.RootElement;
			if (responseElement.ValueKind == JsonValueKind.Object && responseElement.TryGetProperty("d", out var dElement))
			{
				responseElement = dElement;
			}

			var feed = new ODataFeed<T>();
			if (responseElement.ValueKind == JsonValueKind.Object)
			{
				if (responseElement.TryGetProperty("value", out var valueElement) || responseElement.TryGetProperty("results", out valueElement))
				{
					feed.Value = DeserializeFeedValue<T>(valueElement);
				}

				feed.NextLink = GetStringProperty(responseElement, "@odata.nextLink", "odata.nextLink", "nextLink", "__next");
				return feed;
			}

			feed.Value = DeserializeFeedValue<T>(responseElement);
			return feed;
		}

		private static T[] DeserializeFeedValue<T>(JsonElement valueElement)
		{
			if (valueElement.ValueKind != JsonValueKind.Array)
			{
				return Array.Empty<T>();
			}

			return JsonSerializer.Deserialize<T[]>(valueElement.GetRawText(), SerializerOptions) ?? Array.Empty<T>();
		}

		private static string GetStringProperty(JsonElement element, params string[] propertyNames)
		{
			foreach (var propertyName in propertyNames)
			{
				if (element.TryGetProperty(propertyName, out var propertyElement) && propertyElement.ValueKind == JsonValueKind.String)
				{
					return propertyElement.GetString();
				}
			}

			return null;
		}

		private static string ProcessSpecialChars(string value)
		{
			return WebUtility.UrlEncode(value.Replace("/", "#", StringComparison.Ordinal).Replace("'", "|", StringComparison.Ordinal))?.Replace("+", "%20", StringComparison.Ordinal);
		}

		private static JsonElement UnwrapODataResponse(JsonElement responseElement)
		{
			if (responseElement.ValueKind != JsonValueKind.Object)
			{
				return responseElement;
			}

			if (responseElement.TryGetProperty("d", out var dElement))
			{
				return dElement.TryGetProperty("results", out var resultsElement) ? resultsElement : dElement;
			}

			return responseElement.TryGetProperty("value", out var valueElement) ? valueElement : responseElement;
		}

		private static string GetErrorMessage(HttpStatusCode statusCode, string responseText)
		{
			var statusMessage = $"SharePoint Online REST request failed with status {(int)statusCode} ({statusCode}).";
			if (string.IsNullOrWhiteSpace(responseText))
			{
				return statusMessage;
			}

			try
			{
				using var jsonDocument = JsonDocument.Parse(responseText);
				var rootElement = jsonDocument.RootElement;
				if (TryGetODataErrorMessage(rootElement, out var errorMessage))
				{
					return $"{statusMessage} {errorMessage}";
				}
			}
			catch (JsonException)
			{
			}

			return $"{statusMessage} {responseText}";
		}

		private static bool TryGetODataErrorMessage(JsonElement rootElement, out string errorMessage)
		{
			errorMessage = null;
			if (!rootElement.TryGetProperty("error", out var errorElement) && !rootElement.TryGetProperty("odata.error", out errorElement))
			{
				return false;
			}

			if (errorElement.TryGetProperty("message", out var messageElement))
			{
				if (messageElement.ValueKind == JsonValueKind.String)
				{
					errorMessage = messageElement.GetString();
					return !string.IsNullOrWhiteSpace(errorMessage);
				}

				if (messageElement.ValueKind == JsonValueKind.Object && messageElement.TryGetProperty("value", out var valueElement))
				{
					errorMessage = valueElement.GetString();
					return !string.IsNullOrWhiteSpace(errorMessage);
				}
			}

			if (errorElement.TryGetProperty("code", out var codeElement))
			{
				errorMessage = codeElement.GetString();
				return !string.IsNullOrWhiteSpace(errorMessage);
			}

			return false;
		}

		private sealed class ODataFeed<T>
		{
			public T[] Value { get; set; }

			public string NextLink { get; set; }
		}
	}
}
