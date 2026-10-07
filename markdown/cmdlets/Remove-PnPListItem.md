---
Module Name: PnP.PowerShell
title: Remove-PnPListItem
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPListItem.html
---
 
# Remove-PnPListItem

## SYNOPSIS

Deletes an item from a list

<a href="https://pnp.github.io/powershell/articles/batching.html">
<img src="https://raw.githubusercontent.com/pnp/powershell/gh-pages/images/batching/Batching.png" alt="Supports Batching">
</a>

## SYNTAX

### Single

```powershell
Remove-PnPListItem [-List] <ListPipeBind> -Identity <ListItemPipeBind> [-Recycle] [-Force] 
```

### Batched

```powershell
Remove-PnPListItem [-List] <ListPipeBind> -Identity <ListItemPipeBind> -Batch <PnPBatch> [-Recycle]
```

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

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force

Specifying the Force parameter will skip the confirmation question

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The ID of the listitem, or actual ListItem object

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List

The ID, Title or Url of the list

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Recycle

When provided, items will be sent to the recycle bin. When omitted, items will permanently be deleted.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Batch

Batch object used to add items in a batched manner. See examples on how to use this.

```yaml
Type: PnPBatch
Parameter Sets: Batched

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
