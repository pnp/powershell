---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPApp
---

# Add-PnPApp

## SYNOPSIS

Add/uploads an available app to the app catalog

## SYNTAX

### Default (Default)

```
Add-PnPApp [-Path] <String> [-Scope <AppCatalogScope>] [-Overwrite] [-Timeout <Int32>]
 [-SkipFeatureDeployment] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to upload an app to the app catalog at tenant or site collection level. By specifying `-Publish` option it is possible to deploy/trust it at the same time.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPApp -Path ./myapp.sppkg
```

This will upload the specified app package to the tenant app catalog

### EXAMPLE 2

```powershell
Add-PnPApp -Path ./myapp.sppkg -Publish
```

This will upload the specified app package to the tenant app catalog and deploy/trust it at the same time.

### EXAMPLE 3

```powershell
Add-PnPApp -Path ./myapp.sppkg -Scope Site -Publish
```

This will upload the specified app package to the site collection app catalog and deploy/trust it at the same time.

### EXAMPLE 4

```powershell
Add-PnPApp -Path ./myapp.sppkg -Publish -SkipFeatureDeployment
```

This will upload the specified app package to the tenant app catalog, deploy/trust it and make it globally available on all site collections.

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

### -Overwrite

When provided, it will overwrite the existing app package if it already exists

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

### -Path

The path to the app package to deploy to the App Catalog

```yaml
Type: String
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

### -Publish

This will deploy/trust an app into the App Catalog

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

Defines which app catalog to use: the site collection scoped App Catalog or the tenant wide App Catalog. Defaults to Tenant.

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

When provided, the solution will be globally deployed, meaning one does not have to go into every site to add it as an app to have its components available. Instead they will be available rightaway.

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

### -Timeout

Specifies the timeout in seconds. Defaults to 200.

```yaml
Type: Int32
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
