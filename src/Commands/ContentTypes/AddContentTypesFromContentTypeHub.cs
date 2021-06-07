using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "PnPContentTypesFromContentTypeHub")]
    public class AddContentTypesFromContentTypeHub : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public List<string> ContentTypes;

        protected override void ExecuteCmdlet()
        {
            Microsoft.SharePoint.Client.Site site = ClientContext.Site;
            ClientContext.Load(site);
            ClientContext.ExecuteQueryRetry();

            var sub = new Microsoft.SharePoint.Client.Taxonomy.ContentTypeSync.ContentTypeSubscriber(ClientContext);
            ClientContext.Load(sub);
            ClientContext.ExecuteQueryRetry();

            var res = sub.SyncContentTypesFromHubSite2(site.Url, ContentTypes);
            ClientContext.ExecuteQueryRetry();
            
            WriteObject(res);
        }

    }
}
