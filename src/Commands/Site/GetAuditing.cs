using Microsoft.SharePoint.Client;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPAuditing")]
    [OutputType(typeof(Audit))]
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
