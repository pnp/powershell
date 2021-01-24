using PnP.Framework;
using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace PnP.PowerShell.Commands.Utilities
{
    public static class AzureAuthHelper
    {
        private static string CLIENTID = "1950a258-227b-4e31-a9cf-717495945fc2"; // Well-known Azure Management App Id
        internal static async Task<string> AuthenticateAsync(string tenantId, string username, SecureString password, AzureEnvironment azureEnvironment)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(tenantId)} is required");
            }

            using (var authManager = new PnP.Framework.AuthenticationManager(username, password, azureEnvironment))
            {
                return await authManager.GetAccessTokenAsync(new[] { "https://graph.microsoft.com/.default" });
            }
        }

        internal static string AuthenticateDeviceLogin(string tenantId, CancellationTokenSource cancellationTokenSource, CmdletMessageWriter messageWriter, bool noPopup, AzureEnvironment azureEnvironment)
        {
            try
            {
                using (var authManager = new PnP.Framework.AuthenticationManager(CLIENTID, (result) =>
                {
                    if (Utilities.OperatingSystem.IsWindows() && !noPopup)
                    {
                        ClipboardService.SetText(result.UserCode);
                        messageWriter.WriteMessage($"Please login.\n\nWe opened a browser and navigated to {result.VerificationUrl}\n\nEnter code: {result.UserCode} (we copied this code to your clipboard)\n\nNOTICE: close the popup after you authenticated successfully to continue the process.");
                        BrowserHelper.GetWebBrowserPopup(result.VerificationUrl, "Please login");
                    }
                    else
                    {
                        messageWriter.WriteMessage(result.Message);
                    }
                    return Task.FromResult(0);
                }, azureEnvironment))
                {
                    return authManager.GetAccessTokenAsync(new string[] { "https://graph.microsoft.com/.default" }, cancellationTokenSource.Token).GetAwaiter().GetResult();
                }
            }
            catch (OperationCanceledException)
            {
                cancellationTokenSource.Cancel();
            }
            return null;
        }

        internal static string AuthenticateInteractive(string tenantId, CancellationTokenSource cancellationTokenSource, CmdletMessageWriter messageWriter, bool noPopup, AzureEnvironment azureEnvironment)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(tenantId)} is required");
            }

            try
            {
                using (var authManager = new PnP.Framework.AuthenticationManager(CLIENTID, (url, port) =>
                {
                    BrowserHelper.OpenBrowserForInteractiveLogin(url, port, !noPopup);
                },
                successMessageHtml: $"You successfully authenticated with PnP PowerShell. Feel free to close this {(noPopup ? "tab" : "window")}.",
                failureMessageHtml: $"You did not authenticate with PnP PowerShell. Feel free to close this browser {(noPopup ? "tab" : "window")}."))
                {
                    return authManager.GetAccessTokenAsync(new string[] { "https://graph.microsoft.com/.default" }, cancellationTokenSource.Token).GetAwaiter().GetResult();
                }
            }
            catch (OperationCanceledException)
            {
                cancellationTokenSource.Cancel();
            }
            return null;
        }
    }
}