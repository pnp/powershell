using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Framework.Utilities;
using System;
using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFile", DefaultParameterSetName = URLASFILEOBJECT)]
    public class GetFile : PnPWebCmdlet
    {
        private const string URLTOPATH = "Save to local path";
        private const string URLASSTRING = "Return as string";
        private const string URLASLISTITEM = "Return as list item";
        private const string URLASFILEOBJECT = "Return as file object";
        private const string URLASMEMORYSTREAM = "Return as memorystream";

        [Parameter(Mandatory = true, ParameterSetName = URLASFILEOBJECT, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = URLASLISTITEM, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = URLTOPATH, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = URLASSTRING, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = URLASMEMORYSTREAM, Position = 0, ValueFromPipeline = true)]
        [Alias("ServerRelativeUrl", "SiteRelativeUrl")]
        public string Url;

        [Parameter(Mandatory = true, ParameterSetName = URLTOPATH)]
        public string Path = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = URLTOPATH)]
        public string Filename = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = URLTOPATH)]
        public SwitchParameter AsFile;

        [Parameter(Mandatory = true, ParameterSetName = URLASLISTITEM)]
        public SwitchParameter AsListItem;

        [Parameter(Mandatory = false, ParameterSetName = URLASLISTITEM)]
        public SwitchParameter ThrowExceptionIfFileNotFound;

        [Parameter(Mandatory = false, ParameterSetName = URLASSTRING)]
        public SwitchParameter AsString;

        [Parameter(Mandatory = false, ParameterSetName = URLTOPATH)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false, ParameterSetName = URLASFILEOBJECT)]
        public SwitchParameter AsFileObject;

        [Parameter(Mandatory = false, ParameterSetName = URLASMEMORYSTREAM)]
        public SwitchParameter AsMemoryStream;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoUrlDecode;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;
            if (string.IsNullOrEmpty(Path))
            {
                Path = SessionState.Path.CurrentFileSystemLocation.Path;
            }
            else
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
            }

            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
            {
                // We can't deal with absolute URLs
                Url = UrlUtility.MakeRelativeUrl(Url);
            }

            // Remove URL decoding from the Url as that will not work. We will encode the + character specifically, because if that is part of the filename, it needs to stay and not be decoded.
            if (!NoUrlDecode)
            {
                Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));
            }

            var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!Url.ToLower().StartsWith(webUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(webUrl, Url);
            }
            else
            {
                serverRelativeUrl = Url;
            }

            switch (ParameterSetName)
            {
                case URLTOPATH:

                    // Get a reference to the file to download
                    IFile fileToDownload = Connection.PnPContext.Web.GetFileByServerRelativeUrl(serverRelativeUrl);
                    string fileToDownloadName = !string.IsNullOrEmpty(Filename) ? Filename : fileToDownload.Name;
                    string fileOut = System.IO.Path.Combine(Path, fileToDownloadName);

                    if (System.IO.File.Exists(fileOut) && !Force)
                    {
                        LogWarning($"File '{fileToDownloadName}' exists already. Use the -Force parameter to overwrite the file.");
                    }
                    else
                    {
                        SaveFileToLocal(fileToDownload, fileOut).GetAwaiter().GetResult();
                    }

                    break;
                case URLASFILEOBJECT:
                    var fileObject = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
                    try
                    {
                        ClientContext.Load(fileObject, f => f.Author, f => f.Length, f => f.ModifiedBy, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                        ClientContext.ExecuteQueryRetry();
                    }
                    catch (ServerException)
                    {
                        // Assume the cause of the exception is that a principal cannot be found and try again without:
                        // Fallback in case the creator or person having last modified the file no longer exists in the environment such that the file can still be downloaded
                        ClientContext.Load(fileObject, f => f.Length, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                        ClientContext.ExecuteQueryRetry();
                    }
                    WriteObject(fileObject);
                    break;
                case URLASLISTITEM:
                    var fileListItem = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));

                    ClientContext.Load(fileListItem, f => f.Exists, f => f.ListItemAllFields);

                    ClientContext.ExecuteQueryRetry();
                    if (fileListItem.Exists)
                    {
                        WriteObject(fileListItem.ListItemAllFields);
                    }
                    else
                    {
                        if (ThrowExceptionIfFileNotFound)
                        {
                            throw new PSArgumentException($"No file found with the provided Url {serverRelativeUrl}", "Url");
                        }
                    }
                    break;
                case URLASSTRING:
                    WriteObject(CurrentWeb.GetFileAsString(serverRelativeUrl));
                    break;
                case URLASMEMORYSTREAM:
                    IFile fileMemoryStream;

                    try
                    {
                        fileMemoryStream = Connection.PnPContext.Web.GetFileByServerRelativeUrl(ResourcePath.FromDecodedUrl(serverRelativeUrl).DecodedUrl, f => f.Author, f => f.Length, f => f.ModifiedBy, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                    }
                    catch (ServerException)
                    {
                        // Assume the cause of the exception is that a principal cannot be found and try again without:
                        // Fallback in case the creator or person having last modified the file no longer exists in the environment such that the file can still be downloaded
                        fileMemoryStream = Connection.PnPContext.Web.GetFileByServerRelativeUrl(ResourcePath.FromDecodedUrl(serverRelativeUrl).DecodedUrl, f => f.Length, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                    }

                    var stream = new System.IO.MemoryStream(fileMemoryStream.GetContentBytes());
                    WriteObject(stream);
                    break;
            }
        }

        private static async Task SaveFileToLocal(IFile fileToDownload, string filePath)
        {
            // Start the download
            using (Stream downloadedContentStream = await fileToDownload.GetContentAsync(true))
            {
                // Download the file bytes in 2MB chunks and immediately write them to a file on disk 
                // This approach avoids the file being fully loaded in the process memory
                var bufferSize = 2 * 1024 * 1024;  // 2 MB buffer

                using (FileStream content = System.IO.File.Create(filePath))
                {
                    byte[] buffer = new byte[bufferSize];
                    int read;
                    while ((read = await downloadedContentStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        content.Write(buffer, 0, read);
                    }
                }
            }
        }
    }
}
