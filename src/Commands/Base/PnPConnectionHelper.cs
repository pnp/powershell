using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.Auth;
using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Base
{
    internal class PnPConnectionHelper
    {

        #region Connection Creation
        internal static PnPConnection InstantiateACSAppOnlyConnection(Uri url, string realm, string clientId, string clientSecret, string tenantAdminUrl, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            ConnectionType connectionType;
            PnPClientContext context = null;

            if (url != null)
            {
                PnP.Framework.AuthenticationManager authManager = null;
                if (PnPConnection.CachedAuthenticationManager != null)
                {
                    authManager = PnPConnection.CachedAuthenticationManager;
                    PnPConnection.CachedAuthenticationManager = null;
                }
                else
                {
                    authManager = new PnP.Framework.AuthenticationManager();
                }
                using (authManager)
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
                        context = PnPClientContext.ConvertFrom(authManager.GetACSAppOnlyContext(url.ToString(), realm, clientId, clientSecret, acsHostUrl: Framework.AuthenticationManager.GetACSEndPoint(azureEnvironment), globalEndPointPrefix: Framework.AuthenticationManager.GetACSEndPointPrefix(azureEnvironment)));
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

            var spoConnection = new PnPConnection(context, connectionType, null, clientId, clientSecret, url?.ToString(), tenantAdminUrl, PnPPSVersionTag, InitializationType.ClientIDSecret)
            {
                Tenant = realm,
                AzureEnvironment = azureEnvironment
            };

            return spoConnection;
        }

        internal static PnPConnection InstantiateDeviceLoginConnection(string url, bool launchBrowser, CmdletMessageWriter messageWriter, AzureEnvironment azureEnvironment, CancellationToken cancellationToken)
        {
            var connectionUri = new Uri(url);
            var scopes = new[] { $"{connectionUri.Scheme}://{connectionUri.Authority}//.default" }; // the second double slash is not a typo.
            PnP.Framework.AuthenticationManager authManager = null;
            if (PnPConnection.CachedAuthenticationManager != null)
            {
                authManager = PnPConnection.CachedAuthenticationManager;
                PnPConnection.CachedAuthenticationManager = null;
            }
            else
            {
                authManager = PnP.Framework.AuthenticationManager.CreateWithDeviceLogin(PnPConnection.PnPManagementShellClientId, (deviceCodeResult) =>
                 {
                     if (launchBrowser)
                     {
                         if (Utilities.OperatingSystem.IsWindows())
                         {
                             ClipboardService.SetText(deviceCodeResult.UserCode);
                             messageWriter.WriteWarning($"\n\nCode {deviceCodeResult.UserCode} has been copied to your clipboard\n\n");
                             BrowserHelper.GetWebBrowserPopup(deviceCodeResult.VerificationUrl, "Please log in");
                         }
                         else
                         {
                             messageWriter.WriteWarning($"\n\n{deviceCodeResult.Message}\n\n");
                         }
                     }
                     else
                     {
                         messageWriter.WriteWarning($"\n\n{deviceCodeResult.Message}\n\n");
                     }
                     return Task.FromResult(0);
                 }, azureEnvironment);
            }
            using (authManager)
            {
                var clientContext = authManager.GetContext(url.ToString(), cancellationToken);
                var context = PnPClientContext.ConvertFrom(clientContext);

                var connectionType = ConnectionType.O365;

                var spoConnection = new PnPConnection(context, connectionType, null, PnPConnection.PnPManagementShellClientId, null, url.ToString(), null, PnPPSVersionTag, InitializationType.DeviceLogin)
                {
                    ConnectionMethod = ConnectionMethod.DeviceLogin,
                    AzureEnvironment = azureEnvironment
                };
                return spoConnection;
            }
        }

        internal static PnPConnection InstantiateConnectionWithCert(Uri url, string clientId, string tenant, string tenantAdminUrl, AzureEnvironment azureEnvironment, X509Certificate2 certificate, bool certificateFromFile = false)
        {
            PnP.Framework.AuthenticationManager authManager = null;
            if (PnPConnection.CachedAuthenticationManager != null)
            {
                authManager = PnPConnection.CachedAuthenticationManager;
                PnPConnection.CachedAuthenticationManager = null;
            }
            else
            {
                authManager = PnP.Framework.AuthenticationManager.CreateWithCertificate(clientId, certificate, tenant, azureEnvironment: azureEnvironment);
            }
            using (authManager)
            {
                var clientContext = authManager.GetContext(url.ToString());
                var context = PnPClientContext.ConvertFrom(clientContext);

                var connectionType = ConnectionType.O365;

                if (IsTenantAdminSite(context))
                {
                    connectionType = ConnectionType.TenantAdmin;
                }

                var spoConnection = new PnPConnection(context, connectionType, null, clientId, null, url.ToString(), tenantAdminUrl, PnPPSVersionTag, InitializationType.ClientIDCertificate)
                {
                    ConnectionMethod = ConnectionMethod.AzureADAppOnly,
                    Certificate = certificate,
                    Tenant = tenant,
                    DeleteCertificateFromCacheOnDisconnect = certificateFromFile,
                    AzureEnvironment = azureEnvironment
                };
                return spoConnection;
            }
        }

        internal static PnPConnection InstantiateManagedIdentityConnection(Cmdlet cmdlet)
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            var accesstoken = TokenHandler.GetManagedIdentityTokenAsync(cmdlet, httpClient, "https://graph.microsoft.com/").GetAwaiter().GetResult();
            var connection = new PnPConnection(PnPPSVersionTag, InitializationType.Graph);
            return connection;
        }

        internal static PnPConnection InstantiateConnectionWithCredentials(Uri url, PSCredential credentials, bool currentCredentials, string tenantAdminUrl, AzureEnvironment azureEnvironment = AzureEnvironment.Production, string clientId = null, string redirectUrl = null, bool onPrem = false, InitializationType initializationType = InitializationType.Credentials)
        {
            var context = new PnPClientContext(url.AbsoluteUri)
            {
                ApplicationName = Resources.ApplicationName,
                DisableReturnValueCache = true
            };
            PnPConnection spoConnection = null;
            if (!onPrem)
            {
                var tenantId = string.Empty;
                try
                {
                    if (!string.IsNullOrWhiteSpace(clientId))
                    {
                        PnP.Framework.AuthenticationManager authManager = null;
                        if (PnPConnection.CachedAuthenticationManager != null)
                        {
                            authManager = PnPConnection.CachedAuthenticationManager;
                            PnPConnection.CachedAuthenticationManager = null;
                        }
                        else
                        {
                            authManager = PnP.Framework.AuthenticationManager.CreateWithCredentials(clientId, credentials.UserName, credentials.Password, redirectUrl, azureEnvironment);
                        }
                        using (authManager)
                        {
                            context = PnPClientContext.ConvertFrom(authManager.GetContext(url.ToString()));
                            context.ExecuteQueryRetry();
                            var accesstoken = authManager.GetAccessTokenAsync(url.ToString()).GetAwaiter().GetResult();
                            var parsedToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(accesstoken);
                            tenantId = parsedToken.Claims.FirstOrDefault(c => c.Type == "tid").Value;
                        }
                    }
                    else
                    {
                        PnP.Framework.AuthenticationManager authManager = null;
                        if (PnPConnection.CachedAuthenticationManager != null)
                        {
                            authManager = PnPConnection.CachedAuthenticationManager;
                        }
                        else
                        {
                            authManager = PnP.Framework.AuthenticationManager.CreateWithCredentials(credentials.UserName, credentials.Password, azureEnvironment);
                        }
                        using (authManager)
                        {
                            context = PnPClientContext.ConvertFrom(authManager.GetContext(url.ToString()));
                            context.ExecuteQueryRetry();

                            var accessToken = authManager.GetAccessTokenAsync(url.ToString()).GetAwaiter().GetResult();
                            var parsedToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(accessToken);
                            tenantId = parsedToken.Claims.FirstOrDefault(c => c.Type == "tid").Value;
                        }
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

                spoConnection = new PnPConnection(context, connectionType, credentials, url.ToString(), tenantAdminUrl, PnPPSVersionTag, initializationType)
                {
                    ConnectionMethod = Model.ConnectionMethod.Credentials,
                    AzureEnvironment = azureEnvironment,
                    Tenant = tenantId
                };
            }
            else
            {
                PnP.Framework.AuthenticationManager authManager = null;
                if (PnPConnection.CachedAuthenticationManager != null)
                {
                    authManager = PnPConnection.CachedAuthenticationManager;
                }
                else
                {
                    authManager = new PnP.Framework.AuthenticationManager();
                }
                using (authManager)
                {
                    if (currentCredentials)
                    {
                        context = PnPClientContext.ConvertFrom(authManager.GetOnPremisesContext(url.ToString()));
                    }
                    else
                    {
                        context = PnPClientContext.ConvertFrom(authManager.GetOnPremisesContext(url.ToString(), credentials.UserName, credentials.Password));
                    }
                }

                spoConnection = new PnPConnection(context, ConnectionType.O365, credentials, url.ToString(), tenantAdminUrl, PnPPSVersionTag, initializationType)
                {
                    ConnectionMethod = Model.ConnectionMethod.Credentials,
                    AzureEnvironment = azureEnvironment,
                };
            }

            return spoConnection;
        }

        internal static PnPConnection InstantiateWebloginConnection(Uri url, string tenantAdminUrl, bool clearCookies, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            if (Utilities.OperatingSystem.IsWindows())
            {
                // Log in to a specific page on the tenant which is known to be performant
                var webLoginClientContext = BrowserHelper.GetWebLoginClientContext(url.ToString(), clearCookies, loginRequestUri: new Uri(url, "/_layouts/15/settings.aspx"));

                // Ensure the login process has been completed
                if (webLoginClientContext == null)
                {
                    return null;
                }

                var context = PnPClientContext.ConvertFrom(webLoginClientContext);

                if (context != null)
                {
                    context.ApplicationName = Resources.ApplicationName;
                    context.DisableReturnValueCache = true;
                    var spoConnection = new PnPConnection(context, ConnectionType.O365, null, url.ToString(), tenantAdminUrl, PnPPSVersionTag, InitializationType.InteractiveLogin);
                    spoConnection.ConnectionMethod = Model.ConnectionMethod.WebLogin;
                    spoConnection.AzureEnvironment = azureEnvironment;
                    return spoConnection;
                }

                throw new Exception("Error establishing a connection, context is null");
            }
            else
            {
                return null;
            }
        }

        internal static PnPConnection InstantiateInteractiveConnection(Uri uri, string clientId, string tenantAdminUrl, bool launchBrowser, AzureEnvironment azureEnvironment, CancellationTokenSource cancellationTokenSource, bool forceAuthentication)
        {
            PnP.Framework.AuthenticationManager authManager = null;
            if (PnPConnection.CachedAuthenticationManager != null && !forceAuthentication)
            {
                authManager = PnPConnection.CachedAuthenticationManager;
                PnPConnection.CachedAuthenticationManager = null;
            }
            else
            {
                authManager = PnP.Framework.AuthenticationManager.CreateWithInteractiveLogin(clientId, (url, port) =>
                {
                    BrowserHelper.OpenBrowserForInteractiveLogin(url, port, !launchBrowser, cancellationTokenSource);
                },
                successMessageHtml: $"You successfully authenticated with PnP PowerShell. Feel free to close this {(launchBrowser ? "tab" : "window")}.",
                failureMessageHtml: $"You did not authenticate with PnP PowerShell. Feel free to close this browser {(launchBrowser ? "tab" : "window")}.",
                azureEnvironment: azureEnvironment);
            }
            using (authManager)
            {
                var clientContext = authManager.GetContext(uri.ToString(), cancellationTokenSource.Token);
                var context = PnPClientContext.ConvertFrom(clientContext);

                var connectionType = ConnectionType.O365;

                if (IsTenantAdminSite(context))
                {
                    connectionType = ConnectionType.TenantAdmin;
                }

                var spoConnection = new PnPConnection(context, connectionType, null, clientId, null, uri.ToString(), tenantAdminUrl, PnPPSVersionTag, InitializationType.ClientIDCertificate)
                {
                    ConnectionMethod = ConnectionMethod.Credentials,
                    AzureEnvironment = azureEnvironment
                };
                return spoConnection;
            }
        }

        #endregion

        #region Helper Methods
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
            if (clientContext.Url.ToLower().Contains(".sharepoint."))
            {
                return clientContext.Url.ToLower().Contains("-admin.sharepoint.");
            }
            else
            {
                // fall back to old code in case of vanity domains
                try
                {
                    var tenant = new Microsoft.Online.SharePoint.TenantAdministration.Tenant(clientContext);
                    clientContext.ExecuteQueryRetry();
                    return true;
                }
                catch (ServerException)
                {
                    return false;
                }
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


        #endregion

    }
}
