---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteFileVersionExpirationReportJobStatus.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteFileVersionExpirationReportJobStatus
---

# Get-PnPSiteFileVersionExpirationReportJobStatus

## SYNOPSIS

Gets the status for a file version usage report generation job for a site collection.

## SYNTAX

### Default (Default)

```
Get-PnPSiteFileVersionExpirationReportJobStatus -ReportUrl <string>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Gets the status for a file version usage report generation job for a site collection.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteFileVersionExpirationReportJobStatus -ReportUrl "https://contoso.sharepoint.com/sites/reports/MyReports/VersionReport.csv"
```

Gets the status for a file version usage report generation job for a site collection.

## PARAMETERS

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
