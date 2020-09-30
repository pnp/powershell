using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPAuditing")]
    public class GetAuditing : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var audit = ClientContext.Site.Audit;
            ClientContext.Load(audit);
            ClientContext.ExecuteQueryRetry();
            WriteObject(audit);
        }
    }
}
