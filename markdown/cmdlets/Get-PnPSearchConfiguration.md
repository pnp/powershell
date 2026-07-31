---
Module Name: PnP.PowerShell
title: Get-PnPSearchConfiguration
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchConfiguration.html
---

# Get-PnPSearchConfiguration

## SYNOPSIS

Returns the search configuration

## SYNTAX

### Xml (Default)

```powershell
Get-PnPSearchConfiguration [-Scope <SearchConfigurationScope>] [-Path <String>]
 [-Connection <PnPConnection>] 
```

### OutputFormat

```powershell
Get-PnPSearchConfiguration [-Scope <SearchConfigurationScope>] [-OutputFormat <OutputFormat>]
 [-Connection <PnPConnection>] 
```

### BookmarksCSV

```powershell
Get-PnPSearchConfiguration [-Scope <SearchConfigurationScope>] [-PromotedResultsToBookmarkCSV] [-ExcludeVisualPromotedResults <Boolean>] [-BookmarkStatus <BookmarkStatus>] [-Path <String>]
 [-Connection <PnPConnection>] 
```

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

### -OutputFormat

Output format for of the configuration. Defaults to complete XML

```yaml
Type: OutputFormat
Parameter Sets: OutputFormat
Accepted values: CompleteXml, ManagedPropertyMappings

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path

Local path where the search configuration will be saved

```yaml
Type: String
Parameter Sets: Xml

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope

Scope to use. Either Web, Site, or Subscription. Defaults to Web

```yaml
Type: SearchConfigurationScope
Parameter Sets: (All)
Accepted values: Web, Site, Subscription

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PromotedResultsToBookmarkCSV

Output promoted results to a compatible CSV file to be used as Bookmark import at https://admin.microsoft.com/#/MicrosoftSearch/bookmarks.

Export details:

- Promoted results marked as "Render the URL as a banner instead of as a hyperlink" (visual promoted results) and query rules with no triggers will be skipped by default.
- Triggers set to "Advanced Query Text Match" and "Query Contains Action Term" will have "Match Similar Keywords" set to true for the Bookmark.
- Multiple triggers on a query rule will be merged into a single trigger.

```yaml
Type: SwitchParameter
Parameter Sets: CSV

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeVisualPromotedResults

Exclude promoted results marked as "Render the URL as a banner instead of as a hyperlink". Defaults to true.

```yaml
Type: Boolean
Parameter Sets: CSV

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BookmarkStatus
Output bookmarks to be in suggested or published status upon CSV import. Defaults to suggested status.

```yaml
Type: BookmarkStatus
Parameter Sets: CSV
Accepted values: Suggested, Published

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
