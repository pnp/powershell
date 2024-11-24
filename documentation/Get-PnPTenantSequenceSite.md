---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantSequenceSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantSequenceSite
---

# Get-PnPTenantSequenceSite

## SYNOPSIS

Returns one or more sites from a tenant template

## SYNTAX

### Default (Default)

```
Get-PnPTenantSequenceSite -Sequence <ProvisioningSequence> [-Identity <ProvisioningSitePipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve list of sites from tenant template sequence.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantSequenceSite -Sequence $mysequence
```

Returns all sites from the specified sequence

### EXAMPLE 2

```powershell
Get-PnPTenantSequenceSite -Sequence $mysequence -Identity 8058ea99-af7b-4bb7-b12a-78f93398041e
```

Returns the specified site from the specified sequence

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

### -Identity

Optional Id of the site

```yaml
Type: ProvisioningSitePipeBind
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

### -Sequence

The sequence to retrieve the site from

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
