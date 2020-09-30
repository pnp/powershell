using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "PnPFieldToContentType")]
    public class AddFieldToContentType : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public FieldPipeBind Field;

        [Parameter(Mandatory = true)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public SwitchParameter Required;

        [Parameter(Mandatory = false)]
        public SwitchParameter Hidden;

        protected override void ExecuteCmdlet()
        {
            Field field = Field.Field;
            if (field == null)
            {
                if (Field.Id != Guid.Empty)
                {
                    field = SelectedWeb.Fields.GetById(Field.Id);
                }
                else if (!string.IsNullOrEmpty(Field.Name))
                {
                    field = SelectedWeb.Fields.GetByInternalNameOrTitle(Field.Name);
                }
                ClientContext.Load(field);
                ClientContext.ExecuteQueryRetry();
            }
            if (field != null)
            {
                if (ContentType.ContentType != null)
                {
                    SelectedWeb.AddFieldToContentType(ContentType.ContentType, field, Required, Hidden);
                }
                else
                {
                    ContentType ct;
                    if (!string.IsNullOrEmpty(ContentType.Id))
                    {
                        ct = SelectedWeb.GetContentTypeById(ContentType.Id);
                      
                    }
                    else
                    {
                        ct = SelectedWeb.GetContentTypeByName(ContentType.Name);
                    }
                    if (ct != null)
                    {
                        SelectedWeb.AddFieldToContentType(ct, field, Required, Hidden);
                    }
                }
            }
            else
            {
                throw new Exception("Field not found");
            }
        }


    }
}
