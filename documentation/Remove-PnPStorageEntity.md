---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPStorageEntity.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPStorageEntity
---

# Remove-PnPStorageEntity

## SYNOPSIS

Remove Storage Entities / Farm Properties from either the tenant scoped app catalog or the current site collection if the site has a site collection scoped app catalog

## SYNTAX

### Default (Default)

```
Remove-PnPStorageEntity -Key <String> [-Scope <StorageEntityScope>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove Storage Entities / Farm Properties from either the tenant scoped app catalog or the current site collection app catalog.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPStorageEntity -Key MyKey
```

Removes an existing storage entity / farm property.

### EXAMPLE 2

```powershell
Remove-PnPStorageEntity -Key MyKey -Scope Site
```

Removes an existing storage entity from the current site collection.

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

### -Key

The key of the value to remove.

```yaml
Type: String
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

### -Scope

Defines the scope of the storage entity. Defaults to Tenant.

```yaml
Type: StorageEntityScope
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
- Site
- Tenant
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
