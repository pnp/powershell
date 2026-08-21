using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections;
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

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsLifecycle.Register, "PnPEntraIDApp")]
    [Alias("Register-PnPAzureADApp")]
    [Attributes.ApiPermissionsNotRequired(Remarks = "It authenticates separately instead of using the PnP connection. Creating the application registration requires that the tenant allows users to register applications, or that the account signing in holds a role that may create them, such as Application Developer, Application Administrator, Cloud Application Administrator or Global Administrator.")]
    public class RegisterAzureADApp : BasePSCmdlet, IDynamicParameters
    {
        private const string ParameterSet_EXISTINGCERT = "Existing Certificate";
        private const string ParameterSet_NEWCERT = "Generate Certificate";

        /// <summary>
        /// The resources for which a -{Name}DelegatePermissions and, when the resource exposes application permissions at all, a
        /// -{Name}ApplicationPermissions parameter is offered. Permissions of resources that do not ship with the module are resolved
        /// from the tenant, so no permission list has to be maintained for them.
        /// </summary>
        private static readonly (string Name, string ResourceAppId, bool HasApplicationPermissions)[] resources = new[]
        {
            ("Graph", PermissionScopes.ResourceAppId_Graph, true),
            ("SharePoint", PermissionScopes.ResourceAppId_SPO, true),
            ("O365Management", PermissionScopes.ResourceAppID_O365Management, true),
            ("Exchange", "00000002-0000-0ff1-ce00-000000000000", true),
            ("PowerBI", "00000009-0000-0000-c000-000000000000", true),
            // Dataverse, PowerApps and Azure Resource Manager expose delegated permissions only. Should they ever gain application
            // permissions, they can be requested through -ResourcePermissions without a change here, as that resolves from the tenant.
            ("Dataverse", "00000007-0000-0000-c000-000000000000", false),
            ("PowerApps", "475226c6-020e-4fb2-8a90-7a972cbfc1d4", false),
            ("AzureServiceManagement", "797f4846-ba00-4fd7-ba43-dac1f8f63013", false)
        };

        private static IEnumerable<string> PermissionParameterNames =>
            resources.SelectMany(r => r.HasApplicationPermissions
                ? new[] { $"{r.Name}ApplicationPermissions", $"{r.Name}DelegatePermissions" }
                : new[] { $"{r.Name}DelegatePermissions" });

        private static readonly string[] resourcePermissionKeys = { "Resource", "ApplicationPermissions", "DelegatePermissions" };

        private CancellationTokenSource cancellationTokenSource;

        private readonly PermissionScopes permissionScopes = new PermissionScopes();

        private readonly Dictionary<string, List<PermissionScope>> tenantScopes = new Dictionary<string, List<PermissionScope>>();

        private HttpClient httpClient;

        private string accessToken;

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

        [Parameter(Mandatory = false)]
        public Hashtable[] ResourcePermissions;

        protected override void ProcessRecord()
        {
            if (ParameterSpecified(nameof(Store)) && !OperatingSystem.IsWindows())
            {
                throw new PSArgumentException("The Store parameter is only supported on Microsoft Windows");
            }

            ValidateRequestedPermissions();

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

            var record = new PSObject();

            string token = GetAuthToken(messageWriter);

            if (!string.IsNullOrEmpty(token))
            {
                httpClient = Framework.Http.PnPHttpClient.Instance.GetHttpClient();
                accessToken = token;

                var scopes = GetRequestedScopes();

                X509Certificate2 cert = null;
                if (!SkipCertCreation)
                {
                    cert = GetCertificate(record);
                }

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
                // Deduplicating on id is only correct within a single resource: different resources do reuse permission ids,
                // Microsoft Graph and SharePoint for instance share the id of their User.Read.All and User.ReadWrite.All app roles
                appResource.ResourceAccess.AddRange(scopes.Where(s => s.resourceAppId == id).GroupBy(s => (s.Id, s.Type)).Select(g => g.First()));
                resourcePermissions.Add(appResource);
            }
            return resourcePermissions;
        }

        /// <summary>
        /// Validates everything about the requested permissions that can be checked without contacting the tenant, so that a malformed
        /// invocation fails before the user is asked to authenticate. Whether a permission exists is checked later, in <see cref="AddScopes"/>.
        /// </summary>
        private void ValidateRequestedPermissions()
        {
            if (GetBoundPermissions("Scopes").Any())
            {
                var conflicting = PermissionParameterNames.Where(MyInvocation.BoundParameters.ContainsKey).ToList();
                if (ResourcePermissions != null)
                {
                    conflicting.Add(nameof(ResourcePermissions));
                }
                if (conflicting.Any())
                {
                    throw new PSArgumentException($"-Scopes cannot be combined with {string.Join(", ", conflicting.Select(c => $"-{c}"))}, as only -Scopes would be applied. Use the per resource parameters, -Scopes is obsolete.", "Scopes");
                }
            }

            foreach (var resourcePermission in ResourcePermissions ?? Array.Empty<Hashtable>())
            {
                var unknownKeys = resourcePermission.Keys.Cast<object>().Select(k => k?.ToString())
                    .Where(k => !resourcePermissionKeys.Contains(k, StringComparer.OrdinalIgnoreCase)).ToArray();
                if (unknownKeys.Any())
                {
                    throw new PSArgumentException($"A -ResourcePermissions entry contains the unsupported key(s) {string.Join(", ", unknownKeys)}. Supported keys are {string.Join(", ", resourcePermissionKeys)}.", nameof(ResourcePermissions));
                }

                var resourceAppId = GetResourceAppId(resourcePermission["Resource"] as string);
                if (!AsStrings(resourcePermission["ApplicationPermissions"]).Any() && !AsStrings(resourcePermission["DelegatePermissions"]).Any())
                {
                    throw new PSArgumentException($"The -ResourcePermissions entry for resource {resourceAppId} has neither an ApplicationPermissions nor a DelegatePermissions key holding a permission.", nameof(ResourcePermissions));
                }
            }
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
                foreach (var identifier in legacyScopes)
                {
                    scopes.Add(permissionScopes.GetScopeByLegacyIdentifier(identifier)
                        ?? throw new PSArgumentException($"Permission '{identifier}' does not exist.", "Scopes"));
                }
                return scopes;
            }

            foreach (var (name, resourceAppId, hasApplicationPermissions) in resources)
            {
                if (hasApplicationPermissions)
                {
                    AddScopes(scopes, resourceAppId, "Role", GetBoundPermissions($"{name}ApplicationPermissions"), false);
                }
                AddScopes(scopes, resourceAppId, "Scope", GetBoundPermissions($"{name}DelegatePermissions"), false);
            }

            foreach (var resourcePermission in ResourcePermissions ?? Array.Empty<Hashtable>())
            {
                var resourceAppId = GetResourceAppId(resourcePermission["Resource"] as string);

                // Always resolved from the tenant, so that permissions missing from the lists shipping with the module can be requested too
                AddScopes(scopes, resourceAppId, "Role", AsStrings(resourcePermission["ApplicationPermissions"]), true);
                AddScopes(scopes, resourceAppId, "Scope", AsStrings(resourcePermission["DelegatePermissions"]), true);
            }

            if (scopes.Any())
            {
                return scopes;
            }

            LogWarning("No permissions specified, using default permissions");
            AddScopes(scopes, PermissionScopes.ResourceAppId_SPO, "Role", new[] { "Sites.FullControl.All", "User.ReadWrite.All" }, false);
            AddScopes(scopes, PermissionScopes.ResourceAppId_SPO, "Scope", new[] { "AllSites.FullControl" }, false);
            AddScopes(scopes, PermissionScopes.ResourceAppId_Graph, "Role", new[] { "Group.ReadWrite.All", "User.ReadWrite.All" }, false);
            return scopes;
        }

        private void AddScopes(List<PermissionScope> scopes, string resourceAppId, string type, IEnumerable<string> identifiers, bool resolveFromTenant)
        {
            foreach (var identifier in identifiers)
            {
                var scope = !resolveFromTenant && PermissionScopes.IsCuratedResource(resourceAppId)
                    ? permissionScopes.GetScope(resourceAppId, identifier, type)
                    : GetTenantScopes(resourceAppId).FirstOrDefault(s => s.Identifier == identifier && s.Type == type);

                scopes.Add(scope ?? throw new PSArgumentException($"Resource {resourceAppId} does not expose {(type == "Role" ? "an application" : "a delegated")} permission named '{identifier}'."));
            }
        }

        /// <summary>
        /// Reads the permissions a resource exposes from its service principal in the tenant, so that no list of them has to be maintained.
        /// </summary>
        private List<PermissionScope> GetTenantScopes(string resourceAppId)
        {
            if (tenantScopes.TryGetValue(resourceAppId, out var cachedScopes))
            {
                return cachedScopes;
            }

            var servicePrincipal = RestHelper.Get<RestResultCollection<AzureADServicePrincipal>>(httpClient, $"{GetGraphEndPoint()}/v1.0/servicePrincipals?$filter=appId eq '{resourceAppId}'&$select=appRoles,oauth2PermissionScopes", accessToken)?.Items?.FirstOrDefault();
            if (servicePrincipal == null)
            {
                throw new PSArgumentException($"Resource {resourceAppId} has no service principal in tenant {Tenant}, so the permissions it exposes cannot be determined. The API may not be available in this tenant.");
            }

            var resolvedScopes = new List<PermissionScope>();
            foreach (var appRole in servicePrincipal.AppRoles ?? new List<AzureADServicePrincipalAppRole>())
            {
                // Only roles that are enabled and assignable to an application can be granted to the app being registered
                if (appRole.IsEnabled != true || appRole.AllowedMemberTypes?.Contains("Application") != true)
                {
                    continue;
                }
                resolvedScopes.Add(new PermissionScope { resourceAppId = resourceAppId, Id = appRole.Id?.ToString(), Identifier = appRole.Value, Type = "Role" });
            }
            foreach (var oauth2PermissionScope in servicePrincipal.Oauth2PermissionScopes ?? new List<AzureADServicePrincipalOauth2PermissionScopes>())
            {
                if (oauth2PermissionScope.IsEnabled != true)
                {
                    continue;
                }
                resolvedScopes.Add(new PermissionScope { resourceAppId = resourceAppId, Id = oauth2PermissionScope.Id?.ToString(), Identifier = oauth2PermissionScope.Value, Type = "Scope" });
            }

            tenantScopes.Add(resourceAppId, resolvedScopes);
            return resolvedScopes;
        }

        private static string GetResourceAppId(string resource)
        {
            var wellKnownResourceAppId = resources.FirstOrDefault(r => r.Name.Equals(resource, StringComparison.OrdinalIgnoreCase)).ResourceAppId;
            if (wellKnownResourceAppId != null)
            {
                return wellKnownResourceAppId;
            }
            if (!Guid.TryParse(resource, out var resourceAppId))
            {
                throw new PSArgumentException($"Every -ResourcePermissions entry needs a Resource key holding the application id of the resource, or one of: {string.Join(", ", resources.Select(r => r.Name))}.", nameof(ResourcePermissions));
            }
            // Normalised, as the application id is compared against the well known ones and grouped per resource, both of which are case sensitive
            return resourceAppId.ToString();
        }

        private IEnumerable<string> GetBoundPermissions(string parameterName)
        {
            return MyInvocation.BoundParameters.TryGetValue(parameterName, out var value) ? AsStrings(value) : Enumerable.Empty<string>();
        }

        private static IEnumerable<string> AsStrings(object value)
        {
            return value == null ? Enumerable.Empty<string>() : LanguagePrimitives.ConvertTo<string[]>(value);
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

            foreach (var (name, resourceAppId, hasApplicationPermissions) in resources)
            {
                if (hasApplicationPermissions)
                {
                    parameterDictionary.Add($"{name}ApplicationPermissions", GetParameter($"{name}ApplicationPermissions", resourceAppId, "Role"));
                }
                parameterDictionary.Add($"{name}DelegatePermissions", GetParameter($"{name}DelegatePermissions", resourceAppId, "Scope"));
            }

            return parameterDictionary;
        }

        private RuntimeDefinedParameter GetParameter(string parameterName, string resourceAppId, string type)
        {
            var attributeCollection = new System.Collections.ObjectModel.Collection<Attribute>
            {
                new ParameterAttribute { ValueFromPipeline = false, ValueFromPipelineByPropertyName = false, Mandatory = false }
            };
            if (PermissionScopes.IsCuratedResource(resourceAppId))
            {
                // Permissions of the other resources are validated against the tenant, as their available permissions are only known there
                attributeCollection.Add(new ValidateSetAttribute(permissionScopes.GetIdentifiers(resourceAppId, type)));
            }
            return new RuntimeDefinedParameter(parameterName, typeof(string[]), attributeCollection);
        }

        private string GetGraphEndPoint()
        {
            if (AzureEnvironment == AzureEnvironment.Custom)
            {
                return Environment.GetEnvironmentVariable("MicrosoftGraphEndPoint", EnvironmentVariableTarget.Process) ?? MicrosoftGraphEndPoint;
            }
            return $"https://{AuthenticationManager.GetGraphEndPoint(AzureEnvironment)}";
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
                    throw new PSArgumentException($"Certificate not found at path: {CertificatePath}", nameof(CertificatePath));
                }

                try
                {
                    cert = new X509Certificate2(CertificatePath, CertificatePassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.PersistKeySet);
                }
                catch (CryptographicException e) when (e.Message.Contains("The specified password is not correct"))
                {
                    throw new PSArgumentNullException(nameof(CertificatePassword), $"Failed to import private key certificate. Ensure the correct password is provided for parameter: {nameof(CertificatePassword)}");
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
                    CertificateHelper.WritePrivateKeyFile(pfxPath, certPfxData);
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

            var azureApps = RestHelper.Get<RestResultCollection<AzureADApp>>(httpClient, $"{GetGraphEndPoint()}/v1.0/applications?$filter=displayName eq '{appName}'&$select=Id", token);
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

            var graphEndpoint = GetGraphEndPoint();

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

            // The consent flow needs a resource to request a token for, which can only be derived for Microsoft Graph and SharePoint
            var resource = scopes.Any(s => s.resourceAppId == PermissionScopes.ResourceAppId_Graph) ? $"{GetGraphEndPoint()}/.default"
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

                    var endpoint = $"{GetGraphEndPoint()}/v1.0/applications/{azureApp.Id}/logo";

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
