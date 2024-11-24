---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPApp
---

# Get-PnPApp

## SYNOPSIS

Returns the available apps from the app catalog

## SYNTAX

### Default (Default)

```
Get-PnPApp [-Identity <AppMetadataPipeBind>] [-Scope <AppCatalogScope>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve available apps from the app catalog. In order to get apps from site collection scoped app catalog set `Scope` option to `Site`.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPApp
```

This will return all available apps from the tenant app catalog. It will list the installed version in the current site.

### EXAMPLE 2

```powershell
Get-PnPApp -Scope Site
```

This will return all available apps from the site collection scoped app catalog. It will list the installed version in the current site.

### EXAMPLE 3

```powershell
Get-PnPApp -Identity 2646ccc3-6a2b-46ef-9273-81411cbbb60f
```

This will retrieve the specific app from the app catalog.

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

Specifies the Id of an app which is available in the app catalog

```yaml
Type: AppMetadataPipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
