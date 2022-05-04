﻿
using Newtonsoft.Json.Linq;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using PnP.PowerShell.Commands.Utilities.JSON;
using System;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using PnP.PowerShell.Commands.Model.Graph;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPGraphMethod")]
    public class InvokeGraphMethod : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public HttpRequestMethod Method = HttpRequestMethod.Get;

        private string _url;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url
        {
            get { return _url; }
            set
            {
                var url = value;
                if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    if (url.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                    {
                        url = url.Remove(0, 1);
                    }
                    if (!url.StartsWith("beta/", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("v1.0/", StringComparison.OrdinalIgnoreCase))
                    {
                        url = UrlUtility.Combine("v1.0", url);
                    }
                }
                _url = url;
            }
        }

        [Parameter(Mandatory = false)]
        public object Content;

        [Parameter(Mandatory = false)]
        public string ContentType = "application/json";

        IDictionary<string, string> additionalHeaders = null;

        [Parameter(Mandatory = false)]
        public IDictionary<string, string> AdditionalHeaders
        {
            get
            {
                if (ConsistencyLevelEventual.IsPresent)
                {
                    if (additionalHeaders == null)
                    {
                        additionalHeaders = new Dictionary<string, string>();
                    }
                    additionalHeaders.Remove("ConsistencyLevel");
                    additionalHeaders.Add("ConsistencyLevel", "eventual");
                }
                return additionalHeaders;
            }
            set
            {
                additionalHeaders = value;
            }
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter ConsistencyLevelEventual;

        [Parameter(Mandatory = false)]
        public SwitchParameter Raw;

        [Parameter(Mandatory = false)]
        public SwitchParameter All;

        protected override void ExecuteCmdlet()
        {
            try
            {
                SendRequest();
            }
            catch (Exception ex)
            {
                WriteError(ex, ErrorCategory.WriteError);
            }
        }

        private HttpContent GetHttpContent()
        {
            if (Content != null)
            {
                if (Content is HttpContent)
                {
                    return (HttpContent)Content;
                }
                if (string.IsNullOrEmpty(ContentType))
                {
                    ContentType = "application/json";
                }
                var contentString = Content is string ? Content.ToString() :
                    JsonSerializer.Serialize(Content);

                HttpContent httpContent = new StringContent(contentString, System.Text.Encoding.UTF8);
                httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentType);
                return httpContent;
            }
            return null;
        }

        private void SendRequest()
        {
            try
            {
                switch (Method)
                {
                    case HttpRequestMethod.Get:
                        GetRequest();
                        return;
                    case HttpRequestMethod.Post:
                        PostRequest();
                        return;
                    case HttpRequestMethod.Patch:
                        PatchRequest();
                        return;
                    case HttpRequestMethod.Put:
                        PutRequest();
                        return;
                    case HttpRequestMethod.Delete:
                        DeleteRequest();
                        return;
                }
                throw new NotSupportedException($"method [{Method}] not supported");
            }
            catch (PnP.PowerShell.Commands.Model.Graph.GraphException gex)
            {
                if (gex.Error.Code == "Authorization_RequestDenied")
                {
                    if (!string.IsNullOrEmpty(gex.AccessToken))
                    {
                        TokenHandler.ValidateTokenForPermissions(GetType(), gex.AccessToken);
                    }
                }
                throw new PSInvalidOperationException(gex.Error.Message);
            }
        }

        private void ThrowIfNoSuccess(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var exception = JsonSerializer.Deserialize<GraphException>(errorContent, new JsonSerializerOptions() { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                exception.AccessToken = AccessToken;
                throw exception;
            }
        }

        private object Deserialize(string result)
        {
            var element = JsonSerializer.Deserialize<JsonElement>(result);
            return Deserialize(element);
        }

        private object Deserialize(JsonElement element)
        {
            var obj = Utilities.JSON.Convert.ConvertToObject(element);
            return obj;
        }

        private void WriteGraphResult(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                if (Raw.IsPresent)
                {
                    WriteObject(result);
                }
                else
                {
                    WriteObject(Deserialize(result));
                }
            }
        }

        private void GetRequest()
        {
            var result = GraphHelper.GetAsync(HttpClient, Url, AccessToken, AdditionalHeaders).GetAwaiter().GetResult();
            if (Raw.IsPresent)
            {
                WriteObject(result);
            }
            else
            {
                var element = JsonSerializer.Deserialize<JsonElement>(result);
                var rootObj = Deserialize(element);

                dynamic obj = rootObj;

                if (All.IsPresent && obj != null && obj.value != null && (obj.value is List<object>))
                {
                    List<object> values = obj.value;
                    while (true)
                    {
                        if (element.TryGetProperty("@odata.nextLink", out JsonElement nextLinkProperty))
                        {
                            if (nextLinkProperty.ValueKind != JsonValueKind.String)
                            {
                                break;
                            }
                            var nextLink = nextLinkProperty.ToString();
                            result = GraphHelper.GetAsync(HttpClient, nextLink, AccessToken, AdditionalHeaders).GetAwaiter().GetResult();
                            element = JsonSerializer.Deserialize<JsonElement>(result);
                            dynamic nextObj = Deserialize(element);
                            if (nextObj != null && nextObj.value != null && (nextObj.value is List<object>))
                            {
                                List<object> nextValues = nextObj.value;
                                values.AddRange(nextValues);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                WriteObject(rootObj);
            }
        }

        private void PostRequest()
        {
            var response = GraphHelper.PostAsync(HttpClient, Url, AccessToken, GetHttpContent(), AdditionalHeaders).GetAwaiter().GetResult();
            ThrowIfNoSuccess(response);
            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            WriteGraphResult(result);
        }

        private void PutRequest()
        {
            var response = GraphHelper.PutAsync(HttpClient, Url, AccessToken, GetHttpContent(), AdditionalHeaders).GetAwaiter().GetResult();
            ThrowIfNoSuccess(response);
            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            WriteGraphResult(result);
        }

        private void PatchRequest()
        {
            var response = GraphHelper.PatchAsync(HttpClient, AccessToken, GetHttpContent(), Url, AdditionalHeaders).GetAwaiter().GetResult();
            ThrowIfNoSuccess(response);
            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            WriteGraphResult(result);
        }

        private void DeleteRequest()
        {
            var response = GraphHelper.DeleteAsync(HttpClient, Url, AccessToken, AdditionalHeaders).GetAwaiter().GetResult();
            ThrowIfNoSuccess(response);
            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            WriteGraphResult(result);
        }
    }
}