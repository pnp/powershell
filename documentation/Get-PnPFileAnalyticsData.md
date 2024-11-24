---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFileAnalyticsData.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFileAnalyticsData
---

# Get-PnPFileAnalyticsData

## SYNOPSIS

Retrieves analytics data for a file.

## SYNTAX

### Return analytics data

```
Get-PnPFileAnalyticsData -Url <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Retrieves file analytics data within a specific date range.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPFileAnalyticsData -Url "/sites/project/Shared Documents/Document.docx"
```

Retrieves all available analytics data for the specified file.

### EXAMPLE 2

```powershell
Get-PnPFileAnalyticsData -Url "/sites/project/Shared Documents/Document.docx" -LastSevenDays
```

Retrieves analytics data for the last seven days of the specified file.

### EXAMPLE 3

```powershell
Get-PnPFileAnalyticsData -Url "/sites/project/Shared Documents/Document.docx" -StartDate (Get-date).AddDays(-15) -EndDate (Get-date) -AnalyticsAggregationInterval Day
```

Retrieves analytics data for the last 15 days of the specified file with aggregation interval as days.

## PARAMETERS

### -All

When specified, it will retrieve all analytics data.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All analytics data
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AnalyticsAggregationInterval

When specified, it will retrieve analytics data with specified aggregation interval. Default is day.
Allowed values are `Day`,`Week` and `Month`.

```yaml
Type: DateTime
DefaultValue: Day
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Analytics by date range
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

### -EndDate

When specified, it will retrieve analytics data ending with specified end date. Should be used along with StartDate parameter

```yaml
Type: DateTime
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Analytics by date range
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LastSevenDays

When specified, it will retrieve analytics data for the last seven days.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Analytics by specific intervals
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -StartDate

When specified, it will retrieve analytics data starting from the specified start date.

```yaml
Type: DateTime
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Analytics by date range
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Url

The URL (server or site relative) to the file

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- ServerRelativeUrl
- SiteRelativeUrl
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
