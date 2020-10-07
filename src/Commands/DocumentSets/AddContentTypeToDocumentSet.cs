using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.DocumentSets
{
    [Cmdlet(VerbsCommon.Add,"ContentTypeToDocumentSet")]
    public class AddContentTypeToDocumentSet : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public ContentTypePipeBind[] ContentType;

        [Parameter(Mandatory = true)]
        public DocumentSetPipeBind DocumentSet;

        protected override void ExecuteCmdlet()
        {
            var docSetTemplate = DocumentSet.GetDocumentSetTemplate(SelectedWeb);

            foreach (var ct in ContentType)
            {
                var contentType = ct.GetContentType(SelectedWeb);

                docSetTemplate.AllowedContentTypes.Add(contentType.Id);
            }
            docSetTemplate.Update(true);
            ClientContext.ExecuteQueryRetry();
        }
    }
}
