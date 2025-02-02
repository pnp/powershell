using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPKnowledgeHubSite")]
    public class SetKnowledgeHubSite : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string KnowledgeHubSiteUrl;

        protected override void ExecuteCmdlet()
        {
            Tenant.SetKnowledgeHubSite(KnowledgeHubSiteUrl);
            Tenant.Context.ExecuteQueryRetry();
        }
    }
}