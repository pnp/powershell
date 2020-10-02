using System.Management.Automation;

using PnP.PowerShell.Commands.Base.PipeBinds;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.DocumentSets
{
    [Cmdlet(VerbsCommon.Remove, "ContentTypeFromDocumentSet")]
    public class RemoveContentTypeFromDocumentSet : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = true)]
        public DocumentSetPipeBind DocumentSet;
        protected override void ExecuteCmdlet()
        {
            var ct = ContentType.GetContentType(SelectedWeb);
            var docSetTemplate = DocumentSet.GetDocumentSetTemplate(SelectedWeb);

            docSetTemplate.AllowedContentTypes.Remove(ct.Id);

            docSetTemplate.Update(true);

            ClientContext.ExecuteQueryRetry();
        }
    }
}
