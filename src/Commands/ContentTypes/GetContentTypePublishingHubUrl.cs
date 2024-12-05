using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Get, "PnPContentTypePublishingHubUrl")]
    public class GetContentTypePublishingHub : PnPSharePointCmdlet
    {
   
        protected override void ExecuteCmdlet()
        {
            TaxonomySession session = TaxonomySession.GetTaxonomySession(ClientContext);
            var termStore = session.GetDefaultSiteCollectionTermStore();
            ClientContext.Load(termStore, t => t.ContentTypePublishingHub);
            ClientContext.ExecuteQueryRetry();
            WriteObject(termStore.ContentTypePublishingHub);
        }

    }
}
