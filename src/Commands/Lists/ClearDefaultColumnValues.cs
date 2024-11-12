using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Entities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    //TODO: Create Test

    [Cmdlet(VerbsCommon.Clear, "PnPDefaultColumnValues")]
    [OutputType(typeof(void))]
    public class ClearDefaultColumnValues : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public FieldPipeBind Field;

        [Parameter(Mandatory = false)]
        public string Folder = "/";

        protected override void ExecuteCmdlet()
        {
            List list = List.GetList(CurrentWeb);

            if (list.BaseTemplate == (int)ListTemplateType.DocumentLibrary || list.BaseTemplate == (int)ListTemplateType.WebPageLibrary || list.BaseTemplate == (int)ListTemplateType.PictureLibrary)
            {
                Field field = null;
                // Get the field
                if (Field.Field != null)
                {
                    field = Field.Field;

                    ClientContext.Load(field);
                    ClientContext.ExecuteQueryRetry();

                    field.EnsureProperties(f => f.TypeAsString, f => f.InternalName);
                }
                else if (Field.Id != Guid.Empty)
                {
                    field = list.Fields.GetById(Field.Id);
                    ClientContext.Load(field, f => f.InternalName, f => f.TypeAsString);
                    ClientContext.ExecuteQueryRetry();
                }
                else if (!string.IsNullOrEmpty(Field.Name))
                {
                    field = list.Fields.GetByInternalNameOrTitle(Field.Name);
                    ClientContext.Load(field, f => f.InternalName, f => f.TypeAsString);
                    ClientContext.ExecuteQueryRetry();
                }
                if (field != null)
                {
                    // Folder must be the relative path to the library with "/" prefix
                    if (Folder != null && !Folder.StartsWith("/"))
                    {
                        Folder = $"/{Folder}";
                    }

                    IDefaultColumnValue defaultColumnValue = field.GetDefaultColumnValueFromField(ClientContext, Folder, new string[0]);
                    list.ClearDefaultColumnValues(new List<IDefaultColumnValue>() { defaultColumnValue });
                }
                else
                {
                    throw new PSArgumentException("Field not found", nameof(Field));
                }
            }
            else
            {
                WriteWarning("List is not a document library");
            }
        }
    }
}
