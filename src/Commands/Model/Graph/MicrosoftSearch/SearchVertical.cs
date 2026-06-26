using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Wrapper for the GCS verticals list response
/// </summary>
public class SearchVerticalCollection
{
	[JsonPropertyName("verticals")]
	public List<SearchVertical> Verticals { get; set; }
}

/// <summary>
/// Defines a Microsoft Search vertical as returned by the GCS API
/// </summary>
public class SearchVertical
{
	/// <summary>
	/// Site path in the format :GUID:
	/// </summary>
	[JsonPropertyName("path")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string Path { get; set; }

	/// <summary>
	/// Unique identifier of the vertical (e.g. SITEALL, SITEFILES, or a generated ID)
	/// </summary>
	[JsonPropertyName("logicalId")]
	public string LogicalId { get; set; }

	/// <summary>
	/// Last modified date and time
	/// </summary>
	[JsonPropertyName("lastModifiedDateTime")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string LastModifiedDateTime { get; set; }

	/// <summary>
	/// The vertical configuration payload
	/// </summary>
	[JsonPropertyName("payload")]
	public SearchVerticalPayload Payload { get; set; }
}

/// <summary>
/// The configuration payload of a search vertical
/// </summary>
public class SearchVerticalPayload
{
	[JsonPropertyName("displayName")]
	public string DisplayName { get; set; }

	/// <summary>
	/// State of the vertical: 0 = disabled, 1 = enabled
	/// </summary>
	[JsonPropertyName("state")]
	public int State { get; set; }

	/// <summary>
	/// Type of vertical: 0 = built-in (OOB), 1 = custom
	/// </summary>
	[JsonPropertyName("verticalType")]
	public int VerticalType { get; set; }

	[JsonPropertyName("queryTemplate")]
	public string QueryTemplate { get; set; }

	[JsonPropertyName("lastModifiedBy")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string LastModifiedBy { get; set; }

	[JsonPropertyName("extendedQueryTemplate")]
	public string ExtendedQueryTemplate { get; set; }

	/// <summary>
	/// Template type: All, File, Sites, News, Images, Custom
	/// </summary>
	[JsonPropertyName("templateType")]
	public string TemplateType { get; set; }

	[JsonPropertyName("entities")]
	public List<SearchVerticalEntity> Entities { get; set; }

	[JsonPropertyName("refiners")]
	public List<SearchVerticalRefiner> Refiners { get; set; }

	[JsonPropertyName("scope")]
	public int Scope { get; set; }

	[JsonPropertyName("allowedActions")]
	public int AllowedActions { get; set; }

	[JsonPropertyName("includeConnectorResults")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? IncludeConnectorResults { get; set; }

	/// <summary>
	/// Captures additional properties not explicitly modeled, preserving them during GET-then-PUT round-trips
	/// </summary>
	[JsonExtensionData]
	public IDictionary<string, JsonElement> AdditionalData { get; set; }

	public override string ToString()
	{
		var state = State == 1 ? "Enabled" : "Disabled";
		return $"{DisplayName} ({state})";
	}
}

/// <summary>
/// Defines an entity (content source group) within a search vertical
/// </summary>
public class SearchVerticalEntity
{
	[JsonPropertyName("contentSources")]
	public List<SearchVerticalContentSource> ContentSources { get; set; }

	/// <summary>
	/// Entity type: File, External, Image
	/// </summary>
	[JsonPropertyName("entityType")]
	public string EntityType { get; set; }

	[JsonPropertyName("refinerIds")]
	public List<string> RefinerIds { get; set; }
}

/// <summary>
/// Defines a content source within a vertical entity
/// </summary>
public class SearchVerticalContentSource
{
	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }
}

/// <summary>
/// Defines a refiner (filter) within a search vertical
/// </summary>
public class SearchVerticalRefiner
{
	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("displayName")]
	public string DisplayName { get; set; }

	[JsonPropertyName("lastModifiedTime")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string LastModifiedTime { get; set; }

	[JsonPropertyName("lastModifiedBy")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string LastModifiedBy { get; set; }

	[JsonPropertyName("state")]
	public int State { get; set; }

	[JsonPropertyName("category")]
	public int Category { get; set; }

	[JsonPropertyName("layout")]
	public SearchVerticalRefinerLayout Layout { get; set; }
}

/// <summary>
/// Defines the layout of a refiner
/// </summary>
public class SearchVerticalRefinerLayout
{
	[JsonPropertyName("mappedProperties")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string> MappedProperties { get; set; }

	[JsonPropertyName("fieldName")]
	public string FieldName { get; set; }

	[JsonPropertyName("type")]
	public int Type { get; set; }

	[JsonPropertyName("values")]
	public List<object> Values { get; set; }

	[JsonPropertyName("displayInterface")]
	public int DisplayInterface { get; set; }

	[JsonPropertyName("manualEntryEnabled")]
	public bool ManualEntryEnabled { get; set; }

	[JsonPropertyName("showCount")]
	public bool ShowCount { get; set; }
}
