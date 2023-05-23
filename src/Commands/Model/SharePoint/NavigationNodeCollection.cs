using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public sealed class NavigationNodeCollection
    {
        public List<object> AudienceIds { get; set; }
        public string FriendlyUrlPrefix { get; set; }
        public bool IsAudienceTargetEnabledForGlobalNav { get; set; }
        public List<NavigationNode> Nodes { get; set; }
        public string SimpleUrl { get; set; }
        public string SPSitePrefix { get; set; }
        public string SPWebPrefix { get; set; }
        public string StartingNodeKey { get; set; }
        public string StartingNodeTitle { get; set; }
        public DateTime Version { get; set; }
    }
}
