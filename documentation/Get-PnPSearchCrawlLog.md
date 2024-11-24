---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchCrawlLog.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSearchCrawlLog
---

# Get-PnPSearchCrawlLog

## SYNOPSIS

Returns entries from the SharePoint search crawl log. Make sure you are granted access to the crawl log via the SharePoint search admin center at https://<tenant>-admin.sharepoint.com/_layouts/15/searchadmin/crawllogreadpermission.aspx in order to run this cmdlet.

## SYNTAX

### Default (Default)

```
Get-PnPSearchCrawlLog [-LogLevel <LogLevel>] [-RowLimit <Int32>] [-Filter <String>]
 [-ContentSource <ContentSource>] [-StartDate <DateTime>] [-EndDate <DateTime>] [-RawFormat]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve entries from the SharePoint search crawl log. To be able to use this command you need to grant access to the crawl log via the SharePoint search admin center at https://<tenant>-admin.sharepoint.com/_layouts/15/searchadmin/crawllogreadpermission.aspx.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSearchCrawlLog
```

Returns the last 100 crawl log entries for site content.

### EXAMPLE 2

```powershell
Get-PnPSearchCrawlLog -Filter "https://contoso-my.sharepoint.com/personal"
```

Returns the last 100 crawl log entries for OneDrive content.

### EXAMPLE 3

```powershell
Get-PnPSearchCrawlLog -ContentSource UserProfiles
```

Returns the last 100 crawl log entries for user profiles.

### EXAMPLE 4

```powershell
Get-PnPSearchCrawlLog -ContentSource UserProfiles -Filter "mikael"
```

Returns the last 100 crawl log entries for user profiles with the term "mikael" in the user principal name.

### EXAMPLE 5

```powershell
Get-PnPSearchCrawlLog -ContentSource Sites -LogLevel Error -RowLimit 10
```

Returns the last 10 crawl log entries with a state of Error for site content.

### EXAMPLE 6

```powershell
Get-PnPSearchCrawlLog -EndDate (Get-Date).AddDays(-100)
```

Returns the last 100 crawl log entries up until 100 days ago.

### EXAMPLE 7

```powershell
Get-PnPSearchCrawlLog -RowFilter 3 -RawFormat
```

Returns the last 3 crawl log entries showing the raw crawl log data.

## PARAMETERS

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

### -ContentSource

Content to retrieve (Sites, User Profiles). Defaults to Sites.

```yaml
Type: ContentSource
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
- Sites
- UserProfiles
HelpMessage: ''
```

### -EndDate

End date to stop getting entries from. Default to current time.

```yaml
Type: DateTime
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

### -Filter

Filter to limit what is being returned. Has to be a URL prefix for SharePoint content, and part of a user principal name for user profiles. Wildcard characters are not supported.

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

### -LogLevel

Filter what log entries to return (All, Success, Warning, Error). Defaults to All

```yaml
Type: LogLevel
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
- Success
- Warning
- Error
- All
HelpMessage: ''
```

### -RawFormat

Show raw crawl log data

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

### -RowLimit

Number of entries to return. Defaults to 100.

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

### -StartDate

Start date to start getting entries from. Defaults to start of time.

```yaml
Type: DateTime
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
