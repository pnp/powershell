using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines an external item used by Microsoft Search
/// </summary>
/// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-externalitem?#properties/>
public class ExternalItem
{
    /// <summary>
    /// Unique identifier of the external item within the connection
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// The managed properties to provide to Microsoft Search about this external item
    /// </summary>
    [JsonConverter(typeof(JsonConverters.MicrosoftSearchExternalItemPropertyConverter))]
    [JsonPropertyName("properties")]
    public Hashtable Properties { get; set; }

    /// <summary>
    /// The ACLs to assign to set permissions on this external item
    /// </summary>
    [JsonPropertyName("acl")]
    public List<ExternalItemAcl> Acls { get; set; }

    /// <summary>
    /// The content of the external item
    /// </summary>
    [JsonPropertyName("content")]
    public ExternalItemContent Content { get; set; }
}