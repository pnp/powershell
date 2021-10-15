using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;
using PnP.Framework.Utilities;

using System;
using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;
using File = Microsoft.SharePoint.Client.File;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFile", DefaultParameterSetName = "Return as file object")]
    public class GetFile : PnPWebCmdlet
    {
        private const string URLTOPATH = "Save to local path";
        private const string URLASSTRING = "Return as string";
        private const string URLASLISTITEM = "Return as list item";
        private const string URLASFILEOBJECT = "Return as file object";

        [Parameter(Mandatory = true, ParameterSetName = URLASFILEOBJECT, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = URLASLISTITEM, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = URLTOPATH, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = URLASSTRING, Position = 0, ValueFromPipeline = true)]
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

            var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!Url.ToLower().StartsWith(webUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(webUrl, Url);
            }
            else
            {
                serverRelativeUrl = Url;
            }

            File file;

            switch (ParameterSetName)
            {
                case URLTOPATH:

                    SaveFileToLocal(CurrentWeb, serverRelativeUrl, Path, Filename, (fileToSave) =>
                    {
                        if (!Force)
                        {
                            WriteWarning($"File '{fileToSave}' exists already. use the -Force parameter to overwrite the file.");
                        }
                        return Force;
                    }).Wait();
                    break;
                case URLASFILEOBJECT:
                    file = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
                    try
                    {
                        ClientContext.Load(file, f => f.Author, f => f.Length, f => f.ModifiedBy, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                        ClientContext.ExecuteQueryRetry();
                    }
                    catch (ServerException)
                    {
                        // Assume the cause of the exception is that a principal cannot be found and try again without:
                        // Fallback in case the creator or person having last modified the file no longer exists in the environment such that the file can still be downloaded
                        ClientContext.Load(file, f => f.Length, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                        ClientContext.ExecuteQueryRetry();
                    }
                    WriteObject(file);
                    break;
                case URLASLISTITEM:
                    file = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));

                    ClientContext.Load(file, f => f.Exists, f => f.ListItemAllFields);

                    ClientContext.ExecuteQueryRetry();
                    if (file.Exists)
                    {
                        WriteObject(file.ListItemAllFields);
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
            }
        }

        private async Task SaveFileToLocal(Web web, string serverRelativeUrl, string localPath, string localFileName = null, Func<string, bool> fileExistsCallBack = null)
        {
            // Get a reference to the file to download
            IFile fileToDownload = await PnPContext.Web.GetFileByServerRelativeUrlAsync(serverRelativeUrl);

            // Start the download
            Stream downloadedContentStream = await fileToDownload.GetContentAsync(true);

            // Download the file bytes in 2MB chunks and immediately write them to a file on disk 
            // This approach avoids the file being fully loaded in the process memory
            var bufferSize = 2 * 1024 * 1024;  // 2 MB buffer

            var fileOut = System.IO.Path.Combine(localPath, !string.IsNullOrEmpty(localFileName) ? localFileName : fileToDownload.Name);

            if (!System.IO.File.Exists(fileOut) || (fileExistsCallBack != null && fileExistsCallBack(fileOut)))
            {
                using (var content = System.IO.File.Create(fileOut))
                {
                    var buffer = new byte[bufferSize];
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
