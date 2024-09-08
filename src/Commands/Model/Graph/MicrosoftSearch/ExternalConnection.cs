using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines a connection to an external datasource used by Microsoft Search
/// </summary>
/// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-externalconnection?view=graph-rest-1.0#properties" />
public class ExternalConnection
{
    /// <summary>
    /// Unique identifier of the external connection
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// Name of the external connection
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Description of the external connection
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// The configuration on the external connection
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("configuration")]
    public ExternalConnectionConfiguration Configuration { get; set; }

    /// <summary>
    /// The state of the external connection
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("state")]
    public Enums.SearchExternalConnectionState? State { get; set; }
}