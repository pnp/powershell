using PnP.Core.Model.SharePoint;
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPListItemAttachment")]
    public class GetListItemAttachment : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Path = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            IList list = List.GetList(PnPContext);

            if (list == null)
            {
                throw new PSArgumentException($"Cannot find the list provided through -{nameof(List)}", nameof(List));
            }

            IListItem item = Identity.GetListItem(list);

            if (item == null)
            {
                throw new PSArgumentException($"Cannot find the list item provided through -{nameof(Identity)}", nameof(Identity));
            }

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

            item.EnsureProperties(i => i.AttachmentFiles);

            var attachmentFilesCollection = item.AttachmentFiles.AsRequested().ToArray();

            if (attachmentFilesCollection.Length == 0)
            {
                WriteWarning($"No attachments found for the list item provided through -{nameof(Identity)}");
            }
            else
            {
                // Enumerate over the attachments and download them
                foreach (var attachment in attachmentFilesCollection)
                {
                    string fileOut = System.IO.Path.Combine(Path, attachment.FileName);

                    if (System.IO.File.Exists(fileOut) && !Force)
                    {
                        WriteWarning($"File '{attachment.FileName}' exists already in the specified path. This file will be skipped. Use the -Force parameter to overwrite the file in the specified path.");
                    }
                    else
                    {
                        // Start the download
                        using (Stream downloadedContentStream = attachment.GetContent())
                        {
                            // Download the file bytes in 2MB chunks and immediately write them to a file on disk 
                            // This approach avoids the file being fully loaded in the process memory
                            var bufferSize = 2 * 1024 * 1024;  // 2 MB buffer

                            using (FileStream content = System.IO.File.Create(fileOut))
                            {
                                byte[] buffer = new byte[bufferSize];
                                int read;
                                while ((read = downloadedContentStream.ReadAsync(buffer, 0, buffer.Length).GetAwaiter().GetResult()) != 0)
                                {
                                    content.Write(buffer, 0, read);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
