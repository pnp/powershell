using Microsoft.SharePoint.Client;
using PnP.Framework.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace PnP.PowerShell.Commands.Utilities.REST
{
    internal static class RestHelper
    {
        #region GET
        public static T ExecuteGetRequest<T>(ClientContext context, string url, string select = null, string filter = null, string expand = null, uint? top = null)
        {
            var returnValue = ExecuteGetRequest(context, url, select, filter, expand, top);

            var returnObject = JsonSerializer.Deserialize<T>(returnValue, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return returnObject;
        }

        public static string ExecuteGetRequest(ClientContext context, string endPointUrl, string select = null, string filter = null, string expand = null, uint? top = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new List<string>();
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
            return JsonSerializer.Deserialize<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult(), new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
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
            var restparams = new List<string>();
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

        public static string Get(HttpClient httpClient, string url, string accessToken, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Get, accessToken, accept);
            return SendMessage(httpClient, message);
        }

        public static byte[] GetByteArray(HttpClient httpClient, string url, string accessToken, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Get, accessToken, accept);
            return SendMessageByteArray(httpClient, message);
        }

        public static string Get(HttpClient httpClient, string url, ClientContext clientContext, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Get, clientContext, accept);
            return SendMessage(httpClient, message);
        }

        public static T Get<T>(HttpClient httpClient, string url, string accessToken, bool camlCasePolicy = true)
        {
            var stringContent = Get(httpClient, url, accessToken);
            if (stringContent != null)
            {
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
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

        public static T Get<T>(HttpClient httpClient, string url, ClientContext clientContext, bool camlCasePolicy = true)
        {
            var stringContent = Get(httpClient, url, clientContext);
            if (stringContent != null)
            {
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
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

        public static string Post(HttpClient httpClient, string url, string accessToken, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Post, accessToken, accept);
            return SendMessage(httpClient, message);
        }

        public static string Post(HttpClient httpClient, string url, ClientContext clientContext, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Post, clientContext, accept);
            return SendMessage(httpClient, message);
        }

        public static string Post(HttpClient httpClient, string url, string accessToken, object payload, string accept = "application/json")
        {
            HttpRequestMessage message = null;
            if (payload != null)
            {
                var content = new StringContent(JsonSerializer.Serialize(payload, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                message = GetMessage(url, HttpMethod.Post, accessToken, accept, content);
            }
            else
            {
                message = GetMessage(url, HttpMethod.Post, accessToken, accept);
            }
            return SendMessage(httpClient, message);
        }

        public static string Post(HttpClient httpClient, string url, ClientContext clientContext, object payload, string accept = "application/json")
        {
            HttpRequestMessage message = null;
            if (payload != null)
            {
                var content = new StringContent(JsonSerializer.Serialize(payload, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                message = GetMessage(url, HttpMethod.Post, clientContext, accept, content);
            }
            else
            {
                message = GetMessage(url, HttpMethod.Post, clientContext, accept);
            }
            return SendMessage(httpClient, message);
        }

        public static string Post(HttpClient httpClient, string url, ClientContext clientContext, string payload, string contentType = "application/json", string accept = "application/json")
        {
            HttpRequestMessage message = null;
            if (payload != null)
            {
                var content = new StringContent(payload);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType ?? "application/json");
                message = GetMessage(url, HttpMethod.Post, clientContext, accept, content);
            }
            else
            {
                message = GetMessage(url, HttpMethod.Post, clientContext, accept);
            }
            return SendMessage(httpClient, message);
        }

        public static T Post<T>(HttpClient httpClient, string url, string accessToken, object payload, bool camlCasePolicy = true)
        {
            var stringContent = Post(httpClient, url, accessToken, payload);
            if (stringContent != null)
            {
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
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

        public static System.Net.Http.Headers.HttpResponseHeaders PostGetResponseHeader<T>(HttpClient httpClient, string url, string accessToken, object payload, bool camlCasePolicy = true, string accept = "application/json")
        {
            HttpRequestMessage message = null;
            if (payload != null)
            {
                var content = new StringContent(JsonSerializer.Serialize(payload, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                message = GetMessage(url, HttpMethod.Post, accessToken, accept, content);
            }
            else
            {
                message = GetMessage(url, HttpMethod.Post, accessToken, accept);
            }
            return SendMessageGetResponseHeader(httpClient, message);
        }

        public static T Post<T>(HttpClient httpClient, string url, ClientContext clientContext, object payload, bool camlCasePolicy = true)
        {
            var stringContent = Post(httpClient, url, clientContext, payload);
            if (stringContent != null)
            {
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
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
        public static T Patch<T>(HttpClient httpClient, string url, string accessToken, object payload, bool camlCasePolicy = true)
        {
            var stringContent = Patch(httpClient, url, accessToken, payload);
            if (stringContent != null)
            {
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
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

        public static string Patch(HttpClient httpClient, string url, string accessToken, object payload, string accept = "application/json")
        {
            HttpRequestMessage message = null;
            if (payload != null)
            {
                var content = new StringContent(JsonSerializer.Serialize(payload, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                message = GetMessage(url, HttpMethod.Patch, accessToken, accept, content);
            }
            else
            {
                message = GetMessage(url, HttpMethod.Patch, accessToken, accept);
            }
            return SendMessage(httpClient, message);
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
            return JsonSerializer.Deserialize<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult(), new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
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
            var restparams = new List<string>();
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
            return JsonSerializer.Deserialize<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult(), new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
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
            var restparams = new List<string>();
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

        public static string Delete(HttpClient httpClient, string url, string accessToken, string accept = "application/json")
        {
            var message = GetMessage(url, HttpMethod.Delete, accessToken, accept);
            return SendMessage(httpClient, message);
        }

        public static T Delete<T>(HttpClient httpClient, string url, string accessToken, bool camlCasePolicy = true)
        {
            var stringContent = Delete(httpClient, url, accessToken);
            if (stringContent != null)
            {
                var options = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
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
            var restparams = new List<string>();
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

            var message = new HttpRequestMessage
            {
                Version = new Version(2, 0),
                Method = method,
                RequestUri = new Uri(url)
            };
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

            var message = new HttpRequestMessage
            {
                Version = new Version(2, 0),
                Method = method,
                RequestUri = new Uri(url)
            };
            message.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse(accept));
            PnP.Framework.Http.PnPHttpClient.AuthenticateRequestAsync(message, clientContext).GetAwaiter().GetResult();
            if (method == HttpMethod.Post || method == HttpMethod.Put || method.Method == "PATCH")
            {
                message.Content = content;
            }

            return message;
        }

        private static string SendMessage(HttpClient httpClient, HttpRequestMessage message)
        {
            var response = httpClient.SendAsync(message).GetAwaiter().GetResult();
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;
                Thread.Sleep(retryAfter.Delta.Value.Seconds * 1000);
                response = httpClient.Send(CloneMessage(message));
            }
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            else
            {
                var errorContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                throw new HttpRequestException(errorContent);
            }
        }

        private static byte[] SendMessageByteArray(HttpClient httpClient, HttpRequestMessage message)
        {
            var response = httpClient.Send(message);
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;
                Thread.Sleep(retryAfter.Delta.Value.Seconds * 1000);
                response = httpClient.Send(CloneMessage(message));
            }
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            }
            else
            {
                var errorContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                throw new HttpRequestException($"HTTP Error {response.StatusCode}: {errorContent}");
            }
        }

        private static System.Net.Http.Headers.HttpResponseHeaders SendMessageGetResponseHeader(HttpClient httpClient, HttpRequestMessage message)
        {
            var response = httpClient.SendAsync(message).GetAwaiter().GetResult();
            while (response.StatusCode == (HttpStatusCode)429)
            {
                // throttled
                var retryAfter = response.Headers.RetryAfter;
                Thread.Sleep(retryAfter.Delta.Value.Seconds * 1000);
                response = httpClient.SendAsync(CloneMessage(message)).GetAwaiter().GetResult();
            }
            if (response.IsSuccessStatusCode)
            {
                return response.Headers;
            }
            else
            {
                var errorContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                throw new HttpRequestException(errorContent);
            }
        }

        private static HttpRequestMessage CloneMessage(HttpRequestMessage req)
        {
            HttpRequestMessage clone = new HttpRequestMessage(req.Method, req.RequestUri);

            clone.Content = req.Content;
            clone.Version = req.Version;

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