using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPKnowledgeHubSite")]
    public class GetKnowledgeHubSite : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var results = Tenant.GetKnowledgeHubSite();
            Tenant.Context.ExecuteQueryRetry();
            WriteObject(results.Value);
        }
    }
}