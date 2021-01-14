using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.New, "PnPSiteCollectionTermStore")]
    public class NewPnPSiteCollectionTermStore : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            TaxonomySession session = ClientContext.Site.GetTaxonomySession();
            var termStore = session.GetDefaultSiteCollectionTermStore();
            var termGroup = termStore.GetSiteCollectionGroup(ClientContext.Site, true);
            ClientContext.Load(termGroup, g => g.Id, g => g.Name);
            ClientContext.ExecuteQueryRetry();
            WriteObject(termGroup);
        }
    }
}
