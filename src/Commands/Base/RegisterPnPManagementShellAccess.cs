using Microsoft.Identity.Client;
using PnP.Framework;

using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Diagnostics;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Register, "PnPManagementShellAccess")]
    public class RegisterPnPManagementShellAccess : PSCmdlet
    {
        CancellationTokenSource source;

        [Parameter(Mandatory = false)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        protected override void ProcessRecord()
        {
            source = new CancellationTokenSource();
            var messageWriter = new CmdletMessageWriter(this);
            CancellationToken cancellationToken = source.Token;

            var endPoint = GenericToken.GetAzureADLoginEndPoint(AzureEnvironment);



            Task.Factory.StartNew(() =>
            {

                var deviceCodeApplication = PublicClientApplicationBuilder.Create(PnPConnection.PnPManagementShellClientId).WithAuthority($"{endPoint}/organizations/").WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient").Build();
                deviceCodeApplication.AcquireTokenWithDeviceCode(new[] { "https://graph.microsoft.com/.default" }, codeResult =>
                {
                    if (Utilities.OperatingSystem.IsWindows())
                    {
                        ClipboardService.SetText(codeResult.UserCode);
                        messageWriter.WriteMessage($"Provide consent for the PnP Management Shell application to access SharePoint.\n\nWe opened a browser and navigated to {codeResult.VerificationUrl}\n\nEnter code: {codeResult.UserCode} (we copied this code to your clipboard)");
                        BrowserHelper.GetWebBrowserPopup(codeResult.VerificationUrl, "Provide consent for the PnP Management Shell application", new[] { ("/common/Consent/Set", BrowserHelper.UrlMatchType.EndsWith), ("/common/reprocess?ctx=", BrowserHelper.UrlMatchType.Contains), ("https://login.microsoftonline.com/common/login", BrowserHelper.UrlMatchType.FullMatch) });
                    }
                    else
                    {
                        messageWriter.WriteMessage($"Please provide consent for the PnP Management Shell application by navigating to\n\n{codeResult.VerificationUrl}\n\nEnter code: {codeResult.UserCode}.");
                    }
                    return Task.FromResult(0);
                }).ExecuteAsync(cancellationToken).GetAwaiter().GetResult();
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