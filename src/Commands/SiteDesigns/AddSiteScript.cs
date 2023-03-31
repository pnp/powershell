using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteScript")]
    [OutputType(typeof(TenantSiteScript))]
    public class AddSiteScript : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
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
            AdminContext.Load(script);
            AdminContext.ExecuteQueryRetry();
            WriteObject(script);
        }
    }
}