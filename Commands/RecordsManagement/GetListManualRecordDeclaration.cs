using Microsoft.SharePoint.Client;
using PnP.Framework;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.RecordsManagement
{
    [Cmdlet(VerbsCommon.Get, "ListRecordDeclaration")]
    public class GetListRecordDeclaration : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(SelectedWeb);

            var d = new
            {
                ManualRecordDeclaration = list.GetListManualRecordDeclaration(),
                AutoRecordDeclaration = list.GetListAutoRecordDeclaration()
            };
            WriteObject(d);
        }
    }
}
