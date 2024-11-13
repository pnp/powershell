using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.DocumentSets
{
    [Cmdlet(VerbsCommon.Set, "PnPDocumentSetField")]
    [OutputType(typeof(void))]
    public class SetFieldInDocumentSet : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public DocumentSetPipeBind DocumentSet;

        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(FieldInternalNameCompleter))]
        public FieldPipeBind Field;

        [Parameter(Mandatory = false)]
        public SwitchParameter SetSharedField;

        [Parameter(Mandatory = false)]
        public SwitchParameter SetWelcomePageField;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveSharedField;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveWelcomePageField;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(SetSharedField)) && ParameterSpecified(nameof(RemoveSharedField)))
            {
                WriteWarning("Cannot set and remove a shared field at the same time");
                return;
            }
            if (ParameterSpecified(nameof(SetWelcomePageField)) && ParameterSpecified(nameof(RemoveWelcomePageField)))
            {
                WriteWarning("Cannot set and remove a welcome page field at the same time");
                return;
            }

            var docSetTemplate = DocumentSet.GetDocumentSetTemplate(CurrentWeb);


            ClientContext.Load(docSetTemplate, dt => dt.AllowedContentTypes, dt => dt.SharedFields, dt => dt.WelcomePageFields);
            ClientContext.ExecuteQueryRetry();

            var field = Field.Field;
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
            if (field != null)
            {
                // Check if field is part of the content types in the document set
                Field existingField = null;
                foreach (var allowedCtId in docSetTemplate.AllowedContentTypes)
                {
                    var allowedCt = CurrentWeb.GetContentTypeById(allowedCtId.StringValue, true);

                    var fields = allowedCt.Fields;
                    ClientContext.Load(fields, fs => fs.Include(f => f.Id));
                    ClientContext.ExecuteQueryRetry();
                    existingField = fields.FirstOrDefault(f => f.Id == field.Id);
                    if (existingField != null)
                    {
                        break;
                    }
                }
                if (existingField == null)
                {
                    var docSetCt = DocumentSet.ContentType;
                    var fields = docSetCt.Fields;
                    ClientContext.Load(fields, fs => fs.Include(f => f.Id));
                    ClientContext.ExecuteQueryRetry();
                    existingField = fields.FirstOrDefault(f => f.Id == field.Id);
                }
                if (existingField != null)
                {
                    if (SetSharedField)
                    {
                        docSetTemplate.SharedFields.Add(field);
                    }
                    if (SetWelcomePageField)
                    {
                        docSetTemplate.WelcomePageFields.Add(field);
                    }
                    if (RemoveSharedField)
                    {
                        docSetTemplate.SharedFields.Remove(field);
                    }
                    if (RemoveWelcomePageField)
                    {
                        docSetTemplate.WelcomePageFields.Remove(field.Id);
                    }
                    docSetTemplate.Update(true);
                    ClientContext.ExecuteQueryRetry();
                }
                else
                {
                    WriteWarning("Field not present in document set allowed content types");
                }
            }
        }
    }
}
