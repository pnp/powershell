using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// An tag on an item in the Microsoft 365 Roadmap
    /// </summary>
    public class Microsoft365RoadmapTag
    {
        [JsonPropertyName("tagName")]
        public string TagName { get; set; }
    }
}