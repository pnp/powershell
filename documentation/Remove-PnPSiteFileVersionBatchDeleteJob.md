---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteFileVersionBatchDeleteJob.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPSiteFileVersionBatchDeleteJob
---

# Remove-PnPSiteFileVersionBatchDeleteJob

## SYNOPSIS

Cancels further processing of a file version batch trim job for a site collection.

## SYNTAX

### Default (Default)

```
Remove-PnPSiteFileVersionBatchDeleteJob [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Cancels further processing of a file version batch trim job for a site collection.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPSiteFileVersionBatchDeleteJob
```

Cancels further processing of the file version batch trim job for the site collection.

### EXAMPLE 2

```powershell
Remove-PnPSiteFileVersionBatchDeleteJob -Force
```

Cancels further processing of the file version batch trim job for the site collection, without prompting the user for confirmation.

## PARAMETERS

### -Force

When provided, no confirmation prompts will be shown to the user.

```yaml
Type: SwitchParameter
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
