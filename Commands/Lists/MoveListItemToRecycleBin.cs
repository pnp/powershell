using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Move, "PnPListItemToRecycleBin", SupportsShouldProcess = true)]
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
            var list = List.GetList(SelectedWeb);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");
            if (Identity != null)
            {
                var item = Identity.GetListItem(list);
                if (Force || ShouldContinue(string.Format(Properties.Resources.MoveListItemWithId0ToRecycleBin,item.Id), Properties.Resources.Confirm))
                {
                    item.Recycle();
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}
