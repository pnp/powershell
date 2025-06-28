using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.Framework;
using PnP.Framework.Diagnostics;
using PnP.Framework.Utilities.Context;
using PnP.PowerShell.ALC;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

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
        internal HttpClient HttpClient => Framework.Http.PnPHttpClient.Instance.GetHttpClient();

        private PnPContext _pnpContext { get; set; }

        internal PnPContext PnPContext
        {
            get
            {
                if (_pnpContext == null && Context != null)
                {
                    _pnpContext = PnPCoreSdk.Instance.GetPnPContext(Context);
                }
                return _pnpContext;
            }
        }
        /// <summary>
        /// User Agent identifier to use on all connections being made to the APIs
        /// </summary>
        internal string UserAgent { get; set; }

        internal static Framework.AuthenticationManager CachedAuthenticationManager { get; set; }

        /// <summary>
        /// Identifier set on the SharePoint ClientContext as the ClientTag to identify the source of the requests to SharePoint
        /// </summary>
        internal string PnPVersionTag { get; set; }

        internal static List<ClientContext> ContextCache { get; set; }

        /// <summary>
        /// Indicates the method used to establish a connection for the authentication
        /// </summary>
        public ConnectionMethod ConnectionMethod { get; set; }

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

        internal PnP.Framework.AuthenticationManager AuthenticationManager { get; set; }

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

        internal static PnPConnection CreateWithDeviceLogin(string clientId, string url, string tenantId, CmdletMessageWriter messageWriter, AzureEnvironment azureEnvironment, CancellationTokenSource cancellationTokenSource, bool persistLogin, System.Management.Automation.Host.PSHost host)
        {
            if (persistLogin)
            {
                EnableCaching(url, clientId);
            }
            if (CacheEnabled(url, clientId))
            {
                WriteCacheEnabledMessage(host);
            }
            var connectionUri = new Uri(url);
            var scopes = new[] { $"{connectionUri.Scheme}://{connectionUri.Authority}//.default" }; // the second double slash is not a typo.
            Framework.AuthenticationManager authManager = null;
            if (CachedAuthenticationManager != null)
            {
                authManager = CachedAuthenticationManager;
                CachedAuthenticationManager = null;
            }
            else
            {
                authManager = Framework.AuthenticationManager.CreateWithDeviceLogin(clientId, tenantId, (deviceCodeResult) =>
                 {
                     try
                     {
                         ClipboardService.SetText(deviceCodeResult.UserCode);
                     }
                     catch
                     {
                     }
                     messageWriter.LogWarning($"\n\nCode {deviceCodeResult.UserCode} has been copied to your clipboard and a new tab in the browser has been opened. Please paste this code in there and proceed.\n\n");
                     BrowserHelper.OpenBrowserForInteractiveLogin(deviceCodeResult.VerificationUrl, BrowserHelper.FindFreeLocalhostRedirectUri(), cancellationTokenSource);

                     return Task.FromResult(0);
                 }, azureEnvironment, tokenCacheCallback: async (tokenCache) =>
                 {
                     await MSALCacheHelper(tokenCache, url, clientId);
                 });
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
                    spoConnection.AuthenticationManager = authManager;
                    return spoConnection;
                }
                catch (Microsoft.Identity.Client.MsalServiceException msalServiceException)
                {
                    if (msalServiceException.Message.StartsWith("AADSTS50059:"))
                    {
                        cancellationTokenSource.Cancel();
                        throw new Exception("Default authentication request failed. Please specify the -Tenant parameter with either the tenant id or hostname to authenticate.");
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
            Framework.AuthenticationManager authManager = null;
            if (CachedAuthenticationManager != null)
            {
                authManager = CachedAuthenticationManager;
                CachedAuthenticationManager = null;
            }
            else
            {
                authManager = Framework.AuthenticationManager.CreateWithCertificate(clientId, certificate, tenant, azureEnvironment: azureEnvironment);
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
        internal static PnPConnection CreateWithManagedIdentity(string url, string tenantAdminUrl, string userAssignedManagedIdentityObjectId = null, string userAssignedManagedIdentityClientId = null, string userAssignedManagedIdentityAzureResourceId = null)
        {
            // Define the type of Managed Identity that will be used
            ManagedIdentityType managedIdentityType = ManagedIdentityType.SystemAssigned;
            string managedIdentityUserAssignedIdentifier = null;

            if (!string.IsNullOrEmpty(userAssignedManagedIdentityObjectId))
            {
                managedIdentityType = ManagedIdentityType.UserAssignedByObjectId;
                managedIdentityUserAssignedIdentifier = userAssignedManagedIdentityObjectId;
            }
            if (!string.IsNullOrEmpty(userAssignedManagedIdentityClientId))
            {
                managedIdentityType = ManagedIdentityType.UserAssignedByClientId;
                managedIdentityUserAssignedIdentifier = userAssignedManagedIdentityClientId;
            }
            if (!string.IsNullOrEmpty(userAssignedManagedIdentityAzureResourceId))
            {
                managedIdentityType = ManagedIdentityType.UserAssignedByResourceId;
                managedIdentityUserAssignedIdentifier = userAssignedManagedIdentityAzureResourceId;
            }

            // Ensure if its not a System Assigned Managed Identity, that we an identifier pointing to the user assigned Managed Identity
            if (managedIdentityType != ManagedIdentityType.SystemAssigned && string.IsNullOrEmpty(managedIdentityUserAssignedIdentifier))
            {
                throw new InvalidOperationException("Unable to use a User Assigned Managed Identity without passing in an identifier for the User Assigned Managed Identity.");
            }

            // Set up the AuthenticationManager in PnP Framework to use a Managed Identity context
            using (var authManager = Framework.AuthenticationManager.CreateWithManagedIdentity(null, null, managedIdentityType, managedIdentityUserAssignedIdentifier))
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

                // Set up PnP PowerShell to use a Managed Identity
                var connection = new PnPConnection(context, connectionType, null, url?.ToString(), tenantAdminUrl, PnPPSVersionTag, InitializationType.ManagedIdentity)
                {
                    UserAssignedManagedIdentityObjectId = userAssignedManagedIdentityObjectId,
                    UserAssignedManagedIdentityClientId = userAssignedManagedIdentityClientId,
                    UserAssignedManagedIdentityAzureResourceId = userAssignedManagedIdentityAzureResourceId,
                    ConnectionMethod = ConnectionMethod.ManagedIdentity,
                };
                return connection;
            }
        }

        internal static PnPConnection CreateWithCredentials(Cmdlet cmdlet, Uri url, PSCredential credentials, bool currentCredentials, string tenantAdminUrl, bool persistLogin, System.Management.Automation.Host.PSHost host, AzureEnvironment azureEnvironment = AzureEnvironment.Production, string clientId = null, string redirectUrl = null, bool onPrem = false, InitializationType initializationType = InitializationType.Credentials)
        {
            if (persistLogin)
            {
                EnableCaching(url.ToString(), clientId);
            }
            if (CacheEnabled(url.ToString(), clientId))
            {
                WriteCacheEnabledMessage(host);
            }
            var context = new PnPClientContext(url.AbsoluteUri)
            {
                ApplicationName = Resources.ApplicationName,
                DisableReturnValueCache = true,
            };
            PnPConnection spoConnection = null;
            if (!onPrem)
            {
                var tenantId = string.Empty;
                try
                {
                    PnP.Framework.AuthenticationManager authManager = null;
                    if (CachedAuthenticationManager != null)
                    {
                        authManager = CachedAuthenticationManager;
                        CachedAuthenticationManager = null;
                    }
                    else
                    {
                        authManager = PnP.Framework.AuthenticationManager.CreateWithCredentials(clientId, credentials.UserName, credentials.Password, redirectUrl, azureEnvironment, tokenCacheCallback: async (tokenCache) =>
                        {
                            await MSALCacheHelper(tokenCache, url.ToString(), clientId);
                        });
                    }
                    using (authManager)
                    {
                        var clientContext = authManager.GetContext(url.ToString());
                        context = PnPClientContext.ConvertFrom(clientContext);

                        context.ExecutingWebRequest += (sender, e) =>
                        {
                            e.WebRequestExecutor.WebRequest.UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version} ({System.Environment.OSVersion.VersionString})";
                        };
                        context.ExecuteQueryRetry();
                        Log.Debug("PnPConnection", "Acquiring token");
                        var accesstoken = authManager.GetAccessTokenAsync(url.ToString()).GetAwaiter().GetResult();
                        Log.Debug("PnPConnection", "Token acquired");
                        var parsedToken = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(accesstoken);
                        tenantId = parsedToken.Claims.FirstOrDefault(c => c.Type == "tid").Value;

                        spoConnection = new PnPConnection(context, ConnectionType.O365, credentials, url.ToString(), tenantAdminUrl, PnPPSVersionTag, initializationType);

                        spoConnection.ConnectionMethod = ConnectionMethod.Credentials;
                        spoConnection.AzureEnvironment = azureEnvironment;
                        spoConnection.Tenant = tenantId;
                        spoConnection.ClientId = clientId;

                        spoConnection.AuthenticationManager = authManager;
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
                spoConnection.ConnectionType = connectionType;
                return spoConnection;
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
                    ClientId = clientId
                };
            }

            return spoConnection;
        }

        internal static PnPConnection CreateWithInteractiveLogin(Uri uri, string clientId, string tenantAdminUrl, AzureEnvironment azureEnvironment, CancellationTokenSource cancellationTokenSource, bool forceAuthentication, string tenant, bool enableLoginWithWAM, bool persistLogin, System.Management.Automation.Host.PSHost host)
        {
            if (persistLogin)
            {
                EnableCaching(uri.ToString(), clientId);
            }
            if (CacheEnabled(uri.ToString(), clientId))
            {
                WriteCacheEnabledMessage(host);
            }

            var htmlMessageSuccess = "<html lang=en><meta charset=utf-8><title>PnP PowerShell - Sign In</title><meta content=\"width=device-width,initial-scale=1\"name=viewport><style>html{height:100%}.message-container{flex-grow:1;display:flex;align-items:center;justify-content:center;margin:0 30px}body{box-sizing:border-box;min-height:100%;display:flex;flex-direction:column;color:#fff;font-family:\"Segoe UI\",\"Helvetica Neue\",Helvetica,Arial,sans-serif;background-color:#2c2c32;margin:0;padding:15px 30px}.message{font-weight:300;font-size:1.4rem}.branding{background-image:url(data:image/png;base64,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);background-repeat:no-repeat;padding-left:26px;font-size:20px;letter-spacing:-.04rem;font-weight:400;height:26px;color:#fff;background-position:left center;text-decoration:none}</style><a class=branding href=https://pnp.github.io/powershell>PnP PowerShell</a><div class=message-container><div class=message>You are signed in now and can close this page.</div></div>";
            var htmlMessageFailure = "<html lang=en><meta charset=utf-8><title>PnP PowerShell - Sign In</title><meta content=\"width=device-width,initial-scale=1\"name=viewport><style>html{{height:100%}}.error-text{{color:red;font-size:1rem}}.message-container{{flex-grow:1;display:flex;align-items:center;justify-content:center;margin:0 30px}}body{{box-sizing:border-box;min-height:100%;display:flex;flex-direction:column;color:#fff;font-family:\"Segoe UI\",\"Helvetica Neue\",Helvetica,Arial,sans-serif;background-color:#2c2c32;margin:0;padding:15px 30px}}.message{{font-weight:300;font-size:1.4rem}}.branding{{background-image:url(data:image/png;base64,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);background-repeat:no-repeat;height:26px;padding-left:26px;font-size:20px;letter-spacing:-.04rem;font-weight:400;color:#fff;background-position:left center;text-decoration:none}}</style><a class=branding href=https://pnp.github.io/powershell>PnP PowerShell</a><div class=message-container><div class=message>An error occured while signing in: {0}</div></div>";

            PnP.Framework.AuthenticationManager authManager = null;
            if (PnPConnection.CachedAuthenticationManager != null && !forceAuthentication)
            {
                authManager = PnPConnection.CachedAuthenticationManager;
                PnPConnection.CachedAuthenticationManager = null;
            }
            else
            {
                authManager = PnP.Framework.AuthenticationManager.CreateWithInteractiveWebBrowserLogin(clientId, (url, port) =>
                {
                    BrowserHelper.OpenBrowserForInteractiveLogin(url, port, cancellationTokenSource);
                },
                tenant,
                htmlMessageSuccess,
                htmlMessageFailure,
                azureEnvironment: azureEnvironment, tokenCacheCallback: async (tokenCache) =>
                {
                    await MSALCacheHelper(tokenCache, uri.ToString(), clientId);
                }, useWAM: enableLoginWithWAM);
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
                spoConnection.AuthenticationManager = authManager;
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
        internal static PnPConnection CreateWithAzureADWorkloadIdentity(string url, string tenantAdminUrl)
        {
            string defaultResource = "https://graph.microsoft.com/.default";
            if (url != null)
            {
                var resourceUri = new Uri(url);
                defaultResource = $"{resourceUri.Scheme}://{resourceUri.Authority}/.default";
            }

            PnP.Framework.Diagnostics.Log.Debug("PnPConnection", "Acquiring token for resource " + defaultResource);
            var accessToken = TokenHandler.GetAzureADWorkloadIdentityTokenAsync(defaultResource).GetAwaiter().GetResult();

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

        /// <summary>
        /// Creates a PnPConnection using a Federated Identity
        /// </summary>
        /// <param name="url">Url to the SharePoint Online site to connect to</param>
        /// <param name="tenantAdminUrl">Url to the SharePoint Online Admin Center site to connect to</param>
        /// <param name="appClientId">The Client ID of the Federated Identity application</param>
        /// <param name="tenantId">The Tenant ID of the Federated Identity application</param>
        /// <returns>Instantiated PnPConnection</returns>
        /// <remarks>
        /// This method is used to create a PnPConnection using a Federated Identity, which allows for authentication without the need for a client secret.
        /// </remarks>
        internal static PnPConnection CreateWithFederatedIdentity(string url, string tenantAdminUrl, string appClientId, string tenantId)
        {
            string defaultResource = "https://graph.microsoft.com/.default";
            if (url != null)
            {
                var resourceUri = new Uri(url);
                defaultResource = $"{resourceUri.Scheme}://{resourceUri.Authority}/.default";
            }

            PnP.Framework.Diagnostics.Log.Debug("PnPConnection", "Acquiring token for resource " + defaultResource);
            var accessToken = TokenHandler.GetFederatedIdentityTokenAsync(appClientId, tenantId, defaultResource).GetAwaiter().GetResult();

            // Set up the AuthenticationManager in PnP Framework to use a Federated Identity context
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

                var connection = new PnPConnection(context, connectionType, null, url != null ? url.ToString() : null, tenantAdminUrl, PnPPSVersionTag, InitializationType.FederatedIdentity);
                connection.ClientId = appClientId;
                connection.Tenant = tenantId;
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

            var connectionMethod = ConnectionMethod.Credentials;
            if (initializationType == InitializationType.AzureADWorkloadIdentity)
            {
                connectionMethod = ConnectionMethod.AzureADWorkloadIdentity;
            }
            else if (initializationType == InitializationType.ManagedIdentity)
            {
                connectionMethod = ConnectionMethod.ManagedIdentity;
            }
            else if (initializationType == InitializationType.FederatedIdentity)
            {
                connectionMethod = ConnectionMethod.FederatedIdentity;
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
                Url = new Uri(url).AbsoluteUri;
            }
            ConnectionMethod = connectionMethod;
            ClientId = PnPManagementShellClientId;
        }

        #endregion

        #region Methods
        internal void RestoreCachedContext(string url)
        {
            Context = ContextCache.FirstOrDefault(c => new Uri(c.Url).AbsoluteUri == new Uri(url).AbsoluteUri);
            _pnpContext = null;
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
            _pnpContext = null;
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

        public static bool IsTenantAdminSite(ClientRuntimeContext clientContext)
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
            var telemetryFile = Path.Combine(userProfile, ".pnppowershelltelemetry");

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

                ApplicationInsights.Initialize(serverLibraryVersion, serverVersion, initializationType.ToString(), ((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version.ToString(), operatingSystem, PSUtility.PSVersion);
                ApplicationInsights.TrackEvent("Connect-PnPOnline");
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
#pragma warning disable CA1416 // Validate platform compatibilit
                    RSACryptoServiceProvider rsaCSP = certificate.GetRSAPrivateKey() as RSACryptoServiceProvider;
                    uniqueKeyContainerName = rsaCSP.CspKeyContainerInfo.KeyContainerName;
#pragma warning restore CA1416 // Validate platform compatibility
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

        private static async Task MSALCacheHelper(ITokenCache tokenCache, string url, string clientid)
        {
            const string CacheSchemaName = "pnp.powershell.tokencache";
            string cacheDir = Path.Combine(MsalCacheHelper.UserRootDirectory, @".m365pnppowershell");

            if (CacheEnabled(url, clientid))
            {
                try
                {
                    StorageCreationPropertiesBuilder builder =
                         new StorageCreationPropertiesBuilder("pnp.msal.cache", cacheDir)
                         .WithMacKeyChain(
                            serviceName: $"{CacheSchemaName}.service",
                            accountName: $"{CacheSchemaName}.account")
                        .WithLinuxKeyring(
                            schemaName: CacheSchemaName,
                            collection: MsalCacheHelper.LinuxKeyRingDefaultCollection,
                            secretLabel: "MSAL token cache for PnP PowerShell.",
                            attribute1: new KeyValuePair<string, string>("Version", "1"),
                            attribute2: new KeyValuePair<string, string>("Product", "PnPPowerShell"));

                    var storage = builder.Build();
                    var cacheHelper = await MsalCacheHelper.CreateAsync(storage).ConfigureAwait(false);
                    cacheHelper.VerifyPersistence();

                    cacheHelper.RegisterCache(tokenCache);
                }
                catch (MsalCachePersistenceException)
                {
                    PnP.Framework.Diagnostics.Log.Debug("PnPConnection", "Cache persistence failed. Trying again.");
                    var storage =
                     new StorageCreationPropertiesBuilder("pnp.msal.cache", cacheDir)
                     .WithMacKeyChain(
                        serviceName: $"{CacheSchemaName}.service",
                        accountName: $"{CacheSchemaName}.account")
                     .WithLinuxUnprotectedFile()
                    .Build();
                    var cacheHelper = await MsalCacheHelper.CreateAsync(storage).ConfigureAwait(false);

                    cacheHelper.RegisterCache(tokenCache);
                }
            }
        }

        internal static bool CacheEnabled(string url, string clientid)
        {
            var settings = Settings.Current;

            var cacheEntries = settings.Cache;
            var urls = GetCheckUrls(url);
            var entry = settings.Cache?.FirstOrDefault(c => urls.Contains(c.Url) && c.ClientId == clientid);
            if (entry != null && entry.Enabled)
            {
                return true;
            }
            return false;
        }

        internal static string GetCacheClientId(string url)
        {
            var settings = Settings.Current;

            var cacheEntries = settings.Cache;
            var urls = GetCheckUrls(url);
            var entry = settings.Cache?.FirstOrDefault(c => urls.Contains(c.Url));
            if (entry != null && entry.Enabled)
            {
                return entry.ClientId;
            }
            return null;
        }

        private static List<string> GetCheckUrls(string url)
        {
            var urls = new List<string>();
            var uri = new Uri(url);
            var baseAuthority = uri.Authority;
            baseAuthority = baseAuthority.Replace("-admin.sharepoint.com", ".sharepoint.com").Replace("-my.sharepoint.com", ".sharepoint.com");
            var baseUri = new Uri($"https://{baseAuthority}");
            var host = baseUri.Host.Split('.')[0];
            urls = [$"https://{host}.sharepoint.com", $"https://{host}-my.sharepoint.com", $"https://{host}-admin.sharepoint.com"];

            return urls;
        }

        private static void EnableCaching(string url, string clientid)
        {
            var urls = GetCheckUrls(url);
            var entry = Settings.Current.Cache?.FirstOrDefault(c => urls.Contains(c.Url) && c.ClientId == clientid);
            if (entry != null)
            {
                entry.Enabled = true;
            }
            else
            {
                var baseAuthority = new Uri(url).Authority.Replace("-admin.sharepoint.com", ".sharepoint.com").Replace("-my.sharepoint.com", ".sharepoint.com");
                var baseUrl = $"https://{baseAuthority}";
                Settings.Current.Cache.Add(new TokenCacheConfiguration() { ClientId = clientid, Url = baseUrl, Enabled = true });
            }
            Settings.Current.Save();
        }

        private static void WriteCacheEnabledMessage(PSHost host)
        {
            host.UI.WriteWarningLine("Connecting using token cache. See https://pnp.github.io/powershell/articles/persistedlogin.html for more information.");
        }

        internal static void ClearCache(PnPConnection connection)
        {
            var urls = GetCheckUrls(connection.Url);
            var entry = Settings.Current.Cache?.FirstOrDefault(c => urls.Contains(c.Url) && c.ClientId == connection.ClientId);
            if (entry != null)
            {
                Settings.Current.Cache.Remove(entry);
                Settings.Current.Save();
            }
            if (connection.AuthenticationManager != null)
            {
                connection.AuthenticationManager.ClearTokenCache();
            }
        }
        #endregion
    }
}