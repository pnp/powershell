---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Start-PnPEnterpriseAppInsightsReport.html
external help file: PnP.PowerShell.dll-Help.xml
title: Start-PnPEnterpriseAppInsightsReport
---
  
# Start-PnPEnterpriseAppInsightsReport

## SYNOPSIS

**Required Permissions**

  * Microsoft SharePoint API: Sites.ReadWrite.All

Generates a report for the App Insights data.

## SYNTAX

```powershell
Start-PnPEnterpriseAppInsightsReport [-ReportPeriodInDays <1, 7, 14, 28>] 
```

## DESCRIPTION

This cmdlet will start the generation of a new App Insights report. It can generate a report over the past day (default), 7 days, 14 days, or 28 days. It will overwrite any existing report for the same period. The report will be generated in the background. Use [Get-SPOEnterpriseAppInsightsReport](Get-SPOEnterpriseAppInsightsReport.md) to check the status of the report.

## EXAMPLES

### EXAMPLE 1
```powershell
Start-PnPEnterpriseAppInsightsReport
```

A report will be generated which covers the past day.

### EXAMPLE 2
```powershell
Start-PnPEnterpriseAppInsightsReport 28
```

A report will be generated which covers the past 28 days.

## PARAMETERS

### -ReportPeriodInDays
The amount of days to cover in the report. Valid values are 1, 7, 14, and 28. Default is 1.

```yaml
Type: short
Parameter Sets: (All)

Required: True
Position: Named
Default value: 1
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Generate App Insights Reports](https://learn.microsoft.com/sharepoint/app-insights)