---
Module Name: PnP.PowerShell
title: New-PnPBatch
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPBatch.html
---
 
# New-PnPBatch

## SYNOPSIS

Creates a new batch

![Supports Batching](../images/batching/Batching.png)

## SYNTAX

```powershell
New-PnPBatch [-RetainRequests]
```

## DESCRIPTION

Creates a new batch to be used by cmdlets that support batching. The requests in the batch are clear after execute Invoke-PnPBatch, unless you specify -RetainRequests. That allows you to execute batch multiple times.

## EXAMPLES

### EXAMPLE 1

```powershell
$batch = New-PnPBatch
Add-PnPListItem -List "DemoList" -Values @{"Title"="Demo Item 1"} -Batch $batch
Add-PnPListItem -List "DemoList" -Values @{"Title"="Demo Item 2"} -Batch $batch
Add-PnPListItem -List "DemoList" -Values @{"Title"="Demo Item 3"} -Batch $batch
Invoke-PnPBatch -Batch $batch
```

This will add the 3 defined list items in the batch.

### EXAMPLE 2

```powershell
$batch = New-PnPBatch
1..50 | Foreach-Object{Remove-PnPListItem -List "DemoList" -Identity $_ -Batch $batch}
Invoke-PnPBatch -Batch $batch
```

This will delete all the items with Id 1 to Id 50 in the specified list.

## PARAMETERS

### -RetainRequests

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
