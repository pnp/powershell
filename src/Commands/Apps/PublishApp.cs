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
                manager.Deploy(app, SkipFeatureDeployment, Scope);
            }
            else
            {
                throw new Exception("Cannot find app");
            }
        }
    }
}