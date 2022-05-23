using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.RecycleBin
{
    [Cmdlet(VerbsCommon.Get, "PnPRecycleBinItem", DefaultParameterSetName = ParameterSet_ALL)]
    [OutputType(typeof(RecycleBinItem))]
    public class GetRecycleBinItems : PnPRetrievalsCmdlet<RecycleBinItem>
    {
        private const string ParameterSet_ALL = "All";
        private const string ParameterSet_IDENTITY = "Identity";
        private const string ParameterSet_FIRSTSTAGE = "FirstStage";
        private const string ParameterSet_SECONDSTAGE = "SecondStage";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_IDENTITY)]
        public Guid Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FIRSTSTAGE)]
        public SwitchParameter FirstStage;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SECONDSTAGE)]
        public SwitchParameter SecondStage;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FIRSTSTAGE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SECONDSTAGE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public int RowLimit;
        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<RecycleBinItem, object>>[] { r => r.Id, r => r.Title, r => r.ItemType, r => r.LeafName, r => r.DirName };
            if (ParameterSetName == ParameterSet_IDENTITY)
            {
                RecycleBinItem item = ClientContext.Site.RecycleBin.GetById(Identity);

                ClientContext.Load(item, RetrievalExpressions);
                ClientContext.ExecuteQueryRetry();
                WriteObject(item);
            }
            else
            {

                if (ParameterSpecified(nameof(RowLimit)))
                {
                    RecycleBinItemState recycleBinStage;
                    switch (ParameterSetName)
                    {
                        case ParameterSet_FIRSTSTAGE:
                            recycleBinStage = RecycleBinItemState.FirstStageRecycleBin;
                            break;
                        case ParameterSet_SECONDSTAGE:
                            recycleBinStage = RecycleBinItemState.SecondStageRecycleBin;
                            break;
                        default:
                            recycleBinStage = RecycleBinItemState.None;
                            break;
                    }

                    if (FirstStage.IsPresent || SecondStage.IsPresent)
                    {
                        RecycleBinItemCollection items = ClientContext.Site.GetRecycleBinItems(null, RowLimit, false, RecycleBinOrderBy.DeletedDate, recycleBinStage);
                        ClientContext.Load(items);
                        ClientContext.ExecuteQueryRetry();

                        List<RecycleBinItem> recycleBinItemList = items.ToList();
                        WriteObject(recycleBinItemList, true);
                    }
                    else
                    {
                        ClientContext.Site.Context.Load(ClientContext.Site.RecycleBin, r => r.IncludeWithDefaultProperties(RetrievalExpressions));
                        ClientContext.Site.Context.ExecuteQueryRetry();

                        List<RecycleBinItem> recycleBinItemList = ClientContext.Site.RecycleBin.ToList();
                        recycleBinItemList = recycleBinItemList.Take(RowLimit).ToList();
                        WriteObject(recycleBinItemList, true);
                    }


                }
                else
                {
                    ClientContext.Site.Context.Load(ClientContext.Site.RecycleBin, r => r.IncludeWithDefaultProperties(RetrievalExpressions));
                    ClientContext.Site.Context.ExecuteQueryRetry();

                    List<RecycleBinItem> recycleBinItemList = ClientContext.Site.RecycleBin.ToList();

                    switch (ParameterSetName)
                    {
                        case ParameterSet_FIRSTSTAGE:
                            WriteObject(
                                recycleBinItemList.Where(i => i.ItemState == RecycleBinItemState.FirstStageRecycleBin), true);
                            break;
                        case ParameterSet_SECONDSTAGE:
                            WriteObject(
                                recycleBinItemList.Where(i => i.ItemState == RecycleBinItemState.SecondStageRecycleBin),
                                true);
                            break;
                        default:
                            WriteObject(recycleBinItemList, true);
                            break;
                    }
                }
            }
        }
    }
}
