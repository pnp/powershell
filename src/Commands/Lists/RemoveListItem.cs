using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "PnPListItem")]
    public class RemoveListItem : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ValidateNotNull]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);
            if (list == null)
            {
                throw new PSArgumentException(string.Format(Resources.ListNotFound, List.ToString()));
            }
            if (Identity != null)
            {
                var item = Identity.GetListItem(list);
                var message = $"{(Recycle ? "Recycle" : "Remove")} list item with id {item.Id}?";
                if (Force || ShouldContinue(message, Resources.Confirm))
                {
                    if (Recycle)
                    {
                        item.Recycle();
                    }
                    else
                    {
                        item.DeleteObject();
                    }
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}
