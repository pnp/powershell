using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph
{
    public class Group
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string MailNickname { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GroupVisibility? Visibility { get; set; }

        [JsonPropertyName("owners@odata.bind")]
        public List<string> Owners { get; set; }

        [JsonPropertyName("members@odata.bind")]
        public List<string> Members { get; set; }

        public string Classification { get; set; }

        public bool MailEnabled { get; set; }

        public List<string> GroupTypes { get; set; }

        public bool? SecurityEnabled { get; set; }

        public List<string> CreationOptions { get; set; }
        
        [JsonPropertyName("extension_fe2174665583431c953114ff7268b7b3_Education_ObjectType")]
        public string EducationObjectType { get; set; }

        public List<string> ResourceBehaviorOptions { get; set; }
        public List<AssignedLabels> AssignedLabels { get; set; }
    }

    public enum GroupVisibility
    {
        NotSpecified,
        Private,
        Public,
        HiddenMembership
    }

}
