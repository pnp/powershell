---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPLibraryFileVersionExpirationReportJobStatus.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPLibraryFileVersionExpirationReportJobStatus
---
  
# Get-PnPLibraryFileVersionExpirationReportJobStatus

## SYNOPSIS

Gets the status for a file version usage report generation job for a document library.

## SYNTAX

```powershell
Get-PnPLibraryFileVersionExpirationReportJobStatus -Identity <ListPipeBind> -ReportUrl <string>
```

## DESCRIPTION

Gets the status for a file version usage report generation job for a document library.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPLibraryFileVersionExpirationReportJobStatus -Identity "Documents" -ReportUrl "https://contoso.sharepoint.com/sites/reports/MyReports/VersionReport.csv"
```

Gets the status for a file version usage report generation job for a document library.

## PARAMETERS

### -Identity
The ID, name or Url (Lists/MyList) of the document library to get the job status on.

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
