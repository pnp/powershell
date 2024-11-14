using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.RecordsManagement
{
    [Cmdlet(VerbsCommon.Clear, "PnPListItemAsRecord")]
    public class ClearListItemAsRecord : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

            var item = Identity.GetListItem(list);

            Microsoft.SharePoint.Client.RecordsRepository.Records.UndeclareItemAsRecord(ClientContext, item);

            ClientContext.ExecuteQueryRetry();
        }

    }

}