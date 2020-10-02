using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;


namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Get, "TaxonomySession")]
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
