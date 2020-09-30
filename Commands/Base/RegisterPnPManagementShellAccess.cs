using Microsoft.Identity.Client;
using PnP.Framework;

using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Diagnostics;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Register, "PnPManagementShellAccess")]
    public class RegisterPnPManagementShellAccess : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        [Parameter(Mandatory = true)]
        public string SiteUrl;
        protected override void ProcessRecord()
        {
            var endPoint = GenericToken.GetAzureADLoginEndPoint(AzureEnvironment);
            var uri = new Uri(SiteUrl);
            var scopes = new[] { $"{uri.Scheme}://{uri.Authority}//.default" };

            var application = PublicClientApplicationBuilder.Create(PnPConnection.PnPManagementShellClientId).WithAuthority($"{endPoint}/organizations/").WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient").Build();

            application.AcquireTokenWithDeviceCode(scopes, codeResult =>
             {
                 WriteUpdateMessage($"\n\nProvide consent for the PnP Management Shell application to access SharePoint.\n\nWe opened a browser and navigated to {codeResult.VerificationUrl}\n\nEnter code: {codeResult.UserCode}\n\n");
                 BrowserHelper.LaunchBrowser(codeResult.VerificationUrl);
                 return Task.FromResult("");

             }).ExecuteAsync().GetAwaiter().GetResult();


            application.AcquireTokenWithDeviceCode(new[] { "https://graph.microsoft.com/.default" }, codeResult =>
            {
                WriteUpdateMessage($"\n\nProvide consent for the PnP Management Shell application to access the Microsoft Graph.\n\nWe opened a browser and navigated to {codeResult.VerificationUrl}\n\nEnter code: {codeResult.UserCode}\n\n");
                BrowserHelper.LaunchBrowser(codeResult.VerificationUrl);
                return Task.FromResult("");
            }).ExecuteAsync().GetAwaiter().GetResult();

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