using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsData.Unpublish, "PnPContentType")]
    public class UnpublishContentType : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ContentTypeCompleter))]
        public ContentTypePipeBind ContentType;
        protected override void ExecuteCmdlet()
        {
            Microsoft.SharePoint.Client.Site site = ClientContext.Site;
            var pub = new Microsoft.SharePoint.Client.Taxonomy.ContentTypeSync.ContentTypePublisher(ClientContext, site);
            ClientContext.Load(pub);
            ClientContext.ExecuteQueryRetry();
            var ct = ContentType.GetContentTypeOrError(this, nameof(ContentType), site.RootWeb);

            if (ct == null)
            {
                WriteError(new ErrorRecord(new Exception($"Invalid content type id."), "INVALIDCTID", ErrorCategory.InvalidArgument, ContentType));
                return;
            }

            pub.Unpublish(ct);
            ClientContext.ExecuteQueryRetry();
        }
    }
}
