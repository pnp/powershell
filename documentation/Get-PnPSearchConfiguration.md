---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchConfiguration.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSearchConfiguration
---

# Get-PnPSearchConfiguration

## SYNOPSIS

Returns the search configuration

## SYNTAX

### Xml (Default)

```
Get-PnPSearchConfiguration [-Scope <SearchConfigurationScope>] [-Path <String>]
 [-Connection <PnPConnection>]
```

### OutputFormat

```
Get-PnPSearchConfiguration [-Scope <SearchConfigurationScope>] [-OutputFormat <OutputFormat>]
 [-Connection <PnPConnection>]
```

### BookmarksCSV

```
Get-PnPSearchConfiguration [-Scope <SearchConfigurationScope>] [-PromotedResultsToBookmarkCSV]
 [-ExcludeVisualPromotedResults <Boolean>] [-BookmarkStatus <BookmarkStatus>] [-Path <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve search configuration.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSearchConfiguration
```

Returns the search configuration for the current web.

### EXAMPLE 2

```powershell
Get-PnPSearchConfiguration -Scope Site
```

Returns the search configuration for the current site collection.

### EXAMPLE 3

```powershell
Get-PnPSearchConfiguration -Scope Subscription
```

Returns the search configuration for the current tenant.

### EXAMPLE 4

```powershell
Get-PnPSearchConfiguration -Path searchconfig.xml -Scope Subscription
```

Returns the search configuration for the current tenant and saves it to the specified file.

### EXAMPLE 5

```powershell
Get-PnPSearchConfiguration -Scope Site -OutputFormat ManagedPropertyMappings
```

Returns all custom managed properties and crawled property mapping at the current site collection.

### EXAMPLE 6

```powershell
Get-PnPSearchConfiguration -Scope Site -PromotedResultsToBookmarkCSV -Path bookmarks.csv
```

Export promoted results excluding visual ones from query rules on the site collection as a CSV file with the bookmarks in suggested status.

### EXAMPLE 7

```powershell
Get-PnPSearchConfiguration -Scope Site -PromotedResultsToBookmarkCSV -Path bookmarks.csv -BookmarkStatus Published
```

Export promoted results excluding visual from query rules on the site collection as a CSV file with the bookmarks in published status.

### EXAMPLE 8

```powershell
Get-PnPSearchConfiguration -Scope Subscription -PromotedResultsToBookmarkCSV -ExcludeVisualPromotedResults $false
```

Export promoted results including visual ones from query rules on the tenant in CSV format with the bookmarks in suggested status.

## PARAMETERS

### -BookmarkStatus

Output bookmarks to be in suggested or published status upon CSV import. Defaults to suggested status.

```yaml
Type: BookmarkStatus
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: CSV
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Suggested
- Published
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

### -ExcludeVisualPromotedResults

Exclude promoted results marked as "Render the URL as a banner instead of as a hyperlink". Defaults to true.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: CSV
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OutputFormat

Output format for of the configuration. Defaults to complete XML

```yaml
Type: OutputFormat
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: OutputFormat
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- CompleteXml
- ManagedPropertyMappings
HelpMessage: ''
```

### -Path

Local path where the search configuration will be saved

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Xml
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -PromotedResultsToBookmarkCSV

Output promoted results to a compatible CSV file to be used as Bookmark import at https://admin.microsoft.com/#/MicrosoftSearch/bookmarks.

Export details:

- Promoted results marked as "Render the URL as a banner instead of as a hyperlink" (visual promoted results) and query rules with no triggers will be skipped by default.
- Triggers set to "Advanced Query Text Match" and "Query Contains Action Term" will have "Match Similar Keywords" set to true for the Bookmark.
- Multiple triggers on a query rule will be merged into a single trigger.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: CSV
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Scope

Scope to use. Either Web, Site, or Subscription. Defaults to Web

```yaml
Type: SearchConfigurationScope
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
AcceptedValues:
- Web
- Site
- Subscription
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
