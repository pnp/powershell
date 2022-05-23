using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    public class UserWithRightsAssignedDetailed
    {
        public string Title { get; set; }
        public string LoginName { get; set; }
        public string Email { get; set; }
        public List<string> Groups { get; set; }
        public Dictionary<string, string> Permissions { get; set; }
    }
}