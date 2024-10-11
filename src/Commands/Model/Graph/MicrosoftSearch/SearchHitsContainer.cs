using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines a hitcontainer in a search result from Microsoft Graph
/// </summary>
/// <seealso cref="https://learn.microsoft.com/en-us/graph/api/search-query?view=graph-rest-1.0&tabs=http" />
public class SearchHitsContainer
{
    /// <summary>
    /// Collection with search hits
    /// </summary>
    [JsonPropertyName("hits")]
    public List<SearchHit> Hits { get; set; }
}