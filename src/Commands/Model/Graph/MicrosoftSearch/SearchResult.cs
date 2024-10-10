using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines a search result from Microsoft Graph
/// </summary>
/// <seealso cref="https://learn.microsoft.com/en-us/graph/api/search-query?view=graph-rest-1.0&tabs=http" />
public class SearchResult
{
    /// <summary>
    /// Unique identifier of the search result
    /// </summary>
    [JsonPropertyName("hitsContainers")]
    public List<SearchHitsContainer> HitsContainers { get; set; }
}