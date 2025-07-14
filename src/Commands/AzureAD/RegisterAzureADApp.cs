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
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;
using OperatingSystem = PnP.PowerShell.Commands.Utilities.OperatingSystem;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsLifecycle.Register, "PnPAzureADApp")]
    [Alias("Register-PnPEntraIDApp")]
    public class RegisterAzureADApp : BasePSCmdlet, IDynamicParameters
    {
        private const string ParameterSet_EXISTINGCERT = "Existing Certificate";
        private const string ParameterSet_NEWCERT = "Generate Certificate";

        private CancellationTokenSource cancellationTokenSource;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string ApplicationName;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Tenant;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_EXISTINGCERT)]
        public string CertificatePath;

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = ParameterSet_NEWCERT)]
        public string CommonName;

        [Parameter(Mandatory = false, Position = 1, ParameterSetName = ParameterSet_NEWCERT)]
        public string Country = String.Empty;

        [Parameter(Mandatory = false, Position = 2, ParameterSetName = ParameterSet_NEWCERT)]
        public string State = string.Empty;

        [Parameter(Mandatory = false, Position = 3, ParameterSetName = ParameterSet_NEWCERT)]
        public string Locality = string.Empty;

        [Parameter(Mandatory = false, Position = 4, ParameterSetName = ParameterSet_NEWCERT)]
        public string Organization = string.Empty;

        [Parameter(Mandatory = false, Position = 5, ParameterSetName = ParameterSet_NEWCERT)]
        public string OrganizationUnit = string.Empty;

        [Parameter(Mandatory = false, Position = 7, ParameterSetName = ParameterSet_NEWCERT)]
        public int ValidYears = 10;

        [Parameter(Mandatory = false, Position = 8, ParameterSetName = ParameterSet_NEWCERT)]
        [Parameter(Mandatory = false, Position = 8, ParameterSetName = ParameterSet_EXISTINGCERT)]
        public SecureString CertificatePassword;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_NEWCERT)]
        public string OutPath;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_NEWCERT)]
        public StoreLocation Store;

        [Parameter(Mandatory = false)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        [Parameter(Mandatory = false)]
        public SwitchParameter DeviceLogin;

        [Parameter(Mandatory = false)]
        public string LogoFilePath;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipCertCreation;

        [Parameter(Mandatory = false)]
        public string MicrosoftGraphEndPoint;

        [Parameter(Mandatory = false)]
        public string EntraIDLoginEndPoint;

        [Parameter(Mandatory = false)]
        public EntraIDSignInAudience SignInAudience;

        protected override void ProcessRecord()
        {
            if (ParameterSpecified(nameof(Store)) && !OperatingSystem.IsWindows())
            {
                throw new PSArgumentException("The Store parameter is only supported on Microsoft Windows");
            }

            if (!string.IsNullOrWhiteSpace(OutPath))
            {
                if (!Path.IsPathRooted(OutPath))
                {
                    OutPath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, OutPath);
                }
            }
            else
            {
                OutPath = SessionState.Path.CurrentFileSystemLocation.Path;
            }

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
                messageWriter.LogWarning("No permissions specified, using default permissions");
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_SPO, "Sites.FullControl.All", "Role")); // AppOnly
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_SPO, "AllSites.FullControl", "Scope")); // AppOnly
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_Graph, "Group.ReadWrite.All", "Role")); // AppOnly
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_SPO, "User.ReadWrite.All", "Role")); // AppOnly
                scopes.Add(permissionScopes.GetScope(PermissionScopes.ResourceAppId_Graph, "User.ReadWrite.All", "Role")); // AppOnly
            }
            var record = new PSObject();

            string token = GetAuthToken(messageWriter);

            if (!string.IsNullOrEmpty(token))
            {
                X509Certificate2 cert = null;
                if (!SkipCertCreation)
                {
                    cert = GetCertificate(record);
                }
                var httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();

                if (!AppExists(ApplicationName, httpClient, token))
                {
                    var azureApp = CreateApp(loginEndPoint, httpClient, token, cert, redirectUri, scopes);

                    record.Properties.Add(new PSVariableProperty(new PSVariable("AzureAppId/ClientId", azureApp.AppId)));
                    if (cert != null)
                    {
                        record.Properties.Add(new PSVariableProperty(new PSVariable("Certificate Thumbprint", cert.GetCertHashString())));
                        byte[] certPfxData = cert.Export(X509ContentType.Pfx, CertificatePassword);
                        var base64String = Convert.ToBase64String(certPfxData);
                        record.Properties.Add(new PSVariableProperty(new PSVariable("Base64Encoded", base64String)));
                    }
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

        private X509Certificate2 GetCertificate(PSObject record)
        {
            X509Certificate2 cert = null;
            if (ParameterSetName == ParameterSet_EXISTINGCERT)
            {
                if (!Path.IsPathRooted(CertificatePath))
                {
                    CertificatePath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, CertificatePath);
                }
                // Ensure a file exists at the provided CertificatePath
                if (!File.Exists(CertificatePath))
                {
                    throw new PSArgumentException(string.Format(Resources.CertificateNotFoundAtPath, CertificatePath), nameof(CertificatePath));
                }

                try
                {
                    cert = new X509Certificate2(CertificatePath, CertificatePassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.PersistKeySet);
                }
                catch (CryptographicException e) when (e.Message.Contains("The specified password is not correct"))
                {
                    throw new PSArgumentNullException(nameof(CertificatePassword), string.Format(Resources.PrivateKeyCertificateImportFailedPasswordIncorrect, nameof(CertificatePassword)));
                }

                // Ensure the certificate at the provided CertificatePath holds a private key
                if (!cert.HasPrivateKey)
                {
                    throw new PSArgumentException(string.Format(Resources.CertificateAtPathHasNoPrivateKey, CertificatePath), nameof(CertificatePath));
                }
            }
            else
            {
                if (!MyInvocation.BoundParameters.ContainsKey("CommonName"))
                {
                    CommonName = ApplicationName;
                }
                DateTime validFrom = DateTime.Today;
                DateTime validTo = validFrom.AddYears(ValidYears);
                cert = CertificateHelper.CreateSelfSignedCertificate(CommonName, Country, State, Locality, Organization, OrganizationUnit, CertificatePassword, CommonName, validFrom, validTo, Array.Empty<string>());

                if (Directory.Exists(OutPath))
                {
                    string pfxPath = Path.Combine(OutPath, $"{ApplicationName}.pfx");
                    string cerPath = Path.Combine(OutPath, $"{ApplicationName}.cer");
                    byte[] certPfxData = cert.Export(X509ContentType.Pfx, CertificatePassword);
                    File.WriteAllBytes(pfxPath, certPfxData);
                    record.Properties.Add(new PSVariableProperty(new PSVariable("Pfx file", pfxPath)));

                    byte[] certCerData = cert.Export(X509ContentType.Cert);
                    File.WriteAllBytes(cerPath, certCerData);
                    record.Properties.Add(new PSVariableProperty(new PSVariable("Cer file", cerPath)));
                }
                if (ParameterSpecified(nameof(Store)))
                {
                    if (OperatingSystem.IsWindows())
                    {
                        using (var store = new X509Store("My", Store))
                        {
                            store.Open(OpenFlags.ReadWrite);
                            store.Add(cert);
                            store.Close();
                        }
                        Host.UI.WriteLine(ConsoleColor.Yellow, Host.UI.RawUI.BackgroundColor, "Certificate added to store");
                    }
                }
            }
            return cert;
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

        private AzureADApp CreateApp(string loginEndPoint, HttpClient httpClient, string token, X509Certificate2 cert, string redirectUri, List<PermissionScope> scopes)
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

            if (cert != null)
            {
                var expirationDate = cert.NotAfter.ToUniversalTime();
                var startDate = cert.NotBefore.ToUniversalTime();
                payload.keyCredentials = new[] {
                    new {
                        customKeyIdentifier = cert.GetCertHashString(),
                        endDateTime = expirationDate,
                        keyId = Guid.NewGuid().ToString(),
                        startDateTime = startDate,
                        type= "AsymmetricX509Cert",
                        usage= "Verify",
                        key = Convert.ToBase64String(cert.GetRawCertData()),
                        displayName = cert.Subject,
                    }
                };
            }

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
            var htmlMessageConsentSuccess = $"<html lang=en><meta charset=utf-8><title>PnP PowerShell - Consent</title><meta content=\"width=device-width,initial-scale=1\"name=viewport><style>html{{height:100%}}.message-container{{flex-grow:1;display:flex;align-items:center;justify-content:center;margin:0 30px}}body{{box-sizing:border-box;min-height:100%;display:flex;flex-direction:column;color:#fff;font-family:\"Segoe UI\",\"Helvetica Neue\",Helvetica,Arial,sans-serif;background-color:#2c2c32;margin:0;padding:15px 30px}}.message{{font-weight:300;font-size:1.4rem}}.branding{{background-image:url(data:image/png;base64,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);background-repeat:no-repeat;padding-left:26px;font-size:20px;letter-spacing:-.04rem;font-weight:400;height:26px;color:#fff;background-position:left center;text-decoration:none}}</style><a class=branding href=https://pnp.github.io/powershell>PnP PowerShell</a><div class=message-container><div class=message>You successfully provided consent now and can close this page.</div></div>";
            var htmlMessageConsentFailed = $"<html lang=en><meta charset=utf-8><title>PnP PowerShell - Consent</title><meta content=\"width=device-width,initial-scale=1\"name=viewport><style>html{{height:100%}}.error-text{{color:red;font-size:1rem}}.message-container{{flex-grow:1;display:flex;align-items:center;justify-content:center;margin:0 30px}}body{{box-sizing:border-box;min-height:100%;display:flex;flex-direction:column;color:#fff;font-family:\"Segoe UI\",\"Helvetica Neue\",Helvetica,Arial,sans-serif;background-color:#2c2c32;margin:0;padding:15px 30px}}.message{{font-weight:300;font-size:1.4rem}}.branding{{background-image:url(data:image/png;base64,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);background-repeat:no-repeat;height:26px;padding-left:26px;font-size:20px;letter-spacing:-.04rem;font-weight:400;color:#fff;background-position:left center;text-decoration:none}}</style><a class=branding href=https://pnp.github.io/powershell>PnP PowerShell</a><div class=message-container><div class=message>You failed to provide consent. Please try again. You can close this page.</div></div>";

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
                    using (var authManager = AuthenticationManager.CreateWithInteractiveWebBrowserLogin(azureApp.AppId, (url, port) =>
                    {
                        BrowserHelper.OpenBrowserForInteractiveLogin(url, port, cancellationTokenSource);
                    }, Tenant, htmlMessageConsentSuccess, htmlMessageConsentFailed, azureEnvironment: AzureEnvironment, useWAM: false))
                    {
                        authManager.ClearTokenCache();
                        authManager.GetAccessToken(resource, Microsoft.Identity.Client.Prompt.Consent);
                    }
                }
            }

            WriteObject(record);
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
