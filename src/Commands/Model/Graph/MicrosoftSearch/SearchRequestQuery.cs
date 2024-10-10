using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines the query for  request to Microsoft Graph
/// </summary>
public class SearchRequestQuery
{
    /// <summary>
    /// The search query to execute
    /// </summary>
    [JsonPropertyName("queryString")]
    public string QueryString { get; set; }
}