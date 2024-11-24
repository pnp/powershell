---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPTenantSequence.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPTenantSequence
---

# New-PnPTenantSequence

## SYNOPSIS

Creates a new tenant sequence object

## SYNTAX

### Default (Default)

```
New-PnPTenantSequence [-Id <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to create a new tenant sequence object.

## EXAMPLES

### EXAMPLE 1

```powershell
$sequence = New-PnPTenantSequence
```

Creates a new instance of a tenant sequence object.

### EXAMPLE 2

```powershell
$sequence = New-PnPTenantSequence -Id "MySequence"
```

Creates a new instance of a tenant sequence object and sets the Id to the value specified.

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- cf
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

### -Id

Optional Id of the sequence

```yaml
Type: String
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
