using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Get, "PnPTaxonomySession")]
    public class GetTaxonomySession : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var session = ClientContext.Site.GetTaxonomySession();
            ClientContext.Load(session.TermStores);
            ClientContext.ExecuteQueryRetry();
            WriteObject(session);
        }

    }
}
