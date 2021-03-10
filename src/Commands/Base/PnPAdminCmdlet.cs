using System;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Base
{
    public abstract class PnPAdminCmdlet : PnPSharePointCmdlet
    {
        private Tenant _tenant;
        private Uri _baseUri;

        public Tenant Tenant
        {
            get
            {
                if (_tenant == null)
                {
                    _tenant = new Tenant(ClientContext);
                }
                return _tenant;
            }
        }

        public Uri BaseUri => _baseUri;

        internal ClientContext SiteContext;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            
            if (PnPConnection.Current == null)
            {
                throw new InvalidOperationException(Resources.NoSharePointConnection);
            }
            if (ClientContext == null)
            {
                throw new InvalidOperationException(Resources.NoSharePointConnection);
            }
            SiteContext = PnPConnection.Current.Context;
            
            PnPConnection.Current.CacheContext();

            if (PnPConnection.Current.TenantAdminUrl != null &&
                (PnPConnection.Current.ConnectionType == ConnectionType.O365))
            {
                var uri = new Uri(PnPConnection.Current.Url);
                var uriParts = uri.Host.Split('.');
                if (uriParts[0].ToLower().EndsWith("-admin"))
                {
                    _baseUri = new Uri($"{uri.Scheme}://{uriParts[0].ToLower().Replace("-admin", "")}.{string.Join(".", uriParts.Skip(1))}{(!uri.IsDefaultPort ? ":" + uri.Port : "")}");
                }
                else
                {
                    _baseUri = new Uri($"{uri.Scheme}://{uri.Authority}");
                }
                IsDeviceLogin(PnPConnection.Current.TenantAdminUrl);
                PnPConnection.Current.CloneContext(PnPConnection.Current.TenantAdminUrl);
            }
            else
            {
                Uri uri = new Uri(ClientContext.Url);
                var uriParts = uri.Host.Split('.');
                if (!uriParts[0].EndsWith("-admin") &&
                    PnPConnection.Current.ConnectionType == ConnectionType.O365)
                {                    
                    _baseUri = new Uri($"{uri.Scheme}://{uri.Authority}");

                    // Remove -my postfix from the tenant name, if present, to allow elevation to the admin context even when being connected to the MySite
                    var tenantName = uriParts[0].EndsWith("-my") ? uriParts[0].Remove(uriParts[0].Length - 3, 3) : uriParts[0];

                    var adminUrl = $"https://{tenantName}-admin.{string.Join(".", uriParts.Skip(1))}";
                    IsDeviceLogin(adminUrl);
                    PnPConnection.Current.Context =
                        PnPConnection.Current.CloneContext(adminUrl);
                }
                else
                {
                    _baseUri = new Uri($"{uri.Scheme}://{uriParts[0].ToLower().Replace("-admin", "")}{(uriParts.Length > 1 ? $".{string.Join(".", uriParts.Skip(1))}" : string.Empty)}{(!uri.IsDefaultPort ? ":" + uri.Port : "")}");
                }
            }
        }

        private void IsDeviceLogin(string tenantAdminUrl)
        {
            if (PnPConnection.Current.ConnectionMethod == Model.ConnectionMethod.DeviceLogin)
            {
                if (tenantAdminUrl != PnPConnection.Current.Url)
                {
                    throw new PSInvalidOperationException($"You used a device login connection to authenticate to SharePoint. We do not support automatically switching context to the tenant administration site which is required to execute this cmdlet. Please use Connect-PnPOnline and connect to '{tenantAdminUrl}' with the appropriate connection parameters");
                }
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            PnPConnection.Current.RestoreCachedContext(PnPConnection.Current.Url);
        }
    }
}
