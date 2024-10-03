using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines the body for a collection of search requests to Microsoft Graph
/// </summary>
public class SearchRequests
{
    /// <summary>
    /// Collection of search request items
    /// </summary>
    [JsonPropertyName("requests")]
    public List<SearchRequest> Requests { get; set; }
}