using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Properties;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "PnPListItemComment")]
    [OutputType(typeof(void))]
    public class RemoveListItemComment : PnPWebCmdlet
    {
        private const string ParameterSet_SINGLE = "Single";
        private const string ParameterSet_Multiple = "Multiple";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_Multiple)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1, ParameterSetName = ParameterSet_Multiple)]
        public ListItemPipeBind Identity;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_SINGLE)]
        public string Text;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_Multiple)]
        public SwitchParameter All;

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_SINGLE)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_Multiple)]
        public SwitchParameter Force;

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

            var commentsCollection = item.GetComments();

            var commentsEnumerable = commentsCollection.AsRequested();

            if (All.IsPresent)
            {
                if (Force || ShouldContinue($"Remove all list item comments?", Resources.Confirm))
                {
                    var commentsList = commentsEnumerable.ToList();
                    for (int i = commentsList.Count - 1; i >= 0; i--)
                    {
                        commentsList[i].Delete();
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Text))
                {
                    throw new PSArgumentException($"Comment Text must contain a value", nameof(Text));
                }

                var commentToBeDeleted = commentsEnumerable.Where(t => t.Text == Text).FirstOrDefault();

                if (commentToBeDeleted != null)
                {
                    if (Force || ShouldContinue($"Remove list item comment?", Resources.Confirm))
                    {
                        commentToBeDeleted.Delete();
                    }
                }
            }
        }
    }
}