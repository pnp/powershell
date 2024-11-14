using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Add, "PnPFieldToContentType")]
    public class AddFieldToContentType : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(FieldInternalNameCompleter))]
        public FieldPipeBind Field;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ContentTypeCompleter))]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public SwitchParameter Required;

        [Parameter(Mandatory = false)]
        public SwitchParameter Hidden;

        [Parameter(Mandatory = false)]
        public bool UpdateChildren = true;

        protected override void ExecuteCmdlet()
        {
            Field field = Field.Field;
            if (field == null)
            {
                if (Field.Id != Guid.Empty)
                {
                    field = CurrentWeb.Fields.GetById(Field.Id);
                }
                else if (!string.IsNullOrEmpty(Field.Name))
                {
                    field = CurrentWeb.Fields.GetByInternalNameOrTitle(Field.Name);
                }
                ClientContext.Load(field);
                ClientContext.ExecuteQueryRetry();
            }
            if (field == null)
            {
                throw new PSArgumentException("Field not found", nameof(Field));
            }
            var ct = ContentType.GetContentTypeOrWarn(this, CurrentWeb);
            if (ct != null)
            {
                CurrentWeb.AddFieldToContentType(ct, field, Required, Hidden, UpdateChildren, true, false);
            }
        }
    }
}
