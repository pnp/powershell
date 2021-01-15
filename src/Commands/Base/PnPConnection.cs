using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;
using PnP.Framework.Extensions;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using TextCopy;
using PnP.Framework;
using PnP.PowerShell.ALC;
using PnP.PowerShell.Commands.Attributes;
using System.Threading;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PnP.PowerShell.Commands.Base
{
    public class PnPConnection
    {
        #region Constants

        /// <summary>
        /// ClientId of the application registered in Azure Active Directory which should be used for the device oAuth flow
        /// </summary>
        internal const string PnPManagementShellClientId = "31359c7f-bd7e-475c-86db-fdb8c937548e";
        #endregion

        #region Properties

        internal Dictionary<string, string> TelemetryProperties { get; set; }

        private HttpClient httpClient;

        private PnPContext pnpContext { get; set; }

        public HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                {
                    httpClient = new HttpClient();
                }
                return httpClient;
            }
        }

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

        #region Fields

        /// <summary>
        /// Collection with all available access tokens in the current session to access APIs. Key is the token audience, value is the JWT token itself.
        /// </summary>
        // private readonly Dictionary<TokenAudience, GenericToken> AccessTokens = new Dictionary<TokenAudience, GenericToken>();

        #endregion

        #region Methods

        // internal ReadOnlyDictionary<TokenAudience, GenericToken> GetAllStoredTokens()
        // {
        //     return new ReadOnlyDictionary<TokenAudience, GenericToken>(AccessTokens);
        // }

        /// <summary>
        /// Tries to get an access token for the provided audience
        /// </summary>
        /// <param name="tokenAudience">Audience to try to get an access token for</param>
        /// <param name="permissionScopes">The specific permission scopes to request access to (i.e. Group.ReadWrite.All). Optional, will use default permission scopes assigned to clientId if not specified.</param>
        /// <returns>AccessToken for the audience or NULL if unable to retrieve an access token for the audience on the current connection</returns>
        // internal async Task<string> TryGetAccessTokenAsync(TokenAudience tokenAudience, string[] permissionScopes = null)
        // {
        //     var token = await TryGetTokenAsync(tokenAudience, AzureEnvironment, permissionScopes);
        //     return token?.AccessToken;
        // }

        // internal static Action<DeviceCodeResult> DeviceLoginCallback(CmdletMessageWriter messageWriter, bool launchBrowser)
        // {
        //     return deviceCodeResult =>
        //     {

        //         if (launchBrowser)
        //         {
        //             if (Utilities.OperatingSystem.IsWindows())
        //             {
        //                 ClipboardService.SetText(deviceCodeResult.UserCode);
        //                 messageWriter.WriteMessage($"\n\nCode {deviceCodeResult.UserCode} has been copied to your clipboard\n\n");
        //                 BrowserHelper.GetWebBrowserPopup(deviceCodeResult.VerificationUrl, "Please log in");
        //             }
        //             else
        //             {
        //                 messageWriter.WriteMessage($"\n\n{deviceCodeResult.Message}\n\n");
        //             }
        //         }
        //         else
        //         {
        //             messageWriter.WriteMessage($"\n\n{deviceCodeResult.Message}\n\n");
        //         }
        //     };
        // }
        

        /// <summary>
        /// Adds the provided token to the available tokens in the current connection
        /// </summary>
        /// <param name="tokenAudience">Audience the token is for</param>
        /// <param name="token">The token to add</param>
        // internal void AddToken(TokenAudience tokenAudience, GenericToken token)
        // {
        //     AccessTokens[tokenAudience] = token;
        // }

        /// <summary>
        /// Clears all available tokens on the current connection
        /// </summary>
        // internal void ClearTokens()
        // {
        //     AccessTokens.Clear();
        // }

        // private (bool valid, string message) ValidateTokenForPermissions(GenericToken token, TokenAudience tokenAudience, string[] orPermissionScopes = null, string[] andPermissionScopes = null, TokenType tokenType = TokenType.All)
        // {
        //     bool valid = false;
        //     var message = string.Empty;
        //     if (tokenType != TokenType.All && token.TokenType != tokenType)
        //     {
        //         throw new PSSecurityException($"Access to {tokenAudience} failed because the API requires {(tokenType == TokenType.Application ? "an" : "a")} {tokenType} token while you currently use {(token.TokenType == TokenType.Application ? "an" : "a")} {token.TokenType} token.");
        //     }
        //     var andScopesMatched = false;
        //     if (andPermissionScopes != null && andPermissionScopes.Length != 0)
        //     {
        //         // we have explicitely required permission scopes
        //         andScopesMatched = andPermissionScopes.All(r => token.Roles.Contains(r));
        //     }
        //     else
        //     {
        //         andScopesMatched = true;
        //     }

        //     var orScopesMatched = false;
        //     if (orPermissionScopes != null && orPermissionScopes.Length != 0)
        //     {
        //         orScopesMatched = orPermissionScopes.Any(r => token.Roles.Contains(r));
        //     }
        //     else
        //     {
        //         orScopesMatched = true;
        //     }

        //     if (orScopesMatched && andScopesMatched)
        //     {
        //         valid = true;
        //     }

        //     if (orPermissionScopes != null || andPermissionScopes != null)
        //     {
        //         if (!valid)
        //         {                // Requested role was not part of the access token, throw an exception explaining which application registration is missing which role
        //             if (!orScopesMatched)
        //             {
        //                 message += "for one of the following permission scopes: " + string.Join(", ", orPermissionScopes);
        //             }
        //             if (!andScopesMatched)
        //             {
        //                 message += (message != string.Empty ? ", and " : ", ") + "for all of the following permission scopes: " + string.Join(", ", andPermissionScopes);
        //             }
        //             throw new PSSecurityException($"Access to {tokenAudience} failed because the app registration {ClientId} in tenant {Tenant} is not granted {message}");
        //         }
        //     }
        //     return (valid, message);
        // }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a basic new PnP Connection. Use one of the static methods to retrieve a PnPConnection for a specific purpose.
        /// </summary>
        /// <param name="host">PowerShell Host environment in which the commands are being run</param>
        /// <param name="initializationType">Indicator of type <see cref="InitializationType"/> which indicates the method used to set up the connection. Used for gathering usage analytics.</param>
        /// <param name="url">Url of the SharePoint environment to connect to, if applicable. Leave NULL not to connect to a SharePoint environment.</param>
        /// <param name="clientContext">A SharePoint ClientContext to make available within this connection. Leave NULL to not connect to a SharePoint environment.</param>
        /// <param name="pnpVersionTag">Identifier set on the SharePoint ClientContext as the ClientTag to identify the source of the requests to SharePoint. Leave NULL not to set it.</param>
        // private PnPConnection(InitializationType initializationType,
        //                       string url = null,
        //                       ClientContext clientContext = null,
        //                       Dictionary<TokenAudience, GenericToken> tokens = null,
        //                       string pnpVersionTag = null)
        // {
        //     InitializeTelemetry(clientContext, initializationType);

        //     UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version}";
        //     Context = clientContext;

        //     // Enrich the AccessTokens collection with the token(s) passed in
        //     if (tokens != null)
        //     {
        //         AccessTokens.AddRange(tokens);
        //     }

        //     // Validate if we have a SharePoint Context
        //     if (Context != null)
        //     {
        //         // We have a SharePoint Context, configure the context
        //         ContextCache = new List<ClientContext> { Context };

        //         // If we have a SharePoint or a Graph Access Token, use it for the SharePoint connection
        //         var accessToken = AccessTokens.ContainsKey(TokenAudience.SharePointOnline) ? TryGetAccessTokenAsync(TokenAudience.SharePointOnline).GetAwaiter().GetResult() : TryGetAccessTokenAsync(TokenAudience.MicrosoftGraph).GetAwaiter().GetResult();
        //         if (accessToken != null)
        //         {
        //             Context.ExecutingWebRequest += (sender, args) =>
        //             {
        //                 args.WebRequestExecutor.WebRequest.UserAgent = UserAgent;
        //                 args.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + accessToken;
        //             };
        //         }
        //     }
        //     else
        //     {
        //         // We do not have a SharePoint Context
        //         ContextCache = null;
        //     }

        //     PnPVersionTag = pnpVersionTag;
        //     Url = url;
        // }

        #endregion

        #region Connection Creation

        /// <summary>
        /// Returns a PnPConnection based on connecting using a ClientId and ClientSecret
        /// </summary>
        /// <param name="clientId">ClientId to connect with</param>
        /// <param name="clientSecret">ClientSecret to connect with</param>
        /// <param name="aadDomain">The Azure Active Directory tenant name (i.e. contoso.onmicrosoft.com) or the tenant identifier to which to connect</param>
        /// <param name="host">PowerShell Host environment in which the commands are being run</param>
        /// <param name="initializationType">Indicator of type <see cref="InitializationType"/> which indicates the method used to set up the connection. Used for gathering usage analytics.</param>
        /// <param name="url">Url of the SharePoint environment to connect to, if applicable. Leave NULL not to connect to a SharePoint environment.</param>
        /// <param name="clientContext">A SharePoint ClientContext to make available within this connection. Leave NULL to not connect to a SharePoint environment.</param>
        /// <param name="pnpVersionTag">Identifier set on the SharePoint ClientContext as the ClientTag to identify the source of the requests to SharePoint. Leave NULL not to set it.</param>
        /// <returns><see cref="PnPConnection"/ instance which can be used to communicate with one of the supported APIs</returns>
        // public static PnPConnection GetConnectionWithClientIdAndClientSecret(string clientId,
        //                                                                      string clientSecret,
        //                                                                      InitializationType initializationType,
        //                                                                      string url = null,
        //                                                                      string aadDomain = null,
        //                                                                      ClientContext clientContext = null,
        //                                                                      string pnpVersionTag = null)
        // {
        //     return new PnPConnection(initializationType, url, clientContext, null, pnpVersionTag)
        //     {
        //         ClientId = clientId,
        //         ClientSecret = clientSecret,
        //         ConnectionMethod = ConnectionMethod.AzureADAppOnly,
        //         Tenant = aadDomain
        //     };
        // }

        /// <summary>
        /// Returns a PnPConnection based on connecting using a ClientId and Certificate
        /// </summary>
        /// <param name="clientId">ClientId to connect with</param>
        /// <param name="certificate">Certificate to connect with</param>
        /// <param name="aadDomain">The Azure Active Directory tenant name (i.e. contoso.onmicrosoft.com) or the tenant identifier to which to connect</param>
        /// <param name="host">PowerShell Host environment in which the commands are being run</param>
        /// <param name="initializationType">Indicator of type <see cref="InitializationType"/> which indicates the method used to set up the connection. Used for gathering usage analytics.</param>
        /// <param name="url">Url of the SharePoint environment to connect to, if applicable. Leave NULL not to connect to a SharePoint environment.</param>
        /// <param name="clientContext">A SharePoint ClientContext to make available within this connection. Leave NULL to not connect to a SharePoint environment.</param>
        /// <param name="pnpVersionTag">Identifier set on the SharePoint ClientContext as the ClientTag to identify the source of the requests to SharePoint. Leave NULL not to set it.</param>
        /// <returns><see cref="PnPConnection"/ instance which can be used to communicate with one of the supported APIs</returns>
        // public static PnPConnection GetConnectionWithClientIdAndCertificate(string clientId,
        //                                                                     X509Certificate2 certificate,
        //                                                                     InitializationType initializationType,
        //                                                                     string url = null,
        //                                                                     string aadDomain = null,
        //                                                                     ClientContext clientContext = null,
        //                                                                     string pnpVersionTag = null)
        // {
        //     return new PnPConnection(initializationType, url, clientContext, null, pnpVersionTag)
        //     {
        //         ClientId = clientId,
        //         Certificate = certificate,
        //         ConnectionMethod = ConnectionMethod.AzureADAppOnly,
        //         Tenant = aadDomain
        //     };
        // }

        /// <summary>
        /// Returns a PnPConnection based on connecting using an username and password
        /// </summary>
        /// <param name="credential">Credential set to connect with</param>
        /// <param name="host">PowerShell Host environment in which the commands are being run</param>
        /// <param name="initializationType">Indicator of type <see cref="InitializationType"/> which indicates the method used to set up the connection. Used for gathering usage analytics.</param>
        /// <param name="url">Url of the SharePoint environment to connect to, if applicable. Leave NULL not to connect to a SharePoint environment.</param>
        /// <param name="clientContext">A SharePoint ClientContext to make available within this connection. Leave NULL to not connect to a SharePoint environment.</param>
        /// <param name="pnpVersionTag">Identifier set on the SharePoint ClientContext as the ClientTag to identify the source of the requests to SharePoint. Leave NULL not to set it.</param>
        /// <returns><see cref="PnPConnection"/ instance which can be used to communicate with one of the supported APIs</returns>
        //public static PnPConnection GetConnectionWithPsCredential(PSCredential credential,
        //                                                          InitializationType initializationType,
        //                                                          string url = null,
        //                                                          ClientContext clientContext = null,
        //                                                          string pnpVersionTag = null)
        //{
        //    return new PnPConnection(initializationType, url, clientContext, null, pnpVersionTag)
        //    {
        //        PSCredential = credential,
        //        ConnectionMethod = ConnectionMethod.Credentials
        //    };
        //}

        /// <summary>
        /// Returns a PnPConnection based on connecting using an existing token
        /// </summary>
        /// <param name="token">Token to connect with</param>
        /// <param name="tokenAudience">Indicator of <see cref="TokenAudience"/> indicating for which API this token is meant to be used</param>
        /// <param name="host">PowerShell Host environment in which the commands are being run</param>
        /// <param name="initializationType">Indicator of type <see cref="InitializationType"/> which indicates the method used to set up the connection. Used for gathering usage analytics.</param>
        /// <param name="url">Url of the SharePoint environment to connect to, if applicable. Leave NULL not to connect to a SharePoint environment.</param>
        /// <param name="clientContext">A SharePoint ClientContext to make available within this connection. Leave NULL to not connect to a SharePoint environment.</param>
        /// <param name="pnpVersionTag">Identifier set on the SharePoint ClientContext as the ClientTag to identify the source of the requests to SharePoint. Leave NULL not to set it.</param>
        /// <returns><see cref="PnPConnection"/ instance which can be used to communicate with one of the supported APIs</returns>
        // public static PnPConnection GetConnectionWithToken(GenericToken token,
        //                                                    TokenAudience tokenAudience,
        //                                                    InitializationType initializationType,
        //                                                    PSCredential credentials,
        //                                                    string url = null,
        //                                                    ClientContext clientContext = null,
        //                                                    string pnpVersionTag = null,
        //                                                    AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        // {
        //     var connection = new PnPConnection(initializationType, url, clientContext, new Dictionary<TokenAudience, GenericToken>(1) { { tokenAudience, token } }, pnpVersionTag)
        //     {
        //         ConnectionMethod = ConnectionMethod.AccessToken,
        //         Tenant = token.ParsedToken.Claims.FirstOrDefault(c => c.Type.Equals("tid", StringComparison.InvariantCultureIgnoreCase))?.Value,
        //         ClientId = token.ParsedToken.Claims.FirstOrDefault(c => c.Type.Equals("appid", StringComparison.InvariantCultureIgnoreCase))?.Value,
        //         AzureEnvironment = azureEnvironment
        //     };
        //     connection.PSCredential = credentials;
        //     if (clientContext != null)
        //     {
        //         clientContext.ExecutingWebRequest += (sender, args) =>
        //         {
        //             args.WebRequestExecutor.WebRequest.UserAgent = connection.UserAgent;
        //             args.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + token.AccessToken;
        //         };
        //         connection.Context = clientContext;
        //     }
        //     return connection;
        // }

        #endregion

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
            Context = context;
            Context.ExecutingWebRequest += Context_ExecutingWebRequest;

            ConnectionType = connectionType;
            TenantAdminUrl = tenantAdminUrl;

            PSCredential = credential;
            PnPVersionTag = pnpVersionTag;
            ContextCache = new List<ClientContext> { context };
            Url = (new Uri(url)).AbsoluteUri;
            ConnectionMethod = ConnectionMethod.Credentials;
            ClientId = PnPManagementShellClientId;
        }

        // internal PnPConnection(ClientContext context, GenericToken tokenResult, ConnectionType connectionType, PSCredential credential, string url, string tenantAdminUrl, string pnpVersionTag, InitializationType initializationType)
        // {
        //     InitializeTelemetry(context, initializationType);
        //     var coreAssembly = Assembly.GetExecutingAssembly();
        //     UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version}";
        //     Context = context ?? throw new ArgumentNullException(nameof(context));
        //     ConnectionType = connectionType;
        //     PSCredential = credential;
        //     TenantAdminUrl = tenantAdminUrl;
        //     ContextCache = new List<ClientContext> { context };
        //     PnPVersionTag = pnpVersionTag;
        //     Url = (new Uri(url)).AbsoluteUri;
        //     ConnectionMethod = ConnectionMethod.AccessToken;
        //     ClientId = PnPManagementShellClientId;
        //     Tenant = tokenResult.ParsedToken.Claims.FirstOrDefault(c => c.Type == "tid").Value;
        //     context.ExecutingWebRequest += (sender, args) =>
        //     {
        //         args.WebRequestExecutor.WebRequest.UserAgent = UserAgent;
        //         args.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + tokenResult.AccessToken;
        //     };
        // }

        // internal PnPConnection(ConnectionMethod connectionMethod, ConnectionType connectionType, string pnpVersionTag, InitializationType initializationType)
        // {
        //     Context = new ClientContext("https://localhost"); // dummy context
        //     InitializeTelemetry(null, initializationType);
        //     var coreAssembly = Assembly.GetExecutingAssembly();
        //     UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version}";
        //     ConnectionType = connectionType;
        //     PnPVersionTag = pnpVersionTag;
        //     ConnectionMethod = connectionMethod;
        // }

        //internal PnPConnection(ConnectionMethod connectionMethod, ConnectionType connectionType, string pnpVersionTag, InitializationType initializationType)
        //{
        //        InitializeTelemetry(null, initializationType);
        //    var coreAssembly = Assembly.GetExecutingAssembly();
        //    UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version}";
        //    ConnectionType = connectionType;
        //    PnPVersionTag = pnpVersionTag;
        //    ConnectionMethod = connectionMethod;
        //}

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

        //internal static ClientContext GetCachedContext(string url)
        //{
        //    return ContextCache.FirstOrDefault(c => System.Net.WebUtility.UrlEncode(c.Url) == System.Net.WebUtility.UrlEncode(url));
        //}

        //internal static void ClearContextCache()
        //{
        //    ContextCache.Clear();
        //}

        internal PnPConnection(string pnpVersionTag, InitializationType initializationType)
        {
            InitializeTelemetry(null, initializationType);
            var coreAssembly = Assembly.GetExecutingAssembly();
            UserAgent = $"NONISV|SharePointPnP|PnPPS/{((AssemblyFileVersionAttribute)coreAssembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version}";
            //if (context == null)
            //    throw new ArgumentNullException(nameof(context));
            ConnectionType = ConnectionType.O365;
            PnPVersionTag = pnpVersionTag;

            ConnectionMethod = ConnectionMethod.ManagedIdentity;
            ManagedIdentity = true;
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
    }
}