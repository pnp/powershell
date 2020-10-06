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
    [Cmdlet(VerbsCommon.Set, "ListRecordDeclaration")]
    public class SetListRecordDeclaration : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public EcmListManualRecordDeclaration? ManualRecordDeclaration;

        [Parameter(Mandatory = false)]
        public bool? AutoRecordDeclaration;
        
        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(SelectedWeb);

            if (ManualRecordDeclaration.HasValue)
            {
                list.SetListManualRecordDeclaration(ManualRecordDeclaration.Value);
            }

            if(AutoRecordDeclaration.HasValue)
            {
                list.SetListAutoRecordDeclaration(AutoRecordDeclaration.Value);
            }
        }

    }
}
