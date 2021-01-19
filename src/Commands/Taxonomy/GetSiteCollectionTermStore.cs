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
            TaxonomySession session = ClientContext.Site.GetTaxonomySession();
            var termStore = session.GetDefaultSiteCollectionTermStore();
            var termGroup = termStore.GetSiteCollectionGroup(ClientContext.Site, false);
            ClientContext.Load(termGroup, g => g.Id, g => g.Name);
            ClientContext.ExecuteQueryRetry();

            if(!termGroup.ServerObjectIsNull.GetValueOrDefault(true))
            {
                WriteObject(termGroup);
            }
        }
    }
}
