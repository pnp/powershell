using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines the content of an external item to add to Microsoft Search
/// </summary>
/// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-externalitemcontent#properties"/>
public class ExternalItemContent
{
    /// <summary>
    /// The type of content, Text or Html
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("type")]
    public Enums.SearchExternalItemContentType Type { get; set; }

    /// <summary>
    /// The content to add to the Microsoft Search index
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; }
}