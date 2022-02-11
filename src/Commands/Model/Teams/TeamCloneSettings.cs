using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Graph;
using PnP.PowerShell.Commands.Model.Graph;

namespace PnP.PowerShell.Commands.Model.Teams
{
     public class TeamCloneInformation
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string MailNickName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GroupVisibility Visibility { get; set; }

        public string Classification { get; set; }
        public ClonableTeamParts[] PartsToClone { get; set; }
    }
}
