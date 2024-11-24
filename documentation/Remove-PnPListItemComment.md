---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPListItemComment.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPListItemComment
---

# Remove-PnPListItemComment

## SYNOPSIS

Deletes a comment or all comments from a list item in a SharePoint list.

## SYNTAX

### Single

```
Remove-PnPListItemComment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-Text] [-Force]
```

### All

```
Remove-PnPListItemComment [-List] <ListPipeBind> [-Identity] <ListItemPipeBind> [-All] [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove comments from list item.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPListItemComment -List "Demo List" -Identity "1" -Text "test comment" -Force
```

Removes the comment with text "test comment" from list item with id "1" from the "Demo List" list. The text needs to be case sensitive. It may not work with comments containing mentions.

### EXAMPLE 2

```powershell
Remove-PnPListItemComment -List "Demo List" -Identity "1" -Text "test comment"
```

Removes the comment with text "test comment" from list item with id "1" from the "Demo List" list after asking for confirmation. The text needs to be case sensitive. It will may work with comments containing mentions.

### EXAMPLE 3

```powershell
Remove-PnPListItemComment -List "Demo List" -Identity "1" -All -Force
```

Removes all comments from list item with id "1" from the "Demo List" list.

### EXAMPLE 4

```powershell
Remove-PnPListItemComment -List "Demo List" -Identity "1" -All
```

Removes all comments from list item with id "1" from the "Demo List" list after asking for confirmation.

## PARAMETERS

### -All

When specified, it will delete all comments for the specified list item.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Multiple
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

### -Force

Specifying the Force parameter will skip the confirmation question

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

The ID of the listitem, or actual ListItem object

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

The ID, Title or Url of the list

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

### -Text

When provided, item comments with specified text will be deleted. The text is case sensitive. If the comment contains mentions, it may not work.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Single
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
