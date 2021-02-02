using PnP.Framework;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
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

        [Parameter(Mandatory = false)]
        public SwitchParameter LaunchBrowser;

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
                using(var authManager = AuthenticationManager.CreateWithInteractiveLogin(PnPConnection.PnPManagementShellClientId, (url, port) =>
                {
                    BrowserHelper.OpenBrowserForInteractiveLogin(url, port, !LaunchBrowser);
                },
                successMessageHtml: $"You successfully consented the PnP Management Shell Application for use by PnP PowerShell. Feel free to close this window.",
                failureMessageHtml: $"You did not consent for the PnP Management Shell Application for use by PnP PowerShell. Feel free to close this browser window.",
                azureEnvironment: AzureEnvironment))
                {
                    authManager.GetAccessTokenAsync(new[] {"https://graph.microsoft.com/.default"}, cancellationToken, Microsoft.Identity.Client.Prompt.Consent).GetAwaiter().GetResult();
                }
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