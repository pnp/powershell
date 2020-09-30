using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;


namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteCollectionTermStore")]
    public class GetPnPSiteCollectionTermStore : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            TaxonomySession session = TaxonomySession.GetTaxonomySession(ClientContext);
            var termStore = session.GetDefaultSiteCollectionTermStore();
            ClientContext.Load(termStore, t => t.Id, t => t.Name, t => t.Groups, t => t.KeywordsTermSet);
            ClientContext.ExecuteQueryRetry();
            WriteObject(termStore);
        }

    }
}
