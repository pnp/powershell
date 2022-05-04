using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Get, "PnPContentTypePublishingStatus")]
    public class GetContentTypePublishingStatus : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
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

            var isPublished = pub.IsPublished(ct);
            ClientContext.ExecuteQueryRetry();
            WriteObject(isPublished);
        }
    }
}
