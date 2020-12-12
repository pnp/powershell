using System.Collections;
using System.IO;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPFile")]
    public class AddFile : PnPWebCmdlet
    {
        private const string ParameterSet_ASFILE = "Upload file";
        private const string ParameterSet_ASSTREAM = "Upload file from stream";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASFILE)]
        public string Path = string.Empty;

        [Parameter(Mandatory = true)]
        public string Folder = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASSTREAM)]
        public string FileName = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASFILE)]
        public string NewFileName = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASSTREAM)]
        public Stream Stream;

        [Parameter(Mandatory = false)]
        public SwitchParameter Checkout;

        [Parameter(Mandatory = false)]
        public string CheckInComment = string.Empty;

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

            ContentType targetContentType = null;
            // Check to see if the Content Type exists. If it doesn't we are going to throw an exception and block this transaction right here.
            if (ContentType != null)
            {
                try
                {
                    var list = SelectedWeb.GetListByUrl(Folder);

                    if (!string.IsNullOrEmpty(ContentType.Id))
                    {
                        targetContentType = list.GetContentTypeById(ContentType.Id);
                    }
                    else if (!string.IsNullOrEmpty(ContentType.Name))
                    {
                        targetContentType = list.GetContentTypeByName(ContentType.Name);
                    }
                    else if (ContentType.ContentType != null)
                    {
                        targetContentType = ContentType.ContentType;
                    }
                    if (targetContentType == null)
                    {
                        ThrowTerminatingError(new ErrorRecord(new ArgumentException($"Content Type Argument: {ContentType} does not exist in the list: {list.Title}"), "CONTENTTYPEDOESNOTEXIST", ErrorCategory.InvalidArgument, this));
                    }
                }
                catch
                {
                    ThrowTerminatingError(new ErrorRecord(new ArgumentException($"The Folder specified ({folder.ServerRelativeUrl}) does not have a corresponding List, the -ContentType parameter is not valid."), "RELATIVEPATHNOTINLIBRARY", ErrorCategory.InvalidArgument, this));
                }
            }

            // Check if the file exists
            if (Checkout)
            {
                try
                {
                    var existingFile = SelectedWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(fileUrl));

                    existingFile.EnsureProperty(f => f.Exists);
                    if (existingFile.Exists)
                    {
                        SelectedWeb.CheckOutFile(fileUrl);
                    }
                }
                catch
                { // Swallow exception, file does not exist 
                }
            }
            Microsoft.SharePoint.Client.File file;
            if (ParameterSetName == ParameterSet_ASFILE)
            {

                file = folder.UploadFile(FileName, Path, true);
            }
            else
            {
                file = folder.UploadFile(FileName, Stream, true);
            }

            if (Values != null)
            {
                var item = file.ListItemAllFields;

                ListItemHelper.UpdateListItem(item, Values, ListItemUpdateType.UpdateOverwriteVersion,
                    (warning) =>
                    {
                        WriteWarning(warning);
                    },
                    (terminatingErrorMessage, terminatingErrorCode) =>
                    {
                        ThrowTerminatingError(new ErrorRecord(new Exception(terminatingErrorMessage), terminatingErrorCode, ErrorCategory.InvalidData, this));
                    });
            }
            if (ContentType != null)
            {
                var item = file.ListItemAllFields;
                item["ContentTypeId"] = targetContentType.Id.StringValue;
                item.UpdateOverwriteVersion();
                ClientContext.ExecuteQueryRetry();
            }

            if (Checkout)
                SelectedWeb.CheckInFile(fileUrl, CheckinType.MajorCheckIn, CheckInComment);


            if (Publish)
                SelectedWeb.PublishFile(fileUrl, PublishComment);

            if (Approve)
                SelectedWeb.ApproveFile(fileUrl, ApproveComment);
            ClientContext.Load(file);
            ClientContext.ExecuteQueryRetry();
            WriteObject(file);
        }

        /// <summary>
        /// Ensures the folder to which the file is to be uploaded exists. Changed from using the EnsureFolder implementation in PnP Sites Core as that requires at least member rights to the entire site to work.
        /// </summary>
        /// <returns>The folder to which the file needs to be uploaded</returns>
        private Folder EnsureFolder()
        {
            // First try to get the folder if it exists already. This avoids an Access Denied exception if the current user doesn't have Full Control access at Web level
            SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);
            var Url = UrlUtility.Combine(SelectedWeb.ServerRelativeUrl, Folder);

            Folder folder = null;
            try
            {
                folder = SelectedWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(Url));
                folder.EnsureProperties(f => f.ServerRelativeUrl);
                return folder;
            }
            // Exception will be thrown if the folder does not exist yet on SharePoint
            catch (ServerException serverEx) when (serverEx.ServerErrorCode == -2147024894)
            {
                // Try to create the folder
                folder = SelectedWeb.EnsureFolder(SelectedWeb.RootFolder, Folder);
                folder.EnsureProperties(f => f.ServerRelativeUrl);
                return folder;
            }
        }
    }
}
