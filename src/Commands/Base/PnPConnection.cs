using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using PnP.Framework;
using PnP.PowerShell.ALC;

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


        private PnPContext pnpContext { get; set; }

        internal PnPContext PnPContext
        {
            get
            {
                if (pnpContext == null && Context != null)
                {
                    pnpContext = PnP.Framework.PnPCoreSdk.Instance.GetPnPContext(Context, UserAgent);
                }
                return pnpContext;
            }
        }
        /// <summary>
        /// User Agent identifier to use on all connections being made to the APIs
        /// </summary>
        internal string UserAgent { get; set; }

        internal static AuthenticationManager CachedAuthenticationManager { get; set; }

        internal ConnectionMethod ConnectionMethod { get; set; }

        /// <summary>
        /// Identifier set on the SharePoint ClientContext as the ClientTag to identify the source of the requests to SharePoint
        /// </summary>
        internal string PnPVersionTag { get; set; }

        internal static List<ClientContext> ContextCache { get; set; }

        public static PnPConnection CurrentConnection { get; internal set; }
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

        //public TelemetryClient TelemetryClient { get; set; }
        public ApplicationInsights ApplicationInsights { get; set; }

        public string Url { get; protected set; }

        public string TenantAdminUrl { get; protected set; }

        /// <summary>
        /// Certificate used to set up the connection
        /// </summary>
        public X509Certificate2 Certificate { get; internal set; }

        /// <summary>
        /// Boolean indicating if when using Disconnect-PnPOnline to destruct this PnPConnection instance, if the certificate file should be deleted from C:\ProgramData\Microsoft\Crypto\RSA\MachineKeys
        /// </summary>
        public bool DeleteCertificateFromCacheOnDisconnect { get; internal set; }

        public ClientContext Context { get; set; }

        /// <summary>
        /// Tenant name to which the connection exists
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// Defines if this is a managed identity connection for use in cloud shell
        /// </summary>
        internal bool ManagedIdentity { get; set; }
        public AzureEnvironment AzureEnvironment { get; set; } = AzureEnvironment.Production;

        #endregion

        #region Constructors

        internal PnPConnection(ClientContext context, ConnectionType connectionType, PSCredential credential, string clientId, string clientSecret, string url, string tenantAdminUrl, string pnpVersionTag, InitializationType initializationType)
        : this(context, connectionType, credential, url, tenantAdminUrl, pnpVersionTag, initializationType)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        internal PnPConnection(ClientContext context,
                                    ConnectionType connectionType,
                                    PSCredential credential,
                                    string url,
                                    string tenantAdminUrl,
                                    string pnpVersionTag,
                                    InitializationType initializationType)
        {
            InitializeTelemetry(context, initializationType);
            var coreAssembly = Assembly.GetExecutingAssembly();
            UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version}";
            //if (context == null)
            //    throw new ArgumentNullException(nameof(context));
            if (context != null)
            {
                Context = context;
                Context.ExecutingWebRequest += Context_ExecutingWebRequest;
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
            ConnectionMethod = ConnectionMethod.Credentials;
            ClientId = PnPManagementShellClientId;
        }

        internal PnPConnection(string pnpVersionTag, InitializationType initializationType, string tenantAdminUrl)
        {
            InitializeTelemetry(null, initializationType);
            var coreAssembly = Assembly.GetExecutingAssembly();
            UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version}";
            //if (context == null)
            //    throw new ArgumentNullException(nameof(context));
            ConnectionType = ConnectionType.O365;
            PnPVersionTag = pnpVersionTag;
            TenantAdminUrl = tenantAdminUrl;
            ConnectionMethod = ConnectionMethod.ManagedIdentity;
            ManagedIdentity = true;
        }

        #endregion

        #region Methods
        private void Context_ExecutingWebRequest(object sender, WebRequestEventArgs e)
        {
            e.WebRequestExecutor.WebRequest.UserAgent = UserAgent;
        }

        internal void RestoreCachedContext(string url)
        {
            Context = ContextCache.FirstOrDefault(c => new Uri(c.Url).AbsoluteUri == new Uri(url).AbsoluteUri);
            pnpContext = null;
        }

        internal void CacheContext()
        {
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

        internal string GetGraphEndPoint()
        {
            if(Context != null)
            {
                var settings = Context.GetContextSettings();
                if(settings.AuthenticationManager != null)
                {
                    return settings.AuthenticationManager.GetGraphEndPoint();
                }
            }
            return "graph.microsoft.com";
        }


        internal void InitializeTelemetry(ClientContext context, InitializationType initializationType)
        {

            var enableTelemetry = false;
            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var telemetryFile = System.IO.Path.Combine(userProfile, ".pnppowershelltelemetry");
            if (Environment.GetEnvironmentVariable("PNPPOWERSHELL_DISABLETELEMETRY") != null)
            {
                enableTelemetry = Environment.GetEnvironmentVariable("PNPPOWERSHELL_DISABLETELEMETRY").ToLower().Equals("false");
            }

            if (!System.IO.File.Exists(telemetryFile))
            {
                enableTelemetry = true;
            }
            else
            {
                if (System.IO.File.ReadAllText(telemetryFile).ToLower() == "allow")
                {
                    enableTelemetry = true;
                }
            }

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

                ApplicationInsights.Initialize(serverLibraryVersion, serverVersion, initializationType.ToString(), ((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version.ToString(), operatingSystem);
                ApplicationInsights.TrackEvent("Connect-PnPOnline");
            }
        }
        #endregion
    }
}