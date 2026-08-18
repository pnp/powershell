---
Module Name: PnP.PowerShell
title: Move-PnPRecycleBinItem
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Move-PnPRecycleBinItem.html
---
 
# Move-PnPRecycleBinItem

## SYNOPSIS
Moves all items or a specific item in the first stage recycle bin of the current site collection to the second stage recycle bin

## SYNTAX

```powershell
Move-PnPRecycleBinItem [-Identity <RecycleBinItemPipeBind>] [-Force] [-Connection <PnPConnection>]
 
```

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
If provided, no confirmation will be asked to move the first stage recycle bin items to the second stage

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
If provided, moves the item with the specific ID to the second stage recycle bin

```yaml
Type: RecycleBinItemPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

