using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.RecycleBin
{
    [Cmdlet(VerbsCommon.Clear, "PnPRecycleBinItem", DefaultParameterSetName = PARAMETERSET_ALL)]
    [OutputType(typeof(void))]
    public class ClearRecycleBinItem : PnPSharePointCmdlet
    {
        const string PARAMETERSET_ALL = "All";
        const string PARAMETERSET_IDENTITY = "Identity";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = PARAMETERSET_IDENTITY)]
        public RecycleBinItemPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_ALL)]
        public SwitchParameter All;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_ALL)]
        public SwitchParameter SecondStageOnly = false;
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_IDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_ALL)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_ALL)]
        public int RowLimit;

        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case PARAMETERSET_IDENTITY:
                    var recycleBinItem = Identity.GetRecycleBinItem(ClientContext.Site);

                    if (Force || ShouldContinue(string.Format(Resources.ClearRecycleBinItem, recycleBinItem.LeafName), Resources.Confirm))
                    {
                        recycleBinItem.DeleteObject();
                        ClientContext.ExecuteQueryRetry();
                    }
                    break;
                case PARAMETERSET_ALL:
                    if (ParameterSpecified(nameof(RowLimit)))
                    {
                        if (Force || ShouldContinue(SecondStageOnly ? Resources.ClearSecondStageRecycleBin : Resources.ClearBothRecycleBins, Resources.Confirm))
                        {
                            RecycleBinItemState recycleBinStage = SecondStageOnly ? RecycleBinItemState.SecondStageRecycleBin : RecycleBinItemState.None;

                            RecycleBinUtility.RestoreOrClearRecycleBinItems(ClientContext, RowLimit, recycleBinStage, false);
                        }
                    }
                    else
                    {
                        if (SecondStageOnly)
                        {
                            if (Force || ShouldContinue(Resources.ClearSecondStageRecycleBin, Resources.Confirm))
                            {
                                ClientContext.Site.RecycleBin.DeleteAllSecondStageItems();
                                ClientContext.ExecuteQueryRetry();
                            }
                        }
                        else
                        {
                            if (Force || ShouldContinue(Resources.ClearBothRecycleBins, Resources.Confirm))
                            {
                                ClientContext.Site.RecycleBin.DeleteAll();
                                ClientContext.ExecuteQueryRetry();
                            }
                        }
                    }
                    break;
            }
        }
    }
}
