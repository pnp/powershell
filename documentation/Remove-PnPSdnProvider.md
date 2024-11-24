---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPSdnProvider.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPSdnProvider
---

# Remove-PnPSdnProvider

## SYNOPSIS

Removes Software-Defined Networking (SDN) Support in your SharePoint Online tenant.

## SYNTAX

### Default (Default)

```
Remove-PnPSdnProvider [-Confirm]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes SDN Support in your SharePoint Online tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPSdnProvider -Confirm:false
```

This command removes the SDN support for your Online Tenant without confirmation.

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

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
