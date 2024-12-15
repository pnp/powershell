using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPDefaultColumnValues")]
    [OutputType(typeof(ListDefaultColumnValue))]
    public class GetDefaultColumnValues : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

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
                    var defaultValues = list.GetDefaultColumnValues();
                    if (defaultValues != null)
                    {
                        foreach (var dict in defaultValues)
                        {
                            WriteObject(new ListDefaultColumnValue()
                            {
                                Path = dict["Path"],
                                Field = dict["Field"],
                                Value = dict["Value"]
                            });

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
}
