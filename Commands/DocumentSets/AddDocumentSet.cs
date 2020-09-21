using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.DocumentSet;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;

namespace PnP.PowerShell.Commands.DocumentSets
{
    [Cmdlet(VerbsCommon.Add, "PnPDocumentSet")]
    public class AddDocumentSet : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = true)]
        public ContentTypePipeBind ContentType;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(SelectedWeb);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");
            list.EnsureProperties(l => l.RootFolder, l => l.ContentTypes);

            // Try getting the content type from the Web
            var contentType = ContentType.GetContentType(SelectedWeb);

            // If content type could not be found but a content type ID has been passed in, try looking for the content type ID on the list instead
            if (contentType == null && !string.IsNullOrEmpty(ContentType.Id))
            {
                contentType = list.ContentTypes.FirstOrDefault(ct => ct.StringId.Equals(ContentType.Id));
            }
            else
            {
                // Content type has been found on the web, check if it also exists on the list
                if (list.ContentTypes.All(ct => !ct.StringId.Equals(contentType.Id.StringValue, System.StringComparison.InvariantCultureIgnoreCase)))
                {
                    contentType = list.ContentTypes.FirstOrDefault(ct => ct.Name.Equals(ContentType.Name));
                }
            }

            if (contentType == null)
            {
                throw new PSArgumentException("The provided contenttype has not been found", "ContentType");
            }

            // Create the document set
            var result = DocumentSet.Create(ClientContext, list.RootFolder, Name, contentType.Id);
            ClientContext.ExecuteQueryRetry();

            WriteObject(result.Value);
        }
    }
}