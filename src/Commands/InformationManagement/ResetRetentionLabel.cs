﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Reset, "PnPRetentionLabel", DefaultParameterSetName = ParamSet_List)]
    public class ResetRetentionLabel : PnPSharePointCmdlet
    {
        private const int MAXBATCHSIZE = 25;

        private const string ParamSet_List = "Reset on a list";
        private const string ParamSet_BulkItems = "Reset on items in bulk";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_BulkItems)]
        public List<int> ItemIds;

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_BulkItems)]
        [Range(0, MAXBATCHSIZE)]
        public int BatchSize = MAXBATCHSIZE;

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_List)]
        public bool SyncToItems;

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

            var pnpContext = Connection.PnPContext;
            var list = List.GetList(pnpContext);
            if (list != null)
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

                        WriteVerbose($"Clearing retention label on batch {rangeIndex} of items");
                        Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.SetComplianceTagOnBulkItems(ClientContext, range, rootUrl + list.RootFolder.ServerRelativeUrl, string.Empty);
                        ClientContext.ExecuteQueryRetry();

                        ItemIds.RemoveRange(0, itemsToProcess);
                    }
                }
                else 
                {
                    list.SetComplianceTag(string.Empty, false, false, SyncToItems);
                }
            }
            else
            {
                WriteWarning("List or library not found.");
            }
        }
    }
}