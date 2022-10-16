using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities;

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

                    List<RecycleBinItem> recycleBinItemList = RecycleBinUtility.GetRecycleBinItems(ClientContext, RowLimit, recycleBinStage);
                    WriteObject(recycleBinItemList, true);
                }
                else
                {
                    List<RecycleBinItem> recycleBinItemList;
                    switch (ParameterSetName)
                    {
                        case ParameterSet_FIRSTSTAGE:
                            recycleBinItemList = RecycleBinUtility.GetRecycleBinItems(ClientContext, RowLimit, RecycleBinItemState.FirstStageRecycleBin);
                            WriteObject(recycleBinItemList, true);
                            break;
                        case ParameterSet_SECONDSTAGE:
                            recycleBinItemList = RecycleBinUtility.GetRecycleBinItems(ClientContext, RowLimit, RecycleBinItemState.SecondStageRecycleBin);
                            WriteObject(recycleBinItemList, true);
                            break;
                        default:
                            recycleBinItemList = RecycleBinUtility.GetRecycleBinItems(ClientContext, RowLimit);
                            WriteObject(recycleBinItemList, true);
                            break;
                    }
                }
            }
        }


    }
}
