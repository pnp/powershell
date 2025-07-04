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
        internal static async Task<string> AuthenticateAsync(string tenantId, string username, SecureString password, AzureEnvironment azureEnvironment, string customGraphEndpoint = "")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(tenantId)} is required");
            }

            using (var authManager = PnP.Framework.AuthenticationManager.CreateWithCredentials(CLIENTID, username, password, azureEnvironment: azureEnvironment))
            {
                var graphEndpoint = $"https://{AuthenticationManager.GetGraphEndPoint(azureEnvironment)}";
                if (azureEnvironment == AzureEnvironment.Custom)
                {
                    graphEndpoint = Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process) ?? customGraphEndpoint;
                }
                return await authManager.GetAccessTokenAsync(new[] { $"{graphEndpoint}/.default" });
            }
        }

        internal static string AuthenticateDeviceLogin(CancellationTokenSource cancellationTokenSource, CmdletMessageWriter messageWriter, AzureEnvironment azureEnvironment, string clientId = "1950a258-227b-4e31-a9cf-717495945fc2", string customGraphEndpoint = "")
        {
            try
            {
                using (var authManager = AuthenticationManager.CreateWithDeviceLogin(CLIENTID, (deviceCodeResult) =>
                {
                    if (PSUtility.IsAzureCloudShell())
                    {
                        messageWriter.LogWarning($"\n\nTo sign in, use a web browser to open the page {deviceCodeResult.VerificationUrl} and enter the code {deviceCodeResult.UserCode} to authenticate.");
                    }
                    else
                    {
                        try
                        {
                            ClipboardService.SetText(deviceCodeResult.UserCode);
                        }
                        catch
                        {
                        }
                        messageWriter.LogWarning($"Please login.\n\nWe opened a browser and navigated to {deviceCodeResult.VerificationUrl}\n\nEnter code: {deviceCodeResult.UserCode} (we copied this code to your clipboard)\n\nNOTICE: close the browser tab after you authenticated successfully to continue the process.");
                        BrowserHelper.OpenBrowserForInteractiveLogin(deviceCodeResult.VerificationUrl, BrowserHelper.FindFreeLocalhostRedirectUri(), cancellationTokenSource);
                    }

                    return Task.FromResult(0);
                }, azureEnvironment))
                {
                    authManager.ClearTokenCache();
                    try
                    {
                        var graphEndpoint = $"https://{AuthenticationManager.GetGraphEndPoint(azureEnvironment)}";
                        if (azureEnvironment == AzureEnvironment.Custom)
                        {
                            graphEndpoint = Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process) ?? customGraphEndpoint;
                        }
                        return authManager.GetAccessTokenAsync(new string[] { $"{graphEndpoint}/.default" }, cancellationTokenSource.Token).GetAwaiter().GetResult();
                    }
                    catch (Microsoft.Identity.Client.MsalException)
                    {
                        return null;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                cancellationTokenSource.Cancel();
            }
            return null;
        }

        internal static string AuthenticateInteractive(CancellationTokenSource cancellationTokenSource, CmdletMessageWriter messageWriter, AzureEnvironment azureEnvironment, string tenantId, string customGraphEndpoint = "")
        {
            var htmlMessageSuccess = "<html lang=en><meta charset=utf-8><title>PnP PowerShell - Sign In</title><meta content=\"width=device-width,initial-scale=1\"name=viewport><style>html{height:100%}.message-container{flex-grow:1;display:flex;align-items:center;justify-content:center;margin:0 30px}body{box-sizing:border-box;min-height:100%;display:flex;flex-direction:column;color:#fff;font-family:\"Segoe UI\",\"Helvetica Neue\",Helvetica,Arial,sans-serif;background-color:#2c2c32;margin:0;padding:15px 30px}.message{font-weight:300;font-size:1.4rem}.branding{background-image:url(data:image/png;base64,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);background-repeat:no-repeat;padding-left:26px;font-size:20px;letter-spacing:-.04rem;height:26px;font-weight:400;color:#fff;background-position:left center;text-decoration:none}</style><a class=branding href=https://pnp.github.io/powershell>PnP PowerShell</a><div class=message-container><div class=message>You are signed in now and can close this page.</div></div>";
            var htmlMessageFailure = "<html lang=en><meta charset=utf-8><title>PnP PowerShell - Sign In</title><meta content=\"width=device-width,initial-scale=1\"name=viewport><style>html{{height:100%}}.error-text{{color:red;font-size:1rem}}.message-container{{flex-grow:1;display:flex;align-items:center;justify-content:center;margin:0 30px}}body{{box-sizing:border-box;min-height:100%;display:flex;flex-direction:column;color:#fff;font-family:\"Segoe UI\",\"Helvetica Neue\",Helvetica,Arial,sans-serif;background-color:#2c2c32;margin:0;padding:15px 30px}}.message{{font-weight:300;font-size:1.4rem}}.branding{{background-image:url(data:image/png;base64,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);background-repeat:no-repeat;padding-left:26px;font-size:20px;letter-spacing:-.04rem;height:26px;font-weight:400;color:#fff;background-position:left center;text-decoration:none}}</style><a class=branding href=https://pnp.github.io/powershell>PnP PowerShell</a><div class=message-container><div class=message>An error occured while signing in: {0}</div></div>";
            try
            {
                using (var authManager = PnP.Framework.AuthenticationManager.CreateWithInteractiveWebBrowserLogin(CLIENTID, (url, port) =>
                {
                    BrowserHelper.OpenBrowserForInteractiveLogin(url, port, cancellationTokenSource);
                },
                tenantId,
                htmlMessageSuccess,
                htmlMessageFailure,
                azureEnvironment)
                )
                {
                    authManager.ClearTokenCache();
                    try
                    {
                        var graphEndpoint = $"https://{AuthenticationManager.GetGraphEndPoint(azureEnvironment)}";
                        if (azureEnvironment == AzureEnvironment.Custom)
                        {
                            graphEndpoint = Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process) ?? customGraphEndpoint;
                        }
                        return authManager.GetAccessTokenAsync(new string[] { $"{graphEndpoint}/.default" }, cancellationTokenSource.Token).GetAwaiter().GetResult();
                    }
                    catch (Microsoft.Identity.Client.MsalException)
                    {
                        return null;
                    }
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