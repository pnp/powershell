---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPBatch.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPBatch
---

# New-PnPBatch

## SYNOPSIS

Creates a new batch

![Supports Batching](../images/batching/Batching.png)

## SYNTAX

### Default (Default)

```
New-PnPBatch [-RetainRequests]
```

## ALIASES

This cmdlet has no aliases.

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
