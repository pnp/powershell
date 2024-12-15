using PnP.Framework.ALM;
using PnP.Framework.Enums;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsData.Update, "PnPApp")]
  
    public class UpdateApp : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public AppMetadataPipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public AppCatalogScope Scope = AppCatalogScope.Tenant;

        protected override void ExecuteCmdlet()
        {
            var manager = new AppManager(ClientContext);
            var app = Identity.GetAppMetadata(ClientContext, Scope);
            if (app != null)
            {
                manager.Upgrade(Identity.Id, Scope);
            }
            else
            {
                throw new Exception("Cannot find app");
            }
        }
    }
}