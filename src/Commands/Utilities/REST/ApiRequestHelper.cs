using PnP.Framework.Diagnostics;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace PnP.PowerShell.Commands.Utilities.REST
{
    /// <summary>
    /// Helper class that aids in making calls towards the RESTful APIs
    /// </summary>
    public class ApiRequestHelper
    {
        /// <summary>
        /// Connection to use to make the calls
        /// </summary>
        public PnPConnection Connection { get; private set; }

        /// <summary>
        /// Type of cmdlet to make the calls for
        /// </summary>
        private Type CmdletType { get; set; }

        /// <summary>
        /// Access token to use for the calls
        /// </summary>
        private string AccessToken => TokenHandler.GetAccessToken(Audience, Connection);

        /// <summary>
        /// Audience to use for the oAuth JWT
        /// </summary>
        public string Audience { get; private set; }

        /// <summary>
        /// The default Graph endpoint
        /// </summary>
        public string GraphEndPoint => Connection.GraphEndPoint;

        /// <summary>
        /// Instantiates a new instance of the <see cref="ApiRequestHelper"/> class which can be used to make calls to the RESTful APIs
        /// </summary>
        /// <param name="cmdletType">Type of cmdlet to make the calls for</param>
        /// <param name="connection">Connection to use to make the calls</param>
        /// <param name="audience">Audience to use for the oAuth JWT. Defaults to Microsoft Graph if not specified.</param>
        public ApiRequestHelper(Type cmdletType, PnPConnection connection, string audience = null)
        {
            Connection = connection;
            CmdletType = cmdletType;
            Audience = audience ?? $"https://{Connection.GraphEndPoint}/.default";
        }

        private static void LogDebug(string message)
        {
            Log.Debug("ApiRequestHelper", message);
        }

        private static void LogError(string message)
        {
            Log.Error("ApiRequestHelper", message);
        }

        public bool TryGetGraphException(HttpResponseMessage responseMessage, out GraphException exception)
        {
            if (responseMessage == null)
            {
                exception = null;
                return false;
            }
            var content = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (string.IsNullOrEmpty(content))
            {
                exception = null;
                return false;
            }
            try
            {
                exception = JsonSerializer.Deserialize<GraphException>(content, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return true;
            }
            catch
            {
                exception = null;
                return false;
            }
        }

        public static bool TryGetGraphException2(HttpResponseMessage responseMessage, out GraphException exception)
        {
            if (responseMessage == null)
            {
                exception = null;
                return false;
            }
            var content = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (string.IsNullOrEmpty(content))
            {
                exception = null;
                return false;
            }
            try
            {
                exception = JsonSerializer.Deserialize<GraphException>(content, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return true;
            }
            catch
            {
                exception = null;
                return false;
            }
        }

        public static bool IsUnauthorizedAccessException(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return false;

            try
            {
                using var doc = JsonDocument.Parse(message);
                var root = doc.RootElement;

                if (root.TryGetProperty("odata.error", out var odataError))
                {
                    if (odataError.TryGetProperty("code", out var codeProp))
                    {
                        var code = codeProp.GetString();
                        if (!string.IsNullOrEmpty(code) && code.Contains("System.UnauthorizedAccessException", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (JsonException)
            {
                // Not a valid JSON, ignore
            }

            return false;
        }

        #region GET

        private HttpRequestMessage GetMessage(string url, HttpMethod method, HttpContent content = null, IDictionary<string, string> additionalHeaders = null)
        {
            if (url.StartsWith("/"))
            {
                url = url[1..];
            }

            var message = new HttpRequestMessage
            {
                Version = new Version(2, 0),
                Method = method
            };
            if (!url.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!url.StartsWith("v1.0/", StringComparison.InvariantCultureIgnoreCase) && !url.StartsWith("beta/", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Assume v1.0
                    url = $"v1.0/{url}";
                }
                url = $"https://{Connection.GraphEndPoint}/{url}";
            }
            message.Headers.TryAddWithoutValidation("Accept", "application/json");
            message.RequestUri = new Uri(url);
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            if (additionalHeaders != null)
            {
                foreach (var kv in additionalHeaders)
                {
                    message.Headers.Remove(kv.Key);
                    message.Headers.Add(kv.Key, kv.Value);
                }
            }
            if (method == HttpMethod.Post || method == HttpMethod.Put || method.Method == "PATCH")
            {
                message.Content = content;
            }

            return message;
        }

        private static HttpRequestMessage GetMessage2(string url, string accessToken, HttpMethod method, HttpContent content = null, IDictionary<string, string> additionalHeaders = null)
        {
            if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            var message = new HttpRequestMessage
            {
                Version = new Version(2, 0),
                Method = method
            };
            message.Headers.TryAddWithoutValidation("Accept", "application/json");
            message.RequestUri = new Uri(url);
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            if (additionalHeaders != null)
            {
                foreach (var kv in additionalHeaders)
                {
                    message.Headers.Remove(kv.Key);
                    message.Headers.Add(kv.Key, kv.Value);
                }
            }
            if (method == HttpMethod.Post || method == HttpMethod.Put || method.Method == "PATCH")
            {
                message.Content = content;
            }

            return message;
        }

        public string Get(string url, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Get, null, additionalHeaders);
            return SendMessage(message);
        }

        public HttpResponseMessage GetResponse(string url)
        {
            var message = GetMessage(url, HttpMethod.Get);
            return GetResponseMessage(message);
        }

        /// <summary>
        /// Queries the provided URL and looks for the NextLink in the results to fetch all the results
        /// </summary>
        /// <typeparam name="T">Type of object to cast the response items to</typeparam>
        /// <param name="url">Url to query</param>
        /// <param name="camlCasePolicy">Policy indicating the CamlCase that should be applied when mapping results to typed objects</param>
        /// <param name="propertyNameCaseInsensitive">Indicates if the response be mapped to the typed object ignoring different casing</param>
        /// <returns>List with objects of type T returned by the request</returns>
        public IEnumerable<T> GetResultCollection<T>(string url, bool camlCasePolicy = true, bool propertyNameCaseInsensitive = false, IDictionary<string, string> additionalHeaders = null)
        {
            var results = new List<T>();
            var request = Get<RestResultCollection<T>>(url, camlCasePolicy, propertyNameCaseInsensitive, additionalHeaders);

            if (request != null && request.Items.Any())
            {
                results.AddRange(request.Items);
                while (!string.IsNullOrEmpty(request.NextLink))
                {
                    LogDebug($"Paged request. Thus far {results.Count} {typeof(T)} item{(results.Count != 1 ? "s" : "")} retrieved.");

                    request = Get<RestResultCollection<T>>(request.NextLink, camlCasePolicy, propertyNameCaseInsensitive, additionalHeaders);
                    if (request.Items.Any())
                    {
                        results.AddRange(request.Items);
                    }
                }
            }
            LogDebug($"Returning {results.Count} {typeof(T)} item{(results.Count != 1 ? "s" : "")}");

            return results;
        }

        /// <summary>
        /// Queries the provided URL and returns the results as typed objects. It does NOT follow NextLink pages, use GetResultCollectionAsync instead for that.
        /// </summary>
        /// <typeparam name="T">Type of object to cast the response items to</typeparam>
        /// <param name="url">Url to query</param>
        /// <param name="camlCasePolicy">Policy indicating the CamlCase that should be applied when mapping results to typed objects</param>
        /// <param name="propertyNameCaseInsensitive">Indicates if the response be mapped to the typed object ignoring different casing</param>
        /// <returns>List with objects of type T returned by the request</returns>
        public T Get<T>(string url, bool camlCasePolicy = true, bool propertyNameCaseInsensitive = false, IDictionary<string, string> additionalHeaders = null)
        {
            var stringContent = Get(url, additionalHeaders);
            if (stringContent != null)
            {
                var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
                options.Converters.Add(new JsonStringEnumConverter());
                if (camlCasePolicy)
                {
                    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                }
                if (propertyNameCaseInsensitive)
                {
                    options.PropertyNameCaseInsensitive = true;
                }
                try
                {
                    var entity = JsonSerializer.Deserialize<T>(stringContent, options);
                    return entity;
                }
                catch (Exception e)
                {
                    LogError($"Failed to parse response from server. Error message: '{e.Message}'. Received content: '{stringContent.Replace("{", "{{").Replace("}", "}}")}'. Model type to parse it to: '{typeof(T)}'.");
                    //Cmdlet.LogWarning($"Failed to parse response from server. Error message: '{e.Message}'. Received content: '{stringContent}'. Model type to parse it to: '{typeof(T)}'.");
                    return default;
                }
            }
            return default;
        }

        #endregion

        #region DELETE
        public HttpResponseMessage Delete(string url, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Delete, null, additionalHeaders);
            return GetResponseMessage(message);
        }

        public T Delete<T>(Cmdlet cmdlet, PnPConnection connection, string url, bool camlCasePolicy = true, IDictionary<string, string> additionalHeaders = null)
        {
            var response = Delete(url);
            if (response.IsSuccessStatusCode)
            {
                var stringContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (stringContent != null)
                {
                    var options = new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                    if (camlCasePolicy)
                    {
                        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    }
                    try
                    {
                        return JsonSerializer.Deserialize<T>(stringContent, options);
                    }
                    catch (Exception)
                    {
                        return default(T);
                    }
                }
            }
            return default(T);
        }
        #endregion

        #region PATCH
        public T Patch<T>(string url, T content, IDictionary<string, string> additionalHeaders = null, bool camlCasePolicy = true)
        {
            var serializerSettings = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            if (camlCasePolicy)
            {
                serializerSettings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }
            var requestContent = new StringContent(JsonSerializer.Serialize(content, serializerSettings));
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var message = GetMessage(url, HttpMethod.Patch, requestContent, additionalHeaders);
            var returnValue = SendMessage(message);
            if (!string.IsNullOrEmpty(returnValue))
            {
                return JsonSerializer.Deserialize<T>(returnValue);
            }
            else
            {
                return default;
            }
        }

        public T Patch<T>(string url, HttpContent content, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Patch, content, additionalHeaders);
            var returnValue = SendMessage(message);
            if (!string.IsNullOrEmpty(returnValue))
            {
                return JsonSerializer.Deserialize<T>(returnValue);
            }
            else
            {
                return default;
            }
        }

        public HttpResponseMessage Patch(HttpContent content, string url, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Patch, content, additionalHeaders);
            return GetResponseMessage(message);
        }

        #endregion

        #region POST

        public HttpResponseMessage PostHttpContent(string url, HttpContent content, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Post, content, additionalHeaders);
            return GetResponseMessage(message);
        }

        public T Post<T>(string url, HttpContent content, IDictionary<string, string> additionalHeaders = null, bool propertyNameCaseInsensitive = false)
        {
            return PostInternal<T>(url, content, additionalHeaders, propertyNameCaseInsensitive);
        }

        public T Post<T>(string url, T content)
        {
            // If we're passing in content which derives from HttpContent, we'll leave it as is. If not, we'll try to serialize it to JSON.
            if (content is not HttpContent requestContent)
            {
                requestContent = new StringContent(JsonSerializer.Serialize(content, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }

            return PostInternal<T>(url, requestContent);
        }

        public T Post<T>(string url)
        {
            return PostInternal<T>(url, null);
        }

        private void PostInternal(string url, HttpContent content, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Post, content, additionalHeaders);
            GetResponseMessage(message);
        }

        private T PostInternal<T>(string url, HttpContent content, IDictionary<string, string> additionalHeaders = null, bool propertyNameCaseInsensitive = false)
        {
            var message = GetMessage(url, HttpMethod.Post, content, additionalHeaders);
            var stringContent = SendMessage(message);
            if (stringContent != null)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = propertyNameCaseInsensitive });
                }
                catch
                {
                    return default;
                }
            }
            return default;
        }

        #endregion

        #region PUT

        public T Put<T>(string url, HttpContent content, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Put, content, additionalHeaders);
            var stringContent = SendMessage(message);
            if (stringContent != null)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(stringContent);
                }
                catch
                {
                    return default;
                }
            }
            return default;
        }

        public T Put<T>(string url, T content)
        {
            var requestContent = new StringContent(JsonSerializer.Serialize(content, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var message = GetMessage(url, HttpMethod.Put, requestContent);
            var returnValue = SendMessage(message);
            if (!string.IsNullOrEmpty(returnValue))
            {
                return JsonSerializer.Deserialize<T>(returnValue, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                return default;
            }
        }

        public HttpResponseMessage PutHttpContent(string url, HttpContent content, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Put, content, additionalHeaders);
            return GetResponseMessage(message);
        }

        public HttpResponseMessage Put2(string url, HttpContent content, string accessToken, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage2(url, accessToken, HttpMethod.Put, content, additionalHeaders);
            return GetResponseMessage2(message);
        }

        #endregion

        #region DELETE

        public HttpResponseMessage Delete(string url)
        {
            var message = GetMessage(url, HttpMethod.Delete);
            var response = GetResponseMessage(message);
            return response;
        }

        #endregion

        private string SendMessage(HttpRequestMessage message)
        {
            LogDebug($"Making {message.Method} call to {message.RequestUri}{(message.Content != null ? $" with payload" : "")}");

            // Ensure we have the required permissions in the access token to make the call
            TokenHandler.EnsureRequiredPermissionsAvailableInAccessTokenAudience(CmdletType, AccessToken);

            var response = Connection.HttpClient.SendAsync(message).GetAwaiter().GetResult();
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;

                LogDebug($"Call got throttled. Retrying in {retryAfter.Delta.Value.Seconds} second{(retryAfter.Delta.Value.Seconds != 1 ? "s" : "")}.");

                Thread.Sleep(retryAfter.Delta.Value.Seconds * 1000);
                LogDebug($"Making {message.Method} call to {message.RequestUri}");
                response = Connection.HttpClient.SendAsync(CloneMessage(message)).GetAwaiter().GetResult();
            }
            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                LogDebug($"Response successful with HTTP {(int)response.StatusCode} {response.StatusCode} containing {responseBody.Length} character{(responseBody.Length != 1 ? "s" : "")}");

                return responseBody;
            }
            else
            {
                var errorContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                LogError($"Response failed with HTTP {(int)response.StatusCode} containing {errorContent.Length} character{(errorContent.Length != 1 ? "s" : "")}: {errorContent.Replace("{", "{{").Replace("}", "}}")}");

                var exception = JsonSerializer.Deserialize<GraphException>(errorContent, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                exception.AccessToken = AccessToken;
                exception.HttpResponse = response;

                throw exception;
            }
        }

        public HttpResponseMessage GetResponseMessage(HttpRequestMessage message)
        {
            LogDebug($"Making {message.Method} call to {message.RequestUri}");

            var response = Connection.HttpClient.SendAsync(message).GetAwaiter().GetResult();
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;
                LogDebug($"Call got throttled. Retrying in {retryAfter.Delta.Value.Seconds} second{(retryAfter.Delta.Value.Seconds != 1 ? "s" : "")}.");

                Thread.Sleep(retryAfter.Delta.Value.Seconds * 1000);

                LogDebug($"Making {message.Method} call to {message.RequestUri}");
                response = Connection.HttpClient.SendAsync(CloneMessage(message)).GetAwaiter().GetResult();
            }

            // Validate if the response was successful, if not throw an exception
            if (!response.IsSuccessStatusCode)
            {
                LogDebug($"Response failed with HTTP {(int)response.StatusCode} {response.StatusCode}");

                if (TryGetGraphException(response, out GraphException ex))
                {
                    if (ex.Error != null)
                    {
                        throw new PSInvalidOperationException($"Call to Microsoft Graph URL {message.RequestUri} failed with status code {response.StatusCode}{(!string.IsNullOrEmpty(ex.Error.Message) ? $": {ex.Error.Message}" : "")}");
                    }
                }
                else
                {
                    throw new PSInvalidOperationException($"Call to Microsoft Graph URL {message.RequestUri} failed with status code {response.StatusCode}");
                }
            }
            else
            {
                LogDebug($"Response successful with HTTP {(int)response.StatusCode} {response.StatusCode}");
            }

            return response;
        }

        public HttpResponseMessage GetResponseMessage2(HttpRequestMessage message)
        {
            var response = Connection.HttpClient.SendAsync(message).GetAwaiter().GetResult();
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;

                Thread.Sleep(retryAfter.Delta.Value.Seconds * 1000);

                response = Connection.HttpClient.SendAsync(CloneMessage(message)).GetAwaiter().GetResult();
            }

            // Validate if the response was successful, if not throw an exception
            if (!response.IsSuccessStatusCode)
            {
                if (TryGetGraphException2(response, out GraphException ex))
                {
                    if (ex.Error != null)
                    {
                        throw new PSInvalidOperationException($"Call to Microsoft Graph URL {message.RequestUri} failed with status code {response.StatusCode}{(!string.IsNullOrEmpty(ex.Error.Message) ? $": {ex.Error.Message}" : "")}");
                    }
                }
                else
                {
                    throw new PSInvalidOperationException($"Call to Microsoft Graph URL {message.RequestUri} failed with status code {response.StatusCode}");
                }
            }

            return response;
        }

        private static HttpRequestMessage CloneMessage(HttpRequestMessage req)
        {
            HttpRequestMessage clone = new(req.Method, req.RequestUri)
            {
                Content = req.Content,
                Version = req.Version
            };

            foreach (KeyValuePair<string, object> prop in req.Options)
            {
                clone.Options.Set(new HttpRequestOptionsKey<object>(prop.Key), prop.Value);
            }

            foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return clone;
        }
    }
}
