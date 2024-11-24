---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPListItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPListItem
---

# Remove-PnPListItem

## SYNOPSIS

Deletes an item from a list

<a href="https://pnp.github.io/powershell/articles/batching.html">
<img src="https://raw.githubusercontent.com/pnp/powershell/gh-pages/images/batching/Batching.png" alt="Supports Batching">
</a>

## SYNTAX

### Single

```
Remove-PnPListItem [-List] <ListPipeBind> -Identity <ListItemPipeBind> [-Recycle] [-Force]
```

### Batched

```
Remove-PnPListItem [-List] <ListPipeBind> -Identity <ListItemPipeBind> -Batch <PnPBatch> [-Recycle]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove a list item.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPListItem -List "Demo List" -Identity "1" -Force
```

Removes the listitem with id "1" from the "Demo List" list

### EXAMPLE 2

```powershell
Remove-PnPListItem -List "Demo List" -Identity "1" -Force -Recycle
```

Removes the listitem with id "1" from the "Demo List" list and saves it in the Recycle Bin

### EXAMPLE 3

```powershell
$batch = New-PnPBatch
1..50 | Foreach-Object{Remove-PnPListItem -List "DemoList" -Identity $_ -Batch $batch}
Invoke-PnPBatch -Batch $batch
```

Removes all the items with Id 1 to Id 50 in the "Demo List" list

### EXAMPLE 4

```powershell
Remove-PnPListItem -List "Demo List"
```

Removes all items from the "Demo List" list after asking for confirmation

## PARAMETERS

### -Batch

Batch object used to add items in a batched manner. See examples on how to use this.

```yaml
Type: PnPBatch
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Batched
  Position: Named
  IsRequired: true
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

### -Force

Specifying the Force parameter will skip the confirmation question

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

The ID of the listitem, or actual ListItem object

```yaml
Type: ListItemPipeBind
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

### -List

The ID, Title or Url of the list

```yaml
Type: ListPipeBind
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

### -Recycle

When provided, items will be sent to the recycle bin. When omitted, items will permanently be deleted.

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
