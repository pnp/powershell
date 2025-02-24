using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Reset, "PnPDocumentId", DefaultParameterSetName = ParameterSet_RESETFILE)]
    public class ResetDocumentId : PnPWebCmdlet
    {
        private const string ParameterSet_RESETFILE = "Reset file";
        private const string ParameterSet_RESETLIBRARY = "Reset library";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_RESETFILE)]
        public FilePipeBind File;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_RESETLIBRARY)]
        public ListPipeBind Library;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_RESETLIBRARY)]
        public ContentTypePipeBind ContentType;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.Url);

            string url = null;
            switch (ParameterSetName)
            {
                case ParameterSet_RESETFILE:
                    var file = File.GetFile(ClientContext);
                    file.EnsureProperties(f => f.ServerRelativeUrl);

                    // Even though the URL parameter states server relative path, it requires the site relative path of the file
                    var siteRelativeUrl = file.ServerRelativeUrl.Remove(0, new Uri(CurrentWeb.Url).AbsolutePath.Length);
                    url = $"{CurrentWeb.Url}/_api/SP.DocumentManagement.DocumentId/ResetDocIdByServerRelativePath(decodedUrl='{siteRelativeUrl}')";

                    WriteVerbose($"Making a POST request to {url} to request a new document ID for the file {file.ServerRelativeUrl}");
                    break;
                case ParameterSet_RESETLIBRARY:
                    var library = Library.GetList(CurrentWeb, l => l.ParentWebUrl);

                    if(library.BaseTemplate != (int)ListTemplateType.DocumentLibrary)
                    {
                        throw new PSArgumentException("The specified list is not a document library.", nameof(Library));
                    }

                    var contentType = ContentType.GetContentType(library);

                    url = $"{CurrentWeb.Url}/_api/SP.DocumentManagement.DocumentId/ResetDocIdsInLibrary(decodedUrl='{library.RootFolder.ServerRelativeUrl.Remove(0, library.ParentWebUrl.Length)}',contentTypeId='{contentType.Id}')";
                    WriteVerbose($"Making a POST request to {url} to request new document IDs for the files in library {library.Title} with content type ID {contentType.Id}");
                    break;
            }

            RestHelper.Post(Connection.HttpClient, url, ClientContext);
        }
    }
}
