using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.DocumentSet;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;

namespace PnP.PowerShell.Commands.DocumentSets
{
    [Cmdlet(VerbsCommon.Add, "PnPDocumentSet")]
    [OutputType(typeof(string))]
    public class AddDocumentSet : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name;

        [Parameter(Mandatory = false)]
        public string Folder;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ContentTypePipeBind ContentType;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetListOrThrow(nameof(List), CurrentWeb, l => l.RootFolder, l => l.ContentTypes);
            
            var listContentType = ContentType.GetContentType(list);

            if (listContentType.ServerObjectIsNull == null || listContentType.ServerObjectIsNull == true)
            {
                var siteContentType = ContentType.GetContentTypeOrThrow(nameof(ContentType), CurrentWeb);
                listContentType = new ContentTypePipeBind(siteContentType.Name).GetContentTypeOrThrow(nameof(ContentType), list);
            }

            listContentType.EnsureProperty(ct => ct.StringId);

            if (!listContentType.StringId.StartsWith("0x0120D520"))
            {
                throw new PSArgumentException($"Content type '{ContentType}' does not inherit from the base Document Set content type. Document Set content type IDs start with 0x120D520");
            }

            var targetFolder = list.RootFolder;

            if (Folder != null)
            {
                // Create the folder if it doesn't exist
                targetFolder = CurrentWeb.EnsureFolder(list.RootFolder, Folder);
            }

            // Create the document set
            var result = DocumentSet.Create(ClientContext, targetFolder, Name, listContentType.Id);
            ClientContext.ExecuteQueryRetry();

            WriteObject(result.Value);
        }
    }
}