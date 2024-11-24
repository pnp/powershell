---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Move-PnPRecycleBinItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Move-PnPRecycleBinItem
---

# Move-PnPRecycleBinItem

## SYNOPSIS

Moves all items or a specific item in the first stage recycle bin of the current site collection to the second stage recycle bin

## SYNTAX

### Default (Default)

```
Move-PnPRecycleBinItem [-Identity <RecycleBinItemPipeBind>] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Move-PnPRecycleBinItem
```

Moves all the items in the first stage recycle bin of the current site collection to the second stage recycle bin

### EXAMPLE 2
```powershell
Move-PnPRecycleBinItem -Identity 26ffff29-b526-4451-9b6f-7f0e56ba7125
```

Moves the item with the provided ID in the first stage recycle bin of the current site collection to the second stage recycle bin

### EXAMPLE 3
```powershell
Move-PnPRecycleBinItem -Force
```

Moves all the items in the first stage recycle bin of the current context to the second stage recycle bin without asking for confirmation first

## EXAMPLES

### EXAMPLE 1

```powershell
Move-PnPRecycleBinItem
```

Moves all the items in the first stage recycle bin of the current site collection to the second stage recycle bin

### EXAMPLE 2

```powershell
Move-PnPRecycleBinItem -Identity 26ffff29-b526-4451-9b6f-7f0e56ba7125
```

Moves the item with the provided ID in the first stage recycle bin of the current site collection to the second stage recycle bin

### EXAMPLE 3

```powershell
Move-PnPRecycleBinItem -Force
```

Moves all the items in the first stage recycle bin of the current context to the second stage recycle bin without asking for confirmation first

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

If provided, no confirmation will be asked to move the first stage recycle bin items to the second stage

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

If provided, moves the item with the specific ID to the second stage recycle bin

```yaml
Type: RecycleBinItemPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
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
