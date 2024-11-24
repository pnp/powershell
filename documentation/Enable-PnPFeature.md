---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Enable-PnPFeature.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Enable-PnPFeature
---

# Enable-PnPFeature

## SYNOPSIS

Enables a feature

## SYNTAX

### Default (Default)

```
Enable-PnPFeature [-Identity] <Guid> [-Force] [-Scope <FeatureScope>] [-Sandboxed]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to enable to feature.

## EXAMPLES

### EXAMPLE 1

```powershell
Enable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will enable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"

### EXAMPLE 2

```powershell
Enable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Force
```

This will enable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe" with force.

### EXAMPLE 3

```powershell
Enable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Web
```

This will enable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe" with the web scope.

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

Specifies whether to overwrite an existing feature with the same feature identifier. This parameter is ignored if there are no errors.

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

The id of the feature to enable.

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Sandboxed

Specify this parameter if the feature you're trying to activate is part of a sandboxed solution.

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

### -Scope

Specify the scope of the feature to activate, either Web or Site. Defaults to Web.

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
