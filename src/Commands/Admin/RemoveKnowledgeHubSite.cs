using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPKnowledgeHubSite")]
    public class RemoveKnowledgeHubSite : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            Tenant.RemoveKnowledgeHubSite();
            Tenant.Context.ExecuteQueryRetry();
        }
    }
}