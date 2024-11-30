using System;
using System.Linq;
using System.Management.Automation;
using System.Net;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base cmdlet for cmdlets that require running on against the admin site collection
    /// </summary>
    public abstract class PnPAdminCmdlet : PnPSharePointCmdlet
    {        
        private Tenant _tenant;
        /// <summary>
        /// Tenant instance
        /// </summary>
        public Tenant Tenant
        {
            get
            {
                if (_tenant == null)
                {
                    _tenant = new Tenant(AdminContext);
                }
                return _tenant;
            }
        }
        
        private Uri _baseUri;
        /// <summary>
        /// The root sitecollection URL of the SharePoint Online tenant
        /// </summary>
        public Uri BaseUri => _baseUri;

        /// <summary>
        /// ClientContext which points to the admin context
        /// </summary>
        internal ClientContext AdminContext;        

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
            
            Connection.CacheContext();

            string tenantAdminUrl;
            if (Connection.TenantAdminUrl != null && Connection.ConnectionType == ConnectionType.O365)
            {
                // An explicit SharePoint Online Admin Center URL has been provided in the connect, use it                
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

                tenantAdminUrl = Connection.TenantAdminUrl;
            }
            else
            {
                // No explicit SharePoint Online Admin Center URL has been provided in the connect, try to guess it using the default <tenant>-admin.sharepoint.<tld> syntax
                Uri uri = new Uri(ClientContext.Url);
                var uriParts = uri.Host.Split('.');

                if (!uriParts[0].EndsWith("-admin") && Connection.ConnectionType == ConnectionType.O365)
                {
                    // The current connection has not been made to the SharePoint Online Admin Center, try to predict the admin center URL        
                    _baseUri = new Uri($"{uri.Scheme}://{uri.Authority}");

                    // Remove -my postfix from the tenant name, if present, to allow elevation to the admin context even when being connected to the MySite
                    var tenantName = uriParts[0].EndsWith("-my") ? uriParts[0].Remove(uriParts[0].Length - 3, 3) : uriParts[0];

                    tenantAdminUrl = $"https://{tenantName}-admin.{string.Join(".", uriParts.Skip(1))}";
                }
                else
                {
                    // The current connection has been made to the SharePoint Online Admin Center URL already, we can use it as is
                    WriteVerbose($"Already connect to the SharePoint Online Admin Center at '{ClientContext.Url}'");
                    _baseUri = new Uri($"{uri.Scheme}://{uriParts[0].ToLower().Replace("-admin", "")}{(uriParts.Length > 1 ? $".{string.Join(".", uriParts.Skip(1))}" : string.Empty)}{(!uri.IsDefaultPort ? ":" + uri.Port : "")}");
                    AdminContext = ClientContext;
                    return;
                }
            }

            // Check if a connection has been made using DeviceLogin, in this case we cannot clone the context to the admin URL and will throw an exception
            IsDeviceLogin(tenantAdminUrl);

            // Set up a temporary context to the SharePoint Online Admin Center URL to allow this cmdlet to execute
            WriteVerbose($"Connecting to the SharePoint Online Admin Center at '{tenantAdminUrl}' to run this cmdlet");
            try
            {
                AdminContext = Connection.CloneContext(tenantAdminUrl);
            }
            catch(WebException e) when (e.Status == WebExceptionStatus.NameResolutionFailure)
            {
                throw new PSInvalidOperationException($"The hostname '{tenantAdminUrl}' which you have passed in your Connect-PnPOnline -TenantAdminUrl is invalid. Please connect again using the proper hostname.", e);
            }
            catch(Exception e)
            {
                throw new PSInvalidOperationException($"Unable to connect to the SharePoint Online Admin Center at '{tenantAdminUrl}' to run this cmdlet. If this URL is incorrect for your tenant, you can pass in the correct Admin Center URL using Connect-PnPOnline -TenantAdminUrl. If you are using Privileged Identity Management (PIM) on your tenant, please ensure you have activated at least the SharePoint Administrator role and allowed some time for it to activate. Error message: {e.Message}", e);
            }
            WriteVerbose($"Connected to the SharePoint Online Admin Center at '{tenantAdminUrl}' to run this cmdlet");
        }
    }
}
