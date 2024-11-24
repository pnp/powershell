---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Disable-PnPFeature.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Disable-PnPFeature
---

# Disable-PnPFeature

## SYNOPSIS

Disables a feature

## SYNTAX

### Default (Default)

```
Disable-PnPFeature [-Identity] <Guid> [-Force] [-Scope <FeatureScope>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Deactivates a feature that was active on a site

## EXAMPLES

### EXAMPLE 1

```powershell
Disable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will disable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"

### EXAMPLE 2

```powershell
Disable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Force
```

This will disable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe" with force.

### EXAMPLE 3

```powershell
Disable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Web
```

This will disable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe" with the web scope.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
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

### -Force

Specifies whether to continue if an error occurs when deactivating the feature.

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

The id of the feature to disable.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Scope

Specify the scope of the feature to deactivate, either Web or Site. Defaults to Web.

```yaml
Type: FeatureScope
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
AcceptedValues:
- Web
- Site
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
