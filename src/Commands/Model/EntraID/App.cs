using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    public class App
    {
        public string Id { get; set; }
        public string AppId { get; set; }
        public string DisplayName { get; set; }
        public string SignInAudience { get; set; }
        public bool? IsFallbackPublicClient { get; set; }
        public List<AppResource> RequiredResourceAccess { get; set; }

        public AppPublicClient PublicClient { get; set; }
    }
}