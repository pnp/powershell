using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Base.Completers;

// IMPORTANT: If you make changes to this cmdlet, also make the similar/same changes to the Set-PnPListItem Cmdlet

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Add, "PnPListItem", DefaultParameterSetName = ParameterSet_SINGLE)]
    [OutputType(typeof(ListItem))]
    public class AddListItem : PnPWebCmdlet
    {
        private const string ParameterSet_SINGLE = "Single";
        private const string ParameterSet_BATCHED = "Batched";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        [ValidateNotNull]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        [Parameter(Mandatory = false)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        [Parameter(Mandatory = false)]
        public Hashtable Values;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        [Parameter(Mandatory = false)]
        public string Folder;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        public string Label;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BATCHED)]
        [ValidateNotNull]
        public PnPBatch Batch;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Batch)))
            {
                var list = List.GetList(Batch, false) ?? throw new PSArgumentException($"The specified list through the {nameof(List)} parameter was not found. Notice that the title is case sensitive.", nameof(List));

                var values = ListItemHelper.GetFieldValues(list, null, Values, ClientContext, Batch);
                if (ContentType != null)
                {                
                    var contentType = ContentType.GetContentTypeOrWarn(this, Batch, list);
                    values.Add("ContentTypeId", contentType.StringId);
                }
                list.Items.AddBatch(Batch.Batch, values, Folder);
            }
            else
            {
                List list = List.GetList(CurrentWeb) ?? throw new PSArgumentException($"The specified list through the {nameof(List)} parameter was not found. Notice that the title is case sensitive.", nameof(List));

                ListItemCreationInformation liCI = new();
                if (Folder != null)
                {
                    // Create the folder if it doesn't exist
                    var rootFolder = list.EnsureProperty(l => l.RootFolder);
                    var targetFolder = CurrentWeb.EnsureFolder(rootFolder, Folder);

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

                if (!string.IsNullOrEmpty(Label))
                {
                    IList<Microsoft.SharePoint.Client.CompliancePolicy.ComplianceTag> tags = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAvailableTagsForSite(ClientContext, ClientContext.Url);
                    ClientContext.ExecuteQueryRetry();

                    var tag = tags.Where(t => t.TagName == Label).FirstOrDefault();

                    if (tag != null)
                    {
                        item.SetComplianceTag(tag.TagName, tag.BlockDelete, tag.BlockEdit, tag.IsEventTag, tag.SuperLock, tag.UnlockedAsDefault);
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