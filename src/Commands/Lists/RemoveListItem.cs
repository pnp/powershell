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
                List list;
                if(List != null)
                {
                    list = List.GetList(CurrentWeb);

                    if (Identity == null || (Identity.Item == null && Identity.Id == 0))
                    {
                        // Remove all list items from the list
                        if (Force || ShouldContinue($"{(Recycle ? "Recycle" : "Remove")} all items in the list?", Resources.Confirm))
                        {
                            CamlQuery query = new CamlQuery { ViewXml = "<View><Query><Where></Where></Query><ViewFields><FieldRef Name='ID' /></ViewFields><RowLimit>100</RowLimit></View>" };

                            bool stillItemsToProcess = true;
                            while(stillItemsToProcess)
                            {
                                var listItems = list.GetItems(query);
                                ClientContext.Load(listItems, listItem => listItem.Include(oneItem => oneItem, oneItem => oneItem["ID"]));
                                ClientContext.ExecuteQueryRetry();

                                var itemsToProcess = listItems.Count;
                                if (itemsToProcess > 0)
                                {
                                    for (var x = itemsToProcess - 1; x >= 0; x--)
                                    {
                                        if (Recycle)
                                        {
                                            listItems[x].Recycle();
                                        }
                                        else
                                        {
                                            listItems[x].DeleteObject();
                                        }
                                    }
                                    ClientContext.ExecuteQueryRetry();
                                }
                                else
                                {
                                    stillItemsToProcess = false;
                                }
                            } 
                        }
                        return;
                    }
                }
                else
                {
                    if(Identity == null || Identity.Item == null)
                    {
                        throw new PSArgumentException($"No -Identity has been provided specifying the item to remove");
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
