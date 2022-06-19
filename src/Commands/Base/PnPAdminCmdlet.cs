using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base cmdlet for cmdlets that require running on against the admin site collection
    /// </summary>
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

        /// <summary>
        /// ClientContext which was the active context before elevating to the admin context
        /// </summary>
        internal ClientContext SiteContext;

        /// <summary>
        /// Checks if the current context has been set up using a device login. In that case we cannot elevate to an admin context.
        /// </summary>
        private void IsDeviceLogin(string tenantAdminUrl)
        {
            if (Connection.ConnectionMethod == Model.ConnectionMethod.DeviceLogin)
            {
                if (tenantAdminUrl != Connection.Url)
                {
                    throw new PSInvalidOperationException($"You used a device login connection to authenticate to SharePoint. We do not support automatically switching context to the tenant administration site which is required to execute this cmdlet. Please use Connect-PnPOnline and connect to '{tenantAdminUrl}' with the appropriate connection parameters");
                }
            }
        }

        /// <summary>
        /// Executed before executing the specific admin cmdlet logic
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            
            // Keep an instance of the client context which is currently active before elevating to an admin client context so we can restore it afterwards
            SiteContext = Connection.Context;
            
            Connection.CacheContext();

            if (Connection.TenantAdminUrl != null &&
               (Connection.ConnectionType == ConnectionType.O365))
            {
                var uri = new Uri(Connection.Url);
                var uriParts = uri.Host.Split('.');
                if (uriParts[0].ToLower().EndsWith("-admin"))
                {
                    _baseUri = new Uri($"{uri.Scheme}://{uriParts[0].ToLower().Replace("-admin", "")}.{string.Join(".", uriParts.Skip(1))}{(!uri.IsDefaultPort ? ":" + uri.Port : "")}");
                }
                else
                {
                    _baseUri = new Uri($"{uri.Scheme}://{uri.Authority}");
                }
                IsDeviceLogin(Connection.TenantAdminUrl);
                Connection.CloneContext(Connection.TenantAdminUrl);
            }
            else
            {
                Uri uri = new Uri(ClientContext.Url);
                var uriParts = uri.Host.Split('.');
                if (!uriParts[0].EndsWith("-admin") &&
                    Connection.ConnectionType == ConnectionType.O365)
                {                    
                    _baseUri = new Uri($"{uri.Scheme}://{uri.Authority}");

                    // Remove -my postfix from the tenant name, if present, to allow elevation to the admin context even when being connected to the MySite
                    var tenantName = uriParts[0].EndsWith("-my") ? uriParts[0].Remove(uriParts[0].Length - 3, 3) : uriParts[0];

                    var adminUrl = $"https://{tenantName}-admin.{string.Join(".", uriParts.Skip(1))}";
                    IsDeviceLogin(adminUrl);
                    Connection.Context =
                        Connection.CloneContext(adminUrl);
                }
                else
                {
                    _baseUri = new Uri($"{uri.Scheme}://{uriParts[0].ToLower().Replace("-admin", "")}{(uriParts.Length > 1 ? $".{string.Join(".", uriParts.Skip(1))}" : string.Empty)}{(!uri.IsDefaultPort ? ":" + uri.Port : "")}");
                }
            }
        }

        /// <summary>
        /// Executed after completing the specific admin cmdlet logic
        /// </summary>
        protected override void EndProcessing()
        {
            base.EndProcessing();

            // Restore the client context to the context which was used before the admin context elevation
            Connection.Context = SiteContext;
        }
    }
}