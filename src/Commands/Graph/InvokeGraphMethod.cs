using PnP.Core.Model;
using PnP.Core.Services;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPGraphMethod", DefaultParameterSetName = ParameterSet_TOCONSOLE)]
    public class InvokeGraphMethod : PnPGraphCmdlet
    {
        private const string ParameterSet_TOSTREAM = "Out to stream";
        private const string ParameterSet_TOFILE = "Out to file";
        private const string ParameterSet_TOCONSOLE = "Out to console";
        public const string PARAMETERSET_Batch = "Batch";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Batch)]
        public HttpRequestMethod Method = HttpRequestMethod.Get;

        private string _url;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_TOSTREAM)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = PARAMETERSET_Batch)]
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

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOSTREAM)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Batch)]
        public object Content;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOSTREAM)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Batch)]
        public string ContentType = "application/json";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOSTREAM)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Batch)]
        public GraphAdditionalHeadersPipeBind AdditionalHeaders = new(new Dictionary<string, string>());

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOSTREAM)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Batch)]
        public SwitchParameter ConsistencyLevelEventual;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        public SwitchParameter Raw;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        public SwitchParameter All;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TOFILE)]
        public string OutFile;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TOSTREAM)]
        public SwitchParameter OutStream;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Batch)]
        public PnPBatch Batch;

        protected override void ExecuteCmdlet()
        {
            try
            {
                if (ParameterSpecified(nameof(Batch)))
                {
                    CallBatchRequest(new HttpMethod(Method.ToString().ToUpper()), Url);
                }
                else
                {
                    SendRequest();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex);
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
                    JsonSerializer.Serialize(Content, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true });

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
                        if (ParameterSetName == ParameterSet_TOCONSOLE)
                        {
                            GetRequestWithPaging();
                        }
                        else
                        {
                            GetRequestWithoutPaging();
                        }
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
            catch (Model.Graph.GraphException gex)
            {
                if (gex.Error.Code == "Authorization_RequestDenied")
                {
                    if (!string.IsNullOrEmpty(gex.AccessToken))
                    {
                        TokenHandler.EnsureRequiredPermissionsAvailableInAccessTokenAudience(GetType(), gex.AccessToken);
                    }
                }
                throw new PSInvalidOperationException(gex.Error.Message);
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

        private void GetRequestWithPaging()
        {
            var result = this.GraphRequestHelper.Get(Url, additionalHeaders: AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));

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
                            nextLink = nextLink.Replace("https://graph.microsoft.com/v1.0/", "");
                            result = GraphRequestHelper.Get(nextLink, additionalHeaders: AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
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

        private void GetRequestWithoutPaging()
        {
            LogDebug($"Sending HTTP GET to {Url}");
            using var response = this.GraphRequestHelper.GetResponse(Url);
            HandleResponse(response);
        }

        private void PostRequest()
        {
            LogDebug($"Sending HTTP POST to {Url}");
            var response = GraphRequestHelper.PostHttpContent(Url, GetHttpContent(), AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
            HandleResponse(response);
        }

        private void PutRequest()
        {
            LogDebug($"Sending HTTP PUT to {Url}");
            var response = GraphRequestHelper.PutHttpContent(Url, GetHttpContent(), AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
            HandleResponse(response);
        }

        private void PatchRequest()
        {
            LogDebug($"Sending HTTP PATCH to {Url}");
            var response = GraphRequestHelper.Patch(GetHttpContent(), Url, AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
            HandleResponse(response);
        }

        private void DeleteRequest()
        {
            LogDebug($"Sending HTTP DELETE to {Url}");
            var response = GraphRequestHelper.Delete(Url, AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
            HandleResponse(response);
        }

        private void HandleResponse(HttpResponseMessage response)
        {
            switch (ParameterSetName)
            {
                case ParameterSet_TOCONSOLE:
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    LogDebug($"Returning {result.Length} characters response");

                    WriteGraphResult(result);
                    break;

                case ParameterSet_TOFILE:
                    using (var responseStreamForFile = response.Content.ReadAsStream())
                    {
                        LogDebug($"Writing {responseStreamForFile.Length} bytes response to {OutFile}");

                        using (var fileStream = new FileStream(OutFile, FileMode.Create, FileAccess.Write))
                        {
                            responseStreamForFile.CopyTo(fileStream);
                            fileStream.Close();
                        }
                    }
                    break;

                case ParameterSet_TOSTREAM:
                    var responseStream = response.Content.ReadAsStream();

                    LogDebug($"Writing {responseStream.Length} bytes response to outputstream");

                    var memoryStream = new MemoryStream();
                    responseStream.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    responseStream.Close();
                    responseStream.Dispose();

                    WriteObject(memoryStream);
                    break;

                default:
                    throw new Exception($"Parameter set {ParameterSetName} not supported");
            }
        }

        private void CallBatchRequest(HttpMethod method, string requestUrl)
        {
            var web = Connection.PnPContext.Web;
            string contentString = null;
            if (ParameterSpecified(nameof(Content)))
            {
                contentString = Content is string ? Content.ToString() :
                        JsonSerializer.Serialize(Content, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true });

            }

            Dictionary<string, string> extraHeaders = AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent);
            extraHeaders.Add("Content-Type", ContentType);

            ApiRequestType apiRequestType = ApiRequestType.Graph;
            if (requestUrl.IndexOf("/beta/", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                apiRequestType = ApiRequestType.GraphBeta;
            }

            if (requestUrl.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
            {
                if (requestUrl.IndexOf("/v1.0/", StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    requestUrl = requestUrl.Replace($"https://{Connection.GraphEndPoint}/v1.0/", "", StringComparison.InvariantCultureIgnoreCase);
                }
                else if (requestUrl.IndexOf("/beta/", StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    requestUrl = requestUrl.Replace($"https://{Connection.GraphEndPoint}/beta/", "", StringComparison.InvariantCultureIgnoreCase);
                }
            }

            web.WithHeaders(extraHeaders).ExecuteRequestBatch(Batch.Batch, new ApiRequest(method, apiRequestType, requestUrl, contentString));
        }
    }
}