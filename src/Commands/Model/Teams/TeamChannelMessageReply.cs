using System;

namespace PnP.PowerShell.Commands.Model.Teams
{
    public class TeamChannelMessageReply
    {
        public string Id { get; set; }

        public string ReplyToId { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? DeletedDateTime { get; set; }

        public DateTime? LastModifiedDateTime { get; set; }

        public string Importance { get; set; } = "normal";

        public TeamChannelMessageBody Body { get; set; } = new TeamChannelMessageBody();

        public TeamChannelMessageFrom From { get; set; } = new TeamChannelMessageFrom();
    }
}
