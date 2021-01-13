using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace PnP.PowerShell.Commands.Utilities
{
    public static class AzureAuthHelper
    {
        private static string CLIENTID = "1950a258-227b-4e31-a9cf-717495945fc2"; // Well-known Azure Management App Id
        internal static async Task<string> AuthenticateAsync(string tenantId, string username, SecureString password, string loginEndPoint = "https://login.microsoftonline.com")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(tenantId)} is required");
            }

            var authority = $"{loginEndPoint}/{tenantId}";
            var scopes = new string[] { "https://graph.microsoft.com/.default" };
            var app = PublicClientApplicationBuilder.Create(CLIENTID).WithAuthority(authority).Build();

            var result = await app.AcquireTokenByUsernamePassword(scopes, username, password).ExecuteAsync();
            return result.AccessToken;
        }

        internal static string AuthenticateDeviceLogin(string tenantId, CancellationTokenSource cancellationTokenSource, CmdletMessageWriter messageWriter, bool NoPopup, string loginEndPoint = "https://login.microsoftonline.com")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(tenantId)} is required");
            }
            var authority = $"{loginEndPoint}/{tenantId}";


            var app = PublicClientApplicationBuilder.Create(CLIENTID).WithAuthority(authority).Build();
            var scopes = new string[] { "https://graph.microsoft.com/.default" };

            try
            {
                var tokenResult = app.AcquireTokenWithDeviceCode(scopes, result =>
                {
                    if (Utilities.OperatingSystem.IsWindows() && !NoPopup)
                    {
                        ClipboardService.SetText(result.UserCode);
                        messageWriter.WriteMessage($"Provide consent.\n\nWe opened a browser and navigated to {result.VerificationUrl}\n\nEnter code: {result.UserCode} (we copied this code to your clipboard)\n\nNOTICE: close the popup after you authenticated successfully to continue the process.");
                        BrowserHelper.GetWebBrowserPopup(result.VerificationUrl, "Provide consent");
                    }
                    else
                    {
                        messageWriter.WriteMessage(result.Message);
                    }
                    return Task.FromResult(0);
                }).ExecuteAsync(cancellationTokenSource.Token).GetAwaiter().GetResult();
                return tokenResult.AccessToken;
            }
            catch (OperationCanceledException)
            {
                cancellationTokenSource.Cancel();
            }
            return null;
        }
    }
}