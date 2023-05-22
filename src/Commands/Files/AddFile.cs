using System.Collections;
using System.IO;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPFile")]
    public class AddFile : PnPWebCmdlet
    {
        private const string ParameterSet_ASFILE = "Upload file";
        private const string ParameterSet_ASSTREAM = "Upload file from stream";
        private const string ParameterSet_ASTEXT = "Upload file from text";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASFILE)]
        [ValidateNotNullOrEmpty]
        public string Path = string.Empty;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASSTREAM)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASTEXT)]
        [ValidateNotNullOrEmpty]
        public string FileName = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASFILE)]
        [ValidateNotNullOrEmpty]
        public string NewFileName = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASSTREAM, ValueFromPipeline = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public Stream Stream;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASTEXT)]
        public string Content;

        [Parameter(Mandatory = false)]
        public SwitchParameter Checkout;

        [Parameter(Mandatory = false)]
        public string CheckInComment = string.Empty;

        [Parameter(Mandatory = false)]
        public CheckinType CheckinType = CheckinType.MinorCheckIn;

        [Parameter(Mandatory = false)]
        public SwitchParameter Approve;

        [Parameter(Mandatory = false)]
        public string ApproveComment = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter Publish;

        [Parameter(Mandatory = false)]
        public string PublishComment = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter UseWebDav;

        [Parameter(Mandatory = false)]
        public Hashtable Values;

        [Parameter(Mandatory = false)]
        public ContentTypePipeBind ContentType;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_ASFILE)
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                if (string.IsNullOrEmpty(NewFileName))
                {
                    FileName = System.IO.Path.GetFileName(Path);
                }
                else
                {
                    FileName = NewFileName;
                }
            }

            var folder = EnsureFolder();
            var fileUrl = UrlUtility.Combine(folder.ServerRelativeUrl, FileName);

            string targetContentTypeId = null;
            // Check to see if the Content Type exists. If it doesn't we are going to throw an exception and block this transaction right here.
            if (ContentType != null)
            {
                CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                var list = CurrentWeb.GetListByUrl(folder.ServerRelativeUrl.Substring(CurrentWeb.ServerRelativeUrl.TrimEnd('/').Length + 1));
                if (list is null)
                {
                    throw new PSArgumentException("The folder specified does not have a corresponding list", nameof(Folder));
                }
                targetContentTypeId = ContentType?.GetIdOrThrow(nameof(ContentType), list);
            }

            // Check if the file exists
            if (Checkout)
            {
                try
                {
                    var existingFile = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(fileUrl));

                    existingFile.EnsureProperty(f => f.Exists);
                    if (existingFile.Exists)
                    {
                        CurrentWeb.CheckOutFile(fileUrl);
                    }
                }
                catch
                { // Swallow exception, file does not exist 
                }
            }

            Microsoft.SharePoint.Client.File file;
            switch (ParameterSetName)
            {
                case ParameterSet_ASFILE:
                    file = folder.UploadFile(FileName, Path, true);
                    break;

                case ParameterSet_ASTEXT:
                    using (var stream = new MemoryStream())
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.Write(Content);
                            writer.Flush();
                            stream.Position = 0;
                            file = folder.UploadFile(FileName, stream, true);
                        }
                    }
                    break;

                default:
                    file = folder.UploadFile(FileName, Stream, true);
                    break;
            }

            bool updateRequired = false;
            var item = file.ListItemAllFields;
            if (Values != null)
            {
                ListItemHelper.SetFieldValues(item, Values, this);
                updateRequired = true;
            }

            if (ContentType != null)
            {
                item["ContentTypeId"] = targetContentTypeId;
                updateRequired = true;
            }

            if (updateRequired)
            {
                item.UpdateOverwriteVersion();
            }

            if (Checkout)
            {
                CurrentWeb.CheckInFile(fileUrl, CheckinType, CheckInComment);
            }

            if (Publish)
            {
                CurrentWeb.PublishFile(fileUrl, PublishComment);
            }

            if (Approve)
            {
                CurrentWeb.ApproveFile(fileUrl, ApproveComment);
            }

            ClientContext.Load(file);
            try
            {
                ClientContext.ExecuteQueryRetry();
            }
            catch (ServerException)
            {
                // Can happen when uploading a file to a location not residing inside a document library, such as to the _cts folder. Switch to fallback option so that the upload doesn't result in an error.
                ClientContext.Load(file, f => f.Length, f => f.Name, f => f.TimeCreated, f => f.TimeLastModified, f => f.Title);
                ClientContext.ExecuteQueryRetry();
            }
            WriteObject(file);
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
