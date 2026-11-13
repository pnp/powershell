---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPSiteFileVersionExpirationReportJob.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnPSiteFileVersionExpirationReportJob
---
  
# New-PnPSiteFileVersionExpirationReportJob

## SYNOPSIS

Starts generating file version usage report for a site collection.

## SYNTAX

```powershell
New-PnPSiteFileVersionExpirationReportJob -ReportUrl <string>
```

## DESCRIPTION

Starts generating file version usage report for a site collection.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPSiteFileVersionExpirationReportJob -ReportUrl "https://contoso.sharepoint.com/sites/reports/MyReports/VersionReport.csv"
```

Starts generating file version usage report on for the site collection, saving the result to a csv file within the site collection.

## PARAMETERS

### -ReportUrl
The URL of the report to save to.

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
