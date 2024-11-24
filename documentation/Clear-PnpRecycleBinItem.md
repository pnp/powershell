---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Clear-PnPRecycleBinItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Clear-PnPRecycleBinItem
---

# Clear-PnPRecycleBinItem

## SYNOPSIS

Permanently deletes all or a specific recycle bin item

## SYNTAX

### All (Default)

```
Clear-PnPRecycleBinItem [-All] [-SecondStageOnly] [-Force] [-RowLimit <Int32>]
 [-Connection <PnPConnection>]
```

### Identity

```
Clear-PnPRecycleBinItem -Identity <RecycleBinItemPipeBind> [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to permanently delete items from recycle bin. By default the command will delete all items but it is allowed to specify the items by using the `Identity` or `RowLimit` options.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPRecycleBinItem | Where-Object LeafName -like "*.docx" | Clear-PnPRecycleBinItem
```

Permanently deletes all the items in the first and second stage recycle bins of which the file names have the `.docx` extension

### EXAMPLE 2

```powershell
Clear-PnPRecycleBinItem -Identity 72e4d749-d750-4989-b727-523d6726e442
```

Permanently deletes the recycle bin item with Id 72e4d749-d750-4989-b727-523d6726e442 from the recycle bin

### EXAMPLE 3

```powershell
Clear-PnPRecycleBinItem -Identity $item -Force
```

Permanently deletes the recycle bin item stored under variable `$item` from the recycle bin without asking for confirmation from the end user first

### EXAMPLE 4

```powershell
Clear-PnPRecycleBinItem -All -RowLimit 10000
```

Permanently deletes up to 10,000 items in the recycle bin

## PARAMETERS

### -All

Clears all items

```yaml
Type: SwitchParameter
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

If provided, no confirmation will be asked to clear the recycle bin

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

Id of the recycle bin item or the recycle bin item itself to permanently delete

```yaml
Type: RecycleBinItemPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Identity
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RowLimit

Limits deletion to specified number of items

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
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SecondStageOnly

If provided, only all the items in the second stage recycle bin will be cleared

```yaml
Type: SwitchParameter
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
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
