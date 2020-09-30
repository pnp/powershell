using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;

// IMPORTANT: If you make changes to this cmdlet, also make the similar/same changes to the Set-PnPListItem Cmdlet

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Add, "PnPListItem")]
    public class AddListItem : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public Hashtable Values;

        [Parameter(Mandatory = false)]
        public string Folder;

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
                ListItemCreationInformation liCI = new ListItemCreationInformation();
                if (Folder != null)
                {
                    // Create the folder if it doesn't exist
                    var rootFolder = list.EnsureProperty(l => l.RootFolder);
                    var targetFolder =
                        SelectedWeb.EnsureFolder(rootFolder, Folder);

                    liCI.FolderUrl = targetFolder.ServerRelativeUrl;
                }
                var item = list.AddItem(liCI);

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
                        item.Update();
                        ClientContext.ExecuteQueryRetry();
                    }
                }

                if (Values != null)
                {
                    item = ListItemHelper.UpdateListItem(item, Values, ListItemUpdateType.Update,
                        (warning) =>
                        {
                            WriteWarning(warning);
                        },
                        (terminatingErrorMessage, terminatingErrorCode) =>
                        {
                            ThrowTerminatingError(new ErrorRecord(new Exception(terminatingErrorMessage), terminatingErrorCode, ErrorCategory.InvalidData, this));
                        });
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

                item.Update();
                ClientContext.Load(item);
                ClientContext.ExecuteQueryRetry();
                WriteObject(item);
            }
        }
    }
}
