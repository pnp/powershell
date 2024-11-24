---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Restore-PnPRecycleBinItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Restore-PnPRecycleBinItem
---

# Restore-PnPRecycleBinItem

## SYNOPSIS

Restores the provided recycle bin item to its original location.

## SYNTAX

### Default (Default)

```
Restore-PnPRecycleBinItem -Identity <RecycleBinItemPipeBind> [-Force] [-RowLimit <Int32>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet restores the specified item from the recycle bin to its original location.

## EXAMPLES

### EXAMPLE 1

```powershell
Restore-PnPRecycleBinItem -Identity 72e4d749-d750-4989-b727-523d6726e442
```

Restores the recycle bin item with Id 72e4d749-d750-4989-b727-523d6726e442 to its original location.

### EXAMPLE 2

```powershell
Get-PnPRecycleBinItem | ? -Property LeafName -like "*.docx" | Restore-PnPRecycleBinItem
```

Restores all the items of which the filename ends with the .docx extension from the first and second stage recycle bins to their original location.

### EXAMPLE 3

```powershell
Get-PnPRecycleBinItem -RowLimit 10000 | Restore-PnPRecycleBinItem -Force
```

Permanently restores up to 10,000 items in the recycle bin without asking for confirmation.

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

If provided, no confirmation will be asked to restore the recycle bin item.

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

Id of the recycle bin item or the recycle bin item object itself to restore.

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

### -RowLimit

Limits restoration to specified number of items.

```yaml
Type: Int32
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
