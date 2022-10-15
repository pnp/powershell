using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPImageListItemColumn")]
    [OutputType(typeof(ListItem))]
    public class ImageListItemColumn : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public FieldPipeBind Field = new FieldPipeBind();

        [Parameter(Mandatory = true)]
        public string Path;

        [Parameter(Mandatory = false)]
        public ListItemUpdateType UpdateType;

        protected override void ExecuteCmdlet()
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
                ?? throw new PSArgumentException($"Provided -Identity is not valid.", nameof(Identity));

            var f = Field.Field;

            if (f == null)
            {
                if (Field.Id != Guid.Empty)
                {
                    f = list.Fields.GetById(Field.Id);
                }
                else if (!string.IsNullOrEmpty(Field.Name))
                {
                    f = list.Fields.GetByInternalNameOrTitle(Field.Name);
                }
            }
            ClientContext.Load(f);
            ClientContext.ExecuteQueryRetry();

            if (f.FieldTypeKind != FieldType.Thumbnail)
            {
                throw new PSInvalidOperationException("Field Type is not Image, please use a valid field");
            }

            var tenantUri = new Uri(Connection.Url);

            var payload = new
            {
                type = "thumbnail",
                fileName = System.IO.Path.GetFileName(Path),
                nativeFile = new { },
                fieldName = f.InternalName,
                serverUrl = "https://" + tenantUri.Host,
                fieldId = f.Id,
                serverRelativeUrl = Path,
                id = Guid.NewGuid().ToString()
            };

            item[f.InternalName] = JsonSerializer.Serialize(payload);

            ListItemHelper.UpdateListItem(item, UpdateType);

            ClientContext.ExecuteQueryRetry();
            ClientContext.Load(item);
            WriteObject(item);
        }
    }
}