---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPLibraryFileVersionExpirationReportJobStatus.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPLibraryFileVersionExpirationReportJobStatus
---

# Get-PnPLibraryFileVersionExpirationReportJobStatus

## SYNOPSIS

Gets the status for a file version usage report generation job for a document library.

## SYNTAX

### Default (Default)

```
Get-PnPLibraryFileVersionExpirationReportJobStatus -Identity <ListPipeBind> -ReportUrl <string>
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
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

### -ReportUrl

The URL of the report to get the job status on.

```yaml
Type: string
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
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
