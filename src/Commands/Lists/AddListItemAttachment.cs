using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Add, "PnPListItemAttachment")]
    [OutputType(typeof(IAttachment))]
    public class AddListItemAttachment : PnPWebCmdlet
    {
        private const string ParameterSet_ASFILE = "Upload file";
        private const string ParameterSet_ASSTREAM = "Upload file from stream";
        private const string ParameterSet_ASTEXT = "Upload file from text";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_ASSTREAM)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_ASFILE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_ASTEXT)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1, ParameterSetName = ParameterSet_ASSTREAM)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1, ParameterSetName = ParameterSet_ASFILE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1, ParameterSetName = ParameterSet_ASTEXT)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASFILE)]
        [ValidateNotNullOrEmpty]
        public string Path = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASSTREAM)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASTEXT)]
        [ValidateNotNullOrEmpty]
        public string FileName = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASFILE)]
        [ValidateNotNullOrEmpty]
        public string NewFileName = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASSTREAM)]
        [ValidateNotNullOrEmpty]
        public Stream Stream;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASTEXT)]
        public string Content;

        protected override void ExecuteCmdlet()
        {
            IList list = List.GetList(Connection.PnPContext);

            if (list == null)
            {
                throw new PSArgumentException($"Cannot find list provided through -{nameof(List)}", nameof(List));
            }

            IListItem item = Identity.GetListItem(list);

            if (item == null)
            {
                throw new PSArgumentException($"Cannot find list item provided through -{nameof(Identity)}", nameof(Identity));
            }

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

            item.EnsureProperties(i => i.AttachmentFiles);

            IAttachment addedAttachment = null;
            switch (ParameterSetName)
            {
                case ParameterSet_ASFILE:
                    addedAttachment = item.AttachmentFiles.Add(FileName, File.OpenRead(Path));
                    WriteObject(addedAttachment);
                    break;

                case ParameterSet_ASTEXT:
                    using (var stream = new MemoryStream())
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.Write(Content);
                            writer.Flush();
                            stream.Position = 0;
                            addedAttachment = item.AttachmentFiles.Add(FileName, stream);
                            WriteObject(addedAttachment);
                        }
                    }
                    break;

                default:
                    addedAttachment = item.AttachmentFiles.Add(FileName, Stream);
                    WriteObject(addedAttachment);
                    break;
            }
        }
    }
}
