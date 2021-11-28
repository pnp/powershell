using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPListItemComments")]
    public class GetListItemComments : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(PnPContext);

            if (list == null)
            {
                throw new PSArgumentException($"Cannot find List with Identity {List}", nameof(List));
            }

            var item = Identity.GetListItem(list);
            if (item == null)
            {
                throw new PSArgumentException($"Cannot find item with Identity {Identity}", nameof(Identity));
            }

            var commentsCollection = item.GetCommentsAsync().GetAwaiter().GetResult();

            WriteObject(commentsCollection);

        }
    }
}
