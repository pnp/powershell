using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "PnPListItem")]
    public class RemoveListItem : PnPWebCmdlet
    {
        const string ParameterSet_BATCHED = "Batched";
        const string ParameterSet_SINGLE = "Single";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ValidateNotNull]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = false)]
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
}
