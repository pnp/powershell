using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Utilities.REST
{
    internal static class RestHelper
    {        
        #region GET
        public static T ExecuteGetRequest<T>(ClientContext context, string url, string select = null, string filter = null, string expand = null, uint? top = null)
        {
            var returnValue = ExecuteGetRequest(context, url, select, filter, expand, top);
#if NETFRAMEWORK
            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
#else
            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
#endif

            var returnObject = JsonSerializer.Deserialize<T>(returnValue, jsonSerializerOptions);
            return returnObject;
        }

        public static string ExecuteGetRequest(ClientContext context, string endPointUrl, string select = null, string filter = null, string expand = null, uint? top = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter={filter}");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (top.HasValue)
            {
                restparams.Add($"$top={top}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }
            var client = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.GetAccessToken());

            var authManager = context.GetContextSettings().AuthenticationManager;
            if (authManager.CookieContainer != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var cookie in authManager.CookieContainer.GetCookies(new Uri(url)))
                {
                    sb.AppendFormat("{0}; ", cookie.ToString());
                }
                client.DefaultRequestHeaders.Add("Cookie", sb.ToString());
            }
            var returnValue = client.GetStringAsync(url).GetAwaiter().GetResult();
            return returnValue;
        }

        public static T ExecutePostRequest<T>(ClientContext context, string url, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = "application/json", uint? top = null)
        {
            HttpContent stringContent = null;
            if (content != null)
            {
                stringContent = new StringContent(content);
                if (contentType != null)
                {
                    stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
                }
            }

            var returnValue = ExecutePostRequestInternal(context, url, stringContent, select, filter, expand, additionalHeaders, top);
#if NETFRAMEWORK
            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
#else
            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
#endif
            return JsonSerializer.Deserialize<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonSerializerOptions);
        }

        public static HttpResponseMessage ExecutePostRequest(ClientContext context, string endPointUrl, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = "application/json", uint? top = null)
        {
            HttpContent stringContent = null;
            if (!string.IsNullOrEmpty(content))
            {
                stringContent = new StringContent(content);
            }
            if (stringContent != null && contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }
            return ExecutePostRequestInternal(context, endPointUrl, stringContent, select, filter, expand, additionalHeaders, top);
        }

        private static HttpResponseMessage ExecutePostRequestInternal(ClientContext context, string endPointUrl, HttpContent content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, uint? top = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter=({filter})");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (top.HasValue)
            {
                restparams.Add($"$top={top}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }

            var client = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.GetAccessToken());
            client.DefaultRequestHeaders.Add("X-RequestDigest", context.GetRequestDigestAsync().GetAwaiter().GetResult());

            if (additionalHeaders != null)
            {
                foreach (var key in additionalHeaders.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
                }
            }
            var returnValue = client.PostAsync(url, content).GetAwaiter().GetResult();
            returnValue.EnsureSuccessStatusCode();
            return returnValue;
        }

        public static async Task<string> GetAsync(HttpClient httpClient, string url, string accessToken, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Get, accessToken, accept);
            return await SendMessageAsync(httpClient, message);
        }

        public static async Task<string> GetAsync(HttpClient httpClient, string url, ClientContext clientContext, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Get, clientContext, accept);
            return await SendMessageAsync(httpClient, message);
        }

        public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, string accessToken, bool camlCasePolicy = true)
        {
            var stringContent = await GetAsync(httpClient, url, accessToken);
            if (stringContent != null)
            {
#if NETFRAMEWORK
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#else
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#endif
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
            return default(T);
        }

        public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, ClientContext clientContext, bool camlCasePolicy = true)
        {
            var stringContent = await GetAsync(httpClient, url, clientContext);
            if (stringContent != null)
            {
#if NETFRAMEWORK
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#else
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#endif
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
            return default(T);
        }

#endregion

#region POST

        public static async Task<string> PostAsync(HttpClient httpClient, string url, string accessToken, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Post, accessToken, accept);
            return await SendMessageAsync(httpClient, message);
        }

        public static async Task<string> PostAsync(HttpClient httpClient, string url, ClientContext clientContext, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Post, clientContext, accept);
            return await SendMessageAsync(httpClient, message);
        }

        public static async Task<string> PostAsync(HttpClient httpClient, string url, string accessToken, object payload, string accept = "application/json")
        {
            HttpRequestMessage message = null;
            if (payload != null)
            {
#if NETFRAMEWORK
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#else
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#endif
                var content = new StringContent(JsonSerializer.Serialize(payload, options));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                message = GetMessage(url, HttpMethod.Post, accessToken, accept, content);
            }
            else
            {
                message = GetMessage(url, HttpMethod.Post, accessToken, accept);
            }
            return await SendMessageAsync(httpClient, message);
        }

        public static async Task<string> PostAsync(HttpClient httpClient, string url, ClientContext clientContext, object payload, string accept = "application/json")
        {
            HttpRequestMessage message = null;
            if (payload != null)
            {
#if NETFRAMEWORK
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#else
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#endif
                var content = new StringContent(JsonSerializer.Serialize(payload, options));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                message = GetMessage(url, HttpMethod.Post, clientContext, accept, content);
            }
            else
            {
                message = GetMessage(url, HttpMethod.Post, clientContext, accept);
            }
            return await SendMessageAsync(httpClient, message);
        }

        public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, string accessToken, object payload, bool camlCasePolicy = true)
        {
            var stringContent = await PostAsync(httpClient, url, accessToken, payload);
            if (stringContent != null)
            {
#if NETFRAMEWORK
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#else
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#endif                
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
            return default(T);
        }



        public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, ClientContext clientContext, object payload, bool camlCasePolicy = true)
        {
            var stringContent = await PostAsync(httpClient, url, clientContext, payload);
            if (stringContent != null)
            {
#if NETFRAMEWORK
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#else
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#endif                
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
            return default(T);
        }


#endregion

#region PATCH
        public static async Task<T> PatchAsync<T>(HttpClient httpClient, string url, string accessToken, object payload, bool camlCasePolicy = true)
        {
            var stringContent = await PatchAsync(httpClient, url, accessToken, payload);
            if (stringContent != null)
            {
#if NETFRAMEWORK
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#else
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#endif                
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
            return default(T);
        }

        public static async Task<string> PatchAsync(HttpClient httpClient, string url, string accessToken, object payload, string accept = "application/json")
        {
            HttpRequestMessage message = null;
            if (payload != null)
            {
#if NETFRAMEWORK
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#else
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#endif
                var content = new StringContent(JsonSerializer.Serialize(payload, options));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
#if NETFRAMEWORK
                message= GetMessage(url, new HttpMethod("PATCH"), accessToken, accept, content);
#else
                message = GetMessage(url, HttpMethod.Patch, accessToken, accept, content);
#endif
            }
            else
            {
#if NETFRAMEWORK
                message = GetMessage(url, new HttpMethod("PATCH"), accessToken, accept);
#else
                message = GetMessage(url, HttpMethod.Patch, accessToken, accept);
#endif
            }
            return await SendMessageAsync(httpClient, message);
        }
#endregion

#region PUT
        public static T ExecutePutRequest<T>(ClientContext context, string url, string content, string select = null, string filter = null, string expand = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }

            var returnValue = ExecutePutRequestInternal(context, url, stringContent, select, filter, expand);
#if NETFRAMEWORK
            var options = new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
#else
            var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
#endif
            return JsonSerializer.Deserialize<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult(), options);
        }

        public static HttpResponseMessage ExecutePutRequest(ClientContext context, string endPointUrl, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }
            return ExecutePutRequestInternal(context, endPointUrl, stringContent, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecutePutRequestInternal(ClientContext context, string endPointUrl, HttpContent content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter=({filter})");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }

            var client = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.GetAccessToken());
            client.DefaultRequestHeaders.Add("X-RequestDigest", context.GetRequestDigestAsync().GetAwaiter().GetResult());

            if (additionalHeaders != null)
            {
                foreach (var key in additionalHeaders.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
                }
            }
            var returnValue = client.PutAsync(url, content).GetAwaiter().GetResult();
            return returnValue;
        }
#endregion

#region MERGE
        public static T ExecuteMergeRequest<T>(ClientContext context, string url, string content, string select = null, string filter = null, string expand = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }

            var returnValue = ExecuteMergeRequestInternal(context, url, stringContent, select, filter, expand);
#if NETFRAMEWORK
            var options = new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase  };
#else
            var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
#endif
            return JsonSerializer.Deserialize<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult(), options);
        }

        public static HttpResponseMessage ExecuteMergeRequest(ClientContext context, string endPointUrl, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }
            return ExecuteMergeRequestInternal(context, endPointUrl, stringContent, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecuteMergeRequestInternal(ClientContext context, string endPointUrl, HttpContent content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter=({filter})");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }

            var client = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.GetAccessToken());
            client.DefaultRequestHeaders.Add("IF-MATCH", "*");
            client.DefaultRequestHeaders.Add("X-RequestDigest", context.GetRequestDigestAsync().GetAwaiter().GetResult());
            client.DefaultRequestHeaders.Add("X-HTTP-Method", "MERGE");
            if (additionalHeaders != null)
            {
                foreach (var key in additionalHeaders.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
                }
            }
            var returnValue = client.PostAsync(url, content).GetAwaiter().GetResult();
            return returnValue;
        }
#endregion

#region DELETE

        public static async Task<string> DeleteAsync(HttpClient httpClient, string url, string accessToken, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Delete, accessToken, accept);
            return await SendMessageAsync(httpClient, message);
        }

        public static async Task<T> DeleteAsync<T>(HttpClient httpClient, string url, string accessToken, bool camlCasePolicy = true)
        {
            var stringContent = await DeleteAsync(httpClient, url, accessToken);
            if (stringContent != null)
            {
#if NETFRAMEWORK
                var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#else
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#endif                
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
            return default(T);
        }

        public static HttpResponseMessage ExecuteDeleteRequest(ClientContext context, string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            return ExecuteDeleteRequestInternal(context, endPointUrl, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecuteDeleteRequestInternal(ClientContext context, string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter=({filter})");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }

            var client = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.GetAccessToken());
            client.DefaultRequestHeaders.Add("X-RequestDigest", context.GetRequestDigestAsync().GetAwaiter().GetResult());
            client.DefaultRequestHeaders.Add("X-HTTP-Method", "DELETE");
            if (additionalHeaders != null)
            {
                foreach (var key in additionalHeaders.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
                }
            }
            var returnValue = client.DeleteAsync(url).GetAwaiter().GetResult();
            return returnValue;
        }
#endregion

        private static HttpRequestMessage GetMessage(string url, HttpMethod method, string accessToken, string accept = "application/json", HttpContent content = null)
        {
            if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            var message = new HttpRequestMessage();
            message.Method = method;
            message.RequestUri = new Uri(url);
            if (!string.IsNullOrEmpty(accessToken))
            {
                message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(accept));
            if (method == HttpMethod.Post || method == HttpMethod.Put || method.Method == "PATCH")
            {
                message.Content = content;
            }

            return message;
        }

        private static HttpRequestMessage GetMessage(string url, HttpMethod method, ClientContext clientContext, string accept = "application/json", HttpContent content = null)
        {
            if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            var message = new HttpRequestMessage();
            message.Method = method;
            message.RequestUri = new Uri(url);
            message.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(accept));
            PnP.Framework.Http.PnPHttpClient.AuthenticateRequestAsync(message, clientContext).GetAwaiter().GetResult();
            if (method == HttpMethod.Post || method == HttpMethod.Put || method.Method == "PATCH")
            {
                message.Content = content;
            }

            return message;
        }

        private static async Task<string> SendMessageAsync(HttpClient httpClient, HttpRequestMessage message)
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
                throw new HttpRequestException(errorContent);
            }
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