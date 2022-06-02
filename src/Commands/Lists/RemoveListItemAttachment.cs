using PnP.Core.Model.SharePoint;
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Properties;
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
            IList list = List.GetList(PnPContext);

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
            var files = item.AttachmentFiles.AsRequested();
            var removeText = Recycle.IsPresent ? "Recycle" : "Remove";
            if (All.IsPresent)
            {
                if (Force || ShouldContinue($"{removeText} all list item attachments?", Resources.Confirm))
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
                if (Force || ShouldContinue($"{removeText} all list item attachments?", Resources.Confirm))
                {
                    if (Recycle.IsPresent)
                    {
                        files?.ToList().Where(i => i.FileName == FileName)?.FirstOrDefault()?.Recycle();
                    }
                    else
                    {
                        files?.ToList().Where(i => i.FileName == FileName)?.FirstOrDefault()?.Delete();
                    }
                }
            }
        }
    }
}
