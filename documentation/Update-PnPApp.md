---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Update-PnPApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Update-PnPApp
---

# Update-PnPApp

## SYNOPSIS

Updates an available app from the app catalog.

## SYNTAX

### Default (Default)

```
Update-PnPApp [-Identity] <AppMetadataPipeBind> [-Scope <AppCatalogScope>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to update an available app from the app catalog.

## EXAMPLES

### EXAMPLE 1

```powershell
Update-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will update an already installed app if a new version is available in the tenant app catalog. Retrieve a list of all available apps and the installed and available versions with Get-PnPApp.

### EXAMPLE 2

```powershell
Update-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Site
```

This will update an already installed app if a new version is available in the site collection app catalog. Retrieve a list of all available apps and the installed and available versions with Get-PnPApp -Scope Site.

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

Specifies the Id or an actual app metadata instance.

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

Defines which app catalog to use. Defaults to Tenant.

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
