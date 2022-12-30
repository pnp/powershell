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
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

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
                }
                else
                {
                    // Something went wrong...
                    throw new Exception(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }
            }
        }

        private void SetAuthenticationCookies(HttpClientHandler handler, ClientContext context)
        {
            context.Web.EnsureProperty(w => w.Url);
            //if (context.Credentials is SharePointOnlineCredentials spCred)
            //{
            //    handler.Credentials = context.Credentials;
            //    handler.CookieContainer.SetCookies(new Uri(context.Web.Url), spCred.GetAuthenticationCookie(new Uri(context.Web.Url)));
            //}
            //else if (context.Credentials == null)
            //{
            var cookieString = CookieReader.GetCookie(context.Web.Url).Replace("; ", ",").Replace(";", ",");
            var authCookiesContainer = new System.Net.CookieContainer();
            // Get FedAuth and rtFa cookies issued by ADFS when accessing claims aware applications.
            // - or get the EdgeAccessCookie issued by the Web Application Proxy (WAP) when accessing non-claims aware applications (Kerberos).
            IEnumerable<string> authCookies = null;
            if (Regex.IsMatch(cookieString, "FedAuth", RegexOptions.IgnoreCase))
            {
                authCookies = cookieString.Split(',').Where(c => c.StartsWith("FedAuth", StringComparison.InvariantCultureIgnoreCase) || c.StartsWith("rtFa", StringComparison.InvariantCultureIgnoreCase));
            }
            else if (Regex.IsMatch(cookieString, "EdgeAccessCookie", RegexOptions.IgnoreCase))
            {
                authCookies = cookieString.Split(',').Where(c => c.StartsWith("EdgeAccessCookie", StringComparison.InvariantCultureIgnoreCase));
            }
            if (authCookies != null)
            {
                authCookiesContainer.SetCookies(new Uri(context.Web.Url), string.Join(",", authCookies));
            }
            handler.CookieContainer = authCookiesContainer;
            //}
        }
    }

    //Taken from "Remote Authentication in SharePoint Online Using the Client Object Model"
    //https://code.msdn.microsoft.com/Remote-Authentication-in-b7b6f43c

    /// <summary>
    /// WinInet.dll wrapper
    /// </summary>
    internal static class CookieReader
    {
        /// <summary>
        /// Enables the retrieval of cookies that are marked as "HTTPOnly". 
        /// Do not use this flag if you expose a scriptable interface, 
        /// because this has security implications. It is imperative that 
        /// you use this flag only if you can guarantee that you will never 
        /// expose the cookie to third-party code by way of an 
        /// extensibility mechanism you provide. 
        /// Version:  Requires Internet Explorer 8.0 or later.
        /// </summary>
        private const int INTERNET_COOKIE_HTTPONLY = 0x00002000;

        /// <summary>
        /// Returns cookie contents as a string
        /// </summary>
        /// <param name="url">Url to get cookie</param>
        /// <returns>Returns Cookie contents as a string</returns>
        public static string GetCookie(string url)
        {
            int size = 512;
            StringBuilder sb = new StringBuilder(size);
            if (!NativeMethods.InternetGetCookieEx(url, null, sb, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
            {
                if (size < 0)
                {
                    return null;
                }
                sb = new StringBuilder(size);
                if (!NativeMethods.InternetGetCookieEx(url, null, sb, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
                {
                    return null;
                }
            }
            return sb.ToString();
        }

        private static class NativeMethods
        {
            [DllImport("wininet.dll", EntryPoint = "InternetGetCookieEx", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool InternetGetCookieEx(
                string url,
                string cookieName,
                StringBuilder cookieData,
                ref int size,
                int flags,
                IntPtr pReserved);
        }
    }
}