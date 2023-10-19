using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System.IO;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsData.Convert, "PnPFileToPDF")]
    public class ConvertFileToPDF : PnPWebCmdlet
    {
        private const string URLTOPATH = "Save to local path";
        private const string URLASMEMORYSTREAM = "Return as memorystream";
        private const string UPLOADTOSHAREPOINT = "Upload to SharePoint";

        [Parameter(Mandatory = true, ParameterSetName = URLTOPATH, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = URLASMEMORYSTREAM, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = UPLOADTOSHAREPOINT, Position = 0, ValueFromPipeline = true)]
        [Alias("ServerRelativeUrl", "SiteRelativeUrl")]
        public string Url;

        [Parameter(Mandatory = true, ParameterSetName = URLTOPATH)]
        public string Path = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = URLTOPATH)]
        public SwitchParameter Force;

        [Parameter(Mandatory = true, ParameterSetName = UPLOADTOSHAREPOINT)]
        [ValidateNotNullOrEmpty]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = false, ParameterSetName = URLASMEMORYSTREAM)]
        public SwitchParameter AsMemoryStream;

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrEmpty(Path))
            {
                Path = SessionState.Path.CurrentFileSystemLocation.Path;
            }
            else if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));
            var serverRelativeUrl = GetServerRelativeUrl(Url);
            var fileObj = GetFileByServerRelativePath(serverRelativeUrl);
            var sourceFileName = fileObj.Name.ToString().Substring(0, fileObj.Name.ToString().LastIndexOf("."));
            var apiUrl = GeneratePdfApiUrl(Url, sourceFileName, fileObj);
            byte[] response = RestHelper.GetByteArrayAsync(Connection.HttpClient, apiUrl, GraphAccessToken).GetAwaiter().GetResult();
            var fileToDownloadName = !string.IsNullOrEmpty(sourceFileName) ? sourceFileName : "Download";

            switch (ParameterSetName)
            {
                case URLTOPATH:
                    var fileOut = System.IO.Path.Combine(Path, $"{fileToDownloadName}.pdf");
                    if (!Directory.Exists(Path))
                    {
                        throw new PSArgumentException("Path does not exists");
                    }
                    if (!Force && System.IO.File.Exists(fileOut))
                    {
                        WriteWarning($"File '{fileToDownloadName}' exists already. Use the -Force parameter to overwrite the file.");
                    }
                    else
                    {
                        System.IO.File.WriteAllBytes(fileOut, response);
                        WriteObject($"File saved as {fileOut}");
                    }
                    break;

                case URLASMEMORYSTREAM:
                    var stream = new MemoryStream(response);
                    WriteObject(stream);
                    break;

                case UPLOADTOSHAREPOINT:
                    var targetLibrary = EnsureFolder();
                    Stream fileStream = new MemoryStream(response);
                    var targetFileName = $"{fileToDownloadName}.pdf";
                    var uploadedFile = targetLibrary.UploadFileAsync(targetFileName, fileStream, true).GetAwaiter().GetResult();
                    ClientContext.Load(uploadedFile);
                    try
                    {
                        ClientContext.ExecuteQueryRetry();
                    }
                    catch (ServerException)
                    {
                        ClientContext.Load(uploadedFile, f => f.Length, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                        ClientContext.ExecuteQueryRetry();
                    }
                    WriteObject(uploadedFile);
                    break;
            }
        }

        private string GetServerRelativeUrl(string url)
        {
            var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
            return url.ToLower().StartsWith(webUrl.ToLower()) ? url : UrlUtility.Combine(webUrl, Url);
        }

        private Microsoft.SharePoint.Client.File GetFileByServerRelativePath(string serverRelativeUrl)
        {
            var fileListItem = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
            ClientContext.Load(fileListItem, f => f.Exists, f => f.ListItemAllFields, f => f.ListId, f => f.Name);
            ClientContext.ExecuteQueryRetry();
            if (fileListItem.Exists)
            {
                return fileListItem;
            }
            else
            {
                throw new PSArgumentException($"No file found with the provided Url {serverRelativeUrl}", "Url");
            }
        }

        private string GeneratePdfApiUrl(string url, string sourceFileName, Microsoft.SharePoint.Client.File fileObj)
        {
            var siteId = PnPContext.Site.Id.ToString();
            var listId = fileObj.ListId.ToString();
            var itemId = fileObj.ListItemAllFields.Id.ToString();
            return $"https://{Connection.GraphEndPoint}/v1.0/sites/{siteId}/lists/{listId}/items/{itemId}/driveItem/content?format=pdf";
        }


        private Folder EnsureFolder()
        {
            // First try to get the folder if it exists already. This avoids an Access Denied exception if the current user doesn't have Full Control access at Web level
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            Folder library = null;
            try
            {
                library = Folder.GetFolder(CurrentWeb);
                library.EnsureProperties(f => f.ServerRelativeUrl);
                return library;
            }
            // Exception will be thrown if the library does not exist yet on SharePoint
            catch (ServerException serverEx) when (serverEx.ServerErrorCode == -2147024894)
            {
                // create the library
                CurrentWeb.CreateList(ListTemplateType.DocumentLibrary, Folder.ServerRelativeUrl, false, true, "", false, false);
                library = Folder.GetFolder(CurrentWeb);
                library.EnsureProperties(f => f.ServerRelativeUrl);
                return library;
            }
        }
    }
}
