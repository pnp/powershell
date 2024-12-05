using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsLifecycle.Uninstall, "PnPApp")]
    public class UninstallApp : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public AppMetadataPipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public AppCatalogScope Scope = AppCatalogScope.Tenant;
        
        protected override void ExecuteCmdlet()
        {
            var manager = new PnP.Framework.ALM.AppManager(ClientContext);

            var app = Identity.GetAppMetadata(ClientContext, Scope);
            if (app != null)
            {
                manager.Uninstall(app, Scope);
            }
            else
            {
                throw new Exception("Cannot find app");
            }
        }
    }
}