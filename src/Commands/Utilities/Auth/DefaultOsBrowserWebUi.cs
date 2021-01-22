using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Extensibility;

namespace PnP.PowerShell.Commands.Utilities.Auth
{
    // Originates from https://github.com/bgavrilMS/msal-interactive-netcore
    internal class DefaultOsBrowserWebUi : ICustomWebUi
    {
        private bool _usePopup;
        public DefaultOsBrowserWebUi(bool usePopup)
        {
            _usePopup = usePopup;
        }

        private const string CloseWindowSuccessHtml = @"<html><head><title>Authentication Complete</title><style>body{{font-family: sans-serif;margin: 0}}.title{{font-size: 1.2em;background-color: darkgreen;padding: 4;color: white}}.message{{font-size: 1.0em;margin: 10}}</style></head><body><div class=""title"">Authentication complete</div><div class=""message"">You successfully authenticated with PnP PowerShell. Feel free to close this {0}.</div></body></html>";
        private const string CloseWindowFailureHtml = @"<html><head><title>Authentication Failed</title><style>body{{font-family: sans-serif;margin: 0}}.title{{font-size: 1.2em;background-color: darkred;padding: 4;color: white}}.message{{font-size: 1.0em;margin: 10;}}</style></head><body><div class=""title"">Authentication Failed</div><div class=""message"">You did not authenticate with PnP PowerShell. Feel free to close this browser {2}.</br></br></br></br>Error details: error {0} error_description:{1}</div></body></html>";

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

        private void OpenBrowser(string url, int port)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && _usePopup)
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
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
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

        private string GetMessageToShowInBrowserAfterAuth(Uri uri)
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
                    errorDescription,
                    _usePopup ? "window":"tab");
            }

            return string.Format(CloseWindowSuccessHtml, _usePopup ? "window" : "tab");
        }
    }
}