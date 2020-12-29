using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Set, "PnPLabel")]
    public class SetLabel : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public string Label;

        [Parameter(Mandatory = false)]
        public bool SyncToItems;

        [Parameter(Mandatory = false)]
        public bool BlockDeletion;

        [Parameter(Mandatory = false)]
        public bool BlockEdit;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb, PnPContext);
            if (list != null)
            {
                list.SetComplianceTag(Label, BlockDeletion, BlockEdit, SyncToItems);
            }
            else
            {
                WriteWarning("List or library not found.");
            }
        }
    }
}