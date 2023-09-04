using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.CompliancePolicy;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;

// IMPORTANT: If you make changes to this cmdlet, also make the similar/same changes to the Add-PnPListItem Cmdlet

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPListItem", DefaultParameterSetName = ParameterSet_SINGLE)]
    [OutputType(typeof(ListItem))]
    public class SetListItem : PnPWebCmdlet
    {
        const string ParameterSet_SINGLE = "Single";
        const string ParameterSet_BATCHED = "Batched";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        [Parameter(ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        [Parameter(ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        public Hashtable Values;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        public string Label;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        public DateTime LabelAppliedDate;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        public string LabelAppliedByEmail;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        public SwitchParameter ClearLabel;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        public ListItemUpdateType UpdateType;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BATCHED)]
        [ValidateNotNull]
        public PnPBatch Batch;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BATCHED)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Batch)))
            {
                SetListItemBatched();
            }
            else
            {
                SetListItemSingle();
            }
        }

        private void SetListItemBatched()
        {
            var list = List.GetList(Batch, throwError: true);
            var item = Identity.GetListItem(list);
            if (item == null)
            {
                throw new PSArgumentException($"Cannot find item with Identity {Identity}", nameof(Identity));
            }

            var values = ListItemHelper.GetFieldValues(list, item, Values, ClientContext, Batch);
            if (values == null)
            {
                values = new Dictionary<string, object>();
            }
            if (ContentType != null)
            {
                var ct = ContentType.GetContentTypeOrWarn(this, Batch, list);
                if (ct != null)
                {
                    values.Add("ContentTypeId", ct.StringId);
                }
            }

            if (!values.Any() && !Force)
            {
                WriteWarning("No values provided. Pass -Force to update anyway.");
            }
            else
            {
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

        private void SetListItemSingle()
        {
            if (Identity == null || (Identity.Item == null && Identity.Id == 0))
            {
                throw new PSArgumentException($"No -Identity has been provided specifying the item to update", nameof(Identity));
            }

            List list;
            if (List != null)
            {
                list = List.GetList(CurrentWeb);
            }
            else
            {
                if (Identity.Item == null)
                {
                    throw new PSArgumentException($"No -List has been provided specifying the list to update the item in", nameof(Identity));
                }

                list = Identity.Item.ParentList;
            }

            var item = Identity.GetListItem(list)
                ?? throw new PSArgumentException($"Provided -Identity is not valid.", nameof(Identity)); ;

            bool itemUpdated = false;

            if (ClearLabel)
            {
                ClientContext.Web.EnsureProperties(w => w.Url, w => w.ServerRelativeUrl);
                list.EnsureProperties(l => l.RootFolder, l => l.RootFolder.ServerRelativeUrl);

                string listUrl = string.Empty;
                if (ClientContext.Web.ServerRelativeUrl.Equals("/"))
                {
                    listUrl = ClientContext.Web.Url + list.RootFolder.ServerRelativeUrl;
                }
                else
                {
                    listUrl = ClientContext.Web.Url.Replace(ClientContext.Web.ServerRelativeUrl, "") + list.RootFolder.ServerRelativeUrl;
                }

                SPPolicyStoreProxy.SetComplianceTagOnBulkItems(ClientContext, new[] { item.Id }, listUrl, string.Empty);
                ClientContext.ExecuteQueryRetry();
                itemUpdated = true;
            }

            if (!string.IsNullOrEmpty(Label))
            {
                var tags = SPPolicyStoreProxy.GetAvailableTagsForSite(ClientContext, ClientContext.Url);
                ClientContext.ExecuteQueryRetry();

                var tag = tags.Where(t => t.TagName == Label).FirstOrDefault();

                if (tag is null)
                {
                    WriteWarning("Can not find compliance tag with value: " + Label);
                }
                else
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(LabelAppliedByEmail))
                        {
                            if (LabelAppliedDate == default(DateTime))
                            {
                                LabelAppliedDate = new DateTime();
                            }
                            item.SetComplianceTagWithMetaInfo(tag.TagName, tag.BlockDelete, tag.BlockEdit, LabelAppliedDate, LabelAppliedByEmail, tag.SuperLock, tag.UnlockedAsDefault);
                        }
                        else
                        {
                            ClientContext.Web.EnsureProperties(w => w.Url, w => w.ServerRelativeUrl);
                            list.EnsureProperties(l => l.RootFolder, l => l.RootFolder.ServerRelativeUrl);

                            string listUrl = string.Empty;
                            if (ClientContext.Web.ServerRelativeUrl.Equals("/"))
                            {
                                listUrl = ClientContext.Web.Url + list.RootFolder.ServerRelativeUrl;
                            }
                            else
                            {
                                listUrl = ClientContext.Web.Url.Replace(ClientContext.Web.ServerRelativeUrl, "") + list.RootFolder.ServerRelativeUrl;
                            }

                            SPPolicyStoreProxy.SetComplianceTagOnBulkItems(ClientContext, new[] { item.Id }, listUrl, tag.TagName);
                            ClientContext.ExecuteQueryRetry();
                        }
                    }
                    catch (System.Exception error)
                    {
                        WriteWarning(error.Message.ToString());
                    }
                }
                itemUpdated = true;
            }

            if (ContentType != null)
            {
                ContentType ct = ContentType.GetContentTypeOrWarn(this, list);
                if (ct != null)
                {
                    item["ContentTypeId"] = ct.EnsureProperty(w => w.StringId); ;
                    ListItemHelper.UpdateListItem(item, UpdateType);
                    ClientContext.ExecuteQueryRetry();
                }
                itemUpdated = true;
            }

            if (Values?.Count > 0)
            {
                ListItemHelper.SetFieldValues(item, Values, this);
                ListItemHelper.UpdateListItem(item, UpdateType);
                itemUpdated = true;
            }

            if (!itemUpdated && !Force)
            {
                WriteWarning("No values provided. Pass -Force to update anyway.");
            }
            else
            {
                if (Force)
                {
                    ListItemHelper.UpdateListItem(item, UpdateType);
                }
            }

            ClientContext.ExecuteQueryRetry();
            ClientContext.Load(item);
            WriteObject(item);
        }
    }
}