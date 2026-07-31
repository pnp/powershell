using PnP.PowerShell.Commands.Model.Graph;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.Teams
{
    /// <summary>
    /// Response of a bulk add of members to a team. Microsoft Graph answers the call with an HTTP 200 when every member
    /// was added and an HTTP 207 when only some of them were, so the per member results have to be inspected to find out
    /// which additions failed.
    /// </summary>
    internal class TeamMemberAddResultCollection
    {
        public List<TeamMemberAddResult> Value { get; set; }
    }

    internal class TeamMemberAddResult
    {
        public string UserId { get; set; }

        public GraphError Error { get; set; }
    }
}
