using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Set, "PnPComplianceTagOnBulkItems")]
    public class SetComplianceTagOnBulkItems : PnPSharePointCmdlet
    {
        private const int MAXBATCHSIZE = 25;

        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public List<int> ItemIds;

        [Parameter(Mandatory = true)]
        [AllowEmptyString()]
        public string ComplianceTag;

        [Parameter(Mandatory = false)]
        [Range(0, MAXBATCHSIZE)]
        public int BatchSize = MAXBATCHSIZE;

        protected override void ExecuteCmdlet()
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

            if (ComplianceTag != string.Empty) 
            {
                var availableTags = PnPContext.Site.GetAvailableComplianceTags();
                ClientContext.ExecuteQueryRetry();

                var tag = availableTags.Where(t => t.TagName == ComplianceTag).FirstOrDefault();

                if (tag == null)
                {
                    WriteWarning($"Cannot find compliance tag with value: {ComplianceTag}");
                    return;
                }
            }

            PnPContext.Web.LoadAsync(i => i.Url);
            
            var list = List.GetList(PnPContext);
            list.LoadAsync(i => i.RootFolder);
            
            if (list == null)
            {
                WriteWarning("List or library not found.");
                return;
            }
            else 
            {
                var rootUrl = PnPContext.Web.Url.GetLeftPart(UriPartial.Authority);
                
                var rangeIndex = 0;

                while(ItemIds.Count > 0) 
                {
                    rangeIndex++;
                    var itemsToProcess = (ItemIds.Count > BatchSize) ? BatchSize : ItemIds.Count;

                    var range = ItemIds.GetRange(0, itemsToProcess);

                    WriteVerbose($"Applying compliance tag to batch {rangeIndex} of items");
                    Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.SetComplianceTagOnBulkItems(ClientContext, range, rootUrl + list.RootFolder.ServerRelativeUrl, ComplianceTag);

                    ItemIds.RemoveRange(0, itemsToProcess);
                }
            }
        }
    }
}