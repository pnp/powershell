using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.SharePoint.Client;
using PnP.Framework;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class BrowserHelper
    {

#pragma warning disable CS0169,CA1823
        // not required when compiling for .NET Framework
        private static ConcurrentDictionary<string, (string requestDigest, DateTime expiresOn)> requestDigestInfos = new ConcurrentDictionary<string, (string requestDigest, DateTime expiresOn)>();
#pragma warning restore CS0169,CA1823

        internal static ClientContext GetWebLoginClientContext(string siteUrl, bool clearCookies, bool scriptErrorsSuppressed = true, Uri loginRequestUri = null, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
#if Windows

            if (OperatingSystem.IsWindows())
            {
                var authCookiesContainer = new CookieContainer();
                var siteUri = new Uri(siteUrl);
                var cookieUrl = $"{siteUri.Scheme}://{siteUri.Host}";
                var thread = new Thread(() =>
                {
                    if (clearCookies)
                    {
                        CookieReader.SetCookie(cookieUrl, "FedAuth", "ignore;expires=Mon, 01 Jan 0001 00:00:00 GMT");
                        CookieReader.SetCookie(cookieUrl, "rtFa", "ignore;expires=Mon, 01 Jan 0001 00:00:00 GMT");
                        CookieReader.SetCookie(cookieUrl, "EdgeAccessCookie", "ignore;expires=Mon, 01 Jan 0001 00:00:00 GMT");
                    }
                    var form = new System.Windows.Forms.Form();

                    var browser = new System.Windows.Forms.WebBrowser
                    {
                        ScriptErrorsSuppressed = scriptErrorsSuppressed,
                        Dock = System.Windows.Forms.DockStyle.Fill
                    };

                    form.SuspendLayout();
                    form.Icon = null;
                    form.Width = 1024;
                    form.Height = 768;
                    form.MinimizeBox = false;
                    form.MaximizeBox = false;
                    form.Text = $"Log in to {siteUrl}";
                    form.Controls.Add(browser);
                    form.ResumeLayout(false);

                    browser.Navigate(loginRequestUri ?? siteUri);

                    browser.Navigated += (sender, args) =>
                    {
                        if ((loginRequestUri ?? siteUri).Host.Equals(args.Url.Host))
                        {
                            var cookieString = CookieReader.GetCookie(siteUrl).Replace("; ", ",").Replace(";", ",");

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
                                // Set the authentication cookies both on the SharePoint Online Admin as well as on the SharePoint Online domains to allow for APIs on both domains to be used
                                //var authCookiesString = string.Join(",", authCookies);
                                //authCookiesContainer.SetCookies(siteUri, authCookiesString);
                                var extension = Framework.AuthenticationManager.GetSharePointDomainSuffix(azureEnvironment);
                                var cookieCollection = new CookieCollection();
                                foreach (var cookie in authCookies)
                                {
                                    var cookieName = cookie.Substring(0, cookie.IndexOf("=")); // cannot use split as there might '=' in the value
                                    var cookieValue = cookie.Substring(cookieName.Length + 1);
                                    cookieCollection.Add(new Cookie(cookieName, cookieValue));
                                }
                                authCookiesContainer.Add(new Uri(cookieUrl), cookieCollection);
                                var adminSiteUri = new Uri(siteUri.Scheme + "://" + siteUri.Authority.Replace($".sharepoint.{extension}", $"-admin.sharepoint.{extension}"));
                                authCookiesContainer.Add(adminSiteUri, cookieCollection);
                                form.Close();
                            }
                        }
                    };

                    form.Focus();
                    form.ShowDialog();
                    browser.Dispose();
                });

                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();

                if (authCookiesContainer.Count > 0)
                {
                    var ctx = new ClientContext(siteUrl);
                    ctx.DisableReturnValueCache = true;
#if !NETFRAMEWORK
                    // We only have to add a request digest when running in dotnet core
                    var requestDigestInfo = GetRequestDigestAsync(siteUrl, authCookiesContainer).GetAwaiter().GetResult();
                    requestDigestInfos.AddOrUpdate(siteUrl, requestDigestInfo, (key, oldValue) => requestDigestInfo);

                    //expiresOn = requestDigestInfo.expiresOn;
#endif
                    ctx.ExecutingWebRequest += (sender, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.CookieContainer = authCookiesContainer;
#if !NETFRAMEWORK
                        var hostUrl = $"https://{e.WebRequestExecutor.WebRequest.Host}";
                        var requestUri = e.WebRequestExecutor.WebRequest.RequestUri;
                        if (requestUri.LocalPath.Contains("/sites/") || requestUri.LocalPath.Contains("/teams/"))
                        {
                            var managedPath = requestUri.LocalPath.Substring(0, requestUri.LocalPath.IndexOf('/', 2));
                            var siteName = requestUri.LocalPath.Substring(managedPath.Length + 1);
                            siteName = siteName.Substring(0, siteName.IndexOf('/'));
                            hostUrl = $"{hostUrl}{managedPath}/{siteName}";
                        }
                        if (requestDigestInfos.TryGetValue(hostUrl, out requestDigestInfo))
                        {
                            // We only have to add a request digest when running in dotnet core
                            if (DateTime.Now > requestDigestInfo.expiresOn)
                            {
                                requestDigestInfo = GetRequestDigestAsync(hostUrl, authCookiesContainer).GetAwaiter().GetResult();
                                requestDigestInfos.AddOrUpdate(hostUrl, requestDigestInfo, (key, oldValue) => requestDigestInfo);
                            }
                            e.WebRequestExecutor.WebRequest.Headers.Add("X-RequestDigest", requestDigestInfo.digestToken);
                        }
                        else
                        {
                            // admin url maybe?
                            requestDigestInfo = GetRequestDigestAsync(hostUrl, authCookiesContainer).GetAwaiter().GetResult();
                            requestDigestInfos.AddOrUpdate(hostUrl, requestDigestInfo, (key, oldValue) => requestDigestInfo);
                            e.WebRequestExecutor.WebRequest.Headers.Add("X-RequestDigest", requestDigestInfo.digestToken);
                        }
#endif
                    };

                    var settings = new PnP.Framework.Utilities.Context.ClientContextSettings();
                    settings.Type = PnP.Framework.Utilities.Context.ClientContextType.Cookie;
                    settings.SiteUrl = siteUrl;

                    ctx.AddContextSettings(settings);
                    return ctx;
                }
            }
#endif
            return null;
        }

        internal enum UrlMatchType
        {
            FullMatch,
            EndsWith,
            StartsWith,
            Contains
        }

        internal static bool GetWebBrowserPopup(string siteUrl, string title, (string url, UrlMatchType matchType)[] closeUrls = null, bool noThreadJoin = false)
        {
            bool success = false;
#if Windows

            if (OperatingSystem.IsWindows())
            {
                var thread = new Thread(() =>
                {
                    var form = new System.Windows.Forms.Form();

                    var browser = new System.Windows.Forms.WebBrowser
                    {
                        ScriptErrorsSuppressed = true,
                        Dock = System.Windows.Forms.DockStyle.Fill
                    };
                    var assembly = typeof(BrowserHelper).Assembly;
                    form.Icon = null;
                    form.SuspendLayout();
                    form.Width = 1024;
                    form.Height = 768;
                    form.MinimizeBox = false;
                    form.MaximizeBox = false;
                    form.Text = title;
                    form.Controls.Add(browser);
                    form.ResumeLayout(false);

                    browser.Navigate(siteUrl);

                    browser.Navigated += (sender, args) =>
                    {
                        var navigatedUrl = args.Url.ToString();
                        var matched = false;
                        if (null != closeUrls && closeUrls.Length > 0)
                        {
                            foreach (var closeUrl in closeUrls)
                            {
                                switch (closeUrl.matchType)
                                {
                                    case UrlMatchType.FullMatch:
                                        matched = navigatedUrl.Equals(closeUrl.url, StringComparison.OrdinalIgnoreCase);
                                        break;
                                    case UrlMatchType.StartsWith:
                                        matched = navigatedUrl.StartsWith(closeUrl.url, StringComparison.OrdinalIgnoreCase);
                                        break;
                                    case UrlMatchType.EndsWith:
                                        matched = navigatedUrl.EndsWith(closeUrl.url, StringComparison.OrdinalIgnoreCase);
                                        break;
                                    case UrlMatchType.Contains:
#if NETFRAMEWORK
                                        matched = navigatedUrl.Contains(closeUrl.url);
#else
                                        matched = navigatedUrl.Contains(closeUrl.url, StringComparison.OrdinalIgnoreCase);
#endif
                                        break;
                                }
                                if (matched)
                                {
                                    break;
                                }
                            }
                        }
                        if (matched)
                        {
                            form.Close();
                            success = true;
                        }
                    };

                    form.Focus();
                    form.ShowDialog();
                    browser.Dispose();
                });

                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                if (!noThreadJoin)
                {
                    thread.Join();
                }
            }
#endif
            return success;
        }

        private static async Task<(string digestToken, DateTime expiresOn)> GetRequestDigestAsync(string siteUrl, CookieContainer cookieContainer)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.CookieContainer = cookieContainer;
                using (var httpClient = new HttpClient(handler))
                {
                    string responseString = string.Empty;

                    string requestUrl = string.Format("{0}/_api/contextinfo", siteUrl.TrimEnd('/'));
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                    request.Headers.Add("accept", "application/json;odata=nometadata");
                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        var errorSb = new System.Text.StringBuilder();

                        errorSb.AppendLine(await response.Content.ReadAsStringAsync());
                        if (response.Headers.Contains("SPRequestGuid"))
                        {
                            var values = response.Headers.GetValues("SPRequestGuid");
                            if (values != null)
                            {
                                var spRequestGuid = values.FirstOrDefault();
                                errorSb.AppendLine($"ServerErrorTraceCorrelationId: {spRequestGuid}");
                            }
                        }

                        throw new Exception(errorSb.ToString());
                    }

                    var contextInformation = JsonSerializer.Deserialize<JsonElement>(responseString);

                    string formDigestValue = contextInformation.GetProperty("FormDigestValue").GetString();
                    int expiresIn = contextInformation.GetProperty("FormDigestTimeoutSeconds").GetInt32();
                    return (formDigestValue, DateTime.Now.AddSeconds(expiresIn - 30));
                }
            }
        }

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

            public static void SetCookie(string url, string cookiename, string cookiedata)
            {
                NativeMethods.InternetSetCookieEx(url, cookiename, cookiedata, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero);
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

                [DllImport("wininet.dll", EntryPoint = "InternetSetCookieEx", CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern bool InternetSetCookieEx(
                    string url,
                    string cookieName,
                    string cookieData,
                    int flags,
                    IntPtr pReserved);

            }
        }
    }


    internal class DefaultOsBrowserWebUi : ICustomWebUi
    {
        private const string CloseWindowSuccessHtml = @"<html><head><title>Authentication Complete</title><style>body{font-family: sans-serif;}.title{font-size: 1.2em;}.message{font-size: 1.0em;margin-top: 10px;}</style></head><body><div class=""title"">Authentication complete</div><div class=""message"">You can return to the application. Feel free to close this window.</div></body></html>";
        private const string CloseWindowFailureHtml = @"<html><head><title>Authentication Failed</title><style>body{font-family: sans-serif;}.title{font-size: 1.2em;}.message{font-size: 1.0em;margin-top: 10px;}</style></head><body><div class=""title"">Authentication Failed</div><div class=""message"">You can return to the application. Feel free to close this browser tab.</br></br></br></br>Error details: error {0} error_description:{1}</div></body></html>";

        public async Task<Uri> AcquireAuthorizationCodeAsync(
            Uri authorizationUri,
            Uri redirectUri,
            CancellationToken cancellationToken)
        {
            if (!redirectUri.IsLoopback)
            {
                throw new ArgumentException("Only loopback redirect uri is supported with this WebUI. Configure http://localhost or http://localhost:port during app registration. ");
            }

            Uri result = await InterceptAuthorizationUriAsync(
                authorizationUri,
                redirectUri,
                cancellationToken)
                .ConfigureAwait(true);

            return result;
        }

        public static string FindFreeLocalhostRedirectUri()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listener.Start();
                int port = ((IPEndPoint)listener.LocalEndpoint).Port;
                return "http://localhost:" + port;
            }
            finally
            {
                listener?.Stop();
            }
        }

        private static void OpenBrowser(string url, int port)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    BrowserHelper.GetWebBrowserPopup(url, "Please login", new[] { ($"http://localhost:{port}/?code=", BrowserHelper.UrlMatchType.StartsWith) }, noThreadJoin: true);
                }
                else
                {

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    BrowserHelper.GetWebBrowserPopup(url, "Please login");
                    //Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw new PlatformNotSupportedException(RuntimeInformation.OSDescription);
                }
            }
        }

        private async Task<Uri> InterceptAuthorizationUriAsync(
            Uri authorizationUri,
            Uri redirectUri,
            CancellationToken cancellationToken)
        {
            OpenBrowser(authorizationUri.ToString(), redirectUri.Port);
            using (var listener = new SingleMessageTcpListener(redirectUri.Port))
            {
                Uri authCodeUri = null;
                await listener.ListenToSingleRequestAndRespondAsync(
                    (uri) =>
                    {
                        Trace.WriteLine("Intercepted an auth code url: " + uri.ToString());
                        authCodeUri = uri;

                        return GetMessageToShowInBrowserAfterAuth(uri);
                    },
                    cancellationToken)
                .ConfigureAwait(false);

                return authCodeUri;
            }
        }

        private static string GetMessageToShowInBrowserAfterAuth(Uri uri)
        {
#if !NETFRAMEWORK
            // Parse the uri to understand if an error was returned. This is done just to show the user a nice error message in the browser.
            var authCodeQueryKeyValue = System.Web.HttpUtility.ParseQueryString(uri.Query);

            string errorString = authCodeQueryKeyValue.Get("error");
#else
            Dictionary<string, string> dicQueryString = uri.Query.Split('&').ToDictionary(c => c.Split('=')[0], c => Uri.UnescapeDataString(c.Split('=')[1]));
            var errorString = dicQueryString.ContainsKey("error") ? dicQueryString["error"] : null;

#endif
            if (!string.IsNullOrEmpty(errorString))
            {
#if !NETFRAMEWORK
                string errorDescription = authCodeQueryKeyValue.Get("error_description");
#else
                string errorDescription = dicQueryString.ContainsKey("error_description") ? dicQueryString["error_description"] : null;
#endif
                return string.Format(
                    CultureInfo.InvariantCulture,
                    CloseWindowFailureHtml,
                    errorString,
                    errorDescription);
            }

            return CloseWindowSuccessHtml;
        }
    }

    internal class SingleMessageTcpListener : IDisposable
    {
        private readonly int _port;
        private readonly TcpListener _tcpListener;

        public SingleMessageTcpListener(int port)
        {
            if (port < 1 || port == 80)
            {
                throw new ArgumentOutOfRangeException("Expected a valid port number, > 0, not 80");
            }

            _port = port;
            _tcpListener = new TcpListener(IPAddress.Loopback, _port);


        }

        public async Task ListenToSingleRequestAndRespondAsync(
            Func<Uri, string> responseProducer,
            CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _tcpListener.Stop());
            _tcpListener.Start();

            TcpClient tcpClient = null;
            try
            {
                tcpClient =
                    await AcceptTcpClientAsync(cancellationToken)
                    .ConfigureAwait(false);

                await ExtractUriAndRespondAsync(tcpClient, responseProducer, cancellationToken).ConfigureAwait(false);

            }
            finally
            {
                tcpClient?.Close();
            }
        }

        /// <summary>
        /// AcceptTcpClientAsync does not natively support cancellation, so use this wrapper. Make sure
        /// the cancellation token is registered to stop the listener.
        /// </summary>
        /// <remarks>See https://stackoverflow.com/questions/19220957/tcplistener-how-to-stop-listening-while-awaiting-accepttcpclientasync</remarks>
        private async Task<TcpClient> AcceptTcpClientAsync(CancellationToken token)
        {
            try
            {
                return await _tcpListener.AcceptTcpClientAsync().ConfigureAwait(false);
            }
            catch (Exception ex) when (token.IsCancellationRequested)
            {
                throw new OperationCanceledException("Cancellation was requested while awaiting TCP client connection.", ex);
            }
        }

        private async Task ExtractUriAndRespondAsync(
            TcpClient tcpClient,
            Func<Uri, string> responseProducer,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string httpRequest = await GetTcpResponseAsync(tcpClient, cancellationToken).ConfigureAwait(false);
            Uri uri = ExtractUriFromHttpRequest(httpRequest);

            // write an "OK, please close the browser message" 
            await WriteResponseAsync(responseProducer(uri), tcpClient.GetStream(), cancellationToken)
                .ConfigureAwait(false);
        }

#pragma warning disable CS1570 // XML comment has badly formed XML
        /// <summary>
        /// Example TCP response:
        /// 
        /// {GET /?code=OAQABAAIAAAC5una0EUFgTIF8ElaxtWjTl5wse5YHycjcaO_qJukUUexKz660btJtJSiQKz1h4b5DalmXspKis-bS6Inu8lNs4CpoE4FITrLv00Mr3MEYEQzgrn6JiNoIwDFSl4HBzHG8Kjd4Ho65QGUMVNyTjhWyQDf_12E8Gw9sll_sbOU51FIreZlVuvsqIWBMIJ8mfmExZBSckofV6LbcKJTeEZKaqjC09x3k1dpsCNJAtYTQIus5g1DyhAW8viDpWDpQJlT55_0W4rrNKY3CSD5AhKd3Ng4_ePPd7iC6qObfmMBlCcldX688vR2IghV0GoA0qNalzwqP7lov-yf38uVZ3ir6VlDNpbzCoV-drw0zhlMKgSq6LXT7QQYmuA4RVy_7TE9gjQpW-P0_ZXUHirpgdsblaa3JUq4cXpbMU8YCLQm7I2L0oCkBTupYXKLoM2gHSYPJ5HChhj1x0pWXRzXdqbx_TPTujBLsAo4Skr_XiLQ4QPJZpkscmXezpPa5Z87gDenUBRBI9ppROhOksekMbvPataF0qBaM38QzcnzeOCFyih1OjIKsq3GeryChrEtfY9CL9lBZ6alIIQB4thD__Tc24OUmr04hX34PjMyt1Z9Qvr76Pw0r7A52JvqQLWupx8bqok6AyCwqUGfLCPjwylSLA7NYD7vScAbfkOOszfoCC3ff14Dqm3IAB1tUJfCZoab61c6Mozls74c2Ujr3roHw4NdPuo-re5fbpSw5RVu8MffWYwXrO3GdmgcvIMkli2uperucLldNVIp6Pc3MatMYSBeAikuhtaZiZAhhl3uQxzoMhU-MO9WXuG2oIkqSvKjghxi1NUhfTK4-du7I5h1r0lFh9b3h8kvE1WBhAIxLdSAA&state=b380f309-7d24-4793-b938-e4a512b2c7f6&session_state=a442c3cd-a25e-4b88-8b33-36d194ba11b2 HTTP/1.1
        /// Host: localhost:9001
        /// Accept-Language: en-GB,en;q=0.9,en-US;q=0.8,ro;q=0.7,fr;q=0.6
        /// Connection: keep-alive
        /// Upgrade-Insecure-Requests: 1
        /// User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36
        /// Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
        /// Accept-Encoding: gzip, deflate, br
        /// </summary>
        /// <returns>http://localhost:9001/?code=foo&session_state=bar</returns>
        private Uri ExtractUriFromHttpRequest(string httpRequest)
#pragma warning restore CS1570 // XML comment has badly formed XML
        {
            string regexp = @"GET \/\?(.*) HTTP";
            string getQuery = null;
            Regex r1 = new Regex(regexp);
            Match match = r1.Match(httpRequest);
            if (!match.Success)
            {
                throw new InvalidOperationException("Not a GET query");
            }

            getQuery = match.Groups[1].Value;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Query = getQuery;
            uriBuilder.Port = _port;

            return uriBuilder.Uri;
        }

        private static async Task<string> GetTcpResponseAsync(TcpClient client, CancellationToken cancellationToken)
        {
            NetworkStream networkStream = client.GetStream();

            byte[] readBuffer = new byte[1024];
            StringBuilder stringBuilder = new StringBuilder();
            int numberOfBytesRead = 0;

            // Incoming message may be larger than the buffer size. 
            do
            {
                numberOfBytesRead = await networkStream.ReadAsync(readBuffer, 0, readBuffer.Length, cancellationToken)
                    .ConfigureAwait(false);

                string s = Encoding.ASCII.GetString(readBuffer, 0, numberOfBytesRead);
                stringBuilder.Append(s);

            }
            while (networkStream.DataAvailable);

            return stringBuilder.ToString();
        }

        private async Task WriteResponseAsync(
            string message,
            NetworkStream stream,
            CancellationToken cancellationToken)
        {
            string fullResponse = $"HTTP/1.1 200 OK\r\n\r\n{message}";
            var response = Encoding.ASCII.GetBytes(fullResponse);
            await stream.WriteAsync(response, 0, response.Length, cancellationToken).ConfigureAwait(false);
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _tcpListener?.Stop();
        }
    }
}
