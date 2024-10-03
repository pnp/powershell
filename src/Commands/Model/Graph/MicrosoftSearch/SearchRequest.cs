using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines the body for a single search request to Microsoft Graph
/// </summary>
public class SearchRequest
{
    /// <summary>
    /// The types of entities to query, i.e. externalItem
    /// </summary>
    [JsonPropertyName("entityTypes")]
    public List<string> EntityTypes { get; set; }

    /// <summary>
    /// The names of the content sources to query, i.e. /external/connections/<externalconnectionname>
    /// </summary>
    [JsonPropertyName("contentSources")]
    public List<string> ContentSources { get; set; }

    /// <summary>
    /// The search query to execute
    /// </summary>
    [JsonPropertyName("query")]
    public SearchRequestQuery Query { get; set; }
}