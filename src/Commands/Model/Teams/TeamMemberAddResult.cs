using PnP.PowerShell.Commands.Model.Graph;

namespace PnP.PowerShell.Commands.Model.Teams
{
    /// <summary>
    /// Outcome of adding one member to a team, as reported by a bulk add of members.
    /// </summary>
    internal class TeamMemberAddResult
    {
        public string UserId { get; set; }

        public GraphError Error { get; set; }
    }
}
