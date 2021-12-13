using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsData.Publish, "PnPContentType")]
    public class PublishContentType : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ContentType ContentType;

        protected override void ExecuteCmdlet()
        {
            Microsoft.SharePoint.Client.Site site = ClientContext.Site;
            ClientContext.Load(site);
            ClientContext.ExecuteQuery();
            var pub = new Microsoft.SharePoint.Client.Taxonomy.ContentTypeSync.ContentTypePublisher(ClientContext, site);
            ClientContext.Load(pub);
            ClientContext.ExecuteQuery();
            var republish = pub.IsPublished(ContentType);
            pub.Publish(ContentType, republish);
            ClientContext.ExecuteQuery();
        }
    }
}
