using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch;

/// <summary>
/// Defines a schema property for a connection to an external datasource used by Microsoft Search
/// </summary>
/// <seealso cref="https://learn.microsoft.com/graph/api/resources/externalconnectors-property?view=graph-rest-1.0" />
public class ExternalSchemaProperty
{
    /// <summary>
    /// Name of the schema property
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// A set of aliases or a friendly name for the property. Maximum 32 characters. Only alphanumeric characters allowed. For example, each string may not contain control characters, whitespace, or any of the following: :, ;, ,, (, ), [, ], {, }, %, $, +, !, *, =, &, ?, @, #, \, ~, ', ", <, >, `, ^. Optional.
    /// </summary>
    [JsonPropertyName("aliases")]
    public string[] Aliases { get; set; }

    /// <summary>
    /// Specifies if the property is queryable. Queryable properties can be used in Keyword Query Language (KQL) queries. Optional.
    /// </summary>
    [JsonPropertyName("isQueryable")]
    public bool IsQueryable { get; set; }

    /// <summary>
    /// Specifies if the property is refinable. Refinable properties can be used to filter search results in the Search API and add a refiner control in the Microsoft Search user experience. Optional.
    /// </summary>
    [JsonPropertyName("isRefinable")]
    public bool IsRefinable { get; set; }

    /// <summary>
    /// Specifies if the property is retrievable. Retrievable properties are returned in the result set when items are returned by the search API. Retrievable properties are also available to add to the display template used to render search results. Optional.
    /// </summary>
    [JsonPropertyName("isRetrievable")]
    public bool IsRetrievable { get; set; }

    /// <summary>
    /// Specifies if the property is searchable. Only properties of type String or StringCollection can be searchable. Nonsearchable properties aren't added to the search index. Optional.
    /// </summary>
    [JsonPropertyName("isSearchable")]
    public bool IsSearchable { get; set; }

    /// <summary>
    /// Specifies the type of information stored in the property. Required.
    /// </summary>
    [JsonPropertyName("type")]
    public Enums.SearchExternalSchemaPropertyType? Type { get; set; }

    /// <summary>
    /// Specifies the labels to tag the property with to make Microsoft Search better understand the data. Optional.
    /// </summary>
    public List<Enums.SearchExternalSchemaPropertyLabel> Labels { get; set; }
}