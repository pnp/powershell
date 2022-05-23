using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.RecycleBin
{
    [Cmdlet(VerbsData.Restore, "PnPRecycleBinItem")]
    [OutputType(typeof(void))]
    public class RestoreRecycleBinItem : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public RecycleBinItemPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public int RowLimit;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {

                var recycleBinItem = Identity.GetRecycleBinItem(ClientContext.Site);

                if (Force || ShouldContinue(string.Format(Resources.RestoreRecycleBinItem, recycleBinItem.LeafName), Resources.Confirm))
                {
                    recycleBinItem.Restore();
                    ClientContext.ExecuteQueryRetry();
                }
            }
            else
            {
                if (ParameterSpecified(nameof(RowLimit)))
                {
                    if (Force || ShouldContinue(Resources.RestoreRecycleBinItems, Resources.Confirm))
                    {
                        RecycleBinItemCollection items = ClientContext.Site.GetRecycleBinItems(null, RowLimit, false, RecycleBinOrderBy.DeletedDate, RecycleBinItemState.None);
                        ClientContext.Load(items);
                        ClientContext.ExecuteQueryRetry();

                        items.RestoreAll();
                        ClientContext.ExecuteQueryRetry();
                    }
                }
                else
                {
                    if (Force || ShouldContinue(Resources.RestoreRecycleBinItems, Resources.Confirm))
                    {
                        ClientContext.Site.RecycleBin.RestoreAll();
                        ClientContext.ExecuteQueryRetry();
                    }
                }
            }
        }
    }
}
