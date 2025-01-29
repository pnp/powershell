using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteScriptPackage")]
    [OutputType(typeof(TenantSiteScript))]
    public class AddSiteScriptPackage : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = true)]
        public string ContentPath;

        protected override void ExecuteCmdlet()
        {
            if (!Path.IsPathRooted(ContentPath))
            {
                ContentPath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, ContentPath);
            }
            using (var contentStream = System.IO.File.OpenRead(ContentPath))
            {
                TenantSiteScriptCreationInfo siteScriptCreationInfo = new TenantSiteScriptCreationInfo
                {
                    Title = Title,
                    Description = Description,
                    ContentStream = contentStream
                };
                var script = Tenant.CreateSiteScript(siteScriptCreationInfo);
                AdminContext.Load(script);
                AdminContext.ExecuteQueryRetry();
                WriteObject(script);
            }
        }
    }
}