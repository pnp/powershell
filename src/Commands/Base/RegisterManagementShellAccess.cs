using PnP.Framework;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Register, "PnPManagementShellAccess")]
    public class RegisterManagementShellAccess : PSCmdlet
    {
        CancellationTokenSource source;

        [Parameter(Mandatory = false)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        protected override void ProcessRecord()
        {
            source = new CancellationTokenSource();
            var messageWriter = new CmdletMessageWriter(this);
            CancellationToken cancellationToken = source.Token;

            var endPoint = string.Empty;
            using (var authManager = new AuthenticationManager())
            {
                endPoint = authManager.GetAzureADLoginEndPoint(AzureEnvironment);
            }

            Task.Factory.StartNew(() =>
            {
                using(var authManager = new AuthenticationManager(PnPConnection.PnPManagementShellClientId, codeResult =>
                {
                    if (Utilities.OperatingSystem.IsWindows())
                    {
                        ClipboardService.SetText(codeResult.UserCode);
                        messageWriter.WriteMessage($"Provide consent for the PnP Management Shell application to access SharePoint.\n\nWe opened a browser and navigated to {codeResult.VerificationUrl}\n\nEnter code: {codeResult.UserCode} (we copied this code to your clipboard)");
                        BrowserHelper.GetWebBrowserPopup(codeResult.VerificationUrl, "Provide consent for the PnP Management Shell application");
                    }
                    else
                    {
                        messageWriter.WriteMessage($"Please provide consent for the PnP Management Shell application by navigating to\n\n{codeResult.VerificationUrl}\n\nEnter code: {codeResult.UserCode}.");
                    }
                    return Task.FromResult(0);
                }, AzureEnvironment))
                {
                    authManager.GetAccessTokenAsync(new[] { "https://graph.microsoft.com/.default" }, cancellationToken).GetAwaiter().GetResult();
                }
                // var deviceCodeApplication = PublicClientApplicationBuilder.Create(PnPConnection.PnPManagementShellClientId).WithAuthority($"{endPoint}/organizations/").WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient").Build();
                // deviceCodeApplication.AcquireTokenWithDeviceCode(new[] { "https://graph.microsoft.com/.default" }, codeResult =>
                // {
                //     if (Utilities.OperatingSystem.IsWindows())
                //     {
                //         ClipboardService.SetText(codeResult.UserCode);
                //         messageWriter.WriteMessage($"Provide consent for the PnP Management Shell application to access SharePoint.\n\nWe opened a browser and navigated to {codeResult.VerificationUrl}\n\nEnter code: {codeResult.UserCode} (we copied this code to your clipboard)");
                //         BrowserHelper.GetWebBrowserPopup(codeResult.VerificationUrl, "Provide consent for the PnP Management Shell application");
                //     }
                //     else
                //     {
                //         messageWriter.WriteMessage($"Please provide consent for the PnP Management Shell application by navigating to\n\n{codeResult.VerificationUrl}\n\nEnter code: {codeResult.UserCode}.");
                //     }
                //     return Task.FromResult(0);
                // }).ExecuteAsync(cancellationToken).GetAwaiter().GetResult();
                messageWriter.Finished = true;
            }, cancellationToken);
            messageWriter.Start();
        }

        protected override void StopProcessing()
        {
            source.Cancel();
        }
    }
}