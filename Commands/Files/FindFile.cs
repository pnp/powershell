using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Find, "PnPFile", DefaultParameterSetName = "Web")]
    public class FindFile : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Web", Position = 0)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "List", Position = 0)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Folder", Position = 0)]
        public string Match = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = "List")]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = "Folder")]
        public FolderPipeBind Folder;

        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case "List":
                {
                    var list = List.GetList(SelectedWeb);
                    WriteObject(list.FindFiles(Match));
                    break;
                }
                case "Folder":
                {
                    var folder = Folder.GetFolder(SelectedWeb);
                    WriteObject(folder.FindFiles(Match));
                    break;
                }
                default:
                {
                    WriteObject(SelectedWeb.FindFiles(Match));
                    break;
                }
            }
        }
    }
}
