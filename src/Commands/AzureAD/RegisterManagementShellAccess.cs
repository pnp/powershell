using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace PnP.PowerShell.Commands.AzureAD
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

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SHOWURL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_REGISTER)]
        public string TenantName;

        protected override void ProcessRecord()
        {
            WriteWarning("This cmdlet will provide consent for the PnP Managment Shell Entra ID multi-tenant app. It is strongly recommmended to register your own app in your Entra ID and use that for authentication instead. See the documentation for Register-PnPEntraIDApp.");
            source = new CancellationTokenSource();
            var messageWriter = new CmdletMessageWriter(this);

            var endPoint = string.Empty;
            using (var authManager = new AuthenticationManager())
            {
                endPoint = authManager.GetAzureADLoginEndPoint(AzureEnvironment);
            }

            if (AzureEnvironment != AzureEnvironment.Production && string.IsNullOrEmpty(TenantName))
            {
                WriteWarning("Please specify the Tenant name for non-commercial clouds, otherwise this operation will fail.");
            }

            var graphEndpoint = $"https://{AuthenticationManager.GetGraphEndPoint(AzureEnvironment)}";
            if (AzureEnvironment == AzureEnvironment.Custom)
            {
                graphEndpoint = Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process);
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
                    azureEnvironment: AzureEnvironment, tenantId: TenantName))
                    {
                        try
                        {
                            authManager.GetAccessTokenAsync(new[] { $"{graphEndpoint}/.default" }, source.Token, Microsoft.Identity.Client.Prompt.Consent).GetAwaiter().GetResult();
                        }
                        catch (Microsoft.Identity.Client.MsalException)
                        {
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(TenantName))
                    {
                        messageWriter.WriteMessage($"Share the following URL with a person that has appropriate access rights on the Azure AD to grant consent for Application Registrations:\n\n{endPoint}/{TenantName}/adminconsent?client_id={PnPConnection.PnPManagementShellClientId}");
                    }
                    else
                    {
                        using (var authManager = AuthenticationManager.CreateWithInteractiveLogin(PnPConnection.AzureManagementShellClientId, (url, port) =>
                        {
                            BrowserHelper.OpenBrowserForInteractiveLogin(url, port, !LaunchBrowser, source);
                        },
                    successMessageHtml: $"You successfully logged in. Feel free to close this window.",
                    failureMessageHtml: $"You failed to login succesfully. Feel free to close this browser window.",
                                        azureEnvironment: AzureEnvironment))
                        {
                            var tenantId = "{M365-Tenant-Id}";
                            var accessToken = string.Empty;
                            try
                            {
                                accessToken = authManager.GetAccessTokenAsync(new[] { $"{graphEndpoint}/.default" }, source.Token).GetAwaiter().GetResult();
                            }
                            catch (Microsoft.Identity.Client.MsalException)
                            {
                            }

                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();
                                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{graphEndpoint}/v1.0/organization"))
                                {
                                    requestMessage.Version = new System.Version(2, 0);
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
                            messageWriter.WriteMessage($"Share the following URL with a person that has appropriate access rights on the Azure AD to grant consent for Application Registrations:\n\n{endPoint}/{tenantId}/adminconsent?client_id={PnPConnection.PnPManagementShellClientId}");
                            if (tenantId == "{M365-Tenant-Id}")
                            {
                                messageWriter.WriteMessage($"To get M365-Tenant-Id value, use the Get-PnPTenantId cmdlet:\nhttps://pnp.github.io/powershell/cmdlets/Get-PnPTenantId.html");
                            }
                        }
                    }
                }
                messageWriter.Finished = true;
            }, source.Token);
            messageWriter.Start();
        }

        protected override void StopProcessing()
        {
            source.Cancel();
        }
    }
}
