using Microsoft.Identity.Client;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Base
{
    internal class PnPConnectionHelper
    {

#if DEBUG
        private static readonly Uri VersionCheckUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/dev/version.txt");
#else
        private static readonly Uri VersionCheckUrl = new Uri("https://raw.githubusercontent.com/pnp/powershell/master/version.txt");
#endif
        private static bool VersionChecked;

        static PnPConnectionHelper()
        {
        }

        internal static PnPConnection InstantiateSPOnlineConnection(Uri url, string realm, string clientId, string clientSecret, string tenantAdminUrl, bool disableTelemetry, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            ConnectionType connectionType;
            PnPClientContext context = null;

            if (url != null)
            {
                using (var authManager = new PnP.Framework.AuthenticationManager())
                {
                    if (realm == null)
                    {
                        realm = GetRealmFromTargetUrl(url);
                    }

                    if (url.DnsSafeHost.Contains("spoppe.com"))
                    {
                        context = PnPClientContext.ConvertFrom(authManager.GetACSAppOnlyContext(url.ToString(), realm, clientId, clientSecret, acsHostUrl: "windows-ppe.net", globalEndPointPrefix: "login"));
                    }
                    else
                    {
                        context = PnPClientContext.ConvertFrom(authManager.GetACSAppOnlyContext(url.ToString(), realm, clientId, clientSecret, acsHostUrl: authManager.GetACSEndPoint(azureEnvironment), globalEndPointPrefix: authManager.GetACSEndPointPrefix(azureEnvironment)));
                    }
                    context.ApplicationName = Resources.ApplicationName;
                    context.DisableReturnValueCache = true;
                    connectionType = ConnectionType.O365;
                    if (IsTenantAdminSite(context))
                    {
                        connectionType = ConnectionType.TenantAdmin;
                    }
                }
            }
            else
            {
                connectionType = ConnectionType.O365;
            }

            var spoConnection = new PnPConnection(context, connectionType, null, clientId, clientSecret, url?.ToString(), tenantAdminUrl, PnPPSVersionTag, disableTelemetry, InitializationType.SPClientSecret)
            {
                Tenant = realm
            };

            return spoConnection;
        }

        internal static PnPConnection InstantiateDeviceLoginConnection(string url, bool launchBrowser, string tenantAdminUrl, PSCmdlet cmdlet, bool disableTelemetry, AzureEnvironment azureEnvironment, ref CancellationToken cancellationToken)
        {
            var connectionUri = new Uri(url);
            var scopes = new[] { $"{connectionUri.Scheme}://{connectionUri.Authority}//.default" }; // the second double slash is not a typo.
            var context = new ClientContext(url);
            GenericToken tokenResult = null;
            try
            {
                tokenResult = GraphToken.AcquireApplicationTokenDeviceLogin(PnPConnection.PnPManagementShellClientId, scopes, PnPConnection.DeviceLoginCallback(cmdlet, launchBrowser), azureEnvironment, ref cancellationToken);
            }
            catch (MsalUiRequiredException ex)
            {
                if (ex.Classification == UiRequiredExceptionClassification.ConsentRequired)
                {
                    cmdlet.WriteFormattedWarning("You need to provide consent to the PnP Management Shell application for your tenant. The easiest way to do this is by issueing: 'Connect-PnPOnline -Url [yoursiteur] -PnPManagementShell -LaunchBrowser'. Make sure to authenticate as a Azure administrator allowing to provide consent to the application. Follow the steps provided.");
                    throw ex;
                }
            }
            var spoConnection = new PnPConnection(context, tokenResult, ConnectionType.O365, null, url.ToString(), tenantAdminUrl, PnPPSVersionTag, disableTelemetry, InitializationType.DeviceLogin)
            {
                //var spoConnection = new PnPConnection(context, ConnectionType.O365, url.ToString(), tenantAdminUrl, PnPPSVersionTag, host, disableTelemetry, InitializationType.DeviceLogin);
                Scopes = scopes,
                AzureEnvironment = azureEnvironment,
            };
            if (spoConnection != null)
            {
                spoConnection.ConnectionMethod = ConnectionMethod.DeviceLogin;
            }
            return spoConnection;
        }

        internal static PnPConnection InstantiateGraphAccessTokenConnection(string accessToken, bool disableTelemetry)
        {
            var tokenResult = new GenericToken(accessToken);
            var spoConnection = new PnPConnection(tokenResult, ConnectionMethod.AccessToken, ConnectionType.O365, PnPPSVersionTag, disableTelemetry, InitializationType.Graph)
            {
                ConnectionMethod = ConnectionMethod.GraphDeviceLogin
            };
            return spoConnection;
        }

        internal static PnPConnection InstantiateGraphDeviceLoginConnection(bool launchBrowser, PSCmdlet cmdlet, bool disableTelemetry, AzureEnvironment azureEnvironment, ref CancellationToken cancellationToken)
        {
            var tokenResult = GraphToken.AcquireApplicationTokenDeviceLogin(
                PnPConnection.PnPManagementShellClientId,
                new[] { "Group.Read.All", "openid", "email", "profile", "Group.ReadWrite.All", "User.Read.All", "Directory.ReadWrite.All" },
                PnPConnection.DeviceLoginCallback(cmdlet, launchBrowser),
                azureEnvironment,
                ref cancellationToken);
            var spoConnection = new PnPConnection(tokenResult, ConnectionMethod.GraphDeviceLogin, ConnectionType.O365, PnPPSVersionTag, disableTelemetry, InitializationType.GraphDeviceLogin)
            {
                Scopes = new[] { "Group.Read.All", "openid", "email", "profile", "Group.ReadWrite.All", "User.Read.All", "Directory.ReadWrite.All" },
                AzureEnvironment = azureEnvironment,
            };
            return spoConnection;
        }

        //private static GenericToken GetTokenResult(Uri connectionUri, Dictionary<string, string> returnData, Action<string> messageCallback, Action<string> progressCallback, Func<bool> cancelRequest)
        //{
        //    HttpClient client = new HttpClient();
        //    var body = new StringContent($"resource={connectionUri.Scheme}://{connectionUri.Host}&client_id={PnPConnection.PnPManagementShellClientId}&grant_type=device_code&code={returnData["device_code"]}");
        //    body.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

        //    var responseMessage = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
        //    var stopWatch = new Stopwatch();
        //    stopWatch.Start();
        //    var shouldCancel = cancelRequest();
        //    while (!responseMessage.IsSuccessStatusCode && !shouldCancel)
        //    {
        //        if (stopWatch.ElapsedMilliseconds > 60 * 1000)
        //        {
        //            break;
        //        }
        //        progressCallback(".");
        //        System.Threading.Thread.Sleep(1000);
        //        body = new StringContent($"resource={connectionUri.Scheme}://{connectionUri.Host}&client_id={PnPConnection.PnPManagementShellClientId}&grant_type=device_code&code={returnData["device_code"]}");
        //        body.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
        //        responseMessage = client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", body).GetAwaiter().GetResult();
        //        shouldCancel = cancelRequest();
        //    }
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        return JsonSerializer.Deserialize<SharePointToken>(responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        //    }
        //    else
        //    {
        //        if (shouldCancel)
        //        {
        //            messageCallback("Cancelled");
        //        }
        //        else
        //        {
        //            messageCallback("Timeout");
        //        }
        //        return null;
        //    }
        //}

        //internal static void OpenBrowser(string url)
        //{
        //    try
        //    {
        //        System.Diagnostics.Process.Start(url);
        //    }
        //    catch
        //    {
        //        // hack because of this: https://github.com/dotnet/corefx/issues/10361
        //        if (Utilities.OperatingSystem.IsWindows())
        //        {
        //            url = url.Replace("&", "^&");
        //            System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        //        }
        //        else if (Utilities.OperatingSystem.IsLinux())
        //        {
        //            System.Diagnostics.Process.Start("xdg-open", url);
        //        }
        //        else if (Utilities.OperatingSystem.IsMacOS())
        //        {
        //            System.Diagnostics.Process.Start("open", url);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

        //internal static PnPConnection InitiateAzureADNativeApplicationConnection(Uri url, string clientId, Uri redirectUri, int requestTimeout, string tenantAdminUrl, PSHost host, bool disableTelemetry, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        //{
        //    using (var authManager = new PnP.Framework.AuthenticationManager())
        //    {
        //        string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //        string configFile = Path.Combine(appDataFolder, "PnP.PowerShell\\tokencache.dat");
        //        FileTokenCache cache = new FileTokenCache(configFile);
        //        var context = PnPClientContext.ConvertFrom(authManager.GetAzureADNativeApplicationAuthenticatedContext(url.ToString(), clientId, redirectUri, cache, azureEnvironment));
        //        var connectionType = ConnectionType.OnPrem;
        //        if (url.Host.ToLowerInvariant().EndsWith($"sharepoint.{PnP.Framework.AuthenticationManager.GetSharePointDomainSuffix(azureEnvironment)}"))
        //        {
        //            connectionType = ConnectionType.O365;
        //        }
        //            if (IsTenantAdminSite(context))
        //            {
        //                connectionType = ConnectionType.TenantAdmin;
        //            }
        //        var spoConnection = new PnPConnection(context, connectionType, null, clientId, null, url.ToString(), tenantAdminUrl, PnPPSVersionTag, host, disableTelemetry, InitializationType.AADNativeApp)
        //        {
        //            ConnectionMethod = ConnectionMethod.AzureADNativeApplication
        //        };
        //        return spoConnection;
        //    }
        //}

        internal static PnPConnection InitiateAzureADAppOnlyConnection(Uri url, string clientId, string tenant, string certificatePath, SecureString certificatePassword, string tenantAdminUrl, bool disableTelemetry, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            X509Certificate2 certificate = CertificateHelper.GetCertificateFromPath(certificatePath, certificatePassword);

            return InitiateAzureAdAppOnlyConnectionWithCert(url, clientId, tenant, tenantAdminUrl, disableTelemetry, azureEnvironment, certificate, true);
        }

        internal static PnPConnection InitiateAzureADAppOnlyConnection(Uri url, string clientId, string tenant, string thumbprint, string tenantAdminUrl, bool disableTelemetry, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            X509Certificate2 certificate = CertificateHelper.GetCertificatFromStore(thumbprint);

            if (certificate == null)
            {
                throw new PSArgumentOutOfRangeException(nameof(thumbprint), null, string.Format(Resources.CertificateWithThumbprintNotFound, thumbprint));
            }

            // Ensure the private key of the certificate is available
            if (!certificate.HasPrivateKey)
            {
                throw new PSArgumentOutOfRangeException(nameof(thumbprint), null, string.Format(Resources.CertificateWithThumbprintDoesNotHavePrivateKey, thumbprint));
            }

            return InitiateAzureAdAppOnlyConnectionWithCert(url, clientId, tenant, tenantAdminUrl, disableTelemetry, azureEnvironment, certificate, false);
        }

        internal static PnPConnection InitiateAzureADAppOnlyConnection(Uri url, string clientId, string tenant, string certificatePEM, string privateKeyPEM, SecureString certificatePassword, string tenantAdminUrl, bool disableTelemetry, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            string password = new System.Net.NetworkCredential(string.Empty, certificatePassword).Password;
            X509Certificate2 certificate = CertificateHelper.GetCertificateFromPEMstring(certificatePEM, privateKeyPEM, password);

            return InitiateAzureAdAppOnlyConnectionWithCert(url, clientId, tenant, tenantAdminUrl, disableTelemetry, azureEnvironment, certificate, false);
        }

        internal static PnPConnection InitiateAzureAdAppOnlyConnectionWithCert(Uri url, string clientId, string tenant, string tenantAdminUrl, bool disableTelemetry, AzureEnvironment azureEnvironment, string base64EncodedCertificate)
        {
            X509Certificate2 certificate = CertificateHelper.GetCertificateFromBase64Encodedstring(base64EncodedCertificate);
            return InitiateAzureAdAppOnlyConnectionWithCert(url, clientId, tenant, tenantAdminUrl, disableTelemetry, azureEnvironment, certificate);
        }

        internal static PnPConnection InitiateAzureAdAppOnlyConnectionWithCert(Uri url, string clientId, string tenant, string tenantAdminUrl, bool disableTelemetry, AzureEnvironment azureEnvironment, X509Certificate2 certificate)
        {
            return InitiateAzureAdAppOnlyConnectionWithCert(url, clientId, tenant, tenantAdminUrl, disableTelemetry, azureEnvironment, certificate, false);
        }

        private static PnPConnection InitiateAzureAdAppOnlyConnectionWithCert(Uri url, string clientId, string tenant, string tenantAdminUrl, bool disableTelemetry, AzureEnvironment azureEnvironment, X509Certificate2 certificate, bool certificateFromFile)
        {
            using (var authManager = new PnP.Framework.AuthenticationManager(clientId, certificate, tenant, azureEnvironment: azureEnvironment))
            {
                var clientContext = authManager.GetContext(url.ToString());
                var context = PnPClientContext.ConvertFrom(clientContext);

                var connectionType = ConnectionType.O365;

                if (IsTenantAdminSite(context))
                {
                    connectionType = ConnectionType.TenantAdmin;
                }

                var spoConnection = new PnPConnection(context, connectionType, null, clientId, null, url.ToString(), tenantAdminUrl, PnPPSVersionTag, disableTelemetry, InitializationType.AADAppOnly)
                {
                    ConnectionMethod = ConnectionMethod.AzureADAppOnly,
                    Certificate = certificate,
                    Tenant = tenant,
                    DeleteCertificateFromCacheOnDisconnect = certificateFromFile
                };
                return spoConnection;
            }
        }

        /// <summary>
        /// Sets up a connection to Microsoft Graph using a Client Id and Client Secret
        /// </summary>
        /// <param name="clientId">Client ID to use to authenticate</param>
        /// <param name="clientSecret">Client Secret to use to authenticate</param>
        /// <param name="aadDomain">The Azure Active Directory domain to authenticate to, i.e. contoso.onmicrosoft.com</param>
        /// <param name="host">The PowerShell host environment reference</param>
        /// <param name="disableTelemetry">Boolean indicating if telemetry should be disabled</param>
        /// <returns></returns>
        //internal static PnPConnection InitiateAzureAdAppOnlyConnectionWithClientIdClientSecret(string clientId, string clientSecret, string aadDomain, PSHost host, bool disableTelemetry)
        //{
        //    var app = ConfidentialClientApplicationBuilder.Create(clientId).WithAuthority($"https://login.microsoftonline.com/{aadDomain}").WithClientSecret(clientSecret).Build();
        //    var result = app.AcquireTokenForClient(new[] { "https://graph.microsoft.com/.default" }).ExecuteAsync().GetAwaiter().GetResult();
        //    if (result == null)
        //    {
        //        return null;
        //    }

        //    var spoConnection = InstantiateGraphAccessTokenConnection(result.AccessToken, host, disableTelemetry);
        //    return spoConnection;
        //}

        /// <summary>
        /// Tries to remove the local cached machine copy of the private key
        /// </summary>
        /// <param name="certificate">Certificate to try to clean up the local cached copy of the private key of</param>
        internal static void CleanupCryptoMachineKey(X509Certificate2 certificate)
        {
            if (!certificate.HasPrivateKey)
            {
                // If somehow a public key certificate was passed in, we can't clean it up, thus we have nothing to do here
                return;
            }

            var privateKey = (RSACryptoServiceProvider)certificate.PrivateKey;
            string uniqueKeyContainerName = privateKey.CspKeyContainerInfo.UniqueKeyContainerName;
            certificate.Reset();

            var programDataPath = Environment.GetEnvironmentVariable("ProgramData");
            if (string.IsNullOrEmpty(programDataPath))
            {
                programDataPath = @"C:\ProgramData";
            }
            try
            {
                var temporaryCertificateFilePath = $@"{programDataPath}\Microsoft\Crypto\RSA\MachineKeys\{uniqueKeyContainerName}";
                if (System.IO.File.Exists(temporaryCertificateFilePath))
                {
                    System.IO.File.Delete(temporaryCertificateFilePath);
                }
            }
            catch (Exception)
            {
                // best effort cleanup
            }
        }

        internal static PnPConnection InstantiateSPOnlineConnection(Uri url, PSCredential credentials, string tenantAdminUrl, bool disableTelemetry, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            var context = new PnPClientContext(url.AbsoluteUri)
            {
                ApplicationName = Resources.ApplicationName,
                DisableReturnValueCache = true
            };
            var tenantId = string.Empty;
            try
            {
                using (var authManager = new PnP.Framework.AuthenticationManager(credentials.UserName, credentials.Password))
                {
                    context = PnPClientContext.ConvertFrom(authManager.GetContext(url.ToString()));
                    context.ExecuteQueryRetry();

                    var accessToken = authManager.GetAccessTokenAsync(url.ToString()).GetAwaiter().GetResult();
                    var parsedToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(accessToken);
                    tenantId = parsedToken.Claims.FirstOrDefault(c => c.Type == "tid").Value;
                }
            }
            catch (ClientRequestException)
            {
                context.Credentials = new NetworkCredential(credentials.UserName, credentials.Password);
            }
            catch (ServerException)
            {
                context.Credentials = new NetworkCredential(credentials.UserName, credentials.Password);
            }
            var connectionType = ConnectionType.O365;
            if (url.Host.ToLowerInvariant().EndsWith($"sharepoint.{PnP.Framework.AuthenticationManager.GetSharePointDomainSuffix(azureEnvironment)}"))
            {
                connectionType = ConnectionType.O365;
            }

            if (IsTenantAdminSite(context))
            {
                connectionType = ConnectionType.TenantAdmin;
            }
            var spoConnection = new PnPConnection(context, connectionType, credentials, url.ToString(), tenantAdminUrl, PnPPSVersionTag, disableTelemetry, InitializationType.Credentials)
            {
                ConnectionMethod = Model.ConnectionMethod.Credentials,
                AzureEnvironment = azureEnvironment,
                Tenant = tenantId
            };
            return spoConnection;
        }

        public static string GetRealmFromTargetUrl(Uri targetApplicationUri)
        {
            WebRequest request = WebRequest.Create(targetApplicationUri + "/_vti_bin/client.svc");
            request.Headers.Add("Authorization: Bearer ");

            try
            {
                using (request.GetResponse())
                {
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    return null;
                }

                string bearerResponseHeader = e.Response.Headers["WWW-Authenticate"];
                if (string.IsNullOrEmpty(bearerResponseHeader))
                {
                    return null;
                }

                const string bearer = "Bearer realm=\"";
                int bearerIndex = bearerResponseHeader.IndexOf(bearer, StringComparison.Ordinal);
                if (bearerIndex < 0)
                {
                    return null;
                }

                int realmIndex = bearerIndex + bearer.Length;

                if (bearerResponseHeader.Length >= realmIndex + 36)
                {
                    string targetRealm = bearerResponseHeader.Substring(realmIndex, 36);
                    if (Guid.TryParse(targetRealm, out _))
                    {
                        return targetRealm;
                    }
                }
            }
            return null;
        }

        private static bool IsTenantAdminSite(ClientRuntimeContext clientContext)
        {
            try
            {
                using (var clonedContext = clientContext.Clone(clientContext.Url))
                {
                    var tenant = new Tenant(clonedContext);
                    clonedContext.ExecuteQueryRetry();
                    return true;
                }
            }
            catch (ClientRequestException)
            {
                return false;
            }
            catch (ServerException)
            {
                return false;
            }
            catch (WebException)
            {
                return false;
            }
        }

        private static string PnPPSVersionTag => (PnPPSVersionTagLazy.Value);

        private static readonly Lazy<string> PnPPSVersionTagLazy = new Lazy<string>(
            () =>
            {
                var coreAssembly = Assembly.GetExecutingAssembly();
                var version = ((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version.Split('.');

                var result = $"PnPPS:{version[0]}.{version[1]}";
                return (result);
            },
            true);

        public static string GetLatestVersion()
        {
            try
            {
                if (!VersionChecked)
                {
                    using (var httpClient = new HttpClient())
                    {
                        var response = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, VersionCheckUrl)).GetAwaiter().GetResult();
                        if (response.IsSuccessStatusCode)
                        {
                            var onlineVersion = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            onlineVersion = onlineVersion.Trim(new char[] { '\t', '\r', '\n' });
                            var assembly = Assembly.GetExecutingAssembly();
#if !NET461
                            var currentVersion = new SemanticVersion(((AssemblyFileVersionAttribute)assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version);
                            if (SemanticVersion.TryParse(onlineVersion, out SemanticVersion availableVersion))
#else
                            var currentVersion = new Version(((AssemblyFileVersionAttribute)assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version);
                            if (Version.TryParse(onlineVersion, out Version availableVersion))
#endif
                            {
                                var newVersionAvailable = false;
                                if (availableVersion.Major > currentVersion.Major)
                                {
                                    newVersionAvailable = true;
                                }
                                else
                                {
                                    if (availableVersion.Major == currentVersion.Major && availableVersion.Minor > currentVersion.Minor)
                                    {
                                        newVersionAvailable = true;
                                    }
                                }
                                if (newVersionAvailable)
                                {
                                    return $"\nA newer version of PnP PowerShell is available: {availableVersion}. Use `Update-Module -Name PnP.PowerShell` to update.\n";
                                }
                            }
                            VersionChecked = true;
                        }

                    }
                }
            }
            catch
            { }
            return null;
        }
    }
}
