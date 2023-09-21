using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsSecurity.Revoke, "PnPTenantServicePrincipalPermission")]
    [RequiredMinimalApiPermissions("Directory.ReadWrite.All")]

    public class RevokeTenantServicePrincipal : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Scope;

        [Parameter(Mandatory = false)]
        public string Resource = "Microsoft Graph";

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {

            var tenantUrl = UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
            using (var tenantContext = ClientContext.Clone(tenantUrl))
            {
                var spoWebAppServicePrincipal = new SPOWebAppServicePrincipal(tenantContext);
                var appId = spoWebAppServicePrincipal.EnsureProperty(a => a.AppId);
                var results = GraphHelper.GetAsync<RestResultCollection<ServicePrincipal>>(Connection, $"/v1.0/servicePrincipals?$filter=appId eq '{appId}'&$select=id", AccessToken).GetAwaiter().GetResult();
                if (results.Items.Any())
                {
                    if (Force || ShouldContinue($"Revoke permission {Scope}?", "Continue"))
                    {
                        var servicePrincipal = results.Items.First();
                        spoWebAppServicePrincipal.GrantManager.Remove(servicePrincipal.Id, Resource, Scope);
                        tenantContext.ExecuteQueryRetry();
                    }
                }
                else
                {
                    throw new PSInvalidOperationException("Cannot find the 'SharePoint Online Client Extensibility Web Application Principal' in your Entra ID Enterprise applications. Did you enable it using `Enable-PnPTenantServicePrincipal'?");
                }
            }
        }

        private class ServicePrincipal
        {
            public string Id { get; set; }
        }
    }
}