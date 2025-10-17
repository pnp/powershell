using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsData.Publish, "PnPApp")]
    public class PublishApp : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public AppMetadataPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipFeatureDeployment;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public AppCatalogScope Scope = AppCatalogScope.Tenant;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            try
            {
                PublishPnPApp();
            }
            catch (Exception ex)
            {
                if (ApiRequestHelper.IsUnauthorizedAccessException(ex.Message))
                {
                    bool isScriptSettingUpdated = false;
                    var site = ClientContext.Site;
                    if (site.IsNoScriptSite())
                    {
                        try
                        {
                            if (Force || ShouldContinue("This is a no-script site. You need SharePoint admin permissions to allow scripts. Do you want to temporarily enable scripting on it temporarily?", Properties.Resources.Confirm))
                            {
                                var tenantUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
                                using var tenantContext = ClientContext.Clone(tenantUrl);

                                LogDebug("Temporarily enabling scripting on the app catalog site");

                                var tenant = new Tenant(tenantContext);
                                tenant.SetSiteProperties(ClientContext.Url, noScriptSite: false);
                                isScriptSettingUpdated = true;

                                PublishPnPApp();
                            }
                            else
                            {
                                LogWarning("Scripting is disabled on the site. This command cannot proceed without allowing scripts. Please contact your SharePoint admin to allow scripting.");
                                return;
                            }
                        }
                        catch (Exception innerEx)
                        {
                            LogError(innerEx);
                            return;
                        }
                        finally
                        {
                            if (isScriptSettingUpdated)
                            {
                                var tenantUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
                                using var tenantContext = ClientContext.Clone(tenantUrl);
                                LogDebug("Reverting the no-script setting on the app catalog site");
                                var tenant = new Tenant(tenantContext);
                                tenant.SetSiteProperties(ClientContext.Url, noScriptSite: true);
                            }
                        }
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        private void PublishPnPApp()
        {
            var manager = new PnP.Framework.ALM.AppManager(ClientContext);

            var app = Identity.GetAppMetadata(ClientContext, Scope);
            if (app != null)
            {
                try
                {
                    manager.Deploy(app, SkipFeatureDeployment, Scope);
                }
                catch (ServerException ex) when (ex.Message.Contains("CSPConfig") && ex.Message.Contains("100000"))
                {
                    // Handle the specific CSPConfig error when deploying to site collection app catalog
                    // This is a known SharePoint service-side limitation where tenant-level script sources
                    // are incorrectly included in site-level CSP validation
                    var errorMessage = "Failed to publish the app due to a SharePoint limitation. " +
                        "The error 'Value of: [CSPConfig] cannot exceed: [100000]' occurs when there are too many " +
                        "Trusted Script Sources configured at the tenant level, and SharePoint incorrectly includes " +
                        "them when validating site collection app catalog deployments.\n\n" +
                        "Workarounds:\n" +
                        "1. Manually publish the app through the SharePoint UI (navigate to Site Contents > App Catalog).\n" +
                        "2. Reduce the number of Trusted Script Sources at the tenant level.\n" +
                        "3. Contact Microsoft Support to request a fix for this service-side issue.\n\n" +
                        "For more information, see:\n" +
                        "- https://github.com/SharePoint/sp-dev-docs/issues/10412\n" +
                        "- https://github.com/SharePoint/sp-dev-docs/issues/10369";
                    
                    throw new Exception(errorMessage, ex);
                }
            }
            else
            {
                throw new Exception("Cannot find app");
            }
        }
    }
}