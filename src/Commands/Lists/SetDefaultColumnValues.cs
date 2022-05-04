﻿using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Entities;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "PnPDefaultColumnValues")]
    [OutputType(typeof(void))]
    public class SetDefaultColumnValues : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public FieldPipeBind Field;

        [Parameter(Mandatory = true)]
        public string[] Value;

        [Parameter(Mandatory = false)]
        public string Folder = "/";

        protected override void ExecuteCmdlet()
        {
            List list = null;
            if (List != null)
            {
                list = List.GetList(CurrentWeb);
            }
            if (list != null)
            {
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
                        if (!string.IsNullOrEmpty(Folder))
                        {
                            if (Folder.IndexOfAny(new[] { '#', '%' }) > -1)
                            {
                                throw new PSArgumentException("Due to limitations of SharePoint Online, setting a default column value on a folder with special characters is not supported");
                            }
                        }
                        IDefaultColumnValue defaultColumnValue = field.GetDefaultColumnValueFromField(ClientContext, Folder, Value);
                        list.SetDefaultColumnValues(new List<IDefaultColumnValue>() { defaultColumnValue });
                    }
                }
                else
                {
                    WriteWarning("List is not a document library");
                }
            }
        }
    }
}
