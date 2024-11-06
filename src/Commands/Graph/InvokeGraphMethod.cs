using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.IO;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPGraphMethod", DefaultParameterSetName = ParameterSet_TOCONSOLE)]
    public class InvokeGraphMethod : PnPGraphCmdlet
    {
        private const string ParameterSet_TOSTREAM = "Out to stream";
        private const string ParameterSet_TOFILE = "Out to file";
        private const string ParameterSet_TOCONSOLE = "Out to console";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        public HttpRequestMethod Method = HttpRequestMethod.Get;

        private string _url;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_TOSTREAM)]
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
        public object Content;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOSTREAM)]
        public string ContentType = "application/json";

        
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOSTREAM)]
        public GraphAdditionalHeadersPipeBind AdditionalHeaders = new GraphAdditionalHeadersPipeBind(new Dictionary<string, string>());
        // public IDictionary<string, string> AdditionalHeaders
        // {
        //     get
        //     {
        //         if (ConsistencyLevelEventual.IsPresent)
        //         {
        //             if (additionalHeaders == null)
        //             {
        //                 additionalHeaders = new Dictionary<string, string>();
        //             }
        //             additionalHeaders.Remove("ConsistencyLevel");
        //             additionalHeaders.Add("ConsistencyLevel", "eventual");
        //         }
        //         return additionalHeaders;
        //     }
        //     set
        //     {
        //         additionalHeaders = value;
        //     }
        // }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOSTREAM)]
        public SwitchParameter ConsistencyLevelEventual;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        public SwitchParameter Raw;      

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TOCONSOLE)]
        public SwitchParameter All;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TOFILE)]
        public string OutFile;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TOSTREAM)]
        public SwitchParameter OutStream;        

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
                        if(ParameterSetName == ParameterSet_TOCONSOLE) 
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
                        TokenHandler.EnsureRequiredPermissionsAvailableInAccessTokenAudience(this, gex.AccessToken);
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
            var result = GraphHelper.Get(this, Connection, Url, AccessToken, AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
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
                            result = GraphHelper.Get(this, Connection, nextLink, AccessToken, AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
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
            WriteVerbose($"Sending HTTP GET to {Url}");
            using var response = GraphHelper.GetResponse(this, Connection, Url, AccessToken);
            HandleResponse(response);
        }

        private void PostRequest()
        {
            WriteVerbose($"Sending HTTP POST to {Url}");
            var response = GraphHelper.Post(this, Connection, Url, AccessToken, GetHttpContent(), AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
            HandleResponse(response);
        }

        private void PutRequest()
        {
            WriteVerbose($"Sending HTTP PUT to {Url}");
            var response = GraphHelper.Put(this, Connection, Url, AccessToken, GetHttpContent(), AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
            HandleResponse(response);
        }

        private void PatchRequest()
        {
            WriteVerbose($"Sending HTTP PATCH to {Url}");
            var response = GraphHelper.Patch(this, Connection, AccessToken, GetHttpContent(), Url, AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
            HandleResponse(response);
        }

        private void DeleteRequest()
        {
            WriteVerbose($"Sending HTTP DELETE to {Url}");
            var response = GraphHelper.Delete(this, Connection, Url, AccessToken, AdditionalHeaders?.GetHeaders(ConsistencyLevelEventual.IsPresent));
            HandleResponse(response);
        }

        private void HandleResponse(HttpResponseMessage response)
        {
            switch (ParameterSetName)
            {
                case ParameterSet_TOCONSOLE:
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    WriteVerbose($"Returning {result.Length} characters response");

                    WriteGraphResult(result);
                    break;

                case ParameterSet_TOFILE:
                    using (var responseStreamForFile = response.Content.ReadAsStream())
                    {
                        WriteVerbose($"Writing {responseStreamForFile.Length} bytes response to {OutFile}");

                        using (var fileStream = new FileStream(OutFile, FileMode.Create, FileAccess.Write))
                        {
                            responseStreamForFile.CopyTo(fileStream);
                            fileStream.Close();
                        }
                    }
                    break;

                case ParameterSet_TOSTREAM:
                    var responseStream = response.Content.ReadAsStream();
                    
                    WriteVerbose($"Writing {responseStream.Length} bytes response to outputstream");

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
    }
}