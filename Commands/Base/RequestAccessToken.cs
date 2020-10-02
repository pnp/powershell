using PnP.Framework.Utilities;

using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Request, "AccessToken")]
    public class RequestAccessToken : BasePSCmdlet
    {

        [Parameter(Mandatory = false)]
        public string ClientId = PnPConnection.PnPManagementShellClientId; // defaults to PnPManagementShell

        [Parameter(Mandatory = false)]
        public string Resource;

        [Parameter(Mandatory = false)]
        public List<string> Scopes = new List<string> { "AllSites.FullControl" };

        [Parameter(Mandatory = false)]
        public SwitchParameter Decoded;

        [Parameter(Mandatory = false)]
        public SwitchParameter SetAsCurrent;

        [Parameter(Mandatory = false)]
        public PSCredential Credentials;

        [Parameter(Mandatory = false)]
        public string TenantUrl;

        protected override void ProcessRecord()
        {

            Uri tenantUri = null;
            if (string.IsNullOrEmpty(TenantUrl) && PnPConnection.CurrentConnection != null)
            {

                HttpClient client = new HttpClient();
                var uri = new Uri(PnPConnection.CurrentConnection.Url);
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
            string password;
            string username;
            if (ParameterSpecified(nameof(Credentials)))
            {
                password = EncryptionUtility.ToInsecureString(Credentials.Password);
                username = Credentials.UserName;
            }
            else if (PnPConnection.CurrentConnection != null)
            {
                password = EncryptionUtility.ToInsecureString(PnPConnection.CurrentConnection.PSCredential.Password);
                username = PnPConnection.CurrentConnection.PSCredential.UserName;
            }
            else
            {
                throw new InvalidOperationException("Either a connection needs to be made by Connect-PnPOnline or Credentials needs to be specified");
            }

            GenericToken token = null;
            if (ParameterSpecified(nameof(Resource)) && !string.IsNullOrEmpty(Resource))
            {
                token = GenericToken.AcquireV1Token(tenantId, ClientId, username, password, Resource);
            }

            if (ParameterSpecified(nameof(Scopes)) && Scopes.Count > 0)
            {
                token = GenericToken.AcquireV2Token(tenantId, ClientId, username, password, Scopes.ToArray());
            }

            if (SetAsCurrent.IsPresent)
            {
                if (PnPConnection.CurrentConnection != null)
                {
                    if(token == null)
                    {
                        throw new InvalidOperationException($"-{nameof(SetAsCurrent)} can't be performed as no valid token could be retrieved");
                    }

                    PnPConnection.CurrentConnection.AddToken(Enums.TokenAudience.Other, token);
                }
                else
                {
                    throw new InvalidOperationException($"-{nameof(SetAsCurrent)} can only be used when having an active connection using Connect-PnPOnline");
                }
            }

            if (Decoded.IsPresent)
            {
                WriteObject(token.ParsedToken);
            }
            else
            {
                WriteObject(token.AccessToken);
            }
        }
    }
}