using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;
using OperatingSystem = PnP.PowerShell.Commands.Utilities.OperatingSystem;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsLifecycle.Register, "PnPEntraIDAppForInteractiveLogin", DefaultParameterSetName = "Interactive")]
    [Attributes.ApiPermissionsNotRequired(Remarks = "It authenticates separately instead of using the PnP connection. The account signing in must hold the Application Developer or Global Administrator role to be able to create the application registration.")]
    public class RegisterEntraIDAppForInteractiveLogin : BasePSCmdlet, IDynamicParameters
    {
        /// <summary>
        /// The resources for which a -{Name}ApplicationPermissions and a -{Name}DelegatePermissions parameter is offered.
        /// </summary>
        private static readonly (string Name, string ResourceAppId)[] resources = new[]
        {
            ("Graph", PermissionScopes.ResourceAppId_Graph),
            ("SharePoint", PermissionScopes.ResourceAppId_SPO),
            ("O365Management", PermissionScopes.ResourceAppID_O365Management)
        };

        private static IEnumerable<string> PermissionParameterNames =>
            resources.SelectMany(r => new[] { $"{r.Name}ApplicationPermissions", $"{r.Name}DelegatePermissions" });

        private CancellationTokenSource cancellationTokenSource;

        private readonly PermissionScopes permissionScopes = new PermissionScopes();

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string ApplicationName;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Tenant;

        [Parameter(Mandatory = false)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        [Parameter(Mandatory = true, ParameterSetName = "DeviceLogin")]
        public SwitchParameter DeviceLogin;

        [Parameter(Mandatory = false)]
        public string LogoFilePath;

        [Parameter(Mandatory = false)]
        public string MicrosoftGraphEndPoint;

        [Parameter(Mandatory = false)]
        public string EntraIDLoginEndPoint;

        [Parameter(Mandatory = false)]
        public EntraIDSignInAudience SignInAudience;

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

            var scopes = GetRequestedScopes();

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
                // Deduplicating on id is only correct within a single resource: different resources do reuse permission ids,
                // Microsoft Graph and SharePoint for instance share the id of their User.Read.All and User.ReadWrite.All app roles
                appResource.ResourceAccess.AddRange(scopes.Where(s => s.resourceAppId == id).GroupBy(s => (s.Id, s.Type)).Select(g => g.First()));
                resourcePermissions.Add(appResource);
            }
            return resourcePermissions;
        }

        /// <summary>
        /// Turns the requested permissions into the scopes to register on the app. Every requested permission has to resolve, an unknown
        /// permission is an error rather than something to skip, as silently dropping it would register an app without the access asked for.
        /// </summary>
        private List<PermissionScope> GetRequestedScopes()
        {
            var scopes = new List<PermissionScope>();

            var legacyScopes = GetBoundPermissions("Scopes").ToArray();
            if (legacyScopes.Any())
            {
                var conflicting = PermissionParameterNames.Where(MyInvocation.BoundParameters.ContainsKey).ToArray();
                if (conflicting.Any())
                {
                    throw new PSArgumentException($"-Scopes cannot be combined with {string.Join(", ", conflicting.Select(c => $"-{c}"))}, as only -Scopes would be applied. Use the per resource parameters, -Scopes is obsolete.", "Scopes");
                }
                foreach (var identifier in legacyScopes)
                {
                    scopes.Add(permissionScopes.GetScopeByLegacyIdentifier(identifier)
                        ?? throw new PSArgumentException($"Permission '{identifier}' does not exist.", "Scopes"));
                }
                return scopes;
            }

            foreach (var (name, resourceAppId) in resources)
            {
                AddScopes(scopes, resourceAppId, "Role", GetBoundPermissions($"{name}ApplicationPermissions"));
                AddScopes(scopes, resourceAppId, "Scope", GetBoundPermissions($"{name}DelegatePermissions"));
            }

            if (scopes.Any())
            {
                return scopes;
            }

            LogWarning("No permissions specified, using default permissions");
            AddScopes(scopes, PermissionScopes.ResourceAppId_SPO, "Scope", new[] { "TermStore.ReadWrite.All", "AllSites.FullControl", "User.ReadWrite.All" });
            AddScopes(scopes, PermissionScopes.ResourceAppId_Graph, "Scope", new[] { "Group.ReadWrite.All", "User.ReadWrite.All" });
            return scopes;
        }

        private void AddScopes(List<PermissionScope> scopes, string resourceAppId, string type, IEnumerable<string> identifiers)
        {
            foreach (var identifier in identifiers)
            {
                var scope = permissionScopes.GetScope(resourceAppId, identifier, type);
                scopes.Add(scope ?? throw new PSArgumentException($"Resource {resourceAppId} does not expose {(type == "Role" ? "an application" : "a delegated")} permission named '{identifier}'."));
            }
        }

        private IEnumerable<string> GetBoundPermissions(string parameterName)
        {
            return MyInvocation.BoundParameters.TryGetValue(parameterName, out var value) && value != null
                ? LanguagePrimitives.ConvertTo<string[]>(value)
                : Enumerable.Empty<string>();
        }

        public object GetDynamicParameters()
        {
            var parameterDictionary = new RuntimeDefinedParameterDictionary();

            var attributeCollection = new System.Collections.ObjectModel.Collection<Attribute>
            {
                new ParameterAttribute { ValueFromPipeline = false, ValueFromPipelineByPropertyName = false, Mandatory = false },
                new ObsoleteAttribute("Use either -GraphApplicationPermissions, -GraphDelegatePermissions, -SharePointApplicationPermissions or -SharePointDelegatePermissions"),
                new ValidateSetAttribute(permissionScopes.GetIdentifiers())
            };
            parameterDictionary.Add("Scopes", new RuntimeDefinedParameter("Scopes", typeof(string[]), attributeCollection));

            foreach (var (name, resourceAppId) in resources)
            {
                parameterDictionary.Add($"{name}ApplicationPermissions", GetParameter($"{name}ApplicationPermissions", resourceAppId, "Role"));
                parameterDictionary.Add($"{name}DelegatePermissions", GetParameter($"{name}DelegatePermissions", resourceAppId, "Scope"));
            }

            return parameterDictionary;
        }

        private RuntimeDefinedParameter GetParameter(string parameterName, string resourceAppId, string type)
        {
            var attributeCollection = new System.Collections.ObjectModel.Collection<Attribute>
            {
                new ParameterAttribute { ValueFromPipeline = false, ValueFromPipelineByPropertyName = false, Mandatory = false },
                new ValidateSetAttribute(permissionScopes.GetIdentifiers(resourceAppId, type))
            };
            return new RuntimeDefinedParameter(parameterName, typeof(string[]), attributeCollection);
        }

        private string GetAuthToken(CmdletMessageWriter messageWriter)
        {
            var token = string.Empty;
            if (DeviceLogin.IsPresent)
            {
                Task.Factory.StartNew(() =>
                {
                    token = AzureAuthHelper.AuthenticateDeviceLogin(cancellationTokenSource, messageWriter, AzureEnvironment, MicrosoftGraphEndPoint);
                    if (token == null)
                    {
                        messageWriter.LogWarning("Operation cancelled or no token retrieved.");
                    }
                    messageWriter.Stop();
                });
                messageWriter.Start();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    token = AzureAuthHelper.AuthenticateInteractive(cancellationTokenSource, messageWriter, AzureEnvironment, Tenant, MicrosoftGraphEndPoint);
                    if (token == null)
                    {
                        messageWriter.LogWarning("Operation cancelled or no token retrieved.");
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

            string audience = "AzureADMyOrg";
            if (ParameterSpecified(nameof(SignInAudience)))
            {
                audience = SignInAudience.ToString();
            }

            dynamic payload = new ExpandoObject();
            payload.isFallbackPublicClient = true;
            payload.displayName = ApplicationName;
            payload.signInAudience = audience;
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
            var htmlMessageConsentSuccess = $"<html lang=en><meta charset=utf-8><title>PnP PowerShell - Consent</title><meta content=\"width=device-width,initial-scale=1\"name=viewport><style>html{{height:100%}}.message-container{{flex-grow:1;display:flex;align-items:center;justify-content:center;margin:0 30px}}body{{box-sizing:border-box;min-height:100%;display:flex;flex-direction:column;color:#fff;font-family:\"Segoe UI\",\"Helvetica Neue\",Helvetica,Arial,sans-serif;background-color:#2c2c32;margin:0;padding:15px 30px}}.message{{font-weight:300;font-size:1.4rem}}.branding{{background-image:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAaCAYAAAC3g3x9AAAABHNCSVQICAgIfAhkiAAABhhJREFUSIl1lXuMVHcVxz/3d+/MnXtnX8PudqmyLx672+WtCKWx8irUloKJtY2xsdmo1T9MWmM0Go0JarEoRSQh6R8KFRuiVm3RSAMSCsijsmWXx2KB3QWW2cfszs7uPO7M3DtzHz//QJoA4Zucf84535PvyUnOFx4Ay7JW2bb9muu6Jz3Ps13XLZZKpUO5XO7xB3EeiLGxMdN13fcymbS8g4nxhAyCQBYKBd+27Z89iKvdmxgYGNDr6+v/6ZbLazzXo1Qq0fvhWVKTSTo651NbVy/ylvUTx3H0SCTyg3v5yr0Jx3F+q2naN4ZuXCeTSXPovYMsXLQIx3aYTk0yPZXi5e/9EDMalbZtvxiNRvcDKIoi75NbKBS+EARBIKWUvee65Ttv/0levHBBFosFeePGdTkyHJc7f/WaHOy/JqWU0vO8UmpysmhZ1rfvW/nmzZsRXdd32batpNPTjI2M0DF/AT2953l9xw5SqSnWrl3LU09v4sL5HurqHyKdSYcbG5umUqnU7+9b2XGc7+i6vhPg9L+PUx2rZWBwkG3bfommaRiRCJlslsWLFvKlZ7+IGY2yavUapJQyl8str6mpOQcgAI4dO6ZpmvZdgGwmjWGaqJrGzp2/oaOjg1/v2MGevXvYuPFpLvX1cehfRwg8FykliqIo0Wj0R3eECYAVK1ZsUlW1sVgocObkCVrnzGX79tcByZqVy/jbG9v4/gvPoPs2j3S0c+rUKT661s++vb+jWCygqurmdDrd/PHAUCj0VYBUahLDjHL06Pv09fXR1dXF1Ys9TE9PoYcVBvqvsWHdakKhEPH4MPPa2hhPjKMoimqaZheAGB4eNoQQTyYnxhmJ36KjcwHd3R9SV1dL5+xGfGuChpowZlgwwxCMDHzEypWPcvz4ccpll+qaavL5PEKILwOIWCz2Od/zzBm1dTiOQ7QiSnd3N8uWLSOfSfPKq7tR1DBF22XJ6k1IRcUwDFzXJV8ocvrEMcLhMKqqtieTyXlaKBT6bCgcJm9ZXOw9h6JqFAoFGhpm0jC7nUhFFd/86RuMjY6QyVpoIRVNNzh8+DBFu8j0RILhW0PMmdemVFVVrdOApQCZ9DTtnfNJpaYBeOfAAQ5lshTKZX6xaSNjiQRvHfg75wcHmBUxCAJJuVRm1donKJdLty8sxApNCNEmpaSQz2PlLPJO+XaxqYkjV68B8Mof/0zFVJKzkynKZiWZXJa5QmAYEeLxW3x62WfIpNNUVFYuFMDDVi7Hlf9exjRN1q9fj2maNFUqVGkaVZEItiI4U/LomD0bhGDeJxponduC57okxka5eL4X3/cAWjRVVaORSISyW+bC2Q+wHYcn1z/B1Nk/IGOPMStWz4YF81neOIuZephLA4PI1CAr12xAVQV6WKejcz56JIIQIqYFQeCGdT28aMlSzGgUPWLy0rr17LrxPp4vyNpF/nquh8eamzh49QoACSVKdW094VAYBdA0DdOMIqVESClvSilpmPkwzc2tnDl5gtGROI0rngFgLJNlNJ3mLz09XEkkAEhi8Mn2JZw5eZx5be3U1tYhhACYEL7vHwWJrkdQVZVHOjv5z+lTrPzUch5XLAzp3/NAJQ/Zed7e9yYvfu0lamIxyu7tQ0opP9Asy9oVi8W+bpim3tw6m4rKSuK3hqiI97HXvYpb9OjxK7k8o5lccoLY2DBBySFwG+hcsJCp1CRSSqSU0nGc3aK+vr7fdd1vAV7EMAiQtMyZS7WTBtdDzmzB+8rLtG18ls0/387i519AAYaHRtm95y2yVh7f9/E8b7iiouKEBmAYxr5cLjdgGMaO2tq6R/uvD/Hqu5epS8VYs+opFi1YjGPbRAyD0ObnGXd0suks8fgIuXyB6uoqNE1rzGQyjXd5im3bXbquv3ns5GnePXiE1uZGntv8eYS423qEEIRCIRQUNE0jWhEFyaWtW7cuvasxn88vDILAKxaLMpvLSitvSc/zPg7f9+X/LUdKKWUQBIHnedlyubwvmUzOvMsC7qBYLD4XCoV+rChKi5QyEQTBP3zfHxdCiCAIPEVRMr7vJwqFwmAul7P2798/tWXLluAO/38rUwksVQPdogAAAABJRU5ErkJggg==);background-repeat:no-repeat;padding-left:26px;font-size:20px;letter-spacing:-.04rem;font-weight:400;height:26px;color:#fff;background-position:left center;text-decoration:none}}</style><a class=branding href=https://pnp.github.io/powershell>PnP PowerShell</a><div class=message-container><div class=message>You successfully provided consent now and can close this page.</div></div>";
            var htmlMessageConsentFailed = $"<html lang=en><meta charset=utf-8><title>PnP PowerShell - Consent</title><meta content=\"width=device-width,initial-scale=1\"name=viewport><style>html{{height:100%}}.error-text{{color:red;font-size:1rem}}.message-container{{flex-grow:1;display:flex;align-items:center;justify-content:center;margin:0 30px}}body{{box-sizing:border-box;min-height:100%;display:flex;flex-direction:column;color:#fff;font-family:\"Segoe UI\",\"Helvetica Neue\",Helvetica,Arial,sans-serif;background-color:#2c2c32;margin:0;padding:15px 30px}}.message{{font-weight:300;font-size:1.4rem}}.branding{{background-image:url(data:image/png;base64,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);background-repeat:no-repeat;height:26px;padding-left:26px;font-size:20px;letter-spacing:-.04rem;font-weight:400;color:#fff;background-position:left center;text-decoration:none}}</style><a class=branding href=https://pnp.github.io/powershell>PnP PowerShell</a><div class=message-container><div class=message>You failed to provide consent. Please try again. You can close this page.</div></div>";

            var graphEndpoint = $"https://{AuthenticationManager.GetGraphEndPoint(AzureEnvironment)}";
            if (AzureEnvironment == AzureEnvironment.Custom)
            {
                graphEndpoint = Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process) ?? MicrosoftGraphEndPoint;
            }

            // The consent flow needs a resource to request a token for, which can only be derived for Microsoft Graph and SharePoint
            var resource = scopes.Any(s => s.resourceAppId == PermissionScopes.ResourceAppId_Graph) ? $"{graphEndpoint}/.default"
                : scopes.Any(s => s.resourceAppId == PermissionScopes.ResourceAppId_SPO) ? "https://microsoft.sharepoint-df.com/.default"
                : null;

            if (resource == null)
            {
                LogWarning($"No Microsoft Graph or SharePoint permissions were requested, so no consent flow can be started. Grant admin consent for app {azureApp.AppId} through the Entra ID portal.");
                WriteObject(record);
                return;
            }

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

            //    ? if (OperatingSystem.IsWindows())
            // {
            if (!Stopping)
            {
                if (ParameterSpecified(nameof(DeviceLogin)))
                {
                    using (var authManager = AuthenticationManager.CreateWithDeviceLogin(azureApp.AppId, Tenant, (deviceCodeResult) =>
                    {
                        if (PSUtility.IsAzureCloudShell())
                        {
                            Host.UI.WriteWarningLine($"\n\nTo sign in, use a web browser to open the page {deviceCodeResult.VerificationUrl} and enter the code {deviceCodeResult.UserCode} to authenticate.");
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
                            Host.UI.WriteWarningLine($"\n\nPlease login.\n\nWe opened a browser and navigated to {deviceCodeResult.VerificationUrl}\n\nEnter code: {deviceCodeResult.UserCode} (we copied this code to your clipboard)\n\nNOTICE: close the browser tab after you authenticated successfully to continue the process.");
                            BrowserHelper.OpenBrowserForInteractiveLogin(deviceCodeResult.VerificationUrl, BrowserHelper.FindFreeLocalhostRedirectUri(), cancellationTokenSource);
                        }
                        return Task.FromResult(0);
                    }, AzureEnvironment))
                    {
                        authManager.ClearTokenCache();
                        authManager.GetAccessToken(resource, Microsoft.Identity.Client.Prompt.Consent);
                    }
                }
                else
                {
                    // Interactive
                    using (var authManager = AuthenticationManager.CreateWithInteractiveWebBrowserLogin(azureApp.AppId, (url, port) =>
                     {
                         BrowserHelper.OpenBrowserForInteractiveLogin(url, port, cancellationTokenSource);
                     }, Tenant, htmlMessageConsentSuccess, htmlMessageConsentFailed, azureEnvironment: AzureEnvironment, useWAM: false))
                    {
                        authManager.ClearTokenCache();
                        authManager.GetAccessToken(resource, Microsoft.Identity.Client.Prompt.Consent);
                    }
                }
                // Write results

                WriteObject($"App created. You can now connect to your tenant using Connect-PnPOnline -Url <yourtenanturl> -ClientId {azureApp.AppId}");
                WriteObject(record);

            }
            // else
            // {
            //     if (OperatingSystem.IsMacOS())
            //     {
            //         Process.Start("open", consentUrl);
            //     }
            //     else if (OperatingSystem.IsLinux())
            //     {
            //         Process.Start("xdg-open", consentUrl);
            //     }
            //     else
            //     {
            //         Host.UI.WriteLine(ConsoleColor.Yellow, Host.UI.RawUI.BackgroundColor, $"Open the following URL in a browser window to provide consent. This consent is required in order to use this application.\n\n{consentUrl}");
            //     }
            //     WriteObject($"App created. You can now connect to your tenant using Connect-PnPOnline -Url <yourtenanturl> -ClientId {azureApp.AppId}");
            //     WriteObject(record);
            // }
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
                    LogDebug("Setting the logo for the EntraID app");

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
                        var requestHelper = new ApiRequestHelper(GetType(), PnPConnection.Current);
                        requestHelper.Put2(endpoint, byteArrayContent, token);

                        LogDebug("Successfully set the logo for the Entra ID app");
                    }
                    else
                    {
                        throw new Exception("Unrecognized image format. Supported formats are .png, .jpg, .jpeg and .gif");
                    }
                }
                catch (Exception ex)
                {
                    LogWarning("Something went wrong setting the logo " + ex.Message);
                }
            }
            else
            {
                LogWarning("Logo File does not exist, ignoring setting the logo");
            }
        }
    }
}