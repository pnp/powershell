using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Add, "PnPListItemComment")]
    public class AddListItemComment : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string Text;

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

            if (string.IsNullOrEmpty(Text))
            {
                throw new PSArgumentException($"Comment Text must contain a value", nameof(Text));
            }

            var comments = item.GetCommentsAsync().GetAwaiter().GetResult();

            comments.Add(Text);
        }
    }
}
