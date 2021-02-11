using PnP.Framework;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Register, "PnPManagementShellAccess")]
    public class RegisterManagementShellAccess : PSCmdlet
    {
        private const string ParameterSet_REGISTER = "Register access";
        private const string ParameterSet_SHOWURL = "Show Consent Url";

        CancellationTokenSource source;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_REGISTER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SHOWURL)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_REGISTER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SHOWURL)]
        public SwitchParameter LaunchBrowser;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SHOWURL)]
        public SwitchParameter ShowConsentUrl;

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
                if (ParameterSetName == ParameterSet_REGISTER)
                {
                    using (var authManager = AuthenticationManager.CreateWithInteractiveLogin(PnPConnection.PnPManagementShellClientId, (url, port) =>
                     {
                         BrowserHelper.OpenBrowserForInteractiveLogin(url, port, !LaunchBrowser, source);
                     },
                    successMessageHtml: $"You successfully consented the PnP Management Shell Application for use by PnP PowerShell. Feel free to close this window.",
                    failureMessageHtml: $"You did not consent for the PnP Management Shell Application for use by PnP PowerShell. Feel free to close this browser window.",
                    azureEnvironment: AzureEnvironment))
                    {
                        try
                        {
                            authManager.GetAccessTokenAsync(new[] { "https://graph.microsoft.com/.default" }, cancellationToken, Microsoft.Identity.Client.Prompt.Consent).GetAwaiter().GetResult();
                        }
                        catch (Microsoft.Identity.Client.MsalException)
                        {

                        }
                    }
                }
                else
                {
                    using (var authManager = AuthenticationManager.CreateWithInteractiveLogin(PnPConnection.AzureManagementShellClientId, (url, port) =>
                    {
                        BrowserHelper.OpenBrowserForInteractiveLogin(url, port, !LaunchBrowser, source);
                    },
                    successMessageHtml: $"You successfully consented the PnP Management Shell Application for use by PnP PowerShell. Feel free to close this window.",
                    failureMessageHtml: $"You did not consent for the PnP Management Shell Application for use by PnP PowerShell. Feel free to close this browser window.",
                                        azureEnvironment: AzureEnvironment))
                    {
                        var tenantId = "{azure-tenant-id}";
                        var accessToken = string.Empty;
                        try
                        {
                            accessToken = authManager.GetAccessTokenAsync(new[] { "https://graph.microsoft.com/.default" }, cancellationToken).GetAwaiter().GetResult();
                        }
                        catch (Microsoft.Identity.Client.MsalException)
                        {

                        }

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            using (var httpClient = new HttpClient())
                            {
                                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/organization"))
                                {
                                    requestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");
                                    requestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                    var response = httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();
                                    if (response.IsSuccessStatusCode)
                                    {
                                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                        var responseJson = JsonSerializer.Deserialize<JsonElement>(responseContent);
                                        if (responseJson.TryGetProperty("value", out JsonElement valueElement))
                                        {
                                            foreach (var organization in valueElement.EnumerateArray())
                                            {
                                                if (organization.TryGetProperty("id", out JsonElement idElement))
                                                {
                                                    tenantId = idElement.GetString();

                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        messageWriter.WriteMessage($"Share the following URL with a person that has appropriate access rights on the Azure AD to grant consent for Application Registrations:\n\nhttps://login.microsoftonline.com/{tenantId}/adminconsent?client_id={PnPConnection.PnPManagementShellClientId}");
                    }
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