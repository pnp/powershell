using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "PnPContentTypesFromContentTypeHub")]
    public class SyncContentTypesFromContentTypeHub : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public List<string> ContentTypes;

        protected override void ExecuteCmdlet()
        {
            Microsoft.SharePoint.Client.Site site = ClientContext.Site;
            var sub = new Microsoft.SharePoint.Taxonomy.ContentTypeSync.Internal.ContentTypeSubscriber(ClientContext);
            ClientContext.Load(sub);
            ClientContext.ExecuteQuery();
             var ct = ClientContext.Web.ContentTypes.GetById("0x010109005889DCF4A2B7C342A128AA1330D38D7F");
            ClientContext.Load(ct);
            ClientContext.ExecuteQuery();
            var list = new List<Microsoft.SharePoint.Client.ContentTypeId>();
            list.Add(ct.Id);
            var res = sub.SyncContentTypesFromHubSite(site.Url, list);
            ClientContext.ExecuteQuery();
        }

    }
}
