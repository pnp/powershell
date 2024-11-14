using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.DocumentSet;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.DocumentSets
{
    [Cmdlet(VerbsCommon.Add, "PnPDocumentSet")]
    [OutputType(typeof(string))]
    public class AddDocumentSet : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name;

        [Parameter(Mandatory = false)]
        public FolderPipeBind Folder;

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
                targetFolder = EnsureFolder();
            }

            // Create the document set
            var result = DocumentSet.Create(ClientContext, targetFolder, Name, listContentType.Id);
            ClientContext.ExecuteQueryRetry();

            WriteObject(result.Value);
        }

        /// <summary>
        /// Ensures the folder to which the document set is to be created exists. Changed from using the EnsureFolder implementation in PnP Framework as that requires at least member rights to the entire site to work.
        /// </summary>
        /// <returns>The folder to which the document set needs to be created</returns>
        private Folder EnsureFolder()
        {
            // First try to get the folder if it exists already. This avoids an Access Denied exception if the current user doesn't have Full Control access at Web level
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            Folder folder = null;
            try
            {
                folder = Folder.GetFolder(CurrentWeb);
                folder.EnsureProperties(f => f.ServerRelativeUrl);
                return folder;
            }
            // Exception will be thrown if the folder does not exist yet on SharePoint
            catch (ServerException serverEx) when (serverEx.ServerErrorCode == -2147024894)
            {
                // Try to create the folder
                folder = CurrentWeb.EnsureFolder(CurrentWeb.RootFolder, Folder.ServerRelativeUrl);
                folder.EnsureProperties(f => f.ServerRelativeUrl);
                return folder;
            }
        }
    }
}