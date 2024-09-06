using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines a schema for a connection to an external datasource used by Microsoft Search
/// </summary>
/// <seealso cref="https://learn.microsoft.com/graph/api/externalconnectors-externalconnection-patch-schema?view=graph-rest-1.0&tabs=http" />
public class ExternalSchema
{
    /// <summary>
    /// Schema base type. Do not modify.
    /// </summary>
    [JsonPropertyName("baseType")]
    public string BaseType { get; } = "microsoft.graph.externalItem";

    /// <summary>
    /// Properties in the schema of the external connection defining which information is available on the external items
    /// </summary>
    [JsonPropertyName("properties")]
    public List<ExternalSchemaProperty> Properties { get; set; }
}