---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPTenantSequenceSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPTenantSequenceSite
---

# Add-PnPTenantSequenceSite

## SYNOPSIS

Adds an existing tenant sequence site object to a tenant template

## SYNTAX

### Default (Default)

```
Add-PnPTenantSequenceSite -Site <ProvisioningSitePipeBind> -Sequence <ProvisioningSequence>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add an existing tenant sequence site object to a tenant template.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTenantSequenceSite -Site $myteamsite -Sequence $mysequence
```

Adds an existing site object to an existing template sequence

## PARAMETERS

### -Sequence

The sequence to add the site to

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

### -Site



```yaml
Type: ProvisioningSitePipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
