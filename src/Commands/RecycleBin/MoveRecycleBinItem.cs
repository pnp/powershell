using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.RecycleBin
{
    [Cmdlet(VerbsCommon.Move, "PnPRecycleBinItem")]
    [OutputType(typeof(void))]
    public class MoveRecycleBinItems : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public RecycleBinItemPipeBind Identity;
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var item = Identity.GetRecycleBinItem(ClientContext.Site);
                if (Force || ShouldContinue(string.Format(Resources.MoveRecycleBinItemWithLeaf0ToSecondStage, item.LeafName), Resources.Confirm))
                {
                    item.MoveToSecondStage();
                    ClientContext.ExecuteQueryRetry();
                }
            }
            else
            {
                if (Force || ShouldContinue(Resources.MoveFirstStageRecycleBinItemsToSecondStage, Resources.Confirm))
                {
                    ClientContext.Site.RecycleBin.MoveAllToSecondStage();
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}