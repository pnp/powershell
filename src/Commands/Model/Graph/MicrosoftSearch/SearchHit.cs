using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines a hit in a search result from Microsoft Graph
/// </summary>
/// <seealso cref="https://learn.microsoft.com/en-us/graph/api/search-query?view=graph-rest-1.0&tabs=http" />
public class SearchHit
{
    /// <summary>
    /// Unique identifier of the search result
    /// </summary>
    [JsonPropertyName("hitId")]
    public string Id { get; set; }

    /// <summary>
    /// Name of the source of the content
    /// </summary>
    [JsonPropertyName("contentSource")]
    public string ContentSource { get; set; }

    /// <summary>
    /// The rank of the search result
    /// </summary>
    [JsonPropertyName("rank")]
    public short Rank { get; set; }

    /// <summary>
    /// A summary of the search result
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// The resource properties of the search result
    /// </summary>
    [JsonPropertyName("resource")]
    public SearchResultResource Resource { get; set; }
}