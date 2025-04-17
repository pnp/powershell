using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PnP.PowerShell.Commands.Base.Completers;


namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Set, "PnPRetentionLabel", DefaultParameterSetName = ParamSet_List)]
    public class SetRetentionLabel : PnPSharePointCmdlet
    {
        private const string ParamSet_List = "Set on a list";
        private const string ParamSet_BulkItems = "Set on items in bulk";

        private const int MAXBATCHSIZE = 25;

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_BulkItems)]
        [Range(0, MAXBATCHSIZE)]
        public int BatchSize = MAXBATCHSIZE;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_BulkItems)]
        public List<int> ItemIds;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Label;

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_List)]
        public bool SyncToItems;        

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParamSet_BulkItems)
            {
                if (BatchSize > MAXBATCHSIZE) 
                {
                    BatchSize = MAXBATCHSIZE;
                    LogDebug($"Overriding batch size");
                }

                if (ItemIds == null)
                {
                    LogWarning("No items provided");
                    return;
                }
            }

            var pnpContext = Connection.PnPContext;
            var list = List.GetList(pnpContext);
            var availableTags = pnpContext.Site.GetAvailableComplianceTags();

            if (list != null)
            {
                var availableTag = availableTags.FirstOrDefault(tag => tag.TagName.ToString() == Label);
                if (availableTag != null)
                {
                    if (ParameterSetName == ParamSet_BulkItems) 
                    {
                        pnpContext.Web.LoadAsync(i => i.Url);
                        list.LoadAsync(i => i.RootFolder);                        

                        var rootUrl = pnpContext.Web.Url.GetLeftPart(UriPartial.Authority);
                    
                        var rangeIndex = 0;

                        while(ItemIds.Count > 0) 
                        {
                            rangeIndex++;
                            var itemsToProcess = (ItemIds.Count > BatchSize) ? BatchSize : ItemIds.Count;

                            var range = ItemIds.GetRange(0, itemsToProcess);

                            LogDebug($"Setting retention label to batch {rangeIndex} of items");
                            Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.SetComplianceTagOnBulkItems(ClientContext, range, rootUrl + list.RootFolder.ServerRelativeUrl, Label);
                            ClientContext.ExecuteQueryRetry();
                            ItemIds.RemoveRange(0, itemsToProcess);
                        }
                    }
                    else 
                    {
                        list.SetComplianceTag(Label, availableTag.BlockDelete, availableTag.BlockEdit, SyncToItems);
                    }
                }
                else
                {
                    LogWarning("The provided label is not available in the site.");
                }
            }
            else
            {
                LogWarning("List or library not found.");
            }
        }
    }
}