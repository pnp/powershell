using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "PnPListItem", DefaultParameterSetName = ParameterSet_SINGLE)]
    public class RemoveListItem : PnPWebCmdlet
    {
        const string ParameterSet_BATCHED = "Batched";
        const string ParameterSet_SINGLE = "Single";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        [Parameter(ValueFromPipeline = true, Position = 0)]
        [ValidateNotNull]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        [Parameter(ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        public SwitchParameter Force;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BATCHED)]
        public PnPBatch Batch;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Batch)))
            {
                var list = List.GetList(Batch);

                var id = Identity.GetItemId();
                {
                    if (Recycle)
                    {
                        list.Items.RecycleByIdBatch(Batch.Batch, id.Value);
                    }
                    else
                    {
                        list.Items.DeleteByIdBatch(Batch.Batch, id.Value);
                    }
                }
            }
            else
            {
                if(Identity == null || (Identity.Item == null && Identity.Id == 0))
                {
                    throw new PSArgumentException($"No -Identity has been provided specifying the item to remove");
                }

                List list;
                if(List != null)
                {
                    list = List.GetList(CurrentWeb);
                }
                else
                {
                    if(Identity.Item == null)
                    {
                        throw new PSArgumentException($"No -List has been provided specifying the list to remove");
                    }

                    list = Identity.Item.ParentList;
                }

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
