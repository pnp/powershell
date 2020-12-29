using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Reset, "PnPLabel")]
    public class ResetLabel : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public bool SyncToItems;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb, PnPContext);
            if (list != null)
            {
                list.SetComplianceTag(string.Empty, false, false, SyncToItems);
            }
            else
            {
                WriteWarning("List or library not found.");
            }
        }
    }
}