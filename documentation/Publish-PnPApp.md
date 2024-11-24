---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Publish-PnPApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Publish-PnPApp
---

# Publish-PnPApp

## SYNOPSIS

Publishes/Deploys/Trusts an available app in the app catalog

## SYNTAX

### Default (Default)

```
Publish-PnPApp [-Identity] <AppMetadataPipeBind> [-SkipFeatureDeployment] [-Scope <AppCatalogScope>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to deploy/trust an available app in the app catalog.

## EXAMPLES

### EXAMPLE 1

```powershell
Publish-PnPApp -Identity 2646ccc3-6a2b-46ef-9273-81411cbbb60f
```

This will deploy/trust an app into the app catalog. Notice that the app needs to be available in the tenant scoped app catalog

### EXAMPLE 2

```powershell
Publish-PnPApp -Identity 2646ccc3-6a2b-46ef-9273-81411cbbb60f -Scope Site
```

This will deploy/trust an app into the app catalog. Notice that the app needs to be available in the site collection scoped app catalog

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

Specifies the Id of the app

```yaml
Type: AppMetadataPipeBind
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

### -Scope

Defines which app catalog to use. Defaults to Tenant

```yaml
Type: AppCatalogScope
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
- Tenant
- Site
HelpMessage: ''
```

### -SkipFeatureDeployment



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
