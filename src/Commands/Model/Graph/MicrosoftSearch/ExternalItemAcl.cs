using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines an ACL for an external item used by Microsoft Search
/// </summary>
/// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-acl#properties"/>
public class ExternalItemAcl
{
    /// <summary>
    /// The type of the ACL, i.e. User, Group, Everyone, etc.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("type")]
    public Enums.SearchExternalItemAclType Type { get; set; }

    /// <summary>
    /// The value of the ACL, i.e. the user or group id
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; }

    /// <summary>
    /// The access type of the ACL, i.e. Grant or Deny
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("accessType")]
    public Enums.SearchExternalItemAclAccessType AccessType { get; set; }
}