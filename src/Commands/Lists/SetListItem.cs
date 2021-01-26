using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;

// IMPORTANT: If you make changes to this cmdlet, also make the similar/same changes to the Add-PnPListItem Cmdlet

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPListItem")]
    public class SetListItem : PnPWebCmdlet
    {
        const string ParameterSet_SINGLE = "Single";
        const string Parameterset_BATCHED = "Batched";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]

        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = false)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public Hashtable Values;

        [Parameter(Mandatory = false)]
        [Obsolete("Use '-UpdateType SystemUpdate' instead.")]
        public SwitchParameter SystemUpdate;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        public String Label;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        public SwitchParameter ClearLabel;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        [ValidateNotNull]

        [Parameter(Mandatory = false)]
        public ListItemUpdateType UpdateType;

        public PnPBatch Batch;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(SystemUpdate)))
            {
                UpdateType = ListItemUpdateType.SystemUpdate;
            }
            if (ParameterSpecified(nameof(Batch)))
            {
                var list = List.GetList(Batch);
                if (list != null)
                {
                    var item = Identity.GetListItem(list);
                    if (item == null)
                    {
                        throw new PSArgumentException($"Cannot find item with Identity {Identity}");
                    }
                    var values = ListItemHelper.GetFieldValues(list, item, Values, ClientContext);
                    if (values == null)
                    {
                        values = new Dictionary<string, object>();
                    }
                    if (ContentType != null)
                    {
                        var ct = ContentType.GetContentType(Batch, list);
                        if (ct != null)
                        {
                            values.Add("ContentTypeId", ct.StringId);
                        }
                    }
                    foreach (var value in values)
                    {
                        item[value.Key] = values[value.Key];
                    }
                    switch (UpdateType)
                    {
                        case ListItemUpdateType.SystemUpdate:
                            {
                                item.SystemUpdateBatch(Batch.Batch);
                                break;
                            }
                        case ListItemUpdateType.UpdateOverwriteVersion:
                            {
                                item.UpdateOverwriteVersionBatch(Batch.Batch);
                                break;
                            }
                        case ListItemUpdateType.Update:
                            {
                                item.UpdateBatch(Batch.Batch);
                                break;
                            }
                    }
                }
            }
            else
            {
                List list = List.GetList(CurrentWeb);

                if (list != null)
                {
                    var item = Identity.GetListItem(list);

                    bool updateRequired = false;

                    if (ContentType != null)
                    {
                        ContentType ct = ContentType.GetContentType(list);

                        if (ct != null)
                        {

                            item["ContentTypeId"] = ct.EnsureProperty(w => w.StringId); ;
                            updateRequired = true;
                        }
                    }
                    if (Values != null)
                    {
                        ListItemHelper.SetFieldValues(item, Values, this);
                        updateRequired = true;
                    }

                    if (ParameterSpecified(nameof(ClearLabel)))
                    {
                        item.SetComplianceTag(string.Empty, false, false, false, false);
                        ClientContext.ExecuteQueryRetry();
                    }
                    if (!string.IsNullOrEmpty(Label))
                    {
                        var tags = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAvailableTagsForSite(ClientContext, ClientContext.Url);
                        ClientContext.ExecuteQueryRetry();

                        var tag = tags.Where(t => t.TagName == Label).FirstOrDefault();

                        if (tag != null)
                        {
                            try
                            {
                                item.SetComplianceTag(tag.TagName, tag.BlockDelete, tag.BlockEdit, tag.IsEventTag, tag.SuperLock);
                                ClientContext.ExecuteQueryRetry();
                            }
                            catch (System.Exception error)
                            {
                                WriteWarning(error.Message.ToString());
                            }
                        }
                        else
                        {
                            WriteWarning("Can not find compliance tag with value: " + Label);
                        }
                    }

                    if (updateRequired)
                    {
                        ListItemHelper.UpdateListItem(item, UpdateType);
                    }
                    ClientContext.ExecuteQueryRetry();
                    ClientContext.Load(item);
                    WriteObject(item);
                }
            }
        }
    }
}