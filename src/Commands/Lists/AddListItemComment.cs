using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Add, "PnPListItemComment")]
    [OutputType(typeof(IComment))]
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
            var list = List.GetList(Connection.PnPContext);

            if (list == null)
            {
                throw new PSArgumentException($"Cannot find list provided through -{nameof(List)}", nameof(List));
            }

            var item = Identity.GetListItem(list);
            
            if (item == null)
            {
                throw new PSArgumentException($"Cannot find list item provided through -{nameof(Identity)}", nameof(Identity));
            }

            if (string.IsNullOrEmpty(Text))
            {
                throw new PSArgumentException($"Comment Text must contain a value", nameof(Text));
            }

            var comments = item.GetComments();

            var addedComment = comments.Add(Text);

            WriteObject(addedComment);
        }
    }
}