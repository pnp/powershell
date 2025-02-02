using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework.ALM;
using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Add, "PnPApp")]
    public class AddApp : PnPSharePointOnlineAdminCmdlet
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