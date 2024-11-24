---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPList.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPList
---

# Remove-PnPList

## SYNOPSIS

Deletes a list.

## SYNTAX

### Default (Default)

```
Remove-PnPList [-Identity] <ListPipeBind> [-Recycle] [-LargeList] [-Force]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove a list.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPList -Identity Announcements
```

Removes the list named 'Announcements'. Asks for confirmation.

### EXAMPLE 2

```powershell
Remove-PnPList -Identity Announcements -Force
```

Removes the list named 'Announcements' without asking for confirmation.

### EXAMPLE 3

```powershell
Remove-PnPList -Identity Announcements -Recycle
```

Removes the list named 'Announcements' and moves it to the Recycle Bin.

### EXAMPLE 4

```powershell
Remove-PnPList -Identity Announcements -Recycle -LargeList
```

Removes the large list named 'Announcements' and moves it to the Recycle Bin.
Run Get-PnPLargeListOperationStatus -ListId <ListId> -OperationId <OperationId> to check the status of the operation.

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

Specifying the Force parameter will skip the confirmation question.

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

The ID or Title of the list.

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

### -LargeList

When provided, the large list will be moved to recycle bin through a timer job. It must be paired with the Recycle Parameter.

```yaml
Type: SwitchParameter
DefaultValue: False
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

### -Recycle

When provided, the list will be moved to recycle bin. If omitted, the list will directly be deleted.

```yaml
Type: SwitchParameter
DefaultValue: False
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
