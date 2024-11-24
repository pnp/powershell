---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteFileVersionBatchDeleteJobStatus.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteFileVersionBatchDeleteJobStatus
---

# Get-PnPSiteFileVersionBatchDeleteJobStatus

## SYNOPSIS

Get the progress of deleting existing file versions on the site.

## SYNTAX

### Default (Default)

```
Get-PnPSiteFileVersionBatchDeleteJobStatus [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows retrieval of the progress of deleting existing file versions on the site.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteFileVersionBatchDeleteJobStatus
```

Returns the progress of deleting existing file versions on the site.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
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
