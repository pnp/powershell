using Microsoft.SharePoint.Client;
using PnP.Framework.Http;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CommandResources = PnP.PowerShell.Commands.Properties.Resources;

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
		private const string GeoMoveCompatibilityChecksMinimumApiVersion = "1.3.6";
		private const string GeoMoveCompatibilityChecksPath = "GeoMoveCompatibilityChecks";
		private const string GeoExperienceMinimumApiVersion = "1.3.7";
		private const string UpdateGeoExperienceModePath = "GeoExperience/UpgradeToSPOMode";
		private const string UpdateAllInstancesExperienceModePath = "GeoExperience/UpgradeAllInstancesToSPOMode";
		private const string AllowedDataLocationsApiVersion = "1.3.11";
		private const string AllowedDataLocationsPath = "AllowedDataLocations";
		private const string StorageQuotasMinimumApiVersion = "1.3.1";
		private const string StorageQuotasPath = "StorageQuotas";
		private const string StorageQuotaByLocationPath = "StorageQuotas(geoLocation='{0}')";
		private const string MultiGeoApiVersionsPath = "MultiGeoApiVersions";
		private const string PatchVerbString = "PATCH";
		private const string UserMoveJobsMinimumApiVersion = "1.0";
		private const string UserMoveJobsByMoveIdMinimumApiVersion = "1.2.2";
		private const string UserMoveJobsReportMinimumApiVersion = "1.3.2";
		private const string UserMoveJobsPath = "UserMoveJobs";
		private const string UserMoveJobPathByUpn = "UserMoveJobs(upn='{0}')";
		private const string UserMoveJobPathByMoveId = "UserMoveJobs/GetByMoveId(odbMoveId='{0}')";
		private const string UserMoveJobCancelPath = UserMoveJobPathByUpn + "/Cancel";
		private const string UserMoveJobsPathForMoveReport = "UserMoveJobs/GetMoveReport(moveState={0},moveDirection={1},startTime='{2:u}',endTime='{3:u}',limit='{4}')";
		private const string GroupMoveJobsMinimumApiVersion = "1.3.0";
		private const string GroupMoveJobsPath = "GroupMoveJobs";
		private const string GroupMoveJobPathByGroupName = "GroupMoveJobs(groupname='{0}')";
		private const string SiteMoveJobsMinimumApiVersion = "1.3.0";
		private const string SiteMoveJobsReportMinimumApiVersion = "1.3.8";
		private const string SiteMoveJobsPath = "SiteMoveJobs";
		private const string SiteMoveJobPathByUrl = "SiteMoveJobs(url='{0}')";
		private const string SiteMoveJobPathByMoveId = "SiteMoveJobs/GetByMoveId(SiteMoveId='{0}')";
		private const string SiteMoveJobsPathForMoveReport = "SiteMoveJobs/GetMoveReport(moveState={0},moveDirection={1},startTime='{2:u}',endTime='{3:u}',limit='{4}')";
		private const string SiteMoveJobCancelPath = SiteMoveJobPathByUrl + "/Cancel";
		private const int MaximumPagination = 10;
		private const int ApiVersionCacheValidTimeInHours = 1;
		private static readonly TimeSpan CreateTenantRenameJobTimeout = TimeSpan.FromSeconds(300);
		private static readonly string[] ClientSupportedApiVersions =
		[
			"1.6.0",
			"1.5.20",
			"1.5.19",
			"1.5.18",
			"1.5.17",
			"1.5.16",
			"1.5.15",
			"1.5.14",
			"1.5.13",
			"1.5.12",
			"1.5.11",
			"1.5.10",
			"1.5.9",
			"1.5.8",
			"1.5.7",
			"1.5.6",
			"1.5.5",
			"1.5.4",
			"1.5.3",
			"1.5.2",
			"1.5.1",
			"1.5.0",
			"1.4.7",
			"1.4.6",
			"1.4.5",
			"1.4.4",
			"1.4.3",
			"1.4.2",
			"1.4.1",
			"1.4.0",
			"1.3.11",
			"1.3.10",
			"1.3.9",
			"1.3.8",
			"1.3.7",
			"1.3.6",
			"1.3.5",
			"1.3.4",
			"1.3.3-beta",
			"1.3.2",
			"1.3.1",
			"1.3.0",
			"1.2.2",
			"1.2.1-beta",
			"1.2-beta",
			"1.1",
			"1.0"
		];
		private static readonly ConcurrentDictionary<string, CachedApiVersion> ApiVersionCache = new(StringComparer.OrdinalIgnoreCase);
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
			return GetFeed<GeoMoveTenantCompatibilityCheck>(GeoMoveCompatibilityChecksPath, GetCurrentApiVersion(GeoMoveCompatibilityChecksMinimumApiVersion));
		}

		internal IEnumerable<MultiGeoCompanyAllowedDataLocation> GetAllowedDataLocations()
		{
			return GetFeed<MultiGeoCompanyAllowedDataLocation>(AllowedDataLocationsPath, AllowedDataLocationsApiVersion);
		}

		internal void UpgradeGeoExperience(bool allInstances)
		{
			var apiVersion = GetGeoExperienceApiVersion();
			PostWithEmptyBody(allInstances ? UpdateAllInstancesExperienceModePath : UpdateGeoExperienceModePath, apiVersion);
		}

		internal void EnsureGeoExperienceUpgradeSupported()
		{
			GetGeoExperienceApiVersion();
		}

		internal void AddAllowedDataLocation(MultiGeoCompanyAllowedDataLocationEntityData allowedDataLocation)
		{
			if (allowedDataLocation == null)
			{
				throw new ArgumentNullException(nameof(allowedDataLocation));
			}

			var apiVersion = GetCurrentApiVersion();
			if (!IsSupportedApiVersion(apiVersion, AllowedDataLocationsApiVersion))
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, CommandResources.CrossGeoInvalidVersion, typeof(MultiGeoRestApiClient).Assembly.GetName().Version));
			}

			PostWithoutResponse(AllowedDataLocationsPath, allowedDataLocation, apiVersion);
		}

		internal IEnumerable<StorageQuota> GetStorageQuotas()
		{
			return GetFeed<StorageQuota>(StorageQuotasPath, GetStorageQuotasApiVersion());
		}

		internal StorageQuota GetStorageQuotaByLocation(string geoLocation)
		{
			var apiVersion = GetStorageQuotasApiVersion();
			var path = string.Format(CultureInfo.InvariantCulture, StorageQuotaByLocationPath, ProcessSpecialChars(geoLocation));
			return Get<StorageQuota>(path, apiVersion);
		}

		internal UserAndContentMoveState GetUserAndContentMoveState(string userPrincipalName)
		{
			var apiVersion = GetCurrentApiVersion(UserMoveJobsMinimumApiVersion);
			var path = string.Format(CultureInfo.InvariantCulture, UserMoveJobPathByUpn, ProcessSpecialChars(userPrincipalName));
			return Get<UserAndContentMoveState>(path, apiVersion);
		}

		internal UserAndContentMoveState GetUserAndContentMoveState(Guid odbMoveId)
		{
			var apiVersion = GetCurrentApiVersion(UserMoveJobsByMoveIdMinimumApiVersion);
			var path = string.Format(CultureInfo.InvariantCulture, UserMoveJobPathByMoveId, odbMoveId);
			return Get<UserAndContentMoveState>(path, apiVersion);
		}

		internal IEnumerable<UserAndContentMoveState> GetUserAndContentMoveStates(MoveState moveState, MoveDirection moveDirection, DateTime startTime, DateTime endTime, uint limit)
		{
			var apiVersion = GetCurrentApiVersion(UserMoveJobsReportMinimumApiVersion);
			var path = string.Format(CultureInfo.InvariantCulture, UserMoveJobsPathForMoveReport, (int)moveState, (int)moveDirection, startTime, endTime, limit);
			return GetFeed<UserAndContentMoveState>(path, apiVersion);
		}

		internal UserAndContentMoveState GetUnifiedGroupMoveState(string groupAlias)
		{
			var apiVersion = GetCurrentApiVersion(GroupMoveJobsMinimumApiVersion);
			var path = string.Format(CultureInfo.InvariantCulture, GroupMoveJobPathByGroupName, ProcessSpecialChars(groupAlias));
			return Get<UserAndContentMoveState>(path, apiVersion);
		}

		internal SiteMoveJob GetSiteMoveJob(string sourceSiteUrl)
		{
			var apiVersion = GetCurrentApiVersion(SiteMoveJobsMinimumApiVersion);
			var path = string.Format(CultureInfo.InvariantCulture, SiteMoveJobPathByUrl, ProcessSpecialChars(sourceSiteUrl));
			return Get<SiteMoveJob>(path, apiVersion);
		}

		internal SiteMoveJob GetSiteMoveJob(Guid siteMoveId)
		{
			var apiVersion = GetCurrentApiVersion(SiteMoveJobsMinimumApiVersion);
			var path = string.Format(CultureInfo.InvariantCulture, SiteMoveJobPathByMoveId, siteMoveId);
			return Get<SiteMoveJob>(path, apiVersion);
		}

		internal IEnumerable<SiteMoveJob> GetSiteMoveJobs(MoveState moveState, MoveDirection moveDirection, DateTime startTime, DateTime endTime, uint limit)
		{
			var apiVersion = GetCurrentApiVersion(SiteMoveJobsReportMinimumApiVersion);
			var path = string.Format(CultureInfo.InvariantCulture, SiteMoveJobsPathForMoveReport, (int)moveState, (int)moveDirection, startTime, endTime, limit);
			return GetFeed<SiteMoveJob>(path, apiVersion);
		}

		internal UserAndContentMoveState CreateUserMoveJob(UserMoveJobEntityData job)
		{
			if (job == null)
			{
				throw new ArgumentNullException(nameof(job));
			}

			job.ApiVersion = GetCurrentApiVersion(UserMoveJobsMinimumApiVersion);
			return Post<UserAndContentMoveState>(UserMoveJobsPath, job, apiVersion: UserMoveJobsMinimumApiVersion);
		}

		internal UserAndContentMoveState CreateGroupMoveJob(GroupMoveJobEntityData job)
		{
			if (job == null)
			{
				throw new ArgumentNullException(nameof(job));
			}

			var apiVersion = GetCurrentApiVersion(GroupMoveJobsMinimumApiVersion);
			job.ApiVersion = apiVersion;
			return Post<UserAndContentMoveState>(GroupMoveJobsPath, job, apiVersion: apiVersion);
		}

		internal SiteMoveJob CreateSiteMoveJob(SiteMoveJobEntityData job)
		{
			if (job == null)
			{
				throw new ArgumentNullException(nameof(job));
			}

			var apiVersion = GetCurrentApiVersion(SiteMoveJobsMinimumApiVersion);
			job.ApiVersion = apiVersion;
			return Post<SiteMoveJob>(SiteMoveJobsPath, job, apiVersion: apiVersion);
		}

		internal bool IsCurrentApiVersionSupported(string minimumApiVersion)
		{
			return IsSupportedApiVersion(GetCurrentApiVersion(), minimumApiVersion);
		}

		internal void PartialUpdateStorageQuota(StorageQuotaEntityData quota)
		{
			if (quota == null)
			{
				throw new ArgumentNullException(nameof(quota));
			}

			var apiVersion = GetStorageQuotasApiVersion();
			var path = string.Format(CultureInfo.InvariantCulture, StorageQuotaByLocationPath, ProcessSpecialChars(quota.GeoLocation));
			PostWithMethodOverride(path, quota, PatchVerbString, apiVersion);
		}

		internal void CancelUserMoveJob(string userPrincipalName)
		{
			var apiVersion = GetCurrentApiVersion(UserMoveJobsMinimumApiVersion);
			var path = string.Format(CultureInfo.InvariantCulture, UserMoveJobCancelPath, ProcessSpecialChars(userPrincipalName));
			PostWithEmptyBody(path, apiVersion);
		}

		internal void CancelSiteMoveJob(string sourceSiteUrl)
		{
			var apiVersion = GetCurrentApiVersion(SiteMoveJobsMinimumApiVersion);
			var path = string.Format(CultureInfo.InvariantCulture, SiteMoveJobCancelPath, ProcessSpecialChars(sourceSiteUrl));
			PostWithEmptyBody(path, apiVersion);
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

		private T GetWithoutApiVersion<T>(string path)
		{
			var responseText = Send(() => CreateRequest(HttpMethod.Get, CreateApiUri(path)), timeout: null, allowRetries: true);
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

		private void PostWithoutResponse(string path, object payload, string apiVersion)
		{
			var jsonPayload = payload == null ? null : JsonSerializer.Serialize(payload, SerializerOptions);
			Send(() => CreateRequest(HttpMethod.Post, path, apiVersion, jsonPayload), timeout: null, allowRetries: false);
		}

		private void PostWithEmptyBody(string path, string apiVersion)
		{
			Send(() => CreateRequest(HttpMethod.Post, path, apiVersion, string.Empty), timeout: null, allowRetries: false);
		}

		private void PostWithMethodOverride(string path, object payload, string methodOverride, string apiVersion)
		{
			var jsonPayload = payload == null ? null : JsonSerializer.Serialize(payload, SerializerOptions);
			Send(() =>
			{
				var request = CreateRequest(HttpMethod.Post, path, apiVersion, jsonPayload);
				request.Headers.TryAddWithoutValidation("X-HTTP-Method", methodOverride);
				if (request.Content != null)
				{
					request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=UTF-8");
				}

				return request;
			}, timeout: null, allowRetries: false);
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

		private Uri CreateApiUri(string path)
		{
			var normalizedPath = path.TrimStart('/');
			return new Uri($"{adminContext.Url.TrimEnd('/')}/_api/{normalizedPath}");
		}

		private string GetCurrentApiVersion(string minimumApiVersion)
		{
			var apiVersion = GetCurrentApiVersion();
			if (!IsSupportedApiVersion(apiVersion, minimumApiVersion))
			{
				throw new NotSupportedException($"SharePoint Online MultiGeo API version {apiVersion} does not support this operation. Minimum required version is {minimumApiVersion}.");
			}

			return apiVersion;
		}

		private string GetCurrentApiVersion()
		{
			var cacheKey = adminContext.Url.TrimEnd('/');
			if (ApiVersionCache.TryGetValue(cacheKey, out var cachedApiVersion) && cachedApiVersion.ExpiresOnUtc > DateTime.UtcNow)
			{
				return cachedApiVersion.Identity;
			}

			var supportedVersions = GetWithoutApiVersion<ApiVersions>(MultiGeoApiVersionsPath)?.SupportedVersions;
			var currentApiVersion = GetLatestClientSupportedApiVersion(supportedVersions);
			ApiVersionCache[cacheKey] = new CachedApiVersion
			{
				Identity = currentApiVersion,
				ExpiresOnUtc = DateTime.UtcNow.AddHours(ApiVersionCacheValidTimeInHours)
			};

			return currentApiVersion;
		}

		private static string GetLatestClientSupportedApiVersion(IEnumerable<string> supportedVersions)
		{
			if (supportedVersions == null)
			{
				throw new InvalidOperationException("SharePoint Online REST API did not return any supported MultiGeo API versions.");
			}

			var supportedVersionSet = new HashSet<string>(supportedVersions, StringComparer.OrdinalIgnoreCase);
			var apiVersion = ClientSupportedApiVersions.FirstOrDefault(supportedVersionSet.Contains);
			if (apiVersion == null)
			{
				throw new InvalidOperationException("SharePoint Online REST API did not return a supported MultiGeo API version.");
			}

			return apiVersion;
		}

		private static bool IsSupportedApiVersion(string apiVersion, string minimumApiVersion)
		{
			var apiVersionIndex = Array.IndexOf(ClientSupportedApiVersions, apiVersion);
			var minimumApiVersionIndex = Array.IndexOf(ClientSupportedApiVersions, minimumApiVersion);
			return apiVersionIndex >= 0 && minimumApiVersionIndex >= 0 && apiVersionIndex <= minimumApiVersionIndex;
		}

		private string GetStorageQuotasApiVersion()
		{
			var apiVersion = GetCurrentApiVersion();
			if (!IsSupportedApiVersion(apiVersion, StorageQuotasMinimumApiVersion))
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, CommandResources.CrossGeoInvalidVersion, GetApplicationVersion()));
			}

			return apiVersion;
		}

		private string GetGeoExperienceApiVersion()
		{
			var apiVersion = GetCurrentApiVersion();
			if (!IsSupportedApiVersion(apiVersion, GeoExperienceMinimumApiVersion))
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "The client version '{0}' is not supported. Please try to upgrade client version first.", GetApplicationVersion()));
			}

			return apiVersion;
		}

		private static string GetApplicationVersion()
		{
			var assembly = Assembly.GetExecutingAssembly();
			return assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? assembly.GetName().Version?.ToString();
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

		private sealed class ApiVersions
		{
			public string[] SupportedVersions { get; set; }
		}

		private sealed class CachedApiVersion
		{
			public string Identity { get; set; }

			public DateTime ExpiresOnUtc { get; set; }
		}
	}
}
