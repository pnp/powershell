---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantSequence.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantSequence
---

# Get-PnPTenantSequence

## SYNOPSIS

Returns one ore more provisioning sequence object(s) from a tenant template

## SYNTAX

### Default (Default)

```
Get-PnPTenantSequence -Template <ProvisioningHierarchy> [-Identity <ProvisioningSequencePipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve provisioning sequence objects from a tenant template. By using `Identity` option it is possible to retrieve a specific provisioning sequence object.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantSequence -Template $myTemplateObject
```

Returns all sequences from the specified tenant template

### EXAMPLE 2

```powershell
Get-PnPTenantSequence -Template $myTemplateObject -Identity "mysequence"
```

Returns the specified sequence from the specified tenant template

## PARAMETERS

### -Identity

Optional Id of the sequence

```yaml
Type: ProvisioningSequencePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Template

The template to retrieve the sequence from

```yaml
Type: ProvisioningHierarchy
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
