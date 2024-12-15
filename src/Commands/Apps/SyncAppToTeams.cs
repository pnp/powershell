using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsData.Sync, "PnPAppToTeams")]
    public class SyncAppToTeams : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public AppMetadataPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var manager = new PnP.Framework.ALM.AppManager(ClientContext);

            var app = Identity.GetAppMetadata(ClientContext, AppCatalogScope.Tenant);

            if (app != null)
            {
                manager.SyncToTeams(app);
            }
            else
            {
                throw new Exception("Cannot find app");
            }
        }
    }
}