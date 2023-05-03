using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Extensions;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "PnPListItemVersion")]
    public class RemoveListItemVersion : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemVersionPipeBind Version;
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);

            if (list is null)
            {
                throw new PSArgumentException($"Cannot find the list provided through -{nameof(List)}", nameof(List));
            }

            var item = Identity.GetListItem(list);
        
            if (item is null)
            {
                throw new PSArgumentException($"Cannot find the list item provided through -{nameof(Identity)}", nameof(Identity));
            }

            item.LoadProperties(i => i.Versions);

            ListItemVersion version = null;
            if (!string.IsNullOrEmpty(Version.VersionLabel))
            {
                version = item.Versions.FirstOrDefault(v => v.VersionLabel == Version.VersionLabel);
            }
            else if (Version.Id != -1)
            {
                version = item.Versions.FirstOrDefault(v => v.VersionId == Version.Id);
            }

            if (version is null)
            {
                throw new PSArgumentException($"Cannot find the list item version provided through -{nameof(Version)}", nameof(Version));
            }

            if(Force || ShouldContinue(string.Format(Resources.Delete0, version.VersionLabel), Resources.Confirm))
            {
                WriteVerbose($"Trying to remove version {Version.VersionLabel}");
                
                version.DeleteObject();
                ClientContext.ExecuteQueryRetry();
                
                WriteVerbose($"Removed version {Version.VersionLabel} of list item {item.Id} in list {list.Title}");
            }
        }
    }
}