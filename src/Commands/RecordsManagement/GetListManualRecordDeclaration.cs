using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.RecordsManagement
{
    [Cmdlet(VerbsCommon.Get, "PnPListRecordDeclaration")]
    public class GetListRecordDeclaration : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);

            var d = new
            {
                ManualRecordDeclaration = list.GetListManualRecordDeclaration(),
                AutoRecordDeclaration = list.GetListAutoRecordDeclaration()
            };
            WriteObject(d);
        }
    }
}
