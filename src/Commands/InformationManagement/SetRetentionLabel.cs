﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Set, "PnPRetentionLabel", DefaultParameterSetName = ParamSet_List)]
    [Alias("Set-PnPLabel")]
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
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Label;

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_List)]
        public bool SyncToItems;

        [Obsolete("Overriding Purview retention label settings has been deprecated in SharePoint Online. This parameter will be removed in the next major release.")]
        [Parameter(Mandatory = false, ParameterSetName = ParamSet_List)]
        public bool BlockDeletion;

        [Obsolete("Overriding Purview retention label settings has been deprecated in SharePoint Online. This parameter will be removed in the next major release.")]
        [Parameter(Mandatory = false, ParameterSetName = ParamSet_List)]
        public bool BlockEdit;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParamSet_BulkItems)
            {
                if (BatchSize > MAXBATCHSIZE) 
                {
                    BatchSize = MAXBATCHSIZE;
                    WriteVerbose($"Overriding batch size");
                }

                if (ItemIds == null)
                {
                    WriteWarning("No items provided");
                    return;
                }
            }
            

            var list = List.GetList(PnPContext);
            var availableTags = PnPContext.Site.GetAvailableComplianceTags();

            if (list != null)
            {
                var availableTag = availableTags.FirstOrDefault(tag => tag.TagName.ToString() == Label);
                if (availableTag != null)
                {
                    if (ParameterSetName == ParamSet_BulkItems) 
                    {
                        PnPContext.Web.LoadAsync(i => i.Url);
                        list.LoadAsync(i => i.RootFolder);                        

                        var rootUrl = PnPContext.Web.Url.GetLeftPart(UriPartial.Authority);
                    
                        var rangeIndex = 0;

                        while(ItemIds.Count > 0) 
                        {
                            rangeIndex++;
                            var itemsToProcess = (ItemIds.Count > BatchSize) ? BatchSize : ItemIds.Count;

                            var range = ItemIds.GetRange(0, itemsToProcess);

                            WriteVerbose($"Setting retention label to batch {rangeIndex} of items");
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
                    WriteWarning("The provided label is not available in the site.");
                }
            }
            else
            {
                WriteWarning("List or library not found.");
            }
        }
    }
}