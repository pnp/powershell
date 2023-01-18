using Microsoft.SharePoint.Client;
using PnP.Framework.Http;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPSPRestMethod", DefaultParameterSetName = PARAMETERSET_Parsed)]
    [OutputType(typeof(PSObject), ParameterSetName = new[] { PARAMETERSET_Parsed })]
    [OutputType(typeof(string), ParameterSetName = new[] { PARAMETERSET_Raw })]
    public class InvokeSPRestMethod : PnPSharePointCmdlet
    {
        public const string PARAMETERSET_Parsed = "Parsed";
        public const string PARAMETERSET_Raw = "Raw";

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = PARAMETERSET_Parsed)]
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = PARAMETERSET_Raw)]
        public HttpRequestMethod Method = HttpRequestMethod.Get;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PARAMETERSET_Parsed)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PARAMETERSET_Raw)]
        public string Url;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Parsed)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Raw)]
        public object Content;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Parsed)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Raw)]
        public string ContentType = "application/json";

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Parsed)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Raw)]
        public string Accept = "application/json;odata=nometadata";

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Raw)]
        public SwitchParameter Raw;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Parsed)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_Raw)]
        public string ResponseHeadersVariable;

        protected override void ExecuteCmdlet()
        {
            if (Url.StartsWith("/"))
            {
                // prefix the url with the current web url
                Url = UrlUtility.Combine(ClientContext.Url, Url);
            }

            var method = new HttpMethod(Method.ToString());

            var httpClient = PnPHttpClient.Instance.GetHttpClient(ClientContext);

            var requestUrl = Url;

            bool isResponseHeaderRequired = !string.IsNullOrEmpty(ResponseHeadersVariable);

            using (HttpRequestMessage request = new HttpRequestMessage(method, requestUrl))
            {
                if (string.IsNullOrEmpty(Accept))
                {
                    Accept = "application/json;odata=nometadata";
                }

                request.Headers.Add("accept", Accept);

                if (Method == HttpRequestMethod.Merge)
                {
                    request.Headers.Add("X-HTTP-Method", "MERGE");
                }

                if (Method == HttpRequestMethod.Merge || Method == HttpRequestMethod.Delete)
                {
                    request.Headers.Add("IF-MATCH", "*");
                }
                request.Version = new Version(2, 0);

                PnPHttpClient.AuthenticateRequestAsync(request, ClientContext).GetAwaiter().GetResult();

                if (Method == HttpRequestMethod.Post || Method == HttpRequestMethod.Merge || Method == HttpRequestMethod.Put || Method == HttpRequestMethod.Patch)
                {
                    if (string.IsNullOrEmpty(ContentType))
                    {
                        ContentType = "application/json";
                    }
                    var contentString = Content is string ? Content.ToString() :
                        JsonSerializer.Serialize(Content);
                    request.Content = new StringContent(contentString, System.Text.Encoding.UTF8);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentType);
                }
                HttpResponseMessage response = httpClient.SendAsync(request, new System.Threading.CancellationToken()).Result;
                Dictionary<string, string> responseHeaders = response?.Content?.Headers?.ToDictionary(a => a.Key, a => string.Join(";", a.Value));

                if (response.IsSuccessStatusCode)
                {
                    var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    if (responseString != null)
                    {
                        if (!Raw)
                        {
                            var jsonElement = JsonSerializer.Deserialize<JsonElement>(responseString);

                            string nextLink = string.Empty;
                            if (jsonElement.TryGetProperty("odata.nextLink", out JsonElement nextLinkProperty))
                            {
                                nextLink = nextLinkProperty.ToString();
                            }
                            if (jsonElement.TryGetProperty("value", out JsonElement valueProperty))
                            {
                                var formattedObject = Utilities.JSON.Convert.ConvertToPSObject(valueProperty, "value");
                                if (!string.IsNullOrEmpty(nextLink))
                                {
                                    formattedObject.Properties.Add(new PSNoteProperty("odata.nextLink", nextLink));
                                }
                                WriteObject(formattedObject, true);
                            }
                            else
                            {
                                WriteObject(Utilities.JSON.Convert.ConvertToPSObject(jsonElement, null), true);
                            }
                        }
                        else
                        {
                            WriteObject(responseString);
                        }
                    }
                    if (isResponseHeaderRequired)
                    {
                        SessionState.PSVariable.Set(ResponseHeadersVariable, responseHeaders.ToList());
                    }
                }
                else
                {
                    if (isResponseHeaderRequired)
                    {
                        SessionState.PSVariable.Set(ResponseHeadersVariable, responseHeaders.ToList());
                    }
                    // Something went wrong...
                    throw new Exception(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }
            }
        }
    }
}