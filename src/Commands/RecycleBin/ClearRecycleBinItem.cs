using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
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
                    var recycleBinItem = Identity.GetRecycleBinItem(Connection.PnPContext);

                    if (Force || ShouldContinue(string.Format(Resources.ClearRecycleBinItem, recycleBinItem.LeafName), Resources.Confirm))
                    {
                        recycleBinItem.Delete();
                    }
                    break;
                case PARAMETERSET_ALL:
                    if (ParameterSpecified(nameof(RowLimit)))
                    {
                        if (Force || ShouldContinue(SecondStageOnly ? Resources.ClearSecondStageRecycleBin : Resources.ClearBothRecycleBins, Resources.Confirm))
                        {
                            RecycleBinItemState recycleBinStage = SecondStageOnly ? RecycleBinItemState.SecondStageRecycleBin : RecycleBinItemState.None;

                            var recycleBinItemCollection = RecycleBinUtility.GetRecycleBinItemCollection(ClientContext, RowLimit, recycleBinStage);
                            for (var i = 0; i < recycleBinItemCollection.Count; i++)
                            {
                                var recycleBinItems = recycleBinItemCollection[i];
                                recycleBinItems.DeleteAll();
                                ClientContext.ExecuteQueryRetry();
                            }
                        }
                    }
                    else
                    {
                        if (SecondStageOnly)
                        {
                            if (Force || ShouldContinue(Resources.ClearSecondStageRecycleBin, Resources.Confirm))
                            {
                                Connection.PnPContext.Site.RecycleBin.DeleteAllSecondStageItems();
                            }
                        }
                        else
                        {
                            if (Force || ShouldContinue(Resources.ClearBothRecycleBins, Resources.Confirm))
                            {
                                Connection.PnPContext.Site.RecycleBin.DeleteAll();
                            }
                        }
                    }
                    break;
            }
        }
    }
}
