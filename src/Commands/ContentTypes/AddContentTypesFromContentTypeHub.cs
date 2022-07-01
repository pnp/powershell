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

        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        protected override void ExecuteCmdlet()
        {
            var site = ParameterSpecified(nameof(Site)) ? Site.Url : ClientContext.Url;

            var sub = new Microsoft.SharePoint.Client.Taxonomy.ContentTypeSync.ContentTypeSubscriber(ClientContext);
            ClientContext.Load(sub);
            ClientContext.ExecuteQueryRetry();

            var res = sub.SyncContentTypesFromHubSite2(site, ContentTypes);
            ClientContext.ExecuteQueryRetry();

            var result = new Model.SharePoint.AddContentTypesFromContentTypeHubResponse
            {
                FailedContentTypeErrors = res.Value.FailedContentTypeErrors,
                FailedReason = res.Value.FailedReason,
                FailedContentTypeIDs = res.Value.FailedContentTypeIDs,
                IsPassed = res.Value.IsPassed,
                Value = res.Value
            };
            WriteObject(result, true);
        }
    }
}