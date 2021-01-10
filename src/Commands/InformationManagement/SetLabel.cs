using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;

namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Set, "PnPLabel")]
    public class SetLabel : PnPSharePointCmdlet
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
            var list = List.GetList(PnPContext);
            var availableTags = PnPContext.Site.GetAvailableComplianceTags();

            if (list != null)
            {
                if (availableTags.FirstOrDefault(tag => tag.TagName.ToString() == Label) != null)
                {
                    list.SetComplianceTag(Label, BlockDeletion, BlockEdit, SyncToItems);
                }
                else
                {
                    WriteWarning("The provided label is not available in the site.");
                }
            }
            else
            {
                WriteWarning("List or library not found.");
            }
        }
    }
}