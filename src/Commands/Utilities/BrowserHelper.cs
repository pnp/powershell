﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class BrowserHelper
    {

#pragma warning disable CS0169,CA1823
        // not required when compiling for .NET Framework
        private static ConcurrentDictionary<string, (string requestDigest, DateTime expiresOn)> requestDigestInfos = new ConcurrentDictionary<string, (string requestDigest, DateTime expiresOn)>();
#pragma warning restore CS0169,CA1823      

        internal enum UrlMatchType
        {
            FullMatch,
            EndsWith,
            StartsWith,
            Contains
        }

        //         internal static bool GetWebBrowserPopup(string siteUrl, string title, (string url, UrlMatchType matchType)[] closeUrls = null, bool noThreadJoin = false, CancellationTokenSource cancellationTokenSource = null, bool cancelOnClose = true, bool scriptErrorsSuppressed = true)
        //         {
        //             bool success = false;
        // #if Windows

        //             if (OperatingSystem.IsWindows())
        //             {
        //                 var thread = new Thread(() =>
        //                 {
        //                     var form = new System.Windows.Forms.Form();

        //                     var browser = new System.Windows.Forms.WebBrowser
        //                     {
        //                         ScriptErrorsSuppressed = scriptErrorsSuppressed,
        //                         Dock = System.Windows.Forms.DockStyle.Fill
        //                     };
        //                     var assembly = typeof(BrowserHelper).Assembly;
        //                     form.Icon = new  System.Drawing.Icon(assembly.GetManifestResourceStream("PnP.PowerShell.Commands.Resources.parker.ico"));
        //                     form.SuspendLayout();
        //                     form.Width = 1024;
        //                     form.Height = 768;
        //                     form.MinimizeBox = false;
        //                     form.MaximizeBox = false;
        //                     form.Text = title;
        //                     form.Controls.Add(browser);
        //                     form.ResumeLayout(false);

        //                     form.FormClosed += (a, b) =>
        //                     {
        //                         if (!success && cancelOnClose)
        //                         {
        //                             cancellationTokenSource?.Cancel(false);
        //                         }
        //                     };
        //                     browser.Navigate(siteUrl);

        //                     browser.Navigated += (sender, args) =>
        //                     {
        //                         var navigatedUrl = args.Url.ToString();
        //                         var matched = false;
        //                         if (null != closeUrls && closeUrls.Length > 0)
        //                         {

        //                             foreach (var closeUrl in closeUrls)
        //                             {
        //                                 switch (closeUrl.matchType)
        //                                 {
        //                                     case UrlMatchType.FullMatch:
        //                                         matched = navigatedUrl.Equals(closeUrl.url, StringComparison.OrdinalIgnoreCase);
        //                                         break;
        //                                     case UrlMatchType.StartsWith:
        //                                         matched = navigatedUrl.StartsWith(closeUrl.url, StringComparison.OrdinalIgnoreCase);
        //                                         break;
        //                                     case UrlMatchType.EndsWith:
        //                                         matched = navigatedUrl.EndsWith(closeUrl.url, StringComparison.OrdinalIgnoreCase);
        //                                         break;
        //                                     case UrlMatchType.Contains:

        //                                         matched = navigatedUrl.Contains(closeUrl.url, StringComparison.OrdinalIgnoreCase);
        //                                         break;
        //                                 }
        //                                 if (matched)
        //                                 {
        //                                     break;
        //                                 }
        //                             }
        //                         }
        //                         if (matched)
        //                         {
        //                             success = true;
        //                             form.Close();

        //                         }
        //                     };

        //                     form.Focus();
        //                     form.ShowDialog();
        //                     browser.Dispose();
        //                 });

        //                 thread.SetApartmentState(ApartmentState.STA);
        //                 thread.Start();
        //                 if (!noThreadJoin)
        //                 {
        //                     thread.Join();
        //                 }
        //             }
        // #endif
        //             return success;
        //         }

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
                    request.Version = new Version(2, 0);
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

        internal static void OpenBrowserForInteractiveLogin(string url, int port, CancellationTokenSource cancellationTokenSource)
        {
            // Fixes encoding of scopes and redirect_uri issue on MacOS. It has no negative effects on Windows.
            url = WebUtility.UrlDecode(url);

            try
            {

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);

            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (OperatingSystem.IsWindows())
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (OperatingSystem.IsLinux())
                {
                    Process.Start("xdg-open", url);
                }
                else if (OperatingSystem.IsMacOS())
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw new PlatformNotSupportedException(RuntimeInformation.OSDescription);
                }
            }
        }

        internal static int FindFreeLocalhostRedirectUri()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listener.Start();
                return ((IPEndPoint)listener.LocalEndpoint).Port;
            }
            finally
            {
                listener?.Stop();
            }
        }
    }
}
