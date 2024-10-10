using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines the resources within a search result from Microsoft Graph
/// </summary>
/// <seealso cref="https://learn.microsoft.com/en-us/graph/api/search-query?view=graph-rest-1.0&tabs=http" />
public class SearchResultResource
{
    /// <summary>
    /// The properties of the search result
    /// </summary>
    [JsonPropertyName("properties")]
    public Dictionary<string, object> Properties { get; set; }
}