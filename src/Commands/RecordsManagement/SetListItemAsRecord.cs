using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.RecordsManagement
{
    [Cmdlet(VerbsCommon.Set, "PnPListItemAsRecord")]
    public class SetListItemAsRecord : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public DateTime DeclarationDate;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

            var item = Identity.GetListItem(list);

            if (!ParameterSpecified(nameof(DeclarationDate)))
            {
                Microsoft.SharePoint.Client.RecordsRepository.Records.DeclareItemAsRecord(ClientContext, item);
            }
            else
            {
                Microsoft.SharePoint.Client.RecordsRepository.Records.DeclareItemAsRecordWithDeclarationDate(ClientContext, item, DeclarationDate);
            }
            ClientContext.ExecuteQueryRetry();
        }

    }
}