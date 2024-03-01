---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPLibraryFileVersionExpirationReportJob.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnPLibraryFileVersionExpirationReportJob
---
  
# New-PnPLibraryFileVersionExpirationReportJob

## SYNOPSIS

Starts generating file version usage report for a document library.


## SYNTAX

```powershell
New-PnPLibraryFileVersionExpirationReportJob [-Identity] <ListPipeBind> -ReportUrl <string>
```


## DESCRIPTION

Starts generating file version usage report for a document library.


## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPLibraryFileVersionExpirationReportJob -Identity "Documents" -ReportUrl "https://contoso.sharepoint.com/sites/reports/MyReports/VersionReport.csv"
```

Starts generating file version usage report for a document library, saving the result to a csv file within the site collection.


## PARAMETERS

### -Identity
The ID, name or Url (Lists/MyList) of the document library to gather a file version usage report on.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

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


