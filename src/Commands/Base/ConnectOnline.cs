﻿using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Provider;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using File = System.IO.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommunications.Connect, "PnPOnline", DefaultParameterSetName = ParameterSet_CREDENTIALS)]
    public class ConnectOnline : BasePSCmdlet
    {
        private CancellationTokenSource cancellationTokenSource;
        private const string ParameterSet_CREDENTIALS = "Credentials";
        private const string ParameterSet_ACSAPPONLY = "SharePoint ACS (Legacy) App Only";
        private const string ParameterSet_APPONLYAADCERTIFICATE = "App-Only with Azure Active Directory";
        private const string ParameterSet_APPONLYAADTHUMBPRINT = "App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint";
        private const string ParameterSet_SPOMANAGEMENT = "SPO Management Shell Credentials";
        private const string ParameterSet_DEVICELOGIN = "PnP Management Shell / DeviceLogin";
        private const string ParameterSet_ACCESSTOKEN = "Access Token";
        private const string ParameterSet_WEBLOGIN = "Web Login for Multi Factor Authentication";
        private const string ParameterSet_MANAGEDIDENTITY = "Managed Identity";
        private const string ParameterSet_INTERACTIVE = "Interactive login for Multi Factor Authentication";

        private const string SPOManagementClientId = "9bc3ab49-b65d-410a-85ad-de819febfddc";
        private const string SPOManagementRedirectUri = "https://oauth.spops.microsoft.com/";


        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPOMANAGEMENT, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WEBLOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACCESSTOKEN, ValueFromPipeline = true)]
        public SwitchParameter ReturnConnection;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_CREDENTIALS, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_ACSAPPONLY, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_SPOMANAGEMENT, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_ACCESSTOKEN, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_DEVICELOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_WEBLOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_INTERACTIVE, ValueFromPipeline = true)]
        public string Url;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        public CredentialPipeBind Credentials;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        public SwitchParameter CurrentCredentials;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        // [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYCLIENTIDCLIENTSECRETAADDOMAIN)]
        public string Realm;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ACSAPPONLY)]

        public string ClientSecret;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPOMANAGEMENT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WEBLOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        public SwitchParameter CreateDrive;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPOMANAGEMENT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WEBLOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        public string DriveName = "SPO";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SPOMANAGEMENT)]
        public SwitchParameter SPOManagementShell;


        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Alias("PnPManagementShell", "PnPO365ManagementShell")]
        public SwitchParameter DeviceLogin;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        public SwitchParameter LaunchBrowser;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        public string ClientId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        public string RedirectUri;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        public string Tenant;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        public string CertificatePath;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        public string CertificateBase64Encoded;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        public SecureString CertificatePassword;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        public string Thumbprint;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACCESSTOKEN)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        // [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYCLIENTIDCLIENTSECRETAADDOMAIN)]
        public string AADDomain;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        // [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYCLIENTIDCLIENTSECRETURL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPOMANAGEMENT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WEBLOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        public string TenantAdminUrl;

        [Parameter(Mandatory = false)]
        [Obsolete("Set the environment variable 'PNPPOWERSHELL_DISABLETELEMETRY' to 'true' instead of using this switch.")]
        public SwitchParameter NoTelemetry;

        [Parameter(Mandatory = false)]
        [Obsolete("Set the environment variable 'PNPPOWERSHELL_UPDATECHECK' to 'false' instead of using this switch.")]
        public SwitchParameter NoVersionCheck;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_MANAGEDIDENTITY)]
        public SwitchParameter ManagedIdentity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        public SwitchParameter TransformationOnPrem;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_WEBLOGIN)]
        public SwitchParameter UseWebLogin;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WEBLOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        public SwitchParameter ForceAuthentication;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_INTERACTIVE)]
        public SwitchParameter Interactive;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ACCESSTOKEN)]
        public string AccessToken;

        protected override void ProcessRecord()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

#pragma warning disable CS0618 // NoTelemetry and NoVersionCheck needs to be set through environment variables now
            if (NoTelemetry)
            {
                Environment.SetEnvironmentVariable("PNPPOWERSHELL_DISABLETELEMETRY", "true");
            }
            if (NoVersionCheck)
            {
                Environment.SetEnvironmentVariable("PNPPOWERSHELL_UPDATECHECK", "false");
            }
#pragma warning restore CS6018



            VersionChecker.CheckVersion(this);
            try
            {
                Connect(ref token);
            }
            catch (Exception ex)
            {
                ex.Data["TimeStampUtc"] = DateTime.UtcNow;
                throw;
            }
        }

        /// <summary>
        /// Sets up the connection using the information provided through the cmdlet arguments
        /// </summary>
        protected void Connect(ref CancellationToken cancellationToken)
        {

            if (!string.IsNullOrEmpty(Url) && Url.EndsWith("/"))
            {
                Url = Url.TrimEnd('/');
            }

            PnPConnection connection = null;

            PSCredential credentials = null;
            if (Credentials != null)
            {
                credentials = Credentials.Credential;
            }

            // Connect using the used set parameters
            switch (ParameterSetName)
            {
                case ParameterSet_SPOMANAGEMENT:
                    connection = ConnectSpoManagement();
                    break;
                case ParameterSet_DEVICELOGIN:
                    connection = ConnectDeviceLogin();
                    break;
                case ParameterSet_APPONLYAADCERTIFICATE:
                    connection = ConnectAppOnlyWithCertificate();
                    break;
                case ParameterSet_APPONLYAADTHUMBPRINT:
                    connection = ConnectAppOnlyWithCertificate();
                    break;
                case ParameterSet_ACCESSTOKEN:
                    connection = ConnectAccessToken();
                    break;
                case ParameterSet_ACSAPPONLY:
                    connection = ConnectACSAppOnly();
                    break;
                case ParameterSet_CREDENTIALS:
                    connection = ConnectCredentials(credentials);
                    break;
                case ParameterSet_MANAGEDIDENTITY:
                    connection = ConnectManagedIdentity();
                    break;
                case ParameterSet_WEBLOGIN:
                    connection = ConnectWebLogin();
                    break;
                case ParameterSet_INTERACTIVE:
                    connection = ConnectInteractive();
                    break;
            }

            // Ensure a connection instance has been created by now
            if (connection == null)
            {
                // No connection instance was created
                throw new PSInvalidOperationException("Unable to connect using provided arguments");
            }

            // Connection has been established
#if !NETFRAMEWORK
            WriteVerbose($"PnP PowerShell Cmdlets ({new SemanticVersion(Assembly.GetExecutingAssembly().GetName().Version)})");
#else
            WriteVerbose($"PnP PowerShell Cmdlets ({Assembly.GetExecutingAssembly().GetName().Version})");
#endif
            PnPConnection.Current = connection;
            if (CreateDrive && PnPConnection.Current.Context != null)
            {
                var provider = SessionState.Provider.GetAll().FirstOrDefault(p => p.Name.Equals(SPOProvider.PSProviderName, StringComparison.InvariantCultureIgnoreCase));
                if (provider != null)
                {
                    if (provider.Drives.Any(d => d.Name.Equals(DriveName, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        SessionState.Drive.Remove(DriveName, true, "Global");
                    }

                    var drive = new PSDriveInfo(DriveName, provider, string.Empty, Url, null);
                    SessionState.Drive.New(drive, "Global");
                }
            }

            if (PnPConnection.Current.Url != null)
            {
                var hostUri = new Uri(PnPConnection.Current.Url);
                Environment.SetEnvironmentVariable("PNPPSHOST", hostUri.Host);
                Environment.SetEnvironmentVariable("PNPPSSITE", hostUri.LocalPath);
            }
            else
            {
                Environment.SetEnvironmentVariable("PNPPSHOST", "GRAPH");
                Environment.SetEnvironmentVariable("PNPPSSITE", "GRAPH");
            }

            if (ReturnConnection)
            {
                WriteObject(connection);
            }

        }

        #region Connect Types

        /// <summary>
        /// Connect using the paramater set TOKEN
        /// </summary>
        /// <returns>PnPConnection based on the parameters provided in the parameter set</returns>
        private PnPConnection ConnectACSAppOnly()
        {
            CmdletMessageWriter.WriteFormattedMessage(this, new CmdletMessageWriter.Message { Text = "Connecting with Client Secret uses legacy authentication and provides limited functionality. We can for instance not execute requests towards the Microsoft Graph, which limits cmdlets related to Microsoft Teams, Microsoft Planner, Microsoft Flow and Microsoft 365 Groups. You can hide this warning by using Connect-PnPOnline [your parameters] -WarningAction Ignore", Formatted = true, Type = CmdletMessageWriter.MessageType.Warning });
            if (PnPConnection.Current?.ClientId == ClientId &&
                PnPConnection.Current?.ClientSecret == ClientSecret &&
                PnPConnection.Current?.Tenant == AADDomain)
            {
                ReuseAuthenticationManager();
            }
            return PnPConnection.CreateWithACSAppOnly(new Uri(Url), AADDomain, ClientId, ClientSecret, TenantAdminUrl, AzureEnvironment);
        }

        /// <summary>
        /// Connect using the parameter set SPOMANAGEMENT
        /// </summary>
        /// <returns>PnPConnection based on the parameters provided in the parameter set</returns>
        private PnPConnection ConnectSpoManagement()
        {
            ClientId = SPOManagementClientId;
            RedirectUri = SPOManagementRedirectUri;

            return ConnectCredentials(Credentials?.Credential, InitializationType.SPOManagementShell);
        }

        /// <summary>
        /// Connect using the parameter set DEVICELOGIN
        /// </summary>
        /// <returns>PnPConnection based on the parameters provided in the parameter set</returns>
        private PnPConnection ConnectDeviceLogin()
        {
            var messageWriter = new CmdletMessageWriter(this);
            PnPConnection connection = null;
            var uri = new Uri(Url);
            if ($"https://{uri.Host}".Equals(Url.ToLower()))
            {
                Url += "/";
            }
            var task = Task.Factory.StartNew(() =>
            {
                try
                {
                    Uri oldUri = null;

                    if (PnPConnection.Current != null)
                    {
                        if (PnPConnection.Current.Url != null)
                        {
                            oldUri = new Uri(PnPConnection.Current.Url);
                        }
                    }
                    if (oldUri != null && oldUri.Host == new Uri(Url).Host && PnPConnection.Current?.ConnectionMethod == ConnectionMethod.DeviceLogin)
                    {
                        ReuseAuthenticationManager();
                    }

                    var clientId = PnPConnection.PnPManagementShellClientId;
                    if (ParameterSpecified(nameof(ClientId)))
                    {
                        clientId = ClientId;
                    }


                    var returnedConnection = PnPConnection.CreateWithDeviceLogin(clientId, Url, Tenant, LaunchBrowser, messageWriter, AzureEnvironment, cancellationTokenSource);
                    connection = returnedConnection;
                    messageWriter.Finished = true;
                }
                catch (Exception ex)
                {
                    messageWriter.WriteWarning(ex.Message,false);
                    messageWriter.Finished = true;
                }
            }, cancellationTokenSource.Token);
            messageWriter.Start();
            return connection;
        }

        /// <summary>
        /// Connect using the parameter set APPONLYAAD
        /// </summary>
        /// <returns>PnPConnection based on the parameters provided in the parameter set</returns>
        private PnPConnection ConnectAppOnlyWithCertificate()
        {
            if (ParameterSpecified(nameof(CertificatePath)))
            {
                if (!Path.IsPathRooted(CertificatePath))
                {
                    CertificatePath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path,
                               CertificatePath);
                }
                if (!File.Exists(CertificatePath))
                {
                    throw new FileNotFoundException("Certificate not found");
                }
                X509Certificate2 certificate = CertificateHelper.GetCertificateFromPath(CertificatePath, CertificatePassword);
                if (PnPConnection.Current?.ClientId == ClientId &&
                    PnPConnection.Current?.Tenant == Tenant &&
                    PnPConnection.Current?.Certificate?.Thumbprint == certificate.Thumbprint)
                {
                    ReuseAuthenticationManager();
                }
                return PnPConnection.CreateWithCert(new Uri(Url), ClientId, Tenant, TenantAdminUrl, AzureEnvironment, certificate, true);
            }
            else if (ParameterSpecified(nameof(CertificateBase64Encoded)))
            {
                var certificateBytes = Convert.FromBase64String(CertificateBase64Encoded);
                var certificate = new X509Certificate2(certificateBytes, CertificatePassword);

                if (PnPConnection.Current?.ClientId == ClientId &&
                    PnPConnection.Current?.Tenant == Tenant &&
                    PnPConnection.Current?.Certificate?.Thumbprint == certificate.Thumbprint)
                {
                    ReuseAuthenticationManager();
                }
                return PnPConnection.CreateWithCert(new Uri(Url), ClientId, Tenant, TenantAdminUrl, AzureEnvironment, certificate);
            }
            else if (ParameterSpecified(nameof(Thumbprint)))
            {
                X509Certificate2 certificate = CertificateHelper.GetCertificateFromStore(Thumbprint);

                if (certificate == null)
                {
                    throw new PSArgumentException("Cannot find certificate with this thumbprint in the certificate store.", nameof(Thumbprint));
                }

                // Ensure the private key of the certificate is available
                if (!certificate.HasPrivateKey)
                {
                    throw new PSArgumentException("The certificate specified does not have a private key.", nameof(Thumbprint));
                }
                if (PnPConnection.Current?.ClientId == ClientId &&
                                    PnPConnection.Current?.Tenant == Tenant &&
                                    PnPConnection.Current?.Certificate?.Thumbprint == certificate.Thumbprint)
                {
                    ReuseAuthenticationManager();
                }
                return PnPConnection.CreateWithCert(new Uri(Url), ClientId, Tenant, TenantAdminUrl, AzureEnvironment, certificate);
            }
            else
            {
                throw new ArgumentException("You must either provide CertificatePath, Certificate or CertificateBase64Encoded when connecting using an Azure Active Directory registered application");
            }
        }

        /// <summary>
        /// Connect using the parameter set ACCESSTOKEN
        /// </summary>
        /// <returns>PnPConnection based on the parameters provided in the parameter set</returns>
        private PnPConnection ConnectAccessToken()
        {
            return PnPConnection.CreateWithAccessToken(!string.IsNullOrEmpty(Url) ? new Uri(Url) : null, AccessToken, TenantAdminUrl);
        }

        /// <summary>
        /// Connect using provided credentials or the current credentials
        /// </summary>
        /// <returns>PnPConnection based on credentials authentication</returns>
        private PnPConnection ConnectCredentials(PSCredential credentials, InitializationType initializationType = InitializationType.Credentials)
        {
            if (!CurrentCredentials && credentials == null)
            {
                credentials = GetCredentials();
                if (credentials == null)
                {
                    credentials = Host.UI.PromptForCredential(Resources.EnterYourCredentials, "", "", "");

                    // Ensure credentials have been entered
                    if (credentials == null)
                    {
                        // No credentials have been provided
                        return null;
                    }
                }
            }
            if (ClientId == null)
            {
                ClientId = PnPConnection.PnPManagementShellClientId;
            }

            if (PnPConnection.Current?.ClientId == ClientId)
            {
                if (credentials != null && PnPConnection.Current?.PSCredential?.UserName == credentials.UserName &&
                   PnPConnection.Current?.PSCredential.GetNetworkCredential().Password == credentials.GetNetworkCredential().Password)
                {
                    ReuseAuthenticationManager();
                }
            }

            return PnPConnection.CreateWithCredentials(this, new Uri(Url),
                                                               credentials,
                                                               CurrentCredentials,
                                                               TenantAdminUrl,
                                                               AzureEnvironment,
                                                               ClientId,
                                                               RedirectUri, TransformationOnPrem, initializationType);
        }

        private PnPConnection ConnectManagedIdentity()
        {
            WriteVerbose("Connecting to the Graph with the current Managed Identity");
            return PnPConnection.CreateWithManagedIdentity(this, TenantAdminUrl);
        }

        private PnPConnection ConnectWebLogin()
        {
            WriteWarning("Consider using -Interactive instead, which provides better functionality. See the documentation at https://pnp.github.io/powershell/cmdlets/Connect-PnPOnline.html#interactive-login-for-multi-factor-authentication");
            if (Utilities.OperatingSystem.IsWindows())
            {
                return PnPConnection.CreateWithWeblogin(new Uri(Url.ToLower()), TenantAdminUrl, ForceAuthentication);
            }
            else
            {
                throw new PSArgumentException("-UseWebLogin only works when running on Microsoft Windows due to the requirement to show a login window.");
            }
        }

        private PnPConnection ConnectInteractive()
        {
            if (ClientId == null)
            {
                ClientId = PnPConnection.PnPManagementShellClientId;
            }
            if (PnPConnection.Current?.ClientId == ClientId)
            {
                if (IsSameOrAdminHost(new Uri(Url), new Uri(PnPConnection.Current.Url)))
                {
                    ReuseAuthenticationManager();
                }
            }
            return PnPConnection.CreateWithInteractiveLogin(new Uri(Url.ToLower()), ClientId, TenantAdminUrl, LaunchBrowser, AzureEnvironment, cancellationTokenSource, ForceAuthentication, Tenant);
        }

        #endregion

        #region Helper methods
        private PSCredential GetCredentials()
        {
            var connectionUri = new Uri(Url);

            // Try to get the credentials by full url
            PSCredential credentials = Utilities.CredentialManager.GetCredential(Url);
            if (credentials == null)
            {
                // Try to get the credentials by splitting up the path
                var pathString = $"{connectionUri.Scheme}://{(connectionUri.IsDefaultPort ? connectionUri.Host : $"{connectionUri.Host}:{connectionUri.Port}")}";
                var path = connectionUri.AbsolutePath;
                while (path.IndexOf('/') != -1)
                {
                    path = path.Substring(0, path.LastIndexOf('/'));
                    if (!string.IsNullOrEmpty(path))
                    {
                        var pathUrl = $"{pathString}{path}";
                        credentials = Utilities.CredentialManager.GetCredential(pathUrl);
                        if (credentials != null)
                        {
                            break;
                        }
                    }
                }

                if (credentials == null)
                {
                    // Try to find the credentials by schema and hostname
                    credentials = Utilities.CredentialManager.GetCredential(connectionUri.Scheme + "://" + connectionUri.Host);

                    if (credentials == null)
                    {
                        // Maybe added with an extra slash?
                        credentials = Utilities.CredentialManager.GetCredential(connectionUri.Scheme + "://" + connectionUri.Host + "/");

                        if (credentials == null)
                        {
                            // try to find the credentials by hostname
                            credentials = Utilities.CredentialManager.GetCredential(connectionUri.Host);
                        }
                    }
                }

            }

            return credentials;
        }

        private bool IsSameOrAdminHost(Uri currentUri, Uri previousUri)
        {
            var tenantAdminUrl = string.Empty;
            if (!previousUri.Host.Contains("-admin"))
            {
                tenantAdminUrl = string.IsNullOrEmpty(TenantAdminUrl) ? previousUri.Host.Replace(".sharepoint.", "-admin.sharepoint.") : TenantAdminUrl;
            }
            if (currentUri.Host == tenantAdminUrl)
            {
                return true;
            }

            if (!currentUri.Host.Contains("-admin"))
            {
                tenantAdminUrl = string.IsNullOrEmpty(TenantAdminUrl) ? currentUri.Host.Replace(".sharepoint.", "-admin.sharepoint.") : TenantAdminUrl;
            }
            if (previousUri.Host == tenantAdminUrl)
            {
                return true;
            }
            return currentUri.Host == previousUri.Host;
        }


        protected override void StopProcessing()
        {
            cancellationTokenSource.Cancel();
        }

        private void ReuseAuthenticationManager()
        {
            var contextSettings = PnPConnection.Current.Context.GetContextSettings();
            PnPConnection.CachedAuthenticationManager = contextSettings.AuthenticationManager;
        }
        #endregion
    }
}
