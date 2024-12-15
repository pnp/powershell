using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public sealed class NavigationNode
    {
        public List<Guid> AudienceIds { get; set; }
        public int CurrentLCID { get; set; }
        public List<object> CustomProperties { get; set; }
        public string FriendlyUrlSegment { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsHidden { get; set; }
        public bool IsTitleForExistingLanguage { get; set; }
        public string Key { get; set; }
        public List<NavigationNode> Nodes { get; set; }
        public int NodeType { get; set; }
        public bool? OpenInNewWindow { get; set; }
        public string SimpleUrl { get; set; }
        public string Title { get; set; }
        public List<object> Translations { get; set; }
    }
}
