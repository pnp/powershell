using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.RecordsManagement
{
    [Cmdlet(VerbsCommon.Set, "PnPListRecordDeclaration")]
    public class SetListRecordDeclaration : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public EcmListManualRecordDeclaration? ManualRecordDeclaration;

        [Parameter(Mandatory = false)]
        public bool? AutoRecordDeclaration;
        
        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);

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
