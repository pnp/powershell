using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using PnP.Framework;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class BrowserHelper
    {

#pragma warning disable CS0169
        // not required when compiling for .NET Framework
        private static ConcurrentDictionary<string, (string requestDigest, DateTime expiresOn)> requestDigestInfos = new ConcurrentDictionary<string, (string requestDigest, DateTime expiresOn)>();
#pragma warning restore CS0169
        internal static void LaunchBrowser(string url)
        {
            if (OperatingSystem.IsWindows())
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); // Works ok on windows
            }
            else if (OperatingSystem.IsLinux())
            {
                Process.Start("xdg-open", url);  // Works ok on linux
            }
            else if (OperatingSystem.IsMacOS())
            {
                Process.Start("open", url); // Not tested
            }
        }


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
                            var siteName = requestUri.LocalPath.Substring(managedPath.Length + 1, requestUri.LocalPath.IndexOf('/', managedPath.Length) - 1);
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

        internal static bool GetWebBrowserPopup(string siteUrl, string title, (string url, UrlMatchType matchType)[] closeUrls = null)
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
                thread.Join();
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
}
