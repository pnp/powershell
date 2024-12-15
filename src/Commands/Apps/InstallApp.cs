using PnP.Framework.ALM;
using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;
using System.Threading;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsLifecycle.Install, "PnPApp")]
    public class InstallApp : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public AppMetadataPipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public AppCatalogScope Scope = AppCatalogScope.Tenant;

        [Parameter(Mandatory = false)]
        public SwitchParameter Wait;

        protected override void ExecuteCmdlet()
        {
            var manager = new AppManager(ClientContext);

            var app = Identity.GetAppMetadata(ClientContext, Scope);
            if (app != null)
            {
                manager.Install(app, Scope);
                if(Wait.IsPresent)
                {
                    var installableApp = manager.GetAvailable(app.Id, Scope);
                    while (installableApp.InstalledVersion == null)
                    {
                        Thread.Sleep(1000); // wait a second
                        installableApp = manager.GetAvailable(app.Id, Scope);
                    }
                }
            }
            else
            {
                throw new Exception("Cannot find app");
            }
        }
    }
}