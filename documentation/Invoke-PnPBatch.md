---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Invoke-PnPBatch.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Invoke-PnPBatch
---

# Invoke-PnPBatch

## SYNOPSIS

Executes the batch

![Supports Batching](../images/batching/Batching.png)

## SYNTAX

### Default (Default)

```
Invoke-PnPBatch [-Batch] <PnPBatch> [-Details] [-StopOnException] [-Force]
```

## ALIASES

This cmdlet has no aliases.

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

### -Details

Will return detailed information of the batch executed.

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

### -Force

Once a batch has been executed you cannot execute it again. Using -Force will allow you to re-execute the batch again.

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

### -StopOnException

By default the batch will be fully executed. If any exceptions occur during this process they will be listed after the full run. Specify this switch to stop immediately after an exception occurs. The rest of the batch will be skipped then.

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
