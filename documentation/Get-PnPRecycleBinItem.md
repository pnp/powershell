---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPRecycleBinItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPRecycleBinItem
---

# Get-PnPRecycleBinItem

## SYNOPSIS

**Required Permissions**

* SharePoint: Site Collection Administrator. SharePoint Tenant Admin alone is not enough

Returns one or more items from the Recycle Bin.

## SYNTAX

### All (Default)

```
Get-PnPRecycleBinItem [-RowLimit <Int32>] [-Connection <PnPConnection>] [-Includes <String[]>]
```

### Identity

```
Get-PnPRecycleBinItem [-Identity <Guid>] [-Connection <PnPConnection>] [-Includes <String[]>]
```

### FirstStage

```
Get-PnPRecycleBinItem [-FirstStage] [-RowLimit <Int32>] [-Connection <PnPConnection>]
 [-Includes <String[]>]
```

### SecondStage

```
Get-PnPRecycleBinItem [-SecondStage] [-RowLimit <Int32>] [-Connection <PnPConnection>]
 [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command will return all the items in the recycle bin for the SharePoint site you connected to with Connect-PnPOnline. You must connect as a Site Collection Owner or Administrator. The SharePoint Admin Role in the tenant alone will not work. If you are not a Site Collection Admin connect to the Tenant Admin URL with Connect-PnPOnline and use Get-PnPTenantRecycleBinItem.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPRecycleBinItem
```

Returns all items in both the first and the second stage recycle bins in the current site collection.

### EXAMPLE 2

```powershell
Get-PnPRecycleBinItem -Identity f3ef6195-9400-4121-9d1c-c997fb5b86c2
```

Returns a specific recycle bin item by id.

### EXAMPLE 3

```powershell
Get-PnPRecycleBinItem -FirstStage
```

Returns all items in only the first stage recycle bin in the current site collection.

### EXAMPLE 4

```powershell
Get-PnPRecycleBinItem -SecondStage
```

Returns all items in only the second stage recycle bin in the current site collection.

### EXAMPLE 5

```powershell
Get-PnPRecycleBinItem -RowLimit 10000
```

Returns items in recycle bin limited by number of results.

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

### -FirstStage

Returns all items in the first stage recycle bin

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: FirstStage
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

Returns a recycle bin item with a specific identity.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Includes

Optionally allows properties to be retrieved for the returned recycle bin items which are not included in the response by default

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

### -RowLimit

Limits returned results to specified amount

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: FirstStage
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SecondStage
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SecondStage

Returns all items in the second stage recycle bin.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: SecondStage
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
