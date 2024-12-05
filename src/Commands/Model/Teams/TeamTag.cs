namespace PnP.PowerShell.Commands.Model.Teams
{
    public partial class TeamTag
    {
        public string Id { get; set; }
        public string TeamId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int MemberCount { get; set; }
        public string TagType { get; set; }
    }
}
