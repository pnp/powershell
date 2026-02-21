using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Wrapper for the GCS result types list response
/// </summary>
public class SearchResultTypeCollection
{
	[JsonPropertyName("modernResultTypes")]
	public List<SearchResultType> ResultTypes { get; set; }
}

/// <summary>
/// Defines a Microsoft Search result type as returned by the GCS API
/// </summary>
public class SearchResultType
{
	[JsonPropertyName("path")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string Path { get; set; }

	[JsonPropertyName("logicalId")]
	public string LogicalId { get; set; }

	[JsonPropertyName("lastModifiedDateTime")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string LastModifiedDateTime { get; set; }

	[JsonPropertyName("payload")]
	public SearchResultTypePayload Payload { get; set; }
}

/// <summary>
/// The configuration payload of a search result type
/// </summary>
public class SearchResultTypePayload
{
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("priority")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public int Priority { get; set; }

	[JsonPropertyName("isActive")]
	public bool IsActive { get; set; }

	[JsonPropertyName("contentSourceId")]
	public SearchResultTypeContentSource ContentSourceId { get; set; }

	[JsonPropertyName("contentSourceName")]
	public string ContentSourceName { get; set; }

	[JsonPropertyName("rules")]
	public List<SearchResultTypeRule> Rules { get; set; }

	[JsonPropertyName("ruleProperties")]
	public List<string> RuleProperties { get; set; }

	[JsonPropertyName("displayTemplate")]
	public string DisplayTemplate { get; set; }

	[JsonPropertyName("displayProperties")]
	public List<string> DisplayProperties { get; set; }

	[JsonPropertyName("displaySampleData")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string DisplaySampleData { get; set; }

	[JsonPropertyName("lastModifiedBy")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string LastModifiedBy { get; set; }

	/// <summary>
	/// Captures additional properties not explicitly modeled, preserving them during GET-then-PUT round-trips
	/// </summary>
	[JsonExtensionData]
	public IDictionary<string, JsonElement> AdditionalData { get; set; }

	public override string ToString()
	{
		var state = IsActive ? "Active" : "Inactive";
		return $"{Name} ({state})";
	}
}

/// <summary>
/// Identifies the content source for a result type
/// </summary>
public class SearchResultTypeContentSource
{
	/// <summary>
	/// "SharePoint" for SharePoint content, "Connectors" for external connectors
	/// </summary>
	[JsonPropertyName("contentSourceApplication")]
	public string ContentSourceApplication { get; set; }

	/// <summary>
	/// "SharePoint" for SharePoint, or the connector ID (e.g. "techcrunch") for external
	/// </summary>
	[JsonPropertyName("identity")]
	public string Identity { get; set; }

	/// <summary>
	/// "SharePoint" for SharePoint, or the connector's systemId from Get-PnPSearchSiteConnection for external
	/// </summary>
	[JsonPropertyName("systemId")]
	public string SystemId { get; set; }
}

/// <summary>
/// Defines a rule condition for matching content to a result type.
/// Rules use abbreviated property names: PN = Property Name, PO = Property Operator, PV = Property Values.
/// </summary>
public class SearchResultTypeRule
{
	/// <summary>
	/// Property name to match against (e.g. "FileType", "IsListItem", "AADObjectID")
	/// </summary>
	[JsonPropertyName("PN")]
	public string PropertyName { get; set; }

	/// <summary>
	/// The comparison operator
	/// </summary>
	[JsonPropertyName("PO")]
	public SearchResultTypeRuleOperator Operator { get; set; }

	/// <summary>
	/// Values to compare against
	/// </summary>
	[JsonPropertyName("PV")]
	public List<string> Values { get; set; }
}

/// <summary>
/// Defines the operator for a result type rule.
/// N values: 1=Equals, 2=NotEquals, 3=Contains, 4=DoesNotContain, 5=LessThan, 6=GreaterThan, 7=StartsWith
/// </summary>
public class SearchResultTypeRuleOperator
{
	/// <summary>
	/// Operator code: 1=Equals, 2=NotEquals, 3=Contains, 4=DoesNotContain, 5=LessThan, 6=GreaterThan, 7=StartsWith
	/// </summary>
	[JsonPropertyName("N")]
	public int N { get; set; }

	[JsonPropertyName("JBO")]
	public bool JBO { get; set; }
}
