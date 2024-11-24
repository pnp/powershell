---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPLibraryFileVersionExpirationReportJob.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPLibraryFileVersionExpirationReportJob
---

# New-PnPLibraryFileVersionExpirationReportJob

## SYNOPSIS

Starts generating file version usage report for a document library.

## SYNTAX

### Default (Default)

```
New-PnPLibraryFileVersionExpirationReportJob -Identity <ListPipeBind> -ReportUrl <string>
```

## ALIASES

This cmdlet has no aliases.

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

The URL of the report to save to.

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
