using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.Core.QueryModel;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPListItemComment")]
    [OutputType(typeof(ListItemComments))]
    public class GetListItemComments : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ListItemPipeBind Identity;

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

            var comments = commentsCollection.AsRequested();

            var commentsList = new List<ListItemComments>();

            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    var commentValue = new ListItemComments
                    {
                        Author = comment.Author,
                        CreatedDate = comment.CreatedDate,
                        Id = comment.Id,
                        IsLikedByUser = comment.IsLikedByUser,
                        IsReply = comment.IsReply,
                        ItemId = comment.ItemId,
                        LikeCount = comment.LikeCount,
                        ListId = comment.ListId,
                        ParentId = comment.ParentId,
                        ReplyCount = comment.ReplyCount,
                        Text = comment.Text
                    };

                    commentsList.Add(commentValue);
                }

                WriteObject(commentsList, true);
            }
        }
    }
}