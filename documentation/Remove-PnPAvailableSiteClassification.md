---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPAvailableSiteClassification.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPAvailableSiteClassification
---

# Remove-PnPAvailableSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Removes one or more existing classic site classification values from the list of available values on the tenant

## SYNTAX

### Default (Default)

```
Remove-PnPAvailableSiteClassification
 -Classifications <System.Collections.Generic.List`1[System.String]>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove existing classic site classification values.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPAvailableSiteClassification -Classifications "HBI"
```

Removes the "HBI" site classification from the list of available values.

### EXAMPLE 2

```powershell
Remove-PnPAvailableSiteClassification -Classifications "HBI","Top Secret"
```

Removes the "HBI" and "Top Secret" site classification from the list of available values.

## PARAMETERS

### -Classifications



```yaml
Type: System.Collections.Generic.List`1[System.String]
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

### -Force

If provided or set to $true, a confirmation will be asked before the actual remove takes place. If omitted or set to $false, it will remove the site classification(s) without asking for confirmation.

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
