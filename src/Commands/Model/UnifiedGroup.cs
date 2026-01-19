using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Represents a unified group (Microsoft 365 Group) in SharePoint Multi-Geo
    /// </summary>
    public class UnifiedGroup
    {
        public string Id { get; set; }
        public string GroupAlias { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }
        public string MailNickname { get; set; }
        public string SiteUrl { get; set; }
        public string DataLocation { get; set; }
        public string PreferredDataLocation { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public string[] Owners { get; set; }
        public string[] Members { get; set; }
        public string Visibility { get; set; }
        public string Classification { get; set; }
        public bool MailEnabled { get; set; }
        public bool SecurityEnabled { get; set; }
        public string GroupType { get; set; }
    }
}