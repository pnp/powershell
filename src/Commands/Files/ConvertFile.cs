using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsData.Convert, "PnPFile")]
    public class ConvertFile : PnPWebCmdlet
    {
        private const string URLTOPATH = "Save to local path";
        private const string URLASMEMORYSTREAM = "Return as memorystream";
        private const string UPLOADTOSHAREPOINT = "Upload to SharePoint";

        [Parameter(Mandatory = true, ParameterSetName = URLTOPATH, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = URLASMEMORYSTREAM, Position = 0, ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = UPLOADTOSHAREPOINT, Position = 0, ValueFromPipeline = true)]
        [Alias("ServerRelativeUrl", "SiteRelativeUrl")]
        public string Url;

        [Parameter(Mandatory = false, ParameterSetName = URLTOPATH)]
        [Parameter(Mandatory = false, ParameterSetName = URLASMEMORYSTREAM)]
        [Parameter(Mandatory = false, ParameterSetName = UPLOADTOSHAREPOINT)]
        public ConvertToFormat ConvertToFormat = ConvertToFormat.Pdf;

        [Parameter(Mandatory = true, ParameterSetName = URLTOPATH)]
        public string Path = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = URLTOPATH)]
        [Parameter(Mandatory = false, ParameterSetName = UPLOADTOSHAREPOINT)]
        public SwitchParameter Force;

        [Parameter(Mandatory = true, ParameterSetName = UPLOADTOSHAREPOINT)]
        [ValidateNotNullOrEmpty]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = false, ParameterSetName = URLTOPATH)]
        [Parameter(Mandatory = false, ParameterSetName = UPLOADTOSHAREPOINT)]
        public string NewFileName = string.Empty;

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

            // Remove URL decoding from the Url as that will not work. We will encode the + character spdecifically, because if that is part of the filename, it needs to stay and not be decoded.
            Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));

            var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
            var serverRelativeUrl = string.Empty;
            if (!Url.ToLower().StartsWith(webUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(webUrl, Url);
            }
            else
            {
                serverRelativeUrl = Url;
            }

            IFile sourceFile = Connection.PnPContext.Web.GetFileByServerRelativeUrl(serverRelativeUrl, p => p.VroomDriveID, p => p.VroomItemID);

            LogDebug("Converting file to the specified format");
            var convertedFile = sourceFile.ConvertTo(new ConvertToOptions { Format = ConvertToFormat });
            var newFileExtension = "." + ConvertToFormat.ToString();

            if (string.IsNullOrEmpty(NewFileName))
            {
                // Use original filename with new extension
                var fileName = System.IO.Path.GetFileNameWithoutExtension(sourceFile.Name);
                NewFileName = fileName + newFileExtension;
            }
            else 
            {
                var extensionMatch = System.IO.Path.GetExtension(NewFileName).Equals(newFileExtension, System.StringComparison.OrdinalIgnoreCase);
                if (!extensionMatch)
                {
                    LogWarning($"File extension of NewFileName '{NewFileName}' doesn't match ConvertToFormat '{newFileExtension}'.");
                }
            }
            switch (ParameterSetName)
            {
                case URLTOPATH:

                    var fileOut = System.IO.Path.Combine(Path, NewFileName);
                    if (System.IO.File.Exists(fileOut) && !Force)
                    {
                        LogWarning($"File '{sourceFile.Name}' exists already. Use the -Force parameter to overwrite the file.");
                    }
                    else
                    {
                        LogDebug("Saving file to the disc.");
                        using FileStream fs = new(fileOut, FileMode.Create);
                        convertedFile.CopyTo(fs);
                    }

                    break;

                case URLASMEMORYSTREAM:

                    var stream = new MemoryStream();
                    convertedFile.CopyTo(stream);
                    WriteObject(stream);
                    break;

                case UPLOADTOSHAREPOINT:

                    LogDebug("Uploading file to the specified folder");
                    var folder = EnsureFolder();
                    var uploadedFile = folder.UploadFile(NewFileName, convertedFile, Force);

                    try
                    {
                        ClientContext.Load(uploadedFile, f => f.Author, f => f.Length, f => f.ModifiedBy, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                        ClientContext.ExecuteQueryRetry();
                    }
                    catch (ServerException)
                    {
                        // Assume the cause of the exception is that a principal cannot be found and try again without:
                        // Fallback in case the creator or person having last modified the file no longer exists in the environment such that the file can still be downloaded
                        ClientContext.Load(uploadedFile, f => f.Length, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                        ClientContext.ExecuteQueryRetry();
                    }

                    WriteObject(uploadedFile);
                    LogDebug("File uploaded.");
                    break;

            }
        }

        /// <summary>
        /// Ensures the folder to which the file is to be uploaded exists. Changed from using the EnsureFolder implementation in PnP Framework as that requires at least member rights to the entire site to work.
        /// </summary>
        /// <returns>The folder to which the file needs to be uploaded</returns>
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
