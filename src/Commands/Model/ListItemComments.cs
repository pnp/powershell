using System;

namespace PnP.PowerShell.Commands.Model
{
    public class ListItemComments
    {
        public Core.Model.Security.ISharePointSharingPrincipal Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Id { get; set; }
        public bool IsLikedByUser { get; set; }
        public bool IsReply { get; set; }
        public int ItemId { get; set; }
        public Core.Model.SharePoint.ICommentLikeUserEntityCollection LikedBy { get; set; }
        public int LikeCount { get; set; }
        public Guid ListId { get; set; }
        public string ParentId { get; set; }
        public Core.Model.SharePoint.ICommentCollection Replies { get; set; }
        public Core.Model.SharePoint.ICommentLikeUserEntityCollection Mentions { get; set; }
        public int ReplyCount { get; set; }
        public string Text { get; set; }        
    }
}
