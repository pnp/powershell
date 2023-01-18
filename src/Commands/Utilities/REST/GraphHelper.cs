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
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities.REST
{
    /// <summary>
    /// Helper class that aids in making calls towards the Microsoft Graph API
    /// </summary>
    internal static class GraphHelper
    {
        public static bool TryGetGraphException(HttpResponseMessage responseMessage, out GraphException exception)
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

        private static HttpRequestMessage GetMessage(string url, HttpMethod method, PnPConnection connection, string accessToken, HttpContent content = null, IDictionary<string, string> additionalHeaders = null)
        {
            if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            var message = new HttpRequestMessage();
            message.Version = new Version(2, 0);
            message.Method = method;
            message.Headers.TryAddWithoutValidation("Accept", "application/json");
            message.RequestUri = !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? new Uri($"https://{connection.GraphEndPoint}/{url}") : new Uri(url);
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

        public static async Task<string> GetAsync(PnPConnection connection, string url, string accessToken, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Get, connection, accessToken, null, additionalHeaders);
            return await SendMessageAsync(connection, message, accessToken);
        }

        public static async Task<HttpResponseMessage> GetResponseAsync(PnPConnection connection, string url, string accessToken)
        {
            var message = GetMessage(url, HttpMethod.Get, connection, accessToken);
            return await GetResponseMessageAsync(connection, message);
        }

        /// <summary>
        /// Queries the provided URL and looks for the NextLink in the results to fetch all the results
        /// </summary>
        /// <typeparam name="T">Type of object to cast the response items to</typeparam>
        /// <param name="connection">Connection to use to make the request</param>
        /// <param name="url">Url to query</param>
        /// <param name="accessToken">AccessToken to use for authorizing the request</param>
        /// <param name="camlCasePolicy">Policy indicating the CamlCase that should be applied when mapping results to typed objects</param>
        /// <param name="propertyNameCaseInsensitive">Indicates if the response be mapped to the typed object ignoring different casing</param>
        /// <returns>List with objects of type T returned by the request</returns>
        public static async Task<IEnumerable<T>> GetResultCollectionAsync<T>(PnPConnection connection, string url, string accessToken, bool camlCasePolicy = true, bool propertyNameCaseInsensitive = false, IDictionary<string, string> additionalHeaders = null)
        {
            var results = new List<T>();
            var request = await GetAsync<RestResultCollection<T>>(connection, url, accessToken, camlCasePolicy, propertyNameCaseInsensitive, additionalHeaders);

            if (request.Items.Any())
            {
                results.AddRange(request.Items);
                while (!string.IsNullOrEmpty(request.NextLink))
                {
                    request = await GetAsync<RestResultCollection<T>>(connection, request.NextLink, accessToken, camlCasePolicy, propertyNameCaseInsensitive, additionalHeaders);
                    if (request.Items.Any())
                    {
                        results.AddRange(request.Items);
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Queries the provided URL and returns the results as typed objects. It does NOT follow NextLink pages, use GetResultCollectionAsync instead for that.
        /// </summary>
        /// <typeparam name="T">Type of object to cast the response items to</typeparam>
        /// <param name="connection">Connection to use to make the request</param>
        /// <param name="url">Url to query</param>
        /// <param name="accessToken">AccessToken to use for authorizing the request</param>
        /// <param name="camlCasePolicy">Policy indicating the CamlCase that should be applied when mapping results to typed objects</param>
        /// <param name="propertyNameCaseInsensitive">Indicates if the response be mapped to the typed object ignoring different casing</param>
        /// <returns>List with objects of type T returned by the request</returns>
        public static async Task<T> GetAsync<T>(PnPConnection connection, string url, string accessToken, bool camlCasePolicy = true, bool propertyNameCaseInsensitive = false, IDictionary<string, string> additionalHeaders = null)
        {
            var stringContent = await GetAsync(connection, url, accessToken, additionalHeaders);
            if (stringContent != null)
            {
                var options = new JsonSerializerOptions { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
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
                catch (Exception)
                {
                    return default(T);
                }
            }
            return default(T);
        }

        public static async Task<HttpResponseMessage> PostAsync(PnPConnection connection, string url, string accessToken, HttpContent content, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Post, connection, accessToken, content, additionalHeaders);
            return await GetResponseMessageAsync(connection, message);
        }

        public static async Task<HttpResponseMessage> PutAsync(PnPConnection connection, string url, string accessToken, HttpContent content, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Put, connection, accessToken, content, additionalHeaders);
            return await GetResponseMessageAsync(connection, message);
        }

#region DELETE
        public static async Task<HttpResponseMessage> DeleteAsync(PnPConnection connection, string url, string accessToken, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Delete, connection, accessToken, null, additionalHeaders);
            return await GetResponseMessageAsync(connection, message);
        }

        public static async Task<T> DeleteAsync<T>(PnPConnection connection, string url, string accessToken, bool camlCasePolicy = true, IDictionary<string, string> additionalHeaders = null)
        {
            var response = await DeleteAsync(connection, url, accessToken, additionalHeaders);
            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();

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
        public static async Task<T> PatchAsync<T>(PnPConnection connection, string accessToken, string url, T content, IDictionary<string, string> additionalHeaders = null, bool camlCasePolicy = true)
        {
            var serializerSettings = new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
            if (camlCasePolicy)
            {
                serializerSettings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }
            var requestContent = new StringContent(JsonSerializer.Serialize(content, serializerSettings));
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var message = GetMessage(url, HttpMethod.Patch, connection, accessToken, requestContent, additionalHeaders);
            var returnValue = await SendMessageAsync(connection, message, accessToken);
            if (!string.IsNullOrEmpty(returnValue))
            {
                return JsonSerializer.Deserialize<T>(returnValue);
            }
            else
            {
                return default;
            }
        }

        public static async Task<T> PatchAsync<T>(PnPConnection connection, string accessToken, string url, HttpContent content, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Patch, connection, accessToken, content, additionalHeaders);
            var returnValue = await SendMessageAsync(connection, message, accessToken);
            if (!string.IsNullOrEmpty(returnValue))
            {
                return JsonSerializer.Deserialize<T>(returnValue);
            }
            else
            {
                return default;
            }
        }

        public static async Task<HttpResponseMessage> PatchAsync(PnPConnection connection, string accessToken, HttpContent content, string url, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Patch, connection, accessToken, content, additionalHeaders);
            return await GetResponseMessageAsync(connection, message);
        }

#endregion

        public static async Task<T> PostAsync<T>(PnPConnection connection, string url, HttpContent content, string accessToken, IDictionary<string, string> additionalHeaders = null, bool propertyNameCaseInsensitive = false)
        {
            return await PostInternalAsync<T>(connection, url, accessToken, content, additionalHeaders, propertyNameCaseInsensitive);
        }

        public static async Task<T> PutAsync<T>(PnPConnection connection, string url, string accessToken, HttpContent content, IDictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Put, connection, accessToken, content, additionalHeaders);
            var stringContent = await SendMessageAsync(connection, message, accessToken);
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

        public static async Task<T> PostAsync<T>(PnPConnection connection, string url, T content, string accessToken)
        {
            var requestContent = new StringContent(JsonSerializer.Serialize(content, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return await PostInternalAsync<T>(connection, url, accessToken, requestContent);
        }

        public static async Task<T> PostAsync<T>(PnPConnection connection, string url, string accessToken)
        {
            return await PostInternalAsync<T>(connection, url, accessToken, null);
        }

        private static async Task<T> PostInternalAsync<T>(PnPConnection connection, string url, string accessToken, HttpContent content, IDictionary<string, string> additionalHeaders = null, bool propertyNameCaseInsensitive = false)
        {
            var message = GetMessage(url, HttpMethod.Post, connection, accessToken, content, additionalHeaders);
            var stringContent = await SendMessageAsync(connection, message, accessToken);
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

        public static async Task<T> PutAsync<T>(PnPConnection connection, string url, T content, string accessToken)
        {
            var requestContent = new StringContent(JsonSerializer.Serialize(content, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var message = GetMessage(url, HttpMethod.Put, connection, accessToken, requestContent);
            var returnValue = await SendMessageAsync(connection, message, accessToken);
            if (!string.IsNullOrEmpty(returnValue))
            {
                return JsonSerializer.Deserialize<T>(returnValue, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                return default;
            }
        }

        public static async Task<HttpResponseMessage> DeleteAsync(PnPConnection connection, string url, string accessToken)
        {
            var message = GetMessage(url, HttpMethod.Delete, connection, accessToken);
            var response = await GetResponseMessageAsync(connection, message);
            return response;
        }

        private static async Task<string> SendMessageAsync(PnPConnection connection, HttpRequestMessage message, string accessToken)
        {
            var response = await connection.HttpClient.SendAsync(message);
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;
                await Task.Delay(retryAfter.Delta.Value.Seconds * 1000);
                response = await connection.HttpClient.SendAsync(CloneMessage(message));
            }
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var exception = JsonSerializer.Deserialize<GraphException>(errorContent, new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                exception.AccessToken = accessToken;
                exception.HttpResponse = response;
                
                throw exception;
            }
        }

        public static async Task<HttpResponseMessage> GetResponseMessageAsync(PnPConnection connection, HttpRequestMessage message)
        {
            var response = await connection.HttpClient.SendAsync(message);
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;
                await Task.Delay(retryAfter.Delta.Value.Seconds * 1000);
                response = await connection.HttpClient.SendAsync(CloneMessage(message));
            }

            // Validate if the response was successful, if not throw an exception
            if (!response.IsSuccessStatusCode)
            {
                if (GraphHelper.TryGetGraphException(response, out GraphException ex))
                {
                    if (ex.Error != null)
                    {
                        throw new PSInvalidOperationException(ex.Error.Message);
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
            HttpRequestMessage clone = new HttpRequestMessage(req.Method, req.RequestUri);

            clone.Content = req.Content;
            clone.Version = req.Version;

#if NETFRAMEWORK
            foreach (KeyValuePair<string, object> prop in req.Properties)
            {
                clone.Properties.Add(prop);
            }
#else
            foreach (KeyValuePair<string, object> prop in req.Options)
            {
                clone.Options.Set(new HttpRequestOptionsKey<object>(prop.Key), prop.Value);
            }
#endif

            foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return clone;
        }
    }
}
