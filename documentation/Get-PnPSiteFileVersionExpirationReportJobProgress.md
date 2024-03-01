---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteFileVersionExpirationReportJobProgress.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSiteFileVersionExpirationReportJobProgress
---
  
# Get-PnPSiteFileVersionExpirationReportJobProgress

## SYNOPSIS

Gets the status for a file version usage report generation job for a site collection.


## SYNTAX

```powershell
Get-PnPSiteFileVersionExpirationReportJobProgress -ReportUrl <string>
```


## DESCRIPTION

Gets the status for a file version usage report generation job for a site collection.


## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteFileVersionExpirationReportJobProgress -ReportUrl "https://contoso.sharepoint.com/sites/reports/MyReports/VersionReport.csv"
```

Gets the status for a file version usage report generation job for a site collection.


## PARAMETERS

### -ReportUrl
The URL of the report to get the job status on.

```yaml
Type: string
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


