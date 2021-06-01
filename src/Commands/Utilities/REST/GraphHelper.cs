using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities.REST
{
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
                exception = JsonSerializer.Deserialize<GraphException>(content, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return true;
            }
            catch
            {
                exception = null;
                return false;
            }
        }

        private static HttpRequestMessage GetMessage(string url, HttpMethod method, string accessToken, HttpContent content = null, Dictionary<string, string> additionalHeaders = null)
        {
            if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            var message = new HttpRequestMessage();
            message.Method = method;
            message.RequestUri = !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? new Uri($"https://{PnPConnection.Current.GraphEndPoint}/{url}") : new Uri(url);
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

        public static async Task<string> GetAsync(HttpClient httpClient, string url, string accessToken)
        {
            var message = GetMessage(url, HttpMethod.Get, accessToken);
            return await SendMessageAsync(httpClient, message, accessToken);
        }

        public static async Task<HttpResponseMessage> GetResponseAsync(HttpClient httpClient, string url, string accessToken)
        {
            var message = GetMessage(url, HttpMethod.Get, accessToken);
            return await GetResponseMessageAsync(httpClient, message);
        }

        public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, string accessToken, bool camlCasePolicy = true, bool propertyNameCaseInsensitive = false)
        {
            var stringContent = await GetAsync(httpClient, url, accessToken);
            if (stringContent != null)
            {
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
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
                    return JsonSerializer.Deserialize<T>(stringContent, options);
                }
                catch (Exception)
                {
                    return default(T);
                }
            }
            return default(T);
        }

        public static async Task<HttpResponseMessage> PostAsync(HttpClient httpClient, string url, string accessToken, HttpContent content)
        {
            var message = GetMessage(url, HttpMethod.Post, accessToken, content);
            return await GetResponseMessageAsync(httpClient, message);
        }

        public static async Task<HttpResponseMessage> PutAsync(HttpClient httpClient, string url, string accessToken, HttpContent content)
        {
            var message = GetMessage(url, HttpMethod.Put, accessToken, content);
            return await GetResponseMessageAsync(httpClient, message);
        }

        #region DELETE
        public static async Task<HttpResponseMessage> DeleteAsync(HttpClient httpClient, string url, string accessToken, Dictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Delete, accessToken, null, additionalHeaders);
            return await GetResponseMessageAsync(httpClient, message);
        }

        public static async Task<T> DeleteAsync<T>(HttpClient httpClient, string url, string accessToken, bool camlCasePolicy = true, Dictionary<string, string> additionalHeaders = null)
        {
            var response = await DeleteAsync(httpClient, url, accessToken, additionalHeaders);
            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();

                if (stringContent != null)
                {
                    var options = new JsonSerializerOptions() { IgnoreNullValues = true };
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
        public static async Task<T> PatchAsync<T>(HttpClient httpClient, string accessToken, string url, T content, Dictionary<string, string> additionalHeaders = null, bool camlCasePolicy = true)
        {
            var serializerSettings = new JsonSerializerOptions() { IgnoreNullValues = true };
            if (camlCasePolicy)
            {
                serializerSettings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }
            var requestContent = new StringContent(JsonSerializer.Serialize(content, serializerSettings));
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
#if NETFRAMEWORK
            var message = GetMessage(url,  new HttpMethod("PATCH"), accessToken, requestContent, additionalHeaders);
#else
            var message = GetMessage(url, HttpMethod.Patch, accessToken, requestContent, additionalHeaders);
#endif
            var returnValue = await SendMessageAsync(httpClient, message, accessToken);
            if (!string.IsNullOrEmpty(returnValue))
            {
                return JsonSerializer.Deserialize<T>(returnValue);
            }
            else
            {
                return default;
            }
        }

        public static async Task<T> PatchAsync<T>(HttpClient httpClient, string accessToken, string url, HttpContent content, Dictionary<string, string> additionalHeaders = null)
        {
#if NETFRAMEWORK
            var message = GetMessage(url, new HttpMethod("PATCH"), accessToken, content, additionalHeaders);
#else
            var message = GetMessage(url, HttpMethod.Patch, accessToken, content, additionalHeaders);
#endif
            var returnValue = await SendMessageAsync(httpClient, message, accessToken);
            if (!string.IsNullOrEmpty(returnValue))
            {
                return JsonSerializer.Deserialize<T>(returnValue);
            }
            else
            {
                return default;
            }
        }

        public static async Task<HttpResponseMessage> PatchAsync(HttpClient httpClient, string accessToken, HttpContent content, string url, Dictionary<string, string> additionalHeaders = null)
        {
#if NETFRAMEWORK
            var message = GetMessage(url, new HttpMethod("PATCH"), accessToken, content, additionalHeaders);
#else
            var message = GetMessage(url, HttpMethod.Patch, accessToken, content, additionalHeaders);
#endif
            return await GetResponseMessageAsync(httpClient, message);
        }



        // public static async Task<T> PatchAsync<T>(HttpClient httpClient, string accessToken, string url, T content,Dictionary<string, string> additionalHeaders = null)
        // {
        //     var requestContent = new StringContent(JsonSerializer.Serialize(content, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        //     requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        //     var message = GetMessage(url, new HttpMethod("PATCH"), accessToken, requestContent, additionalHeaders);
        //     var returnValue = await SendMessageAsync(httpClient, message);
        //     if (!string.IsNullOrEmpty(returnValue))
        //     {
        //         return JsonSerializer.Deserialize<T>(returnValue, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        //     }
        //     else
        //     {
        //         return default;
        //     }
        // }
        #endregion


        public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, HttpContent content, string accessToken, Dictionary<string, string> additionalHeaders = null)
        {
            return await PostInternalAsync<T>(httpClient, url, accessToken, content, additionalHeaders);
        }

        public static async Task<T> PutAsync<T>(HttpClient httpClient, string url, string accessToken, HttpContent content, Dictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Put, accessToken, content, additionalHeaders);
            var stringContent = await SendMessageAsync(httpClient, message, accessToken);
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

        public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, T content, string accessToken)
        {
            var requestContent = new StringContent(JsonSerializer.Serialize(content, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return await PostInternalAsync<T>(httpClient, url, accessToken, requestContent);
        }

        public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, string accessToken)
        {
            return await PostInternalAsync<T>(httpClient, url, accessToken, null);
        }

        private static async Task<T> PostInternalAsync<T>(HttpClient httpClient, string url, string accessToken, HttpContent content, Dictionary<string, string> additionalHeaders = null)
        {
            var message = GetMessage(url, HttpMethod.Post, accessToken, content, additionalHeaders);
            var stringContent = await SendMessageAsync(httpClient, message, accessToken);
            if (stringContent != null)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
                catch
                {
                    return default;
                }
            }
            return default;
        }

        public static async Task<T> PutAsync<T>(HttpClient httpClient, string url, T content, string accessToken)
        {
            var requestContent = new StringContent(JsonSerializer.Serialize(content, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var message = GetMessage(url, HttpMethod.Put, accessToken, requestContent);
            var returnValue = await SendMessageAsync(httpClient, message, accessToken);
            if (!string.IsNullOrEmpty(returnValue))
            {
                return JsonSerializer.Deserialize<T>(returnValue, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                return default;
            }
        }

        public static async Task<HttpResponseMessage> DeleteAsync(HttpClient httpClient, string url, string accessToken)
        {
            var message = GetMessage(url, HttpMethod.Delete, accessToken);
            var response = await GetResponseMessageAsync(httpClient, message);
            return response;
        }

        private static async Task<string> SendMessageAsync(HttpClient httpClient, HttpRequestMessage message, string accessToken)
        {
            var response = await httpClient.SendAsync(message);
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;
                await Task.Delay(retryAfter.Delta.Value.Seconds * 1000);
                response = await httpClient.SendAsync(CloneMessage(message));
            }
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var exception = JsonSerializer.Deserialize<GraphException>(errorContent, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                exception.AccessToken = accessToken;
                throw exception;
            }
        }



        public static async Task<HttpResponseMessage> GetResponseMessageAsync(HttpClient httpClient, HttpRequestMessage message)
        {
            var response = await httpClient.SendAsync(message);
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;
                await Task.Delay(retryAfter.Delta.Value.Seconds * 1000);
                response = await httpClient.SendAsync(CloneMessage(message));
            }
            return response;
        }

        private static HttpRequestMessage CloneMessage(HttpRequestMessage req)
        {
            HttpRequestMessage clone = new HttpRequestMessage(req.Method, req.RequestUri);

            clone.Content = req.Content;
            clone.Version = req.Version;

            foreach (KeyValuePair<string, object> prop in req.Properties)
            {
                clone.Properties.Add(prop);
            }

            foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return clone;
        }
    }
}
