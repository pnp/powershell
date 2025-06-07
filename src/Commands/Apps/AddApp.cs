using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework.ALM;
using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Add, "PnPApp")]
    public class AddApp : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Path;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public AppCatalogScope Scope = AppCatalogScope.Tenant;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter Publish;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter SkipFeatureDeployment;

        [Parameter(Mandatory = false)]
        public SwitchParameter Overwrite;

        [Parameter(Mandatory = false)]
        public int Timeout = 200;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            try
            {
                AddPnPApp();
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

                                AddPnPApp();
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

        private void AddPnPApp()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            var fileInfo = new System.IO.FileInfo(Path);

            var bytes = System.IO.File.ReadAllBytes(Path);

            var manager = new AppManager(ClientContext);

            var result = manager.Add(bytes, fileInfo.Name, Overwrite, Scope, timeoutSeconds: Timeout);

            try
            {

                if (Publish)
                {
                    if (manager.Deploy(result, SkipFeatureDeployment, Scope))
                    {
                        result = manager.GetAvailable(result.Id, Scope);
                    }

                }
                WriteObject(result);
            }
            catch
            {
                // Exception occurred rolling back
                manager.Remove(result, Scope);
                throw;
            }
        }
    }
}