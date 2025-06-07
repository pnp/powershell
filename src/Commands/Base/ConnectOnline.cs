using Microsoft.SharePoint.Client;
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
    [Cmdlet(VerbsCommunications.Connect, "PnPOnline", DefaultParameterSetName = ParameterSet_INTERACTIVE)]
    public class ConnectOnline : BasePSCmdlet
    {
        private CancellationTokenSource cancellationTokenSource;
        private const string ParameterSet_CREDENTIALS = "Credentials";
        private const string ParameterSet_ACSAPPONLY = "SharePoint ACS (Legacy) App Only";
        private const string ParameterSet_APPONLYAADCERTIFICATE = "App-Only with Azure Active Directory";
        private const string ParameterSet_APPONLYAADTHUMBPRINT = "App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint";
        private const string ParameterSet_DEVICELOGIN = "PnP Management Shell / DeviceLogin";
        private const string ParameterSet_ACCESSTOKEN = "Access Token";
        private const string ParameterSet_SYSTEMASSIGNEDMANAGEDIDENTITY = "System Assigned Managed Identity";
        private const string ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID = "User Assigned Managed Identity by Client Id";
        private const string ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID = "User Assigned Managed Identity by Principal Id";
        private const string ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID = "User Assigned Managed Identity by Azure Resource Id";
        private const string ParameterSet_INTERACTIVE = "Interactive login for Multi Factor Authentication";
        private const string ParameterSet_ENVIRONMENTVARIABLE = "Environment Variable";
        private const string ParameterSet_AZUREAD_WORKLOAD_IDENTITY = "Azure AD Workload Identity";
        private const string ParameterSet_OSLOGIN = "OS login";
        private const string ParameterSet_FEDERATEDIDENTITY = "Federated Identity";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACCESSTOKEN, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SYSTEMASSIGNEDMANAGEDIDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AZUREAD_WORKLOAD_IDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FEDERATEDIDENTITY)]
        public SwitchParameter ReturnConnection;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACCESSTOKEN, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SYSTEMASSIGNEDMANAGEDIDENTITY, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AZUREAD_WORKLOAD_IDENTITY, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FEDERATEDIDENTITY, ValueFromPipeline = true)]
        public SwitchParameter ValidateConnection;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_CREDENTIALS, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_ACSAPPONLY, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_ACCESSTOKEN, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_DEVICELOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_INTERACTIVE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = ParameterSet_SYSTEMASSIGNEDMANAGEDIDENTITY, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE, ValueFromPipeline = true)]
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = ParameterSet_AZUREAD_WORKLOAD_IDENTITY, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_OSLOGIN, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_FEDERATEDIDENTITY, ValueFromPipeline = true)]
        public string Url;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_AZUREAD_WORKLOAD_IDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        public PnPConnection Connection = PnPConnection.Current;

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
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        public SwitchParameter CreateDrive;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        public string DriveName = "SPO";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEVICELOGIN)]
        public SwitchParameter DeviceLogin;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_FEDERATEDIDENTITY)]
        [Alias("ApplicationId")]
        public string ClientId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        public string RedirectUri;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_FEDERATEDIDENTITY)]
        public string Tenant;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        public string CertificatePath;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        public string CertificateBase64Encoded;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        public X509KeyStorageFlags X509KeyStorageFlags;

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
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SYSTEMASSIGNEDMANAGEDIDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FEDERATEDIDENTITY)]
        public Framework.AzureEnvironment AzureEnvironment = Framework.AzureEnvironment.Production;

        // [Parameter(Mandatory = true, ParameterSetName = ParameterSet_APPONLYCLIENTIDCLIENTSECRETAADDOMAIN)]
        public string AADDomain;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        public string TenantAdminUrl;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SYSTEMASSIGNEDMANAGEDIDENTITY)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID)]
        public SwitchParameter ManagedIdentity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_FEDERATEDIDENTITY)]
        public SwitchParameter FederatedIdentity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID)]
        [Alias("UserAssignedManagedIdentityPrincipalId")]
        public string UserAssignedManagedIdentityObjectId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID)]
        public string UserAssignedManagedIdentityClientId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID)]
        public string UserAssignedManagedIdentityAzureResourceId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        public SwitchParameter TransformationOnPrem;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        public SwitchParameter ForceAuthentication;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        public SwitchParameter Interactive;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ACCESSTOKEN)]
        public string AccessToken;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        public SwitchParameter EnvironmentVariable;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACCESSTOKEN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SYSTEMASSIGNEDMANAGEDIDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FEDERATEDIDENTITY)]
        public string MicrosoftGraphEndPoint;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADCERTIFICATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_APPONLYAADTHUMBPRINT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACSAPPONLY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ACCESSTOKEN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ENVIRONMENTVARIABLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SYSTEMASSIGNEDMANAGEDIDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FEDERATEDIDENTITY)]
        public string AzureADLoginEndPoint;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_AZUREAD_WORKLOAD_IDENTITY)]
        public SwitchParameter AzureADWorkloadIdentity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_OSLOGIN)]
        public SwitchParameter OSLogin;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_INTERACTIVE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEVICELOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_OSLOGIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CREDENTIALS)]
        public SwitchParameter PersistLogin;

        private static readonly string[] sourceArray = ["stop", "ignore", "silentlycontinue"];

        protected override void ProcessRecord()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

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
            LogDebug($"PnP PowerShell Cmdlets ({new SemanticVersion(Assembly.GetExecutingAssembly().GetName().Version)})");

            if (!string.IsNullOrEmpty(Url))
            {
                Url = Url.TrimEnd('/');

                if (!Url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) && !Url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                {
                    Url = $"https://{Url}";
                }
            }

            PnPConnection newConnection = null;

            PSCredential credentials = null;
            if (Credentials != null)
            {
                credentials = Credentials.Credential;
            }

            if (ValidateConnection)
            {
                if (PingHost(new Uri(Url).Host) == false)
                {
                    throw new PSArgumentException("Host not reachable");
                }
            }

            if (ParameterSpecified(nameof(Connection)))
            {
                // Reuse some parameters of the passed in connection
                LogDebug("Reusing some of the connection parameters from passed in connection");
                ClientId = Connection.ClientId;
            }

            if (AzureEnvironment == AzureEnvironment.Custom)
            {
                SetCustomEndpoints();
            }

            // Connect using the used set parameters
            switch (ParameterSetName)
            {
                case ParameterSet_DEVICELOGIN:
                    newConnection = ConnectDeviceLogin();
                    break;
                case ParameterSet_APPONLYAADCERTIFICATE:
                    newConnection = ConnectAppOnlyWithCertificate();
                    break;
                case ParameterSet_APPONLYAADTHUMBPRINT:
                    newConnection = ConnectAppOnlyWithCertificate();
                    break;
                case ParameterSet_ACCESSTOKEN:
                    newConnection = ConnectAccessToken();
                    break;
                case ParameterSet_ACSAPPONLY:
                    newConnection = ConnectACSAppOnly();
                    break;
                case ParameterSet_CREDENTIALS:
                    newConnection = ConnectCredentials(credentials);
                    break;
                case ParameterSet_SYSTEMASSIGNEDMANAGEDIDENTITY:
                case ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYCLIENTID:
                case ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYPRINCIPALID:
                case ParameterSet_USERASSIGNEDMANAGEDIDENTITYBYAZURERESOURCEID:
                    newConnection = ConnectManagedIdentity();
                    break;
                case ParameterSet_INTERACTIVE:
                    newConnection = ConnectInteractive();
                    break;
                case ParameterSet_ENVIRONMENTVARIABLE:
                    newConnection = ConnectEnvironmentVariable();
                    break;
                case ParameterSet_AZUREAD_WORKLOAD_IDENTITY:
                    newConnection = ConnectAzureADWorkloadIdentity();
                    break;
                case ParameterSet_OSLOGIN:
                    newConnection = ConnectWithOSLogin();
                    break;
                case ParameterSet_FEDERATEDIDENTITY:
                    newConnection = ConnectFederatedIdentity();
                    break;
            }

            // Ensure a connection instance has been created by now
            if (newConnection == null)
            {
                // No connection instance was created
                throw new PSInvalidOperationException("Unable to connect using provided arguments");
            }

            // Connection has been established
            LogDebug($"Connected");

            if (newConnection.Url != null)
            {
                var hostUri = new Uri(newConnection.Url);
                Environment.SetEnvironmentVariable("PNPPSHOST", hostUri.Host);
                Environment.SetEnvironmentVariable("PNPPSSITE", hostUri.LocalPath);
            }
            else
            {
                Environment.SetEnvironmentVariable("PNPPSHOST", "GRAPH");
                Environment.SetEnvironmentVariable("PNPPSSITE", "GRAPH");
            }

            if (ValidateConnection)
            {
                // Try requesting the site Id to validate that the site to which is being connected exists
                newConnection.Context.Load(newConnection.Context.Site, p => p.Id);
                try
                {
                    newConnection.Context.ExecuteQueryRetry();
                    LogInformational($"Site at {Url} exists");
                }
                catch (System.Net.WebException e) when (e.Message.Contains("404"))
                {
                    LogError($"Site at {Url} does not exist");
                    //throw new PSInvalidOperationException($"The specified site {Url} does not exist", e);
                }
                catch (TargetInvocationException tex)
                {
                    Exception innermostException = tex;
                    while (innermostException.InnerException != null) innermostException = innermostException.InnerException;

                    string errorMessage;
                    if (innermostException is System.Net.WebException wex)
                    {
                        using var streamReader = new StreamReader(wex.Response.GetResponseStream());
                        errorMessage = $"{wex.Status}: {wex.Message} Response received: {streamReader.ReadToEnd()}";
                    }
                    else
                    {
                        errorMessage = innermostException.Message;
                    }

                    // If the ErrorAction is not set to Stop, Ignore or SilentlyContinue throw an exception, otherwise just continue
                    if (!sourceArray.Contains(ErrorActionSetting.ToLowerInvariant()))
                    {
                        throw new PSInvalidOperationException(errorMessage);
                    }
                }
            }

            if (ReturnConnection)
            {
                WriteObject(newConnection);
            }
            else
            {
                PnPConnection.Current = newConnection;
            }
            if (CreateDrive && newConnection.Context != null)
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
        }

        #region Connect Types

        /// <summary>
        /// Connect using the paramater set TOKEN
        /// </summary>
        /// <returns>PnPConnection based on the parameters provided in the parameter set</returns>
        private PnPConnection ConnectACSAppOnly()
        {
            LogDebug("Connecting using the SharePoint Online Access Control Services(ACS) App-Only");

            CmdletMessageWriter.WriteFormattedMessage(this, new CmdletMessageWriter.Message { Text = "Connecting with Client Secret uses legacy authentication and provides limited functionality. We can for instance not execute requests towards the Microsoft Graph, which limits cmdlets related to Microsoft Teams, Microsoft Planner, Microsoft Flow and Microsoft 365 Groups. You can hide this warning by using Connect-PnPOnline [your parameters] -WarningAction Ignore", Formatted = true, Type = CmdletMessageWriter.MessageType.Warning });
            if (Connection?.ClientId == ClientId &&
                Connection?.ClientSecret == ClientSecret &&
                Connection?.Tenant == Realm)
            {
                ReuseAuthenticationManager();
            }

            if (ClientId == null)
            {
                ClientId = GetAppId();
                if (ClientId != null)
                {
                    LogDebug("Using Managed AppId from secure store");
                }
            }
            LogDebug($"Using ClientID {ClientId}");

            return PnPConnection.CreateWithACSAppOnly(new Uri(Url), Realm, ClientId, ClientSecret, TenantAdminUrl, AzureEnvironment);
        }


        /// <summary>
        /// Connect using the parameter set DEVICELOGIN
        /// </summary>
        /// <returns>PnPConnection based on the parameters provided in the parameter set</returns>
        private PnPConnection ConnectDeviceLogin()
        {
            LogDebug("Connecting using Device Login");

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

                    if (Connection != null)
                    {
                        if (Connection.Url != null)
                        {
                            oldUri = new Uri(Connection.Url);
                        }
                    }
                    if (oldUri != null && oldUri.Host == new Uri(Url).Host && Connection?.ConnectionMethod == ConnectionMethod.DeviceLogin)
                    {
                        ReuseAuthenticationManager();
                    }
                    var clientId = "";
                    if (ParameterSpecified(nameof(ClientId)))
                    {
                        clientId = ClientId;
                    }
                    else
                    {
                        clientId = GetAppId();
                        if (clientId == null)
                        {
                            var environmentAppId = Environment.GetEnvironmentVariable("ENTRAID_APP_ID") ?? Environment.GetEnvironmentVariable("ENTRAID_CLIENT_ID") ?? Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
                            if (!string.IsNullOrEmpty(environmentAppId))
                            {
                                clientId = environmentAppId;
                            }
                            else
                            {
                                CmdletMessageWriter.WriteFormattedMessage(this, new CmdletMessageWriter.Message { Text = "Please specify a valid client id for an Entra ID App Registration.", Formatted = true, Type = CmdletMessageWriter.MessageType.Warning });
                                ThrowTerminatingError(new ErrorRecord(new NotSupportedException(), "CLIENTIDREQUIRED", ErrorCategory.AuthenticationError, this));
                            }
                        }
                        else
                        {
                            messageWriter.LogDebug("Using Managed AppId from secure store");
                        }
                    }

                    var returnedConnection = PnPConnection.CreateWithDeviceLogin(clientId, Url, Tenant, messageWriter, AzureEnvironment, cancellationTokenSource, PersistLogin, Host);
                    connection = returnedConnection;
                    messageWriter.Finished = true;
                }
                catch (Exception ex)
                {
                    messageWriter.LogWarning(ex.Message, false);
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
            LogDebug("Connecting using Entra ID App-Only using a certificate");
            LogDebug($"Using ClientID {ClientId}");

            if (ParameterSpecified(nameof(CertificatePath)))
            {
                if (!Path.IsPathRooted(CertificatePath))
                {
                    CertificatePath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path,
                               CertificatePath);
                }
                if (!File.Exists(CertificatePath))
                {
                    throw new FileNotFoundException("Certificate not found");
                }

                if (!ParameterSpecified(nameof(X509KeyStorageFlags)))
                {
                    X509KeyStorageFlags = X509KeyStorageFlags.Exportable |
                        X509KeyStorageFlags.UserKeySet |
                        X509KeyStorageFlags.PersistKeySet;
                }

                X509Certificate2 certificate = CertificateHelper.GetCertificateFromPath(this, CertificatePath, CertificatePassword, X509KeyStorageFlags);
                if (Connection?.ClientId == ClientId &&
                    Connection?.Tenant == Tenant &&
                    Connection?.Certificate?.Thumbprint == certificate.Thumbprint)

                {
                    ReuseAuthenticationManager();
                }

                return PnPConnection.CreateWithCert(new Uri(Url), ClientId, Tenant, TenantAdminUrl, AzureEnvironment, certificate, true);
            }
            else if (ParameterSpecified(nameof(CertificateBase64Encoded)))
            {
                var certificateBytes = Convert.FromBase64String(CertificateBase64Encoded);
                if (!ParameterSpecified(nameof(X509KeyStorageFlags)))
                {
                    X509KeyStorageFlags = X509KeyStorageFlags.Exportable |
                        X509KeyStorageFlags.UserKeySet |
                        X509KeyStorageFlags.PersistKeySet;
                }
                var certificate = new X509Certificate2(certificateBytes, CertificatePassword, X509KeyStorageFlags);

                if (Connection?.ClientId == ClientId &&
                    Connection?.Tenant == Tenant &&
                    Connection?.Certificate?.Thumbprint == certificate.Thumbprint)
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
                if (Connection?.ClientId == ClientId &&
                                    Connection?.Tenant == Tenant &&
                                    Connection?.Certificate?.Thumbprint == certificate.Thumbprint)
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
            LogDebug("Connecting using a provided Access Token");

            return PnPConnection.CreateWithAccessToken(!string.IsNullOrEmpty(Url) ? new Uri(Url) : null, AccessToken, TenantAdminUrl);
        }

        /// <summary>
        /// Connect using provided credentials or the current credentials
        /// </summary>
        /// <returns>PnPConnection based on credentials authentication</returns>
        private PnPConnection ConnectCredentials(PSCredential credentials, InitializationType initializationType = InitializationType.Credentials)
        {
            LogDebug("Connecting using username and password");

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
            if (ClientId == null && !TransformationOnPrem)
            {
                ClientId = GetAppId();
                if (ClientId == null)
                {
                    var environmentAppId = Environment.GetEnvironmentVariable("ENTRAID_APP_ID") ?? Environment.GetEnvironmentVariable("ENTRAID_CLIENT_ID") ?? Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
                    if (!string.IsNullOrEmpty(environmentAppId))
                    {
                        ClientId = environmentAppId;
                    }
                    else
                    {
                        CmdletMessageWriter.WriteFormattedMessage(this, new CmdletMessageWriter.Message { Text = "Please specify a valid client id for an Entra ID App Registration.", Formatted = true, Type = CmdletMessageWriter.MessageType.Warning });
                        ThrowTerminatingError(new ErrorRecord(new NotSupportedException(), "CLIENTIDREQUIRED", ErrorCategory.AuthenticationError, this));
                    }
                }
                else
                {
                    LogDebug("Using Managed AppId from secure store");
                }
            }

            if (Connection?.ClientId == ClientId)
            {
                if (credentials != null && Connection?.PSCredential?.UserName == credentials.UserName &&
                   Connection?.PSCredential.GetNetworkCredential().Password == credentials.GetNetworkCredential().Password)
                {
                    ReuseAuthenticationManager();
                }
            }
            LogDebug($"Using ClientID {ClientId}");
            return PnPConnection.CreateWithCredentials(this, new Uri(Url),
                                                               credentials,
                                                               CurrentCredentials,
                                                               TenantAdminUrl,
                                                               PersistLogin,
                                                               Host,
                                                               AzureEnvironment,
                                                               ClientId,
                                                               RedirectUri, TransformationOnPrem, initializationType);
        }


        private PnPConnection ConnectManagedIdentity()
        {
            LogDebug("Connecting using an Azure Managed Identity");

            LogDebug($"ClientID: {UserAssignedManagedIdentityClientId}");
            return PnPConnection.CreateWithManagedIdentity(Url, TenantAdminUrl, UserAssignedManagedIdentityObjectId, UserAssignedManagedIdentityClientId, UserAssignedManagedIdentityAzureResourceId);
        }

        private PnPConnection ConnectInteractive()
        {
            LogDebug("Connecting using Interactive login");

            if (ClientId == null)
            {
                ClientId = GetAppId();
                if (ClientId == null)
                {
                    var environmentAppId = Environment.GetEnvironmentVariable("ENTRAID_APP_ID") ?? Environment.GetEnvironmentVariable("ENTRAID_CLIENT_ID") ?? Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
                    if (!string.IsNullOrEmpty(environmentAppId))
                    {
                        ClientId = environmentAppId;
                    }
                    else
                    {
                        CmdletMessageWriter.WriteFormattedMessage(this, new CmdletMessageWriter.Message { Text = "Please specify a valid client id for an Entra ID App Registration.", Formatted = true, Type = CmdletMessageWriter.MessageType.Warning });
                        ThrowTerminatingError(new ErrorRecord(new NotSupportedException(), "CLIENTIDREQUIRED", ErrorCategory.AuthenticationError, this));
                    }
                }
                else
                {
                    LogDebug("Using Managed AppId from secure store");
                }
            }
            if (Connection?.ClientId == ClientId && Connection?.ConnectionMethod == ConnectionMethod.Credentials)
            {
                if (IsSameOrAdminHost(new Uri(Url), new Uri(Connection.Url)))
                {
                    ReuseAuthenticationManager();
                }
            }
            LogDebug($"Using ClientID {ClientId}");
            return PnPConnection.CreateWithInteractiveLogin(new Uri(Url.ToLower()), ClientId, TenantAdminUrl, AzureEnvironment, cancellationTokenSource, ForceAuthentication, Tenant, false, PersistLogin, Host);
        }

        private PnPConnection ConnectEnvironmentVariable(InitializationType initializationType = InitializationType.EnvironmentVariable)
        {
            LogDebug("Connecting using information from environment variables");

            string username = Environment.GetEnvironmentVariable("AZURE_USERNAME") ?? Environment.GetEnvironmentVariable("ENTRAID_USERNAME");
            string password = Environment.GetEnvironmentVariable("AZURE_PASSWORD") ?? Environment.GetEnvironmentVariable("ENTRAID_PASSWORD");
            string azureClientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID") ?? Environment.GetEnvironmentVariable("ENTRAID_APP_ID") ?? Environment.GetEnvironmentVariable("ENTRAID_CLIENT_ID");
            string azureCertificatePath = Environment.GetEnvironmentVariable("AZURE_CLIENT_CERTIFICATE_PATH") ?? Environment.GetEnvironmentVariable("ENTRAID_APP_CERTIFICATE_PATH") ?? Environment.GetEnvironmentVariable("ENTRAID_CLIENT_CERTIFICATE_PATH");
            string azureCertPassword = Environment.GetEnvironmentVariable("AZURE_CLIENT_CERTIFICATE_PASSWORD") ?? Environment.GetEnvironmentVariable("ENTRAID_APP_CERTIFICATE_PASSWORD") ?? Environment.GetEnvironmentVariable("ENTRAID_CLIENT_CERTIFICATE_PASSWORD");

            if (azureClientId == null)
            {
                azureClientId = GetAppId();
                if (azureClientId != null)
                {
                    LogDebug("Using Managed AppId from secure store");
                }
            }

            if (!string.IsNullOrEmpty(azureCertificatePath) && !string.IsNullOrEmpty(azureCertPassword))
            {
                if (!Path.IsPathRooted(azureCertificatePath))
                {
                    azureCertificatePath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path,
                               azureCertificatePath);
                }

                if (!File.Exists(azureCertificatePath))
                {
                    throw new FileNotFoundException("Certificate not found");
                }

                if (string.IsNullOrEmpty(azureClientId))
                {
                    throw new ArgumentNullException("Unable to connect using available environment variables. Please provide necessary value for AZURE_CLIENT_ID environment variable");
                }

                if (!ParameterSpecified(nameof(Tenant)))
                {
                    throw new ArgumentNullException($"{nameof(Tenant)} must be provided when trying to authenticate using Azure environment credentials for Service principal with certificate method.");
                }

                SecureString secPassword = StringToSecureString(azureCertPassword);

                if (!ParameterSpecified(nameof(X509KeyStorageFlags)))
                {
                    X509KeyStorageFlags = X509KeyStorageFlags.Exportable |
                        X509KeyStorageFlags.UserKeySet |
                        X509KeyStorageFlags.PersistKeySet;
                }

                X509Certificate2 certificate = CertificateHelper.GetCertificateFromPath(this, azureCertificatePath, secPassword, X509KeyStorageFlags);
                if (Connection?.ClientId == azureClientId &&
                    Connection?.Tenant == Tenant &&
                    Connection?.Certificate?.Thumbprint == certificate.Thumbprint)
                {
                    ReuseAuthenticationManager();
                }

                LogDebug($"ClientID: {azureClientId}");

                return PnPConnection.CreateWithCert(new Uri(Url), azureClientId, Tenant, TenantAdminUrl, AzureEnvironment, certificate, true);
            }

            else if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (string.IsNullOrEmpty(azureClientId))
                {
                    CmdletMessageWriter.WriteFormattedMessage(this, new CmdletMessageWriter.Message { Text = "Please specify a valid client id for an Entra ID App Registration.", Formatted = true, Type = CmdletMessageWriter.MessageType.Warning });
                    ThrowTerminatingError(new ErrorRecord(new NotSupportedException(), "CLIENTIDREQUIRED", ErrorCategory.AuthenticationError, this)); ;
                }

                SecureString secPassword = StringToSecureString(password);
                var credentials = new PSCredential(username, secPassword);

                if (Connection?.ClientId == azureClientId)
                {
                    if (credentials != null && Connection?.PSCredential?.UserName == credentials.UserName &&
                       Connection?.PSCredential.GetNetworkCredential().Password == credentials.GetNetworkCredential().Password)
                    {
                        ReuseAuthenticationManager();
                    }
                }
                LogDebug($"ClientID: {azureClientId}");

                return PnPConnection.CreateWithCredentials(this, new Uri(Url),
                                                                   credentials,
                                                                   CurrentCredentials,
                                                                   TenantAdminUrl,
                                                                   PersistLogin,
                                                                   Host,
                                                                   AzureEnvironment,
                                                                   azureClientId,
                                                                   RedirectUri, TransformationOnPrem, initializationType);
            }

            return null;
        }

        private PnPConnection ConnectAzureADWorkloadIdentity()
        {
            LogDebug("Connecting using Entra ID Workload Identity");

            return PnPConnection.CreateWithAzureADWorkloadIdentity(Url, TenantAdminUrl);
        }

        private PnPConnection ConnectWithOSLogin()
        {
            LogDebug("Connecting using Web Account Manager (WAM)");

            if (ClientId == null)
            {
                ClientId = GetAppId();
                if (ClientId == null)
                {
                    var environmentAppId = Environment.GetEnvironmentVariable("ENTRAID_APP_ID") ?? Environment.GetEnvironmentVariable("ENTRAID_CLIENT_ID") ?? Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
                    if (!string.IsNullOrEmpty(environmentAppId))
                    {
                        ClientId = environmentAppId;
                    }
                }
                else
                {
                    LogDebug("Using Managed AppId from secure store");
                }
            }
            if (Connection?.ClientId == ClientId && Connection?.ConnectionMethod == ConnectionMethod.Credentials)
            {
                if (IsSameOrAdminHost(new Uri(Url), new Uri(Connection.Url)))
                {
                    ReuseAuthenticationManager();
                }
            }

            LogDebug($"Using ClientID {ClientId}");
            if (PnPConnection.CacheEnabled(Url, ClientId))
            {
                WriteObject("Cache used. Clear the cache entry with Disconnect-PnPOnline");
            }
            return PnPConnection.CreateWithInteractiveLogin(new Uri(Url.ToLower()), ClientId, TenantAdminUrl, AzureEnvironment, cancellationTokenSource, ForceAuthentication, Tenant, true, PersistLogin, Host);
        }

        private PnPConnection ConnectFederatedIdentity()
        {
            LogDebug("Connecting using Federated Identity Credentials");

            return PnPConnection.CreateWithFederatedIdentity(Url, TenantAdminUrl, ClientId, Tenant);
        }
        #endregion

        #region Helper methods

        private static bool PingHost(string nameOrAddress)
        {

            try
            {
                var conn = System.Net.Dns.GetHostEntry(nameOrAddress);
                return true;
            }
            catch
            {
                return false;
            }
        }
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

        private string GetAppId()
        {
            var connectionUri = new Uri(Url);
            // Try to get the credentials by full url
            string appId = PnPConnection.GetCacheClientId(connectionUri.ToString()) ?? Utilities.CredentialManager.GetAppId(connectionUri.ToString());
            if (appId == null)
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
                        appId = PnPConnection.GetCacheClientId(connectionUri.ToString()) ?? Utilities.CredentialManager.GetAppId(pathUrl);
                        if (appId != null)
                        {
                            break;
                        }
                    }
                }

                if (appId == null)
                {
                    // Try to find the credentials by schema and hostname
                    appId = PnPConnection.GetCacheClientId(connectionUri.ToString()) ?? Utilities.CredentialManager.GetAppId(connectionUri.Scheme + "://" + connectionUri.Host);

                    if (appId == null)
                    {
                        // Maybe added with an extra slash?
                        appId = PnPConnection.GetCacheClientId(connectionUri.ToString()) ?? Utilities.CredentialManager.GetAppId(connectionUri.Scheme + "://" + connectionUri.Host + "/");

                        if (appId == null)
                        {
                            // try to find the credentials by hostname
                            appId = PnPConnection.GetCacheClientId(connectionUri.ToString()) ?? Utilities.CredentialManager.GetAppId(connectionUri.Host);
                        }
                    }
                }

            }

            return appId;
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
            var contextSettings = Connection.Context?.GetContextSettings();
            PnPConnection.CachedAuthenticationManager = contextSettings?.AuthenticationManager;
        }

        private static SecureString StringToSecureString(string inputString)
        {
            SecureString secPassword = new SecureString();
            foreach (char ch in inputString)
            {
                secPassword.AppendChar(ch);
            }
            secPassword.MakeReadOnly();

            return secPassword;
        }

        private void SetCustomEndpoints()
        {
            if (!string.IsNullOrWhiteSpace(MicrosoftGraphEndPoint))
            {
                Environment.SetEnvironmentVariable("MicrosoftGraphEndPoint", MicrosoftGraphEndPoint, EnvironmentVariableTarget.Process);
            }
            if (!string.IsNullOrWhiteSpace(AzureADLoginEndPoint))
            {
                Environment.SetEnvironmentVariable("AzureADLoginEndPoint", AzureADLoginEndPoint, EnvironmentVariableTarget.Process);
            }
        }

        #endregion
    }
}
