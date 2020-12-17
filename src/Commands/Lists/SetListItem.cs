using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;

// IMPORTANT: If you make changes to this cmdlet, also make the similar/same changes to the Add-PnPListItem Cmdlet

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPListItem")]
    public class SetListItem : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = false)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public Hashtable Values;

        [Parameter(Mandatory = false)]
        public SwitchParameter SystemUpdate;

        [Parameter(Mandatory = false)]
        public String Label;

        protected override void ExecuteCmdlet()
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
                    var updateType = ListItemUpdateType.Update;
                    if (SystemUpdate.IsPresent)
                    {
                        updateType = ListItemUpdateType.SystemUpdate;
                    }
                    ListItemHelper.UpdateListItem(item, updateType);
                }
                ClientContext.ExecuteQuery();
                ClientContext.Load(item);
                WriteObject(item);
            }
        }
    }
}