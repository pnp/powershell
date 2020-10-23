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
    [Cmdlet(VerbsLifecycle.Register, "ManagementShellAccess")]
    public class RegisterPnPManagementShellAccess : PSCmdlet
    {
        CancellationTokenSource source;

        [Parameter(Mandatory = false)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        protected override void ProcessRecord()
        {
            source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            var endPoint = GenericToken.GetAzureADLoginEndPoint(AzureEnvironment);

            var application = PublicClientApplicationBuilder.Create(PnPConnection.PnPManagementShellClientId).WithAuthority($"{endPoint}/organizations/").WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient").Build();

            application.AcquireTokenWithDeviceCode(new[] { "https://graph.microsoft.com/.default" }, codeResult =>
             {
                 ClipboardService.SetText(codeResult.UserCode);
                 WriteUpdateMessage($"\n\nProvide consent for the PnP Management Shell application to access SharePoint.\n\nWe opened a browser and navigated to {codeResult.VerificationUrl}\n\nEnter code: {codeResult.UserCode} (we copied this code to your clipboard)\n\n");
                 BrowserHelper.LaunchBrowser(codeResult.VerificationUrl);
                 return Task.FromResult("");

             }).ExecuteAsync(token).GetAwaiter().GetResult();
        }

        protected override void StopProcessing()
        {
            source.Cancel();
        }


        private void WriteUpdateMessage(string message)
        {

            if (Host.Name == "ConsoleHost")
            {
                // Use Warning Color
                var notificationColor = "\x1B[7m";
                var resetColor = "\x1B[0m";

                var lineLength = 0;
                foreach (var line in message.Split('\n'))
                {
                    if (line.Length > lineLength)
                    {
                        lineLength = line.Length;
                    }
                }
                var outMessage = string.Empty;
                foreach (var line in message.Split('\n'))
                {
                    var lineToAdd = line.PadRight(lineLength);
                    outMessage += $"{notificationColor} {lineToAdd} {resetColor}\n";
                }
                Host.UI.WriteLine(outMessage);
            }
            else
            {
                WriteWarning(message);
            }
        }
    }
}