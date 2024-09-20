using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines the configuration of an external connection used by Microsoft Search
/// </summary>
/// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-configuration?view=graph-rest-1.0" />
public class ExternalConnectionConfiguration
{
    /// <summary>
    /// A collection of application IDs for registered Microsoft Entra apps that are allowed to manage the externalConnection and to index content in the externalConnection
    /// </summary>
    [JsonPropertyName("authorizedAppIds")]
    public string[] AuthorizedAppIds { get; set; }
}