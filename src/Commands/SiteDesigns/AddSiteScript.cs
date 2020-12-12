using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteScript")]
    public class AddSiteScript : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = true)]
        public string Content;

        protected override void ExecuteCmdlet()
        {
            TenantSiteScriptCreationInfo siteScriptCreationInfo = new TenantSiteScriptCreationInfo
            {
                Title = Title,
                Description = Description,
                Content = Content
            };
            var script = Tenant.CreateSiteScript(siteScriptCreationInfo);
            ClientContext.Load(script);
            ClientContext.ExecuteQueryRetry();
            WriteObject(script);
        }
    }
}