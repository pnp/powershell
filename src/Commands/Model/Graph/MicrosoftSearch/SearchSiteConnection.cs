using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Wrapper for the GCS site connections list response
/// </summary>
public class SearchSiteConnectionCollection
{
	[JsonPropertyName("connections")]
	public List<SearchSiteConnection> Connections { get; set; }
}

/// <summary>
/// Defines a site connection (external connector) as returned by the GCS API
/// </summary>
public class SearchSiteConnection
{
	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("description")]
	public string Description { get; set; }

	[JsonPropertyName("contentSourceDisplayName")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string ContentSourceDisplayName { get; set; }

	[JsonPropertyName("connectorType")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string ConnectorType { get; set; }

	[JsonPropertyName("lastModifiedDate")]
	public long LastModifiedDate { get; set; }

	[JsonPropertyName("lastModifiedByObjectId")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string LastModifiedByObjectId { get; set; }

	[JsonPropertyName("editState")]
	public int EditState { get; set; }

	[JsonPropertyName("enabledContentExperiences")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string> EnabledContentExperiences { get; set; }

	[JsonPropertyName("gcsClusterName")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string GcsClusterName { get; set; }

	[JsonPropertyName("isExpressConnection")]
	public bool IsExpressConnection { get; set; }

	[JsonPropertyName("itemType")]
	public int ItemType { get; set; }

	/// <summary>
	/// System ID used as the systemId in result type contentSourceId when contentSourceApplication is "Connectors"
	/// </summary>
	[JsonPropertyName("systemId")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string SystemId { get; set; }
}

/// <summary>
/// Wrapper for the GCS site connection properties response
/// </summary>
public class SearchSiteConnectionProperties
{
	[JsonPropertyName("properties")]
	public List<SearchSiteConnectionProperty> Properties { get; set; }
}

/// <summary>
/// Defines a property in a connector's schema as returned by the GCS properties endpoint
/// </summary>
public class SearchSiteConnectionProperty
{
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("dataType")]
	public string DataType { get; set; }

	[JsonPropertyName("retrievable")]
	public bool Retrievable { get; set; }

	[JsonPropertyName("queryable")]
	public bool Queryable { get; set; }

	[JsonPropertyName("searchable")]
	public bool Searchable { get; set; }

	[JsonPropertyName("refinable")]
	public bool Refinable { get; set; }

	[JsonPropertyName("alias")]
	public List<string> Alias { get; set; }
}
