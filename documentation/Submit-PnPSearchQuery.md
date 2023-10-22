---
Module Name: PnP.PowerShell
title: Submit-PnPSearchQuery
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Submit-PnPSearchQuery.html
---
 
# Submit-PnPSearchQuery

## SYNOPSIS
Executes an arbitrary search query against the SharePoint search index

## SYNTAX

### Limit (Default)
```powershell
Submit-PnPSearchQuery [-Query] <String> [-StartRow <Int32>] [-MaxResults <Int32>] [-TrimDuplicates <Boolean>]
 [-Properties <Hashtable>] [-Refiners <String>] [-Culture <Int32>] [-QueryTemplate <String>]
 [-SelectProperties <String[]>] [-RefinementFilters <String[]>] [-SortList <Hashtable>]
 [-RankingModelId <String>] [-ClientType <String>] [-CollapseSpecification <String>]
 [-HiddenConstraints <String>] [-TimeZoneId <Int32>] [-EnablePhonetic <Boolean>] [-EnableStemming <Boolean>]
 [-EnableQueryRules <Boolean>] [-SourceId <Guid>] [-ProcessBestBets <Boolean>]
 [-ProcessPersonalFavorites <Boolean>] [-RelevantResults] [-Connection <PnPConnection>]
 
```

### All
```powershell
Submit-PnPSearchQuery [-Query] <String> [-All] [-TrimDuplicates <Boolean>] [-Properties <Hashtable>]
 [-Refiners <String>] [-Culture <Int32>] [-QueryTemplate <String>] [-SelectProperties <String[]>]
 [-RefinementFilters <String[]>] [-SortList <Hashtable>] [-RankingModelId <String>] [-ClientType <String>]
 [-CollapseSpecification <String>] [-HiddenConstraints <String>] [-TimeZoneId <Int32>]
 [-EnablePhonetic <Boolean>] [-EnableStemming <Boolean>] [-EnableQueryRules <Boolean>] [-SourceId <Guid>]
 [-ProcessBestBets <Boolean>] [-ProcessPersonalFavorites <Boolean>] [-RelevantResults] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to run an arbitrary search query against the SharePoint search index.

## EXAMPLES

### EXAMPLE 1
```powershell
Submit-PnPSearchQuery -Query "finance"
```

Returns the top 500 items with the term finance

### EXAMPLE 2
```powershell
Submit-PnPSearchQuery -Query "Title:Intranet*" -MaxResults 10
```

Returns the top 10 items indexed by SharePoint Search of which the title starts with the word Intranet

### EXAMPLE 3
```powershell
Submit-PnPSearchQuery -Query "Title:Intranet*" -All
```

Returns absolutely all items indexed by SharePoint Search of which the title starts with the word Intranet

### EXAMPLE 4
```powershell
Submit-PnPSearchQuery -Query "Title:Intranet*" -Refiners "contentclass,FileType(filter=6/0/*)"
```

Returns absolutely all items indexed by SharePoint Search of which the title starts with the word Intranet, and return refiners for contentclass and FileType managed properties

### EXAMPLE 5
```powershell
Submit-PnPSearchQuery -Query "contentclass:STS_ListItem_DocumentLibrary" -SelectProperties ComplianceTag,InformationProtectionLabelId -All
```

Returns absolutely all items indexed by SharePoint Search which represent a document in a document library and instructs explicitly to return the managed properties InformationProtectionLabelId and ComplianceTag which will give insight into the sensitivity and retention labels assigned to the documents

### EXAMPLE 6
```powershell
Submit-PnPSearchQuery -Query "contentclass:STS_ListItem_DocumentLibrary" -SortList @{"filename" = "ascending"} -All
```

Returns absolutely all items indexed by SharePoint Search which represent a document in a document library and sorts the items by file name in ascending order

## PARAMETERS

### -All
Automatically page results until the end to get more than 500. Use with caution!

```yaml
Type: SwitchParameter
Parameter Sets: All

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientType
Specifies the name of the client which issued the query.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CollapseSpecification
Limit the number of items per the collapse specification. See https://learn.microsoft.com/sharepoint/dev/general-development/customizing-search-results-in-sharepoint#collapse-similar-search-results-using-the-collapsespecification-property for more information.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Culture
The locale for the query.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnablePhonetic
Specifies whether the phonetic forms of the query terms are used to find matches.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableQueryRules
Specifies whether Query Rules are enabled for this query.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableStemming
Specifies whether stemming is enabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HiddenConstraints
The keyword query's hidden constraints.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxResults
Maximum amount of search results to return. Default and max per page is 500 search results.

```yaml
Type: Int32
Parameter Sets: Limit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProcessBestBets
Determines whether Best Bets are enabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProcessPersonalFavorites
Determines whether personal favorites data is processed or not.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Properties
Extra query properties. Can for example be used for Office Graph queries.

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Query
Search query in Keyword Query Language (KQL).

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -QueryTemplate
Specifies the query template that is used at run time to transform the query based on user input.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RankingModelId
The identifier (ID) of the ranking model to use for the query.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RefinementFilters
The set of refinement filters used, separated by a comma.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Refiners
The list of refiners to be returned in a search result, separated by a comma. I.e. contentclass,ContentType(filter=7/0/*).

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelevantResults
Specifies whether only relevant results are returned.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SelectProperties
The list of properties to return in the search results, separated by a comma. I.e. ComplianceTag,InformationProtectionLabelId.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SortList
The list of properties by which the search results are ordered as a hashtable, i.e. each property needs to be a key and the associated value either "Ascending" or "Descending" based on the wanted sort order, or "FQLFormula" if you want to use a formula to define the sort order.

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceId
Specifies the identifier (ID or name) of the result source to be used to run the query.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartRow
Search result item to start returning the results from. Useful for paging. Leave at 0 to return all results.

```yaml
Type: Int32
Parameter Sets: Limit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TimeZoneId
The identifier for the search query time zone.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrimDuplicates
Specifies whether near duplicate items should be removed from the search results.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

