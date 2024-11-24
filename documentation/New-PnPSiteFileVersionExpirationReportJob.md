---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPSiteFileVersionExpirationReportJob.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPSiteFileVersionExpirationReportJob
---

# New-PnPSiteFileVersionExpirationReportJob

## SYNOPSIS

Starts generating file version usage report for a site collection.

## SYNTAX

### Default (Default)

```
New-PnPSiteFileVersionExpirationReportJob -ReportUrl <string>
```

## ALIASES

This cmdlet has no aliases.

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
