using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.RecycleBin
{
    [Cmdlet(VerbsData.Restore, "PnPRecycleBinItem")]
    [OutputType(typeof(void))]
    public class RestoreRecycleBinItem : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
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
                    if (Force || ShouldContinue(string.Format(Resources.Restore0RecycleBinItems, RowLimit), Resources.Confirm))
                    {
                        RecycleBinUtility.RestoreOrClearRecycleBinItems(ClientContext, RowLimit, RecycleBinItemState.None, true);
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
