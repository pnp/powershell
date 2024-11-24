---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFeature.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFeature
---

# Get-PnPFeature

## SYNOPSIS

Returns all activated or a specific activated feature

## SYNTAX

### Default (Default)

```
Get-PnPFeature [[-Identity] <FeaturePipeBind>] [-Scope <FeatureScope>] [-Connection <PnPConnection>]
 [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns all activated features or a specific activated feature.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPFeature
```

This will return all activated web scoped features

### EXAMPLE 2

```powershell
Get-PnPFeature -Scope Site
```

This will return all activated site scoped features

### EXAMPLE 3

```powershell
Get-PnPFeature -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22
```

This will return a specific activated web scoped feature

### EXAMPLE 4

```powershell
Get-PnPFeature -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22 -Scope Site
```

This will return a specific activated site scoped feature

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

### -Identity

The feature ID or name to query for, Querying by name is not supported in version 15 of the Client Side Object Model

```yaml
Type: FeaturePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Includes

Optionally allows properties to be retrieved for the returned feature which are not included in the response by default

```yaml
Type: String[]
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

The scope of the feature. Defaults to Web.

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
