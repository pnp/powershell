using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
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
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = "Folder")]
        public FolderPipeBind Folder;

        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case "List":
                {
                    var list = List.GetList(CurrentWeb);
                    if (list == null)
                        throw new ArgumentException("The specified list was not found");
                    WriteObject(list.FindFiles(Match), true);
                    break;
                }
                case "Folder":
                {
                    var folder = Folder.GetFolder(CurrentWeb, false);
                    if (folder == null)
                        throw new ArgumentException("The specified folder was not found");
                    WriteObject(folder.FindFiles(Match), true);
                    break;
                }
                default:
                {
                    WriteObject(CurrentWeb.FindFiles(Match), true);
                    break;
                }
            }
        }
    }
}
