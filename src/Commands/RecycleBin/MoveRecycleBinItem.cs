using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
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
                var item = Identity.GetRecycleBinItem(Connection.PnPContext);
                if (Force || ShouldContinue(string.Format(Resources.MoveRecycleBinItemWithLeaf0ToSecondStage, item.LeafName), Resources.Confirm))
                {
                    item.MoveToSecondStage();
                }
            }
            else
            {
                if (Force || ShouldContinue(Resources.MoveFirstStageRecycleBinItemsToSecondStage, Resources.Confirm))
                {
                    Connection.PnPContext.Site.RecycleBin.MoveAllToSecondStage();
                }
            }
        }
    }
}