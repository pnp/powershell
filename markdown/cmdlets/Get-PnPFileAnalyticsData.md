---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFileAnalyticsData.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFileAnalyticsData
---
  
# Get-PnPFileAnalyticsData

## SYNOPSIS
Retrieves analytics data for a file.

## SYNTAX

### Return analytics data
```powershell
Get-PnPFileAnalyticsData -Url <String> [-Connection <PnPConnection>]
```

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

### -Url
The URL (server or site relative) to the file

```yaml
Type: String
Parameter Sets: (All)
Aliases: ServerRelativeUrl, SiteRelativeUrl

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -All
When specified, it will retrieve all analytics data.

```yaml
Type: SwitchParameter
Parameter Sets: All analytics data

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LastSevenDays
When specified, it will retrieve analytics data for the last seven days.

```yaml
Type: SwitchParameter
Parameter Sets: Analytics by specific intervals

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartDate
When specified, it will retrieve analytics data starting from the specified start date.

```yaml
Type: DateTime
Parameter Sets: Analytics by date range

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EndDate
When specified, it will retrieve analytics data ending with specified end date. Should be used along with StartDate parameter

```yaml
Type: DateTime
Parameter Sets: Analytics by date range

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AnalyticsAggregationInterval
When specified, it will retrieve analytics data with specified aggregation interval. Default is day.
Allowed values are `Day`,`Week` and `Month`.

```yaml
Type: DateTime
Parameter Sets: Analytics by date range

Required: False
Position: Named
Default value: Day
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
