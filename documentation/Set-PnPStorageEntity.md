---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPStorageEntity.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPStorageEntity
---

# Set-PnPStorageEntity

## SYNOPSIS

Set Storage Entities / Farm Properties in either the tenant scoped app catalog or the site collection app catalog.

## SYNTAX

### Default (Default)

```
Set-PnPStorageEntity -Key <String> -Value <String> [-Comment <String>] [-Description <String>]
 [-Scope <StorageEntityScope>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to set Storage Entities / Farm Properties in either the tenant scoped app catalog or the site collection app catalog.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPStorageEntity -Key MyKey -Value "MyValue" -Comment "My Comment" -Description "My Description"
```

Sets an existing or adds a new storage entity / farm property at the tenant level.

### EXAMPLE 2

```powershell
Set-PnPStorageEntity -Scope Site -Key MyKey -Value "MyValue" -Comment "My Comment" -Description "My Description"
```

Sets an existing or adds a new storage entity at the site collection level.

## PARAMETERS

### -Comment

Specifies additional comments related to the storage entity being set.

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

### -Description

The description to set.

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

### -Key

The key of the value to set.

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
DefaultValue: Tenant
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

### -Value

The value to set.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
