using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Get, "PnPCompatibleHubContentTypes")]
    public class GetCompatibleHubContentTypes : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string WebUrl;

        [Parameter(Mandatory = false)]
        public string ListUrl;

        protected override void ExecuteCmdlet()
        {
            var sub = new Microsoft.SharePoint.Client.Taxonomy.ContentTypeSync.ContentTypeSubscriber(ClientContext);
            ClientContext.Load(sub);
            ClientContext.ExecuteQueryRetry();

            var res = sub.GetCompatibleHubContentTypes(WebUrl, ListUrl);
            ClientContext.ExecuteQueryRetry();

            WriteObject(res);
        }
    }
}
