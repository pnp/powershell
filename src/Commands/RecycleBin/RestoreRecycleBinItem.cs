using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.RecycleBin
{
    [Cmdlet(VerbsData.Restore, "PnPRecycleBinItem")]
    [OutputType(typeof(void))]
    public class RestoreRecycleBinItem : PnPSharePointCmdlet
    {
        private const string ParameterSetName_RESTORE_MULTIPLE_ITEMS_BY_ID = "Restore Multiple Items By Id";
        private const string ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID = "Restore Single Items By Id";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID, Position = 0, ValueFromPipeline = true)]
        public RecycleBinItemPipeBind Identity;

        
        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID)] 
        public int RowLimit;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetName_RESTORE_MULTIPLE_ITEMS_BY_ID)]
        public string[] IdList;

        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case ParameterSetName_RESTORE_SINGLE_ITEM_BY_ID:


                    if (ParameterSpecified(nameof(Identity)))
                    {
                        var recycleBinItem = Identity.GetRecycleBinItem(Connection.PnPContext);

                        if (recycleBinItem == null)
                        {
                            throw new PSArgumentException("Recycle bin item not found with the ID specified", nameof(Identity));
                        }

                        if (Force || ShouldContinue(string.Format(Resources.RestoreRecycleBinItem, recycleBinItem.LeafName), Resources.Confirm))
                        {
                            recycleBinItem.Restore();
                        }
                    }
                    else
                    {
                        if (ParameterSpecified(nameof(RowLimit)))
                        {
                            if (Force || ShouldContinue(string.Format(Resources.Restore0RecycleBinItems, RowLimit), Resources.Confirm))
                            {
                                var recycleBinItemCollection = RecycleBinUtility.GetRecycleBinItemCollection(ClientContext, RowLimit, RecycleBinItemState.None);
                                for (var i = 0; i < recycleBinItemCollection.Count; i++)
                                {
                                    var recycleBinItems = recycleBinItemCollection[i];
                                    recycleBinItems.RestoreAll();
                                    ClientContext.ExecuteQueryRetry();
                                }
                            }
                        }
                        else
                        {
                            if (Force || ShouldContinue(Resources.RestoreRecycleBinItems, Resources.Confirm))
                            {
                                Connection.PnPContext.Site.RecycleBin.RestoreAll();
                            }
                        }
                    }
                    break;

                case ParameterSetName_RESTORE_MULTIPLE_ITEMS_BY_ID:
                    RecycleBinUtility.RestoreRecycleBinItemInBulk(HttpClient, ClientContext, IdList, this);
                    break;

            }
        }
    }
}
