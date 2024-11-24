---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPSdnProvider.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPSdnProvider
---

# New-PnPSdnProvider

## SYNOPSIS

Adds a new Software-Defined Networking (SDN) provider

## SYNTAX

### Default (Default)

```
New-PnPSdnProvider -Identity <String> -License <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This Cmdlet creates a new Software-Defined Networking, and it receives two parameters, the Identity (ID) of the Hive and the License key of the Hive.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPSdnProvider -ID "Hive" -License ""
```

This example creates the Hive for a SDN Provider.

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

### -Identity



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

### -License



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
