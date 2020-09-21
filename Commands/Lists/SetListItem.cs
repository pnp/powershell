using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.CmdletHelpAttributes;
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
            List list = null;
            if (List != null)
            {
                list = List.GetList(SelectedWeb);
            }
            if (list != null)
            {
                var item = Identity.GetListItem(list);

                if (ContentType != null)
                {
                    ContentType ct = null;
                    if (ContentType.ContentType == null)
                    {
                        if (ContentType.Id != null)
                        {
                            ct = SelectedWeb.GetContentTypeById(ContentType.Id, true);
                        }
                        else if (ContentType.Name != null)
                        {
                            ct = SelectedWeb.GetContentTypeByName(ContentType.Name, true);
                        }
                    }
                    else
                    {
                        ct = ContentType.ContentType;
                    }
                    if (ct != null)
                    {
                        ct.EnsureProperty(w => w.StringId);
                        item["ContentTypeId"] = ct.StringId;
                        if (SystemUpdate.IsPresent)
                        {
                            item.SystemUpdate();
                        }
                        else
                        {
                            item.Update();
                        }
                        ClientContext.ExecuteQueryRetry();
                    }
                }
                if (Values != null)
                {
                    var updateType = ListItemUpdateType.Update;
                    if (SystemUpdate.IsPresent)
                    {
                        updateType = ListItemUpdateType.SystemUpdate;
                    }
                    item = ListItemHelper.UpdateListItem(item, Values, updateType, (warning) =>
                      {
                          WriteWarning(warning);
                      },
                      (terminatingErrorMessage, terminatingErrorCode) =>
                      {
                          ThrowTerminatingError(new ErrorRecord(new Exception(terminatingErrorMessage), terminatingErrorCode, ErrorCategory.InvalidData, this));
                      }
                      );
                }

                if (!string.IsNullOrEmpty(Label))
                {
                    IList<Microsoft.SharePoint.Client.CompliancePolicy.ComplianceTag> tags = Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.GetAvailableTagsForSite(ClientContext, ClientContext.Url);
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
                WriteObject(item);
            }
        }
    }
}