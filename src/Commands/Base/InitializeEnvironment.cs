using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsData.Initialize, "PnPEnvironment", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(PnPEnvironmentSetupResult))]
    [ApiPermissionsNotRequired(Remarks = "It registers the application it will connect with, authenticating separately instead of using the PnP connection. Registering the application requires that the tenant allows users to register applications, or that the account signing in holds a role that may create them, such as Application Developer, Application Administrator, Cloud Application Administrator or Global Administrator. Granting admin consent requires an administrator.")]
    public class InitializeEnvironment : BasePSCmdlet, IDynamicParameters
    {
        /// <summary>Entra ID applies a permission grant with a delay, so the first token can arrive without it.</summary>
        private const int ConsentWaitSeconds = 30;

        /// <summary>How many times to wait, connect and verify before reporting a permission as not held.</summary>
        private const int ConsentAttempts = 2;

        /// <summary>The delegated set of Register-PnPEntraIDAppForInteractiveLogin, used when no permission is asked for.</summary>
        private static readonly (string ParameterName, string[] Permissions)[] defaultPermissions =
        {
            ("GraphDelegatePermissions", new[] { "Group.ReadWrite.All", "User.ReadWrite.All" }),
            ("SharePointDelegatePermissions", new[] { "TermStore.ReadWrite.All", "AllSites.FullControl", "User.ReadWrite.All" })
        };

        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Url;

        // Resolved from -Url when omitted, through the same unauthenticated lookup Connect-PnPOnline uses.
        [Parameter(Mandatory = false)]
        public string Tenant;

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string ApplicationName = "PnP PowerShell";

        [Parameter(Mandatory = false)]
        public Hashtable[] ResourcePermissions;

        // Get-PnPSite declares both permission flavours; a cmdlet declaring none is always indeterminate.
        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        public string[] ValidateCommand = new[] { "Get-PnPSite" };

        [Parameter(Mandatory = false)]
        public SwitchParameter DeviceLogin;

        [Parameter(Mandatory = false)]
        public StoreLocation CertificateStore = StoreLocation.CurrentUser;

        [Parameter(Mandatory = false)]
        public string OutPath;

        [Parameter(Mandatory = false)]
        public SecureString CertificatePassword;

        [Parameter(Mandatory = false)]
        public Framework.AzureEnvironment AzureEnvironment = Framework.AzureEnvironment.Production;

        [Parameter(Mandatory = false)]
        public SwitchParameter PersistLogin;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipConnect;

        /// <summary>Windows can hold the certificate in the store and connect by thumbprint; elsewhere, and with an explicit -OutPath, it has to be a PFX.</summary>
        private bool UseCertificateStore => OperatingSystem.IsWindows() && !ParameterSpecified(nameof(OutPath));

        /// <summary>Offers the same permission parameters as Register-PnPEntraIDApp, which they are forwarded to.</summary>
        public object GetDynamicParameters() => EntraIDPermissionParameters.GetDynamicParameters();

        /// <returns>TRUE when the permission parameter was supplied and holds at least one permission.</returns>
        private bool IsPermissionBound(string parameterName) =>
            MyInvocation.BoundParameters.TryGetValue(parameterName, out var value)
            && value != null
            && LanguagePrimitives.ConvertTo<string[]>(value).Length > 0;

        protected override void ExecuteCmdlet()
        {
            var siteUrl = new System.Uri(Url);

            var applicationPermissions = EntraIDPermissionParameters.ApplicationParameterNames.Any(IsPermissionBound)
                || ResourcePermissions?.Any(resource => HasResourceKey(resource, "ApplicationPermissions")) == true;
            var delegatedPermissions = EntraIDPermissionParameters.DelegateParameterNames.Any(IsPermissionBound)
                || ResourcePermissions?.Any(resource => HasResourceKey(resource, "DelegatePermissions")) == true;
            var useDefaultPermissions = !applicationPermissions && !delegatedPermissions;

            // Only app-only needs a certificate; delegated authenticates interactively, so one would go unused.
            var createCertificate = applicationPermissions;

            ValidateParameters(createCertificate);

            // Asked before the tenant is resolved so that -WhatIf reports the plan without making a network call, and it names the permissions because -WhatIf is what a cautious admin runs to see what consent is about to be asked for.
            if (!ShouldProcess(siteUrl.ToString(), $"Register application '{ApplicationName}' in tenant {(string.IsNullOrWhiteSpace(Tenant) ? "resolved from this URL" : Tenant)} requesting {DescribeRequestedPermissions(useDefaultPermissions)}{(createCertificate ? ", with a certificate" : "")}, grant admin consent for it, and store its client id"))
            {
                return;
            }

            ResolveTenant(siteUrl);

            var result = new PnPEnvironmentSetupResult
            {
                Url = siteUrl.ToString(),
                Tenant = Tenant,
                ApplicationName = ApplicationName
            };

            if (useDefaultPermissions)
            {
                LogWarning($"No permissions specified, requesting the default delegated permissions: {DescribeRequestedPermissions(true)}.");
            }

            // 1. Register. Register-PnPEntraIDApp signs in, creates the app and runs its own consent flow.
            var registration = Register(createCertificate, useDefaultPermissions);
            if (registration == null)
            {
                throw new PSInvalidOperationException("Register-PnPEntraIDApp returned no client id, so the environment was not set up. Its own output states what went wrong.");
            }

            result.ClientId = ReadProperty(registration, "ClientId", "AzureAppId/ClientId");
            result.CertificateThumbprint = ReadProperty(registration, "Thumbprint", "Certificate Thumbprint");
            result.CertificatePath = ReadProperty(registration, "PfxPath", "Pfx file");
            result.ConsentUrl = GetConsentUrl(result.ClientId);
            result.PortalUrl = $"https://portal.azure.com/#view/Microsoft_AAD_RegisteredApps/ApplicationMenuBlade/~/CallAnAPI/appId/{result.ClientId}";

            // Checked before NextStep is composed, since without the certificate in the store the PFX is the only key material and there is no command that could use the registration.
            if (createCertificate && !UseCertificateStore && string.IsNullOrWhiteSpace(result.CertificatePath))
            {
                WriteObject(result);
                throw new PSInvalidOperationException($"'{ApplicationName}' was registered as {result.ClientId} but no certificate file was written, so nothing can authenticate as it. Remove it with Remove-PnPEntraIDApp and run again with an -OutPath that exists.");
            }

            result.NextStep = DescribeNextStep(result, createCertificate);

            // Reported before the -SkipConnect path below, because handing the registration to someone else is exactly when this caveat has to travel with it.
            WarnAboutSitesSelected(result);

            // The application now exists in the tenant, so its identifiers have to reach the user even when a later step fails; without them it can neither be used nor removed.
            try
            {
                StoreAppId(result);

                if (!SkipConnect)
                {
                    ConnectAndVerify(result, createCertificate);
                    ReportPermissionOutcome(result);
                }
            }
            // A cancellation is passed through untouched, as the repository does elsewhere, because wrapping it breaks pipeline stop semantics downstream.
            catch (System.Exception exception) when (exception is not PipelineStoppedException)
            {
                WriteObject(result);
                throw new PSInvalidOperationException($"'{ApplicationName}' was registered as {result.ClientId} but the setup did not complete: {exception.Message} Its client id and certificate are on the object returned before this error, so it can be used with '{result.NextStep}' or removed with Remove-PnPEntraIDApp.", exception);
            }

            WriteObject(result);
        }

        /// <summary>Resolves the tenant from the URL when it was not given, through the unauthenticated realm lookup so a vanity domain resolves too.</summary>
        private void ResolveTenant(System.Uri siteUrl)
        {
            if (!string.IsNullOrWhiteSpace(Tenant))
            {
                return;
            }

            try
            {
                Tenant = Microsoft.SharePoint.Client.TenantExtensions.GetTenantIdByUrl(siteUrl.ToString(), AzureEnvironment);
            }
            catch (System.Exception exception) when (exception is not PipelineStoppedException)
            {
                throw new PSArgumentException($"The tenant of {siteUrl} could not be determined ({exception.Message}). Specify -Tenant.", nameof(Tenant));
            }

            if (string.IsNullOrWhiteSpace(Tenant))
            {
                throw new PSArgumentException($"The tenant of {siteUrl} could not be determined. Specify -Tenant.", nameof(Tenant));
            }

            LogDebug($"Resolved tenant {Tenant} from {siteUrl}");
        }

        /// <summary>Rejects parameters that would produce an unusable registration, and reports those that cannot take effect, rather than accepting and ignoring them.</summary>
        private void ValidateParameters(bool createCertificate)
        {
            if (createCertificate && ParameterSpecified(nameof(OutPath)))
            {
                // Checked before anything is created: Register-PnPEntraIDApp writes the PFX only when the folder already exists and writes nothing at all, silently, when it does not.
                var folder = System.IO.Path.IsPathRooted(OutPath) ? OutPath : System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, OutPath);
                if (!System.IO.Directory.Exists(folder))
                {
                    throw new PSArgumentException($"The folder {folder} does not exist, so the certificate would be created without being written anywhere and nothing could authenticate as the registration. Create the folder, or pass one that exists.", nameof(OutPath));
                }
            }

            if (!createCertificate)
            {
                var certificateParameters = new[] { nameof(OutPath), nameof(CertificateStore), nameof(CertificatePassword) }
                    .Where(ParameterSpecified).Select(name => $"-{name}").ToArray();
                if (certificateParameters.Length > 0)
                {
                    LogWarning($"{string.Join(", ", certificateParameters)} {(certificateParameters.Length == 1 ? "describes" : "describe")} the certificate of an app-only registration, and no certificate is created because only delegated permissions were requested. Request an application permission to get one, or drop {(certificateParameters.Length == 1 ? "it" : "them")}.");
                }
            }
            else if (ParameterSpecified(nameof(PersistLogin)))
            {
                LogWarning("-PersistLogin caches the refresh token of an interactive sign in, so it does not apply to the app-only connection made for application permissions and is ignored.");
            }
        }

        /// <summary>Warns that Sites.Selected satisfies the permission check on the token claims while granting access to no site, so a green check says nothing about what the application can read.</summary>
        private void WarnAboutSitesSelected(PnPEnvironmentSetupResult result)
        {
            if (!RequestsSitesSelected())
            {
                return;
            }

            LogWarning($"'{ApplicationName}' requests the SharePoint Sites.Selected permission, which grants access to no site until it is granted per site, and the permission check cannot detect that because the claim is present either way. Grant access with: Grant-PnPEntraIDAppSitePermission -AppId {result.ClientId} -DisplayName '{ApplicationName}' -Site <site url> -Permissions Read. That cmdlet needs a connection holding the delegated Microsoft Graph Sites.FullControl.All permission, so it cannot be run by this application itself.");
        }

        private void StoreAppId(PnPEnvironmentSetupResult result)
        {
            Invoke("Set-PnPManagedAppId", new Hashtable
            {
                { "Url", result.Url },
                { "AppId", result.ClientId },
                { "Overwrite", true }
            });
            result.AppIdStored = true;
        }

        /// <summary>Waits for the grant, connects and verifies, retrying app-only once because reconnecting builds a new confidential client and so asks for a genuinely new token, whereas a delegated token stays cached for the process.</summary>
        private void ConnectAndVerify(PnPEnvironmentSetupResult result, bool appOnly)
        {
            if (ValidateCommand.Length == 0)
            {
                LogWarning("No -ValidateCommand was given, so the permissions of the new connection have not been verified.");
            }

            var attempts = appOnly ? ConsentAttempts : 1;
            for (var attempt = 1; attempt <= attempts; attempt++)
            {
                WaitForConsent(attempt, attempts);
                if (Stopping)
                {
                    return;
                }

                Connect(result, appOnly);
                result.Connected = PnPConnection.Current != null;

                foreach (var commandName in ValidateCommand)
                {
                    result.PermissionChecks[commandName] = TestPermission(commandName);
                }

                if (!result.PermissionChecks.Values.Any(outcome => outcome == false))
                {
                    break;
                }
            }

            // Asserted once after the loop rather than per attempt, since the connection is made the same way each time and the warning would otherwise repeat.
            if (result.Connected)
            {
                AssertConnectionIdentity(result);
            }
        }

        /// <summary>Confirms the connection authenticates as the application just registered, which a connection left over in the session would otherwise satisfy.</summary>
        private void AssertConnectionIdentity(PnPEnvironmentSetupResult result)
        {
            var connectedClientId = PnPConnection.Current?.ClientId;
            if (!string.IsNullOrWhiteSpace(connectedClientId)
                && !string.Equals(connectedClientId, result.ClientId, System.StringComparison.OrdinalIgnoreCase))
            {
                LogWarning($"The connection authenticates as application {connectedClientId} rather than the {result.ClientId} that was just registered, so any permission reported below describes the wrong application. Connect explicitly with '{result.NextStep}'.");
            }
        }

        private void ReportPermissionOutcome(PnPEnvironmentSetupResult result)
        {
            // Left NULL when no check ran, so that an unverified setup is never read as one where consent is known to be fine.
            if (result.PermissionChecks.Count == 0)
            {
                return;
            }

            result.ConsentRequired = false;
            var missing = result.PermissionChecks.Where(check => check.Value == false).Select(check => check.Key).ToArray();
            if (missing.Length > 0)
            {
                result.ConsentRequired = true;
                LogWarning($"The connection does not hold the permissions required for {string.Join(", ", missing)}, after waiting for admin consent to take effect. If consent was declined, or the account that signed in cannot grant it, an administrator can grant it at {result.ConsentUrl} and review what is being requested at {result.PortalUrl}. Then reconnect with '{result.NextStep}'.");
            }

            // Not a failure: a check that could not be performed says nothing about what the connection holds.
            var indeterminate = result.PermissionChecks.Where(check => check.Value == null).Select(check => check.Key).ToArray();
            if (indeterminate.Length > 0)
            {
                LogWarning($"The permissions required for {string.Join(", ", indeterminate)} could not be determined, so the connection could not be verified against them. This does not mean they are missing. Pass -ValidateCommand a cmdlet that declares its permissions to confirm the setup.");
            }
        }

        /// <summary>Lists the permissions that will be requested, so that -WhatIf discloses what consent is about to be asked for.</summary>
        private string DescribeRequestedPermissions(bool useDefaultPermissions)
        {
            if (useDefaultPermissions)
            {
                return string.Join(", ", defaultPermissions.Select(entry => $"-{entry.ParameterName} {string.Join(", ", entry.Permissions)}"));
            }

            var described = EntraIDPermissionParameters.AllParameterNames.Where(IsPermissionBound)
                .Select(name => $"-{name} {string.Join(", ", LanguagePrimitives.ConvertTo<string[]>(MyInvocation.BoundParameters[name]))}")
                .ToList();
            if (ResourcePermissions?.Length > 0)
            {
                described.Add($"-ResourcePermissions ({ResourcePermissions.Length} {(ResourcePermissions.Length == 1 ? "entry" : "entries")})");
            }
            return string.Join(", ", described);
        }

        /// <returns>The values of a -ResourcePermissions key, matched case insensitively as Register-PnPEntraIDApp does, empty when absent.</returns>
        private static string[] ResourceKeyValues(Hashtable resource, string key)
        {
            var match = resource?.Keys.Cast<object>()
                .FirstOrDefault(candidate => string.Equals(candidate?.ToString(), key, System.StringComparison.OrdinalIgnoreCase));
            var value = match == null ? null : resource[match];
            return value == null ? System.Array.Empty<string>() : LanguagePrimitives.ConvertTo<string[]>(value);
        }

        /// <returns>TRUE when a -ResourcePermissions entry carries the key with at least one value.</returns>
        private static bool HasResourceKey(Hashtable resource, string key) => ResourceKeyValues(resource, key).Length > 0;

        /// <returns>TRUE when the SharePoint Sites.Selected application permission is requested, through either the named parameter or -ResourcePermissions.</returns>
        private bool RequestsSitesSelected()
        {
            if (MyInvocation.BoundParameters.TryGetValue("SharePointApplicationPermissions", out var value) && value != null
                && LanguagePrimitives.ConvertTo<string[]>(value).Contains("Sites.Selected", System.StringComparer.OrdinalIgnoreCase))
            {
                return true;
            }

            return ResourcePermissions?.Any(resource => IsSharePointResource(resource)
                && ResourceKeyValues(resource, "ApplicationPermissions").Contains("Sites.Selected", System.StringComparer.OrdinalIgnoreCase)) == true;
        }

        /// <returns>TRUE when the -ResourcePermissions entry targets SharePoint, named or by application id.</returns>
        private static bool IsSharePointResource(Hashtable resource)
        {
            var resourceName = ResourceKeyValues(resource, "Resource").FirstOrDefault();
            return string.Equals(resourceName, "SharePoint", System.StringComparison.OrdinalIgnoreCase)
                || string.Equals(resourceName, PermissionScopes.ResourceAppId_SPO, System.StringComparison.OrdinalIgnoreCase);
        }

        /// <returns>The registration details, or NULL when Register-PnPEntraIDApp produced no client id.</returns>
        private PSObject Register(bool createCertificate, bool useDefaultPermissions)
        {
            var parameters = new Hashtable
            {
                { "ApplicationName", ApplicationName },
                { "Tenant", Tenant },
                { "AzureEnvironment", AzureEnvironment }
            };

            if (DeviceLogin)
            {
                parameters["DeviceLogin"] = true;
            }

            if (useDefaultPermissions)
            {
                // Passed explicitly rather than letting Register-PnPEntraIDApp fall back, as its own default is app-only tenant wide full control.
                foreach (var (parameterName, permissions) in defaultPermissions)
                {
                    parameters[parameterName] = permissions;
                }
            }
            else
            {
                foreach (var parameterName in EntraIDPermissionParameters.AllParameterNames.Where(IsPermissionBound))
                {
                    parameters[parameterName] = MyInvocation.BoundParameters[parameterName];
                }
                if (ResourcePermissions?.Length > 0)
                {
                    parameters[nameof(ResourcePermissions)] = ResourcePermissions;
                }
            }

            if (!createCertificate)
            {
                parameters["SkipCertCreation"] = true;
            }
            else
            {
                if (UseCertificateStore)
                {
                    parameters["Store"] = CertificateStore;
                }
                if (ParameterSpecified(nameof(OutPath)))
                {
                    parameters[nameof(OutPath)] = OutPath;
                }
                if (CertificatePassword != null)
                {
                    parameters[nameof(CertificatePassword)] = CertificatePassword;
                }
            }

            try
            {
                return Invoke("Register-PnPEntraIDApp", parameters)
                    .FirstOrDefault(output => ReadProperty(output, "ClientId", "AzureAppId/ClientId") != null);
            }
            catch (System.Exception exception) when (ReportsExistingApplication(exception, ApplicationName))
            {
                throw new PSArgumentException($"An application named '{ApplicationName}' already exists in {Tenant}. Pass -ApplicationName to register under a different name, or remove the existing one with Remove-PnPEntraIDApp.", nameof(ApplicationName));
            }
        }

        /// <returns>TRUE when the exception, or one it wraps, reports this application name as taken. Matched on the message, naming the application too so an unrelated conflict is not reported as a name clash, because Register-PnPEntraIDApp signals it with a plain PSInvalidOperationException.</returns>
        private static bool ReportsExistingApplication(System.Exception exception, string applicationName)
        {
            for (var candidate = exception; candidate != null; candidate = candidate.InnerException)
            {
                if (candidate.Message?.Contains("already exists", System.StringComparison.OrdinalIgnoreCase) == true
                    && candidate.Message.Contains(applicationName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Waits for the consent granted moments ago to take effect, so the token acquired next already carries the permissions.</summary>
        private void WaitForConsent(int attempt, int attempts)
        {
            var activity = attempt == 1
                ? "Waiting for admin consent to take effect"
                : $"Some permissions have not taken effect yet, waiting again (attempt {attempt} of {attempts})";
            var progress = new ProgressRecord(1, activity, $"Entra ID applies a permission grant with a delay. Waiting {ConsentWaitSeconds} seconds before connecting.");

            for (var second = 0; second < ConsentWaitSeconds && !Stopping; second++)
            {
                progress.PercentComplete = second * 100 / ConsentWaitSeconds;
                WriteProgress(progress);
                Thread.Sleep(1000);
            }

            progress.RecordType = ProgressRecordType.Completed;
            WriteProgress(progress);
        }

        private void Connect(PnPEnvironmentSetupResult result, bool appOnly)
        {
            var parameters = new Hashtable
            {
                { "Url", result.Url },
                { "ClientId", result.ClientId },
                { "Tenant", Tenant },
                { "AzureEnvironment", AzureEnvironment }
            };

            if (appOnly)
            {
                if (UseCertificateStore)
                {
                    parameters["Thumbprint"] = result.CertificateThumbprint;
                }
                else
                {
                    parameters["CertificatePath"] = result.CertificatePath;
                    if (CertificatePassword != null)
                    {
                        parameters[nameof(CertificatePassword)] = CertificatePassword;
                    }
                }
            }
            else
            {
                // -DeviceLogin exists for a machine with no usable browser, so the connection has to honour it too instead of falling back to an interactive sign in.
                parameters[DeviceLogin ? "DeviceLogin" : "Interactive"] = true;
                if (PersistLogin)
                {
                    parameters["PersistLogin"] = true;
                }
            }

            Invoke("Connect-PnPOnline", parameters);
        }

        /// <returns>TRUE or FALSE, or NULL when the required permissions could not be determined.</returns>
        private bool? TestPermission(string commandName)
        {
            // It reports a missing or undeterminable permission as an error, expected here rather than a failure.
            var output = Invoke("Test-PnPConnectionPermission", new Hashtable
            {
                { "CommandName", commandName },
                { "ErrorAction", "SilentlyContinue" }
            });

            return output.FirstOrDefault()?.BaseObject as bool?;
        }

        private string DescribeNextStep(PnPEnvironmentSetupResult result, bool appOnly)
        {
            // -ClientId is always explicit: app-only sets require it, and -Interactive resolves it from the persisted login cache first, where an entry for the same URL naming another application would win.
            if (!appOnly)
            {
                return $"Connect-PnPOnline -Url {result.Url} {(DeviceLogin ? "-DeviceLogin" : "-Interactive")} -ClientId {result.ClientId}";
            }

            return UseCertificateStore
                ? $"Connect-PnPOnline -Url {result.Url} -Tenant {Tenant} -ClientId {result.ClientId} -Thumbprint {result.CertificateThumbprint}"
                : $"Connect-PnPOnline -Url {result.Url} -Tenant {Tenant} -ClientId {result.ClientId} -CertificatePath {result.CertificatePath} -CertificatePassword <password>";
        }

        /// <returns>The one step admin consent URL, or NULL for a custom cloud whose login endpoint is not known here.</returns>
        private string GetConsentUrl(string clientId)
        {
            using (var authenticationManager = new Framework.AuthenticationManager())
            {
                var loginEndPoint = authenticationManager.GetAzureADLoginEndPoint(AzureEnvironment);
                return string.IsNullOrWhiteSpace(loginEndPoint)
                    ? null
                    : $"{loginEndPoint.TrimEnd('/')}/{Tenant}/adminconsent?client_id={clientId}";
            }
        }

        /// <summary>Runs another cmdlet in its own scope so splatting leaves no variables behind; the connection it makes is static, not scoped.</summary>
        private Collection<PSObject> Invoke(string command, Hashtable parameters)
        {
            LogDebug($"Invoking {command}");
            return InvokeCommand.InvokeScript($"$parameters = $args[0]; {command} @parameters", true, PipelineResultTypes.None, null, parameters);
        }

        /// <summary>Reads the first property holding a value, so both Register-PnPEntraIDApp's current names and any cleaner ones added later work.</summary>
        private static string ReadProperty(PSObject source, params string[] names)
        {
            return names.Select(name => source?.Properties[name]?.Value?.ToString())
                        .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
        }
    }
}
