---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPListItemAttachment.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPListItemAttachment
---

# Remove-PnPListItemAttachment

## SYNOPSIS

Removes attachment from the specified list item in the SharePoint list.

## SYNTAX

### Remove attachment from list item

```
Remove-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind>
 [-FileName <String>] [-Recycle <SwitchParameter>] [-Force <SwitchParameter>]
 [-Connection <PnPConnection>]
```

### Remove all attachment files from list item

```
Remove-PnPListItemAttachment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind>
 [-All <SwitchParameter>] [-Recycle <SwitchParameter>] [-Force <SwitchParameter>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes one or all attachments from the specified list item in a SharePoint list.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName test.txt
```

Permanently delete an attachment from the list item with Id "1" in the "Demo List" SharePoint list with file name as test.txt.

### EXAMPLE 2

```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName test.txt -Recycle
```

Removes an attachment from the list item with Id "1" in the "Demo List" SharePoint list with file name as test.txt and sends it to recycle bin.

### EXAMPLE 3

```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -FileName test.txt -Recycle -Force
```

Removes an attachment from the list item with Id "1" in the "Demo List" SharePoint list with file name as test.txt and sends it to recycle bin. It will not ask for confirmation from user.

### EXAMPLE 4

```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -All -Recycle -Force
```

Removes all attachments from the list item with Id "1" in the "Demo List" SharePoint list and sends them to recycle bin. It will not ask for confirmation from user.

### EXAMPLE 5

```powershell
Remove-PnPListItemAttachment -List "Demo List" -Identity 1 -All
```

Permanently deletes all attachments from the list item with Id "1" in the "Demo List" SharePoint list and sends them to recycle bin.

## PARAMETERS

### -All

Specify if you want to delete or recycle all the list item attachments.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (Multiple)
  Position: Named
  IsRequired: true
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

### -FileName

Specify name of the attachment to delete from list item. The filename is not case sensitive.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (Single)
  Position: Named
  IsRequired: true
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
Type: String
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

The ID of the list item, or actual ListItem object.

```yaml
Type: ListItemPipeBind
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

### -List

The ID, Title or Url of the list. Note that when providing the name of the list, the list name is case-sensitive.

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

### -Recycle

Specify if you want to send the attachment(s) to the recycle bin.

```yaml
Type: String
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
