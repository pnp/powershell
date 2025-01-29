using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsData.Unpublish, "PnPApp")]
    public class UnpublishApp : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public AppMetadataPipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public AppCatalogScope Scope = AppCatalogScope.Tenant;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            bool isScriptSettingUpdated = false;

            if (Scope == AppCatalogScope.Tenant)
            {
                var appcatalogUri = ClientContext.Web.GetAppCatalog();
                var ctx = ClientContext.Clone(appcatalogUri);
                WriteVerbose("Checking if the tenant app catalog is a no-script site");
                if (ctx.Site.IsNoScriptSite())
                {
                    if (Force || ShouldContinue("The tenant appcatalog is a no-script site. Do you want to temporarily enable scripting on it?", Properties.Resources.Confirm))
                    {
                        WriteVerbose("Temporarily enabling scripting on the tenant app catalog site");
                        var tenant = new Tenant(AdminContext);
                        tenant.SetSiteProperties(appcatalogUri.AbsoluteUri, noScriptSite: false);
                        isScriptSettingUpdated = true;
                    }
                    else
                    {
                        WriteWarning("Scripting is disabled on the tenant app catalog site. This command cannot proceed without allowing scripts.");
                        return;
                    }
                }
            }

            try
            {
                var manager = new PnP.Framework.ALM.AppManager(ClientContext);

                var app = Identity.GetAppMetadata(ClientContext, Scope);
                if (app != null)
                {
                    manager.Retract(app, Scope);
                }
                else
                {
                    throw new Exception("Cannot find app");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (isScriptSettingUpdated)
                {
                    WriteVerbose("Disabling scripting on the tenant app catalog site");
                    var appcatalogUri = ClientContext.Web.GetAppCatalog();
                    var ctx = ClientContext.Clone(appcatalogUri);

                    var tenant = new Tenant(AdminContext);
                    tenant.SetSiteProperties(appcatalogUri.AbsoluteUri, noScriptSite: true);
                }
            }
        }
    }
}