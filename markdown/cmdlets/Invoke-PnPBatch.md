---
Module Name: PnP.PowerShell
title: Invoke-PnPBatch
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPBatch.html
---
 
# Invoke-PnPBatch

## SYNOPSIS

Executes the batch

![Supports Batching](../images/batching/Batching.png)

## SYNTAX

```powershell
Invoke-PnPBatch [-Batch] <PnPBatch> [-Details] [-StopOnException] [-Force]
```

## DESCRIPTION

Executes any queued actions / changes in the batch.

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

### -Batch

The batch to execute

```yaml
Type: PnPBatch
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force

Once a batch has been executed you cannot execute it again. Using -Force will allow you to re-execute the batch again.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Details

Will return detailed information of the batch executed.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StopOnException

By default the batch will be fully executed. If any exceptions occur during this process they will be listed after the full run. Specify this switch to stop immediately after an exception occurs. The rest of the batch will be skipped then.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
