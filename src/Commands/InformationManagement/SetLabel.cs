using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System;

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

        [Obsolete("Overriding Purview retention label settings has been deprecated in SharePoint Online. This parameter will be removed in the next major release.")]
        [Parameter(Mandatory = false)]
        public bool BlockDeletion;

        [Obsolete("Overriding Purview retention label settings has been deprecated in SharePoint Online. This parameter will be removed in the next major release.")]
        [Parameter(Mandatory = false)]
        public bool BlockEdit;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(PnPContext);
            var availableTags = PnPContext.Site.GetAvailableComplianceTags();

            if (list != null)
            {
                var availableTag = availableTags.FirstOrDefault(tag => tag.TagName.ToString() == Label);
                if (availableTag != null)
                {
                    list.SetComplianceTag(Label, availableTag.BlockDelete, availableTag.BlockEdit, SyncToItems);
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