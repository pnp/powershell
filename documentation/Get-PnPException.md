---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPException.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPException
---

# Get-PnPException

## SYNOPSIS

Returns the last exception that occurred

## SYNTAX

### Default (Default)

```
Get-PnPException [-All]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns the last exception which can be used while debugging PnP Cmdlets

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPException
```

Returns the last exception

### EXAMPLE 2

```powershell
Get-PnPException -All
```

Returns all exceptions that occurred

## PARAMETERS

### -All

Show all exceptions

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
