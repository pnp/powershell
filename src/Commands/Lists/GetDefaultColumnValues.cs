using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPDefaultColumnValues")]
    public class GetDefaultColumnValues : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            List list = null;
            if (List != null)
            {
                list = List.GetList(SelectedWeb);
            }
            if (list != null)
            {
                if (list.BaseTemplate == (int)ListTemplateType.DocumentLibrary || list.BaseTemplate == (int)ListTemplateType.WebPageLibrary || list.BaseTemplate == (int)ListTemplateType.PictureLibrary)
                {
                    var defaultValues = list.GetDefaultColumnValues();
                    var dynamicList = new List<dynamic>();
                    if (defaultValues != null)
                    {
                        foreach (var dict in defaultValues)
                        {
                            dynamicList.Add(
                                new
                                {
                                    Path = dict["Path"],
                                    Field = dict["Field"],
                                    Value = dict["Value"]
                                });

                        }
                        WriteObject(dynamicList, true);
                    }
                }
            }
            else
            {
                WriteWarning("List is not a document library");
            }
        }
    }
}
