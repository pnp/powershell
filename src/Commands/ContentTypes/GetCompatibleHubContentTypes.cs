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
            var subscriber = new Microsoft.SharePoint.Client.Taxonomy.ContentTypeSync.ContentTypeSubscriber(ClientContext);
            ClientContext.Load(subscriber);
            ClientContext.ExecuteQueryRetry();

            var results = subscriber.GetCompatibleHubContentTypes(WebUrl, ListUrl);
            ClientContext.ExecuteQueryRetry();

            WriteObject(results, true);
        }
    }
}