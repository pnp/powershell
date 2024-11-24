---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Submit-PnPSearchQuery.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Submit-PnPSearchQuery
---

# Submit-PnPSearchQuery

## SYNOPSIS

Executes an arbitrary search query against the SharePoint search index.

**Required Permissions**

|        Type     |                    API/ Permission Name                    |                    Admin consent required                    |
| --------------- | --------------------------------------- | -------- |
| Application     | sharepoint/Sites.Read.All, sharepoint/Sites.ReadWrite.All, sharepoint/Sites.Manage.All or sharepoint/Sites.FullControl.All | yes                               |
| Delegated       | sharepoint/Sites.Search.All | yes                               |

## SYNTAX

### Limit (Default)

```
Submit-PnPSearchQuery [-Query] <String> [-StartRow <Int32>] [-MaxResults <Int32>]
 [-TrimDuplicates <Boolean>] [-Properties <Hashtable>] [-Refiners <String>] [-Culture <Int32>]
 [-QueryTemplate <String>] [-SelectProperties <String[]>] [-RefinementFilters <String[]>]
 [-SortList <Hashtable>] [-RankingModelId <String>] [-ClientType <String>]
 [-CollapseSpecification <String>] [-HiddenConstraints <String>] [-TimeZoneId <Int32>]
 [-EnablePhonetic <Boolean>] [-EnableStemming <Boolean>] [-EnableQueryRules <Boolean>]
 [-SourceId <Guid>] [-ProcessBestBets <Boolean>] [-ProcessPersonalFavorites <Boolean>]
 [-RelevantResults] [-Connection <PnPConnection>] [-RetryCount <Int32>] [-Verbose]
 [<CommonParameters>]
```

### All

```
Submit-PnPSearchQuery [-Query] <String> [-All] [-TrimDuplicates <Boolean>] [-Properties <Hashtable>]
 [-Refiners <String>] [-Culture <Int32>] [-QueryTemplate <String>] [-SelectProperties <String[]>]
 [-RefinementFilters <String[]>] [-SortList <Hashtable>] [-RankingModelId <String>]
 [-ClientType <String>] [-CollapseSpecification <String>] [-HiddenConstraints <String>]
 [-TimeZoneId <Int32>] [-EnablePhonetic <Boolean>] [-EnableStemming <Boolean>]
 [-EnableQueryRules <Boolean>] [-SourceId <Guid>] [-ProcessBestBets <Boolean>]
 [-ProcessPersonalFavorites <Boolean>] [-RelevantResults] [-Connection <PnPConnection>]
 [-RetryCount <Int32>] [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to run an arbitrary search query against the SharePoint search index.

## EXAMPLES

### EXAMPLE 1

```powershell
Submit-PnPSearchQuery -Query "finance"
```

Returns the top 500 items with the term finance.

### EXAMPLE 2

```powershell
Submit-PnPSearchQuery -Query "Title:Intranet*" -MaxResults 10
```

Returns the top 10 items indexed by SharePoint Search of which the title starts with the word Intranet.

### EXAMPLE 3

```powershell
Submit-PnPSearchQuery -Query "Title:Intranet*" -All
```

Returns absolutely all items indexed by SharePoint Search of which the title starts with the word Intranet.

### EXAMPLE 4

```powershell
Submit-PnPSearchQuery -Query "Title:Intranet*" -Refiners "contentclass,FileType(filter=6/0/*)"
```

Returns absolutely all items indexed by SharePoint Search of which the title starts with the word Intranet, and return refiners for contentclass and FileType managed properties.

### EXAMPLE 5

```powershell
Submit-PnPSearchQuery -Query "contentclass:STS_ListItem_DocumentLibrary" -SelectProperties ComplianceTag,InformationProtectionLabelId -All
```

Returns absolutely all items indexed by SharePoint Search which represent a document in a document library and instructs explicitly to return the managed properties InformationProtectionLabelId and ComplianceTag which will give insight into the sensitivity and retention labels assigned to the documents.

### EXAMPLE 6

```powershell
Submit-PnPSearchQuery -Query "contentclass:STS_ListItem_DocumentLibrary" -SortList @{"filename" = "ascending"} -All
```

Returns absolutely all items indexed by SharePoint Search which represent a document in a document library and sorts the items by file name in ascending order.

## PARAMETERS

### -All

Automatically page results until the end to get more than 500. Use with caution!

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ClientType

Specifies the name of the client which issued the query.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CollapseSpecification

Limit the number of items per the collapse specification. See https://learn.microsoft.com/sharepoint/dev/general-development/customizing-search-results-in-sharepoint#collapse-similar-search-results-using-the-collapsespecification-property for more information.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Culture

The locale for the query. Uses LCID's as per this [documentation](https://learn.microsoft.com/previous-versions/office/sharepoint-csom/jj167546(v=office.15)).

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EnablePhonetic

Specifies whether the phonetic forms of the query terms are used to find matches.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EnableQueryRules

Specifies whether Query Rules are enabled for this query.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EnableStemming

Specifies whether stemming is enabled.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -HiddenConstraints

The keyword query's hidden constraints.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MaxResults

Maximum amount of search results to return. Default and max per page is 500 search results.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Limit
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ProcessBestBets

Determines whether Best Bets are enabled.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ProcessPersonalFavorites

Determines whether personal favorites data is processed or not.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Properties

Extra query properties. Can for example be used for Office Graph queries.

```yaml
Type: Hashtable
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Query

Search query in Keyword Query Language (KQL).

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -QueryTemplate

Specifies the query template that is used at run time to transform the query based on user input.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RankingModelId

The identifier (ID) of the ranking model to use for the query.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RefinementFilters

The set of refinement filters used, separated by a comma.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Refiners

The list of refiners to be returned in a search result, separated by a comma. I.e. contentclass,ContentType(filter=7/0/*).

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RelevantResults

Specifies whether only relevant results are returned.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RetryCount

How many times to retry for a failed query. Default is 0 (no retries). Will wait 5 seconds between each retry.

```yaml
Type: Int32
DefaultValue: 0
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SelectProperties

The list of properties to return in the search results, separated by a comma. I.e. ComplianceTag,InformationProtectionLabelId.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SortList

The list of properties by which the search results are ordered as a hashtable, i.e. each property needs to be a key and the associated value either "Ascending" or "Descending" based on the wanted sort order, or "FQLFormula" if you want to use a formula to define the sort order.

```yaml
Type: Hashtable
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SourceId

Specifies the identifier (ID or name) of the result source to be used to run the query.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -StartRow

Search result item to start returning the results from. Useful for paging. Leave at 0 to return all results.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Limit
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TimeZoneId

The identifier for the search query time zone.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TrimDuplicates

Specifies whether near duplicate items should be removed from the search results.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
