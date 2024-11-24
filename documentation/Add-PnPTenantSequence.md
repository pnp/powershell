---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPTenantSequence.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPTenantSequence
---

# Add-PnPTenantSequence

## SYNOPSIS

Adds a tenant sequence object to a tenant template

## SYNTAX

### Default (Default)

```
Add-PnPTenantSequence -Template <ProvisioningHierarchy> -Sequence <ProvisioningSequence>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a tenant sequence object to a tenant template.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTenantSequence -Template $mytemplate -Sequence $mysequence
```

Adds an existing sequence object to an existing template object

### EXAMPLE 2

```powershell
New-PnPTenantSequence -Id "MySequence" | Add-PnPTenantSequence -Template $template
```

Creates a new instance of a provisioning sequence object and sets the Id to the value specified, then the sequence is added to an existing template object

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

### -Sequence

Optional Id of the sequence

```yaml
Type: ProvisioningSequence
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Template

The template to add the sequence to

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
