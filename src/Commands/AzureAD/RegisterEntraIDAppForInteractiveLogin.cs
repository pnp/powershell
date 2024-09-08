using PnP.Framework;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OperatingSystem = PnP.PowerShell.Commands.Utilities.OperatingSystem;
using PnP.PowerShell.Commands.Base;
using System.Diagnostics;
using System.Dynamic;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsLifecycle.Register, "PnPEntraIDAppForInteractiveLogin")]
    public class RegisterEntraIDAppForInteractiveLogin : BasePSCmdlet, IDynamicParameters
    {
        private CancellationTokenSource cancellationTokenSource;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string ApplicationName;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Tenant;

        [Parameter(Mandatory = false)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        [Parameter(Mandatory = true, ParameterSetName = "DeviceLogin")]
        public SwitchParameter DeviceLogin;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoPopup;

        [Parameter(Mandatory = true, ParameterSetName = ("Interactive"))]
        public SwitchParameter Interactive;

        [Parameter(Mandatory = false)]
        public string LogoFilePath;

        [Parameter(Mandatory = false)]
        public string MicrosoftGraphEndPoint;

        [Parameter(Mandatory = false)]
        public string EntraIDLoginEndPoint;

        protected override void ProcessRecord()
        {
            var redirectUri = "http://localhost";
            // if (ParameterSpecified(nameof(DeviceLogin)) || OperatingSystem.IsMacOS())
            if (ParameterSpecified(nameof(DeviceLogin)) || OperatingSystem.IsMacOS())
            {
                redirectUri = "https://pnp.github.io/powershell/consent.html";
            }

            var messageWriter = new CmdletMessageWriter(this);
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            var loginEndPoint = string.Empty;

            using (var authenticationManager = new AuthenticationManager())
            {
                loginEndPoint = authenticationManager.GetAzureADLoginEndPoint(AzureEnvironment) ?? EntraIDLoginEndPoint;
            }

            var permissionScopes = new PermissionScopes();
            var scopes = new List<PermissionScope>();
            if (this.Scopes != null)
            {
                foreach (var scopeIdentifier in this.Scopes)
                {
                    PermissionScope scope = null;
                    scope = permissionScopes.GetScope(PermissionScopes.ResourceAppId_Graph, scopeIdentifier.Replace("MSGraph.", ""), "Role");
                    if (scope == null)
                    {
                        scope = permissionScopes.GetScope(PermissionScopes.ResourceAppId_SPO, scopeIdentifier.Replace("SPO.", ""), "Role");
                    }
                    if (scope == null)
                    {
                        scope = permissionScopes.GetScope(PermissionScopes.ResourceAppID_O365Management, scopeIdentifier.Replace("O365.", ""), "Role");
                    }
                    if (scope != null)
                    {
                        scopes.Add(scope);
                    }
                }
            }
            else
            {
                if (GraphApplicationPermissions != null)
                {
                    foreach (var scopeIdentifier in this.GraphApplicationPermissions)
                    {
                        scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_Graph, scopeIdentifier, "Role"));
                    }
                }
                if (GraphDelegatePermissions != null)
                {
                    foreach (var scopeIdentifier in this.GraphDelegatePermissions)
                    {
                        scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_Graph, scopeIdentifier, "Scope"));
                    }
                }
                if (SharePointApplicationPermissions != null)
                {
                    foreach (var scopeIdentifier in this.SharePointApplicationPermissions)
                    {
                        scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_SPO, scopeIdentifier, "Role"));
                    }
                }
                if (SharePointDelegatePermissions != null)
                {
                    foreach (var scopeIdentifier in this.SharePointDelegatePermissions)
                    {
                        scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_SPO, scopeIdentifier, "Scope"));
                    }
                }
            }
            if (!scopes.Any())
            {
                messageWriter.WriteWarning("No permissions specified, using default permissions");
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_SPO, "TermStore.ReadWrite.All", "Scope")); // Delegate
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_SPO, "AllSites.FullControl", "Scope")); // Delegate
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_Graph, "Group.ReadWrite.All", "Scope")); // Delegate
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_SPO, "User.ReadWrite.All", "Scope")); // Delegate
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_Graph, "User.ReadWrite.All", "Scope")); // Delegate
            }
            var record = new PSObject();

            string token = GetAuthToken(messageWriter);

            if (!string.IsNullOrEmpty(token))
            {
                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();

                if (!AppExists(ApplicationName, httpClient, token))
                {
                    var azureApp = CreateApp(loginEndPoint, httpClient, token, redirectUri, scopes);

                    record.Properties.Add(new PSVariableProperty(new PSVariable("AzureAppId/ClientId", azureApp.AppId)));

                    StartConsentFlow(loginEndPoint, azureApp, redirectUri, token, httpClient, record, messageWriter, scopes);

                    if (ParameterSpecified(nameof(LogoFilePath)) && !string.IsNullOrEmpty(LogoFilePath))
                    {
                        SetLogo(azureApp, token);
                    }
                }
                else
                {
                    throw new PSInvalidOperationException($"The application with name {ApplicationName} already exists.");
                }

            }
        }

        protected override void StopProcessing()
        {
            cancellationTokenSource.Cancel();
        }

        private static object GetScopesPayload(List<PermissionScope> scopes)
        {
            var resourcePermissions = new List<AppResource>();
            var distinctResources = scopes.GroupBy(s => s.resourceAppId).Select(r => r.First()).ToList();
            foreach (var distinctResource in distinctResources)
            {
                var id = distinctResource.resourceAppId;
                var appResource = new AppResource() { Id = id };
                appResource.ResourceAccess.AddRange(scopes.Where(s => s.resourceAppId == id).ToList());
                resourcePermissions.Add(appResource);
            }
            return resourcePermissions;
        }

        protected IEnumerable<string> Scopes
        {
            get
            {
                if (ParameterSpecified(nameof(Scopes)) && MyInvocation.BoundParameters["Scopes"] != null)
                {
                    return MyInvocation.BoundParameters["Scopes"] as string[];
                }
                else
                {
                    return null;
                }
            }
        }

        protected IEnumerable<string> GraphApplicationPermissions
        {
            get
            {
                if (ParameterSpecified(nameof(GraphApplicationPermissions)) && MyInvocation.BoundParameters[nameof(GraphApplicationPermissions)] != null)
                {
                    return MyInvocation.BoundParameters[nameof(GraphApplicationPermissions)] as string[];
                }
                else
                {
                    return null;
                }
            }
        }

        protected IEnumerable<string> GraphDelegatePermissions
        {
            get
            {
                if (ParameterSpecified(nameof(GraphDelegatePermissions)) && MyInvocation.BoundParameters[nameof(GraphDelegatePermissions)] != null)
                {
                    return MyInvocation.BoundParameters[nameof(GraphDelegatePermissions)] as string[];
                }
                else
                {
                    return null;
                }
            }
        }

        protected IEnumerable<string> SharePointApplicationPermissions
        {
            get
            {
                if (ParameterSpecified(nameof(SharePointApplicationPermissions)) && MyInvocation.BoundParameters[nameof(SharePointApplicationPermissions)] != null)
                {
                    return MyInvocation.BoundParameters[nameof(SharePointApplicationPermissions)] as string[];
                }
                else
                {
                    return null;
                }
            }
        }

        protected IEnumerable<string> SharePointDelegatePermissions
        {
            get
            {
                if (ParameterSpecified(nameof(SharePointDelegatePermissions)) && MyInvocation.BoundParameters[nameof(SharePointDelegatePermissions)] != null)
                {
                    return MyInvocation.BoundParameters[nameof(SharePointDelegatePermissions)] as string[];
                }
                else
                {
                    return null;
                }
            }
        }

        protected IEnumerable<string> O365ManagementApplicationPermissions
        {
            get
            {
                if (ParameterSpecified(nameof(O365ManagementApplicationPermissions)) && MyInvocation.BoundParameters[nameof(O365ManagementApplicationPermissions)] != null)
                {
                    return MyInvocation.BoundParameters[nameof(O365ManagementApplicationPermissions)] as string[];
                }
                else
                {
                    return null;
                }
            }
        }

        protected IEnumerable<string> O365ManagementDelegatePermissions
        {
            get
            {
                if (ParameterSpecified(nameof(O365ManagementDelegatePermissions)) && MyInvocation.BoundParameters[nameof(O365ManagementDelegatePermissions)] != null)
                {
                    return MyInvocation.BoundParameters[nameof(O365ManagementDelegatePermissions)] as string[];
                }
                else
                {
                    return null;
                }
            }
        }

        public object GetDynamicParameters()
        {
            // var classAttribute = this.GetType().GetCustomAttributes(false).FirstOrDefault(a => a is PropertyLoadingAttribute);
            const string parameterName = "Scopes";

            var parameterDictionary = new RuntimeDefinedParameterDictionary();
            var attributeCollection = new System.Collections.ObjectModel.Collection<Attribute>();

            // Scopes
            var parameterAttribute = new ParameterAttribute
            {
                ValueFromPipeline = false,
                ValueFromPipelineByPropertyName = false,
                Mandatory = false
            };

            attributeCollection.Add(parameterAttribute);
            attributeCollection.Add(new ObsoleteAttribute("Use either -GraphApplicationPermissions, -GraphDelegatePermissions, -SharePointApplicationPermissions or -SharePointDelegatePermissions"));

            var identifiers = new PermissionScopes().GetIdentifiers();

            var validateSetAttribute = new ValidateSetAttribute(identifiers);
            attributeCollection.Add(validateSetAttribute);

            var runtimeParameter = new RuntimeDefinedParameter(parameterName, typeof(string[]), attributeCollection);

            parameterDictionary.Add(parameterName, runtimeParameter);

            // Graph
            parameterDictionary.Add("GraphApplicationPermissions", GetParameter("GraphApplicationPermissions", PermissionScopes.ResourceAppId_Graph, "Role"));
            parameterDictionary.Add("GraphDelegatePermissions", GetParameter("GraphDelegatePermissions", PermissionScopes.ResourceAppId_Graph, "Scope"));

            // SharePoint
            parameterDictionary.Add("SharePointApplicationPermissions", GetParameter("SharePointApplicationPermissions", PermissionScopes.ResourceAppId_SPO, "Role"));
            parameterDictionary.Add("SharePointDelegatePermissions", GetParameter("SharePointDelegatePermissions", PermissionScopes.ResourceAppId_SPO, "Scope"));

            // O365 Management
            parameterDictionary.Add("O365ManagementApplicationPermissions", GetParameter("O365ManagementApplicationPermissions", PermissionScopes.ResourceAppID_O365Management, "Role"));
            parameterDictionary.Add("O365ManagementDelegatePermissions", GetParameter("O365ManagementDelegatePermissions", PermissionScopes.ResourceAppID_O365Management, "Scope"));

            return parameterDictionary;
        }

        private RuntimeDefinedParameter GetParameter(string parameterName, string resourceAppId, string type)
        {
            var attributeCollection = new System.Collections.ObjectModel.Collection<Attribute>();
            var parameterAttribute = new ParameterAttribute
            {
                ValueFromPipeline = false,
                ValueFromPipelineByPropertyName = false,
                Mandatory = false
            };
            attributeCollection.Add(parameterAttribute);
            var validateSetAttribute = new ValidateSetAttribute(new PermissionScopes().GetIdentifiers(resourceAppId, type));
            attributeCollection.Add(validateSetAttribute);
            var parameter = new RuntimeDefinedParameter(parameterName, typeof(string[]), attributeCollection);
            return parameter;
        }

        private string GetAuthToken(CmdletMessageWriter messageWriter)
        {
            var token = string.Empty;
            if (DeviceLogin.IsPresent)
            {
                Task.Factory.StartNew(() =>
                {
                    token = AzureAuthHelper.AuthenticateDeviceLogin(cancellationTokenSource, messageWriter, NoPopup, AzureEnvironment, MicrosoftGraphEndPoint);
                    if (token == null)
                    {
                        messageWriter.WriteWarning("Operation cancelled or no token retrieved.");
                    }
                    messageWriter.Stop();
                });
                messageWriter.Start();
            }
            else if (Interactive.IsPresent)
            {
                Task.Factory.StartNew(() =>
                {
                    token = AzureAuthHelper.AuthenticateInteractive(cancellationTokenSource, messageWriter, NoPopup, AzureEnvironment, Tenant, MicrosoftGraphEndPoint);
                    if (token == null)
                    {
                        messageWriter.WriteWarning("Operation cancelled or no token retrieved.");
                    }
                    messageWriter.Stop();
                });
                messageWriter.Start();
            }

            return token;
        }

        private bool AppExists(string appName, HttpClient httpClient, string token)
        {
            Host.UI.Write(ConsoleColor.Yellow, Host.UI.RawUI.BackgroundColor, $"Checking if application '{appName}' does not exist yet...");

            var graphEndpoint = $"https://{AuthenticationManager.GetGraphEndPoint(AzureEnvironment)}";
            if (AzureEnvironment == AzureEnvironment.Custom)
            {
                graphEndpoint = Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process) ?? MicrosoftGraphEndPoint;
            }

            var azureApps = RestHelper.Get<RestResultCollection<AzureADApp>>(httpClient, $"{graphEndpoint}/v1.0/applications?$filter=displayName eq '{appName}'&$select=Id", token);
            if (azureApps != null && azureApps.Items.Any())
            {
                Host.UI.WriteLine();
                return true;
            }
            Host.UI.WriteLine(ConsoleColor.Green, Host.UI.RawUI.BackgroundColor, $"Success. Application '{appName}' can be registered.");
            return false;
        }

        private AzureADApp CreateApp(string loginEndPoint, HttpClient httpClient, string token, string redirectUri, List<PermissionScope> scopes)
        {
            var scopesPayload = GetScopesPayload(scopes);
            var redirectUris = new List<string>() { $"{loginEndPoint}/common/oauth2/nativeclient", redirectUri };
            if (redirectUri != "http://localhost")
            {
                redirectUris.Add("http://localhost");
            }
            dynamic payload = new ExpandoObject();
            payload.isFallbackPublicClient = true;
            payload.displayName = ApplicationName;
            payload.signInAudience = "AzureADMyOrg";
            payload.publicClient = new { redirectUris = redirectUris.ToArray() };
            payload.requiredResourceAccess = scopesPayload;

            var graphEndpoint = $"https://{AuthenticationManager.GetGraphEndPoint(AzureEnvironment)}";
            if (AzureEnvironment == AzureEnvironment.Custom)
            {
                graphEndpoint = Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process) ?? MicrosoftGraphEndPoint;
            }

            var azureApp = RestHelper.Post<AzureADApp>(httpClient, $"{graphEndpoint}/v1.0/applications", token, payload);

            var retry = true;
            var iteration = 0;
            while (retry)
            {
                try
                {
                    // Add redirectURI to support windows broker
                    dynamic redirectUriPayload = new ExpandoObject();
                    redirectUris.Add($"ms-appx-web://microsoft.aad.brokerplugin/{azureApp.AppId}");
                    redirectUriPayload.publicClient = new { redirectUris = redirectUris.ToArray() };
                    RestHelper.Patch(httpClient, $"{graphEndpoint}/v1.0/applications/{azureApp.Id}", token, redirectUriPayload);
                    retry = false;
                }

                catch (Exception)
                {
                    Thread.Sleep(10000);
                    iteration++;
                }

                if (iteration > 3) // don't try more than 3 times
                {
                    retry = false;
                }
            }

            if (azureApp != null)
            {
                Host.UI.WriteLine(ConsoleColor.Yellow, Host.UI.RawUI.BackgroundColor, $"App {azureApp.DisplayName} with id {azureApp.AppId} created.");
            }
            return azureApp;
        }

        private void StartConsentFlow(string loginEndPoint, AzureADApp azureApp, string redirectUri, string token, HttpClient httpClient, PSObject record, CmdletMessageWriter messageWriter, List<PermissionScope> scopes)
        {
            //Host.UI.WriteLine(ConsoleColor.Yellow, Host.UI.RawUI.BackgroundColor, $"Starting consent flow.");

            var graphEndpoint = $"https://{AuthenticationManager.GetGraphEndPoint(AzureEnvironment)}";
            if (AzureEnvironment == AzureEnvironment.Custom)
            {
                graphEndpoint = Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process) ?? MicrosoftGraphEndPoint;
            }

            var resource = scopes.FirstOrDefault(s => s.resourceAppId == PermissionScopes.ResourceAppId_Graph) != null ? $"{graphEndpoint}/.default" : "https://microsoft.sharepoint-df.com/.default";

            var consentUrl = $"{loginEndPoint}/{Tenant}/v2.0/adminconsent?client_id={azureApp.AppId}&scope={resource}&redirect_uri={redirectUri}";

            var waitTime = 30;

            var progressRecord = new ProgressRecord(1, "Please wait...", $"Waiting {waitTime} seconds to update Entra ID and launch consent flow");
            for (var i = 0; i < waitTime; i++)
            {
                progressRecord.PercentComplete = Convert.ToInt32((Convert.ToDouble(i) / Convert.ToDouble(waitTime)) * 100);
                WriteProgress(progressRecord);
                Thread.Sleep(1000);

                // Check if CTRL+C has been pressed and if so, abort the wait
                if (Stopping)
                {
                    Host.UI.WriteLine("Wait cancelled. You can provide consent manually by navigating to");
                    Host.UI.WriteLine(consentUrl);
                    break;
                }
            }
            progressRecord.RecordType = ProgressRecordType.Completed;
            WriteProgress(progressRecord);


            if (OperatingSystem.IsWindows() && !NoPopup)
            {

                if (!Stopping)
                {

                    if (ParameterSpecified(nameof(Interactive)))
                    {
                        using (var authManager = AuthenticationManager.CreateWithInteractiveLogin(azureApp.AppId, (url, port) =>
                         {
                             BrowserHelper.OpenBrowserForInteractiveLogin(url, port, true, cancellationTokenSource);
                         }, Tenant, "You successfully provided consent", "You failed to provide consent.", AzureEnvironment))
                        {
                            authManager.GetAccessToken(resource, Microsoft.Identity.Client.Prompt.Consent);
                        }
                    }
                    else
                    {
                        BrowserHelper.GetWebBrowserPopup(consentUrl, "Please provide consent", new[] { ("https://pnp.github.io/powershell/consent.html", BrowserHelper.UrlMatchType.StartsWith) }, cancellationTokenSource: cancellationTokenSource, cancelOnClose: false);
                    }
                    // Write results

                    WriteObject($"App created. You can now connect to your tenant using Connect-PnPOnline -Url <yourtenanturl> -Interactive -ClientId {azureApp.AppId}");
                    WriteObject(record);
                }
            }
            else
            {
                if (OperatingSystem.IsMacOS())
                {
                    Process.Start("open", consentUrl);
                }
                else
                {
                    Host.UI.WriteLine(ConsoleColor.Yellow, Host.UI.RawUI.BackgroundColor, $"Open the following URL in a browser window to provide consent. This consent is required in order to use this application.\n\n{consentUrl}");
                }
                WriteObject($"App created. You can now connect to your tenant using Connect-PnPOnline -Url <yourtenanturl> -Interactive -ClientId {azureApp.AppId}");
                WriteObject(record);
            }
        }

        private void SetLogo(AzureADApp azureApp, string token)
        {
            if (!Path.IsPathRooted(LogoFilePath))
            {
                LogoFilePath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, LogoFilePath);
            }
            if (File.Exists(LogoFilePath))
            {
                try
                {
                    WriteVerbose("Setting the logo for the EntraID app");

                    var graphEndpoint = $"https://{AuthenticationManager.GetGraphEndPoint(AzureEnvironment)}";
                    if (AzureEnvironment == AzureEnvironment.Custom)
                    {
                        graphEndpoint = Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process) ?? MicrosoftGraphEndPoint;
                    }

                    var endpoint = $"{graphEndpoint}/v1.0/applications/{azureApp.Id}/logo";

                    var bytes = File.ReadAllBytes(LogoFilePath);

                    var fileInfo = new FileInfo(LogoFilePath);

                    var mediaType = string.Empty;
                    switch (fileInfo.Extension.ToLower())
                    {
                        case ".jpg":
                        case ".jpeg":
                            {
                                mediaType = "image/jpeg";
                                break;
                            }
                        case ".gif":
                            {
                                mediaType = "image/gif";
                                break;
                            }
                        case ".png":
                            {
                                mediaType = "image/png";
                                break;
                            }
                    }

                    if (!string.IsNullOrEmpty(mediaType))
                    {
                        var byteArrayContent = new ByteArrayContent(bytes);
                        byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
                        GraphHelper.Put(this, PnPConnection.Current, endpoint, token, byteArrayContent);

                        WriteVerbose("Successfully set the logo for the Entra ID app");
                    }
                    else
                    {
                        throw new Exception("Unrecognized image format. Supported formats are .png, .jpg, .jpeg and .gif");
                    }
                }
                catch (Exception ex)
                {
                    WriteWarning("Something went wrong setting the logo " + ex.Message);
                }
            }
            else
            {
                WriteWarning("Logo File does not exist, ignoring setting the logo");
            }
        }
    }
}