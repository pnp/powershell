using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Extensions;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPListItemVersion")]
    public class GetListItemVersion : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

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

            if (item.Versions is not null)
            {
                WriteObject(item.Versions.ToList(), true);
            }
        }
    }
}