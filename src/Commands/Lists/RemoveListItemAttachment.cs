using PnP.Core.Model.SharePoint;
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Properties;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "PnPListItemAttachment")]
    public class RemoveListItemAttachment : PnPWebCmdlet
    {
        private const string ParameterSet_SINGLE = "Single";
        private const string ParameterSet_Multiple = "Multiple";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_Multiple)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1, ParameterSetName = ParameterSet_Multiple)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SINGLE)]
        [ValidateNotNullOrEmpty]
        public string FileName = string.Empty;       

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Multiple)]
        public SwitchParameter All;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Multiple)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_Multiple)]
        public SwitchParameter Force;

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

            item.EnsureProperties(i => i.AttachmentFiles);
            var files = item.AttachmentFiles.AsRequested().ToArray();
            var removeText = Recycle.IsPresent ? "Recycle" : "Remove";

            if(files.Length == 0)
            {
                WriteWarning($"No attachments found on the list item that can be {removeText.ToLower()}d");
                return;
            }
            
            if (All.IsPresent)
            {
                if (Force || ShouldContinue($"{removeText} {(files.Length != 1 ? $"all {files.Length}" : "the")} list item attachment{(files.Length != 1 ? "s" : "")}?", Resources.Confirm))
                {
                    foreach (var file in files.ToList())
                    {
                        if (Recycle.IsPresent)
                        {
                            file.Recycle();
                        }
                        else
                        {
                            file.Delete();
                        }
                    }
                }
            }
            else
            {
                // Try to locate the attachment file that needs to be deleted
                var fileToDelete = files.FirstOrDefault(i => i.FileName.Equals(FileName, StringComparison.InvariantCultureIgnoreCase));

                if(fileToDelete == null)
                {
                    WriteWarning($"No attachment found with the name '{FileName}'");
                }
                else
                {
                    if (Force || ShouldContinue($"{removeText} list item attachment '{fileToDelete.FileName}'?", Resources.Confirm))
                    {
                        if (Recycle.IsPresent)
                        {
                            fileToDelete.Recycle();
                        }
                        else
                        {
                            fileToDelete.Delete();
                        }
                    }
                }
            }
        }
    }
}
