---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPStorageEntity.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPStorageEntity
---

# Get-PnPStorageEntity

## SYNOPSIS

Retrieve Storage Entities / Farm Properties from either the Tenant App Catalog or from the current site if it has a site scope app catalog.

## SYNTAX

### Default (Default)

```
Get-PnPStorageEntity [-Key <String>] [-Scope <StorageEntityScope>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve storage entities from either tenant app catalog or current site app catalog.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPStorageEntity
```

Returns all site storage entities/farm properties

### EXAMPLE 2

```powershell
Get-PnPStorageEntity -Key MyKey
```

Returns the storage entity/farm property with the given key.

### EXAMPLE 3

```powershell
Get-PnPStorageEntity -Scope Site
```

Returns all site collection scoped storage entities

### EXAMPLE 4

```powershell
Get-PnPStorageEntity -Key MyKey -Scope Site
```

Returns the storage entity from the site collection with the given key

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

The key of the value to retrieve.

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
