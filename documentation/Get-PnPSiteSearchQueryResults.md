---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteSearchQueryResults.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteSearchQueryResults
---

# Get-PnPSiteSearchQueryResults

## SYNOPSIS

Executes a search query to retrieve indexed site collections

## SYNTAX

### Limit (Default)

```
Get-PnPSiteSearchQueryResults [[-Query] <String>] [-StartRow <Int32>] [-MaxResults <Int32>]
 [-Connection <PnPConnection>]
```

### All

```
Get-PnPSiteSearchQueryResults [[-Query] <String>] [-All] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to run a search query to retrieve indexed site collections.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteSearchQueryResults
```

Returns the top 500 site collections indexed by SharePoint Search

### EXAMPLE 2

```powershell
Get-PnPSiteSearchQueryResults -Query "WebTemplate:STS"
```

Returns the top 500 site collections indexed by SharePoint Search which have are based on the STS (Team Site) template

### EXAMPLE 3

```powershell
Get-PnPSiteSearchQueryResults -Query "WebTemplate:SPSPERS"
```

Returns the top 500 site collections indexed by SharePoint Search which have are based on the SPSPERS (MySite) template, up to the MaxResult limit

### EXAMPLE 4

```powershell
Get-PnPSiteSearchQueryResults -Query "Title:Intranet*"
```

Returns the top 500 site collections indexed by SharePoint Search of which the title starts with the word Intranet

### EXAMPLE 5

```powershell
Get-PnPSiteSearchQueryResults -MaxResults 10
```

Returns the top 10 site collections indexed by SharePoint Search

### EXAMPLE 6

```powershell
Get-PnPSiteSearchQueryResults -All
```

Returns absolutely all site collections indexed by SharePoint Search

## PARAMETERS

### -All

Automatically page results until the end to get more than 500 sites. Use with caution!

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

### -MaxResults

Maximum amount of search results to return. Default and max is 500 search results.

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

### -Query

Search query in Keyword Query Language (KQL) to execute to refine the returned sites. If omitted, all indexed sites will be returned.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
