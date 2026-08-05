---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEnterpriseAppInsightsReport.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPEnterpriseAppInsightsReport
---
  
# Get-PnPEnterpriseAppInsightsReport

## SYNOPSIS

**Required Permissions**

  * Microsoft SharePoint API: Sites.ReadWrite.All

Checks for the status of the generation of the App Insights reports and allows downloading them when they're done.

## SYNTAX

### Details on all available reports

```powershell
Get-PnPEnterpriseAppInsightsReport
```

### Details on a specific report

```powershell
Get-PnPEnterpriseAppInsightsReport -ReportId <string>
```

### Download a report

```powershell
Get-PnPEnterpriseAppInsightsReport -ReportId <string> -Action Download
```

## DESCRIPTION

This cmdlet allows checking for the status of generated App Insights reports. Only one report can exist for every supported timespan `day, 7 days, 14 days, or 28 days. New requests for reports can be initiated using [Start-PnPEnterpriseAppInsightsReport](Get-PnPEnterpriseAppInsightsReport.md) and will overwrite any existing reports thay may exist.

This cmdlet also allows for downloading of the report data when the report is ready.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPEnterpriseAppInsightsReport
```

Will return all reports that have been generated or are still being generated.

### EXAMPLE 2
```powershell
Get-PnPEnterpriseAppInsightsReport -ReportId bed8845f-72ba-43ec-b1f3-844ff6a64f28
```

Will return details on the report with the specified ID.

### EXAMPLE 3
```powershell
Get-PnPEnterpriseAppInsightsReport -ReportId bed8845f-72ba-43ec-b1f3-844ff6a64f28 -Action Download
```

Will return the contents of the report with the specified ID as text.

## PARAMETERS

### -ReportId
The amount of days to cover in the report. Valid values are 1, 7, 14, and 28. Default is 1.

```yaml
Type: string
Parameter Sets: Details on a specific report, Download a report

Required: True
Position: Named
Default value: 1
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Action
When provided with the value `Download`, the cmdlet will return the contents of the report as text.

```yaml
Type: short
Parameter Sets: Download a report

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Generate App Insights Reports](https://learn.microsoft.com/sharepoint/app-insights)