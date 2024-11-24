---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPChangeLog.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPChangeLog
---

# Get-PnPChangeLog

## SYNOPSIS

Returns the changelog for PnP PowerShell

## SYNTAX

### Default (Default)

```
Get-PnPChangeLog [-Nightly]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlets returns the changelog in markdown format. It is retrieved dynamically from GitHub.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPChangeLog
```

Returns the changelog for the currently released version.

### EXAMPLE 2

```powershell
Get-PnPChangeLog -Nightly
```

Returns the changelog for the current nightly build.

## PARAMETERS

### -Nightly

Return the changelog for the nightly build

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
