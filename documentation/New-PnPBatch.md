---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/new-pnpbatch
schema: 2.0.0
title: New-PnPBatch
---

# New-PnPBatch

## SYNOPSIS
Creates a new batch

## SYNTAX

```powershell
New-PnPBatch
```

## DESCRIPTION
Creates a new batch to be used by cmdlets that support batching

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

## PARAMETERS

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)