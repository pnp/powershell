using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;

// IMPORTANT: If you make changes to this cmdlet, also make the similar/same changes to the Set-PnPListItem Cmdlet

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Add, "PnPListItem")]
    public class AddListItem : PnPWebCmdlet
    {
        private const string ParameterSet_SINGLE = "Set add a single list item";
        private const string ParameterSet_BATCHED = "Adds items in a batched manner";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ValidateNotNull]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public Hashtable Values;

        [Parameter(Mandatory = false)]
        public string Folder;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        public string Label;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        [ValidateNotNull]
        public Batch Batch;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Batch)))
            {
                var list = List.GetList(CurrentWeb, PnPContext, l => l.Fields.LoadProperties(f => f.Id, f => f.Title, f => f.InternalName, f => f.TypeAsString));
                var values = ListItemHelper.GetFieldValues(list, Values, this, ClientContext);
                list.Items.AddBatch(Batch, values);
            }
            else
            {
                List list = List.GetList(CurrentWeb);
                ListItemCreationInformation liCI = new ListItemCreationInformation();
                if (Folder != null)
                {
                    // Create the folder if it doesn't exist
                    var rootFolder = list.EnsureProperty(l => l.RootFolder);
                    var targetFolder =
                        CurrentWeb.EnsureFolder(rootFolder, Folder);

                    liCI.FolderUrl = targetFolder.ServerRelativeUrl;
                }
                var item = list.AddItem(liCI);

                bool systemUpdate = false;
                if (ContentType != null)
                {
                    var ct = ContentType.GetContentType(list);

                    if (ct != null)
                    {

                        item["ContentTypeId"] = ct.EnsureProperty(w => w.StringId);
                        item.Update();
                        systemUpdate = true;
                        ClientContext.ExecuteQueryRetry();
                    }
                }

                if (Values?.Count > 0)
                {
                    ListItemHelper.SetFieldValues(item, Values, this);
                }

                if (!String.IsNullOrEmpty(Label))
                {
                    IList<Microsoft.SharePoint.Client.CompliancePolicy.ComplianceTag> tags = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAvailableTagsForSite(ClientContext, ClientContext.Url);
                    ClientContext.ExecuteQueryRetry();

                    var tag = tags.Where(t => t.TagName == Label).FirstOrDefault();

                    if (tag != null)
                    {
                        item.SetComplianceTag(tag.TagName, tag.BlockDelete, tag.BlockEdit, tag.IsEventTag, tag.SuperLock);
                    }
                    else
                    {
                        WriteWarning("Can not find compliance tag with value: " + Label);
                    }
                }

                if (systemUpdate)
                {
                    item.SystemUpdate();
                }
                else
                {
                    item.Update();
                }
                ClientContext.Load(item);
                ClientContext.ExecuteQueryRetry();
                WriteObject(item);
            }
        }
    }
}
