using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using PnP.Framework;
using PnP.PowerShell.ALC;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using System.Net;
using TextCopy;
using PnP.PowerShell.Commands.Utilities;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Base
{
    public class PnPConnection
    {
        #region Constants

        /// <summary>
        /// ClientId of the application registered in Azure Active Directory which should be used for the device oAuth flow
        /// </summary>
        internal const string PnPManagementShellClientId = "31359c7f-bd7e-475c-86db-fdb8c937548e";
        internal const string AzureManagementShellClientId = "1950a258-227b-4e31-a9cf-717495945fc2";

        #endregion

        #region Properties

        /// <summary>
        /// Returns a reusable HTTPClient that can be used to make HTTP calls on this connection instance
        /// </summary>
        internal HttpClient HttpClient => PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();

        private PnPContext pnpContext { get; set; }

        internal PnPContext PnPContext
        {
            get
            {
                if (pnpContext == null && Context != null)
                {
                    pnpContext = PnP.Framework.PnPCoreSdk.Instance.GetPnPContext(Context);
                }
                return pnpContext;
            }
        }
        /// <summary>
        /// User Agent identifier to use on all connections being made to the APIs
        /// </summary>
        internal string UserAgent { get; set; }

        internal static PnP.Framework.AuthenticationManager CachedAuthenticationManager { get; set; }

        internal ConnectionMethod ConnectionMethod { get; set; }

        /// <summary>
        /// Identifier set on the SharePoint ClientContext as the ClientTag to identify the source of the requests to SharePoint
        /// </summary>
        internal string PnPVersionTag { get; set; }

        internal static List<ClientContext> ContextCache { get; set; }

        /// <summary>
        /// Connection instance which is set by connecting without -ReturnConnection
        /// </summary>
        public static PnPConnection Current { get; internal set; }

        public ConnectionType ConnectionType { get; protected set; }

        /// <summary>
        /// Indication for telemetry through which method a connection has been established
        /// </summary>
        public InitializationType InitializationType { get; protected set; }

        /// <summary>
        /// used to retrieve a new token in case the current token expires
        /// </summary>
        public string[] Scopes { get; internal set; }

        public PSCredential PSCredential { get; protected set; }

        /// <summary>
        /// ClientId under which the connection has been made
        /// </summary>
        public string ClientId { get; protected set; }

        /// <summary>
        /// ClientSecret used to set up the connection
        /// </summary>
        public string ClientSecret { get; protected set; }

        /// <summary>
        /// Azure Application Insights instance to provide telemetry
        /// </summary>
        public ApplicationInsights ApplicationInsights { get; set; }

        /// <summary>
        /// Url of the SharePoint Online site to connect to
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Url of the SharePoint Online Admin center to use. If omitted, it will try to automatically determine this.
        /// </summary>
        public string TenantAdminUrl { get; protected set; }

        /// <summary>
        /// Certificate used to set up the connection
        /// </summary>
        public X509Certificate2 Certificate { get; internal set; }

        /// <summary>
        /// Boolean indicating if when using Disconnect-PnPOnline to destruct this PnPConnection instance, if the certificate file should be deleted from C:\ProgramData\Microsoft\Crypto\RSA\MachineKeys
        /// </summary>
        public bool DeleteCertificateFromCacheOnDisconnect { get; internal set; }

        /// <summary>
        /// ClientContext to use to execute Client Side Object Model (CSOM) requests
        /// </summary>
        public ClientContext Context { get; set; }

        /// <summary>
        /// Tenant name to which the connection exists
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// If applicable, will return the object/principal ID of the User Assigned Managed Identity that is being used for this connection
        /// </summary>
        public string UserAssignedManagedIdentityObjectId { get; set; }
        
        /// <summary>
        /// If applicable, will return the client ID of the User Assigned Managed Identity that is being used for this connection
        /// </summary>
        public string UserAssignedManagedIdentityClientId { get; set; }

        /// <summary>
        /// If applicable, will return the Azure Resource ID of the User Assigned Managed Identity that is being used for this connection
        /// </summary>
        public string UserAssignedManagedIdentityAzureResourceId { get; set; }        

        /// <summary>
        /// Type of Azure cloud to connect to
        /// </summary>
        public AzureEnvironment AzureEnvironment { get; set; } = AzureEnvironment.Production;

        private string _graphEndPoint;

        #endregion

        #region Creators
        internal static PnPConnection CreateWithAccessToken(Uri url, string accessToken, string tenantAdminUrl)
        {
            using (var authManager = new PnP.Framework.AuthenticationManager(new System.Net.NetworkCredential("", accessToken).SecurePassword))
            {
                PnPClientContext context = null;
                ConnectionType connectionType = ConnectionType.O365;
                if (url != null)
                {
                    context = PnPClientContext.ConvertFrom(authManager.GetContext(url.ToString()));
                    context.ApplicationName = Resources.ApplicationName;
                    context.DisableReturnValueCache = true;
                    context.ExecutingWebRequest += (sender, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                    };
                    if (IsTenantAdminSite(context))
                    {
                        connectionType = ConnectionType.TenantAdmin;
                    }
                }

                var connection = new PnPConnection(context, connectionType, null, url != null ? url.ToString() : null, tenantAdminUrl, PnPPSVersionTag, InitializationType.Token);
                return connection;
            }
        }

        internal static PnPConnection CreateWithACSAppOnly(Uri url, string realm, string clientId, string clientSecret, string tenantAdminUrl, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
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

                        if (realm == null)
                        {
                            throw new Exception($"Could not determine realm for the target site '{url}'. Please validate that a site exists at this URL.");
                        }
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
                    context.ExecutingWebRequest += (sender, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                    };
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

        internal static PnPConnection CreateWithDeviceLogin(string clientId, string url, string tenantId, bool launchBrowser, CmdletMessageWriter messageWriter, AzureEnvironment azureEnvironment, CancellationTokenSource cancellationTokenSource)
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
                authManager = PnP.Framework.AuthenticationManager.CreateWithDeviceLogin(clientId, tenantId, (deviceCodeResult) =>
                 {
                     if (launchBrowser)
                     {
                         if (Utilities.OperatingSystem.IsWindows())
                         {
                             ClipboardService.SetText(deviceCodeResult.UserCode);
                             messageWriter.WriteWarning($"\n\nCode {deviceCodeResult.UserCode} has been copied to your clipboard\n\n");
                             BrowserHelper.GetWebBrowserPopup(deviceCodeResult.VerificationUrl, "Please log in", cancellationTokenSource: cancellationTokenSource, cancelOnClose: false, scriptErrorsSuppressed: false);
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
                try
                {
                    var clientContext = authManager.GetContext(url.ToString(), cancellationTokenSource.Token);

                    var context = PnPClientContext.ConvertFrom(clientContext);
                    context.ExecutingWebRequest += (sender, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                    };
                    var connectionType = ConnectionType.O365;

                    var spoConnection = new PnPConnection(context, connectionType, null, clientId, null, url.ToString(), null, PnPPSVersionTag, InitializationType.DeviceLogin)
                    {
                        ConnectionMethod = ConnectionMethod.DeviceLogin,
                        AzureEnvironment = azureEnvironment
                    };
                    return spoConnection;
                }
                catch (Microsoft.Identity.Client.MsalServiceException msalServiceException)
                {
                    if (msalServiceException.Message.StartsWith("AADSTS50059:"))
                    {
                        cancellationTokenSource.Cancel();
                        throw new Exception("Please specify -Tenant with either the tenant id or hostname.");
                    }
                    else
                    {
                        throw;
                    }

                }
            }
        }

        internal static PnPConnection CreateWithCert(Uri url, string clientId, string tenant, string tenantAdminUrl, AzureEnvironment azureEnvironment, X509Certificate2 certificate, bool certificateFromFile = false)
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
                context.ExecutingWebRequest += (sender, e) =>
                {
                    e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                };

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

        /// <summary>
        /// Creates a PnPConnection using a Managed Identity
        /// </summary>
        /// <param name="cmdlet">PowerShell instance hosting this execution</param>
        /// <param name="url">Url to the SharePoint Online site to connect to</param>
        /// <param name="tenantAdminUrl">Url to the SharePoint Online Admin Center site to connect to</param>
        /// <param name="userAssignedManagedIdentityObjectId">The Object/Principal ID of the User Assigned Managed Identity to use (optional)</param>
        /// <param name="userAssignedManagedIdentityClientId">The Client ID of the User Assigned Managed Identity to use (optional)</param>
        /// <param name="userAssignedManagedIdentityAzureResourceId">The Azure Resource ID of the User Assigned Managed Identity to use (optional)</param>
        /// <returns>Instantiated PnPConnection</returns>
        internal static PnPConnection CreateWithManagedIdentity(Cmdlet cmdlet, string url, string tenantAdminUrl, string userAssignedManagedIdentityObjectId = null, string userAssignedManagedIdentityClientId = null, string userAssignedManagedIdentityAzureResourceId = null)
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            string defaultResource = "https://graph.microsoft.com";
            if(url != null)
            {
                var resourceUri = new Uri(url);
                defaultResource = $"{resourceUri.Scheme}://{resourceUri.Authority}";
            }

            cmdlet.WriteVerbose("Acquiring token for resource " + defaultResource);
            var accessToken = TokenHandler.GetManagedIdentityTokenAsync(cmdlet, httpClient, defaultResource, userAssignedManagedIdentityObjectId, userAssignedManagedIdentityClientId, userAssignedManagedIdentityAzureResourceId).GetAwaiter().GetResult();

            using (var authManager = new PnP.Framework.AuthenticationManager(new System.Net.NetworkCredential("", accessToken).SecurePassword))
            {
                PnPClientContext context = null;
                ConnectionType connectionType = ConnectionType.O365;
                if (url != null)
                {
                    context = PnPClientContext.ConvertFrom(authManager.GetContext(url.ToString()));
                    context.ApplicationName = Resources.ApplicationName;
                    context.DisableReturnValueCache = true;
                    context.ExecutingWebRequest += (sender, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                    };
                    if (IsTenantAdminSite(context))
                    {
                        connectionType = ConnectionType.TenantAdmin;
                    }
                }

                var connection = new PnPConnection(context, connectionType, null, url != null ? url.ToString() : null, tenantAdminUrl, PnPPSVersionTag, InitializationType.ManagedIdentity);
                connection.UserAssignedManagedIdentityObjectId = userAssignedManagedIdentityObjectId;
                connection.UserAssignedManagedIdentityClientId = userAssignedManagedIdentityClientId;
                return connection;
            }
        }

        internal static PnPConnection CreateWithCredentials(Cmdlet cmdlet, Uri url, PSCredential credentials, bool currentCredentials, string tenantAdminUrl, AzureEnvironment azureEnvironment = AzureEnvironment.Production, string clientId = null, string redirectUrl = null, bool onPrem = false, InitializationType initializationType = InitializationType.Credentials)
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
                            context.ExecutingWebRequest += (sender, e) =>
                            {
                                e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                            };
                            context.ExecuteQueryRetry();
                            cmdlet.WriteVerbose("Acquiring token");
                            var accesstoken = authManager.GetAccessTokenAsync(url.ToString()).GetAwaiter().GetResult();
                            cmdlet.WriteVerbose("Token acquired");
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
                            context.ExecutingWebRequest += (sender, e) =>
                            {
                                e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                            };
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
                    context.ExecutingWebRequest += (sender, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                    };
                }

                spoConnection = new PnPConnection(context, ConnectionType.O365, credentials, url.ToString(), tenantAdminUrl, PnPPSVersionTag, initializationType)
                {
                    ConnectionMethod = Model.ConnectionMethod.Credentials,
                    AzureEnvironment = azureEnvironment,
                };
            }

            return spoConnection;
        }

        internal static PnPConnection CreateWithWeblogin(Uri url, string tenantAdminUrl, bool clearCookies, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            if (Utilities.OperatingSystem.IsWindows())
            {
                // Log in to a specific page on the tenant which is known to be performant
                var webLoginClientContext = BrowserHelper.GetWebLoginClientContext(url.ToString(), clearCookies, scriptErrorsSuppressed: false, loginRequestUri: new Uri(url, "/_layouts/15/settings.aspx"));

                // Ensure the login process has been completed
                if (webLoginClientContext == null)
                {
                    return null;
                }

                var context = PnPClientContext.ConvertFrom(webLoginClientContext);
                context.ExecutingWebRequest += (sender, e) =>
                {
                    e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                };
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

        internal static PnPConnection CreateWithInteractiveLogin(Uri uri, string clientId, string tenantAdminUrl, bool launchBrowser, AzureEnvironment azureEnvironment, CancellationTokenSource cancellationTokenSource, bool forceAuthentication, string tenant)
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
                tenant,
                successMessageHtml: $"You successfully authenticated with PnP PowerShell. Feel free to close this {(launchBrowser ? "tab" : "window")}.",
                failureMessageHtml: $"You did not authenticate with PnP PowerShell. Feel free to close this browser {(launchBrowser ? "tab" : "window")}.",
                azureEnvironment: azureEnvironment);
            }
            using (authManager)
            {
                var clientContext = authManager.GetContext(uri.ToString(), cancellationTokenSource.Token);
                var context = PnPClientContext.ConvertFrom(clientContext);
                context.ExecutingWebRequest += (sender, e) =>
                {
                    e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                };
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

        /// <summary>
        /// Creates a PnPConnection using a Azure AD Workload Identity
        /// </summary>
        /// <param name="cmdlet">PowerShell instance hosting this execution</param>
        /// <param name="url">Url to the SharePoint Online site to connect to</param>
        /// <param name="tenantAdminUrl">Url to the SharePoint Online Admin Center site to connect to</param>
        /// <returns>Instantiated PnPConnection</returns>
        internal static PnPConnection CreateWithAzureADWorkloadIdentity(Cmdlet cmdlet, string url, string tenantAdminUrl)
        {            
            string defaultResource = "https://graph.microsoft.com/.default";
            if (url != null)
            {
                var resourceUri = new Uri(url);
                defaultResource = $"{resourceUri.Scheme}://{resourceUri.Authority}/.default";
            }

            cmdlet.WriteVerbose("Acquiring token for resource " + defaultResource);
            var accessToken = TokenHandler.GetAzureADWorkloadIdentityTokenAsync(cmdlet, defaultResource).GetAwaiter().GetResult();

            using (var authManager = new PnP.Framework.AuthenticationManager(new System.Net.NetworkCredential("", accessToken).SecurePassword))
            {
                PnPClientContext context = null;
                ConnectionType connectionType = ConnectionType.O365;
                if (url != null)
                {
                    context = PnPClientContext.ConvertFrom(authManager.GetContext(url.ToString()));
                    context.ApplicationName = Resources.ApplicationName;
                    context.DisableReturnValueCache = true;
                    context.ExecutingWebRequest += (sender, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                    };
                    if (IsTenantAdminSite(context))
                    {
                        connectionType = ConnectionType.TenantAdmin;
                    }
                }

                var connection = new PnPConnection(context, connectionType, null, url != null ? url.ToString() : null, tenantAdminUrl, PnPPSVersionTag, InitializationType.AzureADWorkloadIdentity);
                
                return connection;
            }
        }
        #endregion

        #region Constructors

        private PnPConnection(ClientContext context, ConnectionType connectionType, PSCredential credential, string clientId, string clientSecret, string url, string tenantAdminUrl, string pnpVersionTag, InitializationType initializationType)
        : this(context, connectionType, credential, url, tenantAdminUrl, pnpVersionTag, initializationType)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        private PnPConnection(ClientContext context,
                                    ConnectionType connectionType,
                                    PSCredential credential,
                                    string url,
                                    string tenantAdminUrl,
                                    string pnpVersionTag,
                                    InitializationType initializationType)
        {
            InitializeTelemetry(context, initializationType);
            var coreAssembly = Assembly.GetExecutingAssembly();

            var connectionMethod = ConnectionMethod.Credentials;
            if(initializationType == InitializationType.AzureADWorkloadIdentity)
            {
                connectionMethod = ConnectionMethod.AzureADWorkloadIdentity;
            }
            else if(initializationType == InitializationType.ManagedIdentity)
            {
                connectionMethod = ConnectionMethod.ManagedIdentity;
            }

            if (context != null)
            {
                Context = context;
            }
            ConnectionType = connectionType;
            TenantAdminUrl = tenantAdminUrl;

            PSCredential = credential;
            PnPVersionTag = pnpVersionTag;
            ContextCache = new List<ClientContext> { context };
            if (!string.IsNullOrEmpty(url))
            {
                Url = (new Uri(url)).AbsoluteUri;
            }
            ConnectionMethod = connectionMethod;
            ClientId = PnPManagementShellClientId;
        }

        #endregion

        #region Methods
        internal void RestoreCachedContext(string url)
        {
            Context = ContextCache.FirstOrDefault(c => new Uri(c.Url).AbsoluteUri == new Uri(url).AbsoluteUri);
            pnpContext = null;
        }

        internal void CacheContext()
        {
            if (Context == null) return;

            var c = ContextCache.FirstOrDefault(cc => new Uri(cc.Url).AbsoluteUri == new Uri(Context.Url).AbsoluteUri);
            if (c == null)
            {
                ContextCache.Add(Context);
            }
        }

        internal ClientContext CloneContext(string url)
        {
            var context = ContextCache.FirstOrDefault(c => new Uri(c.Url).AbsoluteUri == new Uri(url).AbsoluteUri);
            if (context == null)
            {
                context = Context.Clone(url);
                context.ExecuteQueryRetry();
                ContextCache.Add(context);
            }
            pnpContext = null;
            return context;
        }

        internal string GraphEndPoint
        {
            get
            {
                if (string.IsNullOrEmpty(_graphEndPoint))
                {
                    if (Context != null)
                    {
                        var settings = Context.GetContextSettings();
                        if (settings.AuthenticationManager != null)
                        {
                            _graphEndPoint = settings.AuthenticationManager.GetGraphEndPoint();
                        }
                    }
                    else
                    {
                        _graphEndPoint = "graph.microsoft.com";
                    }
                }
                return _graphEndPoint;
            }
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

        internal void InitializeTelemetry(ClientContext context, InitializationType initializationType)
        {
            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var telemetryFile = System.IO.Path.Combine(userProfile, ".pnppowershelltelemetry");

            var enableTelemetry = true;
            if (Environment.GetEnvironmentVariable("PNP_DISABLETELEMETRY") != null)
            {
                enableTelemetry = false;
            }
            // We have old systems to disable telemetry. 
            // If telemetry should be enabled is based on:
            // a. The environmentvariable not having been set OR having been set and having the value false
            //    AND
            // b. The telemetry file not existing OR existing and having the word allow in it
            if (enableTelemetry == true)
            {
                enableTelemetry = ((Environment.GetEnvironmentVariable("PNPPOWERSHELL_DISABLETELEMETRY") == null || Environment.GetEnvironmentVariable("PNPPOWERSHELL_DISABLETELEMETRY").Equals("false", StringComparison.InvariantCultureIgnoreCase)) &&
                                   (!System.IO.File.Exists(telemetryFile) || System.IO.File.ReadAllText(telemetryFile).Equals("allow", StringComparison.InvariantCultureIgnoreCase)));
            }
            // Load Application Insights if telemetry should be enabled
            if (enableTelemetry)
            {
                var serverLibraryVersion = "";
                var serverVersion = "";
                if (context != null)
                {
                    try
                    {
                        if (context.ServerLibraryVersion != null)
                        {
                            serverLibraryVersion = context.ServerLibraryVersion.ToString();
                        }
                        if (context.ServerVersion != null)
                        {
                            serverVersion = context.ServerVersion.ToString();
                        }
                    }
                    catch { }
                }

                ApplicationInsights = new ApplicationInsights();
                var coreAssembly = Assembly.GetExecutingAssembly();
                var operatingSystem = Utilities.OperatingSystem.GetOSString();

                ApplicationInsights.Initialize(serverLibraryVersion, serverVersion, initializationType.ToString(), ((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version.ToString(), operatingSystem, PSVersion);
                ApplicationInsights.TrackEvent("Connect-PnPOnline");
            }
        }

        private static string PSVersion => (PSVersionLazy.Value);

        private static readonly Lazy<string> PSVersionLazy = new Lazy<string>(
            () =>

        {
            var caller = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == "System.Management.Automation");
            //var caller = Assembly.GetCallingAssembly();
            var psVersionType = caller.GetType("System.Management.Automation.PSVersionInfo");
            if (null != psVersionType)
            {
                PropertyInfo propInfo = psVersionType.GetProperty("PSVersion");
                if (null == propInfo)
                {
                    propInfo = psVersionType.GetProperty("PSVersion", BindingFlags.NonPublic | BindingFlags.Static);
                }
                var getter = propInfo.GetGetMethod(true);
                var version = getter.Invoke(null, new object[] { });

                if (null != version)
                {
                    var versionType = version.GetType();
                    var versionProperty = versionType.GetProperty("Major");
                    return ((int)versionProperty.GetValue(version)).ToString();
                }
            }
            return "";
        });

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

        internal static string GetRealmFromTargetUrl(Uri targetApplicationUri)
        {
            var client = Framework.Http.PnPHttpClient.Instance.GetHttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");

            var response = client.GetAsync(targetApplicationUri + "/_vti_bin/client.svc").GetAwaiter().GetResult();
            if (response == null || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            var bearerResponseHeaderValues = response.Headers.GetValues("WWW-Authenticate");
            string bearerResponseHeader = string.Join("", bearerResponseHeaderValues);

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
            return null;
        }

        internal static void CleanupCryptoMachineKey(X509Certificate2 certificate)
        {
            if (!certificate.HasPrivateKey)
            {
                // If somehow a public key certificate was passed in, we can't clean it up, thus we have nothing to do here
                return;
            }
            if (Utilities.OperatingSystem.IsWindows())
            {
                var privateKey = (certificate.GetRSAPrivateKey() as RSACng)?.Key;
                // var privateKey = (certificate.PrivateKey as RSACng)?.Key;
                if (privateKey == null)
                    return;

                string uniqueKeyContainerName = privateKey.UniqueName;
                if (uniqueKeyContainerName == null)
                {
                    RSACryptoServiceProvider rsaCSP = certificate.GetRSAPrivateKey() as RSACryptoServiceProvider;
                    uniqueKeyContainerName = rsaCSP.CspKeyContainerInfo.KeyContainerName;
                }
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
        }
        #endregion
    }
}