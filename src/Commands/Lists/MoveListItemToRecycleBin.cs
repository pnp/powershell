using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Move, "PnPListItemToRecycleBin")]
    [OutputType(typeof(RecycleResult))]
    public class MoveListItemToRecycleBin : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(Connection.PnPContext);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");
            if (Identity != null)
            {
                var item = Identity.GetListItem(list);
                if (Force || ShouldContinue(string.Format(Properties.Resources.MoveListItemWithId0ToRecycleBin, item.Id), Properties.Resources.Confirm))
                {
                    var recycleBinResult = item.Recycle();
                    WriteObject(new RecycleResult { RecycleBinItemId = recycleBinResult });
                }
            }
        }
    }
}
