using PnP.Framework;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Management.Automation;
using System.Security;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Request, "PnPAccessToken")]
    public class RequestAccessToken : PnPConnectedCmdlet
    {
        [Parameter(Mandatory = false)]
        [Alias("ApplicationId")]
        public string ClientId = PnPConnection.PnPManagementShellClientId; // defaults to PnPManagementShell

        [Parameter(Mandatory = false)]
        [Obsolete("Resource is deprecated, use Scopes instead.")]
        public string Resource;

        [Parameter(Mandatory = false)]
        public string[] Scopes = new string[] { "AllSites.FullControl" };

        [Parameter(Mandatory = false)]
        public SwitchParameter Decoded;

        [Parameter(Mandatory = false)]
        public PSCredential Credentials;

        [Parameter(Mandatory = false)]
        public string TenantUrl;

        [Parameter(Mandatory = false)]
        public AzureEnvironment AzureEnvironment;

        protected override void ProcessRecord()
        {
            Uri tenantUri = null;
            if (string.IsNullOrEmpty(TenantUrl) && Connection != null)
            {
                var uri = new Uri(Connection.Url);
                var uriParts = uri.Host.Split('.');
                if (uriParts[0].ToLower().EndsWith("-admin"))
                {
                    tenantUri =
                        new Uri(
                            $"{uri.Scheme}://{uriParts[0].ToLower().Replace("-admin", "")}.{string.Join(".", uriParts.Skip(1))}{(!uri.IsDefaultPort ? ":" + uri.Port : "")}");
                }
                else
                {
                    tenantUri = new Uri($"{uri.Scheme}://{uri.Authority}");
                }
            }
            else if (!string.IsNullOrEmpty(TenantUrl))
            {
                tenantUri = new Uri(TenantUrl);
            }
            else
            {
                throw new InvalidOperationException("Either a connection needs to be made by Connect-PnPOnline or TenantUrl and Credentials needs to be specified");
            }

            var tenantId = Microsoft.SharePoint.Client.TenantExtensions.GetTenantIdByUrl(tenantUri.ToString());
            SecureString password;
            string username;
            AuthenticationManager authManager = null;
            if (ParameterSpecified(nameof(Credentials)))
            {
                password = Credentials.Password;
                username = Credentials.UserName;
                authManager = new AuthenticationManager(ClientId, username, password, azureEnvironment: AzureEnvironment);
            }
            else if (Connection != null)
            {
                authManager = Connection.Context.GetContextSettings().AuthenticationManager;
            }
            else
            {
                throw new InvalidOperationException("Either a connection needs to be made by Connect-PnPOnline or Credentials needs to be specified");
            }

            var token = string.Empty;

            using (authManager)
            {
                if (ParameterSpecified(nameof(Scopes)))
                {
                    token = authManager.GetAccessTokenAsync(Scopes).GetAwaiter().GetResult();
                }
                else
                {
                    token = authManager.GetAccessTokenAsync(Connection.Url).GetAwaiter().GetResult();
                }
            }

            if (Decoded.IsPresent)
            {

                WriteObject(new JwtSecurityToken(token));
            }
            else
            {
                WriteObject(token);
            }
        }
    }
}