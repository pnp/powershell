using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Remove, "PnPFieldFromContentType")]
    public class RemoveFieldFromContentType : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(FieldInternalNameCompleter))]
        public FieldPipeBind Field;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ContentTypeCompleter))]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public SwitchParameter DoNotUpdateChildren;

        protected override void ExecuteCmdlet()
        {
            Field field = Field.Field;
            if (field == null)
            {
                try
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
                catch
                {
                    // Swallow exception in case we fail to retrieve the field. It will be handled by the null-check.
                    field = null;
                }
            }

            if (field is null)
            {
                throw new PSArgumentException("Field not found", nameof(Field));
            }

            var ct = ContentType.GetContentTypeOrThrow(nameof(ContentType), CurrentWeb, true);
            ct.EnsureProperty(c => c.FieldLinks);

            var fieldLink = ct.FieldLinks.FirstOrDefault(f => f.Id == field.Id);
            if (fieldLink is null)
            {
                throw new PSArgumentException("Cannot find field reference in content type");
            }
            fieldLink.DeleteObject();
            ct.Update(!DoNotUpdateChildren);
            ClientContext.ExecuteQueryRetry();
        }
    }
}
